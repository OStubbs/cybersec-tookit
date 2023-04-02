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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Markup;
using System.Net;

namespace cs_toolkit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PingCompletedCallback(object sender, PingCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                //e.Error.ToString()

                // Let the main thread resume. 
                ((AutoResetEvent)e.UserState).Set();
            }

            Update_Report(e.Reply, "ping");

            // Let the main thread resume.
            ((AutoResetEvent)e.UserState).Set();
        }

        private void Submittion_Click(object sender, RoutedEventArgs e)
        {
            ReportOutput.Text = "";
            //PingIpOrHost(ip_address.Text);
            AutoResetEvent waiter = new AutoResetEvent(false);
            IPAddress ip = IPAddress.Parse(ip_address.Text);
            var pingSender = new Ping();

            pingSender.PingCompleted += PingCompletedCallback;
            pingSender.SendAsync(ip, 1000, waiter);
        }
        
        private void Update_Report(object o, string expected)
        {
            switch (expected)
            {
                case "ping":
                    PingReply data = (PingReply)o;
                    network_status.Content = data.Status.ToString();
                    if (data.Status == IPStatus.Success)
                    {
                        ReportOutput.Text = $"Round trip time: {data.RoundtripTime}\nTime to live: {data.Options.Ttl}\nBuffer size {data.Buffer.Length}";

                    }
                    break;
                default:
                    break;
            }
        }
    }
}
