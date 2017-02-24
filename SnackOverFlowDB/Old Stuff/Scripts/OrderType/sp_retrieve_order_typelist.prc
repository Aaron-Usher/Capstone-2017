USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_order_type_list')
BEGIN
Drop PROCEDURE sp_retrieve_order_type_list
Print '' print  ' *** dropping procedure sp_retrieve_order_type_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_order_type_list'
GO
Create PROCEDURE sp_retrieve_order_type_list
AS
BEGIN
SELECT ORDER_TYPE_ID
FROM order_type
END
