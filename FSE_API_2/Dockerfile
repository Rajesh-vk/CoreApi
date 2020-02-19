#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.2-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["FSE_API_2/FSE_API_2.csproj", "FSE_API_2/"]
RUN dotnet restore "FSE_API_2/FSE_API_2.csproj"
COPY . .
WORKDIR "/src/FSE_API_2"
RUN dotnet build "FSE_API_2.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FSE_API_2.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FSE_API_2.dll"]