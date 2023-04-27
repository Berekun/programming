using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connections
{
    public class UserBuilder
    {
        private readonly User user = new User();
        public UserBuilder SetIdClient(int idClient)
        {
            user.idClient = idClient;
            return this;
        }

        public UserBuilder SetName(string name)
        {
            user.name = name;
            return this;
        }

        public UserBuilder SetAge(int age)
        {
            user.age = age;
            return this;
        }

        public UserBuilder SetImage(string image)
        {
            user.image = image;
            return this;
        }

        public UserBuilder SetDescription(string description)
        {
            user.description = description;
            return this;
        }

        public UserBuilder SetGender(string gender)
        {
            user.gender = gender;
            return this;
        }

        public UserBuilder SetRating(int rating)
        {
            user.rating = rating;
            return this;
        }

        public User Create()
        {
            return user;
        }
    }
}
