using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Annotations;
using BusinessLogic.Interfaces;

namespace BusinessLogic.Models
{
    public class Parameter : INotifyPropertyChanged
    {
        public Parameter(string nameParam)
        {
            Values = new ObservableCollection<double>();
            Name = nameParam;
        }

        public ObservableCollection<double> Values { get; set; }
        public double Median => Values?.Count > 0 ? Values.Sum(x => x) / Values.Count : 0 ;

        public double Sigma
        {
            get
            {
                var med = Median;
                return Values?.Count > 0 ? Math.Sqrt(Values.Sum(x => Math.Pow(x - med, 2)) / (Values.Count - 1)) : 0;
            }
        }

        public string Name { get; }
        public void Update()
        {
            OnPropertyChanged(nameof(Median));
            OnPropertyChanged(nameof(Sigma));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
