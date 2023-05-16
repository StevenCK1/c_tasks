namespace TradeAPI.Models
{
    public class StrategyPnlVM
    {
        public string Strategy { get; set; }
        public List<PnLVM> Pnls { get; set; }
    }
}
