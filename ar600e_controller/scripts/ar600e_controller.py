#!/usr/bin/env python

## ar600e_controller script implements basic control and monitoring
## of the AR-600E Humanoid Robot using MoveIt! as Inverse Kinematic Solver

import rospy
import math
from sensor_msgs.msg import JointState

def ConvertTickToRad(AngleInTick):
    AngleInDeg = AngleInTick / 100
    AngleInRad = math.radians(AngleInDeg)
    return AngleInRad

def ConvertRadToTick(AngleInRad):
    AngleInDeg = math.degrees(AngleInRad)
    AngleInTick = AngleInDeg * 100
    return AngleInTick

# Callbacks process incoming JointState Current Position from a ros-windwos-connector
def callback_0(data):
    jointstate_msg.position[0]=ConvertTickToRad(data.position[0])
def callback_1(data):
    jointstate_msg.position[1]=ConvertTickToRad(data.position[0])
def callback_2(data):
    jointstate_msg.position[2]=ConvertTickToRad(data.position[0])
def callback_3(data):
    jointstate_msg.position[3]=ConvertTickToRad(data.position[0])
def callback_4(data):
    jointstate_msg.position[4]=ConvertTickToRad(data.position[0])
def callback_5(data):
    jointstate_msg.position[5]=ConvertTickToRad(data.position[0])
def callback_6(data):
    jointstate_msg.position[6]=ConvertTickToRad(data.position[0])
def callback_7(data):
    jointstate_msg.position[7]=ConvertTickToRad(data.position[0])
def callback_8(data):
    jointstate_msg.position[8]=ConvertTickToRad(data.position[0])
def callback_9(data):
    jointstate_msg.position[9]=ConvertTickToRad(data.position[0])


def talker():
    rate = rospy.Rate(10) # 10hz
    # define Joint Setpoint message. It has to get setpoints from MoveIt! 
    jointsetpoint_msg = JointState()
    num_joints = 10
    jointsetpoint_msg.position = num_joints * [0.0]
    jointsetpoint_msg.name = num_joints * [""]
    jointsetpoint_msg.name[0] = "st_l_shoulder1"
    jointsetpoint_msg.name[1] = "st_l_shoulder2"
    jointsetpoint_msg.name[2] = "st_l_elbow1"
    jointsetpoint_msg.name[3] = "st_l_elbow2"
    jointsetpoint_msg.name[4] = "st_l_forearm"
    jointsetpoint_msg.name[5] = "st_r_shoulder1"
    jointsetpoint_msg.name[6] = "st_r_shoulder2"
    jointsetpoint_msg.name[7] = "st_r_elbow1"
    jointsetpoint_msg.name[8] = "st_r_elbow2"
    jointsetpoint_msg.name[9] = "st_r_forearm"
    jointsetpoint_msg.position[0] = -1
    jointsetpoint_msg.position[1] = -2
    jointsetpoint_msg.position[2] = -3
    jointsetpoint_msg.position[3] = -4
    jointsetpoint_msg.position[4] = -5
    jointsetpoint_msg.position[5] = -6
    jointsetpoint_msg.position[6] = -7
    jointsetpoint_msg.position[7] = -8
    jointsetpoint_msg.position[8] = -9
    jointsetpoint_msg.position[9] = -10
    
    # Define additional Joint Setpoint Messages in order to send to ros-windows-connector data from jointsetpoint_msg
    # It looks like RosSerial from http://wiki.ros.org/rosserial_windows/Tutorials
    # has obsolete C++ library for Windows, because JointState message is a double type,
    # However JointState message from ROS is a vector of double
    num_joints = 1
    
    st_l_shoulder1_msg = JointState()
    st_l_shoulder1_msg.position = num_joints * [0.0]
    st_l_shoulder1_msg.name = num_joints * ["l_shoulder1"]
    pub_1 = rospy.Publisher('st_l_shoulder1', JointState, queue_size=10)
    
    st_l_shoulder2_msg = JointState()
    st_l_shoulder2_msg.position = num_joints * [0.0]
    st_l_shoulder2_msg.name = num_joints * ["l_shoulder2"]
    pub_2 = rospy.Publisher('st_l_shoulder2', JointState, queue_size=10)

    st_l_elbow1_msg = JointState()
    st_l_elbow1_msg.position = num_joints * [0.0]
    st_l_elbow1_msg.name = num_joints * ["l_elbow1"]
    pub_3 = rospy.Publisher('st_l_elbow1', JointState, queue_size=10)

    st_l_elbow2_msg = JointState()
    st_l_elbow2_msg.position = num_joints * [0.0]
    st_l_elbow2_msg.name = num_joints * ["l_elbow2"]
    pub_4 = rospy.Publisher('st_l_elbow2', JointState, queue_size=10)

    st_l_forearm_msg = JointState()
    st_l_forearm_msg.position = num_joints * [0.0]
    st_l_forearm_msg.name = num_joints * ["l_forearm"]
    pub_5 = rospy.Publisher('st_l_forearm', JointState, queue_size=10)

    st_r_shoulder1_msg = JointState()
    st_r_shoulder1_msg.position = num_joints * [0.0]
    st_r_shoulder1_msg.name = num_joints * ["r_shoulder1"]
    pub_6 = rospy.Publisher('st_r_shoulder1', JointState, queue_size=10)
    
    st_r_shoulder2_msg = JointState()
    st_r_shoulder2_msg.position = num_joints * [0.0]
    st_r_shoulder2_msg.name = ["r_shoulder2"]
    pub_7 = rospy.Publisher('st_r_shoulder2', JointState, queue_size=10)

    st_r_elbow1_msg = JointState()
    st_r_elbow1_msg.position = num_joints * [0.0]
    st_r_elbow1_msg.name = num_joints *  ["r_elbow1"]
    pub_8 = rospy.Publisher('st_r_elbow1', JointState, queue_size=10)

    st_r_elbow2_msg = JointState()
    st_r_elbow2_msg.position = num_joints * [0.0]
    st_r_elbow2_msg.name = num_joints * ["r_elbow2"]
    pub_9 = rospy.Publisher('st_r_elbow2', JointState, queue_size=10)

    st_r_forearm_msg = JointState()
    st_r_forearm_msg.position = num_joints * [0.0]
    st_r_forearm_msg.name = num_joints * ["r_forearm"]
    pub_10 = rospy.Publisher('st_r_forearm', JointState, queue_size=10)
    # Make setpoint vary in positive and reverse direction for testing purposes using index and delta
    index = 0
    #    delta 15 deg in rad
    delta = math.radians(15)
    while not rospy.is_shutdown():
        # get Joint Position Setpoint FROM MoveIt!
        jointsetpoint_msg.header.stamp = rospy.Time.now()
	# REPLACE CODE FROM HERE
        CircleInRad = math.radians(index)
        a = math.sin(CircleInRad)
        jointsetpoint_msg.position[0] = jointstate_msg.position[0] + a*delta
        jointsetpoint_msg.position[1] = jointstate_msg.position[1] 
        jointsetpoint_msg.position[2] = jointstate_msg.position[2] 
        jointsetpoint_msg.position[3] = jointstate_msg.position[3] 
        jointsetpoint_msg.position[4] = jointstate_msg.position[4] 
        jointsetpoint_msg.position[5] = jointstate_msg.position[5] 
        jointsetpoint_msg.position[6] = jointstate_msg.position[6] 
        jointsetpoint_msg.position[7] = jointstate_msg.position[7] 
        jointsetpoint_msg.position[8] = jointstate_msg.position[8] 
        jointsetpoint_msg.position[9] = jointstate_msg.position[9]
    # Step sin in degree 
        index += 5
	# TO HERE in order to get new setpoint from MoveIt! and send it to Robot

	# Define new setpoints to JointState Setpoint msgs
        st_l_shoulder1_msg.position = num_joints * [ConvertRadToTick(jointsetpoint_msg.position[0])]
        st_l_shoulder2_msg.position = num_joints * [ConvertRadToTick(jointsetpoint_msg.position[1])]
        st_l_elbow1_msg.position = num_joints * [ConvertRadToTick(jointsetpoint_msg.position[2])]
        st_l_elbow2_msg.position = num_joints * [ConvertRadToTick(jointsetpoint_msg.position[3])]
        st_l_forearm_msg.position = num_joints * [ConvertRadToTick(jointsetpoint_msg.position[4])]
        st_r_shoulder1_msg.position = num_joints * [ConvertRadToTick(jointsetpoint_msg.position[5])]
        st_r_shoulder2_msg.position = num_joints * [ConvertRadToTick(jointsetpoint_msg.position[6])]
        st_r_elbow1_msg.position = num_joints * [ConvertRadToTick(jointsetpoint_msg.position[7])]
        st_r_elbow2_msg.position = num_joints * [ConvertRadToTick(jointsetpoint_msg.position[8])]
        st_r_forearm_msg.position = num_joints * [ConvertRadToTick(jointsetpoint_msg.position[9])]
    # And send new setpoints to ros-windows-connector (it sends it to Robot)
        pub_1.publish(st_l_shoulder1_msg)
        pub_2.publish(st_l_shoulder2_msg)
        pub_3.publish(st_l_elbow1_msg)
        pub_4.publish(st_l_elbow2_msg)
        pub_5.publish(st_l_forearm_msg)
        pub_6.publish(st_r_shoulder1_msg)
        pub_7.publish(st_r_shoulder2_msg)
        pub_8.publish(st_r_elbow1_msg)
        pub_9.publish(st_r_elbow2_msg)
        pub_10.publish(st_r_forearm_msg)
    # print JointState Setpoint for a debug purpose
        print ("Joint_Set_Point")
        rospy.loginfo(jointsetpoint_msg)
        #rospy.loginfo( jointsetpoint_msg.position[0])
    #print JointState Current Position (it is already updated from callbacks) for a debug purpose
        print ("Joint_Current_Point")
        rospy.loginfo(jointstate_msg)
        #rospy.loginfo(jointstate_msg.position[0])
        rate.sleep()

if __name__ == '__main__':
    rospy.init_node('ar600e_controller')
    # define JointState Current Position message. It has to get current value from Robot
    jointstate_msg = JointState()
    num_joints = 10
    jointstate_msg.position = num_joints * [0.0]
    jointstate_msg.name = num_joints * [""]
    jointstate_msg.name[0] = "l_shoulder1"
    jointstate_msg.name[1] = "l_shoulder2"
    jointstate_msg.name[2] = "l_elbow1"
    jointstate_msg.name[3] = "l_elbow2"
    jointstate_msg.name[4] = "l_forearm"
    jointstate_msg.name[5] = "r_shoulder1"
    jointstate_msg.name[6] = "r_shoulder2"
    jointstate_msg.name[7] = "r_elbow1"
    jointstate_msg.name[8] = "r_elbow2"
    jointstate_msg.name[9] = "r_forearm"

    # define callbacks for a JointState Current Position message FROM a Robot
    # It looks like RosSerial from http://wiki.ros.org/rosserial_windows/Tutorials
    # has obsolete C++ library for Windows, because JointState message is a double type,
    # However JointState message from ROS is a vector of double
    rospy.Subscriber('l_shoulder1', JointState, callback_0)
    rospy.Subscriber('l_shoulder2', JointState, callback_1)
    rospy.Subscriber('l_elbow1', JointState, callback_2)
    rospy.Subscriber('l_elbow2', JointState, callback_3)
    rospy.Subscriber('l_forearm', JointState, callback_4)
    rospy.Subscriber('r_shoulder1', JointState, callback_5)
    rospy.Subscriber('r_shoulder2', JointState, callback_6)
    rospy.Subscriber('r_elbow1', JointState, callback_7)
    rospy.Subscriber('r_elbow2', JointState, callback_8)
    rospy.Subscriber('r_forearm', JointState, callback_9)

    
    try:
	# start sending for a jointsetpoint_msg message TO a Robot
        talker()
    except rospy.ROSInterruptException:
        pass
