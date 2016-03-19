using System;
using System.Runtime.InteropServices;

using System.Net;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Shell;

namespace HotspotMe
{
    public class NetworkTraffic 
    {

        private DateTime _startTime;
        private bool _isNetworkOnline;
        IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
        IPGlobalStatistics ipstat = null;
        Decimal start_r_packets;
        NetworkInterface[] fNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
        long start_received_bytes;
        long start_sent_bytes;

 
        public NetworkTraffic()
        {
            _startTime = DateTime.MinValue;
            ipstat = properties.GetIPv4GlobalStatistics();      

        }
 
 
        [DllImport("wininet.dll")]        
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        public static bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }
 
        public string getConnectionInfo(int _interfaceIndex)
        {
            try
            {
                string myHost = System.Net.Dns.GetHostName();
                IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(myHost);
                IPAddress[] addr = ipEntry.AddressList;
                NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(NetworkChange_NetworkAvailabilityChanged);
                _isNetworkOnline = NetworkInterface.GetIsNetworkAvailable();

                string info="";
 
                if (addr.Length > 0)
                {
                    start_r_packets = Convert.ToDecimal(ipstat.ReceivedPackets);
                    //start_r_packets = Math.Round(start_r_packets / 1048576 * 100000) / 100000;

                    info = "";
                    info += "IP Address: " + addr[addr.Length - 1].ToString() + Environment.NewLine;

                    NetworkInterface adapter = fNetworkInterfaces[_interfaceIndex ];

                    start_received_bytes = fNetworkInterfaces[_interfaceIndex].GetIPv4Statistics().BytesReceived;
                    start_sent_bytes = fNetworkInterfaces[_interfaceIndex].GetIPv4Statistics().BytesSent;                   
 
                    info += Environment.NewLine + "Name: " + adapter.Name + Environment.NewLine;
                    info += Environment.NewLine + "Description: " + adapter.Description + Environment.NewLine;
                    info += Environment.NewLine + "Network Type: " + adapter.NetworkInterfaceType + Environment.NewLine;
                    info += Environment.NewLine + "Speed: " + adapter.Speed / 1000000 + " (Mbps)" + Environment.NewLine;
                    info += Environment.NewLine + "Operational Status: " + adapter.OperationalStatus + Environment.NewLine;
 
                    info += Environment.NewLine + "Received: " + start_received_bytes.ToString() + Environment.NewLine;
                    info += Environment.NewLine + "Sent: " + start_sent_bytes.ToString() + Environment.NewLine;
 
                    start_received_bytes = (start_received_bytes / 1048576 * 100000) / 100000;
                    start_sent_bytes = (start_sent_bytes / 1048576 * 100000) / 100000;
 
                    info += Environment.NewLine + "Received (in MB): " + start_received_bytes.ToString() + Environment.NewLine;
                    info += Environment.NewLine + "Sent (in MB): " + start_sent_bytes.ToString() + Environment.NewLine;
 
                    info += Environment.NewLine + "Is Network Available: " + _isNetworkOnline.ToString() + Environment.NewLine;                    
                    info += Environment.NewLine + "Starting Received Packets: " + start_r_packets.ToString() + Environment.NewLine;
                    info += Environment.NewLine + "Is Network up: " + System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable().ToString() + Environment.NewLine;
                    info += Environment.NewLine + "New method output: " + IsConnectedToInternet().ToString();
                }
                return info;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
 
        protected void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e) 
        { 
            _isNetworkOnline = e.IsAvailable; 
        } 

 
        protected void timerTicker(object sender, EventArgs e)
        {
            var timeSinceStartTime = DateTime.Now - _startTime;
            timeSinceStartTime = new TimeSpan(timeSinceStartTime.Hours,
                                              timeSinceStartTime.Minutes,
                                              timeSinceStartTime.Seconds);                        
            string time = "Time: " + timeSinceStartTime.ToString();
        }
    }
}
