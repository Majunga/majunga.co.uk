namespace BotDot.Tests.BusinessLogic.Services
{
    using BotDot.BusinessLogic.Services;
    using System;
    using Xunit;
    public class FFMpegTests
    {
        
        [Fact]
        public void ConvertToMp4_GoodUrl_ReturnsFileInfoOfDownloadedVideo()
        {
            var downloadedVideo = new Youtube_Dl("").DownloadVideo(new Uri($"https://www.youtube.com/watch?v=uq5MtA33OHk")).Result;

            var times = Tuple.Create<string, string>(null, null);

            var ffmpeg = new FFMpeg("").ConvertToMp4(downloadedVideo, times).Result;


            if (downloadedVideo.Exists)
            {
                downloadedVideo.Delete();
            }

            if (ffmpeg.Exists)
            {
                ffmpeg.Delete();
            }

            Assert.NotNull(ffmpeg);
        }

        [Fact]
        public void ConvertToMp4_GoodUrlWithStartTime_ReturnsFileInfoOfDownloadedVideo()
        {
            var downloadedVideo = new Youtube_Dl("").DownloadVideo(new Uri($"https://www.youtube.com/watch?v=uq5MtA33OHk")).Result;

            var times = Tuple.Create<string, string>("00:00:05", null);

            var ffmpeg = new FFMpeg("").ConvertToMp4(downloadedVideo, times).Result;


            if (downloadedVideo.Exists)
            {
                downloadedVideo.Delete();
            }

            if (ffmpeg.Exists)
            {
                ffmpeg.Delete();
            }

            Assert.NotNull(ffmpeg);
        }
    }
}
