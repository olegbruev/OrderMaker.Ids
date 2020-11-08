using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;
using System.Reflection;

namespace Mtd.OrderMaker.Ids.Migrations
{
    public partial class InitSqlScript : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = typeof(InitSqlScript).Namespace + "._InitDatabase.sql";
            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            using StreamReader reader = new StreamReader(stream);
            string sqlResult = reader.ReadToEnd();
            migrationBuilder.Sql(sqlResult);
        }
    }
}
