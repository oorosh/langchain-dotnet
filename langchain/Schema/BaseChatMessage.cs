namespace langchain.Schema
{
    public abstract class BaseChatMessage
    {
        public string Text { get; set; }
        public abstract MessageType GetMessageType();

        public BaseChatMessage(string text)
        {
            Text = text;
        }
    }
}
