namespace langchain.Schema
{
    public abstract class BasePromptValue
    {
        public abstract BaseChatMessage[] ToChatMessages();
    }

}
