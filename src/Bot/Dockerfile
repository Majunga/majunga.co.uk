FROM node:6-onbuild

# Install youtube-dl
RUN curl -L https://yt-dl.org/downloads/latest/youtube-dl -o /usr/local/bin/youtube-dl
RUN chmod a+rx /usr/local/bin/youtube-dl
RUN export PATH=$PATH:/usr/local/bin/youtube-dl

# Create app directory
RUN mkdir -p /usr/src/app 
WORKDIR /usr/src/app

# Install app dependencies
COPY package.json /usr/src/app/
RUN npm install

# Bundle app source
COPY . /usr/src/app
ENTRYPOINT ["node", "app.js"]

EXPOSE 3978