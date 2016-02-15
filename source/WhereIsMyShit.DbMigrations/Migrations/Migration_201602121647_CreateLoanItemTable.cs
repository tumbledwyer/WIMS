using FluentMigrator;
using _Table = WhereIsMyShit.DataConstants.Tables.LoanItem;
using _Columns = WhereIsMyShit.DataConstants.Tables.LoanItem.Columns;

namespace WhereIsMyShit.DbMigrations.Migrations
{
    [Migration(201602121647)]
    public class Migration_201602121647_CreateLoanItemTable : Migration
    {
        public override void Up()
        {
            Create.Table(_Table.NAME)
                .WithColumn(_Columns.ID).AsInt32().PrimaryKey().Identity()
                .WithColumn(_Columns.NAME).AsString();
        }

        public override void Down()
        {
        }
    }
}
