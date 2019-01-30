using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot_Arm_Simulator_WPF
{
    public class RobotProgram
    {
        public List<Command> commands = new List<Command>();
        public string filePath;
        public bool changed = false;
        public string fileName;

        public RobotProgram() {

        }

        public void LoadProgram(string filePathLocal, string fileName) {

            this.fileName = fileName;
            filePath = filePathLocal;
            commands = JsonConvert.DeserializeObject<List<Command>>(File.ReadAllText(filePath));
        }

        public void SaveProgram() {
            File.WriteAllText(filePath, JsonConvert.SerializeObject(commands));
        }

        public void RemoveCommand(int removeIndex) {
            changed = true;
            commands.RemoveAt(removeIndex);
        }

        public void AddCommand(int position, CommandType commandType)
        {

            string commandName = "";

            if (commandType == CommandType.Move)
            {

                RenameWindow nameWindow = new RenameWindow("");
                nameWindow.ShowDialog();

                if (nameWindow.change)
                {
                    commandName = nameWindow.newFileName;
                }
            }
            else {
                commandName = "Home";
            }

            changed = true;

            if (position < 0) {
                position = 0;
            }

            commands.Insert(position, new Command(commandName, commandType));
            
            switch (commandType)
            {
                case CommandType.Home:

                    if (position > 0) {
                        commands[position].startingPose = commands[position - 1].endingPose;
                    }

                    break;
                default:
                    if (position > 0)
                    {
                        commands[position].startingPose = commands[position - 1].endingPose;
                    }
                    else
                    {
                        commands[position].startingPose = new Pose();
                    }

                    commands[position].transition = new Transition();

                    if (position > 0)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            commands[position].endingPose.poseMatrix[i/4,i%4] = commands[position - 1].endingPose.poseMatrix[i / 4, i % 4];
                        }
                    }
                    else
                    {
                        commands[position].endingPose = new Pose();
                        for (int i = 0; i < 16; i++)
                        {
                            commands[position].endingPose.poseMatrix[i / 4, i % 4] = commands[position].startingPose.poseMatrix[i / 4, i % 4];
                        }
                    }
                    break;
            }
           
        }
        
    }
}
