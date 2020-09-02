-- =============================================
-- Author:  Rashik  
-- Create date: 2020-09-02
-- Description: User Management (Add ,Edit,Assign, List Branch )

-- Last Updated By:
-- 2020-09-02 Rashik:Initialized
-- =============================================
ALTER PROCEDURE PROC_USERMANAGEMENT
(
	@Flag						VARCHAR(50),
	@Id							VARCHAR(100)  = NULL,
	@UserName					VARCHAR(100)    = NULL,
	@NormalizedUserName			VARCHAR(100)    = NULL,
	@Email						VARCHAR(100)    = NULL,
	@NormalizedEmail			VARCHAR(100)    = NULL,
	@EmailConfirmed				VARCHAR(100)    = NULL,
	@Password					VARCHAR(100)    = NULL,
	@PasswordHash				VARCHAR(100)    = NULL,
	@SecurityStamp				VARCHAR(100)    = NULL,
	@ConcurrencyStamp			VARCHAR(100)    = NULL,
	@PhoneNumber				VARCHAR(100)    = NULL,
	@PhoneNumberConfirmed		VARCHAR(100)    = NULL,
	@TwoFactorEnabled			VARCHAR(100)    = NULL,
	@LockoutEnd					VARCHAR(100)    = NULL,
	@LockoutEnabled				VARCHAR(100)    = NULL,
	@AccessFailedCount			INT				= NULL,
	@ContactNo					VARCHAR(100)    = NULL,
	@CreatedBy					VARCHAR(100)    = NULL,
	@CreatedDate				VARCHAR(100)    = NULL,
	@FirstName					VARCHAR(100)    = NULL,
	@MiddleName					VARCHAR(100)    = NULL,
	@LastName					VARCHAR(100)    = NULL,
	@ForcePasswordChange		VARCHAR(100)    = NULL,
	@FullName					VARCHAR(100)    = NULL,
	@FullNameLocal				NVARCHAR(100)   = NULL,
	@Gender						VARCHAR(10)		= NULL,
	@ModifiedBy					VARCHAR(100)    = NULL,
	@ModifiedDate				VARCHAR(100)    = NULL,
	@PermanentAddress			VARCHAR(100)    = NULL,
	@TemporaryAddress			VARCHAR(100)    = NULL,

	@Status						CHAR(2)					= NULL,
	@FromDate					VARCHAR(10)				= NULL,
	@ToDate						VARCHAR(10)				= NULL,
	@DisplayLength				INT						= NULL,					
	@DisplayStart				INT						= NULL,
	@SortCol					INT						= NULL,	
	@SortDir					NVARCHAR(10)			= NULL,	
	@Search						NVARCHAR(100)			= NULL,
	@Param						NVARCHAR(300)			= NULL,
	@Role						NVARCHAR(300)			= NULL,
	@AllowLogin					BIT						= NULL
)
AS 
SET NOCOUNT ON
BEGIN TRY
	/*
		UserLists -> Get user lists for grid
		GetRequiredDetails -> Get required details for dropdown list
		CheckUserName -> Check user name for duplication
		GetUserDetails -> Get details for update
		EditUser -> update user
		UpdateUserStatus -> update user status
	*/
	DECLARE @FirstRec INT, @LastRec INT
		SET @FirstRec = @DisplayStart;
		SET @LastRec = @DisplayStart + @DisplayLength;

	IF @Flag = 'UserLists'
	BEGIN
		Select ROW_NUMBER() over (ORDER BY au.Id DESC) AS RowNum,au.*, COUNT(*) OVER() AS FilterCount
		INTO #tempUser
		FROM Authentication.AuthUsers AS au (NOLOCK)
		WHERE (@Search IS NULL OR au.UserName like '%' + @Search + '%'  
				OR (@Search IS NULL OR au.FirstName like '%' + @Search + '%')
				)
		AND au.UserName<>'SUPERUSER'
		SELECT * FROM #tempUser
		Where RowNum > @FirstRec and RowNum <= @LastRec 
		AND UserName<>@UserName
		ORDER BY #tempUser.CreatedDate DESC
		DROP TABLE #tempUser
    END

	ELSE IF @Flag = 'GetRequiredDetails'
	BEGIN
		-- Gender
		SELECT 'M' Value,'Male' Description UNION ALL 
		SELECT 'F' Value,'Female' Description
		-- Roles
		SELECT ar.Name Value, ar.Name Description  FROM Authentication.AuthRoles AS ar (NOLOCK)
    END 

	ELSE IF @Flag = 'CheckUserName'
	BEGIN
		IF NOT EXISTS (SELECT 'x' FROM Authentication.AuthUsers AS au (NOLOCK) WHERE au.UserName = @UserName)
		BEGIN
			SELECT '000' Code,'Username does not exist.' Message,'' Data
			RETURN
		END
        SELECT '111' Code,'Username with the name already exist. Please use different username.' Message,'' Data
	END

	ELSE IF @Flag = 'GetUserDetails'
	BEGIN
		SELECT au.*,ar.Name AS Role FROM Authentication.AuthUsers AS au (NOLOCK)
		INNER JOIN Authentication.AuthUserRoles AS aur (NOLOCK) ON aur.UserId = au.Id
		INNER JOIN Authentication.AuthRoles AS ar (NOLOCK) ON ar.Id = aur.RoleId
		WHERE au.Id=@Id
	END


	ELSE IF @Flag = 'EditUser'
	BEGIN
		IF EXISTS(SELECT 'x' FROM Authentication.AuthUsers AS au (NOLOCK) WHERE au.Id = @Id)
		BEGIN
			UPDATE Authentication.AuthUsers	
				SET 
				Email				= @Email,
				NormalizedEmail		= UPPER(@Email),
				PhoneNumber			= @PhoneNumber,
				ContactNo			= @ContactNo,
				FirstName			= @FirstName,
				MiddleName			= @MiddleName,
				LastName			= @LastName,
				FullName			= @FullName,
				FullNameLocal		= @FullNameLocal,
				ModifiedBy			= @ModifiedBy,
				ModifiedDate		= SYSDATETIME(),
				PermanentAddress	= @PermanentAddress,
				TemporaryAddress	= @TemporaryAddress,
				PasswordHash		= ISNULL(@PasswordHash,PasswordHash),
				Gender				= @Gender,
				Status				= ISNULL(@Status,Status)
			WHERE Id = @Id
			SELECT '000' Code,'User updated successfully.' Message,'' Data
		END
		SELECT '111' Code,'No user with the id exists.' Message,'' Data
	END

	ELSE IF @Flag = 'UpdateUserStatus'
	BEGIN
		IF EXISTS(SELECT 'x' FROM Authentication.AuthUsers AS au (NOLOCK) WHERE au.Id=@Id)
		BEGIN
			UPDATE Authentication.AuthUsers SET 
				Status			= CASE WHEN ISNULL(Status,'A')='A' THEN 'I' ELSE 'A' END,
				ModifiedBy		= @ModifiedBy,
				ModifiedDate	= SYSDATETIME()
			WHERE id = @Id
			SELECT '000' Code,'User status updated successfully.' Message,'' Data
			RETURN
		END
		SELECT '111' Code,'User status update failed.' Message,'' Data
	END

END TRY
BEGIN CATCH
IF @@TRANCOUNT>0
	ROLLBACK
	SELECT 111 Code, ERROR_MESSAGE() Message, '' Data
END CATCH
GO

