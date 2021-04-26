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
        event EventHandler<SensorModel> SensorDataArrived;
        public void ChangeAccessRight(string fromSenstor ,string toSenstorName, bool accses);
        public void GetSenssorData(string sensorName);
       
    }
    public class CAService : ICaService
    {
        private string contractAddress;
        private string sensorName;
        private string url = @"https://rinkeby.infura.io/v3/d07d714973ba46fcbcf79b770db878d0";
        
        private Dictionary<string, Account> accountDic = new Dictionary<string, Account>();
        public Dictionary<string, SensorModel> senVM = new Dictionary<string, SensorModel>();
        private object web3;

        List<string> SensorNames { get;  } = new List<string>();

        List<string> ICaService.SensorNames => throw new NotImplementedException();

        public event EventHandler<SensorModel> SensorDataArrived;
        public CAService(string path)
        {
           
            if (!File.Exists(path))
            {
                throw new Exception();
            }
            using (var fs = File.Open(path, FileMode.Open))
            using (TextReader sr = new StreamReader(fs))
            {
                contractAddress = sr.ReadLine();
                string line;
                while (( line = sr.ReadLine()) != null)
                {
                    SensorNames.Add(line);
                    var svm = new SensorModel();
                    svm.SensorName = line;
                    svm.SensorPublicKey = sr.ReadLine();
                    svm.SensorPrivateKey = sr.ReadLine();
                    accountDic.Add(svm.SensorName,  new Account(sr.ReadLine()));
                    senVM.Add(svm.SensorName, svm);

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
        public async Task InitSensor(string sensorOwner,string sensorName,string sensorPK)
        {
            if(!accountDic.ContainsKey(sensorOwner))
            {
                return;
            }
            var account = accountDic[sensorOwner];
            var web3 = new Web3(account, url);
            var transferHandler = web3.Eth.GetContractTransactionHandler<InitSensorFunction>();      
           
            var initSesorFunctionBase = new InitSensorFunction()
            {
                SensorName = sensorName,
                Sensor = sensorPK
            };
            var transactionReceipt2 = await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, initSesorFunctionBase);
            if(!senVM.ContainsKey(sensorName))
            {
                var svm = new SensorModel();
                svm.SensorName = sensorName;
                svm.SensorPublicKey = sensorPK;
                senVM.Add(svm.SensorName, svm);
            }

        }
        public async Task ChangeAccessRight(string sensorOwner,  string fromSensor, string toSensorName, bool accses)
        {
            if (!accountDic.ContainsKey(sensorOwner) || !senVM.ContainsKey(fromSensor) || !senVM.ContainsKey(toSensorName))
            {
                return;
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
            var grentAccessReceipt = await grentAccessFunctionferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, grentAccessFunction);
        }

        public async Task GetSenssorData(string sensorName)
        {
            if(!senVM.ContainsKey(sensorName))
            {
                return;
            };
            var sensorModel = senVM[sensorName];
            var sesnorPk = sensorModel.SensorPublicKey;
            var account = accountDic.First().Value;
            var web3 = new Web3(account, url);
            var isAccessAllowFunctionHandler = web3.Eth.GetContractQueryHandler<IsAccessAllowFunction>();
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
                var isAccessAllowFunctionAnswer = await isAccessAllowFunctionHandler.QueryAsync<bool>(contractAddress, isAccessAllowFunction);
                sensorModel.AccsesRights.Add(new AccessRight { SensorName = model.SensorName, AccessePermition = isAccessAllowFunctionAnswer });
            }
            RaizeEvent(sensorName);
        }

        public void ChangeAccessRight(string sensorOwner, string fromSenstor, string toSenstorName, bool accses)
        {
            throw new NotImplementedException();
        }

        public void ChangeAccessRight(string fromSenstor, string toSenstorName, bool accses)
        {
            throw new NotImplementedException();
        }

        void ICaService.GetSenssorData(string sensorName)
        {
            throw new NotImplementedException();
        }
    }
}
