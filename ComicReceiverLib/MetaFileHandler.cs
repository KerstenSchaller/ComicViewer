using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DailyDilbertViewer
{
    public class MetaFileHandler
    {
        private Values values;
        private string metaFilePath = "metafile.xml";

        public MetaFileHandler()
        {
            values = this.getValues();
        }

        private Values getValues()
        {
            string path = metaFilePath;
            if (File.Exists(path))
            {
                values = Serialization.ReadFromXmlFile<Values>(path);
            }
            else
            {
                values = new Values();
            }
            return values;
        }

        public void addTagsForDate(DateTime date, List<string> tags)
        {
            values.dates.Add(date);
            values.tags_list.Add( tags);
            this.saveMetaFile(metaFilePath);
        }

        private void saveMetaFile(string path)
        {
            Serialization.WriteToXmlFile<Values>(path, values);
        }
        
        public List<string> getTagsForDate(DateTime date)
        {
            var dates = this.values.dates;
            var tags_l = this.values.tags_list;
            int index = dates.IndexOf(date);
            return tags_l[index];
        }

        public List<string> getUniqueTags()
        {
            List<string> templist = new List<string>();
            foreach (var l in this.values.tags_list)
            {
                templist.AddRange(l);
            }
            var unique_list = new HashSet<string>(templist);
            return unique_list.ToList();
        }

        public class Values
        {
            //public Dictionary<DateTime, List<string>> DateTags = new Dictionary<DateTime, List<string>>();
            public List<DateTime> dates = new List<DateTime>();
            public List<List<string>> tags_list = new List<List<string>>();
            
            //private Dictionary<DateTime, List<string>> datesAndTags = new Dictionary<DateTime, List<string>>();
            public Dictionary<DateTime, List<string>> datesAndTags()
            {  
               Dictionary<DateTime, List<string>> dict = new Dictionary<DateTime, List<string>>();
               for (int i = 0; i < dates.Count; i++)
               {
                    dict.Add(dates[i],tags_list[i]);
               }
               return dict;
            }
            
        }

    }
}
