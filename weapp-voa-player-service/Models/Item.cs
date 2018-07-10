using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace weapp_voa_player_service.Models
{
    public class Item
    {
        public string title { get; set; }
        public string date { get; set; }
        public string content { get; set; }
        public string audio { get; set; }

        public String getHTML(String URL)
        {
            Common.log.Info(String.Format("Get item html from URL {0}", URL));
            using (WebClient client = new WebClient())
            {
                string itemContent = client.DownloadString(URL);
                Common.log.Info("Item html get ready");
                Common.log.Debug(itemContent);
                return itemContent;
            }
        }

        public String getDate(String content)
        {
            String result = "";
            Common.log.Info(String.Format("Parse item date from item html using regex {0}, the group 1 means item DATE", Common.DateRegex));
            Regex regex = new Regex(Common.DateRegex, RegexOptions.Singleline);
            MatchCollection matches = regex.Matches(content);
            Common.log.Info(String.Format("Item html match count: {0}", matches.Count));
            if (matches.Count > 0)
            {
                result = matches[0].Groups[1].ToString();
                Common.log.Info(String.Format("Match group 1 result: {0}", result));
            }
            return result;
        }

        public String getAudio(String content)
        {
            String result = "";
            Common.log.Info(String.Format("Parse item Audio from item html using regex {0}, the group 1 means item AUDIO", Common.AudioRegex));
            Regex regex = new Regex(Common.AudioRegex, RegexOptions.Singleline);
            MatchCollection matches = regex.Matches(content);
            Common.log.Info(String.Format("Item html match count: {0}", matches.Count));
            if (matches.Count > 0)
            {
                result = matches[0].Groups[1].ToString();
                Common.log.Info(String.Format("Match group 1 result: {0}", result));
            }
            return result;
        }

        public String getContent(String content)
        {
            String result = "";
            Common.log.Info(String.Format("Parse item Content from item html using regex {0}, the group 1 means item CONTENT", Common.ContentRegex));
            Regex regex = new Regex(Common.ContentRegex, RegexOptions.Singleline);
            MatchCollection matches = regex.Matches(content);
            Common.log.Info(String.Format("Item html match count: {0}", matches.Count));
            if (matches.Count > 0)
            {
                result = matches[0].Groups[1].ToString();
                Common.log.Info(String.Format("Match group 1 result: {0}", result));
            }
            return result;
        }
    }
}
