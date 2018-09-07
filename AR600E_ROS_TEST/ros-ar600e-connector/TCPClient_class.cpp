//#include "stdafx.h"
#include "TCPClient_class.h"

	TCPClient_class::TCPClient_class()
	{
		server_address = "127.0.0.1";

		recvbuflen = DEFAULT_BUFLEN;
		sendbuflen = DEFAULT_BUFLEN;
		// test message
		message = new MESSAGE;
		message->header = joint::L_elbow_1;
		message->value = 10922;

		// Initialize Winsock
		iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);
		if (iResult != 0) {
			printf("TPClient_class:WSAStartup failed with error: %d\n", iResult);
			//return 1;
		}

		ZeroMemory(&hints, sizeof(hints));
		hints.ai_family = AF_UNSPEC;
		hints.ai_socktype = SOCK_STREAM;
		hints.ai_protocol = IPPROTO_TCP;

	}

	bool TCPClient_class::Connect()
	{
		// Resolve the server address and port
		iResult = getaddrinfo(server_address, DEFAULT_PORT, &hints, &result);
		if (iResult != 0) {
			printf("TPClient_class:getaddrinfo failed with error: %d\n", iResult);
			WSACleanup();
			return false;
		}

		// Attempt to connect to an address until one succeeds
		for (ptr = result; ptr != NULL; ptr = ptr->ai_next) {

			// Create a SOCKET for connecting to server
			ConnectSocket = socket(ptr->ai_family, ptr->ai_socktype,
				ptr->ai_protocol);
			if (ConnectSocket == INVALID_SOCKET) {
				printf("TPClient_class:socket failed with error: %ld\n", WSAGetLastError());
				WSACleanup();
				return false;
			}

			// Connect to server.
			iResult = connect(ConnectSocket, ptr->ai_addr, (int)ptr->ai_addrlen);
			if (iResult == SOCKET_ERROR) {
				closesocket(ConnectSocket);
				ConnectSocket = INVALID_SOCKET;
				continue;
			}
			break;
		}

		freeaddrinfo(result);

		if (ConnectSocket == INVALID_SOCKET) {
			printf("TPClient_class:Unable to connect to server!\n");
			WSACleanup();
			return false;
		}

		return true;
	}

	bool TCPClient_class::Send(MESSAGE* message)
	{
		message_flatten(message, sendbuf);
		iResult = send(ConnectSocket, sendbuf, sendbuflen, 0);

		if (iResult == SOCKET_ERROR) {
			printf("TPClient_class:send failed with error: %d\n", WSAGetLastError());
			closesocket(ConnectSocket);
			WSACleanup();
			return false;
		}
		return true;
	}

	bool TCPClient_class::Receive(MESSAGE* message)
	{
		//select()
		fd_set read_s; // pointer to a set of sockets to be checked for readability
		timeval time_out; // The maximum time for select to wait, provided in the form of a TIMEVAL structure
		FD_ZERO(&read_s); // Clear the set
		FD_SET(ConnectSocket, &read_s); // Adding socket in it
		time_out.tv_sec = 0; time_out.tv_usec = 100000; //Timeout 0.1 sec

		rv = select(0, &read_s, NULL, NULL, &time_out);

		//iResult = recv(ConnectSocket, recvbuf, recvbuflen, 0);
		//if (iResult > 0)
		if (rv == SOCKET_ERROR)
		{
			// select error...
			printf("TPClient_class:Select Error\n");
			return false;
		}
		else if (rv == 0)
		{
			// timeout, socket does not have anything to read
			printf("TPClient_class:Read Timeout\n");
			return false;
		}
		else
		{
			iResult = recv(ConnectSocket, recvbuf, recvbuflen, 0);
			//MESSAGE* message_received = new MESSAGE;
			message_unflatten(recvbuf, message);
			printf("TPClient_class:Read Message \n");
			//print_message("Unlatten Message: %s,%d\n", message_received);
			//printf("value: %d\n", message_received->value);
			//printf("Bytes received: %d\n", iResult);
		}
		//else if (iResult == 0)
			//printf("Connection closed\n");
		//else
			//printf("recv failed with error: %d\n", WSAGetLastError());

		/*
		int rv = select(s + 1, &set, NULL, NULL, &timeout);
		if (rv == SOCKET_ERROR)
		{
			// select error...
		}
		else if (rv == 0)
		{
			// timeout, socket does not have anything to read
		}
		else
		{
			// socket has something to read
			recv_size = recv(s, rx_tmp, bufSize, 0);
			if (recv_size == SOCKET_ERROR)
			{
				// read failed...
			}
			else if (recv_size == 0)
			{
				// peer disconnected...
			}
			else
			{
				// read successful...
			}
		}
		*/

		return true;
	}

	int TCPClient_class::Get_iResult()
	{
		return iResult;
	}

	bool TCPClient_class::Disconnect()
	{
		// shutdown the connection since no more data will be sent
		iResult = shutdown(ConnectSocket, SD_SEND);
		if (iResult == SOCKET_ERROR) {
			printf("TPClient_class:shutdown failed with error: %d\n", WSAGetLastError());
			closesocket(ConnectSocket);
			WSACleanup();
			}

		closesocket(ConnectSocket);
		return true;
	}

	TCPClient_class::~TCPClient_class()
	{
		// cleanup
		WSACleanup();
	}


	

	void TCPClient_class::message_flatten(MESSAGE* message, char* flatten_message)
	{
		int *q = (int*)flatten_message;
		*q = message->header;  q++;
		*q = message->value;   q++;
	}

	void TCPClient_class::message_unflatten(char* flatten_message, MESSAGE* message)
	{
		int *q = (int*)flatten_message;
		message->header = (joint)*q;	q++;
		message->value = *q;			q++;
	}



