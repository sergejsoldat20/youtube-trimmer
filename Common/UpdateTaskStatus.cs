using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common
{
    public class UpdateTaskStatus
    {
        [JsonProperty("workflowInstanceId")]
        public string WorkflowInstanceId { get; set; }

        [JsonProperty("taskId")]
        public string TaskId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("outputData")]
        public JObject OutputData { get; set; }
    }
}
