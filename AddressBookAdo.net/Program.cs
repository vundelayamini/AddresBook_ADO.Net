using System;

namespace AddressBookAdo.net
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome message to Addressbook ADO.Net");
            AddressBookRepo check = new AddressBookRepo();
            check.CheckConnection();
            AddressBookRepo repo = new AddressBookRepo();
            repo.CreateTable();
            //New contact to address book.
             /*data.first_name = "sunil";
            data.last_name = "sneha";
            data.address = "chennai Highway";
            data.city = "chennai";
            data.state = "Tamilnadu";
            data.zip = 523233;
            data.phone_number = "9440969971";
            data.email = "Sunil1234@gmail.com";
            data.addressbook_name = "AddressBook1";
            data.addressbook_type = "Profession";
            check.Addcontatct(data);*/



        }
    }
}
