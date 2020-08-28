using EFConsoleUI.DataAccess;
using EFConsoleUI.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreateTim();
            //ReadAll();
            UpdateFirstName(1,"Macko");
            ReadById(1);

            Console.WriteLine("Done Processing!");
            Console.ReadLine();
        }

        private static void CreateTim()
        {
            var c = new Contact
            {
                FirstName = "Tim",
                LastName = "Corey"
            };
            c.EmailAddresses.Add(new Email {EmailAddresses="tim@iamtimcorey.com" });
            c.EmailAddresses.Add(new Email { EmailAddresses="me@timcorey.com"});
            c.PhoneNumbers.Add(new Phone { PhoneNumber = "555-1212" });
            c.PhoneNumbers.Add(new Phone { PhoneNumber = "555-1234" });

            using (var db =  new ContactContext())
            {
                db.Contacts.Add(c);
                db.SaveChanges();
            }
        }

        private static void ReadAll()
        {
            using (var db = new ContactContext())
            {
                var records = db.Contacts
                    .Include(e => e.EmailAddresses)
                    .Include(p=>p.PhoneNumbers)
                    .ToList();

                foreach (var c in records)
                {
                    Console.WriteLine($"{c.FirstName} {c.LastName}");
                }
            }
        }

        private static void ReadById(int id)
        {
            using (var db = new ContactContext())
            {
                var user = db.Contacts.Where(c=>c.Id ==id).First();

                Console.WriteLine($"{user.FirstName} {user.LastName}");
            }
        }

        private static void UpdateFirstName(int id, string firstName)
        {
            using (var db = new ContactContext())
            {
                var user = db.Contacts.Where(c => c.Id == id).First();

                user.FirstName = firstName;

                db.SaveChanges();
            }
        }
    }
}
