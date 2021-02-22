using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SensorManager
{
    public class CaServiceTest : ICaService
    {
        public List<string> SensorNames =>  new List<string>{"1","2","3"};

        public event EventHandler<SensorViewModel> SensorDataArrived;
        

        public void ChangeAccessRight(string senstorName, bool accses)
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, bool> GetSenssorAccessRight(string sensorName)
        {
            throw new System.NotImplementedException();
        }
        private void RaizeEvent(string sensorName)
        {
            var accessList = new List<AccessRight>();
            bool permition =false;
            foreach(var name in SensorNames)
            {
                if(name.Equals(sensorName))
                {
                    continue;
                }
                permition = !permition;
                accessList.Add(new AccessRight() { SensorName= name ,AccessePermition = permition });
            }
            SensorDataArrived?.Invoke(this, new SensorViewModel
            {
                SensorName = sensorName,
                SensorPublicKey = "0xF1a2e3ae1232132131231",
                AccsesRights = accessList
            });
        }
        public void GetSenssorData(string sensorName)
        {
            Task.Run(() => { RaizeEvent(sensorName); });
           
        }

        
    }
}