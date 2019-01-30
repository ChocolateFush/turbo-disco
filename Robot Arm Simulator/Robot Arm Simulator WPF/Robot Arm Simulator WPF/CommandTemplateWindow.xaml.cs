using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Robot_Arm_Simulator_WPF
{
    /// <summary>
    /// Interaction logic for CommandTemplateWindow.xaml
    /// </summary>
    public partial class CommandTemplateWindow : Window
    {
        public CommandType commandTemplateType = CommandType.Home;
        public bool changed = false;
        public CommandTemplateWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            changed = true;
            string chosenTemplate = ((Button)sender).Content.ToString();
            if (chosenTemplate == "Home")
            {
                commandTemplateType = CommandType.Home;
            }
            else if (chosenTemplate == "Move")
            {
                commandTemplateType = CommandType.Move;
            }
            else if (chosenTemplate == "Rotate")
            {
                commandTemplateType = CommandType.Move;
            }
            this.Close();
        }
    }
}
