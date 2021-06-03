using System;
using System.Collections.Generic;
using System.Data;
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
        //UC2-Create table
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
        //UC3-Insert new Contacts into addressbook.
        public bool Addcontatct(AddressBookModel data)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {

                using (connection)
                {
                    SqlCommand command = new SqlCommand("Sp_AddContactDetails", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@first_name", data.first_name);
                    command.Parameters.AddWithValue("@last_name", data.last_name);
                    command.Parameters.AddWithValue("@address", data.address);
                    command.Parameters.AddWithValue("@city", data.city);
                    command.Parameters.AddWithValue("@state", data.state);
                    command.Parameters.AddWithValue("@zip", data.zip);
                    command.Parameters.AddWithValue("@phone_number", data.phone_number);
                    command.Parameters.AddWithValue("@email", data.email);
                    command.Parameters.AddWithValue("@addressbook_name", data.addressbook_name);
                    command.Parameters.AddWithValue("@addressbook_type", data.addressbook_type);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("connection close");
            }
        }

    }
}

    