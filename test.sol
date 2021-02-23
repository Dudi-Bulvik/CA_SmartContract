// SPDX-License-Identifier: GPL-3.0
pragma solidity ^0.7.4;

  contract CASmartContract {

        uint _multiplier;
        struct Owner{
            address SennsorOwner;
            bool HasOwner;
            string SensorName;
        }
        struct AccessData
        {
            string SensorName;
            address key;
            bool accessPermition;
        }
        struct SensorData{
            string SensorName;
            address key;
            mapping(address=>AccessData) AccessList;
        }
        mapping(address => SensorData) private sesorAccessDat;
        mapping(address => Owner) private sensorOwner;

        constructor (){
        }
        
    function initSesor(address sensor,  string memory sensorName) public returns (bool success) {
        if(!sensorOwner[sensor].HasOwner)
        {
            sensorOwner[sensor].HasOwner =true;
            sensorOwner[sensor].SennsorOwner = msg.sender;
            sensorOwner[sensor].SensorName  = sensorName;
            return success =true;
        }
        return false;
    }
    function GrentAccess(address sensor,bool access) public returns(bool success)
    {
         if(sensorOwner[sensor].HasOwner && sensorOwner[sensor].SennsorOwner = msg.sender)
         {
             
         }
        return false;
    }
    function IsAccessAllow (address sensor) public view returns(bool access) {
             address send = msg.sender;
              access =  sesorAccessDat[send].AccessList[sensor].accessPermition;
             return access;
        }
    }