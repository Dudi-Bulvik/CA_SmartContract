﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace SensorManager
{
    public class AccessRight
    {
        public string SensorName { get; set; }
        public bool AccessePermition { get; set; }
    }
    public class SensorModel
    {
        public string SensorName { get; set; }
        public string SensorPublicKey { get; set; }
        public List<AccessRight> AccsesRights { get; set; } = new List<AccessRight>();
        public string SensorPrivateKey { get; set; }
        public SensorModel Clone()
        {
            var clone = new SensorModel();
            clone.SensorName =(string) SensorName.Clone();
            clone.SensorPublicKey = (string)SensorPublicKey.Clone();
            clone.AccsesRights = new List<AccessRight>();
            foreach(var access in AccsesRights)
            {
                clone.AccsesRights.Add(new AccessRight { AccessePermition = access.AccessePermition, SensorName = (string)access.SensorName.Clone() });
            }
            return clone;
        }
    }
    public class SensorViewModel : ViewModelBase
    {
        private readonly ICaService service;
        private  SensorModel sensorData;
        private ILogger logger;
        public SensorViewModel(ICaService service, SensorModel sensorData , ILogger logger)
        {
            this.logger = logger;
            ChangePermitionCommand = new RelayCommand<object>(ChangSensor);
            this.service = service;
            this.sensorData = sensorData;
        }
        private void ChangSensor(object accessRightobject = null)
        {
            var accessRight = (AccessRight)(accessRightobject);
            this.service.ChangeAccessRight(SensorName, SensorName, accessRight.SensorName, !accessRight.AccessePermition);
        }
        public string SensorName { get { return sensorData.SensorName; } }
        public string SensorPublicKey { get { return sensorData.SensorPublicKey; } }
        public List<AccessRight> AccsesRights { get { return sensorData.AccsesRights; } }
        public void UpdateVM(SensorModel newSensorData)
        {
            this.sensorData = newSensorData;
            logger.AddLogEntey("SensorViewModel New Sesnor Data Sensor Name: " + newSensorData.SensorName);
            RaisePropertyChanged(string.Empty);
        }
        
        public ICommand ChangePermitionCommand { get; set; }
    }
}