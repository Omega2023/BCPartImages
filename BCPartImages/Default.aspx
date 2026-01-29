<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BCPartImages._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1>Omega Enviromental Technologies Cost</h1>
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Parts Pricing:</h3>
    <ol class="round">
        <li class="eleven" style="height:30px;">
            <asp:Table runat="server" Width="610px">
                <asp:TableRow>
                    <asp:TableCell Width="300">
                        <asp:Label ID="Label100" runat="server" Text="Part #" Font-Bold="true"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Width="20px">
                        <asp:PlaceHolder runat="server" />
                    </asp:TableCell>
                    <asp:TableCell Width="200px">
                        <asp:Label ID="Label200" runat="server" Text="Cost" Font-Bold="true"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </li>
        <li class="one" style="height:30px;">
            <asp:Table runat="server" Width="610px">
                <asp:TableRow>
                    <asp:TableCell Width="300">
                        <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Width="20px">
                        <asp:PlaceHolder runat="server" />
                    </asp:TableCell>
                    <asp:TableCell Width="200px">
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </li>
        <li class="two" style="height:30px;">
            <asp:Table runat="server" Width="610px">
                <asp:TableRow>
                    <asp:TableCell Width="300">
                        <asp:TextBox ID="TextBox2" runat="server" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Width="20px">
                        <asp:PlaceHolder runat="server" />
                    </asp:TableCell>
                    <asp:TableCell Width="200px">
                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </li>
        <li class="three" style="height:30px;">
            <asp:Table runat="server" Width="610px">
                <asp:TableRow>
                    <asp:TableCell Width="300">
                        <asp:TextBox ID="TextBox3" runat="server" OnTextChanged="TextBox3_TextChanged"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Width="20px">
                        <asp:PlaceHolder runat="server" />
                    </asp:TableCell>
                    <asp:TableCell Width="200px">
                        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </li>
        <li class="four" style="height:30px;">
            <asp:Table runat="server" Width="610px">
                <asp:TableRow>
                    <asp:TableCell Width="300">
                        <asp:TextBox ID="TextBox4" runat="server" OnTextChanged="TextBox4_TextChanged"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Width="20px">
                        <asp:PlaceHolder runat="server" />
                    </asp:TableCell>
                    <asp:TableCell Width="200px">
                        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </li>
        <li class="five" style="height:30px;">
            <asp:Table runat="server" Width="610px">
                <asp:TableRow>
                    <asp:TableCell Width="300">
                        <asp:TextBox ID="TextBox5" runat="server" OnTextChanged="TextBox5_TextChanged"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Width="20px">
                        <asp:PlaceHolder runat="server" />
                    </asp:TableCell>
                    <asp:TableCell Width="200px">
                        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </li>
        <li class="six" style="height:30px;">
            <asp:Table runat="server" Width="610px">
                <asp:TableRow>
                    <asp:TableCell Width="300">
                        <asp:TextBox ID="TextBox6" runat="server" OnTextChanged="TextBox6_TextChanged"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Width="20px">
                        <asp:PlaceHolder runat="server" />
                    </asp:TableCell>
                    <asp:TableCell Width="200px">
                        <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </li>
        <li class="seven" style="height:30px;">
            <asp:Table runat="server" Width="610px">
                <asp:TableRow>
                    <asp:TableCell Width="300">
                        <asp:TextBox ID="TextBox7" runat="server" OnTextChanged="TextBox7_TextChanged"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Width="20px">
                        <asp:PlaceHolder runat="server" />
                    </asp:TableCell>
                    <asp:TableCell Width="200px">
                        <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </li>
        <li class="eight" style="height:30px;">
            <asp:Table runat="server" Width="610px">
                <asp:TableRow>
                    <asp:TableCell Width="300">
                        <asp:TextBox ID="TextBox8" runat="server" OnTextChanged="TextBox8_TextChanged"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Width="20px">
                        <asp:PlaceHolder runat="server" />
                    </asp:TableCell>
                    <asp:TableCell Width="200px">
                        <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </li>
        <li class="nine" style="height:30px;">
            <asp:Table runat="server" Width="610px">
                <asp:TableRow>
                    <asp:TableCell Width="300">
                        <asp:TextBox ID="TextBox9" runat="server" OnTextChanged="TextBox9_TextChanged"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Width="20px">
                        <asp:PlaceHolder runat="server" />
                    </asp:TableCell>
                    <asp:TableCell Width="200px">
                        <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </li>
        <li class="ten" style="height:30px;">
            <asp:Table runat="server" Width="610px">
                <asp:TableRow>
                    <asp:TableCell Width="300">
                        <asp:TextBox ID="TextBox10" runat="server" OnTextChanged="TextBox10_TextChanged"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell Width="20px">
                        <asp:PlaceHolder runat="server" />
                    </asp:TableCell>
                    <asp:TableCell Width="200px">
                        <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </li>
        <li class="eleven" style="height:30px;">
            <asp:Table runat="server" Width="610px">
                <asp:TableRow>
                    <asp:TableCell Width="300">
                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                    </asp:TableCell>
                    <asp:TableCell Width="20px">
                        <asp:PlaceHolder runat="server" />
                    </asp:TableCell>
                    <asp:TableCell Width="200px">
                        <asp:Button ID="btnExit" runat="server" OnClick="btnExit_Click" Text="Exit" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </li>
    </ol>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Omega-NAV-2009-AConnectionString %>" SelectCommand="SELECT A.[No_]
          ,A.[Description]
          ,A.[Base Unit of Measure]
          ,C.[Qty_ per Unit of Measure] AS [Base Qty]
          ,A.[Purch_ Unit of Measure]
          ,D.[Qty_ per Unit of Measure] AS [Purch Qty]
          ,A.[Inventory Posting Group]
          ,A.[Costing Method]
          ,A.[Unit Cost]
          ,A.[Standard Cost]
          ,A.[Last Direct Cost]
          ,A.[Vendor No_]
          ,A.[Vendor Item No_]
          ,A.[Reorder Quantity]
          ,A.[Minimum Order Quantity]
          ,COUNT(B.[Direct Unit Cost]) AS [Count of Cost]
      INTO #TEMP1x
      FROM [Omega-NAV-2009-A].[dbo].[Omega Environmental Tech_$Item] A
           LEFT JOIN [Omega-NAV-2009-A].[dbo].[Omega Environmental Tech_$Purchase Price] B
              ON A.No_ = B.[Item No_] AND A.[Vendor No_] = B.[Vendor No_] AND B.[Ending Date] = '1753-01-01'
           LEFT JOIN [Omega-NAV-2009-A].[dbo].[Omega Environmental Tech_$Item Unit of Measure] C
              ON A.No_ = C.[Item No_] AND A.[Base Unit of Measure] = C.Code
           LEFT JOIN [Omega-NAV-2009-A].[dbo].[Omega Environmental Tech_$Item Unit of Measure] D
              ON A.No_ = D.[Item No_] AND A.[Purch_ Unit of Measure] = D.Code
     WHERE A.Blocked = 0 
     AND (A.[No_] = 'MT0390'
     OR A.[No_] = 'MT0505'
     OR A.[No_] = 'MT0623'
     OR A.[No_] = 'MT0633'
     OR A.[No_] = 'MT1030'
     OR A.[No_] = 'MT1814'
     OR A.[No_] = 'MT1818'
     OR A.[No_] = 'MT1820'
     OR A.[No_] = 'MT2033'
     OR A.[No_] = 'MT2038'
     OR A.[No_] = 'MT2144'
     OR A.[No_] = 'MT2149'
     OR A.[No_] = 'MT2302'
     OR A.[No_] = 'MT2337'
     OR A.[No_] = 'MT2346'
     OR A.[No_] = '37-13572-AM'
     OR A.[No_] = '20-04042-AM'
     OR A.[No_] = 'P96-25536')
       /*AND A.No_ IN ('29-21905','20-01233','20-21946-AM','A10-5109')*/
     GROUP BY A.No_,A.Description, A.[Base Unit of Measure],C.[Qty_ per Unit of Measure],A.[Purch_ Unit of Measure],D.[Qty_ per Unit of Measure], 
                 A.[Inventory Posting Group], A.[Costing Method], A.[Unit Cost], A.[Standard Cost], A.[Last Direct Cost], A.[Vendor No_], 
                 A.[Vendor Item No_], A.[Reorder Quantity], A.[Minimum Order Quantity]
     ORDER BY A.No_

    SELECT A.[No_]
          ,A.[Base Unit of Measure]
          ,B.[Unit of Measure Code] AS [Ldgr UOM]
          ,MAX(B.[Entry No_]) AS [Entry No_]
      INTO #TEMP1y
      FROM #TEMP1x A
           LEFT JOIN [Omega-NAV-2009-A].[dbo].[Omega Environmental Tech_$Item Ledger Entry] B
              ON A.[No_] = B.[Item No_] AND B.[Entry Type] = 0
     GROUP BY A.[No_], A.[Base Unit of Measure], B.[Unit of Measure Code]
     ORDER BY A.[No_]

    SELECT A.[No_]
          ,A.[Entry No_]
          ,A.[Ldgr UOM]
          ,B.[Cost per Unit] AS [Last Direct Cost]
      INTO #TEMP1z
      FROM #TEMP1y A
           LEFT JOIN [Omega-NAV-2009-A].[dbo].[Omega Environmental Tech_$Value Entry] B
              ON A.[No_] = B.[Item No_] AND A.[Entry No_] = B.[Item Ledger Entry No_] AND B.[Item Ledger Entry Type] = 0 AND B.[Document Type] = 6


    SELECT A.[No_]
          ,A.[Description]
          ,A.[Base Unit of Measure]
          ,A.[Inventory Posting Group]
          ,A.[Costing Method]
          ,A.[Unit Cost]
          ,A.[Standard Cost]
          ,CASE
              WHEN C.[Last Direct Cost] IS NULL THEN 0
              ELSE C.[Last Direct Cost]
           END AS [Last Direct Cost]
          ,A.[Vendor No_]
          ,A.[Vendor Item No_]
          ,A.[Reorder Quantity]
          ,A.[Minimum Order Quantity]
          ,A.[Count of Cost]
          ,AVG(B.[Quantity]) AS [Average Order]
      INTO #TEMP2x
      FROM #TEMP1x A
           LEFT JOIN [Omega-NAV-2009-A].[dbo].[Omega Environmental Tech_$Item Ledger Entry] B
              ON A.No_ = B.[Item No_] AND A.[Vendor No_] = B.[Source No_] AND A.[Vendor Item No_] = B.[Cross-Reference No_] 
                 AND B.[Posting Date] &gt;= (GETDATE() - 730) AND B.[Entry Type] = 0
           LEFT JOIN #TEMP1z C
              ON A.No_ = C.No_
     GROUP BY A.[No_],A.[Description],A.[Base Unit of Measure],A.[Inventory Posting Group],A.[Costing Method],A.[Unit Cost],A.[Standard Cost]
          ,C.[Last Direct Cost],A.[Vendor No_],A.[Vendor Item No_],A.[Reorder Quantity],A.[Minimum Order Quantity],A.[Count of Cost]

    SELECT A.[No_]
          ,A.[Description]
          ,A.[Base Unit of Measure]
          ,A.[Inventory Posting Group]
          ,A.[Costing Method]
          ,A.[Unit Cost]
          ,A.[Standard Cost]
          ,A.[Last Direct Cost]
          ,A.[Vendor No_]
          ,A.[Vendor Item No_]
          ,A.[Reorder Quantity]
          ,A.[Minimum Order Quantity]
          ,A.[Count of Cost]
          ,CASE
              WHEN A.[Average Order] IS NULL THEN 0
              ELSE A.[Average Order]
           END AS [Average Order]
      INTO #TEMP2
      FROM #TEMP2x A
     ORDER BY A.No_

    SELECT A.[No_]
          ,A.[Description]
          ,A.[Base Unit of Measure]
          ,A.[Inventory Posting Group]
          ,A.[Costing Method]
          ,A.[Unit Cost]
          ,A.[Standard Cost]
          ,A.[Last Direct Cost]
          ,A.[Vendor No_]
          ,A.[Vendor Item No_]
          ,A.[Reorder Quantity]
          ,A.[Minimum Order Quantity]
          ,A.[Count of Cost]
          ,A.[Average Order]
          ,MAX(B.[Minimum Quantity]) AS [Minimum Quantity]
      INTO #TEMP3 
      FROM #TEMP2 A
           JOIN [Omega-NAV-2009-A].[dbo].[Omega Environmental Tech_$Purchase Price] B
              ON A.No_ = B.[Item No_] AND A.[Vendor No_] = B.[Vendor No_] AND A.[Average Order] &gt;= B.[Minimum Quantity] AND B.[Ending Date] = '1753-01-01'
     WHERE A.[Count of Cost] &gt; 1
     GROUP BY A.[No_],A.[Description],A.[Base Unit of Measure],A.[Inventory Posting Group],A.[Costing Method],A.[Unit Cost],A.[Standard Cost]
          ,A.[Last Direct Cost],A.[Vendor No_],A.[Vendor Item No_],A.[Reorder Quantity],A.[Minimum Order Quantity],A.[Count of Cost], A.[Average Order]
     ORDER BY A.No_

    SELECT A.[No_]
          ,A.[Description]
          ,A.[Base Unit of Measure]
          ,A.[Inventory Posting Group]
          ,A.[Costing Method]
          ,A.[Unit Cost]
          ,A.[Standard Cost]
          ,A.[Last Direct Cost]
          ,A.[Vendor No_]
          ,A.[Vendor Item No_]
          ,A.[Reorder Quantity]
          ,A.[Minimum Order Quantity]
          ,A.[Count of Cost]
          ,A.[Average Order]
      INTO #TEMP3x
      FROM #TEMP2 A
     WHERE A.[Count of Cost] &gt; 1
       AND A.No_ NOT IN (SELECT B.No_ FROM #TEMP3 B)

    SELECT A.[No_]
          ,A.[Base Unit of Measure]
          ,A.[Costing Method]
          ,A.[Unit Cost]
          ,A.[Standard Cost]
          ,A.[Last Direct Cost]
          ,A.[Vendor No_]
          ,A.[Vendor Item No_]
          ,B.[Direct Unit Cost]
          ,A.[Average Order]
          ,A.[Count of Cost]
      INTO #TEMP4
      FROM #TEMP3 A
           JOIN [Omega-NAV-2009-A].[dbo].[Omega Environmental Tech_$Purchase Price] B
              ON A.No_ = B.[Item No_] AND A.[Vendor No_] = B.[Vendor No_] AND A.[Minimum Quantity] = B.[Minimum Quantity] AND B.[Ending Date] = '1753-01-01'
     UNION
    SELECT A.[No_]
          ,A.[Base Unit of Measure]
          ,A.[Costing Method]
          ,A.[Unit Cost]
          ,A.[Standard Cost]
          ,A.[Last Direct Cost]
          ,A.[Vendor No_]
          ,A.[Vendor Item No_]
          ,0 AS [Direct Unit Cost]
          ,A.[Average Order]
          ,A.[Count of Cost]
      FROM #TEMP3x A
     UNION
    SELECT A.[No_]
          ,A.[Base Unit of Measure]
          ,A.[Costing Method]
          ,A.[Unit Cost]
          ,A.[Standard Cost]
          ,A.[Last Direct Cost]
          ,A.[Vendor No_]
          ,A.[Vendor Item No_]
          ,B.[Direct Unit Cost]
          ,A.[Average Order]
          ,A.[Count of Cost]
      FROM #TEMP2 A
           LEFT JOIN [Omega-NAV-2009-A].[dbo].[Omega Environmental Tech_$Purchase Price] B
              ON A.No_ = B.[Item No_] AND A.[Vendor No_] = B.[Vendor No_] AND B.[Ending Date] = '1753-01-01'
     WHERE A.[Count of Cost] &lt;= 1
     ORDER BY A.No_

    SELECT A.[No_]
          ,A.[Base Unit of Measure]
          ,A.[Costing Method]
          ,A.[Unit Cost]
          ,A.[Standard Cost]
          ,A.[Last Direct Cost]
          ,A.[Vendor No_]
          ,A.[Vendor Item No_]
          ,A.[Direct Unit Cost]
          ,A.[Average Order]
          ,A.[Count of Cost]
          ,CASE
              WHEN A.[Costing Method] = 4 THEN A.[Standard Cost]
              WHEN A.[Direct Unit Cost] IS NOT NULL AND A.[Direct Unit Cost] &gt; 0 THEN A.[Direct Unit Cost]
              WHEN A.[Last Direct Cost] &gt; 0 THEN A.[Last Direct Cost]
              WHEN A.[Unit Cost] &gt; 0 THEN A.[Unit Cost]
              ELSE A.[Standard Cost]
           END AS [Cost]
      FROM #TEMP4 A
     ORDER BY A.No_


    DROP TABLE #TEMP1x
    DROP TABLE #TEMP1y
    DROP TABLE #TEMP1z
    DROP TABLE #TEMP2x
    DROP TABLE #TEMP2
    DROP TABLE #TEMP3
    DROP TABLE #TEMP3x
    DROP TABLE #TEMP4
    "></asp:SqlDataSource>
</asp:Content>
