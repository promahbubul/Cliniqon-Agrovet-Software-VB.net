Imports System.Data.SqlClient


Public Class Product
    Dim Con As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\mahbubpc\Documents\cliniqon.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True")
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

    Private Sub Reset()
        ProductNameTb.Text = ""
        PackSizeTb.Text = ""
        TPTakaTb.Text = ""
        MRPTakaTb.Text = ""
        StockTb.Text = ""
        key = 0
    End Sub
    Private Sub UserBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserBtn.Click
        Me.Hide()
        User.Show()
    End Sub
    Private Sub SaveBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveBtn.Click
        If ProductNameTb.Text = "" Or PackSizeTb.Text = "" Or TPTakaTb.Text = "" Or MRPTakaTb.Text = "" Then
            MsgBox("Information Missing")
        Else
            Con.Open()
            Dim query As String
            query = "insert into ProductsTbl values('" & ProductNameTb.Text & "','" & PackSizeTb.Text & "','" & TPTakaTb.Text & "','" & MRPTakaTb.Text & "','" & StockTb.Text & "')"
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Product Saved Successfully")
            Con.Close()
            Populate()
            Reset()
        End If
    End Sub
    Dim Key = 0
    Private Sub ProductDGV_CellMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles ProductDGV.CellMouseClick
        Dim row As DataGridViewRow = ProductDGV.Rows(e.RowIndex)
        ProductNameTb.Text = row.Cells(1).Value.ToString
        PackSizeTb.Text = row.Cells(2).Value.ToString
        TPTakaTb.Text = row.Cells(3).Value.ToString
        MRPTakaTb.Text = row.Cells(4).Value.ToString
        StockTb.Text = row.Cells(5).Value.ToString
        If ProductNameTb.Text = "" Then
            Key = 0
        Else
            Key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub Product_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Populate()
    End Sub

    Private Sub ResetBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResetBtn.Click
        Reset()
    End Sub

    Private Sub DeleteBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteBtn.Click
        If Key = 0 Then
            MsgBox("Select The Product To Be Deleted")
        Else
            Con.Open()
            Dim query As String
            query = "Delete from ProductsTbl where Id=" & Key & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("User Deleted Successfully")
            Con.Close()
            Populate()
        End If
    End Sub

    Private Sub EixtBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EixtBtn.Click
        Application.Exit()
    End Sub

    Private Sub EditBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditBtn.Click
        If ProductNameTb.Text = "" Or PackSizeTb.Text = "" Or TPTakaTb.Text = "" Or MRPTakaTb.Text = "" Then
            MsgBox("Information Missing")
        Else
            Con.Open()
            Dim query As String
            query = "Update ProductsTbl set [Product Name]='" & ProductNameTb.Text & "',[Pack Size]='" & PackSizeTb.Text & "',[T.P Taka]='" & TPTakaTb.Text & "',[M.R.P Taka]='" & MRPTakaTb.Text & "',Stock='" & StockTb.Text & "' where Id=" & Key & " "
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("User Update Successfully")
            Con.Close()
            Populate()
        End If
    End Sub

   
    Private Sub LogoutBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogoutBtn.Click
        Me.Hide()
        Login.Show()

    End Sub

    Private Sub DashboardBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DashboardBtn.Click
        Me.Hide()
        Dashboard1.Show()
    End Sub
End Class