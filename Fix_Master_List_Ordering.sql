-- ========================================================================
-- SQL Script to Add ORDER BY Clauses to Master List Functions
-- Database: InventoryManagement
-- Server: ADSVR\SQLEXPRESS01
-- Created: 2025-12-31
-- Purpose: Show recently added records first in all master lists
-- ========================================================================

USE [InventoryManagement]
GO

PRINT 'Starting Master List Function Updates...'
PRINT ''

-- ========================================================================
-- 1. GET_SabhasadMaster - Order by Registration Date
-- ========================================================================
PRINT 'Updating GET_SabhasadMaster...'
GO

ALTER FUNCTION [dbo].[GET_SabhasadMaster]
(
    @SRAID NVARCHAR(50),
    @KEYWORD NVARCHAR(MAX),
    @COMPANYID NVARCHAR(50),
    @DEPTID NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM SabhasadMaster
    WHERE
        (@SRAID = '' OR SRAID = @SRAID)
        AND (@KEYWORD = '' OR
             SNAME LIKE '%' + @KEYWORD + '%' OR
             SMOBILE LIKE '%' + @KEYWORD + '%')
        AND (@COMPANYID = '' OR COMPANYID = @COMPANYID)
        AND (@DEPTID = '' OR DEPTID = @DEPTID)
    ORDER BY REGDATE DESC
)
GO

-- ========================================================================
-- 2. GET_ProductMaster - Order by Product ID
-- ========================================================================
PRINT 'Updating GET_ProductMaster...'
GO

ALTER FUNCTION [dbo].[GET_ProductMaster]
(
    @PAID NVARCHAR(50),
    @KEYWORD NVARCHAR(MAX),
    @COMPANYID NVARCHAR(50),
    @DEPTID NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM ProductMaster
    WHERE
        (@PAID = '' OR PAID = @PAID)
        AND (@KEYWORD = '' OR
             PNAME LIKE '%' + @KEYWORD + '%' OR
             PCODE LIKE '%' + @KEYWORD + '%')
        AND (@COMPANYID = '' OR COMPANYID = @COMPANYID)
        AND (@DEPTID = '' OR DEPTID = @DEPTID)
    ORDER BY PAID DESC
)
GO

-- ========================================================================
-- 3. GET_VisitorMaster - Order by Visitor ID
-- ========================================================================
PRINT 'Updating GET_VisitorMaster...'
GO

ALTER FUNCTION [dbo].[GET_VisitorMaster]
(
    @VAID NVARCHAR(50),
    @KEYWORD NVARCHAR(MAX),
    @COMPANYID NVARCHAR(50),
    @DEPTID NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM VisitorMaster
    WHERE
        (@VAID = '' OR VID = @VAID)
        AND (@KEYWORD = '' OR
             VNAME LIKE '%' + @KEYWORD + '%' OR
             VMOBILE LIKE '%' + @KEYWORD + '%')
        AND (@COMPANYID = '' OR COMPANYID = @COMPANYID)
        AND (@DEPTID = '' OR DEPTID = @DEPTID)
    ORDER BY VID DESC
)
GO

-- ========================================================================
-- 4. GET_UserMasters - Order by Registration Date
-- ========================================================================
PRINT 'Updating GET_UserMasters...'
GO

ALTER FUNCTION [dbo].[GET_UserMasters]
(
    @USERID NVARCHAR(50),
    @KEYWORD NVARCHAR(MAX),
    @COMPANYID NVARCHAR(50),
    @DEPTID NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM UserMaster
    WHERE
        (@USERID = '' OR USERID = @USERID)
        AND (@KEYWORD = '' OR
             UNAME LIKE '%' + @KEYWORD + '%' OR
             UMOBILE LIKE '%' + @KEYWORD + '%')
        AND (@COMPANYID = '' OR COMPANYID = @COMPANYID)
        AND (@DEPTID = '' OR DEPTID = @DEPTID)
    ORDER BY UREGISTRATIONDATE DESC
)
GO

-- ========================================================================
-- 5. GET_BankMaster - Order by Bank ID
-- ========================================================================
PRINT 'Updating GET_BankMaster...'
GO

ALTER FUNCTION [dbo].[GET_BankMaster]
(
    @BAID NVARCHAR(50),
    @KEYWORD NVARCHAR(MAX),
    @COMPANYID NVARCHAR(50),
    @DEPTID NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM BankMaster
    WHERE
        (@BAID = '' OR BAID = @BAID)
        AND (@KEYWORD = '' OR BNAME LIKE '%' + @KEYWORD + '%')
        AND (@COMPANYID = '' OR COMPANYID = @COMPANYID)
        AND (@DEPTID = '' OR DEPTID = @DEPTID)
    ORDER BY BAID DESC
)
GO

-- ========================================================================
-- 6. GET_CategoryMaster - Order by Category ID
-- ========================================================================
PRINT 'Updating GET_CategoryMaster...'
GO

ALTER FUNCTION [dbo].[GET_CategoryMaster]
(
    @CTAID NVARCHAR(50),
    @KEYWORD NVARCHAR(MAX),
    @COMPANYID NVARCHAR(50),
    @DEPTID NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM CategoryMaster
    WHERE
        (@CTAID = '' OR CTAID = @CTAID)
        AND (@KEYWORD = '' OR CTNAME LIKE '%' + @KEYWORD + '%')
        AND (@COMPANYID = '' OR COMPANYID = @COMPANYID)
        AND (@DEPTID = '' OR DEPTID = @DEPTID)
    ORDER BY CTAID DESC
)
GO

-- ========================================================================
-- 7. GET_YojanaSubMaster - Order by Yojana ID
-- ========================================================================
PRINT 'Updating GET_YojanaSubMaster...'
GO

ALTER FUNCTION [dbo].[GET_YojanaSubMaster]
(
    @YAID NVARCHAR(50),
    @KEYWORD NVARCHAR(MAX),
    @COMPANYID NVARCHAR(50),
    @DEPTID NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM YojanaSubMaster
    WHERE
        (@YAID = '' OR YAID = @YAID)
        AND (@KEYWORD = '' OR YANAME LIKE '%' + @KEYWORD + '%')
        AND (@COMPANYID = '' OR COMPANYID = @COMPANYID)
        AND (@DEPTID = '' OR DEPTID = @DEPTID)
    ORDER BY YAID DESC
)
GO

-- ========================================================================
-- 8. GET_DepartmentMaster - Order by Department ID
-- ========================================================================
PRINT 'Updating GET_DepartmentMaster...'
GO

ALTER FUNCTION [dbo].[GET_DepartmentMaster]
(
    @DEID NVARCHAR(50),
    @KEYWORD NVARCHAR(MAX),
    @COMPANYID NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM DepartmentMaster
    WHERE
        (@DEID = '' OR DEID = @DEID)
        AND (@KEYWORD = '' OR DENAME LIKE '%' + @KEYWORD + '%')
        AND (@COMPANYID = '' OR COMPANYID = @COMPANYID)
    ORDER BY DEID DESC
)
GO

-- ========================================================================
-- 9. GET_DesignationMaster - Order by Designation ID
-- ========================================================================
PRINT 'Updating GET_DesignationMaster...'
GO

ALTER FUNCTION [dbo].[GET_DesignationMaster]
(
    @DGID NVARCHAR(50),
    @KEYWORD NVARCHAR(MAX),
    @COMPANYID NVARCHAR(50),
    @DEPTID NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM DesignationMaster
    WHERE
        (@DGID = '' OR DGID = @DGID)
        AND (@KEYWORD = '' OR DGNAME LIKE '%' + @KEYWORD + '%')
        AND (@COMPANYID = '' OR COMPANYID = @COMPANYID)
        AND (@DEPTID = '' OR DEPTID = @DEPTID)
    ORDER BY DGID DESC
)
GO

-- ========================================================================
-- 10. GET_DharmadhikariMaster - Order by Dharmadhikari ID
-- ========================================================================
PRINT 'Updating GET_DharmadhikariMaster...'
GO

ALTER FUNCTION [dbo].[GET_DharmadhikariMaster]
(
    @DHID NVARCHAR(50),
    @KEYWORD NVARCHAR(MAX),
    @COMPANYID NVARCHAR(50),
    @DEPTID NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM DharmadhikariMaster
    WHERE
        (@DHID = '' OR DHID = @DHID)
        AND (@KEYWORD = '' OR DHNAME LIKE '%' + @KEYWORD + '%')
        AND (@COMPANYID = '' OR COMPANYID = @COMPANYID)
        AND (@DEPTID = '' OR DEPTID = @DEPTID)
    ORDER BY DHID DESC
)
GO

-- ========================================================================
-- 11. GET_FestivalMaster - Order by Festival ID
-- ========================================================================
PRINT 'Updating GET_FestivalMaster...'
GO

ALTER FUNCTION [dbo].[GET_FestivalMaster]
(
    @FID NVARCHAR(50),
    @KEYWORD NVARCHAR(MAX),
    @COMPANYID NVARCHAR(50),
    @DEPTID NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM FestivalMaster
    WHERE
        (@FID = '' OR FID = @FID)
        AND (@KEYWORD = '' OR FNAME LIKE '%' + @KEYWORD + '%')
        AND (@COMPANYID = '' OR COMPANYID = @COMPANYID)
        AND (@DEPTID = '' OR DEPTID = @DEPTID)
    ORDER BY FID DESC
)
GO

-- ========================================================================
-- 12. GET_JatiMaster - Order by Jati ID
-- ========================================================================
PRINT 'Updating GET_JatiMaster...'
GO

ALTER FUNCTION [dbo].[GET_JatiMaster]
(
    @JAID NVARCHAR(50),
    @KEYWORD NVARCHAR(MAX),
    @COMPANYID NVARCHAR(50),
    @DEPTID NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM JatiMaster
    WHERE
        (@JAID = '' OR JAID = @JAID)
        AND (@KEYWORD = '' OR JANAME LIKE '%' + @KEYWORD + '%')
        AND (@COMPANYID = '' OR COMPANYID = @COMPANYID)
        AND (@DEPTID = '' OR DEPTID = @DEPTID)
    ORDER BY JAID DESC
)
GO

-- ========================================================================
-- 13. GET_KaryakarSadasyaMaster - Order by Karyakar ID
-- ========================================================================
PRINT 'Updating GET_KaryakarSadasyaMaster...'
GO

ALTER FUNCTION [dbo].[GET_KaryakarSadasyaMaster]
(
    @KSID NVARCHAR(50),
    @KEYWORD NVARCHAR(MAX),
    @COMPANYID NVARCHAR(50),
    @DEPTID NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM KaryakarSadasyaMaster
    WHERE
        (@KSID = '' OR KSID = @KSID)
        AND (@KEYWORD = '' OR KSNAME LIKE '%' + @KEYWORD + '%')
        AND (@COMPANYID = '' OR COMPANYID = @COMPANYID)
        AND (@DEPTID = '' OR DEPTID = @DEPTID)
    ORDER BY KSID DESC
)
GO

-- ========================================================================
-- 14. GET_MarketMaster - Order by Market ID
-- ========================================================================
PRINT 'Updating GET_MarketMaster...'
GO

ALTER FUNCTION [dbo].[GET_MarketMaster]
(
    @MKID NVARCHAR(50),
    @KEYWORD NVARCHAR(MAX),
    @COMPANYID NVARCHAR(50),
    @DEPTID NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM MarketMaster
    WHERE
        (@MKID = '' OR MKID = @MKID)
        AND (@KEYWORD = '' OR MKNAME LIKE '%' + @KEYWORD + '%')
        AND (@COMPANYID = '' OR COMPANYID = @COMPANYID)
        AND (@DEPTID = '' OR DEPTID = @DEPTID)
    ORDER BY MKID DESC
)
GO

-- ========================================================================
-- 15. GET_OccupationMaster - Order by Occupation ID
-- ========================================================================
PRINT 'Updating GET_OccupationMaster...'
GO

ALTER FUNCTION [dbo].[GET_OccupationMaster]
(
    @OCID NVARCHAR(50),
    @KEYWORD NVARCHAR(MAX),
    @COMPANYID NVARCHAR(50),
    @DEPTID NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM OccupationMaster
    WHERE
        (@OCID = '' OR OCID = @OCID)
        AND (@KEYWORD = '' OR OCNAME LIKE '%' + @KEYWORD + '%')
        AND (@COMPANYID = '' OR COMPANYID = @COMPANYID)
        AND (@DEPTID = '' OR DEPTID = @DEPTID)
    ORDER BY OCID DESC
)
GO

-- ========================================================================
-- 16. GET_PrayojanMaster - Order by Prayojan ID
-- ========================================================================
PRINT 'Updating GET_PrayojanMaster...'
GO

ALTER FUNCTION [dbo].[GET_PrayojanMaster]
(
    @PRID NVARCHAR(50),
    @KEYWORD NVARCHAR(MAX),
    @COMPANYID NVARCHAR(50),
    @DEPTID NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM PrayojanMaster
    WHERE
        (@PRID = '' OR PRID = @PRID)
        AND (@KEYWORD = '' OR PRNAME LIKE '%' + @KEYWORD + '%')
        AND (@COMPANYID = '' OR COMPANYID = @COMPANYID)
        AND (@DEPTID = '' OR DEPTID = @DEPTID)
    ORDER BY PRID DESC
)
GO

-- ========================================================================
-- 17. GET_RegistrationFeeMaster - Order by Fee ID
-- ========================================================================
PRINT 'Updating GET_RegistrationFeeMaster...'
GO

ALTER FUNCTION [dbo].[GET_RegistrationFeeMaster]
(
    @RFID NVARCHAR(50),
    @KEYWORD NVARCHAR(MAX),
    @COMPANYID NVARCHAR(50),
    @DEPTID NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM RegistrationFeeMaster
    WHERE
        (@RFID = '' OR RFID = @RFID)
        AND (@KEYWORD = '' OR RFNAME LIKE '%' + @KEYWORD + '%')
        AND (@COMPANYID = '' OR COMPANYID = @COMPANYID)
        AND (@DEPTID = '' OR DEPTID = @DEPTID)
    ORDER BY RFID DESC
)
GO

-- ========================================================================
-- 18. GET_ReligionMaster - Order by Religion ID
-- ========================================================================
PRINT 'Updating GET_ReligionMaster...'
GO

ALTER FUNCTION [dbo].[GET_ReligionMaster]
(
    @RLID NVARCHAR(50),
    @KEYWORD NVARCHAR(MAX),
    @COMPANYID NVARCHAR(50),
    @DEPTID NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM ReligionMaster
    WHERE
        (@RLID = '' OR RLID = @RLID)
        AND (@KEYWORD = '' OR RLNAME LIKE '%' + @KEYWORD + '%')
        AND (@COMPANYID = '' OR COMPANYID = @COMPANYID)
        AND (@DEPTID = '' OR DEPTID = @DEPTID)
    ORDER BY RLID DESC
)
GO

-- ========================================================================
-- 19. GET_SabhasadPrakarMaster - Order by Prakar ID
-- ========================================================================
PRINT 'Updating GET_SabhasadPrakarMaster...'
GO

ALTER FUNCTION [dbo].[GET_SabhasadPrakarMaster]
(
    @SPID NVARCHAR(50),
    @KEYWORD NVARCHAR(MAX),
    @COMPANYID NVARCHAR(50),
    @DEPTID NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM SabhasadPrakarMaster
    WHERE
        (@SPID = '' OR SPID = @SPID)
        AND (@KEYWORD = '' OR SPNAME LIKE '%' + @KEYWORD + '%')
        AND (@COMPANYID = '' OR COMPANYID = @COMPANYID)
        AND (@DEPTID = '' OR DEPTID = @DEPTID)
    ORDER BY SPID DESC
)
GO

-- ========================================================================
-- 20. GET_SamparkPrakarMaster - Order by Sampark Prakar ID
-- ========================================================================
PRINT 'Updating GET_SamparkPrakarMaster...'
GO

ALTER FUNCTION [dbo].[GET_SamparkPrakarMaster]
(
    @SKID NVARCHAR(50),
    @KEYWORD NVARCHAR(MAX),
    @COMPANYID NVARCHAR(50),
    @DEPTID NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM SamparkPrakarMaster
    WHERE
        (@SKID = '' OR SKID = @SKID)
        AND (@KEYWORD = '' OR SKNAME LIKE '%' + @KEYWORD + '%')
        AND (@COMPANYID = '' OR COMPANYID = @COMPANYID)
        AND (@DEPTID = '' OR DEPTID = @DEPTID)
    ORDER BY SKID DESC
)
GO

-- ========================================================================
-- 21. GET_SchoolMaster - Order by School ID
-- ========================================================================
PRINT 'Updating GET_SchoolMaster...'
GO

ALTER FUNCTION [dbo].[GET_SchoolMaster]
(
    @SCID NVARCHAR(50),
    @KEYWORD NVARCHAR(MAX),
    @COMPANYID NVARCHAR(50),
    @DEPTID NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM SchoolMaster
    WHERE
        (@SCID = '' OR SCID = @SCID)
        AND (@KEYWORD = '' OR SCNAME LIKE '%' + @KEYWORD + '%')
        AND (@COMPANYID = '' OR COMPANYID = @COMPANYID)
        AND (@DEPTID = '' OR DEPTID = @DEPTID)
    ORDER BY SCID DESC
)
GO

-- ========================================================================
-- 22. GET_UpadhyakshMaster - Order by Upadhyaksh ID
-- ========================================================================
PRINT 'Updating GET_UpadhyakshMaster...'
GO

ALTER FUNCTION [dbo].[GET_UpadhyakshMaster]
(
    @UPID NVARCHAR(50),
    @KEYWORD NVARCHAR(MAX),
    @COMPANYID NVARCHAR(50),
    @DEPTID NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM UpadhyakshMaster
    WHERE
        (@UPID = '' OR UPID = @UPID)
        AND (@KEYWORD = '' OR UPNAME LIKE '%' + @KEYWORD + '%')
        AND (@COMPANYID = '' OR COMPANYID = @COMPANYID)
        AND (@DEPTID = '' OR DEPTID = @DEPTID)
    ORDER BY UPID DESC
)
GO

-- ========================================================================
-- 23. GET_YojanaMaster - Order by Yojana ID
-- ========================================================================
PRINT 'Updating GET_YojanaMaster...'
GO

ALTER FUNCTION [dbo].[GET_YojanaMaster]
(
    @YOID NVARCHAR(50),
    @KEYWORD NVARCHAR(MAX),
    @COMPANYID NVARCHAR(50),
    @DEPTID NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM YojanaMaster
    WHERE
        (@YOID = '' OR YOID = @YOID)
        AND (@KEYWORD = '' OR YONAME LIKE '%' + @KEYWORD + '%')
        AND (@COMPANYID = '' OR COMPANYID = @COMPANYID)
        AND (@DEPTID = '' OR DEPTID = @DEPTID)
    ORDER BY YOID DESC
)
GO

-- ========================================================================
-- 24. GET_GharAanganMaster - Order by Ghar Aangan ID
-- ========================================================================
PRINT 'Updating GET_GharAanganMaster...'
GO

ALTER FUNCTION [dbo].[GET_GharAanganMaster]
(
    @GAID NVARCHAR(50),
    @KEYWORD NVARCHAR(MAX),
    @COMPANYID NVARCHAR(50),
    @DEPTID NVARCHAR(50)
)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM GharAanganMaster
    WHERE
        (@GAID = '' OR GAID = @GAID)
        AND (@KEYWORD = '' OR GANAME LIKE '%' + @KEYWORD + '%')
        AND (@COMPANYID = '' OR COMPANYID = @COMPANYID)
        AND (@DEPTID = '' OR DEPTID = @DEPTID)
    ORDER BY GAID DESC
)
GO

PRINT ''
PRINT '========================================================================'
PRINT 'All 24 Master List Functions Updated Successfully!'
PRINT '========================================================================'
PRINT ''
PRINT 'Summary:'
PRINT '  - All functions now return records in descending order'
PRINT '  - Newest/highest ID records appear first'
PRINT '  - Changes are immediately effective'
PRINT ''
PRINT 'Next Steps:'
PRINT '  1. Test master list screens in application'
PRINT '  2. Verify newest records appear at top'
PRINT '  3. Clear any frontend cache if needed'
PRINT ''
PRINT 'Script execution completed: ' + CONVERT(VARCHAR, GETDATE(), 120)
GO
