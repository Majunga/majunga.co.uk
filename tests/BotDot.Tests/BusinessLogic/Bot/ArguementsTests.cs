using BotDot.BusinessLogic.Bot;
using BotDot.BusinessLogic.Bot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BotDot.Tests.BusinessLogic.Bot
{
    /// <summary>
    /// Tests for the Arguments Class
    /// </summary>
    public class ArguementsTests
    {
        [Fact]
        public void CanAction_WhiteSpace_IsFalse()
        {
            Assert.False(new ArguementsHandler("").CanAction());
        }

        [Fact]
        public void CanAction_Null_IsFalse()
        {
            Assert.False(new ArguementsHandler(null).CanAction());
        }

        [Fact]
        public void CanAction_RandomText_IsFalse()
        {
            Assert.False(new ArguementsHandler("afsdgdsg").CanAction());
        }

        [Fact]
        public void CanAction_Bot_IsTrue()
        {
            Assert.True(new ArguementsHandler("BoT").CanAction());
        }

        [Fact]
        public void GetCommand_WhenIncorrectCommandReturnHelp()
        {
            Assert.True(new ArguementsHandler("BoT dfghhd").GetCommand() == CommandHandler.Commands.Help);
        }

        [Fact]
        public void GetCommand_WhenHelpCommandReturnHelp()
        {
            Assert.True(new ArguementsHandler("BoT HELP").GetCommand() == CommandHandler.Commands.Help);
        }


        [Fact]
        public void GetCommand_WhenDoWnLoadCommandReturnDownload()
        {
            Assert.True(new ArguementsHandler("BoT DoWnLoad").GetCommand() == CommandHandler.Commands.Download);
        }

        [Fact]
        public void GetCommand_WhenDownloadCommandReturnDownload()
        {
            Assert.True(new ArguementsHandler("BoT download").GetCommand() == CommandHandler.Commands.Download);
        }

        [Fact]
        public void GetDownloadCommandArguements_WhenNoArguementsReturnEmptyList()
        {
            Assert.True(new ArguementsHandler("BoT download").GetDownloadCommandArguements().Count == 0);
        }

        [Fact]
        public void GetDownloadCommandArguements_WhenArgIsUrl_ReturnUrlEnumAndValue()
        {
            var url = "https://google.com";
            var result = new ArguementsHandler($"BoT download {url}").GetDownloadCommandArguements()[Download.CommandArguements.Url];

            Assert.Equal(result, url);
        }

        [Fact]
        public void GetDownloadCommandArguements_WhenArgIsBadUrl_ReturnNoUrl()
        {
            var url = "https://googlcom%";
            var result = new ArguementsHandler($"BoT download {url}").GetDownloadCommandArguements();

            Assert.True(result.Count == 0);
        }

        [Fact]
        public void GetDownloadCommandArguements_WhenTimesAreSpecified_ReturnTimeArguements()
        {
            var timeStart = "00:00:01";
            var timeEnd = "00:00:10";
            var result = new ArguementsHandler($"BoT download --start {timeStart} --end {timeEnd}").GetDownloadCommandArguements();

            Assert.True(result.Count == 2);
            Assert.True(result[Download.CommandArguements.Start] == timeStart);
            Assert.True(result[Download.CommandArguements.End] == timeEnd);
        }


        [Fact]
        public void GetDownloadCommandArguements_WhenAllArguementsAreSpecified_ReturnAll()
        {
            var url = "https://google.com";
            var timeStart = "00:00:01";
            var timeEnd = "00:00:10";
            var result = new ArguementsHandler($"BoT download --start {timeStart} --end {timeEnd}  {url}").GetDownloadCommandArguements();

            Assert.True(result.Count == 3);
            Assert.True(result[Download.CommandArguements.Start] == timeStart);
            Assert.True(result[Download.CommandArguements.End] == timeEnd);
            Assert.Equal(result[Download.CommandArguements.Url], url);
        }
    }
}
