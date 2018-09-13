var restify = require('restify');
var builder = require('botbuilder');
require('dotenv').config();
const { exec } = require('child_process');

// Setup Restify Server
var server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () {
    console.log(process.env.MicrosoftAppId)
    console.log(process.env.MicrosoftAppPassword)
    console.log('%s listening to %s', server.name, server.url);
});

server.get("/static/*", restify.plugins.serveStatic({
    directory: __dirname
}));

// Create chat connector for communicating with the Bot Framework Service
var connector = new builder.ChatConnector({
    appId: process.env.MicrosoftAppId,
    appPassword: process.env.MicrosoftAppPassword
});

// Listen for messages from users 
server.post('/api/messages', connector.listen());

// Receive messages from the user and respond by echoing each message back (prefixed with 'You said:')
var bot = new builder.UniversalBot(connector, function (session) {
    console.log(GetDate() + " Message received")
    var message = session.message.text
    if (message.toLocaleLowerCase().startsWith("download")) {
        DownloadFromYoutube(session, message)
    }
    else {
        session.send("You said: %s", session.message.text);
    }
    console.log(GetDate() + " Message sent")
}).set('storage', new builder.MemoryBotStorage());

DownloadFromYoutube = (session, message) => {
    var splitMessage = message.split(" ")

    if (splitMessage.length == 2) {
        var url = splitMessage[1].replace("<", "").replace(">", "")
        var id = url.split("?v=")
        console.log("Downloading " + id[id.length - 1] + ".mp4 from " + url)

        exec('youtube-dl -o "./static/%(id)s.%(ext)s" ' + url , (err, stdout, stderr) => {
            if (err) {
                console.log("Failed to save video: " + err)
                session.send("Failed to save video: " + err)
                return
            }
            else {
                console.log("https://majunga.co.uk:3978/static/"+ id[id.length - 1] + ".mp4")
                session.send("https://majunga.co.uk:3978/static/"+ id[id.length - 1] + ".mp4")
            }
        })
    }
}

GetDate = () => {
    var currentdate = new Date()
    return currentdate.getDate() + "/"
        + (currentdate.getMonth() + 1) + "/"
        + currentdate.getFullYear() + " @ "
        + currentdate.getHours() + ":"
        + currentdate.getMinutes() + ":"
        + currentdate.getSeconds()
}