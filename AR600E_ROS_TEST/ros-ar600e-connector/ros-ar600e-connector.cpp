// ros-ar600e-connector.cpp : sending and receiving joint states
// from Windows using rosserial
//
#include "stdafx.h"

#include <time.h>

#include <string>
#include <stdio.h>
#include "ros.h"
#include <sensor_msgs/JointState.h>

#define WIN32_LEAN_AND_MEAN
#include <windows.h>

#include "TCPClient_class.h"

//using std::string;

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

sensor_msgs::JointState jointsetpoint_msg[10];

void setpoint_l_shoulder1_callback(const sensor_msgs::JointState & setpnt)
{
	//printf("Received pose %f, %f, %f\n", pose.pose.pose.position.x,
	//	pose.pose.pose.position.y, pose.pose.pose.position.z);
	jointsetpoint_msg[0].position = setpnt.position;
}
void setpoint_l_shoulder2_callback(const sensor_msgs::JointState & setpnt)
{
	jointsetpoint_msg[1].position = setpnt.position;
}
void setpoint_l_elbow1_callback(const sensor_msgs::JointState & setpnt)
{
	jointsetpoint_msg[2].position = setpnt.position;
}
void setpoint_l_elbow2_callback(const sensor_msgs::JointState & setpnt)
{
	jointsetpoint_msg[3].position = setpnt.position;
}
void setpoint_l_forearm_callback(const sensor_msgs::JointState & setpnt)
{
	jointsetpoint_msg[4].position = setpnt.position;
}
void setpoint_r_shoulder1_callback(const sensor_msgs::JointState & setpnt)
{
	jointsetpoint_msg[5].position = setpnt.position;
}
void setpoint_r_shoulder2_callback(const sensor_msgs::JointState & setpnt)
{
	jointsetpoint_msg[6].position = setpnt.position;
}
void setpoint_r_elbow1_callback(const sensor_msgs::JointState & setpnt)
{
	jointsetpoint_msg[7].position = setpnt.position;
}
void setpoint_r_elbow2_callback(const sensor_msgs::JointState & setpnt)
{
	jointsetpoint_msg[8].position = setpnt.position;
}
void setpoint_r_forearm_callback(const sensor_msgs::JointState & setpnt)
{
	jointsetpoint_msg[9].position = setpnt.position;
}

int __cdecl main(int argc, char **argv)
{
	printf("\nros-ar600e-connector\n");

	// TCP Client Init and Connect to WINDOWS PC
	msg_count = 0;
	clock_t start, finish;
	double  duration;
	MESSAGE* message = new MESSAGE;
	MESSAGE* message_forSent = new MESSAGE;
	
	 
	TCPClient_class* TcpClient = new TCPClient_class();
	if (!TcpClient->Connect()) { printf("Can not connect to the TCP server at localhost"); }
	

	// ROS-WINDOWS-CONNECTOR PUBLISHER INIT and CONNECT to ROS
	ros::NodeHandle nh;
	char *ros_master = "192.169.1.3";
	printf("Connecting to server at %s\n", ros_master);
	
	nh.initNode(ros_master);
	
	//printf("Advertising jnt_st message\n");

	char* names[10];
	names[0] = "l_shoulder1"; names[1] = "l_shoulder2";
	names[2] = "l_elbow1"; names[3] = "l_elbow2";
	names[4] = "l_forearm";
	names[5] = "r_shoulder1"; names[6] = "r_shoulder2";
	names[7] = "r_elbow1"; names[8] = "r_elbow2";
	names[9] = "r_forearm";

	double positions[10];
	positions[0] = 0; positions[1] = 0;
	positions[2] = 0; positions[3] = 0;
	positions[4] = 0;
	positions[5] = 0; positions[6] = 0;
	positions[7] = 0; positions[8] = 0;
	positions[9] = 0;

	sensor_msgs::JointState jointstate_msg[10];
	//ros::Publisher jnt_st_pub("jnt_state", &jointstate_msg
	ros::Publisher jnt_st_pub_0(names[0], &jointstate_msg[0]);
	ros::Publisher jnt_st_pub_1(names[1], &jointstate_msg[1]);
	ros::Publisher jnt_st_pub_2(names[2], &jointstate_msg[2]);
	ros::Publisher jnt_st_pub_3(names[3], &jointstate_msg[3]);
	ros::Publisher jnt_st_pub_4(names[4], &jointstate_msg[4]);
	ros::Publisher jnt_st_pub_5(names[5], &jointstate_msg[5]);
	ros::Publisher jnt_st_pub_6(names[6], &jointstate_msg[6]);
	ros::Publisher jnt_st_pub_7(names[7], &jointstate_msg[7]);
	ros::Publisher jnt_st_pub_8(names[8], &jointstate_msg[8]);
	ros::Publisher jnt_st_pub_9(names[9], &jointstate_msg[9]);
	nh.advertise(jnt_st_pub_0);
	nh.advertise(jnt_st_pub_1);
	nh.advertise(jnt_st_pub_2);
	nh.advertise(jnt_st_pub_3);
	nh.advertise(jnt_st_pub_4);
	nh.advertise(jnt_st_pub_5);
	nh.advertise(jnt_st_pub_6);
	nh.advertise(jnt_st_pub_7);
	nh.advertise(jnt_st_pub_8);
	nh.advertise(jnt_st_pub_9);
	
	for (int i=0; i < 10; i++)
	{
		jointstate_msg[i].name_length = 1;
		jointstate_msg[i].name = &names[i];
		jointstate_msg[i].position_length = 1;
		jointstate_msg[i].position = &positions[i];
	}
	
	//sensor_msgs::JointState jointsetpoint_msg[10];
	char* st_names[10];
	st_names[0] = "st_l_shoulder1";
	st_names[1] = "st_l_shoulder2";
	st_names[2] = "st_l_elbow1";
	st_names[3] = "st_l_elbow2";
	st_names[4] = "st_l_forearm";
	st_names[5] = "st_r_shoulder1";
	st_names[6] = "st_r_shoulder2";
	st_names[7] = "st_r_elbow1";
	st_names[8] = "st_r_elbow2";
	st_names[9] = "st_r_forearm";

	for (int i = 0; i < 10; i++)
	{
		jointsetpoint_msg[i].name_length = 1;
		jointsetpoint_msg[i].name = &st_names[i];
		jointsetpoint_msg[i].position_length = 1;
		jointsetpoint_msg[i].position = &positions[i];
	}

	ros::Subscriber <sensor_msgs::JointState> st_l_shoulder1_Sub(st_names[0], &setpoint_l_shoulder1_callback);
	ros::Subscriber <sensor_msgs::JointState> st_l_shoulder2_Sub(st_names[1], &setpoint_l_shoulder2_callback);
	ros::Subscriber <sensor_msgs::JointState> st_l_elbow1_Sub(st_names[2], &setpoint_l_elbow1_callback);
	ros::Subscriber <sensor_msgs::JointState> st_l_elbow2_Sub(st_names[3], &setpoint_l_elbow2_callback);
	ros::Subscriber <sensor_msgs::JointState> st_l_forearm_Sub(st_names[4], &setpoint_l_forearm_callback);
	ros::Subscriber <sensor_msgs::JointState> st_r_shoulder1_Sub(st_names[5], &setpoint_r_shoulder1_callback);
	ros::Subscriber <sensor_msgs::JointState> st_r_shoulder2_Sub(st_names[6], &setpoint_r_shoulder2_callback);
	ros::Subscriber <sensor_msgs::JointState> st_r_elbow1_Sub(st_names[7], &setpoint_r_elbow1_callback);
	ros::Subscriber <sensor_msgs::JointState> st_r_elbow2_Sub(st_names[8], &setpoint_r_elbow2_callback);
	ros::Subscriber <sensor_msgs::JointState> st_r_forearm_Sub(st_names[9], &setpoint_r_forearm_callback);
	nh.subscribe(st_l_shoulder1_Sub);
	nh.subscribe(st_l_shoulder2_Sub);
	nh.subscribe(st_l_elbow1_Sub);
	nh.subscribe(st_l_elbow2_Sub);
	nh.subscribe(st_l_forearm_Sub);
	nh.subscribe(st_r_shoulder1_Sub);
	nh.subscribe(st_r_shoulder2_Sub);
	nh.subscribe(st_r_elbow1_Sub);
	nh.subscribe(st_r_elbow2_Sub);
	nh.subscribe(st_r_forearm_Sub);
	//double(*)[10] ;
	
	
	//position_var = positions;
	//jointstate_msg.position.resize(10);
	

	bool SendResult, ReceiveResult;
	ReceiveResult = false;
	// Start timer.  
	start = clock();

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
				break;
			case joint::L_shoulder_2:
				positions[1] = message->value;
				break;
			case joint::L_elbow_1:
				positions[2] = message->value;
				break;
			case joint::L_elbow_2:
				positions[3] = message->value;
				break;
			case joint::L_forearm:
				positions[4] = message->value;
				break;
			case joint::R_shoulder_1:
				positions[5] = message->value;
				break;
			case joint::R_shoulder_2:
				positions[6] = message->value;
				break;
			case joint::R_elbow_1:
				positions[7] = message->value;
				break;
			case joint::R_elbow_2:
				positions[8] = message->value;
				break;
			case joint::R_forearm:
				positions[9] = message->value;
				break;
			default:
				break;
			}
			
		}
		else
		{
			// Publish Current Joint Position to ROS
			jointstate_msg[0].position = &positions[0];
			jointstate_msg[1].position = &positions[1];
			jointstate_msg[2].position = &positions[2];
			jointstate_msg[3].position = &positions[3];
			jointstate_msg[4].position = &positions[4];
			jointstate_msg[5].position = &positions[5];
			jointstate_msg[6].position = &positions[6];
			jointstate_msg[7].position = &positions[7];
			jointstate_msg[8].position = &positions[8];
			jointstate_msg[9].position = &positions[9];
			jnt_st_pub_0.publish(&jointstate_msg[0]);
			jnt_st_pub_1.publish(&jointstate_msg[1]);
			jnt_st_pub_2.publish(&jointstate_msg[2]);
			jnt_st_pub_3.publish(&jointstate_msg[3]);
			jnt_st_pub_4.publish(&jointstate_msg[4]);
			jnt_st_pub_5.publish(&jointstate_msg[5]);
			jnt_st_pub_6.publish(&jointstate_msg[6]);
			jnt_st_pub_7.publish(&jointstate_msg[7]);
			jnt_st_pub_8.publish(&jointstate_msg[8]);
			jnt_st_pub_9.publish(&jointstate_msg[9]);
			nh.spinOnce();
		}

		// Get Joint Setpoint Position from ROS
		// ADD CODE HERE
		
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
					message_forSent->value = *jointsetpoint_msg[i].position;
					break;
				case joint::L_shoulder_2:
					message_forSent->header = joint::L_shoulder_2;
					message_forSent->value = *jointsetpoint_msg[i].position;
					break;
				case joint::L_elbow_1:
					message_forSent->header = joint::L_elbow_1;
					message_forSent->value = *jointsetpoint_msg[i].position;
					break;
				case joint::L_elbow_2:
					message_forSent->header = joint::L_elbow_2;
					message_forSent->value = *jointsetpoint_msg[i].position;
					break;
				case joint::L_forearm:
					message_forSent->header = joint::L_forearm;
					message_forSent->value = *jointsetpoint_msg[i].position;
					break;
				case joint::R_shoulder_1:
					message_forSent->header = joint::R_shoulder_1;
					message_forSent->value = *jointsetpoint_msg[i].position;
					break;
				case joint::R_shoulder_2:
					message_forSent->header = joint::R_shoulder_2;
					message_forSent->value = *jointsetpoint_msg[i].position;
					break;
				case joint::R_elbow_1:
					message_forSent->header = joint::R_elbow_1;
					message_forSent->value = *jointsetpoint_msg[i].position;
					break;
				case joint::R_elbow_2:
					message_forSent->header = joint::R_elbow_2;
					message_forSent->value = *jointsetpoint_msg[i].position;
					break;
				case joint::R_forearm:
					message_forSent->header = joint::R_forearm;
					message_forSent->value = *jointsetpoint_msg[i].position;
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

	} while (true);

	return 0;
}

