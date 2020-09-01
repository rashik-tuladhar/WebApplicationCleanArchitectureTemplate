-- =============================================
-- Author:  Rashik Tuladhar
-- Create date: 2020-09-01
-- Description: Role Management Overall Process (Add ,Edit,Assign, List Roles )

-- Change History:
-- 2020-09-01 Rashik: Initialized
-- =============================================
CREATE PROCEDURE PROC_ROLEMANAGEMENT
(
	@Flag				VARCHAR(20),
	@RoleId				VARCHAR(50)				= NULL,
	@UserId				VARCHAR(50)				= NULL,
	@CreatedBy			VARCHAR(150)			= NULL,

	@Service			VARCHAR(100)			= NULL,
	@FromDate			VARCHAR(10)				= NULL,
	@ToDate				VARCHAR(10)				= NULL,
	@DisplayLength		INT						= NULL,					
	@DisplayStart		INT						= NULL,
	@SortCol			INT						= NULL,	
	@SortDir			NVARCHAR(10)			= NULL,	
	@Search				NVARCHAR(100)			= NULL,
	@UserName			NVARCHAR(100)			= NULL
)
AS 
SET NOCOUNT ON
BEGIN TRY
	/*
		GetRoleLists -> Get Eole Lists For Grid
		AvaibaleRoles -> For Creating Role Groups And Assigning Roles
		RoleDetails ->
		AvailableRoleSlugs -> For difference of roles
		CurrentRole -> Get Current Role For Assigning New Roles
		UpdateRole -> Update role
		AssignRole -> Assign Role To New user
	*/
	DECLARE @FirstRec INT, @LastRec INT
		SET @FirstRec = @DisplayStart;
		SET @LastRec = @DisplayStart + @DisplayLength;

	IF @Flag = 'GetRoleLists'
	BEGIN
		Select ROW_NUMBER() over (ORDER BY Id DESC) AS RowNum,COUNT(*) OVER() AS FilterCount,* 
		INTO #tempRoles
		FROM Authentication.AuthRoles AS ar (NOLOCK)
		WHERE (@Search IS NULL OR [Name] like '%' + @Search + '%' OR 
				(@Search IS NULL OR NormalizedName like '%' + @Search + '%') )
		SELECT * FROM #tempRoles
		Where RowNum > @FirstRec and RowNum <= @LastRec AND Name<>'SUPERUSER'
		ORDER BY #tempRoles.Id DESC
		DROP TABLE #tempRoles
    END

	ELSE IF @Flag='AvailableRoles'
	BEGIN
		SELECT [Group],MenuName,Slug,DisplayOrder,IsActive,SubGroupName
		FROM Authentication.AuthRolePermissions AS arp (NOLOCK)
		WHERE SubGroupName IS NOT NULL 
	END

	ELSE IF @Flag='RoleDetails'
	BEGIN
		SELECT [Group],MenuName,Slug,DisplayOrder,IsActive,SubGroupName
		FROM Authentication.AuthRolePermissions AS arp (NOLOCK)
			
		SELECT ClaimValue FROM Authentication.AuthRoleClaims AS arc (NOLOCK)
		WHERE RoleId=@RoleId

		SELECT Id,Name FROM Authentication.AuthRoles AS ar (NOLOCK)
		WHERE Id=@RoleId
	END

	ELSE IF @Flag='AvailableRoleSlugs'
	BEGIN
		SELECT Slug FROM Authentication.AuthRolePermissions AS arp (NOLOCK)
	END

	ELSE IF @Flag='AvailableRoleSlugs'
	BEGIN
		SELECT Slug FROM Authentication.AuthRolePermissions AS arp (NOLOCK)
	END
    
	ELSE IF @Flag='CurrentRole'
	BEGIN
		SELECT RoleId AS CurrentRole,au.UserName AS Username FROM Authentication.AuthUserRoles AS aur (NOLOCK) 
		RIGHT JOIN Authentication.AuthUsers AS au (NOLOCK) ON au.Id = aur.UserId
		WHERE au.Id=@UserId	
	END
    
	ELSE IF @Flag='UpdateRole'
	BEGIN
		IF EXISTS (SELECT 'x' FROM Authentication.AuthUserRoles AS aur (NOLOCK) WHERE UserId=@UserId)
		BEGIN
			UPDATE Authentication.AuthUserRoles 
			SET RoleId=@RoleId
			WHERE UserId=@UserId
        END
        ELSE
        BEGIN
			INSERT INTO Authentication.AuthUserRoles
			(
			    UserId,
			    RoleId
			)
			VALUES
			(   @UserId, -- UserId - nvarchar(450)
			    @RoleId  -- RoleId - nvarchar(450)
			 )
        END
		SELECT 100 Code,'Roles updated successfully.' Message,@UserId Id
	END

	ELSE IF @Flag='AssignRole'
	BEGIN
		DECLARE @UserIdForRole VARCHAR(100)
		SELECT @UserIdForRole = Id FROM Authentication.AuthUsers AS au (NOLOCK) WHERE UserName = @UserName
		
		IF @UserIdForRole IS NOT NULL
		BEGIN
			INSERT INTO Authentication.AuthUserRoles
			(
			    UserId,
			    RoleId
			)
			VALUES
			(   
				@UserIdForRole, -- UserId - nvarchar(450)
			    @RoleId  -- RoleId - nvarchar(450)
			)	
			SELECT 100 Code,'Roles assigned successfully.' Message,@UserIdForRole Id
			RETURN
		END
		SELECT 101 Code,'User with the username'+ @UserName +' does not exists.' Message,@UserId Id
	END
        
END TRY
BEGIN CATCH
IF @@TRANCOUNT>0
	ROLLBACK
	SELECT 101 Code, ERROR_MESSAGE() Msg, '' Id
END CATCH
GO

