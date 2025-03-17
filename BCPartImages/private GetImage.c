
    private GetImage(ByVal Row As Integer)
    {
        Dim fn As String = "";
        Dim sTmp As String = DataGridView1.Rows(Row).Cells(daCol.Item).Value.ToString();
        Dim MstrPartNum As String = DataGridView1.Rows(Row).Cells(daCol.MasterItem).Value.ToString();
        Dim sTmpImg As String = DataGridView1.Rows(Row).Cells(daCol.Image).Value.ToString();

        sTmp = UCase(DataGridView1.Rows(Row).Cells(daCol.Item).Value.ToString().Trim());
        LogEvent("Get Image sTmp " + sTmp);
        MstrPartNum = DataGridView1.Rows(Row).Cells(daCol.MasterItem).Value.ToString();
        if (MstrPartNum.Equals(String.Empty)) {
            if (Not sTmp.Equals(String.Empty)) {
                MstrPartNum = sTmp;
            } //End If
        } //End If
        LogEvent("Get Image MstrPartNum=" + MstrPartNum);
        if (sTmp.Contains("P96") || sTmp.Contains("RDK") || sTmp.Contains("BK") || sTmp.Contains("78-") ||
           Customer.Equals("SENSOR") ||
           MstrPartNum.Contains("K10") || MstrPartNum.Contains("K14") || MstrPartNum.Contains("H12") ||
           MstrPartNum.Contains("A10") || MstrPartNum.Contains("A12") || MstrPartNum.Contains("E10")) {
            sTmpImg = UCase(DataGridView1.Rows(Row).Cells(daCol.Image).Value.ToString());
            LogEvent("Get Image sTmpImg=" + sTmpImg);
            if (sTmp.Contains("P96") || MstrPartNum.Contains("K10")) {
                if (sTmpImg.Equals(String.Empty)) {
                    if (UCase(Customer).Contains("DTNA") || UCase(Customer).Contains("ALLIAN")) {
                        fn = "\\OmegaFS2\NAVGIF\DTNA\" + sTmp + ".jpg";
                    Else
                        fn = "\\OmegaFS2\NAVGIF\P96-Compressor Kit Photos\" + sTmp + ".jpg"
                    } //End If
                    if (Not File.Exists(fn)) {
                        fn = Replace(fn, ".jpg", ".gif")
                        if (Not File.Exists(fn)) {
                            fn = Replace(fn, ".gif", "-1.jpg")
                            if (Not File.Exists(fn)) {
                                fn = Replace(fn, "-1.jpg", "A.jpg")
                                if (Not File.Exists(fn)) {
                                    LogEvent("Get Image fn " + fn + " Not Found")
                                } //End If
                            } //End If
                        } //End If
                    } //End If
                    LogEvent("Get Image sTmp=" + sTmp + " sTmpImg=" + sTmpImg + " fn=" + fn)
                    DataGridView1.Rows(Row).Cells(daCol.Image).Value = fn
                } //End If
            Elseif (sTmp.Contains("RDK") || MstrPartNum.Contains("K14")) {
                if (sTmpImg.Equals(String.Empty)) {
                    if (UCase(Customer).Contains("DTNA") || UCase(Customer).Contains("ALLIAN")) {
                        fn = "\\OmegaFS2\NAVGIF\DTNA\" + sTmp + ".jpg"
                    Else
                        fn = "\\OmegaFS2\NAVGIF\RDK\" + sTmp + ".jpg"
                    } //End If
                    if (Not File.Exists(fn)) {
                        fn = Replace(fn, ".jpg", ".gif")
                        if (Not File.Exists(fn)) {
                            fn = Replace(fn, ".gif", "-1.jpg")
                            if (Not File.Exists(fn)) {
                                fn = Replace(fn, "-1.jpg", "A.jpg")
                                if (Not File.Exists(fn)) {
                                    LogEvent("Get Image fn " + fn + " Not Found")
                                } //End If
                            } //End If
                        } //End If
                    } //End If
                    LogEvent("Get Image sTmp=" + sTmp + " sTmpImg=" + sTmpImg + " fn=" + fn)
                    DataGridView1.Rows(Row).Cells(daCol.Image).Value = fn
                } //End If
            Elseif (sTmp.Contains("BK") && sTmp.Substring(0, 2).Equals("BK") || MstrPartNum.Contains("H12")) {
                if (sTmpImg.Equals(String.Empty)) {
                    if (UCase(Customer).Contains("DTNA") || UCase(Customer).Contains("ALLIAN")) {
                        fn = "\\OmegaFS2\NAVGIF\DTNA\" + sTmp + ".jpg"
                    Else
                        fn = "\\OmegaFS2\NAVGIF\BMK\" + sTmp + ".jpg"
                    } //End If
                    if (Not File.Exists(fn)) {
                        fn = Replace(fn, ".jpg", ".gif")
                        if (Not File.Exists(fn)) {
                            fn = Replace(fn, ".gif", "-1.jpg")
                            if (Not File.Exists(fn)) {
                                fn = Replace(fn, "-1.jpg", "A.jpg")
                                if (Not File.Exists(fn)) {
                                    LogEvent("Get Image fn " + fn + " Not Found")
                                } //End If
                            } //End If
                        } //End If
                    } //End If
                    LogEvent("Get Image sTmp=" + sTmp + " sTmpImg=" + sTmpImg + " fn=" + fn)
                    DataGridView1.Rows(Row).Cells(daCol.Image).Value = fn
                } //End If
            Elseif (sTmp.Contains("78-") && sTmp.Substring(0, 2).Equals("78") || MstrPartNum.Contains("A10") || MstrPartNum.Contains("A12")) {
                if (sTmpImg.Equals(String.Empty)) {
                    if (UCase(Customer).Contains("DTNA") || UCase(Customer).Contains("ALLIAN")) {
                        fn = "\\OmegaFS2\NAVGIF\DTNA\" + sTmp + ".jpg"
                    Else
                        fn = "\\OmegaFS2\NAVGIF\" + sTmp + ".jpg"
                    } //End If
                    if (Not File.Exists(fn)) {
                        fn = Replace(fn, ".jpg", ".gif")
                        if (Not File.Exists(fn)) {
                            fn = Replace(fn, ".gif", "-1.jpg")
                            if (Not File.Exists(fn)) {
                                fn = Replace(fn, "-1.jpg", "A.jpg")
                                if (Not File.Exists(fn)) {
                                    LogEvent("Get Image fn " + fn + " Not Found")
                                } //End If
                            } //End If
                        } //End If
                    } //End If
                    LogEvent("Get Image sTmp=" + sTmp + " sTmpImg=" + sTmpImg + " fn=" + fn)
                    DataGridView1.Rows(Row).Cells(daCol.Image).Value = fn
                } //End If
            Else
                if (sTmpImg.Equals(String.Empty)) {
                    if (UCase(Customer).Contains("DTNA") || UCase(Customer).Contains("ALLIAN")) {
                        fn = "\\OmegaFS2\NAVGIF\DTNA\" + sTmp + ".jpg"
                    Else
                        fn = "\\OmegaFS2\NAVGIF\" + sTmp + ".jpg"
                    } //End If
                    if (Not File.Exists(fn)) {
                        fn = Replace(fn, ".jpg", ".gif")
                        if (Not File.Exists(fn)) {
                            fn = Replace(fn, ".gif", "-1.jpg")
                            if (Not File.Exists(fn)) {
                                fn = Replace(fn, "-1.jpg", "A.jpg")
                                if (Not File.Exists(fn)) {
                                    LogEvent("Get Image fn " + fn + " Not Found")
                                } //End If
                            } //End If
                        } //End If
                    } //End If
                    LogEvent("Get Image sTmp=" + sTmp + " sTmpImg=" + sTmpImg + " fn=" + fn)
                    DataGridView1.Rows(Row).Cells(daCol.Image).Value = fn
                } //End If
            } //End If
        } //End If
    } //End Sub
