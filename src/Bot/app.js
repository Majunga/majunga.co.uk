var restify = require('restify');
var builder = require('botbuilder');
require('dotenv').config();

// Setup Restify Server
var server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () {
    console.log(process.env.MicrosoftAppId)
    console.log(process.env.MicrosoftAppPassword)
    console.log('%s listening to %s', server.name, server.url); 
});

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
    session.send("You said: %s", session.message.text);
    console.log(GetDate() + " Message sent")
}).set('storage', new builder.MemoryBotStorage());



GetDate = () => {
    var currentdate = new Date()
    return currentdate.getDate() + "/"
                + (currentdate.getMonth()+1)  + "/" 
                + currentdate.getFullYear() + " @ "  
                + currentdate.getHours() + ":"  
                + currentdate.getMinutes() + ":" 
                + currentdate.getSeconds()
}