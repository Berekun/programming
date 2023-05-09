using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Reflection.Metadata;
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

namespace Mikender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<User> users = new List<User>();
        int idClientToRemove = 0;
        public MainWindow()
        {
            InitializeComponent();
            users = Connection.SearchUser("", 0, 10);
            ListViewProducts.ItemsSource = users;
        }

        public void Mostrar_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            User user = (User)b.DataContext;
            ShowInfoPage pageShowMain = new ShowInfoPage();
            pageShowMain.DataContext = user;
            Frame.Content = pageShowMain;
        }

        public void closeButton_Click(object sender, RoutedEventArgs e)
        {
           Close();
        }

        public void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string content = tb.Text;
            users = Connection.SearchUser(content, 0, 10);
            ListViewProducts.ItemsSource = users;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Add.Visibility = Visibility.Hidden;
            Confirm.Visibility = Visibility.Visible;
            Cancel.Visibility = Visibility.Visible;
            var pageAddMain = new AddUserPage();
            Frame.Content = pageAddMain;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            Confirm.Visibility = Visibility.Hidden;
            Cancel.Visibility = Visibility.Hidden;

            var content = Frame.Content;
            if (content is AddUserPage)
            {
                var page = (AddUserPage)content;
                User user = page.User;
                Connection.AddUser(user);
            }
            else if (idClientToRemove != 0)
            {
                Connection.RemoveUser(idClientToRemove);
                Add.Visibility = Visibility.Visible;
            }
            if (content is UpdateUserPage)
            {
                var page = (UpdateUserPage)content;
                User user = page.user;
                Connection.UpdateUser(user);
                Add.Visibility = Visibility.Visible;
            }

            Frame.Content = null;
            Add.Visibility = Visibility.Visible;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = null;
            Confirm.Visibility = Visibility.Hidden;
            Cancel.Visibility = Visibility.Hidden;
            Add.Visibility = Visibility.Visible;
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = null;
            Add.Visibility = Visibility.Hidden;
            Confirm.Visibility = Visibility.Visible;
            Cancel.Visibility = Visibility.Visible;
            Button b = (Button)sender;
            User user = (User)b.DataContext;
            idClientToRemove = user.idClient;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = null;
            Add.Visibility = Visibility.Hidden;
            Confirm.Visibility = Visibility.Visible;
            Cancel.Visibility = Visibility.Visible;
            var pageRemoveMain = new UpdateUserPage();
            Button b = (Button)sender;
            pageRemoveMain.DataContext = (User)b.DataContext; ;
            Frame.Content = pageRemoveMain;
        }
    }
}
