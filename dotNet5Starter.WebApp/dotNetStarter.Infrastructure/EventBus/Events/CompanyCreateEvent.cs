namespace dotNet5Starter.Infrastructure.EventBus.Events
{
    public class CompanyCreateEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Exchange { get; set; }
        public string StockTicker { get; set; }
        public string Isin { get; set; }
        public string Website { get; set; }
    }

}
