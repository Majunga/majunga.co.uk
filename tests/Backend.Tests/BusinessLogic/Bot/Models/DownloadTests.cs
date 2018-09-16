namespace BackendTests.BusinessLogic.Bot.Models
{
    using Backend.BusinessLogic.Bot.Models;
    using Xunit;

    public class DownloadTests
    {
        [Fact]
        public void Validate_WhenUriIsNull_FailWithMessage()
        {
            var result = new Download().Validate();
            Assert.True(!result.Item1 && result.Item2 == "Failed Url not specified or invalid");
        }

        [Fact]
        public void Validate_WhenStartTimeInvalid_FailWithMessage()
        {
            var model = new Download
            {
                Uri = new System.Uri("https://google.com"),
                Start = "dfgdfgd"
            };

            var result = model.Validate();
            Assert.True(!result.Item1 && result.Item2 == "Failed Start time is invalid. All times should be in HH:MM:SS format");
        }

        [Fact]
        public void Validate_WhenEndTimeInvalid_FailWithMessage()
        {
            var model = new Download
            {
                Uri = new System.Uri("https://google.com"),
                End = "dfgdfgd"
            };

            var result = model.Validate();
            Assert.True(!result.Item1 && result.Item2 == "Failed End time is invalid. All times should be in HH:MM:SS format");
        }

        [Fact]
        public void Validate_WhenValidUri_SuccesAndNoMessage()
        {
            var model = new Download
            {
                Uri = new System.Uri("https://google.com"),
            };

            var result = model.Validate();
            Assert.True(result.Item1 && result.Item2 == string.Empty);
        }

        [Fact]
        public void Validate_WhenValidUriAndTimes_SuccesAndNoMessage()
        {
            var model = new Download
            {
                Uri = new System.Uri("https://google.com"),
                Start = "00:00:30",
                End = "00:01:00"
            };

            var result = model.Validate();
            Assert.True(result.Item1 && result.Item2 == string.Empty);
        }


        [Fact]
        public void Validate_WhenValidUriAndEndTimeGreaterThanStartTime_FailWithMessage()
        {
            var model = new Download
            {
                Uri = new System.Uri("https://google.com"),
                Start = "00:01:00",
                End = "00:00:30"
            };

            var result = model.Validate();
            Assert.True(!result.Item1 && result.Item2 == "Failed End time cannot be greater than Start time");
        }
    }
}
