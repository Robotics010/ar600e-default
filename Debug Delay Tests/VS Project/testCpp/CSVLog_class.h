#pragma once

#ifndef CSVLOG_CLASS_H
#define CSVLOG_CLASS_H

#include <stdio.h>

#include <windows.h>
#include <time.h>

class CSVLog_class {
public:
	CSVLog_class(const char* pathfile);
	~CSVLog_class();
	bool Log(char* logmodule, char* logfunction, char* logmessage, double logvalue);
private:
	FILE *f;
	const char* logPC;
	time_t     now;
	struct tm  tstruct;
	SYSTEMTIME windows_time;
	char       datebuf[80];
	char       timebuf[80];
	LONG	   time_ms;
	char csvlog_buffer[50];
};
#endif //CSVLOG_CLASS_H