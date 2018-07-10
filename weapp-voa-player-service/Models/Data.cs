using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace weapp_voa_player_service.Models
{
    public class Data
    {
        public DateTime updated { get; set; }
        public List<Item> items { get; set; }

        public String getHTML()
        {
            Common.log.Info(String.Format("Get list html from URL {0}", Common.ListURL));
            using (WebClient client = new WebClient())
            {
                string listContent = client.DownloadString(Common.ListURL);
                Common.log.Info("List html get ready");
                Common.log.Debug(listContent);
                return listContent;
            }    
        }

        public void getItemsList(String Content)
        {
            this.items = new List<Item>();

            Common.log.Info(String.Format("Parse items from list html using regex {0}, the group 1 means item TITLE and group 2 means item URL", Common.ListRegex));
            Regex listRegex = new Regex(Common.ListRegex, RegexOptions.Singleline);
            MatchCollection listMatches = listRegex.Matches(Content);
            Common.log.Info(String.Format("List html match count: {0}", listMatches.Count));
            Common.log.Info(String.Format("Config max item count: {0}", Common.ItemCount));
            if (listMatches.Count > 0)
            {
                Int16 index = 0;
                foreach (Match match in listMatches)
                {
                    index++;
                    if(index < (Common.ItemCount + 1))
                    {
                        Models.Item itemModel = new Models.Item();

                        itemModel.title = match.Groups[2].ToString();
                        String itemURL = Common.Server + match.Groups[1].ToString();
                        Common.log.Info(String.Format("#Item title: {0}", itemModel.title));
                        Common.log.Info(String.Format("#Item url: {0}", itemURL));

                        String itemHTML = itemModel.getHTML(itemURL);

                        itemModel.date = itemModel.getDate(itemHTML);
                        itemModel.content = Common.clearHTML(itemModel.getContent(itemHTML));
                        itemModel.audio = itemModel.getAudio(itemHTML);

                        Common.log.Info(String.Format("Add item to list"));
                        this.items.Add(itemModel);
                    }
                }
            }
        }
    }
}
