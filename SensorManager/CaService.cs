using System;
using System.Collections.Generic;
using System.Text;

namespace SensorManager
{
   public interface ICaService
    {
        List<string> SensorNames { get; }
        event EventHandler<SensorViewModel> SensorDataArrived;
        public void ChangeAccessRight(string senstorName, bool accses);
        public void GetSenssorData(string sensorName);
       
    }
}
