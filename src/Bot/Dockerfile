FROM node:6-onbuild

# Create app directory
RUN mkdir -p /usr/src/app 
WORKDIR /usr/src/app

# Install youtube-dl
RUN curl -L https://yt-dl.org/downloads/latest/youtube-dl -o /usr/local/bin/youtube-dl
RUN chmod a+rx /usr/local/bin/youtube-dl
RUN export PATH=$PATH:/usr/local/bin/youtube-dl

# Install app dependencies
COPY package.json /usr/src/app/
RUN npm install

# Bundle app source
COPY . /usr/src/app

# Install ffmpeg
RUN mkdir /tmp/ffmpeg
WORKDIR /tmp/ffmpeg
RUN wget https://johnvansickle.com/ffmpeg/builds/ffmpeg-git-64bit-static.tar.xz
RUN wget https://johnvansickle.com/ffmpeg/builds/ffmpeg-git-64bit-static.tar.xz.md5
RUN md5sum -c ffmpeg-git-64bit-static.tar.xz.md5
RUN tar xvf ffmpeg-git-64bit-static.tar.xz

RUN mv /tmp/ffmpeg/ffmpeg-git-20180913-64bit-static/ffmpeg /usr/local/bin/ffmpeg
RUN chmod a+rx /usr/local/bin/ffmpeg
RUN export PATH=$PATH:/usr/local/bin/ffmpeg

WORKDIR /usr/src/app
ENTRYPOINT ["node", "app.js"]

EXPOSE 3978