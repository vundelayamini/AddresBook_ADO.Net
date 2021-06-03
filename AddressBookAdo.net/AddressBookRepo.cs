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
        internal string first_name;
        /// <summary>
        /// Check connection
        /// </summary>
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
        /// <summary>
        /// Add contact method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Addcontatct(AddressBookModel model)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {

                using (connection)
                {
                    SqlCommand command = new SqlCommand("Sp_AddContactDetails", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@first_name", model.first_name);
                    command.Parameters.AddWithValue("@last_name", model.last_name);
                    command.Parameters.AddWithValue("@address", model.address);
                    command.Parameters.AddWithValue("@city", model.city);
                    command.Parameters.AddWithValue("@state", model.state);
                    command.Parameters.AddWithValue("@zip", model.zip);
                    command.Parameters.AddWithValue("@phone_number", model.phone_number);
                    command.Parameters.AddWithValue("@email", model.email);
                    command.Parameters.AddWithValue("@addressbook_name", model.addressbook_name);
                    command.Parameters.AddWithValue("@addressbook_type", model.addressbook_type);
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
        /// <summary>
        /// Edit contact using person name
        /// </summary>
        /// <param name="model"></param>
        public void EditContactUsingPersonName(AddressBookModel model)
           
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string updateQuery = @"UPDATE AddreshBookADo SET last_name = @last_name, City = @city, state = @state, email = @email, addressbook_name = @addressbook_name, addressbook_type = @addressbook_type WHERE first_name = @first_name;";
                    SqlCommand command = new SqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@first_name", model.first_name);
                    command.Parameters.AddWithValue("@last_name", model.last_name);
                    command.Parameters.AddWithValue("@city", model.city);
                    command.Parameters.AddWithValue("@state", model.state);
                    command.Parameters.AddWithValue("email", model.email);
                    command.Parameters.AddWithValue("@addressbook_name", model.addressbook_name);
                    command.Parameters.AddWithValue("@addressbook_type", model.addressbook_type);

                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Contact Updated successfully");
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        ///Delete contact by using name
        /// </summary>
        /// <param name="model"></param>
        public void DeleteContactUsingName(AddressBookModel model)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("Sp_DeletContactDetails", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@first_name", model.first_name);
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Contact Deleted successfully");
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }

    }

}




    