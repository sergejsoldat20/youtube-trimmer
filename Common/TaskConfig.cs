using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	
	public class TaskConfig
	{
		public class SubWorkflowParamModel
		{
			[JsonProperty("name")]
			public string Name { get; set; }
			[JsonProperty("version")]
			public int Version { get; set; }
		}

		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("taskReferenceName")]
		public string TaskReferenceName { get; set; }
		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("subWorkflowParam")]
		public SubWorkflowParamModel SubWorkflowParam { get; set; }
	}
}
