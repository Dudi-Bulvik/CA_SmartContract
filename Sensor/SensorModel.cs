using System;
using System.Collections.Generic;
using System.Text;

namespace Sensor
{
    public class SensorModel
    {
        string name;
        Dictionary<string, string> sensorList = new Dictionary<string,string>();

        public SensorModel()
        {
            this.name = "Sesntor0";
           
            sensorList.Add("Sensor1", "");
            sensorList.Add("Sensor2", "");
            sensorList.Add("Sensor3", "");
        }
        public string Name { get { return name; } }
        public int Secret { get; set; }
        public List<string> SensorList { get { return new List<string>(sensorList.Keys) ; } }
        public void  GetSensor()
        { }
        
    }
}
