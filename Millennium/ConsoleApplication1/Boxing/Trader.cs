using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mlp.interviews.boxing.problem
{
    public class TraderDTO
    {
        public string Trader { get; set; }
        public string Broker { get; set; }
        public string Symbol { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }

    public class ResultTraderDTO
    {
        public string Trader { get; set; }
        public string Symbol { get; set; }
        public int Quantity { get; set; }
    }

    public class ExpectedTraderDTO : ResultTraderDTO
    {
        public int NegQuantity { get; set; }
    }
}
