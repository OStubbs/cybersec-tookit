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

        public static PingReply PingIpOrHost(string ipOrHost)
        {
            // Stuck with default documentation code

            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 10000; // 10 seconds timeout
            PingReply reply = pingSender.Send(ipOrHost, timeout, buffer, options);

            return reply;
        }
        private void Submittion_Click(object sender, RoutedEventArgs e)
        {
            ReportOutput.Text = "";
            PingReply data = PingIpOrHost(ip_address.Text);
            network_status.Content = data.Status.ToString();
            if (data.Status == IPStatus.Success)
            {
                ReportOutput.Text = $"Round trip time: {data.RoundtripTime}\nTime to live: {data.Options.Ttl}\nBuffer size {data.Buffer.Length}";

            }
        }
    }
}
