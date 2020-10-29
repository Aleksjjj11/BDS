using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BDS.Annotations;
using BDS.Commands;

namespace BDS.ViewModels
{
    public class ValuesModel : BaseViewModel
    {
        public long ValueFirstParam { get; set; }
        public long ValueSecondParam { get; set; }
        public long ValueThirdParam { get; set; }
    }

    public class Parameter : BaseViewModel
    {
        public string Name { get; set; }
        public double Median { get; set; }
        public double Sigma { get; set; }
    }
    public class MainViewModel : BaseViewModel
    {
        private ICommand _closeAppCommand;

        public ICommand CloseAppCommand
        {
            get => _closeAppCommand ?? new RelayCommand<Window>(x =>
            {
                x.Close();
            }, x => true);
        }

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
        public ObservableCollection<ValuesModel> ValuesParams { get; set; }
        public ObservableCollection<Parameter> Parameters { get; set; }

        public MainViewModel()
        {
            Parameters = new ObservableCollection<Parameter>();
            ValuesParams = new ObservableCollection<ValuesModel>();
            for (int i = 0; i < 10; i++)
            {
                ValuesParams.Add(new ValuesModel{ ValueFirstParam = i, ValueSecondParam = i, ValueThirdParam = i});
            }

            Parameters.Add(new Parameter
            {
                Name = $"Параметр 1",
                Median = (double)ValuesParams.Sum(x => x.ValueFirstParam) / ValuesParams.Count
            });
            Parameters.Add(new Parameter
            {
                Name = $"Параметр 2",
                Median = (double)ValuesParams.Sum(x => x.ValueSecondParam) / ValuesParams.Count
            });
            Parameters.Add(new Parameter
            {
                Name = $"Параметр 3",
                Median = (double)ValuesParams.Sum(x => x.ValueThirdParam) / ValuesParams.Count
            });
        }
    }
}
