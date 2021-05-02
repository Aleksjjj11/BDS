using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using BDS.Commands;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Definitions.Charts;
using LiveCharts.Wpf;
using Microsoft.Win32;
using Separator = LiveCharts.Wpf.Separator;

namespace BDS.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Parameter> Parameters { get; set; }
        public ObservableCollection<Item> ValuesParameters { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private ICommand _createRowsOnGrid;
        public ICommand CreateRowsOnGrid => _createRowsOnGrid ?? new RelayCommand(() =>
        {
            for (var i = CountIndicator - ValuesParameters.Count; i > 0; i--)
            {
                ValuesParameters.Add(new Item{ValueFirstParameter = 0, ValueSecondParameter = 0, ValueThirdParameter = 0});
            }
        }, () => true);

        private ICommand _updateParameters;

        public ICommand UpdateParameters => _updateParameters ?? new RelayCommand(() =>
        {
            Parameters[0].Values.Clear();
            Parameters[1].Values.Clear();
            Parameters[2].Values.Clear();

            foreach (var valuesParameter in ValuesParameters)
            {
                Parameters[0].Values.Add(valuesParameter.ValueFirstParameter);
                Parameters[1].Values.Add(valuesParameter.ValueSecondParameter);
                Parameters[2].Values.Add(valuesParameter.ValueThirdParameter);
            }
            Parameters[0].Update();
            Parameters[1].Update();
            Parameters[2].Update();
        }, () => true);

        private ICommand _closeAppCommand;

        public ICommand CloseAppCommand =>
            _closeAppCommand ?? new RelayCommand<Window>(x =>
            {
                x.Close();
            }, x => true);

        private ICommand _resizeAppCommand;

        public ICommand ResizeAppCommand => _resizeAppCommand ?? new RelayCommand<Window>(x =>
        {
            x.WindowState = x.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }, x => true);

        private ICommand _rollupAppCommand;

        public ICommand RollupAppCommand => _rollupAppCommand ?? new RelayCommand<Window>(x =>
        {
            x.WindowState = WindowState.Minimized;
        }, x => true);

        private int _countIndicator;
        public int CountIndicator
        {
            get => _countIndicator;
            set
            {
                _countIndicator = value;
                OnPropertyChanged(nameof(CountIndicator));
            }
        }

        private int _numberOfParameter;

        public int NumberOfParameter
        {
            get => _numberOfParameter;
            set
            {
                _numberOfParameter = value;
                OnPropertyChanged(nameof(NumberOfParameter));
            }
        }

        private ICommand _showCharts;
        public ICommand ShowCharts => _showCharts ?? new RelayCommand(() =>
        {
            SeriesCollection.Clear();
            int[] dataToShow = new int[Labels.Length];
            var xLine = new []
            {
                Parameters[NumberOfParameter-1].Sigma * -4, 
                Parameters[NumberOfParameter-1].Sigma * -3, 
                Parameters[NumberOfParameter-1].Sigma * -2, 
                Parameters[NumberOfParameter-1].Sigma * -1, 
                Parameters[NumberOfParameter-1].Sigma * 0, 
                Parameters[NumberOfParameter-1].Sigma * 1, 
                Parameters[NumberOfParameter-1].Sigma * 2, 
                Parameters[NumberOfParameter-1].Sigma * 3,
                Parameters[NumberOfParameter-1].Sigma * 4,
            };
            for (int i = 0; i < dataToShow.Length; i++)
            {
                dataToShow[i] = Parameters[NumberOfParameter - 1].Values.Count(x => x <= xLine[i + 1] && x >= xLine[i]);
            }
            SeriesCollection.Add(new ColumnSeries
            {
                Title = Parameters[NumberOfParameter - 1].Name,
                Values = new ChartValues<int>(dataToShow),
                Fill = System.Windows.Media.Brushes.IndianRed,
                Stroke = System.Windows.Media.Brushes.DarkRed
            });
        }, () => NumberOfParameter > 0 && NumberOfParameter < 4);

        public MainViewModel()
        {
            Parameters = new ObservableCollection<Parameter>();
            ValuesParameters = new ObservableCollection<Item>();

            InitCommands();

            Parameters.Add(new Parameter("Параметр 1"));
            Parameters.Add(new Parameter("Параметр 2"));
            Parameters.Add(new Parameter("Параметр 3"));
            Labels = new[]{ "-4S <= X >= -3S", "-3S <= X >=-2S", "-2S <= X >=-S", "-S <= X >= 0", "0 <= X >= S", "S <= X >= 2S", "2S <= X >= 3S", "3S <= X >= 4S" };
            SeriesCollection = new SeriesCollection();
            Formatter = value => value.ToString("N");
        }

        private string _filePath;

        public string FilePath
        {
            get => _filePath;
            set
            {
                if (_filePath == value) return;

                _filePath = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenExcelFile { get; set; }
        public ICommand LoadDataFromExcelFile { get; set; }

        private void InitCommands()
        {
            OpenExcelFile = new RelayCommand(() =>
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "Excel files (*.xlsx)|*.xlsx|(*.xls)|*.xls";
                openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (openFile.ShowDialog() == true)
                    FilePath = openFile.FileName;

                OnPropertyChanged(nameof(FilePath));
            }, () => true);

            LoadDataFromExcelFile = new RelayCommand(() =>
            {

            }, () => string.IsNullOrWhiteSpace(FilePath) == false);
        }
    }
}
