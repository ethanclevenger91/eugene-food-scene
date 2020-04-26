using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AirtableApiClient;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;


namespace EugeneFoodScene.Data
{
    public class AirTableService
    {
        private readonly string baseId;
        private readonly string appKey;

        private List<AirtableRecord<Place>> _records;

        public List<AirtableRecord<Place>> Records => _records ??= GetPlacesAsync().Result;

        public AirTableService(IConfiguration configuration)
        {
            baseId = configuration["AirTable:BaseId"];
            appKey = configuration["AirTable:AppKey"];
        }

        public async Task<List<AirtableRecord<Place>>> GetPlacesAsync()
        {
            var records = new List<AirtableRecord<Place>>();
            string offset = null;

            using (AirtableBase airtableBase = new AirtableBase(appKey, baseId))
            {
                do
                {

                    Task<AirtableListRecordsResponse<Place>> task = airtableBase.ListRecords<Place>(tableName:"Places", offset: offset );

                    var response = await task;

                    if (response.Success)
                    {
                        records.AddRange(response.Records);
                        offset = response.Offset;
                    }
                    else if (response.AirtableApiError is AirtableApiException)
                    {
                        throw new Exception(response.AirtableApiError.ErrorMessage);
                        break;
                    }
                    else
                    {
                        throw new Exception("Unknown error");
                        break;
                    }

                } while (offset != null) ;

                return records;
            }
        }
    }
}
