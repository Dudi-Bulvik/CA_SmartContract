using CASmartContract.Contracts.CASmartContract;
using CASmartContract.Contracts.CASmartContract.ContractDefinition;
using Nethereum.Contracts;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TestGeth
{
    class Program
    {
         static  void Main(string[] args)
        {
            var toAddress = "0x2339dc10423B924125234A394f9eeADa68EF3F0A";
           //GetAccountBalance(toAddress).Wait();
            
           // Transfer(toAddress);
           // GetAccountBalance(toAddress).Wait();
            
            
            Demo().Wait();
            Console.ReadLine();


        }
        static async Task Demo()
        {
            try
            {
                // Setup using the Nethereum public test chain
                
                var privateKey = "0xacd8aed0732dfafc2f6483c0ff1611fa4162b1d70e24ff993fa0f39111d922d6";
                var account = new Account(privateKey);
                var web3 = new Web3(account, "https://rinkeby.infura.io/v3/d07d714973ba46fcbcf79b770db878d0");
                string path = @"C:\Myproject\MyTest.txt";
                if(File.Exists(path))
                {
                    File.Delete(path);
                }
                FileStream fs = File.Create(path);
                
                StreamWriter writer = new StreamWriter(fs);
                //var privateKeysensor = "0xf5ee10c67ecf5801ba9ed73ede16af5e91b33f526c13b8110a9cbaf5e6a385ad";
                Console.WriteLine("Deploying...");
                //var deployment = new CASmartContractDeployment() { };
                //var deploymentHandler = web3.Eth.GetContractDeploymentHandler<CASmartContractDeployment>();
                //var transactionReceipt = await deploymentHandler.SendRequestAndWaitForReceiptAsync(deployment);
                var contractAddress = "0xf857c1d2a6fc091ee2fce9d2a0f38f55d6325090";
                //writer.WriteLine("ContractAddress: " + transactionReceipt.ContractAddress);
                //writer.WriteLine("BlockNumber: " + transactionReceipt.BlockNumber);
                //writer.WriteLine("TransactionIndex: " + transactionReceipt.TransactionIndex);
                //fs.Flush();
                //writer.Flush();
                //writer.Close();
                //return;
                //  var receipt = await CASmartContractService.DeployContractAndWaitForReceiptAsync(web3, deployment);
                var transferHandler = web3.Eth.GetContractTransactionHandler<InitSensorFunction>();
                //var initSesorFunctionBase = new InitSensorFunction()
                //{
                //    SensorName = "Sensor1",
                //    Sensor = "0x2339dc10423B924125234A394f9eeADa68EF3F0A"
                //};
//                var initSesorFunctionBase = new InitSensorFunction()
                //{
                //    SensorName = "Sensor2",
                //    Sensor = "0x5B38Da6a701c568545dCfcB03FcB875f56beddC4"
                //};
                //var initSesorReceipt = await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, initSesorFunctionBase);
                //var eventList = initSesorReceipt.DecodeAllEvents<InitSensorEventDTOBase>();
                //writer.WriteLine("InitSensorFunction: Sensor1 0x2339dc10423B924125234A394f9eeADa68EF3F0A");
                //writer.WriteLine("BlockNumber: " + initSesorReceipt.BlockNumber);
                //writer.WriteLine("TransactionIndex: " + initSesorReceipt.TransactionIndex);
              

            
                //return;
                //var transferHandler2 = web3.Eth.GetContractTransactionHandler<InitSensorFunction>();
                //var initSesorFunctionBase2 = new InitSensorFunction()
                //{
                //    SensorName = "Sensor2",
                //    Sensor = "0x5B38Da6a701c568545dCfcB03FcB875f56beddC4"
                //};
                //var transactionReceipt2 = await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, initSesorFunctionBase);
                //writer.WriteLine("InitSensorFunction: Sensor2 0x5B38Da6a701c568545dCfcB03FcB875f56beddC4");
                //writer.WriteLine("BlockNumber: " + transactionReceipt2.BlockNumber);
                //writer.WriteLine("TransactionIndex: " + transactionReceipt2.TransactionIndex);

                //var grentAccessFunctionferHandler = web3.Eth.GetContractTransactionHandler<GrentAccessFunction>();
                //var grentAccessFunction = new GrentAccessFunction()
                //{
                //    Sensor = "0x2339dc10423B924125234A394f9eeADa68EF3F0A",
                //    ToSensor = "0x5B38Da6a701c568545dCfcB03FcB875f56beddC4",
                //    Access=true
                //};
                //var grentAccessReceipt = await grentAccessFunctionferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, grentAccessFunction);
                //writer.WriteLine("grentAccessFunction: 0x2339dc10423B924125234A394f9eeADa68EF3F0A 0x5B38Da6a701c568545dCfcB03FcB875f56beddC4");
                //writer.WriteLine("BlockNumber: " + grentAccessReceipt.BlockNumber);
                //writer.WriteLine("TransactionIndex: " + grentAccessReceipt.TransactionIndex);
               // var transferEventOutput = grentAccessReceipt.DecodeAllEvents<GrentAccessEventDTO>();
                //var transferEventOutput1 = grentAccessReceipt.DecodeAllEvents<GrentAccess1EventDTO>();
                //foreach(var message in transferEventOutput1)
                //{
                //    writer.WriteLine("message: " + message);
                //}
                var account2 = new Account("0xf5ee10c67ecf5801ba9ed73ede16af5e91b33f526c13b8110a9cbaf5e6a385ad");
                var web32 = new Web3(account, "https://rinkeby.infura.io/v3/d07d714973ba46fcbcf79b770db878d0");

                var isAccessAllowFunctionHandler = web3.Eth.GetContractQueryHandler<IsAccessAllowFunction>();
                var isAccessAllowFunction = new IsAccessAllowFunction()
                {
                    From = "0x2339dc10423B924125234A394f9eeADa68EF3F0A",
                    To = "0x5B38Da6a701c568545dCfcB03FcB875f56beddC4",
                };
                var isAccessAllowFunctionAnswer = await isAccessAllowFunctionHandler.QueryAsync<bool>(contractAddress, isAccessAllowFunction);
                
                var isAccessAllowFunctionHandler1 = web3.Eth.GetContractQueryHandler<IsAccessAllowFunction>();
                var isAccessAllowFunction1 = new IsAccessAllowFunction()
                {
                    To = "0x2339dc10423B924125234A394f9eeADa68EF3F0A",
                    From = "0x5B38Da6a701c568545dCfcB03FcB875f56beddC4",
                };
                var isAccessAllowFunctionAnswer1 = await isAccessAllowFunctionHandler1.QueryAsync<bool>(contractAddress, isAccessAllowFunction1);

                //var service = new CASmartContractService(web3, dontractAddress);
                ////var service = new CASmartContractService(web3, "0xd9145CCE52D386f254917e481eB44e9943F39138");
                // //Console.WriteLine($"Contract Deployment Tx Status: {receipt.Status.Value}");
                //Console.WriteLine($"Contract Address: {service.ContractHandler.ContractAddress}");
                //Console.WriteLine("");

                Console.WriteLine("Sending a transaction to the function set()...");
                //var receiptForSetFunctionCall = await service.InitSesorRequestAsync(new SetFunction() { X = 42, Gas = 400000 });
                //var receiptForSetFunctionCall = await service.InitSesorRequestAsync(new InitSesorFunction() { SensorName = "43", Sensor = "0x2339dc10423B924125234A394f9eeADa68EF3F0A" });
                //var access = await service.GrentAccessRequestAndWaitForReceiptAsync(new GrentAccessFunction() { Sensor= "0x2339dc10423B924125234A394f9eeADa68EF3F0A", ToSensor= "0xF0e8Ea9983765793a21D9eFa1c014Ce8b8b97e7f",Access=true });
                //var access1 = await service.GrentAccessRequestAndWaitForReceiptAsync(new GrentAccessFunction() { Sensor = "0x2339dc10423B924125234A394f9eeADa68EF3F0A", ToSensor = "0x5B38Da6a701c568545dCfcB03FcB875f56beddC4", Access = true });
                //var accountSensor = new Account(privateKeysensor);
                //var accountSensorWeb= new Web3(account);
                //var serviceSensor = new CASmartContractService(accountSensorWeb, "0xd9145CCE52D386f254917e481eB44e9943F39138");
                //var hasAccess= await serviceSensor.IsAccessAllowQueryAsync("0x5B38Da6a701c568545dCfcB03FcB875f56beddC4");

                //Console.WriteLine($"hasAccess: { hasAccess}" );

                writer.Flush();
                fs.Flush();
                fs.Close();
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("Finished");
            Console.ReadLine();
        }
        private static  async void Transfer(string toAddress)
        {
            try
            {
                var privateKey = "0xacd8aed0732dfafc2f6483c0ff1611fa4162b1d70e24ff993fa0f39111d922d6";
                var account = new Account(privateKey);
                var web3 = new Web3(account, "https://rinkeby.infura.io/v3/d07d714973ba46fcbcf79b770db878d0");

                var transaction = await web3.Eth.GetEtherTransferService()
                    .TransferEtherAndWaitForReceiptAsync(toAddress, 1.11m);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Balance in Ether");
            }
        }
        static async Task GetAccountBalance(string address)
        {
            try
            {
                var web3 = new Web3("https://rinkeby.infura.io/v3/d07d714973ba46fcbcf79b770db878d0");
                var balance = await web3.Eth.GetBalance.SendRequestAsync(address);
                 balance = await web3.Eth.GetBalance.SendRequestAsync("0x2339dc10423B924125234A394f9eeADa68EF3F0A");
                Console.WriteLine($"Balance in Wei: {balance.Value}");

                var etherAmount = Web3.Convert.FromWei(balance.Value);
                Console.WriteLine($"Balance in Ether: {etherAmount}");
            }
            catch( Exception e)
            {
                Console.WriteLine($"Balance in Ether");
            }
        }
    }
}
