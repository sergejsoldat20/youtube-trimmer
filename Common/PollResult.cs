using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common
{
    public class PollResult
    {
        [JsonProperty("workflowInstanceId")]
        public string WorkflowInstanceId { get; set; }

        [JsonProperty("taskId")]
        public string TaskId { get; set; }

        [JsonProperty("inputData")]
        public JObject InputData { get; set; }
    }
}
