#FROM harbor.zhidiancloud.com/common/netcore3.1-alpine
FROM harbor.zhidiancloud.com/common/net6.0:latest
WORKDIR /app
COPY bin/publish /app

ARG ENVIRONMENT

ENV ASPNETCORE_ENVIRONMENT=${ENVIRONMENT} ASPNETCORE_URLS="http://*:80"

EXPOSE 80
ENTRYPOINT ["dotnet", "WMS.WebApi.dll"]