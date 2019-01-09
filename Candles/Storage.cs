using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candles
{
    public class Storage
    {
        public ILoader Loader { get; set; }
        public Queue<Candle> Candles {get;set;}

        public Storage(ILoader loader)
        {
            Loader = loader;
            Candles = Loader.GetCandles();
        }

        public Candle GetCandle()
        {
            return Candles.Dequeue();
        }

        public void Update()
        {
            Candles=Loader.GetCandles();
        }
    }
}
