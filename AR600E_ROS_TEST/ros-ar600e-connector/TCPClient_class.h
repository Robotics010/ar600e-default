#ifndef TCPCLIENT_CLASS_H
#define TCPCLIENT_CLASS_H

#include <stdio.h>
//#pragma once

// Including SDKDDKVer.h defines the highest available Windows platform.

// If you wish to build your application for a previous Windows platform, include WinSDKVer.h and
// set the _WIN32_WINNT macro to the platform you wish to support before including SDKDDKVer.h.

//#include <SDKDDKVer.h>

#ifndef MESSAGE_H
#define MESSAGE_H

enum joint {
	L_shoulder_1, L_shoulder_2, L_elbow_1, L_elbow_2, L_forearm,
	R_shoulder_1, R_shoulder_2, R_elbow_1, R_elbow_2, R_forearm,
	disconnect
};

typedef struct
{
	joint header;
	__int16 value;
} MESSAGE;

#endif 

#define DEFAULT_BUFLEN sizeof(MESSAGE)
#define DEFAULT_PORT "27016"


#define WIN32_LEAN_AND_MEAN
#include <windows.h>
#include <winsock2.h>
#include <ws2tcpip.h>
//#include <stdlib.h>


// Need to link with Ws2_32.lib, Mswsock.lib, and Advapi32.lib
#pragma comment (lib, "Ws2_32.lib")
#pragma comment (lib, "Mswsock.lib")
#pragma comment (lib, "AdvApi32.lib")


class TCPClient_class{
public:
	TCPClient_class();
	bool Connect();
	bool Send(MESSAGE* message);
	bool Receive(MESSAGE* message);
	int Get_iResult();
	bool Disconnect();
private:
	WSADATA wsaData;
	SOCKET ConnectSocket = INVALID_SOCKET;
	struct addrinfo *result = NULL,
		*ptr = NULL,
		hints;
	char *server_address;

	char recvbuf[DEFAULT_BUFLEN];
	int rv;
	char sendbuf[DEFAULT_BUFLEN];
	int iResult;
	int recvbuflen, sendbuflen;

	MESSAGE* message;

	void message_flatten(MESSAGE* message, char* flatten_message);
	void message_unflatten(char* flatten_message, MESSAGE* message);
	
	~TCPClient_class();
};
#endif //TCPCLIENT_CLASS_H