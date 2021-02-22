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
        private string selectedSensorName;
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
            caService.GetSenssorData(SenesorNames[0]);
            SelectionChangedCommand = new RelayCommand(SelectionChanged);
            caService.SensorDataArrived += SensorDataArrivedHandler;            
            SelectedSensorName = this.SenesorNames[0];
        }
        private void SensorDataArrivedHandler(object sender, SensorViewModel sensorDat)
        {
            SlectedSensorData = sensorDat;
        }
        public string SelectedSensorName { get { return selectedSensorName; }
            set {
                if(!value.Equals(selectedSensorName))
                {
                    selectedSensorName = value;
                    caService.GetSenssorData(value);

                    RaisePropertyChanged("SelectedSensorName");
                }
            } }
        private void SelectionChanged()
        {
        }
    }
}
