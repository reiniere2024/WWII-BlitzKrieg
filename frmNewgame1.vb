Public Class frmNewgame1

    Private Sub BMount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BMount.Click

        NeworLoadGame = 1
        Me.Close()

    End Sub

  
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        TypeofGame = 1
        Me.Close()


    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        TypeofGame = 2
        Me.Close()


    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        TypeofGame = 4
        NewPBEMGame = True
        Me.Close()


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        NeworLoadGame = 3
        'TypeofGame = 3
        Me.Close()


    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        NeworLoadGame = 4
        TypeofGame = 4
        NewPBEMGame = False
        Me.Close()

    End Sub
End Class