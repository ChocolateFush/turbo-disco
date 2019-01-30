using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Robot_Arm_Simulator_WPF
{
    /// <summary>
    /// Interaction logic for CommandConfigurationWindow.xaml
    /// </summary>
    public partial class CommandConfigurationWindow : Window
    {
        public Command command;
        public bool changed = false;
        public bool loaded = false;
        Timer animationTimer = new Timer(100);

        public CommandConfigurationWindow(Command command)
        {
            InitializeComponent();
            this.command = command;

            LoadCommandData();

            if (pathTypeComboBox.SelectedIndex == 2)
            {
                circleDirectionComboBox.IsEnabled = true;
                circlePlaneComboBox.IsEnabled = true;
                circleRadiusUpDown.IsEnabled = true;
            }
            else
            {
                circleDirectionComboBox.IsEnabled = false;
                circlePlaneComboBox.IsEnabled = false;
                circleRadiusUpDown.IsEnabled = false;
            }
            loaded = true;
            
            MainWindow mainWindow = (Robot_Arm_Simulator_WPF.MainWindow)Application.Current.MainWindow;
            mainWindow.joint0.Value = double.Parse(joint0.Text);
            mainWindow.joint1.Value = double.Parse(joint1.Text);
            mainWindow.joint2.Value = double.Parse(joint2.Text);
            mainWindow.joint3.Value = double.Parse(joint3.Text);
            mainWindow.joint4.Value = double.Parse(joint4.Text);
            mainWindow.joint5.Value = double.Parse(joint5.Text);

            animationTimer.Elapsed += AnimationTimer_Elapsed;
        }

        int animationCounter = 0;

        private void AnimationTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => { 
            joint0.Text = command.intermediatePoses[animationCounter].poseAngles[0].ToString();
            joint1.Text = command.intermediatePoses[animationCounter].poseAngles[1].ToString();
            joint2.Text = command.intermediatePoses[animationCounter].poseAngles[2].ToString();
            joint3.Text = command.intermediatePoses[animationCounter].poseAngles[3].ToString();
            joint4.Text = command.intermediatePoses[animationCounter].poseAngles[4].ToString();
            joint5.Text = command.intermediatePoses[animationCounter].poseAngles[5].ToString();
            animationCounter++;
            if (animationCounter >= command.intermediatePoses.Count) {
                joint0.Text = command.endingPose.poseAngles[0].ToString();
                joint1.Text = command.endingPose.poseAngles[1].ToString();
                joint2.Text = command.endingPose.poseAngles[2].ToString();
                joint3.Text = command.endingPose.poseAngles[3].ToString();
                joint4.Text = command.endingPose.poseAngles[4].ToString();
                joint5.Text = command.endingPose.poseAngles[5].ToString();
                animationCounter = 0;
                animationTimer.Stop();
            }

            }));
        }

        private void LoadCommandData()
        {
            for (int i = 0; i < currentPoseMatrixUniformGrid.Children.Count; i++)
            {
                ((TextBox)currentPoseMatrixUniformGrid.Children[i]).Text = command.startingPose.poseMatrix[i / 4, i % 4].ToString();
                ((TextBox)currentPoseMatrixUniformGrid.Children[i]).IsEnabled = false;
            }

            for (int i = 0; i < currentPoseAnglesUniformGrid.Children.Count; i++)
            {
                ((TextBox)((Grid)currentPoseAnglesUniformGrid.Children[i]).Children[1]).Text = command.startingPose.poseAngles[i].ToString();
                ((TextBox)((Grid)currentPoseAnglesUniformGrid.Children[i]).Children[1]).IsEnabled = false;
            }

            for (int i = 0; i < targetPoseMatrixUniformGrid.Children.Count; i++)
            {
                ((TextBox)targetPoseMatrixUniformGrid.Children[i]).Text = command.endingPose.poseMatrix[i / 4, i % 4].ToString();
            }

            for (int i = 0; i < targetPoseAnglesUniformGrid.Children.Count; i++)
            {
                ((TextBox)((Grid)targetPoseAnglesUniformGrid.Children[i]).Children[1]).Text = command.endingPose.poseAngles[i].ToString();
            }

            switch (command.transition.pathType)
            {
                case PathType.Direct:
                    pathTypeComboBox.SelectedIndex = 0;
                    break;
                case PathType.StraightLine:
                    pathTypeComboBox.SelectedIndex = 1;
                    break;
                case PathType.Circle:
                    pathTypeComboBox.SelectedIndex = 2;
                    break;
                default:
                    pathTypeComboBox.SelectedIndex = 0;
                    break;
            }

            speedUpDown.Value = command.transition.speed;
            noOfPointsUpDown.Value = command.transition.uniqueIntermediatePoints;
            xLockCheckBox.IsChecked = command.transition.lockValues[0];
            yLockCheckBox.IsChecked = command.transition.lockValues[1];
            zLockCheckBox.IsChecked = command.transition.lockValues[2];
            oLockCheckBox.IsChecked = command.transition.lockValues[3];

            switch (command.transition.circlePlane)
            {
                case CirclePlane.XY:
                    circlePlaneComboBox.SelectedIndex = 0;
                    break;
                case CirclePlane.XZ:
                    circlePlaneComboBox.SelectedIndex = 1;
                    break;
                case CirclePlane.YZ:
                    circlePlaneComboBox.SelectedIndex = 2;
                    break;
                default:
                    circlePlaneComboBox.SelectedIndex = 1;
                    break;
            }

            circleRadiusUpDown.Value = command.transition.circleRadius;

            circleDirectionComboBox.SelectedIndex = command.transition.directionInt;
            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pathTypeComboBox.SelectedIndex == 2)
            {
                circleDirectionComboBox.IsEnabled = true;
                circlePlaneComboBox.IsEnabled = true;
                circleRadiusUpDown.IsEnabled = true;

            }
            else
            {
                circleDirectionComboBox.IsEnabled = false;
                circlePlaneComboBox.IsEnabled = false;
                circleRadiusUpDown.IsEnabled = false;
            }

            if (pathTypeComboBox.SelectedIndex == 2)
            {
                command.transition.pathType = PathType.Circle;
            }
            else if (pathTypeComboBox.SelectedIndex == 1)
            {
                command.transition.pathType = PathType.StraightLine;
            }
            else {
                command.transition.pathType = PathType.Direct;
            }
        }

        private void xLockCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ((TextBox)targetPoseMatrixUniformGrid.Children[3]).IsEnabled = false;
            command.endingPose.poseMatrix[0, 3] = command.startingPose.poseMatrix[0, 3];
            ((TextBox)targetPoseMatrixUniformGrid.Children[3]).Text = command.endingPose.poseMatrix[0,3].ToString();
        }

        private void xLockCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ((TextBox)targetPoseMatrixUniformGrid.Children[3]).IsEnabled = true;
        }

        private void yLockCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ((TextBox)targetPoseMatrixUniformGrid.Children[7]).IsEnabled = false;
            command.endingPose.poseMatrix[1, 3] = command.startingPose.poseMatrix[1, 3];
            ((TextBox)targetPoseMatrixUniformGrid.Children[7]).Text = command.endingPose.poseMatrix[1,3].ToString();
        }

        private void yLockCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ((TextBox)targetPoseMatrixUniformGrid.Children[7]).IsEnabled = true;
        }

        private void zLockCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ((TextBox)targetPoseMatrixUniformGrid.Children[11]).IsEnabled = false;
            command.endingPose.poseMatrix[2, 3] = command.startingPose.poseMatrix[2, 3];
            ((TextBox)targetPoseMatrixUniformGrid.Children[11]).Text = command.endingPose.poseMatrix[2, 3].ToString();
        }

        private void zLockCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ((TextBox)targetPoseMatrixUniformGrid.Children[11]).IsEnabled = true;
        }

        private void oLockCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    ((TextBox)targetPoseMatrixUniformGrid.Children[i*4+j]).IsEnabled = false;
                    command.endingPose.poseMatrix[i, j] = command.startingPose.poseMatrix[i, j];
                    ((TextBox)targetPoseMatrixUniformGrid.Children[i * 4 + j]).Text = command.endingPose.poseMatrix[i, j].ToString();
                }
            }
        }

        private void oLockCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    ((TextBox)targetPoseMatrixUniformGrid.Children[i * 4 + j]).IsEnabled = true;
                    
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < currentPoseMatrixUniformGrid.Children.Count; i++)
            {
                command.startingPose.poseMatrix[i / 4, i % 4] = Double.Parse(((TextBox)currentPoseMatrixUniformGrid.Children[i]).Text);
            }

            for (int i = 0; i < currentPoseAnglesUniformGrid.Children.Count; i++)
            {
                command.startingPose.poseAngles[i] = Double.Parse(((TextBox)((Grid)currentPoseAnglesUniformGrid.Children[i]).Children[1]).Text);
            }

            for (int i = 0; i < targetPoseMatrixUniformGrid.Children.Count; i++)
            {
                command.endingPose.poseMatrix[i / 4, i % 4] = Double.Parse(((TextBox)targetPoseMatrixUniformGrid.Children[i]).Text);
            }

            for (int i = 0; i < targetPoseAnglesUniformGrid.Children.Count; i++)
            {
                command.endingPose.poseAngles[i] = Double.Parse(((TextBox)((Grid)targetPoseAnglesUniformGrid.Children[i]).Children[1]).Text);
            }

            switch (pathTypeComboBox.SelectedIndex)
            {
                case 0:
                    command.transition.pathType = PathType.Direct;
                    break;
                case 1:
                    command.transition.pathType = PathType.StraightLine;
                    break;
                case 2:
                    command.transition.pathType = PathType.Circle;
                    break;
                default:
                    command.transition.pathType = PathType.Direct;
                    break;
            }

            command.transition.speed = (double)speedUpDown.Value;
            command.transition.uniqueIntermediatePoints = (int)noOfPointsUpDown.Value;
            command.transition.lockValues[0] = (bool)xLockCheckBox.IsChecked;
            command.transition.lockValues[1] = (bool)yLockCheckBox.IsChecked;
            command.transition.lockValues[2] = (bool)zLockCheckBox.IsChecked;
            command.transition.lockValues[3] = (bool)oLockCheckBox.IsChecked;

            switch (circlePlaneComboBox.SelectedIndex)
            {
                case 0:
                    command.transition.circlePlane = CirclePlane.XY;
                    break;
                case 1:
                    command.transition.circlePlane = CirclePlane.XZ;
                    break;
                case 2:
                    command.transition.circlePlane = CirclePlane.YZ;
                    break;
                default:
                    command.transition.circlePlane = CirclePlane.XY;
                    break;
            }

            command.transition.circleRadius = (double)circleRadiusUpDown.Value;

            command.transition.directionInt = circleDirectionComboBox.SelectedIndex;

            MainWindow mainWindow = (Robot_Arm_Simulator_WPF.MainWindow)Application.Current.MainWindow;
            mainWindow.robotProgram.changed = true;

            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainWindow mainWindow = (Robot_Arm_Simulator_WPF.MainWindow)Application.Current.MainWindow;
            double jointValue = 0;
            if (loaded && double.TryParse(joint0.Text, out jointValue) 
                        && double.TryParse(joint1.Text, out jointValue) 
                            && double.TryParse(joint2.Text, out jointValue) 
                                && double.TryParse(joint3.Text, out jointValue) 
                                    && double.TryParse(joint4.Text, out jointValue) 
                                        && double.TryParse(joint5.Text, out jointValue))
            {
                mainWindow.joint0.Value = double.Parse(joint0.Text);
                mainWindow.joint1.Value = double.Parse(joint1.Text);
                mainWindow.joint2.Value = double.Parse(joint2.Text);
                mainWindow.joint3.Value = double.Parse(joint3.Text);
                mainWindow.joint4.Value = double.Parse(joint4.Text);
                mainWindow.joint5.Value = double.Parse(joint5.Text);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            command.InverseKinematics(command.startingPose, command.endingPose);
            targetJointConfigurationComboBox.Items.Clear();
            for (int i = 0; i < command.allAngleSets.Count; i++) {
                targetJointConfigurationComboBox.Items.Add(new ComboBoxItem());
                ((ComboBoxItem)targetJointConfigurationComboBox.Items[i]).Content = i.ToString() + " - " + command.scores[i];
            }

        }

        private void targetJointConfigurationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex < 0) {
                return;
            }

            for (int i = 0; i < 6; i++) {
                command.endingPose.poseAngles[i] = command.allAngleSets[int.Parse((((ComboBox)sender).SelectedIndex).ToString())][i].jointAngleValueDeg;
            }

            joint0.Text = command.allAngleSets[int.Parse((((ComboBox)sender).SelectedIndex).ToString())][0].jointAngleValueDeg.ToString();
            joint1.Text = command.allAngleSets[int.Parse((((ComboBox)sender).SelectedIndex).ToString())][1].jointAngleValueDeg.ToString();
            joint2.Text = command.allAngleSets[int.Parse((((ComboBox)sender).SelectedIndex).ToString())][2].jointAngleValueDeg.ToString();
            joint3.Text = command.allAngleSets[int.Parse((((ComboBox)sender).SelectedIndex).ToString())][3].jointAngleValueDeg.ToString();
            joint4.Text = command.allAngleSets[int.Parse((((ComboBox)sender).SelectedIndex).ToString())][4].jointAngleValueDeg.ToString();
            joint5.Text = command.allAngleSets[int.Parse((((ComboBox)sender).SelectedIndex).ToString())][5].jointAngleValueDeg.ToString();

            command.intermediatePoses.Clear();

            if (command.transition.pathType != PathType.Direct)
            {
                if (command.CreateIntermediatePoses())
                {
                    animationTimer.Start();
                }
                else {
                    MessageBox.Show("This path is not possible!", "Path creation error");
                }
            }
        }
        
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (!loaded)
            {
                return;
            }
            double result = 0;
            for (int i = 0; i < 16; i++) {
                if (double.TryParse(((TextBox)((UniformGrid)((TextBox)sender).Parent).Children[i]).Text, out result)){
                    command.endingPose.poseMatrix[i / 4, i % 4] = double.Parse(((TextBox)((UniformGrid)((TextBox)sender).Parent).Children[i]).Text);
                }
            }
        }

        private void noOfPointsUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            int value = 0;

            if (int.TryParse(noOfPointsUpDown.Text, out value)) {
                command.transition.uniqueIntermediatePoints = int.Parse(noOfPointsUpDown.Text);
            }
        }

        private void speedUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            int value = 0;
            if (int.TryParse(speedUpDown.Value.ToString(), out value)) {
                value = int.Parse(speedUpDown.Value.ToString());
                if (value <= 0) {
                    return;
                }
                animationTimer.Interval = 10 * value;
            }
        }
    }
}
