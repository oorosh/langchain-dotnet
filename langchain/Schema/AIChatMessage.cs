namespace langchain.Schema
{
    public class AIChatMessage : BaseChatMessage
    {
        public AIChatMessage(string text) : base(text)
        {
        }

        public override MessageType GetMessageType()
        {
            return MessageType.Ai;
        }
    }
}
