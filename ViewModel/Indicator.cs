using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Candles;

namespace ViewModel
{
    public class Indicator : IIndicator
    {
        private Candle LastCandle { get; set; }

        public Indicator()
        {
            LastCandle = null;
        }

        public decimal Calculate(Candle candle)
        {
            if (this.LastCandle == null)
            {
                this.LastCandle = candle;
                return 0;
            }
            var candleMed = (candle.High + candle.Low) / 2;
            var candleDif = candle.High - candle.Low;
            var lastCandleMed = (this.LastCandle.High + this.LastCandle.Low) / 2;
            return (candleMed - lastCandleMed) * candleDif/ candle.Volume;
        }


        private decimal ADL=0;
        private decimal ADLlast = 0;
        public double CalculateAD(Candle candle)
        {
            decimal MoneyFlowMultiplier = ((candle.Close - candle.Low) - (candle.High - candle.Close)) / (candle.High - candle.Low);
            decimal MoneyFlowVolume = MoneyFlowMultiplier * (candle.Volume/24);
            ADLlast = ADL;
            ADL = ADLlast + MoneyFlowVolume;
            return (double)ADL;
        }
    }
}
