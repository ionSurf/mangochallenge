FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build-image
 
WORKDIR /home/app/
 
COPY ./mangochallenge/MangoChallenge.csproj ./mangochallenge/
COPY ./src/Controllers/Controllers.csproj ./src/Controllers/
COPY ./src/Entities/Entities.csproj ./src/Entities/
COPY ./src/Helpers/Helpers.csproj ./src/Helpers/
COPY ./src/LoggerService/LoggerService.csproj ./src/LoggerService/
COPY ./src/Repositories/Repositories.csproj ./src/Repositories/
COPY ./src/UserService/UserService.csproj ./src/UserService/
COPY ./Tests/Tests.csproj ./Tests/
COPY ./*.sln ./

RUN dotnet restore
 
COPY . .
 
RUN dotnet test ./Tests/Tests.csproj
 
RUN dotnet publish ./mangochallenge/MangoChallenge.csproj -o /publish/
 
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
 
WORKDIR /publish
 
COPY --from=build-image /publish .
COPY ./media/*.jpg /publish/media/
COPY ./media/*.jpeg /publish/media/
COPY ./nlog.config /publish/
EXPOSE 5000/tcp
ENV ASPNETCORE_URLS="http://0.0.0.0:5000"
ENV ASPNETCORE_ENVIRONMENT="Production"
 
ENTRYPOINT ["dotnet", "MangoChallenge.dll"]