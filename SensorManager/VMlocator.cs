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
        public  void Load()
        {
            //if (test)
            //{
            //    SimpleIoc.Default.Register<ICaService, CaServiceTest>();
            //    SimpleIoc.Default.Register<ManagerVM>();
            //}
            SimpleIoc.Default.Register<ICaService>(()=> new CAService(@"C:\Myproject\configSensorsFile.txt"));
            SimpleIoc.Default.Register<ManagerVM>();
            SimpleIoc.Default.Register<SesnorCreatorVM>();
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
        public SesnorCreatorVM SesnorCreatorVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SesnorCreatorVM>();
            }
        }
        public MainWindowVM MainWindowVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainWindowVM>();
            }
        }
    }
}
