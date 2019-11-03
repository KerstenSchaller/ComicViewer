using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Drawing;

namespace DailyDilbertViewer
{
    public class DilbertReceiver : WebClient
    {
        string httpContent ="";
        string ImageUrl = "";
        ImageFileHandler filehandler;
        MetaFileHandler metaFileHandler;
        List<string> all_tags = new List<string>();

        //public DilbertReceiver()
        //{
        //}

        public DilbertReceiver()
        {
            filehandler = new ImageFileHandler();
            //metaFileHandler = new MetaFileHandler();
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
                image = (Image)new Bitmap("error.jpg");
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

            return image;

        }

       
        private string getDilbertComícUrlByDate(DateTime date)
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
            //var t = getTags();
            //metaFileHandler.addTagsForDate(date,t);
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


        public List<string> getTags()
        {

            //data-tags=\"comic strip,inventions,sarcasm,technology,creativity\" data-descri
            string TagStartstring = "data-tags";
            string TagEndstring = @"""";
            int startIndex = httpContent.IndexOf(TagStartstring);


            int startindexTags = startIndex + TagStartstring.Length + 2;
            int endindex = httpContent.IndexOf(TagEndstring, startindexTags);
            int endindexTags = endindex;
            int length = endindexTags - startindexTags;
            string tags = httpContent.Substring(startindexTags, length);
            all_tags = tags.Split(',').ToList();
            return all_tags;
        }


        protected override WebRequest GetWebRequest(Uri uri)
        {
            HttpWebRequest request = base.GetWebRequest(uri) as HttpWebRequest;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            return request;
        }


    }
}
       