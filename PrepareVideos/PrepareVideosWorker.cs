using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PrepareVideos
{
	public class PrepareVideosWorker : Worker
	{

        public PrepareVideosWorker(string BaseUrl, string TaskName) : base(BaseUrl, TaskName) { }

		public async override Task<JObject> RunWorker(JObject inputData)
		{
			
			
			var tasksI = inputData["videosToProcess"].ToObject<IList<Video>>();
			var tasks = new List<TaskConfig>();

			Console.WriteLine(tasksI.Count);

			Dictionary<string, Video> mapTasks = new Dictionary<string, Video>();
			for (int i = 0; i < tasksI.Count; i++)
			{
				mapTasks.Add("key_" + i, tasksI[i]);

				tasks.Add(new TaskConfig
				{
					Name = "download_and_process",
					TaskReferenceName = "key_" + i,
					Type = "SUB_WORKFLOW",
					SubWorkflowParam = new TaskConfig.SubWorkflowParamModel { Name = "download_and_process", Version = 1 }
				});
			}

			var response = new PreparedVideosOutput
			{
				DynamicTasksI = mapTasks,
				DynamicTasks = tasks

			};

			
			string jsonString = JsonConvert.SerializeObject(response);

			
			JObject jsonResponse = JObject.Parse(jsonString);

			Console.WriteLine(jsonResponse);

			return jsonResponse;
		}
	}
}
