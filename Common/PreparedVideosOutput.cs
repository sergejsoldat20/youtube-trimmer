using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public class PreparedVideosOutput
	{
		[JsonProperty("dynamicTasks")]
		public List<TaskConfig> DynamicTasks { get; set; }
		[JsonProperty("dynamicTasksI")]
		public Dictionary<string, Video> DynamicTasksI { get; set; }
	}
}
