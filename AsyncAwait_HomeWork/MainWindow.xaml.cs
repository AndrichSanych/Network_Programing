using System;
using System.Collections.Generic;
using System.IO;
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
using Path = System.IO.Path;

namespace AsyncAwait_HomeWork
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
        private async Task ProgressBarUpAsync(int Max)
        {
            CopyProgressBar.Maximum = Max;
            for (int i = 0; i < Max; i++)
            {
                CopyProgressBar.Value++;
                await Task.Delay(100);
            }
        }
        private async void Copy_Button(object sender, RoutedEventArgs e)
        {
            CopyProgressBar.Value = 0;
            string FileNameDest = CopyTo.Text;
            int coutCopies = int.Parse(CountCopies.Text);
            for (int i = 0; i < coutCopies; i++)
            {
                if (!File.Exists(CopyFrom.Text))
                {
                    MessageBox.Show($"File {CopyFrom.Text} not exist!");
                    return;
                }

                string sourceFileName = System.IO.Path.GetFileName(CopyFrom.Text);
                string destinationFileName = Path.Combine(Path.GetDirectoryName(FileNameDest), sourceFileName);

                int counter = 1;

                while (File.Exists(destinationFileName))
                {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(sourceFileName);
                    string fileExtension = Path.GetExtension(sourceFileName);
                    destinationFileName = Path.Combine(Path.GetDirectoryName(FileNameDest), $"{fileNameWithoutExtension}({counter}){fileExtension}");
                    counter++;
                }

                try
                {
                    File.Copy(CopyFrom.Text, destinationFileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error copying file : {ex.Message}");
                }
                await ProgressBarUpAsync(coutCopies);
            }

            // C:\Users\MKA\Source\Repos\Network_Programing\test.txt;
            // C:\Users\MKA\Source\Repos\Network_Programing\copyfile.txt;
        }

    }

}
