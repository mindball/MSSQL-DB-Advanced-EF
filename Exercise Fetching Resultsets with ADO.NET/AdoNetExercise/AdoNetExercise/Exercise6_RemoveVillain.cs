using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AdoNetExercise
{
    public class Exercise6_RemoveVillain
    {
        public static void RemoveVillains()
        {
            string villainId = Console.ReadLine();

            SqlConnection connection;
            using (connection =
                new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string existVillainId = Configuration.GetNameFromMinionDB(
                    connection, "Villains", villainId);
                if(existVillainId is null)
                {
                    Console.WriteLine("No such villain was found.");
                    return;
                }

                RemoveCurrentVillain(connection, villainId);
            }
        }

        private static void RemoveCurrentVillain(SqlConnection connection, string villainId)
        {
            string villainName =
                Configuration.GetNameFromMinionDB(connection, "Villains", villainId);

            Console.WriteLine($"{villainName} was deleted.");

            SqlCommand command;
            using (command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText =
                    @"DELETE FROM MinionsVillains WHERE VillainId = @villainId";
                command.Parameters.AddWithValue("@villainId", villainId);
                int deletedMinionsRows = command.ExecuteNonQuery();

                Console.WriteLine($"{deletedMinionsRows} minions were released.");

                command.CommandText =
                    @"DELETE FROM Villains WHERE Id = @villainId";
                //command.Parameters.AddWithValue("@villainId", villainId);
                command.ExecuteNonQuery();
            }
        }
    }
}