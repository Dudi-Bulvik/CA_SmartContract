using CASmartContract.Contracts.CASmartContract;
using CASmartContract.Contracts.CASmartContract.ContractDefinition;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Threading.Tasks;

namespace TestGeth
{
    class Program
    {
         static  void Main(string[] args)
        {
            var toAddress = "0x2339dc10423B924125234A394f9eeADa68EF3F0A";
            //GetAccountBalance(toAddress).Wait();
            
            //Transfer(toAddress);
            GetAccountBalance(toAddress).Wait();
            
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
                var web3 = new Web3(account);
                var privateKeysensor = "0xf5ee10c67ecf5801ba9ed73ede16af5e91b33f526c13b8110a9cbaf5e6a385ad";
                Console.WriteLine("Deploying...");
                var deployment = new CASmartContractDeployment();
                var receipt = await CASmartContractService.DeployContractAndWaitForReceiptAsync(web3, deployment);

                var service = new CASmartContractService(web3, receipt.ContractAddress);
                //var service = new CASmartContractService(web3, "0xd9145CCE52D386f254917e481eB44e9943F39138");
                 //Console.WriteLine($"Contract Deployment Tx Status: {receipt.Status.Value}");
                Console.WriteLine($"Contract Address: {service.ContractHandler.ContractAddress}");
                Console.WriteLine("");

                Console.WriteLine("Sending a transaction to the function set()...");
                //var receiptForSetFunctionCall = await service.InitSesorRequestAsync(new SetFunction() { X = 42, Gas = 400000 });
                var receiptForSetFunctionCall = await service.InitSesorRequestAsync(new InitSesorFunction() { SensorName = "43", Sensor = "0x2339dc10423B924125234A394f9eeADa68EF3F0A" });
                var access = await service.GrentAccessRequestAndWaitForReceiptAsync(new GrentAccessFunction() { Sensor= "0x2339dc10423B924125234A394f9eeADa68EF3F0A", ToSensor= "0xF0e8Ea9983765793a21D9eFa1c014Ce8b8b97e7f",Access=true });
                var access1 = await service.GrentAccessRequestAndWaitForReceiptAsync(new GrentAccessFunction() { Sensor = "0x2339dc10423B924125234A394f9eeADa68EF3F0A", ToSensor = "0x5B38Da6a701c568545dCfcB03FcB875f56beddC4", Access = true });
                var accountSensor = new Account(privateKeysensor);
                var accountSensorWeb= new Web3(account);
                var serviceSensor = new CASmartContractService(accountSensorWeb, "0xd9145CCE52D386f254917e481eB44e9943F39138");
                var hasAccess= await serviceSensor.IsAccessAllowQueryAsync("0x5B38Da6a701c568545dCfcB03FcB875f56beddC4");

                Console.WriteLine($"hasAccess: { hasAccess}" );

                
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
                var web3 = new Web3(account);

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
                var web3 = new Web3();
                var balance = await web3.Eth.GetBalance.SendRequestAsync(address);
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
