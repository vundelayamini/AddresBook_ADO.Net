using System;

namespace AddressBookAdo.net
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome message to Addressbook ADO.Net");
            AddressBookRepo addressBookRepo= new AddressBookRepo();
            AddressBookModel model= new AddressBookModel();
            //New contact to address book.
            model.first_name = "sunil";
           model.last_name = "sneha";
           model.address = "chennai Highway";
           model.city = "chennai";
           model.state = "Tamilnadu";
           model.zip = 523233;
           model.phone_number = "9440969971";
           model.email = "Sunil1234@gmail.com";
           model.addressbook_name = "AddressBook1";
           model.addressbook_type = "Profession";
            addressBookRepo.CheckConnection();
            addressBookRepo.Addcontatct(model);

            addressBookRepo.CheckConnection();
             addressBookRepo.Addcontatct(model);

            // addressBookRepo.EditContactUsingName(addressBookModel, "Sunil");
            // addressBookRepo.DeleteContactUsingFirstName("Sunil");

            addressBookRepo.Addcontatct(model);
            addressBookRepo.DeleteContactUsingName("Sneha");



        }
    }
}
