Imports System.Data.SqlClient
Public Class Login
    Dim Con As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\mahbubpc\Documents\cliniqon.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True")
    Dim cmd As SqlCommand
    Private Sub EixtBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EixtBtn.Click
        Application.Exit()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If UsernameTb.Text = "" And PasswordTb.Text = "" Then
            MsgBox("Enter Username and Password")
        ElseIf UsernameTb.Text = "" Then
            MsgBox("Enter Username")
        ElseIf PasswordTb.Text = "" Then
            MsgBox("Enter Password")
        Else
            Con.Open()
            Dim query = "select * from UsersTbl where Name='" & UsernameTb.Text & "' and Password='" & PasswordTb.Text & "'"
            cmd = New SqlCommand(query, Con)
            Dim sda As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim ds As DataSet = New DataSet()
            sda.Fill(ds)
            Dim a As Integer
            a = ds.Tables(0).Rows.Count
            If a = 0 Then
                MsgBox("Wrong Username or Password")
            Else
                Dim Invoice = New Invoice
                Invoice.UserName = UsernameTb.Text
                Invoice.Show()
                Me.Hide()
            End If
            Con.Close()

        End If

    End Sub
End Class