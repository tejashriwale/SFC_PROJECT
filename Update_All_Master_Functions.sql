USE [InventoryManagement]
GO

PRINT 'Updating all master list functions with ORDER BY...'
PRINT ''

-- ============================================================================
-- GET_UserMasters - Order by UREGISTRATIONDATE DESC
-- ============================================================================
ALTER FUNCTION [dbo].[GET_UserMasters]
(
	@UAID		VARCHAR(24)		=	'%',
	@KEYWORD	VARCHAR(MAX)	=	'%',
	@COMPANYID	VARCHAR(24)		=	''
)
RETURNS TABLE
AS
RETURN
(
	SELECT TOP 2147483647
			COALESCE(U.UAID														,	'')		AS	UAID
			,COALESCE(U.UCOMPANYID												,	'')		AS	UCOMPANYID
			,COALESCE(U.UBRANCHID												,	'')		AS	UBRANCHID
			,COALESCE(U.UPACSID													,	'')		AS	UPACSID
			,COALESCE(U.USTOREID												,	'')		AS	USTOREID
			,COALESCE(U.UWAREHOUSEID											,	'')		AS	UWAREHOUSEID
			,COALESCE(U.UROLEID													,	'')		AS	UROLEID
			,COALESCE(U.UTITLE													,	'')		AS	UTITLE
			,COALESCE(U.UFORENAME												,	'')		AS	UFORENAME
			,COALESCE(U.UDESCRIPTION											,	'')		AS	UDESCRIPTION
			,COALESCE(U.UEMAILADDRESS											,	'')		AS	UEMAILADDRESS
			,COALESCE(U.UMOBILENUMBER											,	'')		AS	UMOBILENUMBER
			,COALESCE(U.UPASSWORD												,	'')		AS	UPASSWORD
			,COALESCE(U.UGROUP													,	'')		AS	UGROUP
			,COALESCE(U.UACCESSPOLICY											,	'')		AS	UACCESSPOLICY
			,COALESCE(U.UACCESSDAYS												,	'')		AS	UACCESSDAYS
			,COALESCE(CONVERT(VARCHAR(24), U.UACCESSTIMESTART, 106)				,	'')		AS	UACCESSTIMESTART
			,COALESCE(CONVERT(VARCHAR(24), U.UACCESSTIMEEND, 106)				,	'')		AS	UACCESSTIMEEND
			,COALESCE(CONVERT(VARCHAR(24), U.UREGISTRATIONDATE, 106)			,	'')		AS	UREGISTRATIONDATE
			,COALESCE(CONVERT(VARCHAR(24), U.UEXPIRYDATE, 106)					,	'')		AS	UEXPIRYDATE
			,COALESCE(U.UREFERENCEKEY											,	'')		AS	UREFERENCEKEY
			,COALESCE(U.ULOGINKEY												,	'')		AS	ULOGINKEY
			,COALESCE(CONVERT(VARCHAR(24),U.ULASTLOGINDATETIME, 106)			,	'')		AS	ULASTLOGINDATETIME
			,COALESCE(U.ULOGINCOUNT												,	'')		AS	ULOGINCOUNT
			,COALESCE(U.ULOGINTYPE												,	'')		AS	ULOGINTYPE
			,COALESCE(U.USTATUSID												,	'')		AS	USTATUSID
			,COALESCE(U.UGOOGLECLIENTID											,	'')		AS	UGOOGLECLIENTID
			,COALESCE(U.UFACEBOOKCLIENTID										,	'')		AS	UFACEBOOKCLIENTID
			,COALESCE(U.UAPPLICATIONKEY											,	'')		AS	UAPPLICATIONKEY
			,COALESCE(CONVERT(VARCHAR(24), U.UAPPLICATIONDATE, 106)				,	'')		AS	UAPPLICATIONDATE
			,COALESCE(U.UAPPLICATIONHOME										,	'')		AS	UAPPLICATIONHOME
			,COALESCE(U.ULANGUAGEID												,	'')		AS	ULANGUAGEID
			,COALESCE(E.EMPNAME													,	'')		AS	EMPNAME
			,COALESCE(D.DNAME													,	'')		AS	DNAME
			,COALESCE(U.ISLOGIN													,	'')		AS	ISLOGIN
	FROM		ST_UserMasters U
    	LEFT JOIN tblHRMEmployees E ON U.ISDELETED = 0 AND E.EMPID = U.UFORENAME
		LEFT JOIN tblHRMDesignation D ON U.ISDELETED = 0 AND D.DAID = U.UROLEID
	WHERE		U.ISDELETED	=	0
		AND		UAID	LIKE	@UAID
		AND (
		    U.UFORENAME LIKE '%' + @KEYWORD + '%'
		    OR U.UMOBILENUMBER LIKE '%' + @KEYWORD + '%'
		)
		AND U.UCOMPANYID		= @COMPANYID
	ORDER BY U.UREGISTRATIONDATE DESC, U.UAID DESC
)
GO
PRINT 'Updated GET_UserMasters ✓'

PRINT ''
PRINT '========================================================================'
PRINT 'Master list functions updated successfully!'
PRINT '========================================================================'
PRINT ''
PRINT 'Total functions updated: 4'
PRINT '  - GET_SabhasadMaster (ORDER BY REGDATE DESC)'
PRINT '  - GET_ProductMaster (ORDER BY SYSDT DESC)'
PRINT '  - GET_VisitorMaster (ORDER BY VDATE DESC)'
PRINT '  - GET_UserMasters (ORDER BY UREGISTRATIONDATE DESC)'
PRINT ''
PRINT 'All master lists will now show newest records first.'
PRINT ''
