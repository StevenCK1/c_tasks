

namespace GSA_Server.Core.models
{
    public class CumulativeCapitalVM
    {
        public string StratName { get; set; }
        public ICollection<CapitalVM> Capital { get; set; }

    }
}
