using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAssignmentDatabase
{
    class Program
    {
        static void Main(string[] args)
        {

            DatabaseInteraction dbTest = new DatabaseInteraction();

            Console.WriteLine("\n");
            // command.CommandText = "SELECT * FROM Students";
            dbTest.command.CommandType = CommandType.Text;
            //Calling the execute reader method on this Execute reader object
            //and returns an oledb data reader
            dbTest.Displayo(dbTest.command);
            Console.WriteLine("\n");
            dbTest.InsertInto(dbTest.command, dbTest.connection);
            Console.WriteLine("\n");
            dbTest.Displayo(dbTest.command);
            Console.WriteLine("\n");
            dbTest.Update(dbTest.command, dbTest.connection);
            Console.WriteLine("\n");
            dbTest.Displayo(dbTest.command);
            Console.WriteLine("\n");
            dbTest.Delete(dbTest.command, dbTest.connection);
            Console.WriteLine("\n");
            dbTest.Displayo(dbTest.command);
            dbTest.CountPermits(dbTest.command, dbTest.connection);
            Console.WriteLine("\n");
            dbTest.Month(dbTest.command, dbTest.start, dbTest.end);
            Console.WriteLine("\n");
            dbTest.FeesCalculation(dbTest.command, dbTest.start, dbTest.end);

            Console.ReadKey();

        }
    }
}
