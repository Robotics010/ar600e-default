#include "CSVLog_class.h"

CSVLog_class::CSVLog_class(const char* pathfile)
{
	f = fopen(pathfile, "a");
	logPC = "Windows";
}

bool CSVLog_class::Log(char* logmodule, char* logfunction, char* logmessage)
{
	now = time(0);
	mtime = clock() / (CLOCKS_PER_SEC / 1000);
	tstruct = *localtime(&now);
	strftime(datebuf, sizeof(datebuf), "%d.%m.%Y", &tstruct);
	strftime(timebuf, sizeof(timebuf), "%H:%M:%S", &tstruct);
	//strftime(mtimebuf, sizeof(mtimebuf), "%X", &tstruct);
	
	//char       timebuf[80];
	//char       mtimebuf[80];

	//fprintf(f, "%d;%s;%s;%s;%f\n", 11, logPC, logfunction, logmessage, 3.42);
	fprintf(f, "%s;%s;%d;%s;%s;%s;%s\n", datebuf, timebuf, mtime, logPC, logmodule, logfunction, logmessage);
	return true;
}

CSVLog_class::~CSVLog_class()
{
	fclose(f);
}
