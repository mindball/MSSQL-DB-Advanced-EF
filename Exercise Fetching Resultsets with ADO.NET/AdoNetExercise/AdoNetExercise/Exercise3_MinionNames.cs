using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

/*
    Write a program that prints on the console all minion names and 
    age for a given villain id, ordered by name alphabetically.
    If there is no villain with the given ID, print
    "No villain with ID <VillainId>; exists in the database."
    If the selected villain has no minions, print "(no minions)";
    on the second row.
    **/

namespace AdoNetExercise
{
    class Exercise3_MinionNames
    {
        public static void GetVilianNames()
        {
            SqlConnection connection;
            using (connection =
                new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                SqlCommand command;
                using (command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText =
                      @"
                        ";
                }
            }
        }
    }
}
