FROM mvpstudio/dotnet:v01 as base
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["EugeneFoodScene.csproj", ""]
RUN dotnet restore "./EugeneFoodScene.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "EugeneFoodScene.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EugeneFoodScene.csproj" -c Release -o /app/publish

FROM base AS final
USER mvp
WORKDIR /home/mvp/app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "EugeneFoodScene.dll"]
