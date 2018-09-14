var restify = require('restify');
require('dotenv').config();

//Setup Restify Server
var server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () {
    console.log(process.env.MicrosoftAppId)
    console.log(process.env.MicrosoftAppPassword)
    console.log('%s listening to %s', server.name, server.url);
});