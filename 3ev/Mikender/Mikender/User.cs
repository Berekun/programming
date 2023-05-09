using System.Windows;

namespace Mikender
{
    public class User
    {
        public User()
        {

        }

        public User(int idClient, string name, int age, string image, string description, string gender, int rating)
        {
            this.idClient = idClient;
            this.name = name;
            this.age = age;
            this.image = image;
            this.description = description;
            this.gender = gender;
            this.rating = rating;
        }

        public int idClient { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public string gender { get; set; }
        public int rating { get; set; }
        public Visibility HasOneStar => rating > 1 ? Visibility.Visible : Visibility.Hidden;
        public Visibility HasTwoStar => rating > 2 ? Visibility.Visible : Visibility.Hidden;
        public Visibility HasThreeStar => rating > 3 ? Visibility.Visible : Visibility.Hidden;
        public Visibility HasFourStar => rating > 4 ? Visibility.Visible : Visibility.Hidden;
        public Visibility HasFiveStar => rating >= 5 ? Visibility.Visible : Visibility.Hidden;

    }
}
