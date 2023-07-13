using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public abstract class Worker
	{
		public string BaseUrl { get; set; }
        public string TaskName { get; set; }

        public Worker(string BaseUrl, string TaskName)
        {
            this.BaseUrl = BaseUrl;
			this.TaskName = TaskName;
        }

		public abstract Task<JObject> RunWorker(JObject inputData);

        public async Task Polling()
        {
			var httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl) };

			while (true)
			{
				Thread.Sleep(500);

				var response = await httpClient.GetAsync($"tasks/poll/{TaskName}");

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					PollResult pollResult = JsonConvert.DeserializeObject<PollResult>(await response.Content.ReadAsStringAsync());
					Console.WriteLine(pollResult.InputData);
					var value = await RunWorker(pollResult.InputData);
					var status = new UpdateTaskStatus
					{
						WorkflowInstanceId = pollResult.WorkflowInstanceId,
						TaskId = pollResult.TaskId,
						Status = "COMPLETED",
						OutputData = value
					};

					await httpClient.PostAsync("tasks", new StringContent(JsonConvert.SerializeObject(status).ToString()
						, Encoding.UTF8, "application/json"));
				}
				else if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
				{
					continue;
				}
				else {
					throw new Exception("Http status 400");
				}
			}
		}
	}
}
