using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Nethereum.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorManager
{
    public class SesnorCreatorVM : ViewModelBase
    {
        private readonly ICaService caService;
        private readonly ILogger logger;
        private List<string> sensorNames;
        private string sensorName;
        private string sensorOwner;
        private bool isOwner;

        public SesnorCreatorVM(ICaService caService ,ILogger logger)
        {
            this.caService = caService;
            this.logger = logger;
            sensorNames = caService.SensorNames;
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
             if(string.IsNullOrEmpty(SensorOwner) )
             { 
                logger.AddLogEntey( "SensorOwner can't be empty");
                return false;
             }
            if (string.IsNullOrEmpty(SensorName))
            {
                logger.AddLogEntey("SensorName can't be empty");
                return false;
            }

            if (caService.SensorNames.Contains(SensorName))
            {
                return false;
            }
            return true;
        }

        private void InitSensor()
        {
            caService.InitSensor(SensorOwner, sensorName, SensorPublikKey);
        }

        public RelayCommand InitSensorCommand
        {
            get;
            private set;
        
        }

        public List<string> SensorNames { get { return sensorNames; } }

        public string SensorOwner { get => sensorOwner; set => sensorOwner = value; }
        public string SensorName { get => sensorName; set => sensorName = value; }
        public string SensorPublikKey { get; set; }
        public bool IsOwner
        {
            get { return isOwner; }
            set
            {
                Set(() => IsOwner, ref isOwner, value);
                RaisePropertyChanged("IsNotOwner");
            }
        }
        public bool IsNotOwner
        {
            get { return !isOwner; }            
        }
        public string SensorPrivateKey { get; set; }
    }
}
