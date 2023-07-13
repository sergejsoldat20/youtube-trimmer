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

			



			var process = new Process();
            process.StartInfo.FileName = FFMPEG;
            process.StartInfo.RedirectStandardOutput = true;

			Console.WriteLine(Path.GetExtension(downloadPath));

			var extension = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + Guid.NewGuid() + $".{Path.GetExtension(downloadPath)}";

			
			var args = new[] { "-i", downloadPath, "-ss", from, "-t", length, "-c:v", "copy", "-c:a", "copy", extension };
			
           
           

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
