
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot_Arm_Simulator_WPF
{
    public class Pose
    {
        public double[,] poseMatrix = { { 0, 1, 0, 0 }, 
                                 { 0, 0, 1, 0.36908 }, 
                                 { 1, 0, 0, 0.3582 }, 
                                 { 0, 0, 0, 1 } };
        public int poseIndex;

        public double[] poseAngles = { 0, 0, 0, 0, 0, 0 };

        public Pose(double[,] newPoseMatrix) {
            poseMatrix = newPoseMatrix;
        }

        public Pose() { }
    }
}
