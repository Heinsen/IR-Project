using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheApplication.Model;
using System.IO;

namespace TheApplication.Controller
{
    class ConfigManipulator
    {
        public void LoadConfig()
        {

           // Config Config = new Config();
            //string ConfigContent = FileManipulator.ReadFile(Config.ConfigPath, "Config.txt");

            //try
            //{
            //    Config.SourceCollectionPath = ConfigContent.Substring(0, ConfigContent.IndexOf(Environment.NewLine));
            //    Config.IndexPath = ConfigContent.Substring(1, ConfigContent.IndexOf(Environment.NewLine));
            //}
            //catch(Exception e)
            //{
            //}
            
            //return Config;
        }

        public void SaveConfig(string Config)
        {
            //FileManipulator.SaveFile(Config.ConfigPath, "Config.txt", Config.SourceCollectionPath + " / n" + Config.IndexPath, false);
        }
    }
}
