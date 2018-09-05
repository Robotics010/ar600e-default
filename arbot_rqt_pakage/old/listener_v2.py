#!/usr/bin/env python

import rospy
#from std_msgs.msg import String
from moveit_msgs.msg import RobotState, JointConstraint
from sensor_msgs.msg import JointState
import moveit_commander

group = moveit_commander.MoveGroupCommander("arm")
active_joints = group.get_active_joints()    
scene = moveit_commander.PlanningSceneInterface 

def callback(data):
    i = 0
    while i != len(active_joints): 
        rospy.loginfo("%s = %f", data.name[i], data.position[i])
        i = i + 1
    #rospy.loginfo(data.name)   
    #rospy.loginfo(data.position)

def listener(): 
    
    rospy.sleep(1)
    rospy.init_node('listener', anonymous=True)
    rospy.Subscriber("talker", JointState, callback)

    #joint_limits = JointConstraint()
    #joint_limits.joint_name = active_joints[0]
    #print "Position: ", joint_limits.position
    #print "Min_value: ", joint_limits.tolerance_below
    #print "Max_value: ", joint_limits.tolerance_above
    
    # spin() simply keeps python from exiting until this node is stopped
    rospy.spin()


if __name__ == '__main__':
    try:
        listener()
    except rospy.ROSInterruptException:
        pass
