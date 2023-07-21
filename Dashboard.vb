Public Class Dashboard
    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked
    End Sub

    Private Sub ExitBar_Click(sender As Object, e As EventArgs) Handles ExitBar.Click
        End
    End Sub

    Private Sub LoginBar_Click(sender As Object, e As EventArgs) Handles LoginBar.Click
        LoginForm.ShowDialog()
    End Sub

    Private Sub DataBarangBar_Click(sender As Object, e As EventArgs) Handles DataBarangBar.Click
        BarangForm.ShowDialog()
    End Sub

    Private Sub DataCustomerBar_Click(sender As Object, e As EventArgs) Handles DataCustomerBar.Click
        CustomerForm.ShowDialog()
    End Sub

    Private Sub DataTransaksiBar_Click(sender As Object, e As EventArgs) Handles DataTransaksiBar.Click
        TransaksiForm.ShowDialog()
    End Sub
    Private Sub LogoutBar_Click(sender As Object, e As EventArgs) Handles LogoutBar.Click
        MatikanSemuaMenu()
    End Sub

    Private Sub MatikanSemuaMenu()
        DataBarangBar.Visible = False
        DataCustomerBar.Visible = False
        DataTransaksiBar.Visible = False
        LogoutBar.Visible = False
        LoginBar.Visible = True
    End Sub

    Private Sub Form_utama_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MatikanSemuaMenu()
    End Sub

    Private Sub Form1_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        'Jika user login adalah Admin
        If (admin_role = True) Then
            DataBarangBar.Visible = True
            DataCustomerBar.Visible = True
            DataTransaksiBar.Visible = True
            LoginBar.Visible = False
            LogoutBar.Visible = True
        End If

    End Sub

    Private Sub DataCustonerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataCustomerBar.Click

    End Sub

    Private Sub DataBarangBar_Click_1(sender As Object, e As EventArgs) Handles DataBarangBar.Click

    End Sub
End Class