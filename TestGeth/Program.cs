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
        private static string  contractAddress = "0x18ef532851c26d53bca65b8787204602ebb2724f";
        static string privateKey = "0xacd8aed0732dfafc2f6483c0ff1611fa4162b1d70e24ff993fa0f39111d922d6";
        static Account account;
        static Web3 web3;

        static void Main(string[] args)
        {
             account = new Account(privateKey);
             web3 = new Web3(account, "https://rinkeby.infura.io/v3/d07d714973ba46fcbcf79b770db878d0");
            var toAddress = "0x2339dc10423B924125234A394f9eeADa68EF3F0A";
           //GetAccountBalance(toAddress).Wait();
            
           // Transfer(toAddress);
           // GetAccountBalance(toAddress).Wait();
            
            
            Demo().Wait();
            Console.ReadLine();


        }

        static async Task DeployContract()
        {
          
            string path = @"C:\Myproject\MyTest.txt";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            FileStream fs = File.Create(path);
            StreamWriter writer = new StreamWriter(fs);


            //Deploy contruct
            //var privateKeysensor = "0xf5ee10c67ecf5801ba9ed73ede16af5e91b33f526c13b8110a9cbaf5e6a385ad";
            Console.WriteLine("Deploying...");
            var deployment = new CASmartContractDeployment() { };

            var deploymentHandler = web3.Eth.GetContractDeploymentHandler<CASmartContractDeployment>();
            var transactionReceipt = await deploymentHandler.SendRequestAndWaitForReceiptAsync(deployment);
            contractAddress = transactionReceipt.ContractAddress;
            writer.WriteLine("ContractAddress: " + transactionReceipt.ContractAddress);
            writer.WriteLine("BlockNumber: " + transactionReceipt.BlockNumber);
            writer.WriteLine("TransactionIndex: " + transactionReceipt.TransactionIndex);
            fs.Flush();
            writer.Flush();
            writer.Close();
            return;
        }
        static async Task<bool> GrantAccees(string fromSensor, string toSensor,bool accsses)
        {
           // var grentAccessFunctionferHandler = web3.Eth.GetContractQueryHandler<GrentAccessFunction>();
            var grentAccessFunction = new GrentAccessFunction()
            {
                Sensor = fromSensor,
                ToSensor = toSensor,
                Access = accsses
            };
            //return await grentAccessFunctionferHandler.QueryAsync<bool>(contractAddress, grentAccessFunction);
            //writer.WriteLine("grentAccessFunction: 0x2339dc10423B924125234A394f9eeADa68EF3F0A 0x5B38Da6a701c568545dCfcB03FcB875f56beddC4");
            //writer.WriteLine("BlockNumber: " + grentAccessReceipt.BlockNumber);
            //writer.WriteLine("TransactionIndex: " + grentAccessReceipt.TransactionIndex);
            var grentAccessFunctionferHandler = web3.Eth.GetContractTransactionHandler<GrentAccessFunction>();
            var grentAccessReceipt = await grentAccessFunctionferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, grentAccessFunction);
            var transferEventOutput = grentAccessReceipt.DecodeAllEvents<GrentAccessEventDTO>();
            var transferEventOutput1 = grentAccessReceipt.DecodeAllEvents<GrentAccess1EventDTO>();
            return true;


        }
        static async Task<uint> GetCount()
        {
            var getSensorCountFunction = new GetSensorCountFunction()
            {
                
            };
            var getSensorCountFunctionHandler = web3.Eth.GetContractQueryHandler<GetSensorCountFunction>();
            return await getSensorCountFunctionHandler.QueryAsync<uint>(contractAddress, getSensorCountFunction);
        }

        static async Task<bool> IsAccseesAllow(string from,string to)
        {
            var isAccessAllowFunctionHandler = web3.Eth.GetContractQueryHandler<IsAccessAllowFunction>();
            var isAccessAllowFunction = new IsAccessAllowFunction()
            {
                From = from,
                To = to,
            };
           return await isAccessAllowFunctionHandler.QueryAsync<bool>(contractAddress, isAccessAllowFunction);
        }
        
        static async Task<bool> InitSensor(string sensorName,string sensorPublicKey)
        {
            var initSesorFunctionBase = new InitSensorFunction()
            {
                SensorName = sensorName,
                Sensor = sensorPublicKey
            };
           //    var initSensorFunctionHandler = web3.Eth.GetContractQueryHandler<InitSensorFunction>();
            //return await initSensorFunctionHandler.QueryAsync<bool>(contractAddress, initSesorFunctionBase);
            var transferHandler = web3.Eth.GetContractTransactionHandler<InitSensorFunction>();
            
            var initSesorReceipt = await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, initSesorFunctionBase);
            var eventList = initSesorReceipt.DecodeAllEvents<InitSensorEventDTOBase>();
            
            return true;
        }
        static public async Task<GetSensornameOutputDTOBase> GetSensorname(uint number)
        {
            var getSensornameFunction = new GetSensornameFunction()
            {
               SensorNumber = number
            };
            var sensornameHandler = web3.Eth.GetContractQueryHandler<GetSensornameFunction>();
            var getSensornameOutputDTOBaseReceipt = await sensornameHandler.QueryDeserializingToObjectAsync<GetSensornameOutputDTOBase>(getSensornameFunction, contractAddress);
            
            return getSensornameOutputDTOBaseReceipt;
        }


            static async Task Demo()
        {
            try
            {
                // Setup using the Nethereum public test chain

                //   await DeployContract();
                var count = await GetCount();
                //var anser2 = await GetSensorname(1);
                //for (uint i =1;i<count+1; i++)
                //{
                //    var anser = await GetSensorname(i);
                //}
                //var anwet1  =  await InitSensor("Dudi", "0x5B38Da6a701c568545dCfcB03FcB875f56beddC4");
                //var anwet2 = await InitSensor("Dudi2", "0x2339dc10423B924125234A394f9eeADa68EF3F0A");
                count = 10;
                while (count >0)
                { 
                 var tsak1 =await GrantAccees("0x5B38Da6a701c568545dCfcB03FcB875f56beddC4", "0x2339dc10423B924125234A394f9eeADa68EF3F0A",true);
                 var tsak2 =await  IsAccseesAllow("0x5B38Da6a701c568545dCfcB03FcB875f56beddC4", "0x2339dc10423B924125234A394f9eeADa68EF3F0A");
                var tsak3 = await GrantAccees("0x5B38Da6a701c568545dCfcB03FcB875f56beddC4", "0x2339dc10423B924125234A394f9eeADa68EF3F0A", false);
                var tsak4 = await IsAccseesAllow("0x5B38Da6a701c568545dCfcB03FcB875f56beddC4", "0x2339dc10423B924125234A394f9eeADa68EF3F0A");
                    count--;
                }
                return;
                 var service = new CASmartContractService(web3, contractAddress);
                var isAccessAllowFunction = new IsAccessAllowFunction()
                {
                    From = "0x5B38Da6a701c568545dCfcB03FcB875f56beddC4",
                    To = "0x2339dc10423B924125234A394f9eeADa68EF3F0A",
                };
                var answer = await service.IsAccessAllowQueryAsync(isAccessAllowFunction);
                //var ans= await service.GetSensorCountRequestAsync(new GetSensorCountFunction());

                ////init Sensor
                var initSen = await service.InitSensorRequestAndWaitForReceiptAsync( "0x2339dc10423B924125234A394f9eeADa68EF3F0A", "Sensor1");
                //var initSen = await service.InitSensorRequestAndWaitForReceiptAsync("0x5B38Da6a701c568545dCfcB03FcB875f56beddC4", "Sensor2");
                var eventList = initSen.DecodeAllEvents<InitSensorEventDTOBase>();
                foreach(var even in eventList)
                {
                    Console.WriteLine(even);
                }

                //var ans = await service.GetSensorCountRequestAndWaitForReceiptAsync<Big>(new GetSensorCountFunction());
                // var eventList2 = ans.DecodeAllEvents<CountEventDTO>();

                //  await InitSensor("Sensor1", "0x2339dc10423B924125234A394f9eeADa68EF3F0A");
                //  await InitSensor("Sensor2", "0x2339dc10423B924125234A394f9eeADa68EF3F0A");


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
                //var account2 = new Account("0xf5ee10c67ecf5801ba9ed73ede16af5e91b33f526c13b8110a9cbaf5e6a385ad");
                var web32 = new Web3(account, "https://rinkeby.infura.io/v3/d07d714973ba46fcbcf79b770db878d0");

                var isAccessAllowFunctionHandler = web3.Eth.GetContractQueryHandler<IsAccessAllowFunction>();
                var isAccessAllowFunction1 = new IsAccessAllowFunction()
                {
                    From = "0x2339dc10423B924125234A394f9eeADa68EF3F0A",
                    To = "0x5B38Da6a701c568545dCfcB03FcB875f56beddC4",
                };
                var isAccessAllowFunctionAnswer = await isAccessAllowFunctionHandler.QueryAsync<bool>(contractAddress, isAccessAllowFunction);
                
               

                
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

                //writer.Flush();
                //fs.Flush();
                //fs.Close();
                //Console.WriteLine("");
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
