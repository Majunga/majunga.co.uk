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
    if (message.toLocaleLowerCase().startsWith("bot download")) {
        DownloadFromYoutube(session, message)
    }
    else if (message.toLocaleLowerCase().startsWith("bot")) {
        HelpMessage(session)
    }
}).set('storage', new builder.MemoryBotStorage());

HelpMessage = (session) => {
    session.send("Bot Help. To use this bot please type one of the following commands:");
    session.send("bot download [Youtube URL]");
}

DownloadFromYoutube = (session, message) => {
    var splitMessage = message.split(" ")

    if (splitMessage.length >= 3) {
        var url = splitMessage[2].replace("<", "").replace(">", "")
        var id = url.split("?v=")


        if (splitMessage.length > 3) {
            console.log("Cutting video")
            var times = { start : "", end : "" }

            times.start = splitMessage[splitMessage.indexOf("--time") + 1]
            times.end = splitMessage[splitMessage.indexOf("--to") + 1]

            if (!ValidateTime(times.start) || !ValidateTime(times.end)) {
                session.send("Invalid Times. Times should be in the following format:")
                session.send("--time HH:MM:SS --to HH:MM:SS")
                return
            }

            ExecYoutubeDL(session, id[id.length - 1], url, times)
        }
        else {
            ExecYoutubeDL(session, id[id.length - 1], url)

        }
    }
}

ValidateTime = (value) => {
    return /^([0-5][0-9]):([0-5][0-9]):([0-5][0-9])$/.test(value);
}

ExecYoutubeDL = (session, id, url, times) => {
    console.log("Downloading " + id + ".mkv from " + url)
    console.log(times)
    session.send("Downloading video")

    exec('youtube-dl --restrict-filenames -o "./static/%(id)s.%(ext)s" ' + url, {maxBuffer: 1024 * 500}, (err, stdout, stderr) => {
        if (err) {
            console.log("Failed to save video: " + err)
            session.send("Failed to save video: " + err)
            return
        }
        else {
            console.log(process.env.URL + "/static/" + id + ".mkv")

            if (times) {
                session.send("Video has been downloaded. Please wait while I cut it up!")
                exec('ffmpeg -i "/usr/src/app/static/' + id + '.mkv" -ss ' + times.start + ' -to ' + times.end + ' -c copy /usr/src/app/static/' + id + 'Cut.mkv', (err, stdout, stderr) => {
                    if (err) {
                        console.log("Failed to save video: " + err)
                        session.send("Failed to save video: " + err)
                        return
                    }
                    else {
                        session.send("The video has now been butchered:")
                        session.send(process.env.URL + "/static/" + id + "Cut.mkv")
                    }
                })
            }
            else {
                session.send("Video has been downloaded")
                session.send(process.env.URL + "/static/" + id + ".mkv")
            }

        }
    })
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