FROM node:6-onbuild

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

WORKDIR /usr/src/app
# Install app dependencies
COPY package.json /usr/src/app/
RUN npm install

# Create app directory
RUN mkdir -p /usr/src/app 


# Bundle app source
COPY . /usr/src/app

ENTRYPOINT ["node", "app.js"]

EXPOSE 3978