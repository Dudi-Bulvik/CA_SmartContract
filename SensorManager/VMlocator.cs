using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace SensorManager
{
    public class VMlocator
    {
        private bool test;
         public VMlocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            test = true;
            Load();
        }
        public async void Load()
        {
            if (test)
            {
              //  SimpleIoc.Default.Register<ICaService, CaServiceTest>();
                //SimpleIoc.Default.Register<ManagerVM>();
            }
            SimpleIoc.Default.Register<ILogger, LogViewerVm>();
            SimpleIoc.Default.Register<ISettings, Settings>();
            // SimpleIoc.Default.Register<ICaService, CAService>();
            // SimpleIoc.Default.Register< LogViewerVm>();
            
            
            SimpleIoc.Default.Register<ICaService,CAService>();
            await ((CAService)ServiceLocator.Current.GetInstance<ICaService>()).InitializeAsync();
            SimpleIoc.Default.Register<ManagerVM>();
            SimpleIoc.Default.Register<GrantAccessVM>();
            SimpleIoc.Default.Register<InitSensorVM>();
            SimpleIoc.Default.Register<MainWindowVM>();
            
            
            //var ca = new CAService(@"C:\Myproject\configSensorsFile.txt");


        }

        public ManagerVM ManagerVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ManagerVM>();
            }
        }
        public GrantAccessVM GrantAccessVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GrantAccessVM>();
            }
        }
        public InitSensorVM InitSensorVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<InitSensorVM>();
            }
        }
        public MainWindowVM MainWindowVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainWindowVM>();
            }
        }
        public LogViewerVm LogViewerVm
        {
            get
            {
                return (LogViewerVm)ServiceLocator.Current.GetInstance<ILogger>();
            }
        }
        public ISettings ISettings
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ISettings>();
            }
        }

    }
}
