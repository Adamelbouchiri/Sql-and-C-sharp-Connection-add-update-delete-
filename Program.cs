using System;
using System.Data.SqlClient;

class Program
{
    static string ConnectionString = "Server=.;Database=ContactsDB; User Id=sa;Password=sa123456";
    
    public struct stContacts
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int CountryID { get; set; }
    }

    static void AddNewContact(stContacts Contact)
    {
        SqlConnection connection = new SqlConnection(ConnectionString);

        string query = "Insert into Contacts(FirstName, LastName, Email, Phone, Address, CountryID) Values" +
            "(@FirstName, @LastName, @Email, @Phone, @Address, @CountryID)";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@FirstName", Contact.FirstName);
        command.Parameters.AddWithValue("@LastName", Contact.LastName);
        command.Parameters.AddWithValue("@Email", Contact.Email);
        command.Parameters.AddWithValue("@Phone", Contact.Phone);
        command.Parameters.AddWithValue("@Address", Contact.Address);
        command.Parameters.AddWithValue("@CountryID", Contact.CountryID);

        try
        {
            connection.Open();

            int AffectedRows = command.ExecuteNonQuery();

            if(AffectedRows > 0)
            {
                Console.WriteLine("Record Inserted Succssefully");
            }
            else
            {
                Console.WriteLine("Record Insertion Failed");
            }

            connection.Close();
        }
        catch(Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void AddNewContactAndGetID(stContacts Contact)
    {
        SqlConnection connection = new SqlConnection(ConnectionString);

        string query = "Insert into Contacts(FirstName, LastName, Email, Phone, Address, CountryID) Values" +
            "(@FirstName, @LastName, @Email, @Phone, @Address, @CountryID);" +
            "select SCOPE_IDENTITY();";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@FirstName", Contact.FirstName);
        command.Parameters.AddWithValue("@LastName", Contact.LastName);
        command.Parameters.AddWithValue("@Email", Contact.Email);
        command.Parameters.AddWithValue("@Phone", Contact.Phone);
        command.Parameters.AddWithValue("@Address", Contact.Address);
        command.Parameters.AddWithValue("@CountryID", Contact.CountryID);

        try
        {
            connection.Open();
            object result = command.ExecuteScalar();

            if(result != null && int.TryParse(result.ToString(), out int insertedID))
            {
                Console.WriteLine($"Newly Inserted ID : {insertedID}");
            }
            else
            {
                Console.WriteLine("Failed To Retrieve The Inserted ID");
            }

            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error :" + ex.Message );
        }
    }

    static void UpdateContatct(int contactID, stContacts Contact)
    {

        SqlConnection connection = new SqlConnection(ConnectionString);

        string query = "update Contacts " +
            "set FirstName = @FirstName, LastName = @LastName," +
            " Email = @Email, Address = @Address," +
            " Phone = @Phone, CountryID = @CountryID " +
            "where ContactID = @contactID";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@ContactID", contactID);
        command.Parameters.AddWithValue("@FirstName", Contact.FirstName);
        command.Parameters.AddWithValue("@LastName", Contact.LastName);
        command.Parameters.AddWithValue("@Email", Contact.Email);
        command.Parameters.AddWithValue("@Address", Contact.Address);
        command.Parameters.AddWithValue("@Phone", Contact.Phone);
        command.Parameters.AddWithValue("@CountryID", Contact.CountryID);

        try
        { 
            connection.Open();

            int rowsAffected = command.ExecuteNonQuery();

            if(rowsAffected > 0)
            {
                Console.WriteLine("Record Updated Successfully");
            }
            else
            {
                Console.WriteLine("Record Updated Failed");
            }

            connection.Close();
        }
        catch(Exception ex)
        {
            Console.WriteLine("Error" + ex.Message );
        }

    }

    static void DeleteContact(int contactID)
    {
        SqlConnection connection = new SqlConnection(ConnectionString);

        string query = "Delete from Contacts where ContactID = @contactID";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@contactID", contactID);

        try
        {
            connection.Open();

            int rowsAffected = command.ExecuteNonQuery();

            if(rowsAffected > 0)
            {
                Console.WriteLine("Contact Deleted Successfully");
            }
            else
            {
                Console.WriteLine("Contact Delet Failed");
            }

            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine ("Error" + ex.Message );
        }
    }

    static void Main(string[] args)
    {
        stContacts Contact = new stContacts
        {
            FirstName = "Houda",
            LastName = "Qaddouri",
            Email = "Houda@Mtk.ma",
            Address = "hgt 4, ssr 129, casa",
            Phone = "0787934612",
            CountryID = 1,
        };

        //AddNewContact(Contact);
        //AddNewContactAndGetID(Contact);
        //UpdateContatct(1008, Contact);

        DeleteContact(1008);

        Console.ReadLine();
    }
}

