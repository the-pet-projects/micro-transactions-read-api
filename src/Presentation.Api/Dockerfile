FROM microsoft/aspnetcore:2.0
ARG source
RUN echo "source: $source"

WORKDIR /app

COPY ${source:-/build} .
EXPOSE 80

ENTRYPOINT ["dotnet", "PetProjects.MtsManagementApi.API.dll"]
