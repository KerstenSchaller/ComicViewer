using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Net.Http;
using System.Net;
using System.IO;

using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Drawing;

namespace DailyDilbertViewer
{
    class DilbertReceiver : WebClient
    {
        string httpContent ="";
        string ImageUrl = "";
        public ImageFileHandler filehandler;
        System.Windows.Forms.PictureBox Picturebox;

        public DilbertReceiver()
        {
        }

        public DilbertReceiver(System.Windows.Forms.PictureBox picturebox, System.Windows.Forms.ListBox listbox_dates, System.Windows.Forms.ListBox listbox_tags)
        {
            Picturebox = picturebox;
            filehandler = new ImageFileHandler(listbox_dates,listbox_tags);
        }


        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public Image getDilbertComicImageByDate(DateTime date)
        {
            Image image;
            var d = date.Date;
            if (date.Date.CompareTo(DateTime.Now.Date) > 0)
            {
                image = (Image)new Bitmap(DailyDilbertViewer.Properties.Resources.ErrorResponse);
            }
            else
            { 
                /*Check if image exists already and load it if true*/
                image = filehandler.LoadImage(date);
                if (image == null)
                {
                    string url = getDilbertComícUrlByDate(date);
                    image = GetImageFromURL(url);
                    filehandler.SaveImage(date, image);
                }
            }
            setPictureBox(image);

            return image;

        }

        public bool setPictureBox(Image image)
        {
            if (Picturebox != null)
            {
                Picturebox.Image = image;
                Size comicsize = new Size(image.Width, image.Height);
                Picturebox.Size = comicsize;
                Picturebox.Update();
                return true;
            }
            return false;
        }

        public string getDilbertComícUrlByDate(DateTime date)
        {
            string adress = "https://dilbert.com/strip/";
            adress += date.Year;
            adress += "-";
            adress += (date.Month < 10) ? "0" : "";// prepend 0 if needed
            adress += date.Month;
            adress += "-";
            adress += (date.Day < 10) ? "0" : "";// prepend 0 if needed
            adress += date.Day;
            this.getHtml(adress);
           // var t = getTags();
            return  this.getImageUrl();
        }

        public string getHtml(string address)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //Use SecurityProtocolType.Ssl3 if needed for compatibility reasons

            //Use callback for https connection certificate acception
            ServicePointManager.ServerCertificateValidationCallback +=
                delegate (object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors sslError) 
                {
                    bool validationResult = true;
                    return validationResult;
                };


            Uri uri = new Uri(address);
            WebRequest request = GetWebRequest(uri);
            WebResponse response = request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
            string result = sr.ReadToEnd();
            httpContent = result;
            return result;
        }



        private Image GetImageFromURL(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream stream = httpWebReponse.GetResponseStream();
            return Image.FromStream(stream);
        }

        public string getImageUrl()
        {
            string UrlStartstring = "http://assets.amuniversal";

            int startIndex= httpContent.IndexOf(UrlStartstring);
            int endindex   = httpContent.IndexOf("\"/>\n", startIndex);

            int startindexUrl = startIndex;
            int endindexUrl = endindex;
            int length = endindexUrl - startIndex;
            string imageUrl = httpContent.Substring(startindexUrl, length);
            return imageUrl;
        }


        public string[] getTags()
        {
            string TagStartstring = "<meta property=\"article: tag\" content=\"";

            int startIndex = httpContent.IndexOf(TagStartstring);
            int endindex = httpContent.IndexOf("\"/>", startIndex);

            int startindexTags = startIndex;
            int endindexTags = endindex;
            int length = endindexTags - startIndex;
            string tags = httpContent.Substring(startindexTags, length);
            return tags.Split(',');
        }


        protected override WebRequest GetWebRequest(Uri uri)
        {
            HttpWebRequest request = base.GetWebRequest(uri) as HttpWebRequest;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            return request;
        }


    }
}
       