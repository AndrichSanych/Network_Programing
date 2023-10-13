using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace HTTP_Protocol_WebClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WebClient client;
        string Address;
        string Category;
        string ImageHeight;
        string ImageWidth;
        string FilePath;
        bool pathIsOK = false;
        public MainWindow()
        {
            InitializeComponent();

            client = new WebClient();

            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;

        }
        private void Download_Click(object sender, RoutedEventArgs e)
        {
            Category = CategoryTBox.Text;
            ImageHeight = ImageHeightTBox.Text;
            ImageWidth = ImageWidthTBox.Text;
            FilePath = PathTBox.Text;

            if (client.IsBusy)
            {
                MessageBox.Show("Web Client is busy! Try agin later...");
                return;
            }

            Address = $"https://source.unsplash.com/random/{ImageWidth}x{ImageHeight}/?{Category}";
            DownloadFileAsync(Address);

        }
        private async void DownloadFileAsync(string address)
        {
           
            string destination = FilePath;

            
            try
            {
                await client.DownloadFileTaskAsync(address, destination); 
                pathIsOK = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (pathIsOK)
            {
                MessageBox.Show("Download complete!");
                DownloadPBar.Value = 0;
            }
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (pathIsOK)
                DownloadPBar.Value = e.ProgressPercentage;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }

}
