namespace TradeAPI.ViewModels
{
    public class StrategyPnlVM
    {
        public StrategyPnlVM()
        {
            Capital = new HashSet<CapitalVM>();
            Pnl = new HashSet<PnLVM>();
        }

        public int StrategyId { get; set; }
        public string StratName { get; set; }
        public string Region { get; set; }

        public ICollection<CapitalVM> Capital { get; set; }
        public ICollection<PnLVM> Pnl { get; set; }
    }
}
