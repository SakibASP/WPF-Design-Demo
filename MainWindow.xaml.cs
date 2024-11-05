using Newtonsoft.Json;
using SocialBladeDashboard.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SocialBladeDashboard
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Layout.Children.Clear();
            Layout.Children.Add(new Dashboard());
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void DemoTest(object sender, RoutedEventArgs e)
        {
            Layout.Children.Clear();
            Layout.Children.Add(new Demo());
        }
    }
}