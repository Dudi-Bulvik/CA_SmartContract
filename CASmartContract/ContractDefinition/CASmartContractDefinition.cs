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
        public static string BYTECODE = "608060405234801561001057600080fd5b506109ce806100206000396000f3fe608060405234801561001057600080fd5b50600436106100575760003560e01c806357276f0e1461005c5780637241279c146100845780637743bd171461009557806390fdba80146100b6578063fc60fd70146100f6575b600080fd5b61006f61006a366004610700565b610109565b60405190151581526020015b60405180910390f35b60025460405190815260200161007b565b6100a86100a33660046107bd565b6102a4565b60405161007b929190610899565b61006f6100c4366004610684565b6001600160a01b0391821660009081526001602090815260408083209390941682526003909201909152205460ff1690565b61006f6101043660046106b6565b6103ef565b6001600160a01b038216600090815260016020526040812054600160a01b900460ff16610226576001600160a01b038316600090815260016020818152604090922080546001600160a81b0319163317600160a01b1781558451610175939190920191908501906105cf565b506001600160a01b038316600081815260016020819052604091829020600280820180546001600160a01b0319908116871790915581548085019092557f405787fa12a823e0f2b7631cc41b3ba8828b3321ca811111fa75cd3aa3bb5ace909101805490911690941790935590517f1304cafa734a4292f7d120fb43b8220c3f619be6f1dd1263698fa075b2c8ae64926102169233928892909101906107d5565b60405180910390a150600161029e565b6001600160a01b03838116600081815260016020908152604091829020548251941684528301919091526060828201819052600890830152672430b9a7bbb732b960c11b6080830152517f1304cafa734a4292f7d120fb43b8220c3f619be6f1dd1263698fa075b2c8ae649181900360a00190a15060005b92915050565b6060600060018160026102b78387610924565b815481106102d557634e487b7160e01b600052603260045260246000fd5b60009182526020808320909101546001600160a01b031683528201929092526040018120600190810191600261030b8388610924565b8154811061032957634e487b7160e01b600052603260045260246000fd5b60009182526020808320909101546001600160a01b0390811684529083019390935260409091019020600201548254911690829061036690610947565b80601f016020809104026020016040519081016040528092919081815260200182805461039290610947565b80156103df5780601f106103b4576101008083540402835291602001916103df565b820191906000526020600020905b8154815290600101906020018083116103c257829003601f168201915b5050505050915091509150915091565b6001600160a01b038316600090815260016020526040812054600160a01b900460ff16610470577f19f9d86af6ec532a9f1e3751c730ee0977623b92153acf100cfcd77153c0b7cb6040516104609060208082526008908201526727379037bbb732b960c11b604082015260600190565b60405180910390a15060006105c8565b6001600160a01b038481166000908152600160205260409020541633146104f0577f19f9d86af6ec532a9f1e3751c730ee0977623b92153acf100cfcd77153c0b7cb6040516104609060208082526017908201527f6d73672e73656e646572206973206e6f74206f776e6572000000000000000000604082015260600190565b6001600160a01b0384811660008181526001602090815260408083209488168084526003909501825291829020805460ff19168715159081179091558251938452908301939093526080908201819052600790820152667375636365737360c81b60a082015260ff909116151560608201527f7b82055e4bf2b414d71e36876b3d5bb29802347075626ab62775e3972389416b9060c00160405180910390a17f19f9d86af6ec532a9f1e3751c730ee0977623b92153acf100cfcd77153c0b7cb6040516105bc906108fd565b60405180910390a15060015b9392505050565b8280546105db90610947565b90600052602060002090601f0160209004810192826105fd5760008555610643565b82601f1061061657805160ff1916838001178555610643565b82800160010185558215610643579182015b82811115610643578251825591602001919060010190610628565b5061064f929150610653565b5090565b5b8082111561064f5760008155600101610654565b80356001600160a01b038116811461067f57600080fd5b919050565b60008060408385031215610696578182fd5b61069f83610668565b91506106ad60208401610668565b90509250929050565b6000806000606084860312156106ca578081fd5b6106d384610668565b92506106e160208501610668565b9150604084013580151581146106f5578182fd5b809150509250925092565b60008060408385031215610712578182fd5b61071b83610668565b9150602083013567ffffffffffffffff80821115610737578283fd5b818501915085601f83011261074a578283fd5b81358181111561075c5761075c610982565b604051601f8201601f19908116603f0116810190838211818310171561078457610784610982565b8160405282815288602084870101111561079c578586fd5b82602086016020830137856020848301015280955050505050509250929050565b6000602082840312156107ce578081fd5b5035919050565b6001600160a01b03848116825283166020808301919091526060604083015282546000918291600181811c908281168061081057607f831692505b84831081141561082e57634e487b7160e01b87526022600452602487fd5b606088018390526080880181801561084d576001811461085e57610888565b60ff19861682528682019750610888565b60008b815260209020895b8681101561088257815484820152908501908801610869565b83019850505b50959b9a5050505050505050505050565b6000604082528351806040840152815b818110156108c657602081870181015160608684010152016108a9565b818111156108d75782606083860101525b506001600160a01b0393909316602083015250601f91909101601f191601606001919050565b60006020825261029e6020830160078152667375636365737360c81b602082015260400190565b60008282101561094257634e487b7160e01b81526011600452602481fd5b500390565b600181811c9082168061095b57607f821691505b6020821081141561097c57634e487b7160e01b600052602260045260246000fd5b50919050565b634e487b7160e01b600052604160045260246000fdfea26469706673582212206ccc89fe0e3953405c0708d600a37e68bb7a6cee23a71b7df1d623eff3ea4fed64736f6c63430008030033";
        public CASmartContractDeploymentBase() : base(BYTECODE) { }
        public CASmartContractDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class GetSensorCountFunction : GetSensorCountFunctionBase { }

    [Function("GetSensorCount", "uint256")]
    public class GetSensorCountFunctionBase : FunctionMessage
    {

    }

    public partial class GetSensornameFunction : GetSensornameFunctionBase { }

    [Function("GetSensorname", typeof(GetSensornameOutputDTO))]
    public class GetSensornameFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "sensorNumber", 1)]
        public virtual BigInteger SensorNumber { get; set; }
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

    public partial class GetSensorCountOutputDTO : GetSensorCountOutputDTOBase { }

    [FunctionOutput]
    public class GetSensorCountOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "numberOfSensors", 1)]
        public virtual BigInteger NumberOfSensors { get; set; }
    }

    public partial class GetSensornameOutputDTO : GetSensornameOutputDTOBase { }

    [FunctionOutput]
    public class GetSensornameOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "name", 1)]
        public virtual string Name { get; set; }
        [Parameter("address", "key", 2)]
        public virtual string Key { get; set; }
    }





    public partial class IsAccessAllowOutputDTO : IsAccessAllowOutputDTOBase { }

    [FunctionOutput]
    public class IsAccessAllowOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "access", 1)]
        public virtual bool Access { get; set; }
    }
}
