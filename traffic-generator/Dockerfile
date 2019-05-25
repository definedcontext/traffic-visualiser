FROM microsoft/dotnet:2.2-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Dctx.TrafficGenerator.csproj ./
RUN dotnet restore ./Dctx.TrafficGenerator.csproj
COPY . .
WORKDIR /src/.
RUN dotnet build Dctx.TrafficGenerator.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Dctx.TrafficGenerator.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TrafficGenerator.dll"]
