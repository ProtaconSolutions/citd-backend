<<<<<<< HEAD
FROM centos:7
=======
FROM centos:7.1.1503
>>>>>>> 6677768301d8f617bddb3f430086bb069777f42f

MAINTAINER henri.bjorkman@protacon.com

LABEL vendor="Protacon Solutions" \
  com.protacon.version="1.0.0"

RUN mkdir -p /opt/dotnet \
<<<<<<< HEAD
  && yum update -y \
  && yum install -y tar \
  libunwind \
  icu \
  && yum clean all \
=======
  && yum install -y tar \
  libunwind \
  icu \
>>>>>>> 6677768301d8f617bddb3f430086bb069777f42f
  && curl https://download.microsoft.com/download/8/C/9/8C9182C7-9DCD-40C1-B72A-BEC4C3FC1FC1/dotnet-dev-centos-x64.1.0.0-preview2.1-003155.tar.gz -o /dotnet.tar.gz \
  && tar xzf /dotnet.tar.gz -C /opt/dotnet \
  && ln -s /opt/dotnet/dotnet /usr/local/bin \
  && rm -rf /dotnet.tar.gz \
  && mkdir /srv/citd-backend

COPY bin/release/netcoreapp1.1/publish/ /srv/citd-backend/

WORKDIR /srv/citd-backend/

ENTRYPOINT ["dotnet"]

EXPOSE 5000

CMD ["citd-backend.dll"]
