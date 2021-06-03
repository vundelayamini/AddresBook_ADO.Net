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
        }
    }
}
