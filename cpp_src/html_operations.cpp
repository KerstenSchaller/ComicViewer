#include "html_operations.hpp"

namespace html_operations
{
	std::string filename = "temp.html";
	
	std::string downloadHtmlToString(const char* url)
	{
		downloadHtmlToFile(url);
		std::string html = readFromFile(filename.c_str());
		bool fileRemovedSuccessful = removeFile(filename.c_str());
		if((html != "") && (fileRemovedSuccessful == true))
		{
			std::cout << "html downloaded" << std::endl;
			return html;
		}
		else
		{
			std::cerr << "Error" << std::endl;
			std::cerr << "fileremoval status: "<< fileRemovedSuccessful << std::endl;
			return "";
		}
	}

    size_t write_callback(char *ptr, size_t size, size_t nmemb, void *userdata)
    {
    	if(std::FILE* f1 = std::fopen(filename.c_str(), "wa"))
    	{
    		fwrite(ptr,size,nmemb,f1);
    		std::fclose(f1);
        }
    	return size*nmemb;
    }
     
     
    void downloadHtmlToFile(const char* url)
    {
      CURL *curl;
      CURLcode res;
     
      curl_global_init(CURL_GLOBAL_DEFAULT);
     
      curl = curl_easy_init();
      if(curl) {
        curl_easy_setopt(curl, CURLOPT_URL, url);
    	curl_easy_setopt(curl, CURLOPT_WRITEFUNCTION, write_callback);
     
        #ifdef SKIP_PEER_VERIFICATION
        curl_easy_setopt(curl, CURLOPT_SSL_VERIFYPEER, 0L);
        #endif
     
        #ifdef SKIP_HOSTNAME_VERIFICATION
        curl_easy_setopt(curl, CURLOPT_SSL_VERIFYHOST, 0L);
        #endif
     
        /* Perform the request, res will get the return code */ 
        res = curl_easy_perform(curl);
        /* Check for errors */ 
        if(res != CURLE_OK)
          fprintf(stderr, "curl_easy_perform() failed: %s\n",
                  curl_easy_strerror(res));
     
        /* always cleanup */ 
        curl_easy_cleanup(curl);
      }
     
      curl_global_cleanup();
     
      
    }
    
    
    std::string readFromFile(const char* filename) 
    {
       std::ifstream ifs(filename);
       std::string str;
       
       //t.seekg(0, std::ios::end);   
       //str.reserve(t.tellg());
       //t.seekg(0, std::ios::beg);
       
       //str.assign((std::istreambuf_iterator<char>(t)),std::istreambuf_iterator<char>());
	   std::string content( (std::istreambuf_iterator<char>(ifs) ),
                       (std::istreambuf_iterator<char>()    ) );
	   return content;
    }
	
	bool removeFile(const char* filename)
	{
	  /*	Deletes the file if exists */
	  bool retval = false;
	  if(remove(filename) == 0)
	  {
		retval = true;
	  }
	  return retval;
	}
    

}