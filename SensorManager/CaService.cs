using CASmartContract.Contracts.CASmartContract.ContractDefinition;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CASmartContract.Contracts.CASmartContract;
using Nethereum.Contracts;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System.Threading.Tasks;
using System.Linq;

namespace SensorManager
{
    public interface ICaService
    {
        List<string> SensorNames { get; }
        List<string> SensorOwners { get; }
        event EventHandler<SensorModel> SensorDataArrived;
        bool IsPublicKeyallreadyExist(string publicKey);

        event EventHandler AddNewSensor;
        public Task<bool> ChangeAccessRight(string sensorOwner, string fromSensor, string toSensorName, bool accses);
        public void GetSenssorData(string sensorName);
        public Task<bool> InitSensor(string sensorOwner,string sensorName,string sensorPublikKey);
        public Task<bool> InitOwner(string sensorOwner, string sensorPublikKey, string SensorPrivateKey);

    }
    public class CAService : ICaService
    {
        private string contractAddress;
        private List<string> sensorNames= new List<string>();
        private List<string> sensorOwners = new List<string>();
        private string url = @"https://rinkeby.infura.io/v3/d07d714973ba46fcbcf79b770db878d0";

        private Dictionary<string, Account> accountDic = new Dictionary<string, Account>();
        public Dictionary<string, SensorModel> senVM = new Dictionary<string, SensorModel>();
        List<string> publicKeys = new List<string>();
        private object web3;
        private  ILogger logger;

        public List<string> SensorNames {
            get {
                var temp = new List<string>();
                foreach (var name in sensorNames)
                {
                    temp.Add(name);
                }
                return temp; }
            set
            {
                sensorNames = value;
            }
        }
        public List<string> SensorOwners
        {
            get
            {
                var temp = new List<string>();
                foreach (var name in sensorOwners)
                {
                    temp.Add(name);
                }
                return temp;
            }
            set
            {
                sensorOwners = value;
            }
        }

        public event EventHandler AddNewSensor;

        public event EventHandler<SensorModel> SensorDataArrived;
        public bool IsPublicKeyallreadyExist(string publicKey)
        {
            return publicKey.Contains(publicKey);            
        }

        public bool IsNamellreadyDefine(string name)
        {
            return sensorNames.Contains(name);
        }
       
        public CAService()
        {

        }
        public async Task InitializeAsync(ISettings settings,ILogger logger)
        {
            this.logger = logger;

            if (!File.Exists(settings.ConfigurationFilePath))
            {
                throw new Exception();
            }
            using (var fs = File.Open(settings.ConfigurationFilePath, FileMode.Open))
            using (TextReader sr = new StreamReader(fs))
            {
                contractAddress = sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    //sensorNames.Add(line);
                    var svm = new SensorModel();
                    svm.SensorName = line;

                    svm.SensorPublicKey = sr.ReadLine();                                         
                    svm.SensorPrivateKey = sr.ReadLine();
                    if(publicKeys.Contains(svm.SensorPublicKey) || sensorNames.Contains(svm.SensorName))
                     {
                        continue;
                    }
                    //accountDic.Add(svm.SensorName, new Account(svm.SensorPrivateKey));
                    senVM.Add(svm.SensorName, svm);
                    //publicKeys.Add(svm.SensorPublicKey);
                    
                    await InitOwner(svm.SensorName,  svm.SensorPublicKey, svm.SensorPrivateKey);

                }
            }

          
        }
        
        private void RaizeEvent(string sensorName)
        {

            if (!senVM.ContainsKey(sensorName))
            {
                var clone = senVM[sensorName].Clone();
                SensorDataArrived?.Invoke(this, clone);
            }

        }
        public async Task<bool> InitOwner(string sensorOwner, string sensorPK, string  sensorPrivateKey)
        {
            if (accountDic.ContainsKey(sensorOwner))
            {
                logger.AddLogEntey(sensorOwner + " is allready define");
                return true; 
            }
            accountDic[sensorOwner] = new Account(sensorPrivateKey);
            sensorOwners.Add(sensorOwner);
           return  await InitSensor(sensorOwner, sensorOwner, sensorPK);
            

        }
            
       
            public async Task<bool> InitSensor(string sensorOwner,string sensorName,string sensorPK)
        {
            if(!accountDic.ContainsKey(sensorOwner))
            {
                logger.AddLogEntey("No Private Key");
                return false;
            }
            if (publicKeys.Contains(sensorPK) || sensorNames.Contains(sensorName))
            {
                logger.AddLogEntey("Sesnor allready Define sensorNames");
                return false; ;
            }
            
            var account = accountDic[sensorOwner];
            var web3 = new Web3(account, url);
            var transferHandler = web3.Eth.GetContractTransactionHandler<InitSensorFunction>();      
           
            //var initSesorFunctionBase = new InitSensorFunction()
            //{
            //    SensorName = sensorName,
            //    Sensor = sensorPK
            //};
            //var transactionReceipt2 = await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, initSesorFunctionBase);

            /////////////////////////////////////////////////////////////////
            ///
            var initSesorFunctionBase = new InitSensorFunction()
            {
                SensorName = sensorName,
                Sensor = sensorPK
            };
            //    var initSensorFunctionHandler = web3.Eth.GetContractQueryHandler<InitSensorFunction>();
            //return await initSensorFunctionHandler.QueryAsync<bool>(contractAddress, initSesorFunctionBase);
            //var transferHandler = web3.Eth.GetContractTransactionHandler<InitSensorFunction>();

            var initSesorReceipt = await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, initSesorFunctionBase);
            var eventList = initSesorReceipt.DecodeAllEvents<InitSensorEventDTOBase>();
            foreach(var ev in eventList)
            {
                logger.AddLogEntey("Sensor: "+ ev.Event.Sensor);
                logger.AddLogEntey("SensorOwner: " + ev.Event.SensorOwner);
                logger.AddLogEntey("Name: " + ev.Event.Name);
            }
            logger.AddLogEntey("BlockNumber: " + initSesorReceipt.BlockNumber);
            logger.AddLogEntey("GasUsed: " + initSesorReceipt.GasUsed);
            logger.AddLogEntey("Status: " + initSesorReceipt.Status);
            //////////////////////////////////////////////////////
            if (!senVM.ContainsKey(sensorName))
            {
                var svm = new SensorModel();
                svm.SensorName = sensorName;
                svm.SensorPublicKey = sensorPK;
                senVM.Add(svm.SensorName, svm);
            }     
            if(!sensorNames.Contains(sensorName))
            {
                sensorNames.Add(sensorName);                
            }
            if (!publicKeys.Contains(sensorPK))
            {
                publicKeys.Add(sensorPK);
            }
            AddNewSensor?.Invoke(this, null);
            return true;
        }
        public async  Task<bool> ChangeAccessRight(string sensorOwner,  string fromSensor, string toSensorName, bool accses)
        {
            if (!accountDic.ContainsKey(sensorOwner) )
            {
                logger.AddLogEntey(sensorOwner + " doesn't have private key");
                return false; 
            }
            if (!senVM.ContainsKey(fromSensor))
            {
                logger.AddLogEntey(fromSensor + " doesn't have public key");
                return false;
            }
            if (!senVM.ContainsKey(toSensorName))
            {
                logger.AddLogEntey(toSensorName + " doesn't have public key");
                return false;
            }
            var account = accountDic[sensorOwner];
            var web3 = new Web3(account, url);
            var grentAccessFunctionferHandler = web3.Eth.GetContractTransactionHandler<GrentAccessFunction>();
            var fromSensorPk = senVM[fromSensor].SensorPublicKey;
            var toSensorPk = senVM[toSensorName].SensorPublicKey;
            var grentAccessFunction = new GrentAccessFunction()
            {
                Sensor = fromSensorPk,
                ToSensor = toSensorPk,
                Access = accses
            };
            var grentAccessReceipt =  await grentAccessFunctionferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, grentAccessFunction);
            var eventList = grentAccessReceipt.DecodeAllEvents<GrentAccessEventDTO>();
            var eventList1 = grentAccessReceipt.DecodeAllEvents<GrentAccess1EventDTO>();
            foreach (var ev in eventList1)
            {
                logger.AddLogEntey("Message " + ev.Event.ErrorMessage);               
            }
            foreach (var ev in eventList)
            {
                logger.AddLogEntey("From Sensor: " + ev.Event.Sensor);
                logger.AddLogEntey("ToSensor: " + ev.Event.ToSensor);
                logger.AddLogEntey("Access: " + ev.Event.Access);
                logger.AddLogEntey("Status: " + ev.Event.Status);
            }
            logger.AddLogEntey("BlockNumber: " + grentAccessReceipt.BlockNumber);
            logger.AddLogEntey("GasUsed: " + grentAccessReceipt.GasUsed);
            logger.AddLogEntey("Status: " + grentAccessReceipt.Status);
            return  await IsAccessAllow(account ,fromSensorPk, toSensorPk);
        }

        private  Task<bool> IsAccessAllow(Account account, string fromPK,string toPK)
        {
            var web3 = new Web3(account, url);
            var isAccessAllowFunctionHandler = web3.Eth.GetContractQueryHandler<IsAccessAllowFunction>();
            var isAccessAllowFunction = new IsAccessAllowFunction()
            {
                From = fromPK,
                To = toPK,
            };
            return  isAccessAllowFunctionHandler.QueryAsync<bool>(contractAddress, isAccessAllowFunction);

        }
        public async Task GetSenssorDataFromConract(string sensorName)
        {
            if(!senVM.ContainsKey(sensorName))
            {
                return;
            };
            var sensorModel = senVM[sensorName];
            var sesnorPk = sensorModel.SensorPublicKey;
            var account = accountDic.First().Value;
            var web3 = new Web3(account, url);
            
            sensorModel.AccsesRights.Clear();
            foreach (var model in senVM.Values)
            {
                if(model.SensorName.Equals(sensorName))
                 {
                    continue;
                }                
              
                var isAccessAllowFunction = new IsAccessAllowFunction()
                {
                    From = sesnorPk,
                    To = model.SensorPublicKey,
                };
                var isAccessAllowFunctionHandler = web3.Eth.GetContractQueryHandler<IsAccessAllowFunction>();
                var isAccessAllowFunctionAnswer = await isAccessAllowFunctionHandler.QueryAsync<bool>(contractAddress, isAccessAllowFunction);
                sensorModel.AccsesRights.Add(new AccessRight { SensorName = model.SensorName, AccessePermition = isAccessAllowFunctionAnswer });
            }
            RaizeEvent(sensorName);
        }       
        public void GetSenssorData(string sensorName)
        {
            Task.Run(() => { GetSenssorDataFromConract(sensorName); });
        }
        
       
    }
}
