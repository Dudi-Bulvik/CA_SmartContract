using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorManager
{
   public interface ISettings 
    {
        string ConfigurationFilePath { get; }
    }
    public class Settings : ISettings   
    {
        public Settings()
         {
            ConfigurationFilePath = SesnorManagerSettings.Default.Configfile;
         }
       public  string ConfigurationFilePath { get; private set; }
    }
}
