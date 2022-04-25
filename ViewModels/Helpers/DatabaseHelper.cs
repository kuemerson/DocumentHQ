using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentHQ.ViewModels.Helpers
{
    public class DatabaseHelper
    {
        //sets database path to the directory where project is currently stored and names it (old hqDb.db3) dochqDb.db3 - .db3 is a SQLite database file
        private static string dbFile = Path.Combine(Environment.CurrentDirectory, "dochqDb.db3");

        //create generic methods
        //generic insert method
        public static bool Insert<T>(T item) 
        { 
            bool result = false;
            //create new sqlite connection using dbFile
            //connection closes after exiting using block
            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                //generic create table that can create any type (year, semester, course) if table doesn't exist
                conn.CreateTable<T>();
                //insert method that returns number of rows inserted
                int rows = conn.Insert(item);
                //rows insert is greater than 1 then the insert was sucessful
                if (rows > 0)
                    result = true;
            }
            return result;
        }
        //generic delete method
        public static bool Delete<T>(T item)
        {
            bool result = false;
            
            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {                
                conn.CreateTable<T>();  
                //deletes object via primary key - returns number of rows deleted
                int rows = conn.Delete(item);                
                if (rows > 0)
                    result = true;
            }
            return result;
        }
        //generic update
        public static bool Update<T>(T item)
        {
            bool result = false;
            
            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                
                conn.CreateTable<T>();
                //updates all row excpet for primary key - requires object to have primary key
                int rows = conn.Update(item);                
                if (rows > 0)
                    result = true;
            }
            return result;
        }
        // where T : new() - establishes T will have parameterless constructor - is needed to use the generic Table method since T has to be parameterless constructor
        public static List<T> Read<T>() where T : new()
        {
            List<T> items;
            //SQLite connection
            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {

                conn.CreateTable<T>();
                //call Table method and calls to list on query object that is returned
                items = conn.Table<T>().ToList();
            }
            return items;
        }
    }
}
