#include "CSVLog_class.h"

CSVLog_class::CSVLog_class(const char* pathfile)
{
	f = fopen(pathfile, "a");
	logPC = "Windows";
}

bool CSVLog_class::Log(char* logmodule, char* logfunction, char* logmessage, double logvalue)
{
	now = time(0);
	GetSystemTime(&windows_time);
	
	//mtime = clock() / (CLOCKS_PER_SEC / 1000);
	tstruct = *localtime(&now);
	time_ms = windows_time.wMilliseconds;

	strftime(datebuf, sizeof(datebuf), "%d.%m.%Y", &tstruct);
	strftime(timebuf, sizeof(timebuf), "%H:%M:%S", &tstruct);
	//strftime(mtimebuf, sizeof(mtimebuf), "%X", &tstruct);
	
	//char       timebuf[80];
	//char       mtimebuf[80];

	//fprintf(f, "%d;%s;%s;%s;%f\n", 11, logPC, logfunction, logmessage, 3.42);
	sprintf(csvlog_buffer, logmessage, logvalue); //"L_shoulder_1 position=%.3f"
	fprintf(f, "%s;%s;%d;%s;%s;%s;%s\n", datebuf, timebuf, time_ms, logPC, logmodule, logfunction, csvlog_buffer);
	return true;
}

CSVLog_class::~CSVLog_class()
{
	fclose(f);
}
