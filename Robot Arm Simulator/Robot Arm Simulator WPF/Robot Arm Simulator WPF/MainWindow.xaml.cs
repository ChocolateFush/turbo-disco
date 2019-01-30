// Large sections of this code have been directly taken from Github user Gabryxx7
// Some modifications were made to make it work on this model

using HelixToolkit.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace Robot_Arm_Simulator_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    class Joint {
        public Model3D model = null;
        public double angle = 0;
        public double angleMin = -180;
        public double angleMax = 180;
        public double rotPointX = 0;
        public double rotPointY = 0;
        public double rotPointZ = 0;
        public double rotAxisX = 0;
        public double rotAxisY = 0;
        public double rotAxisZ = 0;

        public Joint(Model3D pModel) {
            model = pModel;
        }
    }
    public partial class MainWindow : Window
    {
        #region GlobalVariables
        Model3DGroup robotArm;
        public RobotProgram robotProgram = new RobotProgram();
        public List<Transition> transitions = new List<Transition>();
        Model3D baseWaistModel;
        Model3D waistShoulderModel;
        Model3D shoulderElbowModel;
        Model3D elbowWrist4Model;
        Model3D wrist4Wrist5Model;
        Model3D wrist5Wrist6Model;
        ModelVisual3D visual3D = new ModelVisual3D();
        List<Joint> joints = new List<Joint>();
        Transform3DGroup F0;
        Transform3DGroup F1;
        Transform3DGroup F2;
        Transform3DGroup F3;
        Transform3DGroup F4;
        Transform3DGroup F5;
        Transform3DGroup B0 = new Transform3DGroup();
        Transform3DGroup B1 = new Transform3DGroup();
        Transform3DGroup B2 = new Transform3DGroup();
        Transform3DGroup B3 = new Transform3DGroup();
        Transform3DGroup B4 = new Transform3DGroup();
        Transform3DGroup B5 = new Transform3DGroup();
        RotateTransform3D R1;
        RotateTransform3D R2;
        RotateTransform3D R3;
        RotateTransform3D R4;
        RotateTransform3D R5;
        TranslateTransform3D T;
        bool fileLoaded = true;

        #endregion

        public MainWindow()
        {
            InitializeComponent();

            InitialiseSimulator();

            InitialiseFileDisplay();

            simulatorTimer.Elapsed += SimulatorTimer_Elapsed;


            serialPort = new SerialPort();
            serialPort.BaudRate = 115200;

        }


        private void InitialiseFileDisplay() {

            fileListView.Items.Clear();

            DirectoryInfo dirInfo = new DirectoryInfo(@"...\\Robot Programs\\");
            FileInfo[] info = dirInfo.GetFiles("*.rpg");
            foreach (FileInfo f in info) {
                fileListView.Items.Add(f.Name);
            }

        }

        private void RefreshCommandDisplay() {
            commandListView.Items.Clear();
            if (fileLoaded) { 
                foreach(Command command in robotProgram.commands) {
                    commandListView.Items.Add(command.commandName);
                }
            }
        }

        private void InitialiseSimulator() {
            
            ModelImporter importer = new ModelImporter();

            robotArm = new Model3DGroup();

            Material material = new DiffuseMaterial(new SolidColorBrush(Colors.Blue));

            importer.DefaultMaterial = material;

            baseWaistModel = importer.Load(@"Links/Base sub Assembly.STL");
            waistShoulderModel = importer.Load(@"Links/Waist sub Assembly.STL");
            shoulderElbowModel = importer.Load(@"Links/Shoulder to Elbow Sub Assembly.STL");
            elbowWrist4Model = importer.Load(@"Links/Elbow To Wrist Sub Assembly.STL");
            wrist4Wrist5Model = importer.Load(@"Links/Wrist4-5 Sub Assembly.STL");
            wrist5Wrist6Model = importer.Load(@"Links/Wrist5-6 Sub Assembly.STL");

            robotArm.Children.Add(baseWaistModel);
            robotArm.Children.Add(waistShoulderModel);
            robotArm.Children.Add(shoulderElbowModel);
            robotArm.Children.Add(elbowWrist4Model);
            robotArm.Children.Add(wrist4Wrist5Model);
            robotArm.Children.Add(wrist5Wrist6Model);

            InitialiseModelTransformations();

            joints.Add(new Joint(baseWaistModel));
            joints.Add(new Joint(waistShoulderModel));
            joints.Add(new Joint(shoulderElbowModel));
            joints.Add(new Joint(elbowWrist4Model));
            joints.Add(new Joint(wrist4Wrist5Model));
            joints.Add(new Joint(wrist5Wrist6Model));

            InitialiseJoints();

            joint0.Value = 0;
            joint1.Value = 0;
            joint2.Value = 0;
            joint3.Value = 0;
            joint4.Value = 0;
            joint5.Value = 0;

            joint0.ValueChanged += joint_ValueChanged;
            joint1.ValueChanged += joint_ValueChanged;
            joint2.ValueChanged += joint_ValueChanged;
            joint3.ValueChanged += joint_ValueChanged;
            joint4.ValueChanged += joint_ValueChanged;
            joint5.ValueChanged += joint_ValueChanged;
        }

        List<List<string>> endEffectorMatrix = new List<List<string>>();
        
        private void InitialiseModelTransformations() {

            TranslateTransform3D baseWaistTransform3D = new TranslateTransform3D(-100, -100, 0);
            baseWaistModel.Transform = baseWaistTransform3D;
            B0.Children.Add(baseWaistTransform3D);
            
            RotateTransform3D waistShoulderRotateTransform3D = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90));
            B1.Children.Add(waistShoulderRotateTransform3D);
            TranslateTransform3D waistShoulderTranslateTransform3D = new TranslateTransform3D(-67, 96.08, 47);
            B1.Children.Add(waistShoulderTranslateTransform3D);
            waistShoulderModel.Transform = B1;
            
            RotateTransform3D shoulderElbowRotateTransform3D = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 90));
            B2.Children.Add(shoulderElbowRotateTransform3D);
            RotateTransform3D shoulderElbowRotateTransform3D2 = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), 180));
            B2.Children.Add(shoulderElbowRotateTransform3D2);
            TranslateTransform3D shoulderElbowTranslateTransform3D = new TranslateTransform3D(54, 88.38, 389);
            B2.Children.Add(shoulderElbowTranslateTransform3D);
            shoulderElbowModel.Transform = B2;
            
            RotateTransform3D elbowWrist4RotateTransform3D = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 90));
            B3.Children.Add(elbowWrist4RotateTransform3D);
            RotateTransform3D elbowWrist4RotateTransform3D2 = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90));
            B3.Children.Add(elbowWrist4RotateTransform3D2);
            TranslateTransform3D elbowWrist4TranslateTransform3D = new TranslateTransform3D(-39, 26.08, 329);
            B3.Children.Add(elbowWrist4TranslateTransform3D);
            elbowWrist4Model.Transform = B3;
            
            RotateTransform3D wrist4Wrist5RotateTransform3D = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), -90));
            B4.Children.Add(wrist4Wrist5RotateTransform3D);
            RotateTransform3D wrist4Wrist5RotateTransform3D2 = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), -90));
            B4.Children.Add(wrist4Wrist5RotateTransform3D2);
            TranslateTransform3D wrist4Wrist5TranslateTransform3D = new TranslateTransform3D(-66.67, 227.58, 329);
            B4.Children.Add(wrist4Wrist5TranslateTransform3D);
            wrist4Wrist5Model.Transform = B4;
            
            RotateTransform3D wrist5Wrist6RotateTransform3D2 = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90));
            B5.Children.Add(wrist5Wrist6RotateTransform3D2);
            TranslateTransform3D wrist5Wrist6TranslateTransform3D = new TranslateTransform3D(-21.12, 368.86, 332.75);
            B5.Children.Add(wrist5Wrist6TranslateTransform3D);
            wrist5Wrist6Model.Transform = B5;
            
            visual3D.Content = robotArm;

            m_helix_viewport.Children.Add(visual3D);

            DataContext = this;
        }

        private void InitialiseJoints()
        {
            joints[0].angleMin = -180;
            joints[0].angleMax = 180;
            joints[0].rotAxisX = 0;
            joints[0].rotAxisY = 0;
            joints[0].rotAxisZ = 1;
            joints[0].rotPointX = 0;
            joints[0].rotPointY = 0;
            joints[0].rotPointZ = 109;

            joints[1].angleMin = -90;
            joints[1].angleMax = 35;
            joints[1].rotAxisX = 1;
            joints[1].rotAxisY = 0;
            joints[1].rotAxisZ = 0;
            joints[1].rotPointX = 0;
            joints[1].rotPointY = 56.08;
            joints[1].rotPointZ = 109;

            joints[2].angleMin = -60;
            joints[2].angleMax = 230;
            joints[2].rotAxisX = 1;
            joints[2].rotAxisY = 0;
            joints[2].rotAxisZ = 0;
            joints[2].rotPointX = 0;
            joints[2].rotPointY = 56.08;
            joints[2].rotPointZ = 359;

            joints[3].angleMin = -90;
            joints[3].angleMax = 90;
            joints[3].rotAxisX = 0;
            joints[3].rotAxisY = 1;
            joints[3].rotAxisZ = 0;
            joints[3].rotPointX = 0;
            joints[3].rotPointY = 229.58;
            joints[3].rotPointZ = 359;

            joints[4].angleMin = -90;
            joints[4].angleMax = 90;
            joints[4].rotAxisX = 1;
            joints[4].rotAxisY = 0;
            joints[4].rotAxisZ = 0;
            joints[4].rotPointX = 0;
            joints[4].rotPointY = 329.58;
            joints[4].rotPointZ = 359;

            joints[5].angleMin = -90;
            joints[5].angleMax = 90;
            joints[5].rotAxisX = 0;
            joints[5].rotAxisY = 1;
            joints[5].rotAxisZ = 0;
            joints[5].rotPointX = 0;
            joints[5].rotPointY = 329.58;
            joints[5].rotPointZ = 359;
        }

        private void joint_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            joints[0].angle = (double)joint0.Value;
            joints[1].angle = (double)joint1.Value;
            joints[2].angle = (double)joint2.Value;
            joints[3].angle = (double)joint3.Value;
            joints[4].angle = (double)joint4.Value;
            joints[5].angle = (double)joint5.Value;
            execute_fk(true);
        }

        private void execute_fk(bool show) {
            double[] angles = { joints[0].angle, joints[1].angle, joints[2].angle, joints[3].angle, joints[4].angle, joints[5].angle };
            ForwardKinematics(angles, show);
        }

        public Vector3D ForwardKinematics(double[] angles, bool show) {

            RotateTransform3D rotateTransform3DOld = new RotateTransform3D();

            F0 = new Transform3DGroup();
            F0.Children.Add(B0);

            F1 = new Transform3DGroup();
            R1 = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[0].rotAxisX, joints[0].rotAxisY, joints[0].rotAxisZ), angles[0]), new Point3D(joints[0].rotAxisX, joints[0].rotAxisY, joints[0].rotAxisZ));
            F1.Children.Add(B1);
            F1.Children.Add(R1);


            F2 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R2 = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[1].rotAxisX, joints[1].rotAxisY, joints[1].rotAxisZ), angles[1]), new Point3D(joints[1].rotPointX, joints[1].rotPointY, joints[1].rotPointZ));
            F2.Children.Add(B2);
            F2.Children.Add(R2);
            F2.Children.Add(R1);

            F3 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R3 = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[2].rotAxisX, joints[2].rotAxisY, joints[2].rotAxisZ), angles[2]), new Point3D(joints[2].rotPointX, joints[2].rotPointY, joints[2].rotPointZ));
            F3.Children.Add(B3);
            F3.Children.Add(R3);
            F3.Children.Add(R2);
            F3.Children.Add(R1);

            F4 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R4 = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[3].rotAxisX, joints[3].rotAxisY, joints[3].rotAxisZ), angles[3]), new Point3D(joints[3].rotPointX, joints[3].rotPointY, joints[3].rotPointZ));
            F4.Children.Add(B4);
            F4.Children.Add(R4);
            F4.Children.Add(R3);
            F4.Children.Add(R2);
            F4.Children.Add(R1);

            F5 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R5 = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[4].rotAxisX, joints[4].rotAxisY, joints[4].rotAxisZ), angles[4]), new Point3D(joints[4].rotPointX, joints[4].rotPointY, joints[4].rotPointZ));
            F5.Children.Add(B5);
            F5.Children.Add(R5);
            F5.Children.Add(R4);
            F5.Children.Add(R3);
            F5.Children.Add(R2);
            F5.Children.Add(R1);

            /*
            F6 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[5].rotAxisX, joints[5].rotAxisY, joints[5].rotAxisZ), angles[5]), new Point3D(joints[5].rotPointX, joints[5].rotPointY, joints[5].rotPointZ));
            F6.Children.Add(T);
            F6.Children.Add(R);
            F6.Children.Add(F5);
            F6.Children.Add(B6);*/

            if (show)
            {
                joints[0].model.Transform = F0;
                joints[1].model.Transform = F1;
                joints[2].model.Transform = F2;
                joints[3].model.Transform = F3;
                joints[4].model.Transform = F4;
                joints[5].model.Transform = F5;
            }

            return new Vector3D(joints[5].model.Bounds.Location.X, joints[5].model.Bounds.Location.Y, joints[5].model.Bounds.Location.Z);
            
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you would like to delete " + fileListView.Items.GetItemAt(fileListView.SelectedIndex).ToString() + "?", "Delete " + fileListView.Items.GetItemAt(fileListView.SelectedIndex).ToString() + "?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes) {
                File.Delete(@"...\\Robot Programs\\" + fileListView.Items.GetItemAt(fileListView.SelectedIndex).ToString());
            }
            InitialiseFileDisplay();
            fileLoaded = false;
            RefreshCommandDisplay();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Save changes to " + robotProgram.fileName + "?", "Save " + robotProgram.fileName + "?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                robotProgram.SaveProgram();
                robotProgram.changed = false;
            }
        }

        private void fileListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (robotProgram.changed) {
                saveButton_Click(sender, e);
            }

            string fileName = fileListView.Items.GetItemAt(fileListView.SelectedIndex).ToString();

            robotProgram = new RobotProgram();

            robotProgram.LoadProgram(@"...\\Robot Programs\\" + fileName, fileName);

            if (robotProgram.commands == null || robotProgram.commands.Count == 0) {
                robotProgram.AddCommand(0, CommandType.Home);
            }

            fileLoaded = true;
            RefreshCommandDisplay();
        }

        private void addAboveButton_Click(object sender, RoutedEventArgs e)
        {
            CommandTemplateWindow cmdTemplateWindow = new CommandTemplateWindow();
            cmdTemplateWindow.ShowDialog();

            if (!cmdTemplateWindow.changed) {
                return;
            }

            robotProgram.AddCommand(commandListView.SelectedIndex, cmdTemplateWindow.commandTemplateType);

            RefreshCommandDisplay();
            
        }

        private void addBelowButton_Click(object sender, RoutedEventArgs e)
        {
            CommandTemplateWindow cmdTemplateWindow = new CommandTemplateWindow();
            cmdTemplateWindow.ShowDialog();

            if (!cmdTemplateWindow.changed)
            {
                return;
            }
            
            robotProgram.AddCommand(commandListView.SelectedIndex+1, cmdTemplateWindow.commandTemplateType);

            RefreshCommandDisplay();
        }

        private void deleteCommandButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you would like to delete " + commandListView.Items.GetItemAt(commandListView.SelectedIndex).ToString() + "?", "Delete " + commandListView.Items.GetItemAt(commandListView.SelectedIndex).ToString() + "?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes) {
                robotProgram.RemoveCommand(commandListView.SelectedIndex);
                RefreshCommandDisplay();
            }
        }

        private void newFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Robot ProGram|*.rpg";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.InitialDirectory = System.IO.Path.GetFullPath( @"...\\Robot Programs");
            if (saveFileDialog.ShowDialog() == true) {

                robotProgram = new RobotProgram();
                robotProgram.filePath = saveFileDialog.FileName;
                robotProgram.SaveProgram();
                InitialiseFileDisplay();
            }
        }

        private void renameButton_Click(object sender, RoutedEventArgs e)
        {
            RenameWindow renameWindow = new RenameWindow(fileListView.Items[fileListView.SelectedIndex].ToString());
            renameWindow.ShowDialog();

            if (renameWindow.change) {

                if (!renameWindow.newFileName.Contains(".rpg")) {
                    renameWindow.newFileName += ".rpg";
                }

                File.Delete(@"...\\Robot Programs\\" + fileListView.Items.GetItemAt(fileListView.SelectedIndex).ToString());
                robotProgram.filePath = @"...\\Robot Programs\\" + renameWindow.newFileName;
                robotProgram.fileName = renameWindow.newFileName;
                robotProgram.SaveProgram();
                InitialiseFileDisplay();
            }

        }

        CommandConfigurationWindow commandConfigWindow = null;
        public bool optionsWindowOpen = false;
        private void commandListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = commandListView.SelectedIndex;

            if (optionsWindowOpen == false && index >= 0)
            {
                commandConfigWindow = new CommandConfigurationWindow(robotProgram.commands[index]);
                commandConfigWindow.Owner = this;
                optionsWindowOpen = true;
                commandConfigWindow.Closing += new System.ComponentModel.CancelEventHandler(ResetCommandWindow);
                commandConfigWindow.Show();
            }
        }

        public void ResetCommandWindow(object sender, System.ComponentModel.CancelEventArgs e) {
            optionsWindowOpen = false;
        }

        Timer simulatorTimer = new Timer();

        private void runSimulatorButton_Click(object sender, RoutedEventArgs e)
        {
            simulatorTimer.Interval = simulationTimerInterval;
            commandCounter = 0;
            simulatorTimer.Start();
            
        }
        int commandCounter = 0;
        int intermediatePoseCounter = 0;
        bool startingPoseRead = false;
        bool endingPoseRead = false;
        int simulationTimerInterval = 100;
        int directCalculationSteps = 0;

        private void SimulatorTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => {
            if (!startingPoseRead && !endingPoseRead)
            {
                simulatorTimer.Interval = 10 * (int)robotProgram.commands[commandCounter].transition.speed;
                commandListView.SelectedIndex = commandCounter;
                    
            }
        
            if (!startingPoseRead)
            {
                for (int i = 0; i < 6; i++)
                {
                    joints[i].angle = robotProgram.commands[commandCounter].startingPose.poseAngles[i];
                }
                execute_fk(true);
                startingPoseRead = true;
            }
            else if (startingPoseRead && directCalculationSteps < 25 && (robotProgram.commands[commandCounter].transition.pathType == PathType.Direct))
            {
                for (int i = 0; i < 6; i++)
                {
                    joints[i].angle = (((robotProgram.commands[commandCounter].endingPose.poseAngles[i] - robotProgram.commands[commandCounter].startingPose.poseAngles[i]) * directCalculationSteps / 25.0) + robotProgram.commands[commandCounter].startingPose.poseAngles[i]);
                }
                execute_fk(true);
                directCalculationSteps++;
            }
            else if ((startingPoseRead && intermediatePoseCounter < robotProgram.commands[commandCounter].intermediatePoses.Count) && (robotProgram.commands[commandCounter].transition.pathType != PathType.Direct)) {

                for (int i = 0; i < 6; i++)
                {
                    joints[i].angle = robotProgram.commands[commandCounter].intermediatePoses[intermediatePoseCounter].poseAngles[i];
                }

                execute_fk(true);
                intermediatePoseCounter++;
                        

            }
            else if (!endingPoseRead)
            {
                for (int i = 0; i < 6; i++)
                {
                    joints[i].angle = robotProgram.commands[commandCounter].endingPose.poseAngles[i];
                }
                execute_fk(true);
                endingPoseRead = true;
            }

            if (startingPoseRead && endingPoseRead)
            {
                startingPoseRead = false;
                endingPoseRead = false;
                commandCounter++;
                directCalculationSteps = 0;
                intermediatePoseCounter = 0;
                if (commandCounter >= (robotProgram.commands.Count))
                {
                    simulatorTimer.Stop();
                    commandCounter = 0;
                    return;
                }
            }
            }));

        }

        SerialPort serialPort;

        private void devicesButton_Click(object sender, RoutedEventArgs e)
        {
            DeviceConfigurationScreen dcs = new DeviceConfigurationScreen();

            dcs.ShowDialog();

            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }

            if (dcs.comChosen)
            {
                serialPort = new SerialPort();
                serialPort.PortName = dcs.chosenComPort;
                serialPort.BaudRate = 115200;
                serialPort.RtsEnable = true;
                serialPort.DtrEnable = true;
                serialPort.Handshake = Handshake.None;
            }
            serialPort.Open();

            serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);

        }
        
        int commandsSentCounter = 0;

        Timer readingTimer = new Timer(100);

        private void runRobotButton_Click(object sender, RoutedEventArgs e)
        {
            commandCounter = 0;
            intermediatePoseCounter = 0;
            commandsSentCounter = 0;
            RunRobot();

        }

        private void RunRobot() {

            double[] motorScalingFactor = new double[6] { 9.875 * 400.0 / 360.0, 5.0 * 400.0 / 360.0, 6.0 * 400.0 / 360.0, 1.0, 1.0, 1.0 };
            double[] angleOffsets = new double[6] { 0.0, 0.0, 0.0, 90.0, 90.0, 90.0 };

            string commandString = "";

            for (; commandCounter < robotProgram.commands.Count; commandCounter++)
            {
                while (!endingPoseRead)
                {
                    if (robotProgram.commands[commandCounter].commandType == CommandType.Home)
                    {
                        commandString = "H/400/400/400/360/360/360/";

                        serialPort.WriteLine(commandString);
                        endingPoseRead = true;
                        return;
                    }

                    commandString += "M/";
                    if ((intermediatePoseCounter < robotProgram.commands[commandCounter].intermediatePoses.Count) && (robotProgram.commands[commandCounter].transition.pathType != PathType.Direct))
                    {

                        for (int i = 0; i < 6; i++)
                        {
                            commandString += ((int)(robotProgram.commands[commandCounter].intermediatePoses[intermediatePoseCounter].poseAngles[i] * motorScalingFactor[i] + angleOffsets[i])).ToString() + "/";
                        }
                        intermediatePoseCounter++;

                    }
                    else if (!endingPoseRead)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            commandString += ((int)(robotProgram.commands[commandCounter].endingPose.poseAngles[i] * motorScalingFactor[i] + angleOffsets[i])).ToString() + "/";
                        }
                        endingPoseRead = true;
                    }

                    commandString += ((int)robotProgram.commands[commandCounter].transition.speed).ToString() + "/";


                    commandsSentCounter++;

                    if ((commandsSentCounter) >= 19)
                    {
                        serialPort.WriteLine(commandString);
                        commandsSentCounter = 0;
                        return;
                    }

                }

                if (commandsSentCounter >= 19)
                {
                    serialPort.WriteLine(commandString);
                    commandsSentCounter = 0;
                    return;
                }
                endingPoseRead = false;
                intermediatePoseCounter = 0;
            }

            commandString += "M/0/0/0/90/90/90/10/";
            
            serialPort.WriteLine(commandString);
        }

        int commandDoneCounter = 0;
        string messageLine = "";
        
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            messageLine += serialPort.ReadLine();
            
            if (messageLine.Contains("Load more"))
            {
                commandDoneCounter++;
                messageLine = "";
            }

            if (commandDoneCounter >= 1)
            {
                commandDoneCounter = 0;
                if (commandCounter < robotProgram.commands.Count)
                {
                    RunRobot();
                }
                else {
                    commandCounter = 0;
                }
            }
        }

    }
}
