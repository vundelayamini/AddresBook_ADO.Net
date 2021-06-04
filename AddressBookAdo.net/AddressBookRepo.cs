using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
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
        /// <summary>
        /// Retrive person in city or state
        /// </summary>
        public void CountByCityOrState()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                AddressBookModel model = new AddressBookModel();
                using (connection)
                {
                    using (SqlCommand command = new SqlCommand(
                        @"SELECT * FROM AddreshBookADo WHERE city = 'Hyderabad' OR state = 'Telangana';
                        SELECT * FROM AddreshBookADo WHERE city = 'Banglore' OR state = 'KA'; ", connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                {
                                    model.first_name = reader.GetString(0);
                                    model.last_name = reader.GetString(1);
                                    model.address = reader.GetString(2);
                                    model.city = reader.GetString(3);
                                    model.state = reader.GetString(4);
                                    model.zip = reader.GetInt32(5);
                                    model.phone_number = reader.GetString(6);
                                    model.email = reader.GetString(7);
                                    Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7}", model.first_name, model.last_name, model.address, model.city, model.state, model.zip, model.phone_number, model.email);
                                    Console.WriteLine("\n");
                                }
                            }
                        }
                    }
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
        /// Count by city or state
        /// </summary>
        public void CountByCityAndState()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                AddressBookModel addressBookModel = new AddressBookModel();
                using (connection)
                {
                    using (SqlCommand command = new SqlCommand(
                        @"select city,COUNT(city)from AddressBookDetails group by city;
                        select state, COUNT(state)from AddressBookDetails group by state; ", connection))
                    {
                        connection.Open();
                        using (SqlDataReader sqlDataReader = command.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                addressBookModel.city = sqlDataReader.GetString(0);
                                int countCIty = sqlDataReader.GetInt32(1);
                                Console.WriteLine("{0},{1}", addressBookModel.city, countCIty);
                                Console.WriteLine("\n");
                            }
                            if (sqlDataReader.NextResult())
                            {
                                while (sqlDataReader.Read())
                                {
                                    addressBookModel.state = sqlDataReader.GetString(0);
                                    int stateCount = sqlDataReader.GetInt32(1);
                                    Console.WriteLine("{0},{1}", addressBookModel.state, stateCount);
                                    Console.WriteLine("\n");
                                }
                            }
                        }
                    }
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
        /// Size of the addressbook by city and state
        /// </summary>
        public void AddressBookSizeByCityANDState()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    using (SqlCommand command = new SqlCommand(
                        @"SELECT COUNT(first_name) FROM AddreshBookADo WHERE city = 'Hyderabad' AND state = 'Telangana'; 
                        SELECT COUNT(first_name) FROM AddreshBookADo WHERE city = 'Banglore' AND state = 'KA';", connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var count = reader.GetInt32(0);
                                Console.WriteLine($"Number of Persons belonging to City 'Hyderabad' And  'Telangana' : {count}");
                                Console.WriteLine("\n");
                            }
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    var count = reader.GetInt32(0);
                                    Console.WriteLine($"Number of Persons belonging to City 'Banglore' And State 'KA' : {count}");
                                    Console.WriteLine("\n");
                                }
                            }
                        }
                    }
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
        ///Sorted records alphabetically 
        /// </summary>
        public void SortedRecordsAlphabeticallyByFirstName()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                AddressBookModel model = new AddressBookModel();
                using (connection)
                {
                    string query = @"select * from AddressBookDetails where City='Margao' order by FirstName;";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.first_name = reader.GetString(0);
                            model.last_name = reader.GetString(1);
                            model.address = reader.GetString(2);
                            model.city = reader.GetString(3);
                            model.state = reader.GetString(4);
                            model.zip = reader.GetInt32(5);
                            model.phone_number = reader.GetString(6);
                            model.email = reader.GetString(7);
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7}", model.first_name, model.last_name, model.address, model.city, model.state, model.zip, model.phone_number, model.email);
                            Console.WriteLine("\n");
                        }

                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    reader.Close();
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
        /// Get number of persons identify each Address Book with name and Type. 
        /// </summary>
        public void GetNumberOfPersonsCountByType()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    using (SqlCommand command = new SqlCommand(
                        @"SELECT COUNT(first_name) FROM AddreshBookADo WHERE addressbook_type = 'Family'; 
                        SELECT COUNT(first_name) FROM AddreshBookADo WHERE addressbook_type = 'Friends';", connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var count = reader.GetInt32(0);
                                Console.WriteLine("Number of Persons belonging to Addressbook Type 'Family' : {0}", count);
                                Console.WriteLine("\n");
                            }
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    var count = reader.GetInt32(0);
                                    Console.WriteLine("Number of Persons belonging to Addressbook Type 'Friends' : {0}", count);
                                    Console.WriteLine("\n");
                                }
                            }
                        }
                    }
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
        /// Add persons as friends and family
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddPersonAsFriendAndFamily(AddressBookModel model)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand cmd = new SqlCommand("spCRUDoperations", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", model.first_name);
                    cmd.Parameters.AddWithValue("@LastName", model.last_name);
                    cmd.Parameters.AddWithValue("@Address", model.address);
                    cmd.Parameters.AddWithValue("@City", model.city);
                    cmd.Parameters.AddWithValue("@State", model.state);
                    cmd.Parameters.AddWithValue("@Zip", model.zip);
                    cmd.Parameters.AddWithValue("@PhoneNumber", model.phone_number);
                    cmd.Parameters.AddWithValue("@Email", model.email);
                    cmd.Parameters.AddWithValue("@AddressBookName", model.addressbook_name);
                    cmd.Parameters.AddWithValue("@AddressBookType",model.addressbook_type);
                    connection.Open();
                    var result = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }

                    SqlCommand cmd1 = new SqlCommand("spCRUDoperations", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", model.first_name);
                    cmd.Parameters.AddWithValue("@LastName", model.last_name);
                    cmd.Parameters.AddWithValue("@Address", model.address);
                    cmd.Parameters.AddWithValue("@City", model.city);
                    cmd.Parameters.AddWithValue("@State", model.state);
                    cmd.Parameters.AddWithValue("@Zip", model.zip);
                    cmd.Parameters.AddWithValue("@PhoneNumber", model.phone_number);
                    cmd.Parameters.AddWithValue("@Email", model.email);
                    cmd.Parameters.AddWithValue("@AddressBookName", model.addressbook_name);
                    cmd.Parameters.AddWithValue("@AddressBookType", model.addressbook_type);
                    connection.Open();
                    var result1 = cmd.ExecuteNonQuery();
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
            }
        }
    }
}
   


