#include <string>
#include <iostream>

#include "html_operations.hpp"


 
int main(void)
{
  //std::string html = html_operations::downloadHtmlToString("https://dilbert.com/");
  std::string html = html_operations::downloadHtmlToString("https://dilbert.com/strip/2020-05-05");
  std::cout << html << std::endl;
  return 0;
}