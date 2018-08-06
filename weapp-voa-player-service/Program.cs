using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace weapp_voa_player_service
{
    class Program
    {
       

        static void Main(string[] args)
        {
            Common.log.Info("Service start...");
            try
            {
                Common.log.Info("Init data model");
                Models.Data dataModel = new Models.Data();
                dataModel.updated = DateTime.Now;
                dataModel.getItemsList(dataModel.getHTML());
                  
                Common.log.Info("Serialize data model");
                string dataString = JsonConvert.SerializeObject(dataModel);
                Common.log.Info("Serialized data string ready");
                Common.log.Debug(dataString);

                string savePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, Common.SaveFile);
                Common.log.Info(String.Format("Save data as file: {0}", savePath));
                File.WriteAllText(savePath, dataString);
                Common.log.Info("Data file ready");
            }
            catch(Exception ex)
            {
                Common.log.Error("Error occurs.", ex);
            }
        }
    }
}
