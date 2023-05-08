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

namespace Mikender
{
    /// <summary>
    /// Lógica de interacción para NewUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        private User? _user = new User();
        public User? User => _user;

        public AddUserPage()
        {
            InitializeComponent();
        }
        private void nameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _user.name = nameBox.Text;
        }

        private void ageBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _user.age = Convert.ToInt32(ageBox.Text);
        }

        private void descriptionBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _user.description = descriptionBox.Text;    
        }

        private void imageBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _user.image = imageBox.Text;
        }

        private void genderBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _user.gender = genderBox.Text;
        }

        private void ratingBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _user.rating = Convert.ToInt32(ratingBox.Text);
        }
    }
}
