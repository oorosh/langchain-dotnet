namespace langchain.Schema
{
    public class HumanChatMessage : BaseChatMessage
    {
        public HumanChatMessage(string text) : base(text)
        {
        }

        public override MessageType GetMessageType()
        {
            return MessageType.Human;
        }
    }
}
