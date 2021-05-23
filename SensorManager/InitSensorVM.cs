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
        private List<string> sensorOwners;
        private string sensorName;
        private string sensorOwner;
        private bool isOwner;
        private string errorMessage;
        private AddressUtil addressUtil;
        private decimal balanceAfter = 0;
        private decimal balanceBefore = 0;
        private bool showStatus;
        public InitSensorVM(ICaService caService ,ILogger logger)
        {
            this.caService = caService;
            this.logger = logger;
            sensorNames = caService.SensorNames;
            sensorOwners = caService.SensorOwners;
            caService.AddNewSensor += AddNewSensor;
            InitSensorCommand = new RelayCommand(InitSensor, CanExecuteInitSensor);
            addressUtil = new AddressUtil();
            errorMessage = "";
            ShowStatus = false;
        }

        private void AddNewSensor(object sender, EventArgs e)
        {
            SensorNames = caService.SensorNames;     
            SensorOwners = caService.SensorOwners;
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
        private  void InitSensor()
        {
            Task.Run(async () =>
            {
                ShowStatus = false;
                if (isOwner)
                {
                    BalanceBefore = await caService.GetAccountBalance(sensorName, SensorPublikKey);
                    await caService.InitOwner(sensorName, SensorPublikKey, SensorPrivateKey);
                    BalanceAfter = await caService.GetAccountBalance(sensorName);
                }
                else
                {
                    BalanceBefore = await caService.GetAccountBalance(SensorOwner);
                    await caService.InitSensor(SensorOwner, sensorName, SensorPublikKey);
                    BalanceAfter = await caService.GetAccountBalance(SensorOwner);
                }

                SensorName = "";
                SensorOwner = "";
                SensorPublikKey = "";
                SensorPrivateKey = "";
                ShowStatus = true;
                RaisePropertyChanged("");

                
            });

           
        }

        public RelayCommand InitSensorCommand
        {
            get;
            private set;
        
        }

        public List<string> SensorNames
        {
            get { return sensorNames; }
            set { Set(() => SensorNames, ref sensorNames, value);
                //ShowStatus = false;
            }
        }

        public List<string> SensorOwners
        {
            get { return sensorOwners; }
            set { Set(() => SensorOwners, ref sensorOwners, value); }
        }

        public string SensorOwner { 
            get { return sensorOwner; }
            set { Set(() => SensorOwner, ref sensorOwner, value);
               // ShowStatus = false;
            }
        }
        public string SensorName { 
            get { return sensorName; }
            set { Set(() => SensorName, ref sensorName, value); }
        
        }
        public string SensorPublikKey { get; set; }
        public bool IsOwner
        {
            get { return isOwner; }
            set
            {
                Set(() => IsOwner, ref isOwner, value);
               // ShowStatus = false;
                RaisePropertyChanged("IsNotOwner");
                
            }
        }
        public bool IsNotOwner
        {
            get { return !isOwner; }            
        }
        public bool ShowStatus
        {
            get { return showStatus; }
            set
            {
                Set(() => ShowStatus, ref showStatus, value);
               
            }

        }

        public decimal GasUsed
        {
            get { return balanceBefore -balanceAfter; }
        }
        public decimal BalanceAfter
        {
            get { return balanceAfter; }
            set
            {
                Set(() => BalanceAfter, ref balanceAfter, value);
                RaisePropertyChanged("GasUsed");
            }

        }

        public decimal BalanceBefore
        {
            get { return balanceBefore; }
            set
            {
                Set(() => BalanceBefore, ref balanceBefore, value);

            }
        }

        public string SensorPrivateKey { get; set; }
    }
}
