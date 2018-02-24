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

namespace lwf
{
    public partial class MainWindow : Window
    {
        static String programName = "Local WLAN";
        static String programVersion = "1.0";
        static String aboutMsg = programName
                + " \nVersion: " + programVersion
                + " \nPublic License: WTFPL v2"
                + " \nhttps://github.com/AlexIII/lwf";
        Image wImg;
        BitmapImage onIcon;
        BitmapImage offIcon;
        bool isOn = false;
        WlanControl wCon;

        public MainWindow()
        {
            InitializeComponent();
            this.Title = programName + " v." + programVersion;
            offIcon = new BitmapImage(new Uri(@"woff.png", UriKind.Relative));
            onIcon = new BitmapImage(new Uri(@"won.png", UriKind.Relative));
            wCon = new WlanControl();
            ssidBox.Text = (String)Properties.Settings.Default["lastSSID"];
            keyBox.Text = (String)Properties.Settings.Default["lastKey"];
        }

        private void offUi()
        {
            statusLabel.Content = "OFFLINE";
            ssidBox.IsEnabled = true;
            keyBox.IsEnabled = true;
            wImg.Source = offIcon;
            InvalidateVisual();
            isOn = false;
        }

        private void onUi()
        {
            statusLabel.Content = "ONLINE";
            ssidBox.IsEnabled = false;
            keyBox.IsEnabled = false;
            wImg.Source = onIcon;
            InvalidateVisual();
            isOn = true;
        }

        private bool checkInput(String ssid, String key)
        {
            try
            {
                if (ssid.Any(x => Char.IsWhiteSpace(x)) ||
                        key.Any(x => Char.IsWhiteSpace(x)))
                    throw new System.ArgumentException("Spaces are not allowed in SSID or key");
                if (ssid.Length == 0)
                    throw new System.ArgumentException("Enter the SSID");
                if (key.Length < 8)
                    throw new System.ArgumentException("The Key must be 8 symbols at least");
            }
            catch (System.ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void onOffButton_Click(object sender, RoutedEventArgs e)
        {
            String ssid = ssidBox.Text;
            String key = keyBox.Text;
            if (!checkInput(ssid, key)) return;

            try
            {
                if (isOn)
                {
                    wCon.off();
                    offUi();
                }
                else
                {
                    Properties.Settings.Default["lastSSID"] = ssid;
                    Properties.Settings.Default["lastKey"] = key;
                    Properties.Settings.Default.Save();
                    wCon.on(ssid, key);
                    onUi();
                }
            }
            catch (System.ArgumentException ex)
            {
                MessageBox.Show("Check if your Wi-Fi is On\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void onOffButton_Loaded(object sender, RoutedEventArgs e)
        {
            wImg = (Image)onOffButton.Template.FindName("wImg", onOffButton);
            offUi();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                wCon.off();
            }
            catch { }
        }

        private void aboutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(aboutMsg, "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}