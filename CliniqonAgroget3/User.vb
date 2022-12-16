Imports System.Data.SqlClient
Public Class User
    Dim Con As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\mahbubpc\Documents\cliniqon.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True")
    Private Sub Populate()
        Con.Open()

        Dim query = "select * from UsersTbl"
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(query, Con)
        Dim builder As SqlCommandBuilder
        builder = New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        UserGDV.DataSource = ds.Tables(0)
        Con.Close()

    End Sub

    Private Sub Reset()
        NameTb.Text = ""
        PhoneTb.Text = ""
        AddressTb.Text = ""
        PasswordTb.Text = ""
        Key = 0
    End Sub

    Private Sub SaveBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveBtn.Click
        If NameTb.Text = "" Or PhoneTb.Text = "" Or AddressTb.Text = "" Or PasswordTb.Text = "" Then
            MsgBox("Information Missing")
        Else
            Con.Open()
            Dim query As String
            query = "insert into UsersTbl values('" & NameTb.Text & "','" & PhoneTb.Text & "','" & AddressTb.Text & "','" & PasswordTb.Text & "')"
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("User Saved Successfully")
            Con.Close()
            Populate()
        End If

    End Sub

    Private Sub UserGDV_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles UserGDV.CellContentClick

    End Sub

    Private Sub User_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Populate()
    End Sub

    Private Sub EixtBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EixtBtn.Click
        Application.Exit()

    End Sub
    Dim Key = 0
    Private Sub DeleteBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteBtn.Click
        If Key = 0 Then
            MsgBox("Select The User To Be Deleted")
        Else
            Con.Open()
            Dim query As String
            query = "Delete from UsersTbl where Id=" & Key & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("User Deleted Successfully")
            Con.Close()
            Populate()
        End If
    End Sub

    Private Sub UserGDV_CellMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles UserGDV.CellMouseClick
        Dim row As DataGridViewRow = UserGDV.Rows(e.RowIndex)
        NameTb.Text = row.Cells(1).Value.ToString
        PhoneTb.Text = row.Cells(2).Value.ToString
        AddressTb.Text = row.Cells(3).Value.ToString
        PasswordTb.Text = row.Cells(4).Value.ToString
        If NameTb.Text = "" Then
            Key = 0
        Else
            Key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub ProductBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProductBtn.Click

        Me.Hide()
        Product.Show()
    End Sub

    Private Sub ResetBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResetBtn.Click
        Reset()
    End Sub

    Private Sub EditBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditBtn.Click
        If NameTb.Text = "" Or PhoneTb.Text = "" Or AddressTb.Text = "" Or PasswordTb.Text = "" Then
            MsgBox("Missing Information")
        Else
            Con.Open()
            Dim query As String
            query = "Update UsersTbl set Name='" & NameTb.Text & "',Phone='" & PhoneTb.Text & "',Address='" & AddressTb.Text & "',Password='" & PasswordTb.Text & "' where Id=" & Key & " "
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("User Update Successfully")
            Con.Close()
            Populate
        End If
    End Sub

    
    Private Sub LogoutBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogoutBtn.Click
        Me.Hide()
        Login.Show()

    End Sub

    Private Sub DashboardBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DashboardBtn.Click
        Dashboard1.Show()
        Me.Hide()

    End Sub
End Class