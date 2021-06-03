using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AddressBookAdo.net
{
    class AddressBookRepo
    {
        public static String connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);
        public void CheckConnection()
        {
            try
            {
                using (this.connection)
                {
                    connection.Open();
                    Console.WriteLine("connection open");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("connection close");
            }
        }
        public void CreateTable()//create table
        {
            try
            {
                using (this.connection)
                {


                    // query for create table -------------
                    /*string query = @"create table AddreshBookADo(
                            first_name varchar(30) not null,
                            last_name varchar(30) not null,
                            address varchar(50) not null,
                            city varchar(20),
                             state varchar(20),
                             zip int,
                             phone_number varchar(10) not null,
                             email varchar(20) not null,
                              addressbook_name varchar(20) not null,
                                addressbook_type varchar(20) not null);";*/
                    //--------------------------------------------------------------------
                    string query = @"insert into AddreshBookADo values
                    ('yamini','Reddy','Marthalli','Banglore','KA',548512,'9440969971','yamini123@gmail.com','Addressbook4', 'friends');";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    var result = cmd.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {
                        Console.WriteLine("success");
                    }
                    else
                    {
                        Console.WriteLine("fail");
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine($"sorry!!! {e}");
            }
        }
    }
}

    