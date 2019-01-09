using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candles
{
    public interface ILoader
    {
        Queue<Candle> GetCandles();
    }
}
