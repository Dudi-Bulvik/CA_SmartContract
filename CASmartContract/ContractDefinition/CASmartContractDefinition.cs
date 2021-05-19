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
        public static string BYTECODE = "608060405234801561001057600080fd5b506108a3806100206000396000f3fe608060405234801561001057600080fd5b50600436106100575760003560e01c806357276f0e1461005c5780637241279c146100845780637743bd171461009557806390fdba80146100b6578063fc60fd70146100f6575b600080fd5b61006f61006a3660046106c0565b610109565b60405190151581526020015b60405180910390f35b60025460405190815260200161007b565b6100a86100a336600461077d565b6102a7565b60405161007b929190610795565b61006f6100c4366004610644565b6001600160a01b0391821660009081526001602090815260408083209390941682526003909201909152205460ff1690565b61006f610104366004610676565b6103f2565b6001600160a01b038216600090815260016020526040812054600160a01b900460ff1661023e576001600160a01b038316600090815260016020818152604090922080546001600160a81b0319163317600160a01b17815584516101759391909201919085019061058f565b506001600160a01b0383166000818152600160208181526040808420600290810180546001600160a01b031990811688179091558154948501825594527f405787fa12a823e0f2b7631cc41b3ba8828b3321ca811111fa75cd3aa3bb5ace909201805490931684179092558051338152918201929092526060818301819052600790820152667375636365737360c81b608082015290517f1304cafa734a4292f7d120fb43b8220c3f619be6f1dd1263698fa075b2c8ae649181900360a00190a15060016102a1565b604080513381526001600160a01b038516602082015260608183018190526006908201526511985a5b195960d21b608082015290517f1304cafa734a4292f7d120fb43b8220c3f619be6f1dd1263698fa075b2c8ae649181900360a00190a15060005b92915050565b6060600060018160026102ba83876107f9565b815481106102d857634e487b7160e01b600052603260045260246000fd5b60009182526020808320909101546001600160a01b031683528201929092526040018120600190810191600261030e83886107f9565b8154811061032c57634e487b7160e01b600052603260045260246000fd5b60009182526020808320909101546001600160a01b039081168452908301939093526040909101902060020154825491169082906103699061081c565b80601f01602080910402602001604051908101604052809291908181526020018280546103959061081c565b80156103e25780601f106103b7576101008083540402835291602001916103e2565b820191906000526020600020905b8154815290600101906020018083116103c557829003601f168201915b5050505050915091509150915091565b6001600160a01b038316600090815260016020526040812054600160a01b900460ff1661047b577f8b9dee208f992615672aabdce25d935ca96d55ded0403a60acc917bcb4a9890960405161046b9060208082526010908201526f2330b4b632b21d1027379037bbb732b960811b604082015260600190565b60405180910390a1506000610588565b6001600160a01b038481166000908152600160205260409020541633146104fb577f8b9dee208f992615672aabdce25d935ca96d55ded0403a60acc917bcb4a9890960405161046b906020808252601f908201527f4661696c65643a206d73672e73656e646572206973206e6f74206f776e657200604082015260600190565b6001600160a01b038085166000908152600160209081526040808320938716835260039093019052819020805484151560ff19909116179055517f8b9dee208f992615672aabdce25d935ca96d55ded0403a60acc917bcb4a989099061057c906020808252600790820152665375636365737360c81b604082015260600190565b60405180910390a15060015b9392505050565b82805461059b9061081c565b90600052602060002090601f0160209004810192826105bd5760008555610603565b82601f106105d657805160ff1916838001178555610603565b82800160010185558215610603579182015b828111156106035782518255916020019190600101906105e8565b5061060f929150610613565b5090565b5b8082111561060f5760008155600101610614565b80356001600160a01b038116811461063f57600080fd5b919050565b60008060408385031215610656578182fd5b61065f83610628565b915061066d60208401610628565b90509250929050565b60008060006060848603121561068a578081fd5b61069384610628565b92506106a160208501610628565b9150604084013580151581146106b5578182fd5b809150509250925092565b600080604083850312156106d2578182fd5b6106db83610628565b9150602083013567ffffffffffffffff808211156106f7578283fd5b818501915085601f83011261070a578283fd5b81358181111561071c5761071c610857565b604051601f8201601f19908116603f0116810190838211818310171561074457610744610857565b8160405282815288602084870101111561075c578586fd5b82602086016020830137856020848301015280955050505050509250929050565b60006020828403121561078e578081fd5b5035919050565b6000604082528351806040840152815b818110156107c257602081870181015160608684010152016107a5565b818111156107d35782606083860101525b506001600160a01b0393909316602083015250601f91909101601f191601606001919050565b60008282101561081757634e487b7160e01b81526011600452602481fd5b500390565b600181811c9082168061083057607f821691505b6020821081141561085157634e487b7160e01b600052602260045260246000fd5b50919050565b634e487b7160e01b600052604160045260246000fdfea26469706673582212204d10c0a35d5cf66cf68eb88b834fb6bfdf348d10b77a54f4e1d8085b69c82f3264736f6c63430008030033";
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
        [Parameter("string", "status", 3, false )]
        public virtual string Status { get; set; }
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
