using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBookAdo.net
{
    class AddressBookModel
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int zip { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
        public string addressbook_name { get; set; }
        public string addressbook_type { get; set; }
    }
}

