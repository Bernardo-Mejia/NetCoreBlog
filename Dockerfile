FROM mcr.microsoft.com/dotnet/sdk:7.0 AS AUX
WORKDIR webapp

EXPOSE 80
EXPOSE 5001

# COPY ./*.csproj ./

# Copiar el archivo de solución y los archivos de proyecto.
COPY *.sln .
COPY AppBlogCore/*.csproj AppBlogCore/
COPY AppBlogCore.DataAccess/*.csproj AppBlogCore.DataAccess/
COPY AppBlogCore.Models/*.csproj AppBlogCore.Models/
COPY AppBlogCore.Utilities/*.csproj AppBlogCore.Utilities/

RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o outDir

FROM mcr.microsoft.com/dotnet/sdk:7.0
workdir /webapp
COPY --from=AUX /webapp/outDir .
RUN mkdir -p /webapp/wwwroot/Images/Articles && chmod -R 777 /webapp/wwwroot/Images/Articles
ENTRYPOINT ["dotnet", "AppBlogCore.dll"]