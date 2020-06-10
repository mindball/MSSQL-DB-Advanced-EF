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
    static class Exercise3_MinionNames
    {
        public static void GetVilianNames()
        {
            int vilianId = int.Parse(Console.ReadLine());

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
                      $"SELECT Name FROM Villains WHERE Id = @id";

                    command.Parameters.AddWithValue("@id", vilianId);
                    string vilianName = (string)command.ExecuteScalar();

                    if (vilianName is null)
                    {
                        Console.WriteLine($"No villain with ID {vilianId} exists in the database.");
                        return;
                    }

                    GetAllMinionName(command, vilianName);
                }
            }
        }

        private static void GetAllMinionName(SqlCommand command, string vilianName)
        {
            command.CommandText =
                      @"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                                         m.Name, 
                                         m.Age
                                    FROM MinionsVillains AS mv
                                    JOIN Minions As m ON mv.MinionId = m.Id
                                   WHERE mv.VillainId = @Id
                                ORDER BY m.Name";

            Dictionary<string, List<Minions>> vilionCollectionOfMinions = new Dictionary<string, List<Minions>>();
            vilionCollectionOfMinions.Add(vilianName, new List<Minions>());

            using (SqlDataReader reader = command.ExecuteReader())
            {

                if (!reader.HasRows)
                {
                    Console.WriteLine($"Villan: {vilianName}");
                    Console.WriteLine("(no minions)");

                    return;
                }
                while (reader.Read())
                {
                    vilionCollectionOfMinions[vilianName].Add
                        (
                            new Minions()
                            {
                                RowNumber = (long)reader[0],
                                Name = (string)reader[1],
                                Age = (int)(reader[2])
                            }
                        );
                }
            }

            foreach (var vilions in vilionCollectionOfMinions)
            {
                Console.WriteLine($"Villan: {vilions.Key}");

                foreach (var minion in vilions.Value)
                {
                    Console.WriteLine($"{minion.RowNumber}. {minion.Name} {minion.Age}");
                }
            }
        }
    }

    
}
