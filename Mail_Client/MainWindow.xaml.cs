using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
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

namespace Mail_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string username = "andrich0105@gmail.com";
        const string password = "ckobjhqnfuzduebl";

        private ImapClient client = new();
        public MainWindow()
        {
            InitializeComponent();

            client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
            
            client.Authenticate(username, password);

            foreach (var fl in client.GetFolders(client.PersonalNamespaces[0]))
            {
                folderList.Items.Add(fl.Name);
                
            }
            
          
        }

        private void folderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var folder = client.GetFolders(client.PersonalNamespaces[0])[folderList.SelectedIndex];
            

          

            folder.Open(FolderAccess.ReadWrite);
            IList<UniqueId> uids = folder.Search(SearchQuery.All);

            Console.WriteLine($"--------{folder.Name} Mailbox:");
            foreach (var i in uids)
            {
                MimeMessage message = folder.GetMessage(i);
                MessageBox.Show (message.Subject);
            }



        }
    }
}
