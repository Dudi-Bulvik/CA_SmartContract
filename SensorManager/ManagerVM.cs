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
        private ILogger logger;
        public List<string> SenesorNames {
            get { return sensorList; }
            set { sensorList = value;
                RaisePropertyChanged("SenesorNames"); }
        }
        public SensorViewModel SlectedSensorData {
            get {return selectedSensoer;}
            set { 
                selectedSensoer = value;
                RaisePropertyChanged("SlectedSensorData");                
            }

    }
                

        public ManagerVM(ICaService caService , ILogger logger)
        {
            this.caService = caService;
            this.logger = logger;
            this.SenesorNames = caService.SensorNames;
            caService.SensorDataArrived += SensorDataArrivedHandler;
            if(SenesorNames.Count > 0)
            {
                caService.GetSenssorData(SenesorNames[0]);
                SelectedSensorName = this.SenesorNames[0];
            }
            
        }
        private void SensorDataArrivedHandler(object sender, SensorModel sensorData)
        {
            //if (selectedSensoer != null && SlectedSensorData.SensorName.Equals(sensorData.SensorName))
            //{
                
            //    return;
            //}
            SlectedSensorData = new SensorViewModel(caService, sensorData, logger);
            SlectedSensorData.UpdateVM(sensorData);
            RaisePropertyChanged("");
        }
        public string SelectedSensorName { get { return selectedSensorName; }
            set {
                if(!value.Equals(selectedSensorName))
                {
                    selectedSensorName = value;
                    caService.GetSenssorData(value);                    
                }
            } }       
    }
}
