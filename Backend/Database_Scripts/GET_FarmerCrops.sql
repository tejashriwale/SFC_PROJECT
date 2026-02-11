-- =============================================
-- Description: Get crops associated with a specific farmer
-- Parameters:
--   @MAID: Farmer ID (Member ID)
--   @COMPANYID: Company ID
--   @DEPTID: Department ID
-- Returns: Table with CROPID and CROPNAME
-- =============================================

CREATE OR ALTER FUNCTION [dbo].[GET_FarmerCrops]
(
    @MAID VARCHAR(50),
    @COMPANYID VARCHAR(50),
    @DEPTID VARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    -- Query to get unique crops for a specific farmer
    -- This assumes you have a table that links farmers to crops (like visitor details, plot details, or crop bill)
    -- Adjust the table names and columns based on your actual database schema

    SELECT DISTINCT
        cd.CROPID,
        i.ITEMNE AS CROPNAME
    FROM VISITERD cd
    INNER JOIN ITEM i ON cd.CROPID = i.ITEMAID
    WHERE cd.MAID = @MAID
        AND cd.COMPANYID = @COMPANYID
        AND cd.DEPTID = @DEPTID
        AND cd.ISDELETED = 0
        AND i.ISDELETED = 0

    UNION

    -- Also check from plot details if farmer has crop plots
    SELECT DISTINCT
        pd.CROPID,
        i.ITEMNE AS CROPNAME
    FROM PLOTD pd
    INNER JOIN ITEM i ON pd.CROPID = i.ITEMAID
    WHERE pd.MAID = @MAID
        AND pd.COMPANYID = @COMPANYID
        AND pd.DEPTID = @DEPTID
        AND pd.ISDELETED = 0
        AND i.ISDELETED = 0

    UNION

    -- Also check from crop invert details
    SELECT DISTINCT
        cid.CROPID,
        i.ITEMNE AS CROPNAME
    FROM CROPINVERTD cid
    INNER JOIN ITEM i ON cid.CROPID = i.ITEMAID
    INNER JOIN CROPINVERTM cim ON cid.CMID = cim.CMID
    WHERE cim.MAID = @MAID
        AND cim.COMPANYID = @COMPANYID
        AND cim.DEPTID = @DEPTID
        AND cid.ISDELETED = 0
        AND i.ISDELETED = 0
)
GO
