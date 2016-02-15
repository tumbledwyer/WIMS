using FluentMigrator;
using WhereIsMyShit.DataConstants;

namespace WhereIsMyShit.DbMigrations.Migrations
{
    [Migration(2016021511800)]
    public class Migration_2016021511800_CreateBorrowerTable : Migration
    {
        public override void Up()
        {
            Create.Table(Tables.Borrower.NAME)
                .WithColumn(Tables.Borrower.Columns.ID).AsInt32().PrimaryKey().Identity()
                .WithColumn(Tables.Borrower.Columns.NAME).AsString()
                .WithColumn(Tables.Borrower.Columns.SURNAME).AsString()
                .WithColumn(Tables.Borrower.Columns.EMAIL).AsString()
                .WithColumn(Tables.Borrower.Columns.PHONENUMBER).AsString()
                .WithColumn(Tables.Borrower.Columns.PHOTO).AsBinary();
        }

        public override void Down()
        {
        }
    }
}