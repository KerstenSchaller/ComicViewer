#include <stdio.h>
#include <curl/curl.h>
#include <iostream>
#include <string>
#include <fstream>
#include <streambuf>

namespace html_operations{

  void downloadHtmlToFile(const char* url);

  size_t write_callback(char *ptr, size_t size, size_t nmemb, void *userdata);

  std::string readFromFile(const char* filename); 
  
  std::string downloadHtmlToString(const char* url);

  bool removeFile(const char* filename);
}