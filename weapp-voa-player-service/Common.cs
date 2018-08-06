using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace weapp_voa_player_service
{
    class Common
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static readonly String SaveFile = ConfigurationManager.AppSettings["SaveFile"];
        public static readonly String Server = ConfigurationManager.AppSettings["Server"];
        public static readonly String ListURL = ConfigurationManager.AppSettings["ListURL"];
        public static readonly Int16 ItemCount = Int16.Parse(ConfigurationManager.AppSettings["ItemCount"]);
        public static readonly String ListRegex = ConfigurationManager.AppSettings["ListRegex"];
        public static readonly String DateRegex = ConfigurationManager.AppSettings["DateRegex"];
        public static readonly String AudioRegex = ConfigurationManager.AppSettings["AudioRegex"];
        public static readonly String ContentRegex = ConfigurationManager.AppSettings["ContentRegex"];
        public static readonly String ContentBrRegex = ConfigurationManager.AppSettings["ContentBrRegex"];
        public static readonly String ContentClearRegex = ConfigurationManager.AppSettings["ContentClearRegex"];

        public static String clearHTML(String input)
        {
            Common.log.Info(String.Format("Replace BR with line break, all matchs will be repaced with CRLF using regex: {0}", Common.ContentBrRegex));
            Regex regexBr = new Regex(ContentBrRegex, RegexOptions.Singleline);
            input = regexBr.Replace(input,"\r\n");

            Common.log.Info(String.Format("Replace HTML tag with blank string, all matchs will be repaced with \"\" using regex: {0}", Common.ContentClearRegex));
            Regex regexClear = new Regex(ContentClearRegex, RegexOptions.Singleline);
            input = regexClear.Replace(input, "");
            Common.log.Info(String.Format("Clear result: {0}", input));
            return input;
        }
    }
}
