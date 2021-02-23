using System;
using System.Collections.Generic;
using System.Text;

namespace SensorManager
{
   public interface ICaService
    {
        List<string> SensorNames { get; }
        event EventHandler<SensorModel> SensorDataArrived;
        public void ChangeAccessRight(string fromSenstor ,string toSenstorName, bool accses);
        public void GetSenssorData(string sensorName);
       
    }
}
