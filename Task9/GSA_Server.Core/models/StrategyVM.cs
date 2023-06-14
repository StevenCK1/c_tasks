namespace GSA_Server.Core.models
{
    public class StrategyVM
    {

        public StrategyVM()
        {
            Capital = new HashSet<CapitalVM>();
            Pnl = new HashSet<PnlVM>();
        }

        public string StratName { get; set; }
        public int StrategyId { get; set; }
        public string Region { get; set; }

        public ICollection<PnlVM> Pnl { get; set; }
        public ICollection<CapitalVM> Capital { get; set; }
    }
}