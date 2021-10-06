using Microsoft.EntityFrameworkCore.Migrations;

namespace PetShop.DAL.EF.Migrations
{
    public partial class Views : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string createViews = @"
            CREATE VIEW VUser
			AS(
				SELECT 
					u.*,
					r.[Name] RoleName
				FROM [User] u
				LEFT JOIN [Role] r ON u.RoleId = r.Id
			)

			GO

			CREATE VIEW VBreed
			AS(
				SELECT 
					b.*,
					a.[Name] AnimalName
				FROM Breed b
				LEFT JOIN Animal a ON b.AnimalId = a.Id
			)

			GO

			CREATE VIEW VPet
			AS(
				SELECT
					p.*,
					b.[Name] BreedName,
					b.[AnimalName],
					b.[AnimalId],
					s.[Name] PetStatusName
				FROM Pet p
				LEFT JOIN VBreed b ON p.BreedId = b.Id
				LEFT JOIN [PetStatus] s ON p.PetStatusId = s.[id]
			)
            ";
			migrationBuilder.Sql(createViews);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
