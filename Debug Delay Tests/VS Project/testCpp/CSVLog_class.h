#pragma once

#ifndef CSVLOG_CLASS_H
#define CSVLOG_CLASS_H

#include <stdio.h>

#include <time.h>

class CSVLog_class {
public:
	CSVLog_class(const char* pathfile);
	~CSVLog_class();
	bool Log(char* logmodule, char* logfunction, char* logmessage);
private:
	FILE *f;
	const char* logPC;
	time_t     now;
	struct tm  tstruct;
	char       datebuf[80];
	char       timebuf[80];
	clock_t		mtime;
};
#endif //CSVLOG_CLASS_H