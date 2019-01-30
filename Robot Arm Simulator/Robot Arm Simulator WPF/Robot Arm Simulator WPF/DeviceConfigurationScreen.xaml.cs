using System;
using System.Collections.Generic;
using System.IO.Ports;
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
    /// Interaction logic for DeviceConfigurationScreen.xaml
    /// </summary>
    public partial class DeviceConfigurationScreen : Window
    {
        public DeviceConfigurationScreen()
        {
            InitializeComponent();

            RefreshCOMList();
        }

        List<string> allPorts;
        public string chosenComPort;
        public bool comChosen = false;

        public void RefreshCOMList() {

            allPorts = new List<string>();

            comPortListView.Items.Clear();

            foreach (string port in SerialPort.GetPortNames()) {
                allPorts.Add(port);
                comPortListView.Items.Add(new ListViewItem());
                ((ListViewItem)comPortListView.Items[comPortListView.Items.Count - 1]).Content = allPorts[comPortListView.Items.Count - 1];
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RefreshCOMList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void comPortListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            chosenComPort = allPorts[comPortListView.SelectedIndex];
            ((ListViewItem)comPortListView.Items[comPortListView.Items.Count - 1]).Content += " - Selected!";
            comChosen = true;
        }
    }
}
