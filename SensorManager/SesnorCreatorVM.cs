using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Nethereum.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorManager
{
    public class SesnorCreatorVM : ViewModelBase
    {
        private readonly ICaService caService;
        private List<string> sensorNmaes;
        private string sensorName;

        public SesnorCreatorVM(ICaService caService)
        {
            this.caService = caService;
            sensorNmaes = caService.SensorNames;
            InitSensorCommand = new RelayCommand(InitSensor, CanExecuteInitSensor);
        }
        //public string GetPublicAddress()
        //{
        //    var initaddr = new Sha3Keccack().CalculateHash(GetPubKeyNoPrefix());
        //    var addr = new byte[initaddr.Length - 12];
        //    Array.Copy(initaddr, 12, addr, 0, initaddr.Length - 12);
        //    return new AddressUtil().ConvertToChecksumAddress(addr.ToHex());
        //}
        private bool CanExecuteInitSensor()
        {
            if (caService.SensorNames.Contains(SensorName))
            {
                return false;
            }
            return true;
        }

        private void InitSensor()
        {
        }

        public RelayCommand InitSensorCommand
        {
            get;
            private set;
        
        }
        public string SensorName { get => sensorName; set => sensorName = value; }
        public string SensorPublikKey { get; set; }
        public bool IsOwner { get; set; }
        public string SensorPrivateKey { get; set; }
    }
}
