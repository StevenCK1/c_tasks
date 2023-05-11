
namespace TradeAPI.Db.Entity
{
    public class StrategyPnL
    {
        public int Id { get; set; }
        public string Strategy { get; set; }
        public List<PnL> Pnls { get; set; }
    }
}
