using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AdoNetExercise
{
    public class Exercise5_ChangeTownNamesCasing
    {
        public static void ChangeTownNamesCasing()
        {
            string countryName = Console.ReadLine();
            List<Towns> towns = new List<Towns>();

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
                        @"SELECT t.Name
                            FROM Towns AS t
                        JOIN Countries AS c ON c.Id = t.CountryCode
                            WHERE c.Name = @townName";

                    command.Parameters.AddWithValue("@townName", countryName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            towns.Add(new Towns()
                            {
                                Name = (string)reader[0]
                            }
                            );
                        }
                    }
                }
            }

            if(towns.Count == 0)
            {
                Console.WriteLine("No town names were affected.");
            }
            else
            {
                Console.WriteLine($"{towns.Count} town names were affected.");
                Console.Write("[");
                foreach (var town in towns)
                {
                    Console.Write(town.Name + " ");
                }
                Console.Write("]");
            }
        }
    }
}
