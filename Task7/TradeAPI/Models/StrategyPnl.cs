namespace TradeAPI.Models
{
    public class StrategyPnl
    {
        public string Strategy { get; set; }
        public List<Pnl> Pnls { get; set; }
    }
}
