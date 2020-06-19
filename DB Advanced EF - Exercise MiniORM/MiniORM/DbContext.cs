using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MiniORM
{
    public abstract class DbContex
    {
        private readonly DatabaseConnection connection;
        private readonly Dictionary<Type, PropertyInfo> dbSetProperties;

        internal static Type[] AllowedSqlTypes =
            {
                typeof(bool),
                typeof(int),
                typeof(string),
                typeof(uint),
                typeof(DateTime),
                typeof(double),
                typeof(decimal),
                typeof(long),
                typeof(ulong)
            };

        protected DbContex(string connectionString)
        {
            this.connection = new DatabaseConnection(connectionString);
            this.dbSetProperties = this.DiscoverDbSets();

            using (new ConnectionManager(connection))
            {                
                this.InitializeDbSets();
            }

            this.MapAllRelations();
        }

        private void MapAllRelations()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            var dbSets = this.dbSetProperties
                .Select(x => x.Value.GetValue(this))
                .ToArray();

            foreach (IEnumerable<object> dbSet in dbSets)
            {
                var invalidEntities = dbSet
                    .Where(x => !IsObjectValid(x))
                    .ToArray();

                if(invalidEntities.Any())
                {
                    throw new InvalidOperationException(
                        $"{invalidEntities.Length} Invalid Entities found in {dbSet.GetType().Name}!");
                }
            }

            using (new ConnectionManager(this.connection))
            {
                using (var transaction = this.connection.StartTransaction())
                {
                    foreach (var dbSet in dbSets)
                    {
                        var dbSetType = dbSet.GetType().GetGenericArguments().First();

                        var persistMethod = typeof(DbContex)
                            .GetMethod("Persist", BindingFlags.NonPublic | BindingFlags.Instance);
                    }
                }
            }
        }

        private void InitializeDbSets()
        {
            throw new NotImplementedException();
        }

        private Dictionary<Type, PropertyInfo> DiscoverDbSets()
        {
            throw new NotImplementedException();
        }
    }
}