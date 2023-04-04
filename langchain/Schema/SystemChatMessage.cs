namespace langchain.Schema
{
    public class SystemChatMessage : BaseChatMessage
    {
        public SystemChatMessage(string text) : base(text)
        {
        }

        public override MessageType GetMessageType()
        {
            return MessageType.System;
        }
    }
}
