using System.Collections.Generic;

namespace langchain.LLMs
{
    public interface OpanAIParam
    {
        int Temperature { get; set; }
        int MaxTokens { get; set; }
        int TopP { get; set; }
        int FrequencyPenalty { get; set; }
        int PresencePenalty { get; set; }
        int N { get; set; }
        int BestOf { get; set; }
        Dictionary<string, int> LogitBias { get; set; }
        bool Streaming { get; set; }
        string ModelName { get; set; }
        Dictionary<string, object> ModelKwargs { get; set; }
        int BatchSize { get; set; }
        string[] Stop { get; set; }
        int Timeout { get; set; }
    }


    public class OpanAI
    {
    }
}
