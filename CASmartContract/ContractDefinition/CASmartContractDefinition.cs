using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace CASmartContract.Contracts.CASmartContract.ContractDefinition
{


    public partial class CASmartContractDeployment : CASmartContractDeploymentBase
    {
        public CASmartContractDeployment() : base(BYTECODE) { }
        public CASmartContractDeployment(string byteCode) : base(byteCode) { }
    }

    public class CASmartContractDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b50610385806100206000396000f3fe608060405234801561001057600080fd5b50600436106100415760003560e01c8063af89f65114610046578063d0106bc114610080578063fc60fd7014610136575b600080fd5b61006c6004803603602081101561005c57600080fd5b50356001600160a01b031661016e565b604080519115158252519081900360200190f35b61006c6004803603604081101561009657600080fd5b6001600160a01b0382351691908101906040810160208201356401000000008111156100c157600080fd5b8201836020820111156100d357600080fd5b803590602001918460018302840111640100000000831117156100f557600080fd5b91908080601f01602080910402602001604051908101604052809392919081815260200183838082843760009201919091525092955061019b945050505050565b61006c6004803603606081101561014c57600080fd5b506001600160a01b038135811691602081013590911690604001351515610223565b336000908152602081815260408083206001600160a01b038516845260030190915290205460ff16919050565b6001600160a01b038216600090815260208190526040812054600160a01b900460ff16610219576001600160a01b03831660009081526020818152604090912080546001600160a01b031960ff60a01b19909116600160a01b171633178155835161020f92600192909201918501906102ae565b506001905061021d565b5060005b92915050565b6001600160a01b038316600090815260208190526040812054600160a01b900460ff16801561026b57506001600160a01b038481166000908152602081905260409020541633145b156102a4576001600160a01b0384811660009081526020818152604080832093871683526003909301905220805460ff19168315151790555b5060009392505050565b828054600181600116156101000203166002900490600052602060002090601f0160209004810192826102e4576000855561032a565b82601f106102fd57805160ff191683800117855561032a565b8280016001018555821561032a579182015b8281111561032a57825182559160200191906001019061030f565b5061033692915061033a565b5090565b5b80821115610336576000815560010161033b56fea2646970667358221220e7e69c3e4e474be7afcb2973b5ead31137cfc9cf2515f604cf23ff0ed6563bbc64736f6c63430007040033";
        public CASmartContractDeploymentBase() : base(BYTECODE) { }
        public CASmartContractDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class GrentAccessFunction : GrentAccessFunctionBase { }

    [Function("GrentAccess", "bool")]
    public class GrentAccessFunctionBase : FunctionMessage
    {
        [Parameter("address", "sensor", 1)]
        public virtual string Sensor { get; set; }
        [Parameter("address", "toSensor", 2)]
        public virtual string ToSensor { get; set; }
        [Parameter("bool", "access", 3)]
        public virtual bool Access { get; set; }
    }

        public partial class IsAccessAllowFunction : IsAccessAllowFunctionBase { }

    [Function("IsAccessAllow", "bool")]
    public class IsAccessAllowFunctionBase : FunctionMessage
    {
        [Parameter("address", "sensor", 1)]
        public virtual string Sensor { get; set; }
    }

    public partial class InitSesorFunction : InitSesorFunctionBase { }

    [Function("initSesor", "bool")]
    public class InitSesorFunctionBase : FunctionMessage
    {
        [Parameter("address", "sensor", 1)]
        public virtual string Sensor { get; set; }
        [Parameter("string", "sensorName", 2)]
        public virtual string SensorName { get; set; }
    }



    public partial class IsAccessAllowOutputDTO : IsAccessAllowOutputDTOBase { }

    [FunctionOutput]
    public class IsAccessAllowOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "access", 1)]
        public virtual bool Access { get; set; }
    }


}
