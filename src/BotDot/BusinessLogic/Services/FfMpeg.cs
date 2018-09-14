namespace BotDot.BusinessLogic.Services
{
    using BotDot.BusinessLogic.Services.Interfaces;
    using System;
    using System.Collections.Generic;
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

        public FileInfo ConvertToMp4(FileInfo file, Tuple<string, string> times)
        {
            throw new NotImplementedException();
        }
    }
}
