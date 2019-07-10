using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace DailyDilbertViewer
{
    class ImageFileHandler
    {
        List<string> Filenames = new List<string>();
        Dictionary<DateTime, string> fileDictionary = new Dictionary<DateTime, string>();
        System.Windows.Forms.ListBox Listbox_days = null;
        System.Windows.Forms.ListBox Listbox_tags = null;
        

        public ImageFileHandler()
        {
            this.parseAllFiles();
        }

        public ImageFileHandler(System.Windows.Forms.ListBox listbox_days, System.Windows.Forms.ListBox listbox_tags)
        {
            Listbox_days = listbox_days;
            this.parseAllFiles();
        }
        

        public void SaveImage(DateTime date, Image image)
        {
            if (fileDictionary.ContainsKey(date.Date) == false)
            {
                string path = getFilepath(date);
                image.Save(path);
                fileDictionary.Add(date, path);
                this.parseAllFiles();

            }

        }

        private string getFilepath(DateTime date)
        {
            string filename = "Dilbert_" + date.Date.ToShortDateString().Replace('.', '_');
            string path = Path.Combine(Directory.GetCurrentDirectory(), filename + ".jpeg");
            return path;
        }

        public Image LoadImage(DateTime date)
        {
            
            if(fileDictionary.ContainsKey(date))
            {
                Image image = Image.FromFile(fileDictionary[date]);
                return image;
            }
            else
            {
                return null;
            }

        }
        
        public List<DateTime> getAllFiles()
        {
            var retlist = fileDictionary.Keys.ToList();
            retlist.Sort();
            return retlist;
        }

        public List<DateTime> parseAllFiles()
        {
            fileDictionary = new Dictionary<DateTime, string>();
            string path = Directory.GetCurrentDirectory();
            string[] files = Directory.GetFiles(path);
            List<DateTime> retlist = new List<DateTime>();
            foreach (string file in files)
            {
                string ext = Path.GetExtension(file);
                if (ext == ".jpeg")
                {
                    string[] dateArray = file.Split('_');
                    dateArray[3] = dateArray[3].Substring(0, 4);
                    DateTime date = new DateTime(Int32.Parse(dateArray[3]), Int32.Parse(dateArray[2]), Int32.Parse(dateArray[1]));
                    retlist.Add(date);
                    fileDictionary.Add(date, file);
                }
            }
            retlist.Sort();
            if (Listbox_days != null)
            {
                Listbox_days.DataSource = retlist;
                Listbox_days.Update();
            }
            
            return retlist;
        }

    }
}
