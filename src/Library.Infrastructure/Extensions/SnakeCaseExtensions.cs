using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Library.Infrastructure.Extensions
{
    public static class SnakeCaseExtensions
    {
        public static void ToSnakeNames(this ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entity.GetTableName().ToSnakeCase();
                entity.SetTableName(tableName);

                foreach (var property in entity.GetProperties())
                {
                    var columnName = property.GetColumnName().ToSnakeCase();
                    property.SetColumnName(columnName);
                }

                foreach (var key in entity.GetKeys())
                {
                    var keyName = key.GetName().ToSnakeCase();
                    key.SetName(keyName);
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    var foreignKeyName = key.GetConstraintName().ToSnakeCase();
                    key.SetConstraintName(foreignKeyName);
                }

                //foreach (var index in entity.GetIndexes())
                //{
                //    var indexName = index.GetName().ToSnakeCase();
                //    index.SetName(indexName);
                //}
            }
        }
        private static string ToSnakeCase(this string name)
        {
            return string.IsNullOrWhiteSpace(name)
                ? name
                : Regex.Replace(
                    name,
                    @"([a-z0-9])([A-Z])",
                    "$1_$2",
                    RegexOptions.Compiled,
                    TimeSpan.FromSeconds(0.2)).ToLower();
        }
    }
}
