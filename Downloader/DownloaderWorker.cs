using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace Downloader
{
	public class DownloaderWorker : Worker
	{

		public static string Ytdlp = "C:\\Users\\serge\\Downloads\\yt-dlp.exe";
		public DownloaderWorker(string BaseUrl, string TaskName) : base(BaseUrl, TaskName) { }

		public async override Task<JObject> RunWorker(JObject inputData)
		{

			var guidValue = $"{Guid.NewGuid()}.%(ext)s";
			var outputFileName = await GetYtdlpFilename(Ytdlp, guidValue, inputData["youtubeLink"] + "");

			await DownloadVideo(Ytdlp, guidValue, Directory.GetCurrentDirectory() + outputFileName, inputData["youtubeLink"] + "");
			var downloadPath = Directory.GetCurrentDirectory() + outputFileName;

			var response = new DownloaderResponse { DownloadPath = downloadPath };

			string jsonString = JsonConvert.SerializeObject(response);


			JObject jsonResponse = JObject.Parse(jsonString);


			return jsonResponse;
		}

		public static async Task<string> GetYtdlpFilename(string ytdlp, string fileFormat, string youtubeLink)
		{
			var process = new Process();
			process.StartInfo.FileName = ytdlp;
			process.StartInfo.RedirectStandardOutput = true;
			var args = new[] { "--print", "filename", "-o", fileFormat, youtubeLink };
			foreach (var arg in args)
			{
				process.StartInfo.ArgumentList.Add(arg);
			}

			process.Start();
			var outputFilename = await process.StandardOutput.ReadToEndAsync();
			outputFilename = outputFilename.Replace("\n", string.Empty);
			await process.WaitForExitAsync();
			return outputFilename;
		}

		public static async Task DownloadVideo(string ytdlp, string fileFormat, string downloadPath,
			string youtubeLink)
		{
			var downloadProcess = Process.Start(ytdlp, new[] { "-o", fileFormat, "-P", downloadPath, youtubeLink });
			await downloadProcess.WaitForExitAsync();
		}
	}
}
