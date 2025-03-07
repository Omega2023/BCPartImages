USE [Omega_BC18_PROD]

/*****************************************************************************
PROGRAM: PartImages.sql
REV DATE: 2025-01-02
*****************************************************************************/

IF OBJECT_ID(N'tempdb..#PASS1', N'U') IS NOT NULL                    DROP TABLE #PASS1;
IF OBJECT_ID(N'tempdb..#PASS2', N'U') IS NOT NULL                    DROP TABLE #PASS2;

DECLARE @PickTicket VARCHAR(20)
SET @PickTicket = '19425051'

SELECT DISTINCT ISNULL(ITM.[No_],'') AS [MasterItem]
      ,ISNULL(ITM.[No_ 2],'') AS [Item]
      ,ISNULL(ITM.[Description],'') AS [Description]
      ,ISNULL(ITM.[Search Description],'') AS [SrchDesc]
      ,ISNULL(ITM.[Description 2],'') AS [Description2]
      ,ISNULL(ITM.[Base Unit of Measure],'') AS [BaseUOI]
      ,ISNULL(ITM.[Vendor No_],'') AS [VendorNo]
      ,ISNULL(ITM.[Vendor Item No_],'') AS [VendorItemNo]
      ,ISNULL(ITM.[Alternative Item No_],'') AS [LblItem]
      ,ITM.[Gross Weight] AS [GrsWeight]
      ,ITM.[Net Weight] AS [NetWeight]
      ,IUM.[Qty_ per Unit of Measure] AS [PkgQty]
      ,ITM.[Unit Volume]AS [Cubage]
      ,ISNULL(ITM.[Country_Region Purchased Code],'') AS [BoughtIn]
--    ,ISNULL(ITM.[Picture],'') AS [Image]
      ,'' AS [Image]
      ,ISNULL(ITM.[Country_Region of Origin Code],'') AS [Country]
      ,ISNULL(ITM.[GTIN],'') AS [BarCode]
      ,ISNULL(ITM.[Sales Unit of Measure],'') AS [SaleUOI]
      ,ISNULL(ITM.[Item Category Code],'') AS [ItemCategory]
      ,ISNULL(ITM.[Routing No_],'') AS [Routing]
      ,ISNULL(ITM.[SAT Hazardous Material],'') AS [Hazard]
      ,ISNULL(IUM.[Code],'') AS [PkgType]
      ,ISNULL(XRF.[Item No_],'') AS [MasterItem2]
      ,ISNULL(XRF.[Variant Code],'') AS [VariantCode2]
      ,ISNULL(XRF.[Unit of Measure],'') AS [BaseUOI2]
      ,ISNULL(XRF.[Reference Type],'') AS [XRefType2]
      ,XRF.[Reference Type No_] AS [Customer2]
      ,XRF.[Reference No_] AS [XRef2]
      ,ISNULL(XRF.[Description],'') AS [XRefDesc]
      ,ISNULL(XRF.[Description 2],'') AS [LongDesc]
      ,ISNULL(XRF.[Item No_],'') AS [MasterItem3]
      ,ISNULL(XRF.[Variant Code],'') AS [VariantCode3]
      ,ISNULL(XRF.[Unit of Measure],'') AS [BaseUOI3]
      ,ISNULL(XRF.[Reference Type],'') AS [XRefType3]
      ,ISNULL(XRF.[Reference Type No_],'') AS [Customer3]
      ,XRF.[Reference No_] AS [XRef]
      ,XREF.[Label Bar Code EAN] AS [BarCode3]
      --,CASE WHEN XREF.[Label Bar Code EAN] = '' AND XREF.[Reference No_] = 'UPC-OMEGA' THEN XREF.[Reference No_]
      -- ELSE XREF.[Label Bar Code EAN]
      -- END AS [BarCode3]
      ,ISNULL(XREF.[Label Description 1],'') AS [LblDesc1]
      ,ISNULL(XREF.[Label Description 2],'') AS [LblDesc2]
      ,ISNULL(XREF.[Label Description 3],'') AS [LblDesc3]
      ,ISNULL(XREF.[Label Description 4],'') AS [LblDesc4]
      ,ISNULL(XREF.[Label Description 5],'') AS [LblDesc5]
      ,ISNULL(XREF.[Label Description 6],'') AS [LblDesc6]
      ,ISNULL(XREF.[Label Description 7],'') AS [LblDesc7]
      ,ISNULL(XREF.[Label Description 8],'') AS [LblDesc8]
      ,ISNULL(XREF.[Label Description 9],'') AS [LblDesc9]
      ,ISNULL(XREF.[Label Description 10],'') AS [LblDesc10]
      ,ISNULL(XREF.[Label Form 1],'') AS [LblForm1]
      ,ISNULL(XREF.[Label Form 2],'') AS [LblForm2]
      ,XREF.[Label Qty_ 1] AS [LblQty1]
      ,XREF.[Label Qty_ 2] AS [LblQty2]
      ,CASE WHEN CUST.[Language Code] IS NOT NULL AND CUST.[Language Code] <> '' THEN CUST.[Language Code] ELSE 'ENU' END AS [Language]
  INTO #PASS1
  FROM [OAC$Item Reference$437dbf0e-84ff-417a-965d-ed2bb9650972] (nolock) XRF
    LEFT JOIN [OAC$Item Reference$d0c9f808-deed-4949-86dc-d19b58410d08] (nolock) XREF
       ON XRF.[Item No_] = XREF.[Item No_] AND XRF.[Reference Type No_] = XREF.[Reference Type No_]
    LEFT JOIN [OAC$Item$437dbf0e-84ff-417a-965d-ed2bb9650972] (nolock) ITM
       ON XRF.[Item No_] = ITM.[No_] AND XRF.[Unit of Measure] = ITM.[Base Unit of Measure]
          LEFT JOIN [OAC$Item Unit of Measure$437dbf0e-84ff-417a-965d-ed2bb9650972] (nolock) IUM
             ON ITM.[No_] = IUM.[Item No_] AND ITM.[Base Unit of Measure] = IUM.[Code]
       LEFT JOIN [OAC$Customer$437dbf0e-84ff-417a-965d-ed2bb9650972] (nolock) CUST
          ON XRF.[Reference Type No_] = CUST.[No_]
 WHERE (ITM.[No_ 2] = @PickTicket OR XRF.[Reference No_] = @PickTicket OR XRF.[Reference No_] = @PickTicket)
 ORDER BY XREF.[Label Bar Code EAN] DESC, XRF.[Reference No_], XRF.[Reference Type No_]

/*
SELECT * FROM #PASS1
DROP TABLE #PASS1
*/

SELECT DISTINCT ISNULL(ITM.[No_],'') AS [MasterItem]
      ,ISNULL(ITM.[No_ 2],'') AS [Item]
      ,ISNULL(ITM.[Description],'') AS [Description]
      ,ISNULL(ITM.[Search Description],'') AS [SrchDesc]
      ,ISNULL(ITM.[Description 2],'') AS [Description2]
      ,ISNULL(ITM.[Base Unit of Measure],'') AS [BaseUOI]
      ,ISNULL(ITM.[Vendor No_],'') AS [VendorNo]
      ,ISNULL(ITM.[Vendor Item No_],'') AS [VendorItemNo]
      ,ISNULL(ITM.[Alternative Item No_],'') AS [LblItem]
      ,ITM.[Gross Weight] AS [GrsWeight]
      ,ITM.[Net Weight] AS [NetWeight]
      ,IUM.[Qty_ per Unit of Measure] AS [PkgQty]
      ,ITM.[Unit Volume]AS [Cubage]
      ,ISNULL(ITM.[Country_Region Purchased Code],'') AS [BoughtIn]
--    ,ISNULL(ITM.[Picture],'') AS [Image]
      ,'' AS [Image]
      ,ISNULL(ITM.[Country_Region of Origin Code],'') AS [Country]
      ,ISNULL(ITM.[GTIN],'') AS [BarCode]
      ,ISNULL(ITM.[Sales Unit of Measure],'') AS [SaleUOI]
      ,ISNULL(ITM.[Item Category Code],'') AS [ItemCategory]
      ,ISNULL(ITM.[Routing No_],'') AS [Routing]
      ,ISNULL(ITM.[SAT Hazardous Material],'') AS [Hazard]
      ,ISNULL(IUM.[Code],'') AS [PkgType]
      ,ISNULL(XRF.[Item No_],'') AS [MasterItem2]
      ,ISNULL(XRF.[Variant Code],'') AS [VariantCode2]
      ,ISNULL(XRF.[Unit of Measure],'') AS [BaseUOI2]
      ,ISNULL(XRF.[Reference Type],'') AS [XRefType2]
      ,XRF.[Reference Type No_] AS [Customer2]
      ,XRF.[Reference No_] AS [XRef2]
      ,ISNULL(XRF.[Description],'') AS [XRefDesc]
      ,ISNULL(XRF.[Description 2],'') AS [LongDesc]
      ,ISNULL(XRF.[Item No_],'') AS [MasterItem3]
      ,ISNULL(XRF.[Variant Code],'') AS [VariantCode3]
      ,ISNULL(XRF.[Unit of Measure],'') AS [BaseUOI3]
      ,ISNULL(XRF.[Reference Type],'') AS [XRefType3]
      ,ISNULL(XRF.[Reference Type No_],'') AS [Customer3]
      ,XRF.[Reference No_] AS [XRef]
      ,XREF.[Label Bar Code EAN] AS [BarCode3]
      --,CASE WHEN XREF.[Label Bar Code EAN] = '' AND XREF.[Reference No_] = 'UPC-OMEGA' THEN XREF.[Reference No_]
      -- ELSE XREF.[Label Bar Code EAN]
      -- END AS [BarCode3]
      ,ISNULL(XREF.[Label Description 1],'') AS [LblDesc1]
      ,ISNULL(XREF.[Label Description 2],'') AS [LblDesc2]
      ,ISNULL(XREF.[Label Description 3],'') AS [LblDesc3]
      ,ISNULL(XREF.[Label Description 4],'') AS [LblDesc4]
      ,ISNULL(XREF.[Label Description 5],'') AS [LblDesc5]
      ,ISNULL(XREF.[Label Description 6],'') AS [LblDesc6]
      ,ISNULL(XREF.[Label Description 7],'') AS [LblDesc7]
      ,ISNULL(XREF.[Label Description 8],'') AS [LblDesc8]
      ,ISNULL(XREF.[Label Description 9],'') AS [LblDesc9]
      ,ISNULL(XREF.[Label Description 10],'') AS [LblDesc10]
      ,ISNULL(XREF.[Label Form 1],'') AS [LblForm1]
      ,ISNULL(XREF.[Label Form 2],'') AS [LblForm2]
      ,XREF.[Label Qty_ 1] AS [LblQty1]
      ,XREF.[Label Qty_ 2] AS [LblQty2]
      ,CASE WHEN CUST.[Language Code] IS NOT NULL AND CUST.[Language Code] <> '' THEN CUST.[Language Code] ELSE 'ENU' END AS [Language]
  INTO #PASS2
  FROM #PASS1 A
    LEFT JOIN [OAC$Item Reference$437dbf0e-84ff-417a-965d-ed2bb9650972] (nolock) XRF
       ON XRF.[Item No_] = A.[MasterItem]
    LEFT JOIN [OAC$Item Reference$d0c9f808-deed-4949-86dc-d19b58410d08] (nolock) XREF
       ON XRF.[Item No_] = XREF.[Item No_] AND XRF.[Reference Type No_] = XREF.[Reference Type No_]
    LEFT JOIN [OAC$Item$437dbf0e-84ff-417a-965d-ed2bb9650972] (nolock) ITM
       ON XRF.[Item No_] = ITM.[No_] AND XRF.[Unit of Measure] = ITM.[Base Unit of Measure]
          LEFT JOIN [OAC$Item Unit of Measure$437dbf0e-84ff-417a-965d-ed2bb9650972] (nolock) IUM
             ON ITM.[No_] = IUM.[Item No_] AND ITM.[Base Unit of Measure] = IUM.[Code]
       LEFT JOIN [OAC$Customer$437dbf0e-84ff-417a-965d-ed2bb9650972] (nolock) CUST
          ON XRF.[Reference Type No_] = CUST.[No_]
 WHERE (ITM.[No_] = A.[MasterItem] OR XRF.[Item No_] = A.[MasterItem] OR XRF.[Item No_] = A.[MasterItem])
 ORDER BY XREF.[Label Bar Code EAN] DESC, XRF.[Reference No_], XRF.[Reference Type No_]

SELECT * FROM #PASS2 A ORDER BY A.[BarCode3] DESC, A.[XRef], A.[Customer3]

DROP TABLE #PASS1
DROP TABLE #PASS2
