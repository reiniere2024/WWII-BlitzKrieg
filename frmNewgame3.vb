Public Class frmNewgame3

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click

        GameMode = 0
        Me.Close()

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        GameMode = 1
        Me.Close()

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        GameMode = 2
        Me.Close()

    End Sub


    Private Sub cbAllies1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbAllies1.Click

        cbAllies1.Checked = True
        cbAllies2.Checked = False
        cbAllies3.Checked = False
        GameMode = 0

    End Sub


    Private Sub cbAllies2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbAllies2.Click

        cbAllies1.Checked = False
        cbAllies2.Checked = True
        cbAllies3.Checked = False
        GameMode = 1

    End Sub


    Private Sub cbAllies3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbAllies3.Click

        cbAllies1.Checked = False
        cbAllies2.Checked = False
        cbAllies3.Checked = True
        GameMode = 2

    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()

    End Sub

    Private Sub cbAllies2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAllies2.CheckedChanged

    End Sub

    Private Sub frmNewgame3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        GameMode = 1

    End Sub
End Class