using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAssignmentDatabase
{
    public class DatabaseInteraction
    {
        //every time the main is called a new OleDbConnection, and OleDbCommand object are created.
        public OleDbConnection connection;
        public OleDbCommand command;
        public OleDbDataReader reader;
        public String start = "01/03/2018", end = "31/03/2018";
        public DatabaseInteraction()

        {
            connection = new OleDbConnection(connectionString: @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = E:\NETDatabase\Assign6DB.accdb; Persist Security Info = False; ");


            /*
            *   Try to connectinitially by calling Open() on the created  
            *   OleDbConnection object: connection
            */

            try
            {

                //Within the command class there is a Connection data member, 
                //so for each object it will store the connection
                connection.Open();
                Console.WriteLine("Connection Open");

                //create command object above
                command = connection.CreateCommand();
                command.Connection = connection;
            }

            /*
             * use a catch should the connection fail and return exception object
            */
            catch (Exception ex)
            {
                Console.WriteLine("Connection Failed");
                Console.WriteLine(ex);
            }

            //finally
            //{
            //    if (connection != null)
            //    {
            //        connection.Close();
            //        Console.WriteLine("Connection Closed");
            //    }
            //}
            Console.ReadKey();
        }
        /*
                * if the connection is not null, close the connection
               */
        public void CloseConnection()
        {
            if (connection != null)
            {
                connection.Close();
                Console.WriteLine("Connection now Closed");
            }
        }


        public void Displayo(OleDbCommand command)
        {
            Console.WriteLine("CURRENT RECORDS");
            //sql command - note, no @ symbol needed
            command.CommandText = "SELECT * FROM Students";
            command.CommandType = CommandType.Text;

            Console.WriteLine(command);

            //create OleDbDataReader object and assign it to command.ExecuteReader
            //and while there is data to be read, keep the reader open by calling Read on the reader instance
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("Student ID: " + reader["Student_ID"] + " \n" + "Owner: " + reader["Owner"] + " \n" + "Vehicle Model: " + reader["Vehicle_Model"]);
                Console.WriteLine("");
            }
            //close the reader when no longer needed
            reader.Close();
        }
        /*
         * Pass in the OleDbCommand and OleDbConnection objects
         * Create an SQL statement of type string to insert hard coded data
         */
        public void InsertInto(OleDbCommand command, OleDbConnection connection)
        {
            Console.WriteLine("UPDATE RECORDS VIA INSERT");
            String Insert = @"INSERT INTO Students(Student_ID, Vehicle_Model, Registration, Owner, Apartment) VALUES ('6', 'Batmobile', 'BAT1', 'Bruce Wayne', 11);";
            command.CommandText = Insert;
            command.CommandType = CommandType.Text;

            command.ExecuteNonQuery();
        }
        /*
         * Pass in the OleDbCommand and OleDbConnection objects
         * Create an SQL statement of type string to update previously inserted data
         */
        public void Update(OleDbCommand command, OleDbConnection connection)
        {
            Console.WriteLine("UPDATE RECORD");
            String Update = @"UPDATE Students SET Owner ='Jason Todd' WHERE Student_ID= '6'";
            command.CommandText = Update;
            command.CommandType = CommandType.Text;

            command.ExecuteNonQuery();
        }
        /*
         * Pass in the OleDbCommand and OleDbConnection objects
         * Create an SQL statement of type string to delete previously updated data
         */
        public void Delete(OleDbCommand command, OleDbConnection connection)
        {
            Console.WriteLine("DELETE RECORDS VIA DELETE");
            String Delete = @"DELETE FROM  Students WHERE Student_ID= '6'";

            command.CommandText = Delete;
            command.CommandType = CommandType.Text;


            command.ExecuteNonQuery();

        }
        public int CountPermits(OleDbCommand command, OleDbConnection connection)
        {
            int counter = 0;
            command.CommandText = "SELECT * FROM Students";
            command.CommandType = CommandType.Text;
            //create OleDbDataReader object and assign it to command.ExecuteReader
            //and while there is data to be read, keep the reader open by calling Read on the reader instance
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                counter++;

            }
            Console.WriteLine("Nuber of permits: " + counter);
            //close the reader when no longer needed
            reader.Close();
            return counter;
        }

        public void Month(OleDbCommand command, String start, String end)
        {
            //create two DateTime objects for the start and end of month
            DateTime startOfMonth;
            DateTime endOfMonth;
            String validUntil;
            DateTime valUntil;
            int count = 0;
            //parse the start and end taken in, and assign to start/endOfMonth
            startOfMonth = DateTime.Parse(start);
            endOfMonth = DateTime.Parse(end);

            command.CommandText = "SELECT * FROM Students";
            command.CommandType = CommandType.Text;
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                //store reader valid until into validUntil variable
                validUntil = reader["Valid_Until"].ToString();
                valUntil = DateTime.Parse(validUntil);
                if (valUntil <= endOfMonth && valUntil >= startOfMonth)
                {
                    count++;

                }
            }
            Console.WriteLine("Number of valid permits: " + count);
            //close the reader when no longer needed
            reader.Close();

        }
        public int FeesCalculation(OleDbCommand command, String start, String end)
        {
            //create two DateTime objects for the start and end of month
            DateTime startOfMonth;
            DateTime endOfMonth;
            String validUntil;
            DateTime valUntil;
            //int count = 0;
            int fees = 100;
            int totalFees = 0;
            //parse the start and end taken in, and assign to start/endOfMonth
            startOfMonth = DateTime.Parse(start);
            endOfMonth = DateTime.Parse(end);

            command.CommandText = "SELECT * FROM Students";
            command.CommandType = CommandType.Text;
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                //store reader valid until into validUntil variable
                validUntil = reader["Valid_Until"].ToString();
                valUntil = DateTime.Parse(validUntil);
                if (valUntil > DateTime.Now)
                {
                    totalFees = totalFees + fees;
                }
            }
            Console.WriteLine("Total fees: " + totalFees);
            //close the reader when no longer needed
            reader.Close();
            return totalFees;

        }
        //public void Unique(OleDbCommand command, String start, String end)
        //{

        //}
        //public void UniqueVehicles(OleDbCommand command, OleDbConnection connection)
        //{

        //    command.CommandText = "SELECT * FROM Students";
        //    command.CommandType = CommandType.Text;
        //    OleDbDataReader reader = command.ExecuteReader();

        //    while (reader.Read())   //while reader is reading 
        //    {
        //        int count = 0;      // counter for each permit to track number of vehicles 
        //        for (int i = 1; i <= 3; i++) //loop through vehicles 
        //        {
        //            if (reader[i] != DBNull.Value)  //if cell exists 
        //            {
        //                count++;        //add to count
        //                if (String.IsNullOrEmpty(Convert.ToString(reader[i])))
        //                {               //if the cell just contains an empty string or nothing
        //                    count--;        //subtract from count
        //                }
        //            }
        //        }
        //        //display contents of the reader ( the id and the count value 
        //        Console.WriteLine(reader[0] + " has " + count + " vehicles assigned");
        //    }

        //}


        //public static int 
        //this is a further test
        //to test github
        //
    }
}
