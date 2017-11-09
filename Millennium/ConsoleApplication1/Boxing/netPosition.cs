using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using mlp.interviews.software.common;

namespace mlp.interviews.boxing.problem
{
    public class Position : BaseClass
    {
        bool disposed = false;
        List<TraderDTO> traders = new List<TraderDTO>();

        private List<TraderDTO> GetTraders()
        {
            //Avoid the repititive calls for extracting the test data from the CSV file.
            if (traders.Count > 0) return traders;

            var applicationPath = Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory)) + @"\Resources\test_data.csv";
            if (!File.Exists(applicationPath)) return traders;

            bool isHeader = true;
            using (StreamReader reader = new StreamReader(applicationPath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (isHeader)
                    {
                        isHeader = false;
                    }
                    else
                    {
                        traders.Add(new TraderDTO
                        {
                            Trader = String.IsNullOrEmpty(values[0]) ? String.Empty : values[0],
                            Broker = String.IsNullOrEmpty(values[1]) ? String.Empty : values[1],
                            Symbol = String.IsNullOrEmpty(values[2]) ? String.Empty : values[2],
                            Quantity = String.IsNullOrEmpty(values[3]) ? 0 : Int32.Parse(values[3]),
                            Price = String.IsNullOrEmpty(values[4]) ? 0 : Int32.Parse(values[4])
                        });
                    }
                }
            }
            return traders;
        }

        public bool CalcNetPositions()
        {
            bool success = false;
            List<TraderDTO> _traders = GetTraders();

            List<ResultTraderDTO> _resultTraders = _traders
                                           .GroupBy(t => new { t.Trader, t.Symbol })
                                           .Select(
                                              resultTrader => new ResultTraderDTO
                                              {
                                                  Trader = resultTrader.First().Trader,
                                                  Symbol = resultTrader.First().Symbol,
                                                  Quantity = resultTrader.Sum(q => q.Quantity)
                                              }
                                             ).ToList<ResultTraderDTO>();


            var srcSymbolCount = _traders.Select(s => s.Symbol).Distinct().Count();
            var netSymbolCount = _resultTraders.Select(s => s.Symbol).Distinct().Count();

            /*  Unit Testing... Make sure the output result is working as expected */
            success = _resultTraders.Count > 0
                      && srcSymbolCount == netSymbolCount;

            if (success) WriteCSV(_resultTraders, "net_positions_expected");
            return success;
        }

        public bool BoxedPositions()
        {
            bool success = false;
            List<TraderDTO> _traders = GetTraders();

            List<ExpectedTraderDTO> _expResults = _traders
                                            .GroupBy(t => new { t.Trader, t.Symbol })
                                            .Select(
                                              resultTrader => new ExpectedTraderDTO
                                              {
                                                  Trader = resultTrader.First().Trader,
                                                  Symbol = resultTrader.First().Symbol,
                                                  Quantity = resultTrader.Max(q => (q.Quantity > 0 ? q.Quantity : 0)),
                                                  NegQuantity = resultTrader.Max(q => (q.Quantity < 0 ? Math.Abs(q.Quantity) : 0))
                                              }
                                             )
                                             .AsEnumerable()
                                             .Where(q => q.NegQuantity > 0 && q.Quantity > 0)
                                             .ToList<ExpectedTraderDTO>();

            List<ResultTraderDTO> _resultTraders = new List<ResultTraderDTO>();
            if (_expResults.Count > 0)
            {
                _resultTraders = _expResults
                                .Select(
                                    rt => new ResultTraderDTO
                                    {
                                        Trader = rt.Trader,
                                        Symbol = rt.Symbol,
                                        Quantity = rt.Quantity - rt.NegQuantity
                                    }
                                    )
                                    .ToList<ResultTraderDTO>();
            }

            /*  Unit Testing... Make sure the output result is working as expected */
            success = _resultTraders.Count > 0;
            if (success) WriteCSV(_resultTraders, "boxed_positions_expected");
            return success;
        }


        private void WriteCSV(List<ResultTraderDTO> traders, string reportFileName = "net_positions_expected")
        {
            var applicationPath = Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory)) + @"\Resources\" + reportFileName + ".csv";
            using (System.IO.StreamWriter objWriter = new System.IO.StreamWriter(applicationPath))
            {
                objWriter.WriteLine(string.Join(",", new[] { "TRADER", "SYMBOL", "QUANTITY" }));
                foreach(ResultTraderDTO trader in traders) {
                    objWriter.WriteLine(string.Join(",", new[] { trader.Trader, trader.Symbol, trader.Quantity.ToString()}));
                }
                
                objWriter.Flush();
                objWriter.Close();
            }
        }


        // Protected implementation of Dispose pattern.
        protected override void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing) { }
            disposed = true;
            // Call the base class implementation.
            base.Dispose(disposing);
        }

        ~Position()
        {
            Dispose(false);
        }
    }
}
