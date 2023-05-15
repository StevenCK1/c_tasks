using System;
using System.Collections.Generic;

namespace TradeAPI.Db.Entity;

public partial class StrategyPnL
{
    public int Idstrategy { get; set; }

    public string Strategy { get; set; } = null!;

    public virtual ICollection<PnL> PnLs { get; set; } = new List<PnL>();
}
