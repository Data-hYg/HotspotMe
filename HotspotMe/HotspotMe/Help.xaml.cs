using FirstFloor.ModernUI.Windows.Controls;
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

namespace HotspotMe
{
    /// <summary>
    /// Interaction logic for Help.xaml
    /// </summary>
    public partial class Help : ModernDialog
    {
        public Help()
        {
            InitializeComponent();

            // define the dialog buttons
            this.Buttons = new Button[] { this.OkButton };
            string text = "<b>ENCRYPTION</b> \n\nYour hotspot is WPA2 encrypted, \nstill I would recommend a password combination out of letters/signs/numbers for maximum safety. \n\n" +
            "<b>SELECT NETWORKADAPTER</b> \n\nThis setting simply changes the networkadapter which is displayed in the Mainwindow monitor. \n\n" +
            "<b>ADMINISTRATOR</b> \n\nTo set up a local hotspot windwos requiers adminrights. Nothing gets installed or similar.";
            txt_helpdialog.Text = text;
        }
    }
}
