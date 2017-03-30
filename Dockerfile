FROM microsoft/dotnet:1.1.1-sdk

LABEL vendor="Protacon Solutions" \
  com.protacon.version="1.0.0"

COPY . /var/src/

WORKDIR /var/src/

RUN dotnet restore
RUN dotnet build

WORKDIR /var/src/bin/release/netcoreapp1.1/

EXPOSE 5000

ENTRYPOINT ["dotnet", "citd-backend.dll"]

