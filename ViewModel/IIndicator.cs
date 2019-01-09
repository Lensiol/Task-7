using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Candles;

namespace ViewModel
{
    public interface IIndicator
    {
        decimal Calculate(Candle candle);
    }
}
