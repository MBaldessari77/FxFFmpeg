# inspired by https://github.com/dotnet/dotnet-docker/blob/master/samples/aspnetapp/Dockerfile
# build "docker build -t mb1977/fxffmpeg ."
# run image example "docker run -it --rm -p 8000:80 -v /n/films:/app/media mb1977/fxffmpeg" mapping port 80->8000 path /app/media->/n/films

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# copy everything else and build app
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "FxFFmpeg.Web.dll"]
