using System.Collections.Generic;

namespace langchain.Schema
{
    public class Generation
    {
        /// <summary>
        /// Generated text output
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Raw generation info response from the provider.
        /// May include things like reason for finishing (e.g. in OpenAI)
        /// </summary>
        public IDictionary<string, object> GenerationInfo { get; set; }
    }
}
