namespace dotNet5Starter.Infrastructure.EventBus.Abstractions
{
    public record EventHubMessage
    {
        public string Message { get; set; }
    }
}
