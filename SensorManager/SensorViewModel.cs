using GalaSoft.MvvmLight;
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
        public string SensorName { get; set; }
        public string SensorPublicKey { get; set; }
        public List<AccessRight> AccsesRights { get; set; }
    }
}
