using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

/*Villain Names
    Write a program that prints on the console all villains’ names
    and their number of minions of those who have more
    than 3 minions ordered descending by number of minions.
**/

namespace AdoNetExercise
{
    public static class Exercise2_VilianNames
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
                      @"SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  
                            FROM Villains AS v
                            JOIN MinionsVillains AS mv ON v.Id = mv.VillainId
                        GROUP BY v.Id, v.Name
                        HAVING COUNT(mv.VillainId) >= 3
                            ORDER BY COUNT(mv.VillainId) DESC";

                    Queue<Villains> vilians = new Queue<Villains>();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            vilians.Enqueue(
                                new Villains()
                                {
                                    Name = reader[0].ToString(),
                                    CountOfMinios = (int)reader[1]
                                });
                        }
                    }

                    foreach (var vilian in vilians)
                    {
                        Console.WriteLine($"{vilian.Name} - {vilian.CountOfMinios}");
                    }
                }
            }
        }
    }
}
