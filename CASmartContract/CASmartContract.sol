// SPDX-License-Identifier: GPL-3.0
pragma solidity ^0.5.11

  contract CASmartContract {
        struct SensorData{
             address SennsorOwner;
            bool HasOwner;
            string SensorName;
            address key;
            mapping(address=>bool) AccessList;
        }
        mapping(address => SensorData) private sensorData
    function initSesor(address sensor,  string memory sensorName) public returns (bool success) {
        if(!sensorData[sensor].HasOwner)
        {
            sensorData[sensor].HasOwner =true;
            sensorData[sensor].SennsorOwner = msg.sender;
            sensorData[sensor].SensorName  = sensorName;
            return success =true;
        }
        return false;
    }
    
    
    function GrentAccess(address sensor,address toSensor,bool access) public returns(bool success){

         if(sensorData[sensor].HasOwner && sensorData[sensor].SennsorOwner == msg.sender)
         {
             sensorData[sensor].AccessList[toSensor] =access;
         }
        return false;
    }
    function IsAccessAllow (address sensor) public view returns(bool access) {
             address send = msg.sender;
              access =  sensorData[send].AccessList[sensor];
             return access;
        }
    }