using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using CASmartContract.Contracts.CASmartContract.ContractDefinition;

namespace CASmartContract.Contracts.CASmartContract
{
    public partial class CASmartContractService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, CASmartContractDeployment cASmartContractDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<CASmartContractDeployment>().SendRequestAndWaitForReceiptAsync(cASmartContractDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, CASmartContractDeployment cASmartContractDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<CASmartContractDeployment>().SendRequestAsync(cASmartContractDeployment);
        }

        public static async Task<CASmartContractService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, CASmartContractDeployment cASmartContractDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, cASmartContractDeployment, cancellationTokenSource);
            return new CASmartContractService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public CASmartContractService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<BigInteger> GetSensorCountQueryAsync(GetSensorCountFunction getSensorCountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetSensorCountFunction, BigInteger>(getSensorCountFunction, blockParameter);
        }

        
        public Task<BigInteger> GetSensorCountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetSensorCountFunction, BigInteger>(null, blockParameter);
        }

        public Task<GetSensornameOutputDTO> GetSensornameQueryAsync(GetSensornameFunction getSensornameFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetSensornameFunction, GetSensornameOutputDTO>(getSensornameFunction, blockParameter);
        }

        public Task<GetSensornameOutputDTO> GetSensornameQueryAsync(BigInteger sensorNumber, BlockParameter blockParameter = null)
        {
            var getSensornameFunction = new GetSensornameFunction();
                getSensornameFunction.SensorNumber = sensorNumber;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetSensornameFunction, GetSensornameOutputDTO>(getSensornameFunction, blockParameter);
        }

        public Task<string> GrentAccessRequestAsync(GrentAccessFunction grentAccessFunction)
        {
             return ContractHandler.SendRequestAsync(grentAccessFunction);
        }

        public Task<TransactionReceipt> GrentAccessRequestAndWaitForReceiptAsync(GrentAccessFunction grentAccessFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(grentAccessFunction, cancellationToken);
        }

        public Task<string> GrentAccessRequestAsync(string sensor, string toSensor, bool access)
        {
            var grentAccessFunction = new GrentAccessFunction();
                grentAccessFunction.Sensor = sensor;
                grentAccessFunction.ToSensor = toSensor;
                grentAccessFunction.Access = access;
            
             return ContractHandler.SendRequestAsync(grentAccessFunction);
        }

        public Task<TransactionReceipt> GrentAccessRequestAndWaitForReceiptAsync(string sensor, string toSensor, bool access, CancellationTokenSource cancellationToken = null)
        {
            var grentAccessFunction = new GrentAccessFunction();
                grentAccessFunction.Sensor = sensor;
                grentAccessFunction.ToSensor = toSensor;
                grentAccessFunction.Access = access;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(grentAccessFunction, cancellationToken);
        }

        public Task<string> InitSensorRequestAsync(InitSensorFunction initSensorFunction)
        {
             return ContractHandler.SendRequestAsync(initSensorFunction);
        }

        public Task<TransactionReceipt> InitSensorRequestAndWaitForReceiptAsync(InitSensorFunction initSensorFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(initSensorFunction, cancellationToken);
        }

        public Task<string> InitSensorRequestAsync(string sensor, string sensorName)
        {
            var initSensorFunction = new InitSensorFunction();
                initSensorFunction.Sensor = sensor;
                initSensorFunction.SensorName = sensorName;
            
             return ContractHandler.SendRequestAsync(initSensorFunction);
        }

        public Task<TransactionReceipt> InitSensorRequestAndWaitForReceiptAsync(string sensor, string sensorName, CancellationTokenSource cancellationToken = null)
        {
            var initSensorFunction = new InitSensorFunction();
                initSensorFunction.Sensor = sensor;
                initSensorFunction.SensorName = sensorName;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(initSensorFunction, cancellationToken);
        }

        public Task<bool> IsAccessAllowQueryAsync(IsAccessAllowFunction isAccessAllowFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<IsAccessAllowFunction, bool>(isAccessAllowFunction, blockParameter);
        }

        
        public Task<bool> IsAccessAllowQueryAsync(string from, string to, BlockParameter blockParameter = null)
        {
            var isAccessAllowFunction = new IsAccessAllowFunction();
                isAccessAllowFunction.From = from;
                isAccessAllowFunction.To = to;
            
            return ContractHandler.QueryAsync<IsAccessAllowFunction, bool>(isAccessAllowFunction, blockParameter);
        }
    }
}
