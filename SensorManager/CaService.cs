﻿using CASmartContract.Contracts.CASmartContract.ContractDefinition;
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
        public void ChangeAccessRight(string sensorOwner, string fromSensor, string toSensorName, bool accses);
        public void GetSenssorData(string sensorName);
        public void InitSensor(string sensorOwner,string sensorName,string sensorPublikKey);
        public void InitOwner(string sensorOwner, string sensorPublikKey, string SensorPrivateKey);

    }
    public class CAService : ICaService
    {
        private string contractAddress;
        private List<string> sensorNames= new List<string>();
        private string url = @"https://rinkeby.infura.io/v3/d07d714973ba46fcbcf79b770db878d0";

        private Dictionary<string, Account> accountDic = new Dictionary<string, Account>();
        public Dictionary<string, SensorModel> senVM = new Dictionary<string, SensorModel>();
        private object web3;
        private readonly ILogger logger;

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




        public event EventHandler<SensorModel> SensorDataArrived;
        public CAService(ISettings Settings,ILogger logger)
        {
            this.logger = logger;

            if (!File.Exists(Settings.ConfigurationFilePath))
            {
                throw new Exception();
            }
            using (var fs = File.Open(Settings.ConfigurationFilePath, FileMode.Open))
            using (TextReader sr = new StreamReader(fs))
            {
                contractAddress = sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    SensorNames.Add(line);
                    var svm = new SensorModel();
                    svm.SensorName = line;
                    svm.SensorPublicKey = sr.ReadLine();
                    svm.SensorPrivateKey = sr.ReadLine();
                    accountDic.Add(svm.SensorName, new Account(svm.SensorPrivateKey));
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
        public async void InitSensorOwner(string sensorOwner, string sensorPK, string  sensorPrivateKey)
        {
            if (accountDic.ContainsKey(sensorOwner))
            {
                return ;
            }
            accountDic[sensorOwner] = new Account(sensorPrivateKey);
            SendInitSensor(sensorOwner, sensorOwner, sensorPK);
            

        }
            
       
            public async void SendInitSensor(string sensorOwner,string sensorName,string sensorPK)
        {
            if(!accountDic.ContainsKey(sensorOwner))
            {
                logger.AddLogEntey("No Private Key");
               
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
                logger.AddLogEntey(ev.ToString());
                
            }
            logger.AddLogEntey("BlockNumber: " + initSesorReceipt.BlockNumber);
            logger.AddLogEntey("GasUsed: " + initSesorReceipt.GasUsed);
            logger.AddLogEntey("GasUsed: " + initSesorReceipt.Status);
            //////////////////////////////////////////////////////
            if (!senVM.ContainsKey(sensorName))
            {
                var svm = new SensorModel();
                svm.SensorName = sensorName;
                svm.SensorPublicKey = sensorPK;
                senVM.Add(svm.SensorName, svm);
            }
           
        }
        public async Task ChangeAccessRightFromConract(string sensorOwner,  string fromSensor, string toSensorName, bool accses)
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
            GetSenssorData(sensorOwner);
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

        public void ChangeAccessRight(string sensorOwner, string fromSensor, string toSensorName, bool accses)
        {
            Task.Run(() => { ChangeAccessRightFromConract(sensorOwner, fromSensor, toSensorName, accses); });
        }

        public void GetSenssorData(string sensorName)
        {
            Task.Run(() => { GetSenssorDataFromConract(sensorName); });
        }

        void ICaService.InitSensor(string sensorOwner, string sensorName, string sensorPublikKey)
        {
            SendInitSensor(sensorOwner, sensorName, sensorPublikKey);
        }

        public void InitOwner(string sensorOwner, string sensorPublikKey, string SensorPrivateKey)
        {
            InitSensorOwner(sensorOwner, sensorPublikKey, SensorPrivateKey);
        }
    }
}
