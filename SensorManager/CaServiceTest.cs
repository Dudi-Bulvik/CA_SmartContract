using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SensorManager
{
  
    public class CaServiceTest : ICaService
    {
        public List<string> SensorNames => new List<string> { "1", "2", "3" };
        public Dictionary<string, SensorModel> senVM = new Dictionary<string, SensorModel>();

        public event EventHandler<SensorModel> SensorDataArrived;

        private Random rand = new Random(12);
        public void ChangeAccessRight(string sensorOwner, string fromSensor, string toSensorName, bool accses)
        {
            if (senVM.ContainsKey(fromSensor))
            {
                var sensorViewModel = senVM[toSensorName];
                sensorViewModel.AccsesRights.Add(new AccessRight() { SensorName = rand.Next().ToString(), AccessePermition = accses });
                foreach (var acc in sensorViewModel.AccsesRights)
                {
                    if (acc.SensorName.Equals(toSensorName))
                    {
                        acc.AccessePermition = accses;
                        Task.Run(() => { RaizeEvent(fromSensor); });
                        break;
                    }
                }
            }
        }
        public CaServiceTest()
        {
            foreach(var name in SensorNames)
            {
                RaizeEvent(name);
            }
        }


        public Dictionary<string, bool> GetSenssorAccessRight(string sensorName)
        {
            throw new System.NotImplementedException();
        }
        private void RaizeEvent(string sensorName)
        {
            
            if (!senVM.ContainsKey(sensorName))
            {
                var accessList = new List<AccessRight>();
                bool permition = false;
                foreach (var name in SensorNames)
                {
                    if (name.Equals(sensorName))
                    {
                        continue;
                    }
                    permition = !permition;
                    accessList.Add(new AccessRight() { SensorName = name, AccessePermition = permition });
                }
                senVM.Add(sensorName, new SensorModel()
                {
                    SensorName = sensorName,
                    SensorPublicKey = "0xF1a2e3ae1232132131231",
                    AccsesRights = accessList
                });
                
            }
            var clone = senVM[sensorName].Clone();
            SensorDataArrived?.Invoke(this, clone);
        }
        public void GetSenssorData(string sensorName)
        {
            Task.Run(() => { RaizeEvent(sensorName); });
           
        }

        public void InitSensor(string sensorOwner, string sensorName, string sensorPublikKey)
        {
            throw new NotImplementedException();
        }

        public void InitOwner(string sensorOwner, string sensorPublikKey, string SensorPrivateKey)
        {
            throw new NotImplementedException();
        }
    }
}