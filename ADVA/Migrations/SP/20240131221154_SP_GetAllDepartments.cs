using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADVA.Migrations.SP
{
    public partial class SP_GetAllDepartments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"GO
Create or ALTER               PROCEDURE [dbo].[GetAllDepartment]   
                                                                   @PageSize                                  INT,
                                                                   @PageNumber                                INT,
																   @OrderBy                                   NVARCHAR(250)  = 'id',
                                                                   @SortDir                                   NVARCHAR(10)   = 'DESC',
																   @Id int = null,
																   @DepartmentName  nvarchar(250) = null,
																   @ManagerId  int = null,
																   @IsActive  bit = null


AS
BEGIN
     DECLARE @countrows DECIMAL;
	-- SET NOCOUNT ON added to prevent extra result sets from 
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  Department.Id, Department.Name, ManagerId,
	Employee.Name as ManagerName,			 
	Department.IsActive from Department 	
	left join Employee on (Employee.FkDepartmentId = Department.Id)and(Employee.IsManager = 'true')
	
	where  
	( Department.Id = @Id  or @Id is null)
	and ( Department.Name LIKE '%' + @DepartmentName + '%' or @DepartmentName is null)
	and( ManagerId = @ManagerId  or @ManagerId is null)	
	and( Department.IsActive = @IsActive or @IsActive is null)

	   ORDER BY CASE
						 WHEN @OrderBy = N'Id'
                                  AND @SortDir = 'ASC'
                             THEN  Department.Id
                         END ASC,
                         CASE
                             WHEN @OrderBy = N'Id'
                                  AND @SortDir = 'DESC'
                             THEN  Department.Id
                         END DESC,
                         CASE    WHEN @OrderBy = N'Name'
                                  AND @SortDir = 'ASC'
                             THEN  Department.Name
                         END ASC,
                         CASE
                             WHEN @OrderBy = N'Name'
                                  AND @SortDir = 'DESC'
                             THEN  Department.Name
                         END DESC,						
						 CASE WHEN @OrderBy = N'IsActive'
                                  AND @SortDir = 'ASC'
                             THEN  Department.IsActive
                         END ASC,
                         CASE
                             WHEN @OrderBy = N'IsActive'
                                  AND @SortDir = 'DESC'
                             THEN  Department.IsActive
                         END DESC

						  OFFSET(@PageNumber - 1) * (@PageSize) ROWS FETCH NEXT(@PageSize) ROWS ONLY;
						      SET @countrows =
                (
                    	select count( Department.Id)
                FROM  Department 			
	inner join Employee on (Employee.FkDepartmentId = Department.Id)and(Employee.IsManager = 'true')
         
                   	where 
		( Department.Id = @Id  or @Id is null)
	and ( Department.Name LIKE '%' + @DepartmentName + '%' or @DepartmentName is null)
	and( ManagerId = @ManagerId  or @ManagerId is null)	
	and( Department.IsActive = @IsActive or @IsActive is null)
	);

                SELECT CEILING(@countrows / @PageSize) AS TotalPages, 
                       @PageNumber AS CurrentPage, 
                       @PageSize AS PageSize, 
                       @countrows AS TotalRows;


END";
     migrationBuilder.Sql(sp);  
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
