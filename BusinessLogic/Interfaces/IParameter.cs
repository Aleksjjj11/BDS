using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace BusinessLogic.Interfaces
{
    public interface IParameter
    {
        ObservableCollection<Item> Values { get; set; }
        double Median { get; }
        double Sigma { get; }
        string Name { get; }
        void Update();
    }
}
