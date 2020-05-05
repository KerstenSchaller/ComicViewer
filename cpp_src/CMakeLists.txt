cmake_minimum_required(VERSION 3.13)

find_package(CURL REQUIRED) 
find_package(LibXml2 REQUIRED) 

add_executable(downloadDilbert
	main.cpp
	html_operations.cpp
	)

target_include_directories(downloadDilbert
	PUBLIC ${LIBXML2_INCLUDE_DIR}
	)

target_link_libraries(downloadDilbert
	curl
	${LIBXML2_LIBRARIES}
	)