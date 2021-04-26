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
        public static string BYTECODE = "608060405234801561001057600080fd5b50610757806100206000396000f3fe608060405234801561001057600080fd5b50600436106100415760003560e01c806357276f0e1461004657806390fdba801461006d578063fc60fd70146100ab575b600080fd5b610059610054366004610528565b6100be565b604051901515815260200160405180910390f35b61005961007b3660046104ac565b6001600160a01b039182166000908152602081815260408083209390941682526003909201909152205460ff1690565b6100596100b93660046104de565b61020f565b6001600160a01b038216600090815260208190526040812054600160a01b900460ff16610193576001600160a01b03831660009081526020818152604090912080546001600160a81b0319163317600160a01b178155835161012992600192909201918501906103f7565b507f1304cafa734a4292f7d120fb43b8220c3f619be6f1dd1263698fa075b2c8ae643384600080876001600160a01b03166001600160a01b03168152602001908152602001600020600101604051610183939291906105e5565b60405180910390a1506001610209565b6001600160a01b0383811660008181526020818152604091829020548251941684528301919091526060828201819052600890830152672430b9a7bbb732b960c11b6080830152517f1304cafa734a4292f7d120fb43b8220c3f619be6f1dd1263698fa075b2c8ae649181900360a00190a15060005b92915050565b6001600160a01b038316600090815260208190526040812054600160a01b900460ff16610290577f19f9d86af6ec532a9f1e3751c730ee0977623b92153acf100cfcd77153c0b7cb6040516102809060208082526008908201526727379037bbb732b960c11b604082015260600190565b60405180910390a15060006103f0565b6001600160a01b038481166000908152602081905260409020541633141561031a577f19f9d86af6ec532a9f1e3751c730ee0977623b92153acf100cfcd77153c0b7cb6040516103119060208082526017908201527f6d73672e73656e646572206973206e6f74206f776e6572000000000000000000604082015260600190565b60405180910390a15b6001600160a01b038481166000818152602081815260408083209488168084526003909501825291829020805460ff19168715159081179091558251938452908301939093526080908201819052600790820152667375636365737360c81b60a082015260ff909116151560608201527f7b82055e4bf2b414d71e36876b3d5bb29802347075626ab62775e3972389416b9060c00160405180910390a17f19f9d86af6ec532a9f1e3751c730ee0977623b92153acf100cfcd77153c0b7cb6040516103e4906106a9565b60405180910390a15060015b9392505050565b828054610403906106d0565b90600052602060002090601f016020900481019282610425576000855561046b565b82601f1061043e57805160ff191683800117855561046b565b8280016001018555821561046b579182015b8281111561046b578251825591602001919060010190610450565b5061047792915061047b565b5090565b5b80821115610477576000815560010161047c565b80356001600160a01b03811681146104a757600080fd5b919050565b600080604083850312156104be578182fd5b6104c783610490565b91506104d560208401610490565b90509250929050565b6000806000606084860312156104f2578081fd5b6104fb84610490565b925061050960208501610490565b91506040840135801515811461051d578182fd5b809150509250925092565b6000806040838503121561053a578182fd5b61054383610490565b9150602083013567ffffffffffffffff8082111561055f578283fd5b818501915085601f830112610572578283fd5b8135818111156105845761058461070b565b604051601f8201601f19908116603f011681019083821181831017156105ac576105ac61070b565b816040528281528860208487010111156105c4578586fd5b82602086016020830137856020848301015280955050505050509250929050565b6001600160a01b03848116825283166020808301919091526060604083015282546000918291600181811c908281168061062057607f831692505b84831081141561063e57634e487b7160e01b87526022600452602487fd5b606088018390526080880181801561065d576001811461066e57610698565b60ff19861682528682019750610698565b60008b815260209020895b8681101561069257815484820152908501908801610679565b83019850505b50959b9a5050505050505050505050565b6000602082526102096020830160078152667375636365737360c81b602082015260400190565b600181811c908216806106e457607f821691505b6020821081141561070557634e487b7160e01b600052602260045260246000fd5b50919050565b634e487b7160e01b600052604160045260246000fdfea2646970667358221220bd2554a8c62e5707d509bb81a0f9920404fe292b0a2872a14c05a5f7898ddd1164736f6c63430008030033";
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

    public partial class InitSensorFunction : InitSensorFunctionBase { }

    [Function("InitSensor", "bool")]
    public class InitSensorFunctionBase : FunctionMessage
    {
        [Parameter("address", "sensor", 1)]
        public virtual string Sensor { get; set; }
        [Parameter("string", "sensorName", 2)]
        public virtual string SensorName { get; set; }
    }

    public partial class IsAccessAllowFunction : IsAccessAllowFunctionBase { }

    [Function("IsAccessAllow", "bool")]
    public class IsAccessAllowFunctionBase : FunctionMessage
    {
        [Parameter("address", "from", 1)]
        public virtual string From { get; set; }
        [Parameter("address", "to", 2)]
        public virtual string To { get; set; }
    }

    public partial class GrentAccessEventDTO : GrentAccessEventDTOBase { }

    [Event("grentAccess")]
    public class GrentAccessEventDTOBase : IEventDTO
    {
        [Parameter("address", "sensor", 1, false )]
        public virtual string Sensor { get; set; }
        [Parameter("address", "toSensor", 2, false )]
        public virtual string ToSensor { get; set; }
        [Parameter("string", "status", 3, false )]
        public virtual string Status { get; set; }
        [Parameter("bool", "access", 4, false )]
        public virtual bool Access { get; set; }
    }

    public partial class GrentAccess1EventDTO : GrentAccess1EventDTOBase { }

    [Event("grentAccess1")]
    public class GrentAccess1EventDTOBase : IEventDTO
    {
        [Parameter("string", "errorMessage", 1, false )]
        public virtual string ErrorMessage { get; set; }
    }

    public partial class InitSensorEventDTO : InitSensorEventDTOBase { }

    [Event("initSensor")]
    public class InitSensorEventDTOBase : IEventDTO
    {
        [Parameter("address", "sensorOwner", 1, false )]
        public virtual string SensorOwner { get; set; }
        [Parameter("address", "sensor", 2, false )]
        public virtual string Sensor { get; set; }
        [Parameter("string", "name", 3, false )]
        public virtual string Name { get; set; }
    }





    public partial class IsAccessAllowOutputDTO : IsAccessAllowOutputDTOBase { }

    [FunctionOutput]
    public class IsAccessAllowOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "access", 1)]
        public virtual bool Access { get; set; }
    }
}
