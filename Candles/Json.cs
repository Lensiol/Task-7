using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Candles
{
    public class Json:ILoader
    {
        private string _filename;

        public Json(string name)
        {
            _filename = name;
        }
        public Queue<Candle> GetCandles()
        {
            Queue<Candle> newCandles = new Queue<Candle>();
            dynamic json = JsonConvert.DeserializeObject(File.ReadAllText(_filename));
            List<Candle> candles = new List<Candle>();
            var count = ((JArray)json.data).Count;
            for(int i=0;i<count;i++)
            {
                Candle c = new Candle
                {
                    High = (decimal)json.data[i].high,
                    Low = (decimal)json.data[i].low,
                    Open = (decimal)json.data[i].open,
                    Close = (decimal)json.data[i].close,
                    Time = DateTimeOffset.FromUnixTimeMilliseconds((long)json.data[i].timeStamp).UtcDateTime,
                    TimeStamp = (long)json.data[i].timeStamp,
                    Volume = (int)json.data[i].volume
                };
                candles.Add(c);
            }
            candles.Sort();

            foreach(Candle c in candles)
            {
                newCandles.Enqueue(c);
            }

            return newCandles;
        }
    }
}
