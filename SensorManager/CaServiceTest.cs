using System.Collections.Generic;

namespace SensorManager
{
    public class CaServiceTest : ICaService
    {
        public List<string> SensorNames =>  new List<string>{"1","2","3"};

        public void ChangeAccessRight(string senstorName, bool accses)
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, bool> GetSenssorAccessRight(string sensorName)
        {
            throw new System.NotImplementedException();
        }

        public SensorViewModel GetSenssorData(string sensorName)
        {
            return new SensorViewModel { SensorName = sensorName, SensorPublicKey = "0xF1a2e3ae1232132131231" ,
                AccsesRights =new List<AccessRight> { new AccessRight {SensorName ="2",AccessePermition= false },
                    new AccessRight {SensorName ="3",AccessePermition= true } }
            };
        }
    }
}