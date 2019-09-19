FROM mcr.microsoft.com/dotnet/core/runtime:3.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY ["AzureARMTagger/AzureARMTagger.csproj", "."]
RUN dotnet restore "AzureARMTagger.csproj"
COPY ./AzureARMTagger/ .
RUN dotnet build "AzureARMTagger.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AzureARMTagger.csproj" -c Release -o /app

FROM base AS final
RUN mkdir /app/volume
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT dotnet AzureARMTagger.dll $pathToTemplateFile $pathToTagFile