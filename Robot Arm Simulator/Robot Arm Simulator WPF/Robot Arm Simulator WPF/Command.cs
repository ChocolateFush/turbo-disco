using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot_Arm_Simulator_WPF
{
    public enum CommandType {
        Home = 0,
        Move,
        Rotate
    };

    [Serializable]
    public class Command
    {
        public Pose startingPose = new Pose();
        public Transition transition = new Transition();
        public List<Pose> intermediatePoses = new List<Pose>();
        public Pose endingPose = new Pose();
        public CommandType commandType;
        public string commandString = "";
        public string commandName = "";
        public List<List<TreeNode>> allAngleSets = new List<List<TreeNode>>();
        public List<double> scores = new List<double>();
        
        double du = 0.1082;
        double a1 = 0.05608;
        double a2 = 0.25;
        double d4 = 0.2735;
        double d6 = 0.0395;

        public Command(string commandName, CommandType cmdType) {
            commandType = cmdType;
            this.commandName = commandName;
        }

        public void CreateCommandString() {
            commandString = "";

            if (commandType == CommandType.Home) {
                commandString = "H/400/400/400/360/360/360/";
                return;
            }

            if (transition.pathType == PathType.Direct)
            {
                int[] robotPositions = { 0, 0, 0, 0, 0, 0 };
                robotPositions[0] = (int)(endingPose.poseAngles[0] * 9.875) * 400 / 360;
                robotPositions[1] = (int)(endingPose.poseAngles[1] * 5) * 400 / 360;
                robotPositions[2] = (int)(endingPose.poseAngles[2] * 6.25) * 400 / 360;
                robotPositions[3] = (int)endingPose.poseAngles[3];
                robotPositions[4] = (int)endingPose.poseAngles[4];
                robotPositions[5] = (int)endingPose.poseAngles[5];
                commandString += "M/";
                for (int i = 0; i < 6; i++)
                {
                    commandString += robotPositions[i] + "/";
                }
            }
            else if (transition.pathType == PathType.StraightLine)
            {
                for (int i = 0; i < transition.uniqueIntermediatePoints; i++) {
                    Pose currentIntermediatePose = new Pose();
                    for (int j = 0; j < 4; j++) {
                        for (int k = 0; k < 4; k++) {
                            currentIntermediatePose.poseMatrix[j, k] = startingPose.poseMatrix[j, k] + (endingPose.poseMatrix[j, k] - startingPose.poseMatrix[j, k]) * (i + 1) / transition.uniqueIntermediatePoints;
                        }
                    }
                }
            }
            else if (transition.pathType == PathType.Circle) {

            }
        }

        public bool CreateIntermediatePoses() {

            if (transition.pathType == PathType.StraightLine)
            {
                intermediatePoses = new List<Pose>();

                for (int i = 0; i < transition.uniqueIntermediatePoints; i++)
                {
                    intermediatePoses.Add(new Pose());
                }

                if (intermediatePoses.Count == 0) {
                    return true;
                }

                for (int j = 0; j < intermediatePoses[intermediatePoses.Count - 1].poseMatrix.Length; j++)
                {
                    intermediatePoses[intermediatePoses.Count - 1].poseMatrix[j / 4, j % 4] = (endingPose.poseMatrix[j / 4, j % 4] - startingPose.poseMatrix[j / 4, j % 4]) / transition.uniqueIntermediatePoints * (double)(transition.uniqueIntermediatePoints - (1)) + startingPose.poseMatrix[j / 4, j % 4];
                }



                /*

                double sum = 0;
                for (int j = 0; j < 12; j += 4)
                {
                    sum += Math.Pow(intermediatePoses[intermediatePoses.Count - 1].poseMatrix[j / 4, j % 4], 2);
                }

                sum = Math.Sqrt(sum);

                for (int j = 0; j < 12; j += 4)
                {
                    intermediatePoses[intermediatePoses.Count - 1].poseMatrix[j / 4, j % 4] = intermediatePoses[intermediatePoses.Count - 1].poseMatrix[j / 4, j % 4] / sum;
                }

                sum = 0;
                for (int j = 1; j < 12; j += 4)
                {
                    sum += Math.Pow(intermediatePoses[intermediatePoses.Count - 1].poseMatrix[j / 4, j % 4], 2);
                }

                sum = Math.Sqrt(sum);
                for (int j = 1; j < 12; j += 4)
                {
                    intermediatePoses[intermediatePoses.Count - 1].poseMatrix[j / 4, j % 4] = intermediatePoses[intermediatePoses.Count - 1].poseMatrix[j / 4, j % 4] / sum;
                }

                sum = 0;
                for (int j = 2; j < 12; j += 4)
                {
                    sum += Math.Pow(intermediatePoses[intermediatePoses.Count - 1].poseMatrix[j / 4, j % 4], 2);
                }
                sum = Math.Sqrt(sum);

                for (int j = 2; j < 12; j += 4)
                {
                    intermediatePoses[intermediatePoses.Count - 1].poseMatrix[j / 4, j % 4] = intermediatePoses[intermediatePoses.Count - 1].poseMatrix[j / 4, j % 4] / sum;
                }

                */




                InverseKinematics(intermediatePoses[intermediatePoses.Count - 1], endingPose);

                List<double> _scores = new List<double>();

                for (int j = 0; j < allAngleSets.Count; j++)
                {
                    _scores.Add(new double());
                    for (int k = 0; k < allAngleSets[j].Count; k++)
                    {
                        _scores[j] += allAngleSets[j][k].jointAngleValueDeg - endingPose.poseAngles[k];
                    }
                }

                double _min = 100;
                int _minIndex = 0;

                for (int j = 0; j < scores.Count; j++)
                {
                    if (_scores[j] < _min)
                    {
                        _min = scores[j];
                        _minIndex = j;
                    }
                }

                for (int k = 0; k < allAngleSets[_minIndex].Count; k++)
                {
                    intermediatePoses[intermediatePoses.Count - 1].poseAngles[k] = allAngleSets[_minIndex][k].jointAngleValueDeg;
                }

                for (int i = transition.uniqueIntermediatePoints - 2; i >= 0; i--)
                {
                    for (int j = 0; j < intermediatePoses[i].poseMatrix.Length; j++)
                    {
                        intermediatePoses[i].poseMatrix[j / 4, j % 4] = endingPose.poseMatrix[j / 4, j % 4] - (endingPose.poseMatrix[j / 4, j % 4] - startingPose.poseMatrix[j / 4, j % 4]) / transition.uniqueIntermediatePoints * (double)(transition.uniqueIntermediatePoints - (i + 1));
                    }
                    /*

                    double _sum = 0;
                    for (int j = 0; j < 12; j += 4)
                    {
                        _sum += Math.Pow(intermediatePoses[i].poseMatrix[j / 4, j % 4], 2);
                    }

                    _sum = Math.Sqrt(_sum);

                    for (int j = 0; j < 12; j += 4)
                    {
                        intermediatePoses[i].poseMatrix[j / 4, j % 4] = intermediatePoses[i].poseMatrix[j / 4, j % 4] / _sum;
                    }

                    _sum = 0;
                    for (int j = 1; j < 12; j += 4)
                    {
                        _sum += Math.Pow(intermediatePoses[i].poseMatrix[j / 4, j % 4], 2);
                    }

                    _sum = Math.Sqrt(_sum);
                    for (int j = 1; j < 12; j += 4)
                    {
                        intermediatePoses[i].poseMatrix[j / 4, j % 4] = intermediatePoses[i].poseMatrix[j / 4, j % 4] / _sum;
                    }

                    _sum = 0;
                    for (int j = 2; j < 12; j += 4)
                    {
                        _sum += Math.Pow(intermediatePoses[i].poseMatrix[j / 4, j % 4], 2);
                    }
                    _sum = Math.Sqrt(_sum);

                    for (int j = 2; j < 12; j += 4)
                    {
                        intermediatePoses[i].poseMatrix[j / 4, j % 4] = intermediatePoses[i].poseMatrix[j / 4, j % 4] / _sum;
                    }
                    */


                    List<double> scores = new List<double>();


                    if (i == 0)
                    {

                        InverseKinematics(startingPose, intermediatePoses[i]);

                        for (int j = 0; j < allAngleSets.Count; j++)
                        {
                            scores.Add(new double());
                            for (int k = 0; k < allAngleSets[j].Count; k++)
                            {
                                scores[j] += allAngleSets[j][k].jointAngleValueDeg - startingPose.poseAngles[k];
                            }
                        }

                    }
                    else { 

                        InverseKinematics(intermediatePoses[i - 1], intermediatePoses[i]);

                        for (int j = 0; j < allAngleSets.Count; j++)
                        {
                            scores.Add(new double());
                            for (int k = 0; k < allAngleSets[j].Count; k++)
                            {
                                scores[j] += allAngleSets[j][k].jointAngleValueDeg - intermediatePoses[i - 1].poseAngles[k];
                            }
                        }

                    }

                    double min = 100;
                    int minIndex = 0;

                    for (int j = 0; j < scores.Count; j++)
                    {
                        if (scores[j] < min)
                        {
                            min = scores[j];
                            minIndex = j;
                        }
                    }

                    if (allAngleSets.Count <= 0) {
                        return false;
                    }

                    for (int k = 0; k < allAngleSets[minIndex].Count; k++)
                    {
                        intermediatePoses[i].poseAngles[k] = allAngleSets[minIndex][k].jointAngleValueDeg;
                    }
                }
            }
            return true;
        }

        public void InverseKinematics(Pose _startingPose, Pose _endingPose) {

            List<TreeNode> treeNodes = new List<TreeNode>();

            // Creating the first nodes
            double[] oC = { _endingPose.poseMatrix[0, 3] - d6 * _endingPose.poseMatrix[0,2], _endingPose.poseMatrix[1, 3] - d6 * _endingPose.poseMatrix[1, 2], _endingPose.poseMatrix[2, 3] - d6 * _endingPose.poseMatrix[2, 2] };
            treeNodes.Add(new TreeNode(-1, -Math.Atan2(oC[0], oC[1]), 1));
            treeNodes.Add(new TreeNode(-1, -Math.Atan2(oC[0], oC[1]) + Math.PI, 1));
            treeNodes.Add(new TreeNode(-1, -Math.Atan2(oC[0], oC[1]) - Math.PI, 1));

            double[] D = { (Math.Pow((Math.Sqrt(Math.Pow(oC[0], 2) + Math.Pow(oC[1],2)) - a1), 2) + Math.Pow((oC[2] - du), 2) - Math.Pow(a2, 2) - Math.Pow(d4, 2)) / (2 * a2 * d4), (Math.Pow((-Math.Sqrt(Math.Pow(oC[0], 2) + Math.Pow(oC[1], 2)) - a1), 2) + Math.Pow((oC[2] - du), 2) - Math.Pow(a2, 2) - Math.Pow(d4, 2)) / (2 * a2 * d4) };

            for (int i = 0; i < treeNodes.Count; i++) {
                if (treeNodes[i].jointNumber == 1)
                {
                    if (treeNodes[i].jointAngleValueDeg < -180 || treeNodes[i].jointAngleValueDeg > 180 || double.IsNaN(treeNodes[i].jointAngleValueDeg))
                    {
                        treeNodes.RemoveAt(i);
                        i--;
                        continue;
                    }
                }
            }

            int firstLayerTreeNodeCount = treeNodes.Count;

            // For both values in the D array, calculate theta3
            for (int j = 0; j < treeNodes.Count; j++)
            {
                if (treeNodes[j].jointNumber == 1)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        treeNodes.Add(new TreeNode(j, Math.Atan2(Math.Sqrt(1 - Math.Pow(D[i], 2)), D[i]) + Math.PI / 2, 3));
                        treeNodes.Last().angleT = treeNodes.Last().jointAngleValue - Math.PI / 2;
                        treeNodes.Add(new TreeNode(j, Math.Atan2(Math.Sqrt(1 - Math.Pow(D[i], 2)), D[i]) + Math.PI / 2 + Math.PI, 3));
                        treeNodes.Last().angleT = treeNodes.Last().jointAngleValue - Math.PI / 2;
                        treeNodes.Add(new TreeNode(j, Math.Atan2(Math.Sqrt(1 - Math.Pow(D[i], 2)), D[i]) + Math.PI / 2 - Math.PI, 3));
                        treeNodes.Last().angleT = treeNodes.Last().jointAngleValue - Math.PI / 2;
                        treeNodes.Add(new TreeNode(j, Math.Atan2(-Math.Sqrt(1 - Math.Pow(D[i], 2)), D[i]) + Math.PI / 2, 3));
                        treeNodes.Last().angleT = treeNodes.Last().jointAngleValue - Math.PI / 2;
                        treeNodes.Add(new TreeNode(j, Math.Atan2(-Math.Sqrt(1 - Math.Pow(D[i], 2)), D[i]) + Math.PI / 2 + Math.PI, 3));
                        treeNodes.Last().angleT = treeNodes.Last().jointAngleValue - Math.PI / 2;
                        treeNodes.Add(new TreeNode(j, Math.Atan2(-Math.Sqrt(1 - Math.Pow(D[i], 2)), D[i]) + Math.PI / 2 - Math.PI, 3));
                        treeNodes.Last().angleT = treeNodes.Last().jointAngleValue - Math.PI / 2;
                    }
                }
            }
            
            // Cutting Invalid Nodes
            for (int i = 0; i < treeNodes.Count; i++) {
                if (treeNodes[i].jointNumber == 3) {
                    if (treeNodes[i].jointAngleValueDeg < -60.48 || treeNodes[i].jointAngleValueDeg > 240.48 || double.IsNaN(treeNodes[i].jointAngleValueDeg)) {
                        treeNodes.RemoveAt(i);
                        i--;
                        continue;
                    }
                }
            }

            // Finding theta2
            for (int i = 0; i < treeNodes.Count; i++) {
                if (treeNodes[i].jointNumber == 3)
                {
                    treeNodes.Add(new TreeNode(i, Math.Atan2(oC[2] - du, Math.Sqrt(Math.Pow(oC[0], 2) + Math.Pow(oC[1], 2)) - a1) - Math.Atan2(d4 * Math.Sin(treeNodes[i].angleT), a2 + d4 * Math.Cos(treeNodes[i].angleT)) - Math.PI / 2, 2));
                    treeNodes.Last().angleT = treeNodes.Last().jointAngleValue + Math.PI / 2;
                    treeNodes.Add(new TreeNode(i, Math.Atan2(oC[2] - du, Math.Sqrt(Math.Pow(oC[0], 2) + Math.Pow(oC[1], 2)) - a1) - Math.Atan2(d4 * Math.Sin(treeNodes[i].angleT), a2 + d4 * Math.Cos(treeNodes[i].angleT)) - Math.PI / 2 + Math.PI, 2));
                    treeNodes.Last().angleT = treeNodes.Last().jointAngleValue + Math.PI / 2;
                    treeNodes.Add(new TreeNode(i, Math.Atan2(oC[2] - du, Math.Sqrt(Math.Pow(oC[0], 2) + Math.Pow(oC[1], 2)) - a1) - Math.Atan2(d4 * Math.Sin(treeNodes[i].angleT), a2 + d4 * Math.Cos(treeNodes[i].angleT)) - Math.PI / 2 - Math.PI, 2));
                    treeNodes.Last().angleT = treeNodes.Last().jointAngleValue + Math.PI / 2;
                    treeNodes.Add(new TreeNode(i, Math.Atan2(oC[2] - du, -Math.Sqrt(Math.Pow(oC[0], 2) + Math.Pow(oC[1], 2)) + a1) - Math.Atan2(d4 * Math.Sin(treeNodes[i].angleT), a2 + d4 * Math.Cos(treeNodes[i].angleT)) - Math.PI / 2, 2));
                    treeNodes.Last().angleT = treeNodes.Last().jointAngleValue + Math.PI / 2;
                    treeNodes.Add(new TreeNode(i, Math.Atan2(oC[2] - du, -Math.Sqrt(Math.Pow(oC[0], 2) + Math.Pow(oC[1], 2)) + a1) - Math.Atan2(d4 * Math.Sin(treeNodes[i].angleT), a2 + d4 * Math.Cos(treeNodes[i].angleT)) - Math.PI / 2 + Math.PI, 2));
                    treeNodes.Last().angleT = treeNodes.Last().jointAngleValue + Math.PI / 2;
                    treeNodes.Add(new TreeNode(i, Math.Atan2(oC[2] - du, -Math.Sqrt(Math.Pow(oC[0], 2) + Math.Pow(oC[1], 2)) + a1) - Math.Atan2(d4 * Math.Sin(treeNodes[i].angleT), a2 + d4 * Math.Cos(treeNodes[i].angleT)) - Math.PI / 2 - Math.PI, 2));
                    treeNodes.Last().angleT = treeNodes.Last().jointAngleValue + Math.PI / 2;
                    treeNodes.Add(new TreeNode(i, Math.Atan2(oC[2] - du, Math.Sqrt(Math.Pow(oC[0], 2) + Math.Pow(oC[1], 2)) - a1) + Math.Atan2(d4 * Math.Sin(treeNodes[i].angleT), a2 + d4 * Math.Cos(treeNodes[i].angleT)) - Math.PI / 2, 2));
                    treeNodes.Last().angleT = treeNodes.Last().jointAngleValue + Math.PI / 2;
                    treeNodes.Add(new TreeNode(i, Math.Atan2(oC[2] - du, Math.Sqrt(Math.Pow(oC[0], 2) + Math.Pow(oC[1], 2)) - a1) + Math.Atan2(d4 * Math.Sin(treeNodes[i].angleT), a2 + d4 * Math.Cos(treeNodes[i].angleT)) - Math.PI / 2 + Math.PI, 2));
                    treeNodes.Last().angleT = treeNodes.Last().jointAngleValue + Math.PI / 2;
                    treeNodes.Add(new TreeNode(i, Math.Atan2(oC[2] - du, Math.Sqrt(Math.Pow(oC[0], 2) + Math.Pow(oC[1], 2)) - a1) + Math.Atan2(d4 * Math.Sin(treeNodes[i].angleT), a2 + d4 * Math.Cos(treeNodes[i].angleT)) - Math.PI / 2 - Math.PI, 2));
                    treeNodes.Last().angleT = treeNodes.Last().jointAngleValue + Math.PI / 2;
                    treeNodes.Add(new TreeNode(i, Math.Atan2(oC[2] - du, -Math.Sqrt(Math.Pow(oC[0], 2) + Math.Pow(oC[1], 2)) + a1) + Math.Atan2(d4 * Math.Sin(treeNodes[i].angleT), a2 + d4 * Math.Cos(treeNodes[i].angleT)) - Math.PI / 2, 2));
                    treeNodes.Last().angleT = treeNodes.Last().jointAngleValue + Math.PI / 2;
                    treeNodes.Add(new TreeNode(i, Math.Atan2(oC[2] - du, -Math.Sqrt(Math.Pow(oC[0], 2) + Math.Pow(oC[1], 2)) + a1) + Math.Atan2(d4 * Math.Sin(treeNodes[i].angleT), a2 + d4 * Math.Cos(treeNodes[i].angleT)) - Math.PI / 2 + Math.PI, 2));
                    treeNodes.Last().angleT = treeNodes.Last().jointAngleValue + Math.PI / 2;
                    treeNodes.Add(new TreeNode(i, Math.Atan2(oC[2] - du, -Math.Sqrt(Math.Pow(oC[0], 2) + Math.Pow(oC[1], 2)) + a1) + Math.Atan2(d4 * Math.Sin(treeNodes[i].angleT), a2 + d4 * Math.Cos(treeNodes[i].angleT)) - Math.PI / 2 - Math.PI, 2));
                    treeNodes.Last().angleT = treeNodes.Last().jointAngleValue + Math.PI / 2;
                }
            }
            
            // Cutting Invalid Nodes
            for (int i = 0; i < treeNodes.Count; i++) {
                if (treeNodes[i].jointNumber == 2) {
                    if (treeNodes[i].jointAngleValueDeg < -90 || treeNodes[i].jointAngleValueDeg > 30.96 || double.IsNaN(treeNodes[i].jointAngleValueDeg))
                    {
                        treeNodes.RemoveAt(i);
                        i--;
                    }
                }
            }
            
            for (int i = 0; i < treeNodes.Count; i++)
            {
                if (treeNodes[i].jointNumber == 2)
                {
                    double theta2 = treeNodes[i].jointAngleValue;
                    double theta3 = treeNodes[treeNodes[i].parentIndex].jointAngleValue;
                    double theta1 = treeNodes[treeNodes[treeNodes[i].parentIndex].parentIndex].jointAngleValue;

                    double s1 = Math.Sin(theta1);
                    double c1 = Math.Cos(theta1);
                    double s2 = Math.Sin(theta2);
                    double c2 = Math.Cos(theta2);
                    double s3 = Math.Sin(theta3);
                    double c3 = Math.Cos(theta3);

                    double r11 = _endingPose.poseMatrix[0, 0];
                    double r12 = _endingPose.poseMatrix[0, 1];
                    double r13 = _endingPose.poseMatrix[0, 2];
                    double r21 = _endingPose.poseMatrix[1, 0];
                    double r22 = _endingPose.poseMatrix[1, 1];
                    double r23 = _endingPose.poseMatrix[1, 2];
                    double r31 = _endingPose.poseMatrix[2, 0];
                    double r32 = _endingPose.poseMatrix[2, 1];
                    double r33 = _endingPose.poseMatrix[2, 2];

                    double rn33 = (s1*s2*s3-s1*c2*c3)*r13 + (-c1*s2*s3+c1*c2*c3)*r23 + (c2*s3+s2*c3)*r33;

                    double angle = Math.Atan2(Math.Sqrt(1 - Math.Pow(rn33, 2)), rn33);
                    double angleN = Math.Atan2(-Math.Sqrt(1 - Math.Pow(rn33, 2)), rn33);

                    treeNodes.Add(new TreeNode(i, angle, 5));
                    treeNodes.Add(new TreeNode(i, angle + Math.PI, 5));
                    treeNodes.Add(new TreeNode(i, angle - Math.PI, 5));
                    treeNodes.Add(new TreeNode(i, angleN, 5));
                    treeNodes.Add(new TreeNode(i, angleN + Math.PI, 5));
                    treeNodes.Add(new TreeNode(i, angleN - Math.PI, 5));
                    treeNodes.Add(new TreeNode(i, 0, 5));


                }
            }
            
            for (int i = 0; i < treeNodes.Count; i++)
            {
                if (treeNodes[i].jointNumber == 5)
                {
                    if (treeNodes[i].jointAngleValueDeg < -90 || treeNodes[i].jointAngleValueDeg > 90 || double.IsNaN(treeNodes[i].jointAngleValueDeg))
                    {
                        treeNodes.RemoveAt(i);
                        i--;
                        continue;
                    }
                }
            }

            for (int i = 0; i < treeNodes.Count; i++) {
                if (treeNodes[i].jointNumber == 5)
                {
                    double theta2 = treeNodes[treeNodes[i].parentIndex].jointAngleValue;
                    double theta3 = treeNodes[treeNodes[treeNodes[i].parentIndex].parentIndex].jointAngleValue;
                    double theta1 = treeNodes[treeNodes[treeNodes[treeNodes[i].parentIndex].parentIndex].parentIndex].jointAngleValue;

                    double s1 = Math.Sin(theta1);
                    double c1 = Math.Cos(theta1);
                    double s2 = Math.Sin(theta2);
                    double c2 = Math.Cos(theta2);
                    double s3 = Math.Sin(theta3);
                    double c3 = Math.Cos(theta3);

                    double r11 = _endingPose.poseMatrix[0, 0];
                    double r12 = _endingPose.poseMatrix[0, 1];
                    double r13 = _endingPose.poseMatrix[0, 2];
                    double r21 = _endingPose.poseMatrix[1, 0];
                    double r22 = _endingPose.poseMatrix[1, 1];
                    double r23 = _endingPose.poseMatrix[1, 2];
                    double r31 = _endingPose.poseMatrix[2, 0];
                    double r32 = _endingPose.poseMatrix[2, 1];
                    double r33 = _endingPose.poseMatrix[2, 2];

                    double rn13 = (s1*s2*c3+s1*c2*s3)*r13 + (-c1*s2*c3-c1*c2*s3)*r23 + (c2*c3-s2*s3)*r33;
                    double rn23 = (c1) * r13 + (s1) * r23 + (0) * r33;
                    double rn31 = (s1 * s2 * s3 - s1 * c2 * c3) * r11 + (-c1 * s2 * s3 + c1 * c2 * c3) * r21 + (c2 * s3 + s2 * c3) * r31;
                    double rn32 = (s1 * s2 * s3 - s1 * c2 * c3) * r12 + (-c1 * s2 * s3 + c1 * c2 * c3) * r22 + (c2 * s3 + s2 * c3) * r32;
                    double rn12 = (s1 * s2 * c3 + s1 * c2 * s3) * r12 + (-c1 * s2 * c3 - c1 * c2 * s3) * r22 + (c2 * c3 - s2 * s3) * r32;
                    double rn11 = (s1 * s2 * c3 + s1 * c2 * s3) * r11 + (-c1 * s2 * c3 - c1 * c2 * s3) * r21 + (c2 * c3 - s2 * s3) * r31;
                    double rn21 = (c1) * r11 + (s1) * r21 + (0) * r31;

                    if (treeNodes[i].jointAngleValue == 0)
                    {
                        double angle = Math.Atan2(rn11, -rn12) - Math.PI/2;

                        treeNodes.Add(new TreeNode(i, _startingPose.poseAngles[3], 4));
                        treeNodes.Add(new TreeNode(treeNodes.Count - 1, angle - _startingPose.poseAngles[3], 6));
                        treeNodes.Add(new TreeNode(i, _startingPose.poseAngles[3], 4));
                        treeNodes.Add(new TreeNode(treeNodes.Count - 1, angle - _startingPose.poseAngles[3] - Math.PI, 6));
                        treeNodes.Add(new TreeNode(i, _startingPose.poseAngles[3], 4));
                        treeNodes.Add(new TreeNode(treeNodes.Count - 1, angle - _startingPose.poseAngles[3] + Math.PI, 6));
                    }
                    else if (Math.Sin(treeNodes[i].jointAngleValue) > 0)
                    {
                        double angle4 = Math.Atan2(rn23,rn13);
                        double angle6 = Math.Atan2(rn32, - rn31);

                        treeNodes.Add(new TreeNode(i, angle4, 4));
                        treeNodes.Add(new TreeNode(treeNodes.Count - 1, angle6, 6));
                        treeNodes.Add(new TreeNode(i, angle4+Math.PI, 4));
                        treeNodes.Add(new TreeNode(treeNodes.Count - 1, angle6, 6));
                        treeNodes.Add(new TreeNode(i, angle4 + Math.PI, 4));
                        treeNodes.Add(new TreeNode(treeNodes.Count - 1, angle6 + Math.PI, 6));
                        treeNodes.Add(new TreeNode(i, angle4, 4));
                        treeNodes.Add(new TreeNode(treeNodes.Count - 1, angle6 + Math.PI, 6));
                        treeNodes.Add(new TreeNode(i, angle4 - Math.PI, 4));
                        treeNodes.Add(new TreeNode(treeNodes.Count - 1, angle6, 6));
                        treeNodes.Add(new TreeNode(i, angle4, 4));
                        treeNodes.Add(new TreeNode(treeNodes.Count - 1, angle6 - Math.PI, 6));
                        treeNodes.Add(new TreeNode(i, angle4 - Math.PI, 4));
                        treeNodes.Add(new TreeNode(treeNodes.Count - 1, angle6 - Math.PI, 6));
                        treeNodes.Add(new TreeNode(i, angle4 - Math.PI, 4));
                        treeNodes.Add(new TreeNode(treeNodes.Count - 1, angle6 + Math.PI, 6));
                        treeNodes.Add(new TreeNode(i, angle4 + Math.PI, 4));
                        treeNodes.Add(new TreeNode(treeNodes.Count - 1, angle6 - Math.PI, 6));
                    }
                    else if (Math.Sin(treeNodes[i].jointAngleValue) < 0)
                    {
                        double angle4 = Math.Atan2(-rn23, -rn13);
                        double angle6 = Math.Atan2(-rn32, rn31);

                        treeNodes.Add(new TreeNode(i, angle4, 4));
                        treeNodes.Add(new TreeNode(treeNodes.Count - 1, angle6, 6));
                        treeNodes.Add(new TreeNode(i, angle4 + Math.PI, 4));
                        treeNodes.Add(new TreeNode(treeNodes.Count - 1, angle6, 6));
                        treeNodes.Add(new TreeNode(i, angle4 + Math.PI, 4));
                        treeNodes.Add(new TreeNode(treeNodes.Count - 1, angle6 + Math.PI, 6));
                        treeNodes.Add(new TreeNode(i, angle4, 4));
                        treeNodes.Add(new TreeNode(treeNodes.Count - 1, angle6 + Math.PI, 6));
                        treeNodes.Add(new TreeNode(i, angle4 - Math.PI, 4));
                        treeNodes.Add(new TreeNode(treeNodes.Count - 1, angle6, 6));
                        treeNodes.Add(new TreeNode(i, angle4, 4));
                        treeNodes.Add(new TreeNode(treeNodes.Count - 1, angle6 - Math.PI, 6));
                        treeNodes.Add(new TreeNode(i, angle4 - Math.PI, 4));
                        treeNodes.Add(new TreeNode(treeNodes.Count - 1, angle6 - Math.PI, 6));
                        treeNodes.Add(new TreeNode(i, angle4 - Math.PI, 4));
                        treeNodes.Add(new TreeNode(treeNodes.Count - 1, angle6 + Math.PI, 6));
                        treeNodes.Add(new TreeNode(i, angle4 + Math.PI, 4));
                        treeNodes.Add(new TreeNode(treeNodes.Count - 1, angle6 - Math.PI, 6));
                    }
                }
            }
            
            allAngleSets = new List<List<TreeNode>>();
            int angleSetsCount = 0;

            for (int i = 0; i < treeNodes.Count; i++) {
                if (treeNodes[i].jointNumber == 6)
                {
                    TreeNode joint6 = treeNodes[i];
                    TreeNode joint4 = treeNodes[joint6.parentIndex];
                    TreeNode joint5 = treeNodes[joint4.parentIndex];
                    TreeNode joint2 = treeNodes[joint5.parentIndex];
                    TreeNode joint3 = treeNodes[joint2.parentIndex];
                    TreeNode joint1 = treeNodes[joint3.parentIndex];
                    
                    if (joint6.jointAngleValueDeg > 90 || joint6.jointAngleValueDeg < -90)
                    {
                        continue;
                    }
                    if (joint5.jointAngleValueDeg > 90 || joint5.jointAngleValueDeg < -90)
                    {
                        continue;
                    }
                    if (joint4.jointAngleValueDeg > 90 || joint4.jointAngleValueDeg < -90)
                    {
                        continue;
                    }
                    if (joint3.jointAngleValueDeg > 240.48 || joint3.jointAngleValueDeg < -60.48)
                    {
                        continue;
                    }
                    if (joint2.jointAngleValueDeg > 30.96 || joint2.jointAngleValueDeg < -90)
                    {
                        continue;
                    }

                    List<TreeNode> tempList = new List<TreeNode>();

                    tempList.Add(new TreeNode(joint1.parentIndex, joint1.jointAngleValue, joint1.jointNumber));
                    tempList.Add(new TreeNode(joint2.parentIndex, joint2.jointAngleValue, joint2.jointNumber));
                    tempList.Add(new TreeNode(joint3.parentIndex, joint3.jointAngleValue, joint3.jointNumber));
                    tempList.Add(new TreeNode(joint4.parentIndex, joint4.jointAngleValue, joint4.jointNumber));
                    tempList.Add(new TreeNode(joint5.parentIndex, joint5.jointAngleValue, joint5.jointNumber));
                    tempList.Add(new TreeNode(joint6.parentIndex, joint6.jointAngleValue, joint6.jointNumber));

                    for (int j = 0; j < 6; j++) {
                        tempList[j].jointAngleValueDeg = tempList[j].jointAngleValue * 180 / Math.PI;
                    }
                    
                    allAngleSets.Add(tempList);
                    angleSetsCount++;
                }
            }
            
            scores = new List<double>();

            for (int i = 0; i < allAngleSets.Count; i++) {
                
                double[] result = new double[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                scores.Add(new double());
                scores[i] = 0;
                
                ForwardKinematics(result, allAngleSets[i]);

                for (int j = 3; j < 16; j+=4)
                {
                    scores[i] += Math.Abs(result[j] - _endingPose.poseMatrix[j/4, j%4]);
                }

                if (scores[i] > 0.001)
                {
                    scores.RemoveAt(i);
                    allAngleSets.RemoveAt(i);
                    i--;
                    continue;
                }
                scores[i] = 0;

                for (int j = 0; j < result.Length; j++) {
                    if (((j / 4) < 3) && ((j % 4) < 3))
                    {
                        scores[i] += Math.Abs(result[j] - _endingPose.poseMatrix[j / 4, j % 4]);
                    }
                }

                if (scores[i] > 0.001)
                {
                    allAngleSets.RemoveAt(i);
                    scores.RemoveAt(i);
                    i--;
                    continue;
                }


            }

        }
        
        public void ForwardKinematics(double[] output, List<TreeNode> angleSet) {

            double s23 = Math.Sin(angleSet[1].jointAngleValue + angleSet[2].jointAngleValue);
            double c23 = Math.Cos(angleSet[1].jointAngleValue + angleSet[2].jointAngleValue);
            double s1 = Math.Sin(angleSet[0].jointAngleValue);
            double s2 = Math.Sin(angleSet[1].jointAngleValue);
            double s3 = Math.Sin(angleSet[2].jointAngleValue);
            double s4 = Math.Sin(angleSet[3].jointAngleValue);
            double s5 = Math.Sin(angleSet[4].jointAngleValue);
            double s6 = Math.Sin(angleSet[5].jointAngleValue);
            double c1 = Math.Cos(angleSet[0].jointAngleValue);
            double c2 = Math.Cos(angleSet[1].jointAngleValue);
            double c3 = Math.Cos(angleSet[2].jointAngleValue);
            double c4 = Math.Cos(angleSet[3].jointAngleValue);
            double c5 = Math.Cos(angleSet[4].jointAngleValue);
            double c6 = Math.Cos(angleSet[5].jointAngleValue);
            double[] result = new double[16] { (((((-s1)*(-s2))*(c3)+((-s1)*(-c2))*(s3))*(c4)+(c1)*(s4))*(c5)+((((-s1)*(-s2))*(s3)+((-s1)*(-c2))*(-c3))*(-1))*(s5))*(c6)+((((-s1)*(-s2))*(c3)+((-s1)*(-c2))*(s3))*(-s4)+(c1)*(c4))*(s6),

                                            (((((-s1)*(-s2))*(c3)+((-s1)*(-c2))*(s3))*(c4)+(c1)*(s4))*(c5)+((((-s1)*(-s2))*(s3)+((-s1)*(-c2))*(-c3))*(-1))*(s5))*(-s6)+((((-s1)*(-s2))*(c3)+((-s1)*(-c2))*(s3))*(-s4)+(c1)*(c4))*(c6),

                                            ((((-s1)*(-s2))*(c3)+((-s1)*(-c2))*(s3))*(c4)+(c1)*(s4))*(s5)+((((-s1)*(-s2))*(s3)+((-s1)*(-c2))*(-c3))*(-1))*(-c5),

                                            (((((-s1)*(-s2))*(c3)+((-s1)*(-c2))*(s3))*(c4)+(c1)*(s4))*(s5)+((((-s1)*(-s2))*(s3)+((-s1)*(-c2))*(-c3))*(-1))*(-c5))*(d6)+(((-s1)*(-s2))*(s3)+((-s1)*(-c2))*(-c3))*(d4)+(-s1)*(-a2*s2)-a1*s1,


                                            (((((c1)*(-s2))*(c3)+((c1)*(-c2))*(s3))*(c4)+(s1)*(s4))*(c5)+((((c1)*(-s2))*(s3)+((c1)*(-c2))*(-c3))*(-1))*(s5))*(c6) + ((((c1) * (-s2)) * (c3) + ((c1) * (-c2)) * (s3)) * (-s4) + (s1) *(c4)) * (s6),

                                            (((((c1) * (-s2)) * (c3) + ((c1) * (-c2)) * (s3)) * (c4) + (s1) * (s4)) * (c5) + ((((c1) * (-s2)) * (s3) + ((c1)*(-c2))*(-c3))*(-1))*(s5))*(-s6) + ((((c1) * (-s2)) * (c3) + ((c1) * (-c2)) * (s3)) * (-s4) + (s1)* (c4)) * (c6),

                                            ((((c1) * (-s2)) * (c3) + ((c1) * (-c2)) * (s3)) * (c4) + (s1) * (s4)) * (s5) + ((((c1) * (-s2)) * (s3) + ((c1) * (-c2)) * (-c3)) * (-1)) * (-c5),

                                            (((((c1) * (-s2)) * (c3) + ((c1) * (-c2)) * (s3)) * (c4) + (s1) * (s4)) * (s5) + ((((c1) * (-s2)) * (s3) + ((c1) * (-c2))*(-c3))*(-1))*(-c5))*(d6) + (((c1) * (-s2)) * (s3) + ((c1) * (-c2)) * (-c3)) * (d4) + (c1) *(-a2 * s2) + a1 * c1,


                                              ((((c2) * (c3) + (-s2) * (s3)) * (c4)) * (c5) + (((c2) * (s3) + (-s2) * (-c3)) * (-1)) * (s5)) * (c6) + (((c2)* (c3) + (-s2) * (s3)) * (-s4)) * (s6),

                                              ((((c2) * (c3) + (-s2) * (s3)) * (c4)) * (c5) + (((c2) * (s3) + (-s2) * (-c3)) * (-1)) * (s5)) * (-s6) + (((c2) * (c3) + (-s2) * (s3)) * (-s4)) * (c6),

                                              (((c2) * (c3) + (-s2) * (s3)) * (c4)) * (s5) + (((c2) * (s3) + (-s2) * (-c3)) * (-1)) * (-c5),

                                              ((((c2) * (c3) + (-s2) * (s3)) * (c4)) * (s5) + (((c2) * (s3) + (-s2) * (-c3)) * (-1)) * (-c5)) * (d6) + ((c2)* (s3) + (-s2) * (-c3)) * (d4) + a2 * c2 + du,


                                            0,

                                            0,

                                            0,

                                            1};

            double sum = 0;
            for (int j = 0; j < 12; j += 4)
            {
                sum += Math.Pow(result[j],2);
            }

            sum = Math.Sqrt(sum);

            for (int j = 0; j < 12; j += 4)
            {
                result[j] = result[j] / sum;
            }

            sum = 0;
            for (int j = 1; j < 12; j += 4)
            {
                sum += Math.Pow(result[j], 2);
            }

            sum = Math.Sqrt(sum);
            for (int j = 1; j < 12; j += 4)
            {
                result[j] = result[j] / sum;
            }

            sum = 0;
            for (int j = 2; j < 12; j += 4)
            {
                sum += Math.Pow(result[j], 2);
            }
            sum = Math.Sqrt(sum);

            for (int j = 2; j < 12; j += 4)
            {
                result[j] = result[j] / sum;
            }

            for (int j = 0; j < 16; j++) {
                output[j] = result[j];
            }

        }

    }
}
