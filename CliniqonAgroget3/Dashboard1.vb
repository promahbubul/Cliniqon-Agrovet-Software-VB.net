Imports System.Data.SqlClient
Public Class Dashboard1
    Dim Con As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\mahbubpc\Documents\cliniqon.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True")

    'Products List Function
    Private Sub CountProduct()
        Dim ProductNum As Integer
        Con.Open()
        Dim sql = "select COUNT(*) from ProductsTbl"
        Dim cmd As SqlCommand
        cmd = New SqlCommand(sql, Con)
        ProductNum = cmd.ExecuteScalar
        ProductsLb.Text = ProductNum
        Con.Close()
    End Sub

    'Users List Function
    Private Sub CountUsers()
        Dim UsersNum As Integer
        Con.Open()
        Dim sql = "select COUNT(*) from UsersTbl"
        Dim cmd As SqlCommand
        cmd = New SqlCommand(sql, Con)
        UsersNum = cmd.ExecuteScalar
        UsersLb.Text = UsersNum
        Con.Close()
    End Sub
    Private Sub Dashboard1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CountProduct()
        CountUsers()
    End Sub

    Private Sub InvoiceBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InvoiceBtn.Click
        Invoice.Show()
        Me.Hide()
    End Sub

    Private Sub ProductBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProductBtn.Click
        Product.Show()
        Me.Hide()
    End Sub

    Private Sub UserBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserBtn.Click
        User.Show()
        Me.Hide()
    End Sub

    Private Sub DashboardBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DashboardBtn.Click
        Me.Refresh()
    End Sub

    Private Sub LogoutBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogoutBtn.Click
        Login.Show()
        Me.Hide()
    End Sub
End Class