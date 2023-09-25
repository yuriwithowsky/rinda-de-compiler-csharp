FROM mcr.microsoft.com/dotnet/sdk:7.0-bullseye-slim AS build
WORKDIR /.

COPY . ./

RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /rinha-compiler

COPY --from=build /out .

ENTRYPOINT ["dotnet", "RinhaDeCompiladores.dll"]