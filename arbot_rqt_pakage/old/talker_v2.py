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
    print "============ Starting setup"
    display_robot_state_publisher = rospy.Publisher('/move_group/fake_controller_joint_states', JointState, queue_size=10)
    rospy.init_node('talker', anonymous=True)
    rate = rospy.Rate(10)

    robot = moveit_commander.RobotCommander()
    group = moveit_commander.MoveGroupCommander("arm")
   
    print "============ Robot Groups:"
    print robot.get_group_names()

    print "============ End effector: %s" % group.get_end_effector_link()
	
    #print "============ Printing robot state"
    #print robot.get_current_state()
    #print "============"
 
    active_joints = group.get_active_joints()
    print "============ Active joints: ", active_joints

    joints_values_str_0 = group.get_current_joint_values()
    joints_values_str = group.get_current_joint_values()
    print "============ Start state: ", joints_values_str_0
    
    print "============ Set goal state..."
    i = 0
    while i != len(active_joints):
        a = float(input("Vvedite znachenie %s: " % active_joints[i]))
        joints_values_str[i] = a
        i = i + 1

    goal_joint_state = JointState()
    goal_joint_state.name = active_joints
    goal_joint_state.position = joints_values_str

    print "============ Start state: ", joints_values_str_0
    print "============ Goal state: ", joints_values_str
    print input("Press enter to plan trajectory...")
    group.clear_pose_targets()
    group.set_joint_value_target(joints_values_str)
    plan = group.plan()
  # Uncomment below line when working with a real robot
  # group.go(wait=True)

  # Use execute instead if you would like the robot to follow
  # the plan that has already been computed
  # group.execute(plan1)

    index = 0
    while not rospy.is_shutdown():
    goal_joint_state.header.stamp = rospy.Time.now()
    display_robot_state_publisher.publish(goal_joint_state)
        rospy.sleep(1)


if __name__ == '__main__':
    try:
        talker()
    except rospy.ROSInterruptException:
        pass
