using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Nethereum.Signer;
using Nethereum.Signer.Crypto;
using Nethereum.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorManager
{
    public class InitSensorVM : ViewModelBase
    {
        private readonly ICaService caService;
        private readonly ILogger logger;
        private List<string> sensorNames;
        private string sensorName;
        private string sensorOwner;
        private bool isOwner;
        private string errorMessage;
        private AddressUtil addressUtil;
        public InitSensorVM(ICaService caService ,ILogger logger)
        {
            this.caService = caService;
            this.logger = logger;
            sensorNames = caService.SensorNames;
            InitSensorCommand = new RelayCommand(InitSensor, CanExecuteInitSensor);
            addressUtil = new AddressUtil();
            errorMessage = "";
        }
        //public string GetPublicAddress()
        //{
        //    var initaddr = new Sha3Keccack().CalculateHash(GetPubKeyNoPrefix());
        //    var addr = new byte[initaddr.Length - 12];
        //    Array.Copy(initaddr, 12, addr, 0, initaddr.Length - 12);
        //    return new AddressUtil().ConvertToChecksumAddress(addr.ToHex());
        //}
        public bool ShowToolTip { get { return !string.IsNullOrEmpty(errorMessage); } }
        private bool CanExecuteInitSensor()
        {
             if(IsNotOwner && string.IsNullOrEmpty(SensorOwner) )
             {
                ErrorMessage = "SensorOwner can't be empty";
                return false;
             }
            if (string.IsNullOrEmpty(SensorName))
            {
                ErrorMessage = "SensorName can't be empty";
                return false;
            }
            else
            {
                if(sensorNames.Contains(SensorName))
                {
                    ErrorMessage = "Sensor Name: "+ SensorName+" is allready existing";
                    return false;
                }
            }

            if (string.IsNullOrEmpty(SensorPublikKey))
            {
                ErrorMessage = "SensorPublikKey can't be empty";
                return false;
            }
            else
            {
                if (!addressUtil.IsValidEthereumAddressHexFormat(SensorPublikKey))
                {
                    ErrorMessage = "Sensor Public address is not valid";
                    return false;
                }
            }
            if (IsOwner && string.IsNullOrEmpty(SensorPrivateKey))
            {
                ErrorMessage = "SensorPublikKey can't be empty";
                return false;
            }
            if(IsOwner)
            {
                return CheckPrivatePublicKeyPair();
            }          
            
            
            
            ErrorMessage = "";
            return true;
        }
        
        private bool CheckPrivatePublicKeyPair()
        {
            try
            {
                var privKey = new EthECKey(SensorPrivateKey);
                var publicAddress = privKey.GetPublicAddress().ToUpper();
                var userPublikKey = SensorPublikKey.ToUpper();
                if (!userPublikKey.StartsWith("0X"))
                {
                    userPublikKey = "0x" + userPublikKey;
                }

                if (!publicAddress.Equals(userPublikKey))
                {
                    ErrorMessage = "Private key and Public Key aren't compatible ";
                    return false;
                }
                ErrorMessage = "";
                return true; ;
            }catch(Exception e )
            {
                ErrorMessage = e.ToString();
                logger.AddLogEntey("Keys are not valid" + e);
                return false;
            }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                Set(() => ErrorMessage, ref errorMessage, value);
                RaisePropertyChanged("ShowToolTip");
            }
        }
        private void InitSensor()
        {
            if(isOwner)
            {
                caService.InitOwner(sensorName, SensorPublikKey, SensorPrivateKey);
            }
            else
            {
                caService.InitSensor(SensorOwner, sensorName, SensorPublikKey);
            }
            
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
