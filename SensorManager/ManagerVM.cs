using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace SensorManager
{

    public class ManagerVM : ViewModelBase
    {
        private readonly ICaService caService;
        private List<string> sensorList;
        private SensorViewModel selectedSensoer;
        public List<string> SenesorNames {
            get { return sensorList; }
            set { sensorList = value;
                RaisePropertyChanged("SenesorNames"); }
        }
        public SensorViewModel SlectedSensorData {
            get {return selectedSensoer;}
            set { selectedSensoer = value;
                RaisePropertyChanged("SlectedSensorData");                
            }

    }
        
        public RelayCommand SelectionChangedCommand { get; private set; }

        public ManagerVM(ICaService caService)
        {
            this.caService = caService;
            this.SenesorNames = caService.SensorNames;
            SlectedSensorData = caService.GetSenssorData(SenesorNames[0]);
            SelectionChangedCommand = new RelayCommand(SelectionChanged);
        }

        private void SelectionChanged()
        {
            throw new NotImplementedException();
        }
    }
}
