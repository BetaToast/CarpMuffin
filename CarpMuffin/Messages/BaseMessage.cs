namespace CarpMuffin.Messages
{
    public class BaseMessage
        : IMessage<object>
    {
        public string Id { get; set; }
        public object Payload { get; set; }
    }
}