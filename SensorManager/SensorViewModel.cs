using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;

namespace SensorManager
{
    public class AccessRight
    {
        public string SensorName { get; set; }
        public bool AccessePermition { get; set; }
    }
    public class SensorViewModel : ViewModelBase
    {
        public SensorViewModel()
        {
            ChangePermitionCommand = new RelayCommand(ChangSensor);
        }
        private void ChangSensor()
        { 
        
        }
        public string SensorName { get; set; }
        public string SensorPublicKey { get; set; }
        public List<AccessRight> AccsesRights { get; set; }

        public RelayCommand ChangePermitionCommand { get; set; }
    }
}
