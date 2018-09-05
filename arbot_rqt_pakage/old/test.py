#!/usr/bin/env python

import sys
import rospy
from std_msgs.msg import ColorRGBA
from sensor_msgs.msg import JointState
from moveit_msgs.msg import RobotState, ObjectColor
import moveit_commander
import moveit_msgs.msg
import geometry_msgs.msg

def talker():

    display_robot_state_publisher = rospy.Publisher('/move_group/fake_controller_joint_states', JointState, queue_size=10)
    rospy.init_node('talker', anonymous=True)
    rate = rospy.Rate(10)

    print "============ Starting setup"

    robot = moveit_commander.RobotCommander()
    group = moveit_commander.MoveGroupCommander("arm")

    print "============ Robot Groups: %s" % robot.get_group_names()

    print "============ End effector: %s" % group.get_end_effector_link()
 
    active_joints = group.get_active_joints()
    print "============ Active joints: ", active_joints

    joints_values_str = group.get_current_joint_values()
    print "============ Joint values: ", joints_values_str

    group.clear_pose_targets()

    print "============ Set start state..."
    i = 0
    while i != len(active_joints):
        a = float(input("Vvedite znachenie %s: " % active_joints[i]))
        joints_values_str[i] = a
        i = i + 1
    
    start_joint_state = JointState()
    start_joint_state.header.stamp = rospy.Time.now()
    start_joint_state.name = active_joints
    start_joint_state.position = joints_values_str
    moveit_robot_state = RobotState()
    moveit_robot_state.joint_state = start_joint_state
    group.set_start_state(moveit_robot_state)
    #group.set_start_state_to_current_state()
    display_robot_state_publisher.publish(start_joint_state)
    print "============ Display start state..."
    while not rospy.is_shutdown():
        #display_robot_state = moveit_msgs.msg.DisplayRobotState()
        #display_robot_state.state = moveit_robot_state
        #Object_color = ObjectColor()
        #Object_color.id = active_joints
        #Object_color.color = ColorRGBA(250, 118, 0, 1)
        #display_robot_state.highlight_links = [250, 118, 0, 1]
        #display_robot_state_publisher.publish(start_joint_state)
        #group.set_start_state(moveit_robot_state)
        rate.sleep()

if __name__ == '__main__':
    try:
        talker()
    except rospy.ROSInterruptException:
        pass


