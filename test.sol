pragma solidity ^0.7.4;

  contract test {

        uint _multiplier;

        constructor (uint multiplier){
             _multiplier = multiplier;
        }

         function multiply (uint a) public view returns(uint d) {
             return a * _multiplier;
        }
    }