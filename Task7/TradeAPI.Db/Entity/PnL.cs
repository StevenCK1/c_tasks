using System;
using System.Collections.Generic;

namespace TradeAPI.Db.Scaffolded;

public partial class PnL
{
    public int IdPnL { get; set; }

    public DateTime Date { get; set; }

    public decimal? Amount { get; set; }

    public int? Idstrategy { get; set; }

    public virtual StrategyPnL? IdstrategyNavigation { get; set; }
}
