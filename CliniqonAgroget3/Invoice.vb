Imports System.Data.SqlClient
Public Class Invoice
    Dim Con As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\mahbubpc\Documents\cliniqon.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True")
    Public Property UserName As String
    Private Sub Populate()
        Con.Open()
        Dim query = "select * from ProductsTbl"
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(query, Con)
        Dim builder As SqlCommandBuilder
        builder = New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        ProductDGV.DataSource = ds.Tables(0)
        Con.Close()

    End Sub
    Private Sub Invoice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Populate()
        UserLbl.Text = UserName
    End Sub
    Dim key = 0, Stock = 0, i = 0, GrandTotal = 0
    Private Sub ProductDGV_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ProductDGV.CellContentClick, InvoiceDGV.CellContentClick
        Dim row As DataGridViewRow = ProductDGV.Rows(e.RowIndex)
        ProductNameTb.Text = row.Cells(1).Value.ToString
        PackSizeTb.Text = row.Cells(2).Value.ToString
        TPTb.Text = row.Cells(3).Value.ToString
        MRPTb.Text = row.Cells(4).Value.ToString
        Stock = Convert.ToInt32(row.Cells(5).Value.ToString)
        If ProductNameTb.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub EixtBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EixtBtn.Click
        Application.Exit()

    End Sub
    Private Sub Update()
        Dim NewQuantity = Stock - Convert.ToInt32(QuantityTb.Text)
        Con.Open()
        Dim query As String
        query = "Update ProductsTbl set Stock='" & NewQuantity & "' where Id=" & key & " "
        Dim cmd As SqlCommand
        cmd = New SqlCommand(query, Con)
        cmd.ExecuteNonQuery()
        MsgBox("User Update Successfully")
        Con.Close()
        Populate()
    End Sub

    Private Sub AddBillBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddBillBtn.Click
        If QuantityTb.Text = "" Then
            MsgBox("Enter the Quantity")
        ElseIf ProductNameTb.Text = "" Then
            MsgBox("Select the Product")
        ElseIf Convert.ToInt32(QuantityTb.Text) > Stock Then
            MsgBox("Not Enough Stock")
        Else
            Dim rnum As Integer = InvoiceDGV.Rows.Add()
            i = i + 1
            Dim total = Convert.ToInt32(QuantityTb.Text) * Convert.ToInt32(TPTb.Text)
            InvoiceDGV.Rows.Item(rnum).Cells("Column1").Value = i
            InvoiceDGV.Rows.Item(rnum).Cells("Column2").Value = ProductNameTb.Text
            InvoiceDGV.Rows.Item(rnum).Cells("Column3").Value = PackSizeTb.Text
            InvoiceDGV.Rows.Item(rnum).Cells("Column4").Value = QuantityTb.Text
            InvoiceDGV.Rows.Item(rnum).Cells("Column5").Value = TPTb.Text
            InvoiceDGV.Rows.Item(rnum).Cells("Column6").Value = BonusTb.Text
            InvoiceDGV.Rows.Item(rnum).Cells("Column7").Value = total
            GrandTotal = GrandTotal + total
            Dim Tot As String
            Tot = "Taka " + Convert.ToString(GrandTotal)
            TotalLb.Text = Tot
            Update()

        End If
    End Sub

    Private Sub Reset()
        key = 0
        ProductNameTb.Text = ""
        PackSizeTb.Text = ""
        QuantityTb.Text = ""
        TPTb.Text = ""
        MRPTb.Text = ""
        BonusTb.Text = ""
    End Sub

    Private Sub ResetBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResetBtn.Click
        Reset()
    End Sub

    Private Sub PrintBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintBtn.Click
        PrintPreviewDialog1.Show()
        AddBill()

    End Sub

    Private Sub AddBill()
            Con.Open()
            Dim query As String
            query = "insert into InvoiceTbl values('" & UserLbl.Text & "','" & ClientNameTb.Text & "','" & GrandTotal & "')"
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Invoice Saved Successfully")
        Con.Close()
    End Sub

    Private Sub PrintPreviewDialog1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub PrintDocument1_PrintPage_1(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        'e.Graphics.DrawString("Cliniqon Agrovet Pvt. Ltd.", New Font("Century Gothic", 25), Brushes.MidnightBlue, 350, 40)
        e.Graphics.DrawString("==============Your Bill==============", New Font("Century Gothic", 16), Brushes.MidnightBlue, 300, 40)
        Dim bm As New Bitmap(Me.InvoiceDGV.Width, Me.InvoiceDGV.Height)
        InvoiceDGV.DrawToBitmap(bm, New Rectangle(0, 0, Me.InvoiceDGV.Width, Me.InvoiceDGV.Height))
        e.Graphics.DrawImage(bm, 60, 120)
        e.Graphics.DrawString("Total Amount Tk " + GrandTotal.ToString, New Font("Century Gothic", 15), Brushes.MidnightBlue, 280, 500)
        e.Graphics.DrawString("==============Buying in our Shop==============", New Font("Century Gothic", 16), Brushes.Crimson, 150, 580)
    End Sub

    Private Sub ProductBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProductBtn.Click, InvoiceBtn.Click
        Product.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dashboard1.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Login.Show()
        Me.Hide()
    End Sub

    Private Sub UserBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserBtn.Click
        User.Show()
        Me.Hide()
    End Sub
End Class