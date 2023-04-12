using System.Collections.Generic;

namespace langchain.Schema
{
    public class LLMResult
    {
        public List<List<Generation>> Generations { get; set; }
        public IDictionary<string, object> LlmOutput { get; set; }
    }
}
