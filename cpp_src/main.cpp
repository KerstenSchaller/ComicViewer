#include <stdio.h>
#include <curl/curl.h>
#include <iostream>
 
 
size_t write_callback(char *ptr, size_t size, size_t nmemb, void *userdata)
{
	if(std::FILE* f1 = std::fopen("html.txt", "wa"))
	{
		fwrite(ptr,size,nmemb,f1);
		std::fclose(f1);
    }
	std::cout << "callback" << std::endl;
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

/*
std::string readFromFile(const char* filename) 
{
   std::ifstream f(filename); //taking file as inputstream
   std::string str;
   if(f) {
      std::ostringstream ss;
      ss << f.rdbuf(); // reading data
      str = ss.str();
   }
   std::cout<<str;
}
*/
 
int main(void)
{
  downloadHtmlToFile("https://example.com");
  //readFromFile("https://example.com");
  return 0;
}