namespace langchain.Schema
{
    public class ChatMessage : BaseChatMessage
    {
        public string Role { get; set; }

        public ChatMessage(string text, string role) : base(text)
        {
            Role = role;
        }

        public override MessageType GetMessageType()
        {
            return MessageType.Generic;
        }
    }
}
