FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
# Install youtube-dl
ADD https://yt-dl.org/downloads/latest/youtube-dl /usr/local/bin/youtube-dl
RUN chmod a+rx /usr/local/bin/youtube-dl; \
    PATH=$PATH:/usr/local/bin/youtube-dl

# Install ffmpeg
RUN mkdir /tmp/ffmpeg
WORKDIR /tmp/ffmpeg
ADD dependencies/ffmpeg-git-64bit-static.tar.xz .
    
RUN mv ./ffmpeg-git-20180913-64bit-static/ffmpeg /usr/local/bin/ffmpeg; \
    chmod a+rx /usr/local/bin/ffmpeg; \
    export PATH=$PATH:/usr/local/bin/ffmpeg; \
    rm -rf /tmp/ffmpeg;

EXPOSE 3978

# Build
FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["./Backend.csproj", "src/Backend/"]
RUN dotnet restore "src/Backend/Backend.csproj"
COPY [".", "src/Backend/"]
WORKDIR "src/Backend"
RUN dotnet build "Backend.csproj" -c Release -o /app

#Publish
FROM build AS publish
RUN dotnet publish "Backend.csproj" -c Release -o /app

#Finals
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Backend.dll"]