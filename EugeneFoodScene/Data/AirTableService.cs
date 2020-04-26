using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirtableApiClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;


namespace EugeneFoodScene.Data
{
    public class AirTableService
    {
        private readonly string baseId;
        private readonly string appKey;

        public AirTableService(IConfiguration configuration)
        {
            baseId = configuration["AirTable:BaseId"];
            appKey = configuration["AirTable:AppKey"];
        }

        public async Task<List<AirtableRecord>> GetFoodAsync()
        {
            var records = new List<AirtableRecord>();
            string offset = null;

            using (AirtableBase airtableBase = new AirtableBase(appKey, baseId))
            {
                do
                {

                    Task<AirtableListRecordsResponse> task = airtableBase.ListRecords("Company", offset);

                    AirtableListRecordsResponse response = await task;

                    if (response.Success)
                    {
                        records.AddRange(response.Records.ToList());
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
