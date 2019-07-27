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
        string directoryName = "Dilbert_Comics";

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
                fileDictionary.Add(date.Date, path);
                this.Listbox_days.DataSource = this.getAllFiles();

            }

        }

        private string getFilepath(DateTime date)
        {
            
            Directory.CreateDirectory(this.directoryName);
            string filename = "Dilbert_" + date.Date.ToShortDateString().Replace('.', '_');
            string path = Path.Combine(Directory.GetCurrentDirectory(), directoryName, filename + ".jpeg");
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
        
        public void parseAllFiles()
        {

            fileDictionary = new Dictionary<DateTime, string>();
            string path = Path.Combine(Directory.GetCurrentDirectory(),this.directoryName);
            if (!Directory.Exists(path)) return;
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                string filename = Path.GetFileNameWithoutExtension(file);
                string ext = Path.GetExtension(file);
                if (ext == ".jpeg")
                {
                    string[] dateArray = filename.Split('_');
                    DateTime date = new DateTime(Int32.Parse(dateArray[3]), Int32.Parse(dateArray[2]), Int32.Parse(dateArray[1]));
                    
                    fileDictionary.Add(date, file);
                }
            }
          
            if (Listbox_days != null)
            {
                Listbox_days.DataSource = this.getAllFiles();
                Listbox_days.Update();
            }
            
            
        }

    }
}
