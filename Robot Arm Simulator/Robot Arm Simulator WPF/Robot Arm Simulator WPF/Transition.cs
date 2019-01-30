using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Robot_Arm_Simulator_WPF
{
    public enum PathType
    {
        Direct = 0,
        StraightLine,
        Circle
    };
    public enum CirclePlane
    {
        XY = 0,
        XZ,
        YZ
    };
    public class Transition
    {
        public PathType pathType = PathType.Direct;
        public double speed = 10.0;
        public CirclePlane circlePlane = CirclePlane.XY;
        public double circleRadius = 0.0;
        public int directionInt = 1;
        public int uniqueIntermediatePoints = 0;
        public bool[] lockValues = { false, false, false, false };

        public Transition() { }
        public Transition(PathType pType, double s, CirclePlane cPlane, double p, int dInt,int uniquePoints, bool[] nlockValues)
        {
            pathType = pType;
            speed = s;
            circlePlane = cPlane;
            circleRadius = p;
            directionInt = dInt;
            uniqueIntermediatePoints = uniquePoints;
            lockValues = nlockValues;
        }

        public bool Calculate() {
            return false;
        }

    }
}
