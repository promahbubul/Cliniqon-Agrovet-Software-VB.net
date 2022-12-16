Public Class Form1

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ProgressBar1.Increment(1.5)
        parsentLB.Text = Convert.ToString(ProgressBar1.Value) + "%"

        If ProgressBar1.Value = 100 Then
            Me.Hide()
            Login.Show()
            Timer1.Enabled = False

        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub
End Class
