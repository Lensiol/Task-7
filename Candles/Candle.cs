﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candles
{
    public class Candle:IComparable
    {
        public long TimeStamp { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public DateTime Time { get; set; }
        public decimal Volume { get; set; }

        public int CompareTo(object obj)
        {
            return Time.CompareTo(((Candle)obj).Time);
        }
    }
}
