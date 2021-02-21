using System;
using System.Collections.Generic;
using System.Text;

namespace SensorManager
{
   public interface ICaService
    {
        List<string> SensorNames { get; }
        public void ChangeAccessRight(string senstorName, bool accses);
        public SensorViewModel GetSenssorData(string sensorName);
       
    }
}
