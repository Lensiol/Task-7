using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Candles;

namespace ViewModel
{
    public class Display
    {
        public delegate void NewCandleEvent(Candle candle, decimal indicator);
        public event NewCandleEvent NewCandle;

        public Storage Storage { get; set; }
        public Indicator Indicator { get; set; }

        public Display(Storage st, Indicator ind)
        {
            this.Storage = st;
            this.Indicator = ind;
        }

        public void Run()
        {
            ThreadPool.QueueUserWorkItem(x => Update());
        }
        private void Update()
        {
            while (this.Storage.Candles.Count != 0)
            {
                var candle = this.Storage.GetCandle();
                NewCandle?.Invoke(candle, this.Indicator.Calculate(candle));
                Thread.Sleep(100);
            }
        }
    }
}
