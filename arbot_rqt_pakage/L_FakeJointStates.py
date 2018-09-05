#!/usr/bin/env python

import rospy
from sensor_msgs.msg import JointState

def callback(data):
    #rospy.loginfo()
    rospy.loginfo(data.name)   
    rospy.loginfo(data.position)

def listener(): 

    rospy.init_node('listener', anonymous=True)
    rate = rospy.Rate(10) # 10hz
    rospy.Subscriber("/move_group/fake_controller_joint_states", JointState, callback)
    
    # spin() simply keeps python from exiting until this node is stopped
    rospy.spin()


if __name__ == '__main__':
    try:
        listener()
    except rospy.ROSInterruptException:
        pass
