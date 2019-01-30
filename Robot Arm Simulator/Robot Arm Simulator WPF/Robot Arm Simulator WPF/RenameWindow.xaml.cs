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
    /// Interaction logic for RenameWindow.xaml
    /// </summary>
    public partial class RenameWindow : Window
    {
        public string newFileName;
        public bool change = false;

        public RenameWindow(string fileName)
        {
            InitializeComponent();

            fileNameTextBox.Text = fileName;
            fileNameTextBox.Focus();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            newFileName = fileNameTextBox.Text;
            change = true;
            this.Close();
        }
    }
}
