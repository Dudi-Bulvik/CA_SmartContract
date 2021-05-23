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
        public Task<bool> IsAccessAllow(string fromSensor, string toSensorName);
        public Task<decimal> GetAccountBalance(string address ,string publicKey =null);
    }
    public class CAService : ICaService
    {
        private string contractAddress;
        private List<string> sensorNames = new List<string>();
        private List<string> sensorOwners = new List<string>();
        private string url = @"https://rinkeby.infura.io/v3/d07d714973ba46fcbcf79b770db878d0";

        private Dictionary<string, Account> accountDic = new Dictionary<string, Account>();
        public Dictionary<string, SensorModel> senVM = new Dictionary<string, SensorModel>();
        List<string> publicKeys = new List<string>();
        private object web3;
        private ILogger logger;

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
        public async Task InitializeAsync(ISettings settings, ILogger logger)
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
                    if (publicKeys.Contains(svm.SensorPublicKey) || sensorNames.Contains(svm.SensorName))
                    {
                        continue;
                    }
                    //accountDic.Add(svm.SensorName, new Account(svm.SensorPrivateKey));
                    senVM.Add(svm.SensorName, svm);
                    //publicKeys.Add(svm.SensorPublicKey);

                    await InitOwner(svm.SensorName, svm.SensorPublicKey, svm.SensorPrivateKey);

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
        public async Task<bool> InitOwner(string sensorOwner, string sensorPK, string sensorPrivateKey)
        {
            if (accountDic.ContainsKey(sensorOwner))
            {
                logger.AddLogEntey(sensorOwner + " is allready define");
                return true;
            }
            accountDic[sensorOwner] = new Account(sensorPrivateKey);
            sensorOwners.Add(sensorOwner);
            return await InitSensor(sensorOwner, sensorOwner, sensorPK);


        }


        public async Task<bool> InitSensor(string sensorOwner, string sensorName, string sensorPK)
        {
            var message = new StringBuilder();
            message.AppendLine("/////////////////////InitSensor////////////////");
            if (!accountDic.ContainsKey(sensorOwner))
            {
                message.AppendLine("No Private Key");
                logger.AddLogEntey(message.ToString());
                return false;
            }
            if (publicKeys.Contains(sensorPK) || sensorNames.Contains(sensorName))
            {
                message.AppendLine("Sesnor allready Define");
                logger.AddLogEntey(message.ToString());
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
            foreach (var ev in eventList)
            {
                message.AppendLine("Sensor Owner: " + sensorOwner + " Public Key: " + ev.Event.SensorOwner);
                message.AppendLine("sensor Name:  " + sensorName + " Public Key: " + ev.Event.Sensor);
                message.AppendLine("status: " + ev.Event.Status);
            }
            message.AppendLine("BlockNumber: " + initSesorReceipt.BlockNumber);
            message.AppendLine("TransactionIndex: " + initSesorReceipt.TransactionIndex);
            message.AppendLine("GasUsed: " + initSesorReceipt.GasUsed);
            message.AppendLine("CumulativeGasUsed: " + initSesorReceipt.CumulativeGasUsed);
            logger.AddLogEntey(message.ToString());
            //////////////////////////////////////////////////////
            if (!senVM.ContainsKey(sensorName))
            {
                var svm = new SensorModel();
                svm.SensorName = sensorName;
                svm.SensorPublicKey = sensorPK;
                senVM.Add(svm.SensorName, svm);
            }
            if (!sensorNames.Contains(sensorName))
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
        public async Task<bool> ChangeAccessRight(string sensorOwner, string fromSensor, string toSensorName, bool accses)
        {
            var message = new StringBuilder();
            message.AppendLine("/////////////////////ChangeAccessRight////////////////");
            if (!accountDic.ContainsKey(sensorOwner))
            {
                message.AppendLine(sensorOwner + " doesn't have private key");
                logger.AddLogEntey(message.ToString());
                return false;
            }
            if (!senVM.ContainsKey(fromSensor))
            {
                message.AppendLine(fromSensor + " doesn't have public key");
                logger.AddLogEntey(message.ToString());
                return false;
            }
            if (!senVM.ContainsKey(toSensorName))
            {
                message.AppendLine(toSensorName + " doesn't have public key");
                logger.AddLogEntey(message.ToString());
                return false;
            }
            try
            {
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
                var grentAccessReceipt = await grentAccessFunctionferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, grentAccessFunction);
                var eventList = grentAccessReceipt.DecodeAllEvents<GrentAccessEventDTO>();

                foreach (var ev in eventList)
                {
                    message.AppendLine("Message: " + ev.Event.ErrorMessage);
                }
                message.AppendLine("BlockNumber: " + grentAccessReceipt.BlockNumber);
                message.AppendLine("TransactionIndex: " + grentAccessReceipt.TransactionIndex);
                message.AppendLine("GasUsed: " + grentAccessReceipt.GasUsed);
                message.AppendLine("CumulativeGasUsed: " + grentAccessReceipt.CumulativeGasUsed);
                logger.AddLogEntey(message.ToString());
                return await IsAccessAllow(account, fromSensorPk, toSensorPk);
            } catch (Exception e)
            {
                message.AppendLine(e.ToString());
                logger.AddLogEntey(message.ToString());
                return false;
            }
        }

        public async Task<bool> IsAccessAllow(string fromSensor, string toSensorName)
        {
            var message = new StringBuilder();
            message.AppendLine("/////////////////////IsAccessAllow////////////////");

            if (!senVM.ContainsKey(fromSensor))
            {
                message.AppendLine(fromSensor + " doesn't have public key");
                logger.AddLogEntey(message.ToString());
                return false;
            }
            if (!senVM.ContainsKey(toSensorName))
            {
                message.AppendLine(toSensorName + " doesn't have public key");
                logger.AddLogEntey(message.ToString());
                return false;
            }
            try
            {
                var fromSensorPk = senVM[fromSensor].SensorPublicKey;
                var toSensorPk = senVM[toSensorName].SensorPublicKey;
                Account account = accountDic.Values.FirstOrDefault();
                return await IsAccessAllow(account, fromSensorPk, toSensorPk);
            }
            catch (Exception e)
            {
                message.AppendLine(e.ToString());
                return false;

            }
        }
        private Task<bool> IsAccessAllow(Account account, string fromPK, string toPK)
        {
            var web3 = new Web3(account, url);
            var isAccessAllowFunctionHandler = web3.Eth.GetContractQueryHandler<IsAccessAllowFunction>();
            var isAccessAllowFunction = new IsAccessAllowFunction()
            {
                From = fromPK,
                To = toPK,
            };
            return isAccessAllowFunctionHandler.QueryAsync<bool>(contractAddress, isAccessAllowFunction);

        }
        public async Task GetSenssorDataFromConract(string sensorName)
        {
            if (!senVM.ContainsKey(sensorName))
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
                if (model.SensorName.Equals(sensorName))
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
        public async Task<decimal> GetAccountBalance(string sensorName, string publicKey = null)
        {
            try
            {
                var message = new StringBuilder();
                message.AppendLine("/////////////////////GetAccountBalance////////////////");
                if (!senVM.ContainsKey(sensorName) && publicKey== null)
                {                    
                    message.AppendLine("No public Key");
                    logger.AddLogEntey(message.ToString());
                    return 0;
                };
                string adress = publicKey;
                if(adress == null)
                {
                    var sensorModel = senVM[sensorName];
                    adress = sensorModel.SensorPublicKey;
                }          
               
                var web3 = new Web3(url);
                var balance = await web3.Eth.GetBalance.SendRequestAsync(adress);
                balance = await web3.Eth.GetBalance.SendRequestAsync("0x2339dc10423B924125234A394f9eeADa68EF3F0A");
                //Console.WriteLine($"Balance in Wei: {balance.Value}");                
                var etherAmount = Web3.Convert.FromWei(balance.Value);
                
                //Console.WriteLine($"Balance in Ether: {etherAmount}");
                message.AppendLine($"Balance is: " + etherAmount);
                logger.AddLogEntey(message.ToString());
                return etherAmount;
            }
            catch (Exception e)
            {
                logger.AddLogEntey($"Balance in Ether failed \n" + e);
                return 0;
            }
        }

        public void GetSenssorData(string sensorName)
        {
            Task.Run(() => { GetSenssorDataFromConract(sensorName); });
        }

    }
}
