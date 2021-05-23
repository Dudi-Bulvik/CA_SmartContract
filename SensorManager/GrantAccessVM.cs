using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.CommandWpf;

namespace SensorManager
{
    public class GrantAccessVM : ViewModelBase
    {
       
        private readonly ICaService caService;
        private List<string> sensorNames;
        private List<string> sensorOwners;
        private string sensorOwner;
        private string fromSensor;
        private string toSensor;
        private bool access;
        private bool isAaccessAllow;
        private string errorMessage;
        private bool checkAccess;
        private bool showStatus;
        private readonly ILogger logger;
        private decimal balanceAfter = 0;
        private decimal balanceBefore = 0;

        public GrantAccessVM(ICaService caService, ILogger logger)
        {
            this.caService = caService;
            this.logger = logger;
            SensorNames = caService.SensorNames;
            sensorOwners = caService.SensorOwners;
            caService.AddNewSensor += AddNewSensor;
            ShowStatus = false;
            GrantAccessCommand = new RelayCommand(GrantAccess, CanExecuteGrantAccess);
            CheckAccessCommand= new RelayCommand(CheckAccessFun, CanExecuteCheckAccess);
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                Set(() => ErrorMessage, ref errorMessage, value);
                RaisePropertyChanged("ShowToolTip");
            }
        }
        private bool CanExecuteCheckAccess()
        {
            if (string.IsNullOrEmpty(fromSensor))
            {
                ErrorMessage = "fromSensor can't be empty";
                return false;
            }
            if (string.IsNullOrEmpty(toSensor))
            {
                ErrorMessage = "toSensor can't be empty";
                return false;
            }
            ErrorMessage = "";
            return true;
        }
        private void CheckAccessFun()
        {
            IsAaccessAllow = false;
            ShowStatus = false;
            Task.Run(async () =>
            {
                BalanceBefore = await caService.GetAccountBalance(fromSensor);
                IsAaccessAllow = await caService.IsAccessAllow(fromSensor, toSensor);
                BalanceAfter = await caService.GetAccountBalance(fromSensor);
            });
        }
        private bool CanExecuteGrantAccess()
        {
            if(string.IsNullOrEmpty(sensorOwner))
            {
                ErrorMessage = "sensorOwner can't be empty";
                return false;
            }
            if (string.IsNullOrEmpty(fromSensor))
            {
                ErrorMessage = "fromSensor can't be empty";
                return false;
            }
            if (string.IsNullOrEmpty(toSensor))
            {
                ErrorMessage = "toSensor can't be empty";
                return false;
            }
            if(fromSensor.Equals(toSensor))
            {
                ErrorMessage = "toSensor can't be same as fromSensor";
                return false;
            }
            ErrorMessage = "";
            return true;
        }
        private void GrantAccess()
        {
            IsAaccessAllow = false;
            ShowStatus = false;
            Task.Run(async () =>
            {
                BalanceBefore = await caService.GetAccountBalance(sensorOwner);
                IsAaccessAllow = await caService.ChangeAccessRight(sensorOwner, fromSensor, toSensor, access);
                BalanceAfter =await caService.GetAccountBalance(sensorOwner);
            });
          
        }
        public RelayCommand GrantAccessCommand
        {
            get;
            private set;

        }
        public RelayCommand CheckAccessCommand
        {
            get;
            private set;

        }
        private void AddNewSensor(object sender, EventArgs e)
        {
            SensorNames = caService.SensorNames;
            SensorOwners = caService.SensorOwners;
        }
        public List<string> SensorNames
        {
            get { return sensorNames; }
            set { Set(() => SensorNames, ref sensorNames, value); }
        }
        public bool ShowToolTip { get { return !string.IsNullOrEmpty(errorMessage); } }
        public List<string> SensorOwners
        {
            get { return sensorOwners; }
            set { Set(() => SensorOwners, ref sensorOwners, value); }
        }

        public string SensorOwner
        {
            get { return sensorOwner; }
            set {
                ShowStatus = false;
                Set(() => SensorOwner, ref sensorOwner, value); }
        }
        public string FromSensor
        {
            get { return fromSensor; }
            set {
                ShowStatus = false;
                Set(() => FromSensor, ref fromSensor, value); }

        }
        public string ToSensor
        {
            get { return toSensor; }
            set {
                ShowStatus = false; 
                Set(() => ToSensor, ref toSensor, value); }

        }
        public bool Access
        {
            get { return access; }
            set {
                ShowStatus = false;
                Set(() => Access, ref access, value); }

        }


        public bool CheckAccess
        {
            get { return checkAccess; }
            set { 
                Set(() => CheckAccess, ref checkAccess, value);
                ShowStatus = false;
                RaisePropertyChanged("ChnageAccess");
                
            }

        }
        public decimal GasUsed
        {
            get { return balanceBefore - balanceAfter; }
        }

        public decimal BalanceAfter
        {
            get { return balanceAfter; }
            set
            {
                Set(() => BalanceAfter, ref balanceAfter, value);
                RaisePropertyChanged("GasUsed");

            }

        }
        public decimal BalanceBefore
        {
            get { return balanceBefore; }
            set
            {
                Set(() => BalanceBefore, ref balanceBefore, value);
             
            }
        }

        public bool ShowStatus
        {
            get { return showStatus; }
            set
            {
                Set(() => ShowStatus, ref showStatus, value);
                RaisePropertyChanged("ChnageAccess");
            }

        }
        public bool ChnageAccess
        {
            get { return !checkAccess; }
            

        }
        
        public bool IsAaccessAllow
        {
            get { return isAaccessAllow; }
            set { Set(() => IsAaccessAllow, ref isAaccessAllow, value);
                ShowStatus = true;
            }

        }
    }
}
