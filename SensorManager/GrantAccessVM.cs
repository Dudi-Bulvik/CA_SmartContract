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
        private readonly ILogger logger;
        public GrantAccessVM(ICaService caService, ILogger logger)
        {
            this.caService = caService;
            this.logger = logger;
            SensorNames = caService.SensorNames;
            sensorOwners = caService.SensorOwners;
            caService.AddNewSensor += AddNewSensor;
            GrantAccessCommand = new RelayCommand(GrantAccess, CanExecuteGrantAccess);
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
             Task.Run( async ()=> IsAaccessAllow = await caService.ChangeAccessRight(sensorOwner, fromSensor, toSensor, access));
        }
        public RelayCommand GrantAccessCommand
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
            set { Set(() => SensorOwner, ref sensorOwner, value); }
        }
        public string FromSensor
        {
            get { return fromSensor; }
            set { Set(() => FromSensor, ref fromSensor, value); }

        }
        public string ToSensor
        {
            get { return toSensor; }
            set { Set(() => ToSensor, ref toSensor, value); }

        }
        public bool Access
        {
            get { return access; }
            set { Set(() => Access, ref access, value); }

        }
        public bool IsAaccessAllow
        {
            get { return isAaccessAllow; }
            set { Set(() => IsAaccessAllow, ref isAaccessAllow, value); }

        }
    }
}
