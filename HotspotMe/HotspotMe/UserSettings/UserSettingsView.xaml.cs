using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
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

namespace HotspotMe.UserSettings
{
    /// <summary>
    /// Interaction logic for ModernWindow1.xaml
    /// </summary>
    public partial class UserSettingsView : ModernWindow
    {
        NetworkInterface[] fNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
        List<string> _descriptions = new List<string>();
        int networkIndex;

        public UserSettingsView()
        {

            InitializeComponent();

            if(Properties.Settings.Default.Theme == "dark")
                AppearanceManager.Current.ThemeSource = AppearanceManager.DarkThemeSource;
            if (Properties.Settings.Default.dontShowAgain == true)
                cBox_showDialog.IsChecked = false;
            else
                cBox_showDialog.IsChecked = true;

            setNetworkDescriptions(fNetworkInterfaces);
            cb_NetworkInterfaces.ItemsSource = _descriptions;
        }

        #region windowhandler
        private void ModernWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            this.Left = mainWindow.Left + (mainWindow.Width - this.ActualWidth) / 2;
            this.Top = mainWindow.Top + (mainWindow.Height - this.ActualHeight) / 2;
        }

        private void ModernWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion 

        #region save/cancel
        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            if (cb_NetworkInterfaces.SelectedItem != null)
            {
                GetterSetter gs = new GetterSetter();
                gs.networkindex = networkIndex;
            }
            if (cBox_showDialog.IsChecked == true)
            {
                Properties.Settings.Default.dontShowAgain = false;
            }

            this.Close();
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Theme changed
        private void rb_dark_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Theme = "dark";
            AppearanceManager.Current.ThemeSource = AppearanceManager.DarkThemeSource;
        }

        private void rb_light_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Theme = "light";
            AppearanceManager.Current.ThemeSource = AppearanceManager.LightThemeSource;

        }
        #endregion

        private void networkadapterLink(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void setNetworkDescriptions(NetworkInterface[] _netWorkInterfaces)
        {
            for (int i = 0; i < _netWorkInterfaces.Length; i++)
            {
                _descriptions.Add(_netWorkInterfaces[i].Description);
            }

        }

        private void cb_NetworkInterfaces_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            networkIndex = (sender as ComboBox).SelectedIndex;
        }


    }
}
