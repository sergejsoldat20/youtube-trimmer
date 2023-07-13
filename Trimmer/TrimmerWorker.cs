using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trimmer
{
	public class TrimmerWorker : Worker
	{

		public static string FFMPEG = "C:\\Users\\serge\\Downloads\\ffmpeg-master-latest-win64-gpl\\ffmpeg-master-latest-win64-gpl\\bin\\ffmpeg.exe";

		public TrimmerWorker(string BaseUrl, string TaskName) : base(BaseUrl, TaskName) { }

		public async override Task<JObject> RunWorker(JObject inputData)
		{
			var length = inputData["length"] + "";
			var from = inputData["from"] + "";
			var downloadPath = inputData["videoPath"] + "";

			
			var firstFile = Directory.GetFiles(downloadPath).ToList().First() + "";




			var process = new Process();
            process.StartInfo.FileName = FFMPEG;
            process.StartInfo.RedirectStandardOutput = true;

			var a = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + Guid.NewGuid() + $".{firstFile.Split(@".")[firstFile.Split(@".").Length - 1]}";

			
			var args = new[] { "-i", firstFile, "-ss", from, "-t", length, "-c:v", "copy", "-c:a", "copy", a == null ? "" : a };
			
           
           

            foreach (var arg in args)
            {
                process.StartInfo.ArgumentList.Add(arg);
                Console.WriteLine(arg);
            }

            process.Start();
            var outputFilename = await process.StandardOutput.ReadToEndAsync();
            outputFilename = outputFilename.Replace("\n", string.Empty);
            await process.WaitForExitAsync();
            //return outputFilename;
            return null;
		}

		
	}
}
