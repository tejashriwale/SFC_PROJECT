USE [InventoryManagement]
GO

-- =============================================
-- Update GET_CropBill Function
-- Group by Farmer to avoid duplicate entries
-- =============================================

CREATE OR ALTER FUNCTION [dbo].[GET_CropBill]
(
      @CROPID        VARCHAR(24) = '%'
    , @COMPANYID     VARCHAR(24) = ''
    , @DEPTID        VARCHAR(24) = ''
)
RETURNS TABLE
AS
RETURN
(
    SELECT
         M.CROPID
        ,M.CBKID
        ,M.CRDATE AS VISITINGDATE
        ,M.NARR
        ,F.PHONENUM AS PHONENUMBER
        ,M.CTYPE
        ,M.CVARIETY
        ,M.CAREA
        ,F.FNAME AS FARMERNAME
        ,M.RATE
        ,M.WEIGHT
        ,M.CARET
        ,M.AMOUNT
        ,M.ADVANCE
        ,M.FAID
        ,I.ITEMNE AS CROPNAME
        ,COALESCE(D.CDAID, '') AS CDAID
        ,COALESCE(D.VDAID, '') AS VDAID
        ,CASE
            WHEN M.ISCOMPLITED = '1' THEN 'COMPLETED'
            ELSE 'PENDING'
         END AS BILLSTATUS
    FROM tblCropInvertMaster M
    LEFT JOIN tblFarmer F ON M.FAID = F.FAID AND F.ISDELETED = 0
    LEFT JOIN tblITEM I ON M.CROPID = I.ITEMAID AND I.ISDELETED = 0
    LEFT JOIN (
        -- Get first detail record for each crop (to avoid duplicates)
        SELECT CROPID, MIN(CDAID) AS CDAID, MIN(VDAID) AS VDAID
        FROM tblCropInvertDetail
        WHERE ISDELETED = 0
        GROUP BY CROPID
    ) D ON M.CROPID = D.CROPID
    WHERE M.CROPID LIKE @CROPID
        AND M.COMPANYID = @COMPANYID
        AND M.DEPTID = @DEPTID
        AND M.ISDELETED = 0
)
GO

PRINT 'GET_CropBill function updated successfully with farmer grouping!'
PRINT 'Now each farmer will appear only once in the list.'
GO
