namespace DatabaseMigrations
{
    internal static class EntityNames
    {
        internal static class TableName
        {
            internal const string User = "AspNetUsers";
            internal const string UserRole = "AspNetRoles";
        }
        internal static class ColumnName {
            internal const string Id = "Id";
            internal const string Name = "Name";
            internal const string Description = "Description";
            internal const string LoginName = "LoginName";
            internal const string HashedPassword = "HashedPassword";

        }
        internal static string foreignkeyname(string tablename, string idname)
        {
            return $"{tablename}{idname}";
        }

        internal static string junctiontablename(string table1, string table2)
        {
            return $"{table1}{table2}";
        }
    }
}
