using Microsoft.EntityFrameworkCore.Migrations;

namespace PetShop.DAL.EF.Migrations
{
    public partial class udf_sp_trigger_fixtures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string udfHash = 
                @"CREATE FUNCTION [dbo].[UDF_Hash]
                (
	                @password NVARCHAR(255),
	                @salt UNIQUEIDENTIFIER
                )
                RETURNS VARBINARY(MAX)
                AS
                BEGIN
	                RETURN HASHBYTES('SHA2_512', @password + convert(VARCHAR(50), @salt))
                END";

            string spRegister = @"
                CREATE PROCEDURE [dbo].[SP_User_INSERT]
	                @email NVARCHAR(255),
	                @password NVARCHAR(255),
	                @lastName NVARCHAR(50),
	                @firstName NVARCHAR(50),
	                @birthDate DATETIME2,
	                @roleId INT
                AS
	                DECLARE @salt UNIQUEIDENTIFIER;
	                SET @salt = NEWID();
	                INSERT INTO [User](Email,Password,Salt,LastName,FirstName,BirthDate,RoleId) 
					OUTPUT INSERTED.* VALUES(
		                @email,
		                dbo.UDF_Hash(@password, @salt),
		                @salt,
		                @lastName,
		                @firstName,
		                @birthDate,
		                @roleId
	                );

                RETURN 0
            ";

            string spGetByCred = @"
                CREATE PROCEDURE [dbo].[SP_User_Select_By_Credentials]
					@email NVARCHAR(255),
					@password NVARCHAR(255)
				AS
					SELECT 
						u.*,
						r.[Name] [RoleName]
					FROM [User] u
					LEFT JOIN [Role] r ON u.RoleId = r.Id 
					WHERE u.Id = (
						SELECT Id
						FROM [User]
						WHERE Email = @email AND [Password] = dbo.UDF_Hash(@password, Salt)
					)
				RETURN 0
            ";

			string trPetUpdate = @"
				CREATE TRIGGER [TR_Pet_Update_Insert]
				ON [dbo].[Pet]
				AFTER UPDATE, INSERT
				AS
				BEGIN
					SET NOCOUNT ON
					UPDATE Pet SET UpdateDate = GETDATE()
					WHERE Id IN (SELECT Id FROM INSERTED) 
				END

			";

			string trAnimalInsert = @"
				CREATE TRIGGER [TR_Insert_Animal]
				ON [dbo].[Animal]
				AFTER INSERT
				AS
				BEGIN
					SET NOCOUNT ON
					INSERT INTO Breed([Name], AnimalId) 
						SELECT 'Unknown', [Id] FROM inserted 
				END
			";

			string fixtures = @"
				INSERT INTO PetStatus VALUES('FREE'),('BOOKED'), ('SOLD');
				INSERT INTO [Role] VALUES('CUSTOMER'), ('ADMIN');
				DECLARE @customer_id INT;
				DECLARE @admin_id INT;
				SELECT @admin_id = Id FROM [Role] WHERE [Name] LIKE 'ADMIN';
				SELECT @customer_id = Id FROM [Role] WHERE [Name] LIKE 'CUSTOMER';
				EXEC SP_User_Insert 'a@a', 'a', 'a', 'a', '1992-09-17', @admin_id;
				EXEC SP_User_Insert 'john@doe.be', 'test1234=', 'Doe', 'John', null, @customer_id;
			";

			migrationBuilder.Sql(udfHash);
			migrationBuilder.Sql(spRegister);
			migrationBuilder.Sql(spGetByCred);
			migrationBuilder.Sql(trAnimalInsert);
			migrationBuilder.Sql(trPetUpdate);
			migrationBuilder.Sql(fixtures);
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
