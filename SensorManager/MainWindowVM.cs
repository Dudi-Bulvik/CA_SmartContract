using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorManager
{
   public class MainWindowVM : ViewModelBase
    {
        public int SelectedItemNumber { get; set; } = 1;
    }
}
