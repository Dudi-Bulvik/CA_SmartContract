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
            if (test)
            {
                SimpleIoc.Default.Register<ICaService, CaServiceTest>();
                SimpleIoc.Default.Register<ManagerVM>();
            }
            
        }

        public ManagerVM ManagerVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ManagerVM>();
            }
        }
    }
}
