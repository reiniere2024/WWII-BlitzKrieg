Public Class Form1
    Public m_PanStartPoint As New Point


    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
        'detect up arrow key

        If keyData = Keys.Up Or keyData = Keys.W Then
            If StartPosy - DeltaY >= 0 Then
                StartPosx = StartPosx
                StartPosy = StartPosy - DeltaY

                Panel1.AutoScrollPosition = New Drawing.Point((StartPosx), (StartPosy))

            End If
            Return True
        End If
        'detect down arrow key
        If keyData = Keys.Down Or keyData = Keys.S Then
            StartPosx = StartPosx
            StartPosy = StartPosy + DeltaY

            Panel1.AutoScrollPosition = New Drawing.Point((StartPosx), (StartPosy))


            Return True
        End If
        'detect left arrow key
        If keyData = Keys.Left Or keyData = Keys.A Then

            If StartPosx - DeltaX >= 0 Then
                StartPosx = StartPosx - DeltaX
                StartPosy = StartPosy

                Panel1.AutoScrollPosition = New Drawing.Point((StartPosx), (StartPosy))
            End If

            Return True
        End If
        'detect right arrow key
        If keyData = Keys.Right Or keyData = Keys.D Then

            StartPosx = StartPosx + DeltaX
            StartPosy = StartPosy

            Panel1.AutoScrollPosition = New Drawing.Point((StartPosx), (StartPosy))

            Return True
        End If

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function



    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer

        'Me.MaximumSize = New Size(1920, 1080)
        'Me.WindowState = FormWindowState.Maximized

        'Panel Settings
        Panel1.AutoScroll = True
        'Picture Box Settings
        'pb1.SizeMode = PictureBoxSizeMode.AutoSize

        StartPosx = Panel1.AutoScrollPosition.X
        StartPosy = Panel1.AutoScrollPosition.Y

        i = 1



    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub pb1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pb1.Click

    End Sub

    Private Sub pb1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pb1.MouseDown

        'Capture the initial point 
        m_PanStartPoint = New Point(e.X, e.Y)


    End Sub

    Private Sub pb1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pb1.MouseMove

        'Verify Left Button is pressed while the mouse is moving
        If e.Button = Windows.Forms.MouseButtons.Left Then

            'Here we get the change in coordinates.
            Dim DeltaX As Integer = (m_PanStartPoint.X - e.X)
            Dim DeltaY As Integer = (m_PanStartPoint.Y - e.Y)

            'Then we set the new autoscroll position.
            'ALWAYS pass positive integers to the panels autoScrollPosition method
            Panel1.AutoScrollPosition = New Drawing.Point((DeltaX - Panel1.AutoScrollPosition.X), (DeltaY - Panel1.AutoScrollPosition.Y))


        End If

    End Sub
End Class
