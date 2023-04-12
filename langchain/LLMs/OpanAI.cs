using System.Collections.Generic;

namespace langchain.LLMs
{
    public class OpanAI
    {
        /** Sampling temperature to use */
        private double Temperature { get; set; }
        /**
        * Maximum number of tokens to generate in the completion. -1 returns as many
        * tokens as possible given the prompt and the model's maximum context size.
        */
        private int MaxTokens { get; set;}
        /** Total probability mass of tokens to consider at each step */
        private int TopP { get; set; }
        /** Penalizes repeated tokens according to frequency */
        private int FrequencyPenalty { get; set; }
        /** Penalizes repeated tokens */
        private int PresencePenalty { get; set; }
        /** Number of completions to generate for each prompt */
        private int N { get; set; }
        /** Generates `bestOf` completions server side and returns the "best" */
        private int BestOf { get; set; }
        /** Dictionary used to adjust the probability of specific tokens being generated */
        private IDictionary<string,int>? LogitBias { get; set; }
        /** Whether to stream the results or not. Enabling disables tokenUsage reporting */
        private bool Streaming { get; set; }
        /** Model name to use */
        private string ModelName { get; set; }
        /** Holds any additional parameters that are valid to pass to {@link
        * https://platform.openai.com/docs/api-reference/completions/create |
        * `openai.createCompletion`} that are not explicitly specified on this class.
        */
        private IDictionary<string,object>? ModelKwargs { get; set; }
        /** Batch size to use when passing multiple documents to generate */
        private int BatchSize { get; set; }
        /** List of stop words to use when generating */
        private string[]? Stop { get; set; }
        /**
        * Timeout to use when making requests to OpenAI.
        */
        private int? Timeout { get; set; }

        public OpanAI(double temperature = 0.7, int maxTokens = 256, int topP = 1, int frequencyPenalty = 0, int presencePenalty = 0, int n = 1, int bestOf = 1, bool streaming = false, IDictionary<string, int> logitBias = null, string modelName = "text-davinci-003", IDictionary<string, object> modelKwargs = null, int batchSize = 20, string[] stop = null, int? timeout = null)
        {
            Temperature = temperature;
            MaxTokens = maxTokens;
            TopP = topP;
            FrequencyPenalty = frequencyPenalty;
            PresencePenalty = presencePenalty;
            N = n;
            BestOf = bestOf;
            LogitBias = logitBias;
            Streaming = streaming;
            ModelName = modelName;
            ModelKwargs = modelKwargs;
            BatchSize = batchSize;
            Stop = stop;
            Timeout = timeout;
        }
    }
}
