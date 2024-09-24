Public Class frmNewgame2

    Private Sub BMount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BMount.Click

        GameMode = 1
        TypeofGame = 1
        Me.Close()

    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        TypeofGame = 2
        Me.Close()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        GameMode = 1
        TypeofGame = 4
        NewPBEMGame = True
        Me.Close()

    End Sub
End Class