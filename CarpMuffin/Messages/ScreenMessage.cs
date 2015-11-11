using CarpMuffin.Screens;

namespace CarpMuffin.Messages
{
    public class ScreenMessage
        : IMessage<object>
    {
        public string Id { get; set; }
        public IScreen Sender { get; set; }
        public object Payload { get; set; }
    }
}