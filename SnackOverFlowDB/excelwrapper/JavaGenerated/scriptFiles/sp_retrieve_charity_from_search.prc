USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_charity_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_charity_from_search
Print '' print  ' *** dropping procedure sp_retrieve_charity_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_charity_from_search'
GO
Create PROCEDURE sp_retrieve_charity_from_search
(
@CHARITY_ID[INT]=NULL,
@USER_ID[INT]=NULL,
@EMPLOYEE_ID[INT]=NULL,
@CHARITY_NAME[NVARCHAR](200)=NULL,
@CONTACT_FIRST_NAME[NVARCHAR](150)=NULL,
@CONTACT_LAST_NAME[NVARCHAR](150)=NULL,
@PHONE_NUMBER[NVARCHAR](20)=NULL,
@EMAIL[NVARCHAR](100)=NULL,
@CONTACT_HOURS[NVARCHAR](150)=NULL,
@STATUS[NVARCHAR](10)=Null
)
AS
BEGIN
Select CHARITY_ID, USER_ID, EMPLOYEE_ID, CHARITY_NAME, CONTACT_FIRST_NAME, CONTACT_LAST_NAME, PHONE_NUMBER, EMAIL, CONTACT_HOURS, STATUS
FROM CHARITY
WHERE (CHARITY.CHARITY_ID=@CHARITY_ID OR @CHARITY_ID IS NULL)
AND (CHARITY.USER_ID=@USER_ID OR @USER_ID IS NULL)
AND (CHARITY.EMPLOYEE_ID=@EMPLOYEE_ID OR @EMPLOYEE_ID IS NULL)
AND (CHARITY.CHARITY_NAME=@CHARITY_NAME OR @CHARITY_NAME IS NULL)
AND (CHARITY.CONTACT_FIRST_NAME=@CONTACT_FIRST_NAME OR @CONTACT_FIRST_NAME IS NULL)
AND (CHARITY.CONTACT_LAST_NAME=@CONTACT_LAST_NAME OR @CONTACT_LAST_NAME IS NULL)
AND (CHARITY.PHONE_NUMBER=@PHONE_NUMBER OR @PHONE_NUMBER IS NULL)
AND (CHARITY.EMAIL=@EMAIL OR @EMAIL IS NULL)
AND (CHARITY.CONTACT_HOURS=@CONTACT_HOURS OR @CONTACT_HOURS IS NULL)
AND (CHARITY.STATUS=@STATUS OR @STATUS IS NULL)
END