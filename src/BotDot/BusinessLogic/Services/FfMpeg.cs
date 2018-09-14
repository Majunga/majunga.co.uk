namespace BotDot.BusinessLogic.Services
{
    using BotDot.BusinessLogic.Services.Interfaces;
    using BotDot.Helpers;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class FFMpeg : IVideoConverter
    {
        private string outputPath;

        public FFMpeg(string outputPath)
        {
            this.outputPath = outputPath;
        }

        public async Task<FileInfo> ConvertToMp4(FileInfo file, Tuple<string, string> times)
        {
            var path = FileHelper.GetFullPath(this.outputPath);
            var newFilename = $"{path}/{file.Name.Replace(file.Extension, string.Empty)}Cut.mp4";
            var arguments = $"-y -i {file.FullName} -f mp4 -strict -2 -c copy";

            if (!string.IsNullOrWhiteSpace(times?.Item1))
            {
                arguments += $" -ss {times.Item1}";
            }

            if (!string.IsNullOrWhiteSpace(times?.Item2))
            {
                arguments += $" -to {times.Item2}";
            }

            arguments += $" -frame_size 160 {newFilename}";

            await new ProcessHelper().Run("ffmpeg", arguments);

            return new FileInfo(newFilename);
        }
    }
}
