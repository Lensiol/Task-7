using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SciChart.Charting.Model.DataSeries;
using SciChart.Data.Model;
using System.Windows.Threading;
using Microsoft.Win32;
using Candles;
using ViewModel;


namespace Task7
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private OhlcDataSeries<DateTime, double> CandlesSeries;
        private DataSeries<DateTime, double> ADLseries;

        private Display Display { get; set; }
        private int candleCount;
        public MainWindow()
        {
            InitializeComponent();
        }
        #region files

        private void FromJson(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";
            Json jsonloader;
            if (openFileDialog.ShowDialog() == true)
            {
                jsonloader = new Json(openFileDialog.FileName);
                List<DateTime> dt = new List<DateTime>();
                List<double> cd = new List<double>();
                LoadDataSource(new Storage(jsonloader));
                this.Loaded += OnLoaded;
            }
        }
        #endregion
        private void LoadDataSource(Storage storage)
        {

            this.Display = new Display(storage, new Indicator());
            this.Display.NewCandle += Display_NewCandle;
            ReloadChartSeries();
            this.Display.Run();
        }
        private void Display_NewCandle(Candle candle, decimal indicator)
        {
            StockChart.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { AddNewCandle(candle, indicator); }));
        }
        private void AddNewCandle(Candle candle, decimal indicator)
        {
            using (ADLseries.SuspendUpdates())
            using (CandlesSeries.SuspendUpdates())
            {
                CandlesSeries.Append(candle.Time, (double)candle.Open, (double)candle.High, (double)candle.Low, (double)candle.Close);
                ADLseries.Append(candle.Time, Display.Indicator.CalculateAD(candle));
                candleCount++;
            }
        }
        private void ReloadChartSeries()
        {
            CandlesSeries = new OhlcDataSeries<DateTime, double>() { SeriesName = "Candles", FifoCapacity = 1000};
            ADLseries = new XyDataSeries<DateTime, double> { SeriesName = "Accumulation/Distribution", FifoCapacity = 100 };
            CandleSeries.DataSeries = CandlesSeries;
            Line.DataSeries = ADLseries;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
        }
    }
}
