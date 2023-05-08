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
    /// Lógica de interacción para UpdateUserPage.xaml
    /// </summary>
    public partial class UpdateUserPage : Page
    {
        public User user { get; set; } = new User();
        public UpdateUserPage()
        {
            InitializeComponent();
        }

        private void nameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            user.name = tb.Text;
        }

        private void ageBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            user.age = Convert.ToInt32(tb.Text);
        }

        private void descriptionBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            user.description = tb.Text;
        }

        private void imageBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            user.image = tb.Text;
        }

        private void genderBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            user.gender = tb.Text;
        }

        private void ratingBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            user.rating = Convert.ToInt32(tb.Text);
        }

        private void idBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            user.idClient = Convert.ToInt32(tb.Text);
        }
    }
}
