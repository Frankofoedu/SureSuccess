#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["../SureSuccess.UpdateSvc/SureSuccess.UpdateSvc.csproj", "../SureSuccess.UpdateSvc/"]
COPY ["SureSuccess.Shared/SureSuccess.Shared.csproj", "SureSuccess.Shared/"]
RUN dotnet restore "../SureSuccess.UpdateSvc/SureSuccess.UpdateSvc.csproj"
COPY . .
WORKDIR "/src/../SureSuccess.UpdateSvc"
RUN dotnet build "SureSuccess.UpdateSvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SureSuccess.UpdateSvc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SureSuccess.UpdateSvc.dll"]
