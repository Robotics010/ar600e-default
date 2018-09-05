#!/usr/bin/env python
# Software License Agreement (BSD License)
#
# Copyright (c) 2008, Willow Garage, Inc.
# All rights reserved.
#
# Redistribution and use in source and binary forms, with or without
# modification, are permitted provided that the following conditions
# are met:
#
#  * Redistributions of source code must retain the above copyright
#    notice, this list of conditions and the following disclaimer.
#  * Redistributions in binary form must reproduce the above
#    copyright notice, this list of conditions and the following
#    disclaimer in the documentation and/or other materials provided
#    with the distribution.
#  * Neither the name of Willow Garage, Inc. nor the names of its
#    contributors may be used to endorse or promote products derived
#    from this software without specific prior written permission.
#
# THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
# "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
# LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
# FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
# COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
# INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
# BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
# LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
# CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
# LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
# ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
# POSSIBILITY OF SUCH DAMAGE.
#
# Revision $Id$

## Simple talker demo that published std_msgs/Strings messages
## to the 'chatter' topic

import rospy
#from std_msgs.msg import String
from sensor_msgs.msg import JointState

def talker():
    pub = rospy.Publisher('jnt_state', JointState, queue_size=10)
    rospy.init_node('talker', anonymous=True)
    rate = rospy.Rate(10) # 10hz
    
    jointstate_msg = JointState()
    num_joints = 2
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
    index = 0
    
    while not rospy.is_shutdown():
        #hello_str = "published jnt_st message at %s" % rospy.get_time()
	
	jointstate_msg.header.stamp = rospy.Time.now()
	
	jointstate_msg.position[0] = index
	index = index + 1
	jointstate_msg.position[1] = index
	index = index + 1
	jointstate_msg.position[2] = index
	index = index + 1
	jointstate_msg.position[3] = index
	index = index + 1
	jointstate_msg.position[4] = index
	index = index + 1
	jointstate_msg.position[5] = index
	index = index + 1
	jointstate_msg.position[6] = index
	index = index + 1
	jointstate_msg.position[7] = index
	index = index + 1
	jointstate_msg.position[8] = index
	index = index + 1
	jointstate_msg.position[9] = index
	index = index + 1

        rospy.loginfo(jointstate_msg)
        pub.publish(jointstate_msg)
        rate.sleep()

if __name__ == '__main__':
    try:
        talker()
    except rospy.ROSInterruptException:
        pass
