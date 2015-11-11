namespace CarpMuffin.Messages
{
    public interface IMessage<T>
    {
        string Id { get; set; }
        T Payload { get; set; }
    }
}