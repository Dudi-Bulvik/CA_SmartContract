// SPDX-License-Identifier: GPL-3.0

pragma solidity ^0.8.1;
//pragma solidity >0.5.8 <0.6.0;

contract CASmartContract {
        struct SensorData{
             address SennsorOwner;
            bool HasOwner;
            string SensorName;
            address Key;
            mapping(address=>bool) AccessList;
        }
        uint count;
        mapping(address => SensorData) private sensorData;
        address[] private sensorDataArray;

     event initSensor(address sensorOwner ,address sensor, string name );
    function InitSensor(address  sensor,  string memory sensorName) public returns (bool success) {
        if(!sensorData[sensor].HasOwner)
        {                    
            sensorData[sensor].HasOwner =true;
            sensorData[sensor].SennsorOwner = msg.sender;
            sensorData[sensor].SensorName  = sensorName;
            sensorData[sensor].Key =sensor;
            
            sensorDataArray.push(sensor);
            emit initSensor( msg.sender,sensor,sensorData[sensor].SensorName);                         
            return success =true;
        }
       emit initSensor(sensorData[sensor].SennsorOwner,sensor,"HasOwner");
        return false;
    }
    
    
      event grentAccess(address sensor ,address toSensor, string status, bool access );
      event grentAccess1(string errorMessage );
    function GrentAccess(address sensor,address toSensor,bool access) public returns(bool success){

         if(!sensorData[sensor].HasOwner)
         {
            emit grentAccess1("No owner");
             return false;
         }
         if(sensorData[sensor].SennsorOwner != msg.sender)
         {
             emit grentAccess1("msg.sender is not owner");
              return false;
         }
         
             sensorData[sensor].AccessList[toSensor] =access;
             emit grentAccess( sensor,toSensor,"success",sensorData[sensor].AccessList[toSensor]);
              emit grentAccess1("success");
             return true;
        
         
    }
    function IsAccessAllow (address from,address to) public view returns(bool access) {            
              access =  sensorData[from].AccessList[to];
             return access;
        }

    
    function GetSensorCount () public  view returns(uint numberOfSensors) {   
                                        
             return sensorDataArray.length;
        }
        function GetSensorname (uint sensorNumber) public view returns( string memory name,address  key) 
        {                         

            return (sensorData[sensorDataArray[sensorNumber-1]].SensorName,sensorData[sensorDataArray[sensorNumber-1]].Key);
        }

    }