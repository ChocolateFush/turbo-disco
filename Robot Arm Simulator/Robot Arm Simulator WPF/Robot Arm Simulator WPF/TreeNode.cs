using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot_Arm_Simulator_WPF
{
    public class TreeNode
    {
        public int parentIndex = 0;
        public double jointAngleValue = 0;
        public int jointNumber = 0;
        public double jointAngleValueDeg = 0;
        public double angleT = 0.0;

        public TreeNode(int pIndex, double jAngleValue, int jNumber) {
            parentIndex = pIndex;
            jointAngleValue = jAngleValue;
            jointNumber = jNumber;
            jointAngleValueDeg = jointAngleValue / Math.PI * 180.0;
        }
        
    }
}
