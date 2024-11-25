using System;
using FinalWorkshop.Models;

namespace FinalWorkshop.Repository
{
	public class UserRepository
	{
        private readonly BookingAnimalsDbContext Db;

        public UserRepository()
        {
        }

        public UserRepository(BookingAnimalsDbContext db)
        {
            Db = db;
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public string UserInput()
        {
            return Console.ReadLine();
        }

        public bool VerifyUserMail(string mail)
        {
            var user = Db.Users.Where(x => x.email == mail);
            return user != null;

        }

        public bool VerifyUserPassword(string password)
        {
            var user = Db.Users.Where(x => x.password == password);
            return user != null;
        }

        public void VerifyConnection()
        {
            bool resultMail;
            bool resultPassword;
            //le mail et le password mis en données brutes dans le DbContext sont "admin" et "Administrator1!"
            do
            {
                DisplayMessage("Entrez votre mail");
                var mail = Console.ReadLine();
                DisplayMessage("Entrez votre mot de passe");
                string password = UserInput();
                resultMail = VerifyUserMail(mail);
                resultPassword = VerifyUserPassword(password);
            }
            while (!resultMail && !resultPassword);
            DisplayMessage("Connection validée");
        }
    }
}

