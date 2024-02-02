using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADVA.Migrations.SP
{
    public partial class SP_GetAllEmployees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
GO
Create or ALTER               PROCEDURE [dbo].[GetAllEmployee]   
                                                                   @PageSize                                  INT,
                                                                   @PageNumber                                INT,
																   @OrderBy                                   NVARCHAR(250)  = 'id',
                                                                   @SortDir                                   NVARCHAR(10)   = 'DESC',
																   @Id int = null,
																   @EmployeeName  nvarchar(250) = null,
																   @EmployeeSalary  int = null,
																   @FkManagerId  int = null,
																   @FkDepartmentId  int = null,
																   @IsManager  bit = null,
																   @IsActive  bit = null


AS
BEGIN
     DECLARE @countrows DECIMAL;
	-- SET NOCOUNT ON added to prevent extra result sets from 
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Emp.Id,Emp.Name, Emp.Salary,Emp.FkManagerId,
			Emp.FkDepartmentId,Emp.IsManager,
			M.Name As ManagerName,
			Department.Name as DepartmentName,
			Emp.IsActive from Employee as Emp
	join Employee as M on (M.Id = Emp.FkManagerId) and (M.IsManager = 'true')
	join Department on Department.Id = Emp.FkDepartmentId
	where  
	(Emp.Id = @Id  or @Id is null)
	and (Emp.Name LIKE '%' + @EmployeeName + '%' or @EmployeeName is null)
	and(Emp.Salary = @EmployeeSalary  or @EmployeeSalary is null)
	and(Emp.FkDepartmentId = @FkDepartmentId or @FkDepartmentId is null)
	and(Emp.FkManagerId = @FkManagerId  or @FkManagerId is null)
	and(Emp.IsManager = @IsManager or @IsManager is null)
	and(Emp.IsActive = @IsActive or @IsActive is null)

	   ORDER BY CASE
						 WHEN @OrderBy = N'Id'
                                  AND @SortDir = 'ASC'
                             THEN Emp.Id
                         END ASC,
                         CASE
                             WHEN @OrderBy = N'Id'
                                  AND @SortDir = 'DESC'
                             THEN Emp.Id
                         END DESC,
                         CASE    WHEN @OrderBy = N'Name'
                                  AND @SortDir = 'ASC'
                             THEN Emp.Name
                         END ASC,
                         CASE
                             WHEN @OrderBy = N'Name'
                                  AND @SortDir = 'DESC'
                             THEN Emp.Name
                         END DESC,
						   CASE WHEN @OrderBy = N'Salary'
                                  AND @SortDir = 'ASC'
                             THEN Emp.Salary
                         END ASC,
                         CASE
                             WHEN @OrderBy = N'Salary'
                                  AND @SortDir = 'DESC'
                             THEN Emp.Salary
                         END DESC,
						 CASE WHEN @OrderBy = N'IsActive'
                                  AND @SortDir = 'ASC'
                             THEN Emp.IsActive
                         END ASC,
                         CASE
                             WHEN @OrderBy = N'IsActive'
                                  AND @SortDir = 'DESC'
                             THEN Emp.IsActive
                         END DESC

						  OFFSET(@PageNumber - 1) * (@PageSize) ROWS FETCH NEXT(@PageSize) ROWS ONLY;
						      SET @countrows =
                (
                    	select count(Emp.Id)
                FROM  Employee as Emp
				join Employee as M on M.Id =Emp.FkManagerId
	join Department on Department.Id = Emp.FkDepartmentId
         
                   	where 
	(Emp.Id = @Id  or @Id is null)
	and (Emp.Name LIKE '%' + @EmployeeName + '%' or @EmployeeName is null)
	and(Emp.Salary = @EmployeeSalary  or @EmployeeSalary is null)
	and(Emp.FkDepartmentId = @FkDepartmentId or @FkDepartmentId is null)
	and(Emp.FkManagerId = @FkManagerId  or @FkManagerId is null)
	and(Emp.IsManager = @IsManager or @IsManager is null)
	and(Emp.IsActive = @IsActive or @IsActive is null)
	);
	
                SELECT CEILING(@countrows / @PageSize) AS TotalPages, 
                       @PageNumber AS CurrentPage, 
                       @PageSize AS PageSize, 
                       @countrows AS TotalRows;

-------------------------------------------------------
					   SELECT EmpM.Id,EmpM.Name, EmpM.Salary,EmpM.FkManagerId,
			EmpM.FkDepartmentId,EmpM.IsManager,
			Manager.Name As ManagerOFManagerName,
			Department.Name as MDepartmentName,
			EmpM.IsActive from Employee as EmpM
	left join Employee as Manager on (Manager.Id = EmpM.FkManagerId)
	left join Department on Department.Id = EmpM.FkDepartmentId
	where  
	(EmpM.IsManager = 'true')
	

	   ORDER BY CASE
						 WHEN @OrderBy = N'Id'
                                  AND @SortDir = 'ASC'
                             THEN EmpM.Id
                         END ASC,
                         CASE
                             WHEN @OrderBy = N'Id'
                                  AND @SortDir = 'DESC'
                             THEN EmpM.Id
                         END DESC,
                         CASE    WHEN @OrderBy = N'Name'
                                  AND @SortDir = 'ASC'
                             THEN EmpM.Name
                         END ASC,
                         CASE
                             WHEN @OrderBy = N'Name'
                                  AND @SortDir = 'DESC'
                             THEN EmpM.Name
                         END DESC,
						   CASE WHEN @OrderBy = N'Salary'
                                  AND @SortDir = 'ASC'
                             THEN EmpM.Salary
                         END ASC,
                         CASE
                             WHEN @OrderBy = N'Salary'
                                  AND @SortDir = 'DESC'
                             THEN EmpM.Salary
                         END DESC,
						 CASE WHEN @OrderBy = N'IsActive'
                                  AND @SortDir = 'ASC'
                             THEN EmpM.IsActive
                         END ASC,
                         CASE
                             WHEN @OrderBy = N'IsActive'
                                  AND @SortDir = 'DESC'
                             THEN EmpM.IsActive
                         END DESC

						  OFFSET(@PageNumber - 1) * (@PageSize) ROWS FETCH NEXT(@PageSize) ROWS ONLY;
						      SET @countrows =
                (
                    	select count(EmpM.Id)
                from Employee as EmpM
	left join Employee as Manager on (Manager.Id = EmpM.FkManagerId) 
	join Department on Department.Id = EmpM.FkDepartmentId
	where  
	(EmpM.IsManager = 'true')
	);

                SELECT CEILING(@countrows / @PageSize) AS TotalPages, 
                       @PageNumber AS CurrentPage, 
                       @PageSize AS PageSize, 
                       @countrows AS TotalRows;


END



";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
