#!/usr/bin/env python
# license removed for brevity
import rospy
from std_msgs.msg import Header
from sensor_msgs.msg import JointState

def talker():
    pub = rospy.Publisher('/joint_states', JointState, queue_size=10)
    rospy.init_node('joint_state_publisher')
    rate = rospy.Rate(10) # 10hz

    print "============ Set start state..."

    joint_start_state_msg = JointState()
    #joint_start_state_msg.header = Header()
    #joint_start_state_msg.header.stamp = rospy.Time.now()
    joint_start_state_msg.header.frame_id = 'base_link'
    joint_start_state_msg.name = ['Ruka_X_Right', 'Ruka_Y_Right', 'Lokot_vr_Right', 'Lokot_Right', 'Kist_povorot_Right']
        
    s = [0, 0, 0, 1, 0]
#    n = len(joint_start_state_msg.name)
#    i = 0

#    for i in range(n):  
#        new_element = float(input("Vvedite znachenie %s: " % joint_start_state_msg.name[i]))
#        s.append(new_element)

    joint_start_state_msg.position = s
    #joint_start_state_msg.position = float(input("Vvedite znachenie %s: "))
    joint_start_state_msg.velocity = []
    joint_start_state_msg.effort = [] 
    
    while not rospy.is_shutdown():
        #rospy.loginfo(hello_str)
        joint_start_state_msg.header.stamp = rospy.Time.now()
        pub.publish(joint_start_state_msg)
        rate.sleep()

if __name__ == '__main__':
    try:
        talker()
    except rospy.ROSInterruptException:
        pass



