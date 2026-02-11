USE [InventoryManagement]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Fix GET_CropDirectInvertDetail Function
-- Add RATE column to the output
-- =============================================

CREATE OR ALTER FUNCTION [dbo].[GET_CropDirectInvertDetail]
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
         COALESCE(D.CDAID                              , '')                AS CDAID
        ,COALESCE(D.CROPID                             , '')                AS CROPID
        ,COALESCE(D.SHAID                              , '')                AS SHAID
        ,COALESCE(D.SHDAID                             , '')                AS SHDAID
        ,COALESCE(CONVERT(VARCHAR(16), D.SHSDATE, 106) , '')                AS SHSDATE
        ,COALESCE(D.VEHNO                              , '')                AS VEHNO
        ,COALESCE(D.PACKER                             , '')                AS PACKER
        ,COALESCE(D.WORKER                             , '')                AS WORKER
        ,COALESCE(D.PSIZE                              , '')                AS PSIZE
        ,COALESCE(D.PQUNTITY                           , '')                AS PQUNTITY
        ,COALESCE(D.WEIGHT                             , 0)                 AS WEIGHT
        ,COALESCE(D.RATE                               , 0)                 AS RATE
        ,COALESCE(D.CRATECOUNT                         , '')                AS CRATECOUNT
        ,COALESCE(D.CROPINVERT                         , '')                AS CROPINVERT
        ,COALESCE(D.CGRADE                             , '')                AS CGRADE
        ,COALESCE(D.VDAID                              , '')                AS VDAID
    FROM tblCropDirectInvertDetail D
    WHERE D.CROPID LIKE @CROPID
        AND D.COMPANYID = @COMPANYID
        AND D.DEPTID = @DEPTID
        AND D.ISDELETED = 0
)
GO

PRINT 'GET_CropDirectInvertDetail function updated successfully with RATE column!'
GO
