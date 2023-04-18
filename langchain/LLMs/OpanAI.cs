using langchain.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharpToken;
using System.Text;

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

        public OpanAI(double temperature = 0.7, int maxTokens = 256, int topP = 1, int frequencyPenalty = 0, int presencePenalty = 0, int n = 1, int bestOf = 1, bool streaming = false, 
            IDictionary<string, int> logitBias = null, string modelName = "text-davinci-003", IDictionary<string, object> modelKwargs = null, int batchSize = 20, string[] stop = null, int? timeout = null)
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

        /**
        * Call out to OpenAI's endpoint with k unique prompts
        *
        * @param prompts - The prompts to pass into the model.
        * @param [stop] - Optional list of stop words to use when generating.
        *
        * @returns The full LLM output.
        *
        * @example
        * ```ts
        * import { OpenAI } from "langchain/llms/openai";
        * const openai = new OpenAI();
        * const response = await openai.generate(["Tell me a joke."]);
        * ```
        */
        public async Task<LLMResult> Generate(string[] prompts, string[]? stop)
        {
            var subPrompts = chunkArray(prompts, this.BatchSize);

            var tokenUsage = new TokenUsage();

            if (this.Stop != null && stop != null)
            {
                throw new Exception("Stop found in input and default params");
            }

            if (MaxTokens == -1)
            {
                if (prompts.Length != 1)
                {
                    throw new Exception("MaxTokens set to -1 not supported for multiple inputs");
                }
                MaxTokens = await calculateMaxTokens(prompts[0], ModelName);
            }

            throw new NotImplementedException();
        }

        public async Task<string> Call(string prompt, string[]? stop)
        {
            var result = await Generate(new string[] { prompt }, stop);
            return result.Generations.FirstOrDefault().FirstOrDefault().Text;
        }

        private T[][] chunkArray<T>(T[] arr, int chunkSize)
        {
            List<T[]> chunks = new List<T[]>();
            for (int i = 0; i < arr.Length; i += chunkSize)
            {
                T[] chunk = new T[Math.Min(chunkSize, arr.Length - i)];
                Array.Copy(arr, i, chunk, 0, chunk.Length);
                chunks.Add(chunk);
            }
            return chunks.ToArray();
        }
        //https://github.com/openai/openai-cookbook/blob/main/examples/How_to_count_tokens_with_tiktoken.ipynb
        private static async Task<int> calculateMaxTokens(string prompt, string modelName)
        {
            var encoding = GptEncoding.GetEncoding(ModelNameMapper.GetModelNameForSharpToken(modelName).ToString());

            // fallback to approximate calculation if tiktoken is not available
            int numTokens = (int)Math.Ceiling(prompt.Length / 4.0);

            try
            {
                if (encoding != null)
                {
                    var tokenized = encoding.Encode(prompt);

                    numTokens = tokenized.Count();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to calculate number of tokens with tiktoken, falling back to approximate count: " + ex.Message);
            }

            int maxTokens = 0;//ModelNameMapper.GetModelContextSize(modelName);

            return maxTokens - numTokens;
        }
    }
    public class TokenUsage
    {
        public int? CompletionTokens { get; set; }
        public int? PromptTokens { get; set; }
        public int? TotalTokens { get; set; }
    }

    public enum SharpTokenModel
    {
        r50k_base,
        p50k_base,
        cl100k_base
    }

    public static class ModelNameMapper
    {
        public static SharpTokenModel GetModelNameForSharpToken(string modelName)
        {
            if (modelName.StartsWith("gpt-3.5-turbo-") || modelName.StartsWith("gpt-4-32k-") || modelName.StartsWith("gpt-4-") || modelName.StartsWith("text-embedding-ada-002"))
            {
                return SharpTokenModel.cl100k_base;
            }

            if (modelName.StartsWith("text-davinci-") || modelName.StartsWith("text-davinci-"))
            {
                return SharpTokenModel.p50k_base;
            }

            if (modelName.StartsWith("davinci"))
            {
                return SharpTokenModel.r50k_base;
            }

            return Enum.Parse<SharpTokenModel>(modelName, true);
        }

        public static int GetModelContextSize(string modelName)
        {
            switch (modelName)
            {
                case "text-davinci-003":
                    return 4097;
                case "text-curie-001":
                    return 2048;
                case "text-babbage-001":
                    return 2048;
                case "text-ada-001":
                    return 2048;
                case "code-davinci-002":
                    return 8000;
                case "code-cushman-001":
                    return 2048;
                default:
                    return 4097;
            }
        }
    }
}
