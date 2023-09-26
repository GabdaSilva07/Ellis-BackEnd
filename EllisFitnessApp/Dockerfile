# Use the official image as a parent image.
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Set the working directory.
WORKDIR /app

# Copy solution and restore dependencies
COPY *.sln .
COPY API/*.csproj ./API/
COPY MongoDb/*.csproj ./MongoDb/
COPY Logger/*.csproj ./Logger/
COPY Authentification/*.csproj ./Authentification/
COPY FCM.Messaging/*.csproj ./FCM.Messaging/
COPY FirebaseService/*.csproj ./FirebaseService/
COPY Domain/*.csproj ./Domain/ 
RUN dotnet restore

# Copy everything else and build the app
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

# Create a non-root user and switch to it
RUN useradd -m myuser
USER myuser

ENTRYPOINT ["dotnet", "API.dll"]