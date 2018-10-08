// testCpp.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <time.h>

#include <string>
#include <stdio.h>

#define WIN32_LEAN_AND_MEAN
#include <windows.h>

#include "TCPClient_class.h"
#include "CSVLog_class.h"

char *joint_types_chars[] =
{
	"L_shoulder_1",
	"L_shoulder_2",
	"L_elbow_1",
	"L_elbow_2",
	"L_forearm",
	"R_shoulder_1",
	"R_shoulder_2",
	"R_elbow_1",
	"R_elbow_2",
	"R_forearm",
	"disconnect"
};

int msg_count;

void print_message(char* output, MESSAGE* message)
{
	// usage printf("Unlatten Message: %s,%d\n", header, value);
	__int16 value = message->value;
	joint header = message->header;
	char* header_char = joint_types_chars[(int)header];

	printf(output, msg_count, header_char, value);
	msg_count++;
}

std::string ExePath() {
	char buffer[MAX_PATH];
	GetModuleFileNameA(NULL, buffer, MAX_PATH);
	std::string::size_type pos = std::string(buffer).find_last_of("\\/");
	return std::string(buffer).substr(0, pos);
}

int main()
{
//#include <stdio.h> 
//
//	struct MyData {
//		int someValue;
//		char const *someString;
//		float someSample;
//	};
//
//	int write_to_file(int count, struct MyData *data, char const *fileName)
//	{
//		FILE *f = fopen(fileName, "w");
//		if (f == NULL) return -1;
//		while (count-- > 0) {
//			// you might want to check for out-of-disk-space here, too 
//			fprintf(f, "%d,%s,%f\n", data->someValue, data->someString, data->someSample);
//			++data;
//		}
//		fclose(f);
//		return 0;
//	}
	
	std::string filepath = ExePath() + "\\cpp_log.csv";
	double logvalue;
	//printf(filepath.c_str());
	// LOGGER USAGE
	CSVLog_class* CsvLogger = new CSVLog_class(filepath.c_str());

	// TCP Client Init and Connect to WINDOWS PC
	msg_count = 0;
	clock_t start, finish;
	double  duration;
	MESSAGE* message = new MESSAGE;
	MESSAGE* message_forSent = new MESSAGE;

	TCPClient_class* TcpClient = new TCPClient_class();
	if (!TcpClient->Connect()) { printf("Can not connect to the TCP server at localhost"); }

	double positions[10];
	positions[0] = 0; positions[1] = 0;
	positions[2] = 0; positions[3] = 0;
	positions[4] = 0;
	positions[5] = 0; positions[6] = 0;
	positions[7] = 0; positions[8] = 0;
	positions[9] = 0;

	bool SendResult, ReceiveResult;
	ReceiveResult = false;
	// Start timer.  
	start = clock();

	int samples = 0;
	// Receive until the peer closes the connection
	do {
		// Receive Current Joint Position from a Robot

		ReceiveResult = TcpClient->Receive(message);

		if (ReceiveResult)
		{
			print_message("%d. Read Message from ROBOT: %s,%d\n", message);
			// Process Received Messages somehow
			switch (message->header)
			{
			case joint::L_shoulder_1:
				positions[0] = message->value;
				logvalue = message->value;
				CsvLogger->Log("testCpp.cpp", "main","L_shoulder_1 position = %.3f", logvalue);
				break;
			case joint::L_shoulder_2:
				positions[1] = message->value;
				logvalue = message->value;
				CsvLogger->Log("testCpp.cpp", "main", "L_shoulder_2 position = %.3f", logvalue);
				break;
			case joint::L_elbow_1:
				positions[2] = message->value;
				logvalue = message->value;
				CsvLogger->Log("testCpp.cpp", "main", "L_elbow_1 position = %.3f", logvalue);
				break;
			case joint::L_elbow_2:
				positions[3] = message->value;
				logvalue = message->value;
				CsvLogger->Log("testCpp.cpp", "main", "L_elbow_2 position = %.3f", logvalue);
				break;
			case joint::L_forearm:
				positions[4] = message->value;
				logvalue = message->value;
				CsvLogger->Log("testCpp.cpp", "main", "L_forearm position = %.3f", logvalue);
				break;
			case joint::R_shoulder_1:
				positions[5] = message->value;
				logvalue = message->value;
				CsvLogger->Log("testCpp.cpp", "main", "R_shoulder_1 position = %.3f", logvalue);
				break;
			case joint::R_shoulder_2:
				positions[6] = message->value;
				logvalue = message->value;
				CsvLogger->Log("testCpp.cpp", "main", "R_shoulder_2 position = %.3f", logvalue);
				break;
			case joint::R_elbow_1:
				positions[7] = message->value;
				logvalue = message->value;
				CsvLogger->Log("testCpp.cpp", "main", "R_elbow_1 position = %.3f", logvalue);
				break;
			case joint::R_elbow_2:
				positions[8] = message->value;
				logvalue = message->value;
				CsvLogger->Log("testCpp.cpp", "main", "R_elbow_2 position = %.3f", logvalue);
				break;
			case joint::R_forearm:
				positions[9] = message->value;
				logvalue = message->value;
				CsvLogger->Log("testCpp.cpp", "main", "R_forearm position = %.3f", logvalue);
				break;
			default:
				break;
			}

		}
		//else
		//{
		//	// Publish Current Joint Position to ROS
		//	jointstate_msg[0].position = &positions[0];
		//	jointstate_msg[1].position = &positions[1];
		//	jointstate_msg[2].position = &positions[2];
		//	jointstate_msg[3].position = &positions[3];
		//	jointstate_msg[4].position = &positions[4];
		//	jointstate_msg[5].position = &positions[5];
		//	jointstate_msg[6].position = &positions[6];
		//	jointstate_msg[7].position = &positions[7];
		//	jointstate_msg[8].position = &positions[8];
		//	jointstate_msg[9].position = &positions[9];
		//	jnt_st_pub_0.publish(&jointstate_msg[0]);
		//	jnt_st_pub_1.publish(&jointstate_msg[1]);
		//	jnt_st_pub_2.publish(&jointstate_msg[2]);
		//	jnt_st_pub_3.publish(&jointstate_msg[3]);
		//	jnt_st_pub_4.publish(&jointstate_msg[4]);
		//	jnt_st_pub_5.publish(&jointstate_msg[5]);
		//	jnt_st_pub_6.publish(&jointstate_msg[6]);
		//	jnt_st_pub_7.publish(&jointstate_msg[7]);
		//	jnt_st_pub_8.publish(&jointstate_msg[8]);
		//	jnt_st_pub_9.publish(&jointstate_msg[9]);
		//	nh.spinOnce();
		//}

		finish = clock();
		duration = (double)(finish - start) / CLOCKS_PER_SEC;
		// Send Joint Setpoint Position to a Robot
		if (duration > 1)
		{
			start = clock();
			for (int i = 0; i < 10; i++)
			{
				//Prepare message before sent
				switch (i)
				{
				case joint::L_shoulder_1:
					message_forSent->header = joint::L_shoulder_1;
					message_forSent->value = 101;
					logvalue = message_forSent->value;
					CsvLogger->Log("testCpp.cpp", "main", "L_shoulder_1 setpoint=%.3f", logvalue);
					break;
				case joint::L_shoulder_2:
					message_forSent->header = joint::L_shoulder_2;
					message_forSent->value = 102;
					logvalue = message_forSent->value;
					CsvLogger->Log("testCpp.cpp", "main", "L_shoulder_2 setpoint=%.3f", logvalue);
					break;
				case joint::L_elbow_1:
					message_forSent->header = joint::L_elbow_1;
					message_forSent->value = 103;
					logvalue = message_forSent->value;
					CsvLogger->Log("testCpp.cpp", "main", "L_elbow_1 setpoint=%.3f", logvalue);
					break;
				case joint::L_elbow_2:
					message_forSent->header = joint::L_elbow_2;
					message_forSent->value = 104;
					logvalue = message_forSent->value;
					CsvLogger->Log("testCpp.cpp", "main", "L_elbow_2 setpoint=%.3f", logvalue);
					break;
				case joint::L_forearm:
					message_forSent->header = joint::L_forearm;
					message_forSent->value = 105;
					logvalue = message_forSent->value;
					CsvLogger->Log("testCpp.cpp", "main", "L_forearm setpoint=%.3f", logvalue);
					break;
				case joint::R_shoulder_1:
					message_forSent->header = joint::R_shoulder_1;
					message_forSent->value = 106;
					logvalue = message_forSent->value;
					CsvLogger->Log("testCpp.cpp", "main", "R_shoulder_1 setpoint=%.3f", logvalue);
					break;
				case joint::R_shoulder_2:
					message_forSent->header = joint::R_shoulder_2;
					message_forSent->value = 107;
					logvalue = message_forSent->value;
					CsvLogger->Log("testCpp.cpp", "main", "R_shoulder_2 setpoint=%.3f", logvalue);
					break;
				case joint::R_elbow_1:
					message_forSent->header = joint::R_elbow_1;
					message_forSent->value = 108;
					logvalue = message_forSent->value;
					CsvLogger->Log("testCpp.cpp", "main", "R_elbow_1 setpoint=%.3f", logvalue);
					break;
				case joint::R_elbow_2:
					message_forSent->header = joint::R_elbow_2;
					message_forSent->value = 109;
					logvalue = message_forSent->value;
					CsvLogger->Log("testCpp.cpp", "main", "R_elbow_2 setpoint=%.3f", logvalue);
					break;
				case joint::R_forearm:
					message_forSent->header = joint::R_forearm;
					message_forSent->value = 110;
					logvalue = message_forSent->value;
					CsvLogger->Log("testCpp.cpp", "main", "R_forearm setpoint=%.3f", logvalue);
					break;
				default:
					break;
				}

				// Send SETPOINTS to ROBOT
				SendResult = TcpClient->Send(message_forSent);
				if (!SendResult) { printf("Can not send test message to the server"); }
				else { print_message("%d. Sent Setpoint Message to ROBOT: %s,%d\n", message_forSent); }
			}
		}

		samples++;
	  } while (samples<100);

	delete CsvLogger;
	return 0;
}