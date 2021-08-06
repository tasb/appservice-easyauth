FROM microsoft/dotnet-framework:4.7.2-sdk-windowsservercore-1803 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY . .
RUN nuget restore
RUN msbuild WebApp.sln /p:Configuration=Release

# copy everything else and build app


FROM microsoft/aspnet:4.7.2-windowsservercore-1803 AS runtime
WORKDIR /inetpub/wwwroot
COPY --from=build /app/WebApp/. ./