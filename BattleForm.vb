Imports System.IO

Public Class BattleForm

    Public Sub MoveArmy(ByVal direction As String)
        Dim myunittype As Integer

        Select Case direction

            Case "DownLeft"
                If Turn = "Axis" Then
                    Me.MoveDownLeft("DownLeft")
                Else
                    Me.MoveDownLeft2("DownLeft")
                End If
            Case "DownRight"
                If Turn = "Axis" Then
                    Me.MoveDownRight("DownRight")
                Else
                    Me.MoveDownRight2("DownRight")

                End If
            Case "UpRight"
                If Turn = "Axis" Then
                    Me.MoveUpRight("UpRight")

                Else
                    Me.MoveUpRight2("UpRight")

                End If
            Case "UpLeft"
                If Turn = "Axis" Then
                    Me.MoveUpLeft("UpLeft")
                Else
                    Me.MoveUpLeft2("UpLeft")
                End If
            Case "Up"
                If Turn = "Axis" Then
                    Me.MoveUp("Up")
                Else
                    Me.MoveUp2("Up")
                End If
            Case "Down"
                If Turn = "Axis" Then
                    Me.MoveDown("Down")
                Else
                    Me.MoveDown2("Down")
                End If
            Case "Left"
                If Turn = "Axis" Then
                    Me.MoveLeft("Left")
                Else
                    Me.MoveLeft2("Left")
                End If
            Case "Right"
                If Turn = "Axis" Then
                    Me.MoveRight("Right")
                Else
                    Me.MoveRight2("Right")
                End If

        End Select
        If Turn = "Axis" Then
            myunittype = Units(CurUnit)
            If myunittype = TypeFighter Or myunittype = TypeBomber Then
                If Me.CheckAirport(1, Curx, Cury) = 1 Then
                    Me.RepairAxisPlane(Curx, Cury, CurUnit)
                    Me.SupplyFuelAxisPlane(Curx, Cury, CurUnit)
                    Me.SupplyAmmoAxisPlane(Curx, Cury, CurUnit)
                Else
                    Me.ShowEnvironmentAxis(Curx, Cury)
                    Me.ShowHealthUnit(1, Curx, Cury, CurUnit)

                End If

            Else
                Me.ShowEnvironmentAxis(Curx, Cury)
                Me.ShowHealthUnit(1, Curx, Cury, CurUnit)
            End If

        Else
            myunittype = Units2(CurUnit)
            If myunittype = TypeFighter Or myunittype = TypeBomber Then
                If Me.CheckAirport(2, Curx, Cury) = 1 Then
                    Me.RepairAlliesPlane(Curx, Cury, CurUnit)
                    Me.SupplyFuelAlliesPlane(Curx, Cury, CurUnit)
                    Me.SupplyAmmoAlliesPlane(Curx, Cury, CurUnit)
                End If
            End If
            Me.ShowEnvironmentAllies(Curx, Cury)
            Me.ShowHealthUnit(2, Curx, Cury, CurUnit)

        End If


    End Sub


    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
        'detect up arrow key

        If keyData = Keys.T Then 'EndTurn

            Me.ChangePlayer()
            If TypeofGame = 2 Then
                Me.ChangePlayer()
            End If
        End If
        If keyData = Keys.R Then 'Rest

            Me.RepairAxis(Curx, Cury, CurUnit)
            Me.SupplyAmmoAxis(Curx, Cury, CurUnit)


        End If
        If keyData = Keys.U Then 'Undo

            Me.Undo()

        End If


        If keyData = Keys.Z Or keyData = Keys.End Then

            Me.MoveArmy("DownLeft")
            Return True

        End If

        If keyData = Keys.C Or keyData = Keys.PageDown Then

            Me.MoveArmy("DownRight")
            Return True

        End If

        If keyData = Keys.E Or keyData = Keys.PageUp Then

            Me.MoveArmy("UpRight")
            Return True

        End If

        If keyData = Keys.Q Or keyData = Keys.Home Then

            Me.MoveArmy("UpLeft")
            Return True

        End If

        If keyData = Keys.Up Or keyData = Keys.W Then

            Me.MoveArmy("Up")
            Return True

        End If
        'detect down arrow key
        If keyData = Keys.Down Or keyData = Keys.S Or keyData = Keys.X Then

            Me.MoveArmy("Down")
            Return True

        End If
        'detect left arrow key
        If keyData = Keys.Left Or keyData = Keys.A Then

            Me.MoveArmy("Left")
            Return True

        End If
        'detect right arrow key
        If keyData = Keys.Right Or keyData = Keys.D Then

            Me.MoveArmy("Right")
            Return True

        End If

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub ShowFlag(ByVal country As Integer, ByVal px As Integer, ByVal py As Integer)
        Dim MyFlag, bmp As Bitmap
        Dim posx, posy As Integer

        If country = 1 Then
            MyFlag = Bitmap.FromFile(CurDir() + "\Pics\FLAGGER.bmp")
        Else
            MyFlag = Bitmap.FromFile(CurDir() + "\Pics\FLAGPOL.bmp")
        End If


        posx = HexesPosx + ((px - 1) * RasterWidth)
        posy = HexesPosy - ((py - 1) * 2 * RasterHeight2)

        bmp = PB1.Image
        bmp = Me.AddSpriteTransWhite(bmp, MyFlag, posx + DeltaFlagX, posy + DeltaFlagY)

        PB1.Image = bmp
        Me.Refresh()


    End Sub

    Private Function GetHealthBM(ByVal country As Integer, ByVal health As Integer) As Bitmap
        Dim bmp As Bitmap
        Dim name As String

        If country = 1 Then
            name = "Health" + health.ToString() + ".bmp"
        Else
            name = "Healthallies" + health.ToString() + ".bmp"
        End If
        bmp = Bitmap.FromFile(CurDir() + "\Pics\" + name)

        Return bmp


    End Function

    Private Sub ShowHealthUnit(ByVal country As Integer, ByVal px As Integer, ByVal py As Integer, ByVal theunit As Integer)
        Dim MyHealth, bmp As Bitmap
        Dim posx, posy, curhealth, curitem As Integer


        posx = HexesPosx + ((px - 1) * RasterWidth)
        posy = HexesPosy - ((py - 1) * 2 * RasterHeight2)

        Try
            bmp = PB1.Image
            If country = 1 Then

                curhealth = Health(theunit)
                curitem = Units(theunit)

                If curhealth > 0 Then
                    MyHealth = Me.GetHealthBM(country, curhealth)
                    If curitem = TypeFighter Or curitem = TypeBomber Then 'plane
                        bmp = Me.AddSpriteTransWhite(bmp, MyHealth, posx + DeltaHealthXPlane, posy + DeltaHealthY)
                    Else
                        bmp = Me.AddSpriteTransWhite(bmp, MyHealth, posx + DeltaHealthX, posy + DeltaHealthY)
                    End If
                End If
            Else

                curhealth = Health2(theunit)
                curitem = Units2(theunit)
                If curhealth > 0 And curhealth <= 10 Then
                    MyHealth = Me.GetHealthBM(country, curhealth)
                    If curitem = TypeFighter Or curitem = TypeBomber Then 'plane
                        bmp = Me.AddSpriteTransWhite(bmp, MyHealth, posx + DeltaHealthX2Plane, posy + DeltaHealthY)
                    Else
                        bmp = Me.AddSpriteTransWhite(bmp, MyHealth, posx + DeltaHealthX2, posy + DeltaHealthY)
                    End If
                End If
            End If

            PB1.Image = bmp
            Me.Refresh()


        Catch ex As Exception


        End Try

    End Sub

    'Private Sub ShowHealth(ByVal country As Integer, ByVal px As Integer, ByVal py As Integer)
    '    Dim MyHealth, bmp As Bitmap
    '    Dim posx, posy, curhealth As Integer


    '    posx = HexesPosx + ((px - 1) * RasterWidth)
    '    posy = HexesPosy - ((py - 1) * 2 * RasterHeight2)

    '    bmp = PB1.Image

    '    If country = 1 Then

    '        curhealth = Health(CurUnit)
    '        If curhealth > 0 And curhealth <= 10 Then
    '            MyHealth = Me.GetHealthBM(country, curhealth)
    '            bmp = Me.AddSpriteTransWhite(bmp, MyHealth, posx + DeltaHealthX, posy + DeltaHealthY)

    '        End If
    '    Else

    '        curhealth = Health2(CurUnit)
    '        If curhealth > 0 And curhealth <= 10 Then
    '            MyHealth = Me.GetHealthBM(country, curhealth)
    '            bmp = Me.AddSpriteTransWhite(bmp, MyHealth, posx + DeltaHealthX2, posy + DeltaHealthY)

    '        End If

    '    End If

    '    PB1.Image = bmp
    '    Me.Refresh()

    'End Sub

    'Private Sub ShowHealth(ByVal country As Integer, ByVal px As Integer, ByVal py As Integer, ByVal curhealth As Integer)
    '    Dim MyHealth, bmp As Bitmap
    '    Dim posx, posy As Integer


    '    posx = HexesPosx + ((px - 1) * RasterWidth)
    '    posy = HexesPosy - ((py - 1) * 2 * RasterHeight2)

    '    bmp = PB1.Image

    '    If country = 1 Then

    '        MyHealth = Me.GetHealthBM(country, curhealth)
    '        bmp = Me.AddSpriteTransWhite(bmp, MyHealth, posx + DeltaHealthX, posy + DeltaHealthY)
    '    Else

    '        MyHealth = Me.GetHealthBM(country, curhealth)
    '        bmp = Me.AddSpriteTransWhite(bmp, MyHealth, posx + DeltaHealthX2, posy + DeltaHealthY)
    '    End If

    '    PB1.Image = bmp
    '    Me.Refresh()


    'End Sub

    Private Sub ShowHealth(ByVal country As Integer)
        Dim rownum, cid, pnum, px, py, ptype, pvic, myhealth As Integer
        Dim MyDS As DataSet

        If country = 1 Then
            MyDS = MyUnitsAxisDS
        Else
            MyDS = MyUnitsAlliesDS
        End If


        For i = 0 To MyDS.Tables(0).Rows.Count - 1

            cid = MyDS.Tables(0).Rows(i).Item(0)
            If cid = country Then
                If cid = 1 Then 'Axis
                    pnum = MyDS.Tables(0).Rows(i).Item(1)
                    px = UnitsX(pnum)
                    py = UnitsY(pnum)
                    myhealth = Health(pnum)

                Else
                    pnum = MyDS.Tables(0).Rows(i).Item(1)
                    px = UnitsX2(pnum)
                    py = UnitsY2(pnum)
                    myhealth = Health2(pnum)

                End If

                'px = MyUnitsAxisDS.Tables(0).Rows(i).Item(2)
                'py = MyUnitsAxisDS.Tables(0).Rows(i).Item(3)
                'Me.ShowHealth(country, px, py, myhealth)
                'Me.ShowHealth(country, px, py)
                Me.ShowHealthUnit(country, px, py, pnum)


            End If

        Next


    End Sub

    Private Sub UpdatePrestigeCountries(ByVal country As String)
        Dim count, owner, cid As Integer


        If country = "Axis" Then
            cid = 1
        Else
            cid = 2
        End If


        For i = 0 To MyCountriesDS.Tables(0).Rows.Count - 1
            owner = MyCountriesDS.Tables(0).Rows(i).Item(0)
            If owner = cid Then
                count = count + 1
            End If
        Next

        If country = "Axis" Then
            PrestigeAxis = PrestigeAxis + (count * PrestigeCountryAxis)
            txtPrestige.Text = PrestigeAxis.ToString()

        Else
            PrestigeAllies = PrestigeAllies + (count * PrestigeCountryAllies)
            txtPrestige.Text = PrestigeAllies.ToString()

        End If


    End Sub

    Private Sub ShowCountries()
        Dim rownum, country, pnum, px, py, ptype, pvic As Integer
        Dim pname As String
        Dim MyFlagGER, MyFlagPOL As Bitmap

        For i = 0 To MyCountriesDS.Tables(0).Rows.Count - 1

            country = MyCountriesDS.Tables(0).Rows(i).Item(0)
            pnum = MyCountriesDS.Tables(0).Rows(i).Item(1)
            px = MyCountriesDS.Tables(0).Rows(i).Item(2)
            py = MyCountriesDS.Tables(0).Rows(i).Item(3)
            ptype = MyCountriesDS.Tables(0).Rows(i).Item(4)
            pname = MyCountriesDS.Tables(0).Rows(i).Item(5)
            pvic = MyCountriesDS.Tables(0).Rows(i).Item(6)

            Me.ShowFlag(country, px, py)

        Next


    End Sub

    Private Sub ShowPlaces()

        cbAxis1.Text = AxisTown1
        cbAxis2.Text = AxisTown2
        cbAxis3.Text = AxisTown3

        If AxisTown3 = "" Then
            cbAxis3.Visible = False
        End If

        cbAllies1.Text = AlliesTown1
        cbAllies2.Text = AlliesTown2
        cbAllies3.Text = AlliesTown3



    End Sub

    Private Sub ContinuePBEM()
        Dim txtAxisDecipheredFile, txtAxisCiphertextFile, txtAllieDecipheredFile, txtAlliesCiphertextFile As String
        Dim txtSettingsDecipheredFile, txtSettingsCiphertextFile As String
        Dim Myform1 As New frmNewgame1
        Dim Myform2 As New frmNewgame2
        Dim rc, country, MyTurnnr As Integer
        Dim filnam, MyFile, str, CNAME As String

        FBD1.InitialDirectory = CurDir() + "\PBEM"
        FBD1.Filter = "XML Files|*Settings*.sav"
        FBD1.ShowDialog()
        MyFile = FBD1.FileName

        If MyFile = "OpenFileDialog1" Then 'Cancel
            Me.Close()
            Return
        End If

        str = MyFile.Substring(MyFile.Length - 7, 2)
        CNAME = MyFile.Substring(MyFile.Length - 5, 1)

        MyUnitsAxisDS = New DataSet
        MyUnitsAlliesDS = New DataSet
        MyCountriesDS = New DataSet
        MyCountriesDS.ReadXml(CurDir() + "\xml\S01_Poland_cities.xml")
        MySettingsDS = New DataSet

        'Decipher Save File
        txtSettingsCiphertextFile = CurDir() + "\PBEM\" + SavegameSettings + str + CNAME + ".sav"
        txtSettingsDecipheredFile = CurDir() + "\PBEM\" + SavegameSettings + str + CNAME + ".xml"

        txtAxisCiphertextFile = CurDir() + "\PBEM\" + SavegameAxis + str + CNAME + ".sav"
        txtAxisDecipheredFile = CurDir() + "\PBEM\" + SavegameAxis + str + CNAME + ".xml"

        txtAlliesCiphertextFile = CurDir() + "\PBEM\" + SavegameAllies + str + CNAME + ".sav"
        txtAllieDecipheredFile = CurDir() + "\PBEM\" + SavegameAllies + str + CNAME + ".xml"

        CryptoStuff.DecryptFile(MyPassword, txtSettingsCiphertextFile, txtSettingsDecipheredFile)
        CryptoStuff.DecryptFile(MyPassword, txtAxisCiphertextFile, txtAxisDecipheredFile)
        CryptoStuff.DecryptFile(MyPassword, txtAlliesCiphertextFile, txtAllieDecipheredFile)

        MySettingsDS.ReadXml(CurDir() + "\PBEM\" + SavegameSettings + str + CNAME + ".xml")
        MyUnitsAxisDS.ReadXml(CurDir() + "\PBEM\" + SavegameAxis + str + CNAME + ".xml")
        MyUnitsAlliesDS.ReadXml(CurDir() + "\PBEM\" + SavegameAllies + str + CNAME + ".xml")

        GameMode = MySettingsDS.Tables(0).Rows(0).Item(2)
        TypeofGame = MySettingsDS.Tables(0).Rows(0).Item(3)

        MyGameFieldNoRaster = Bitmap.FromFile(CurDir() + "\Maps\" + GameMap)
        MyGameFieldRaster = Bitmap.FromFile(CurDir() + "\Maps\Scenario-Polen-raster.bmp")

        MyGameField = MyGameFieldNoRaster.Clone()
        PB1.Image = MyGameField.Clone()

        Panel1.AutoScroll = True
        PB1.SizeMode = PictureBoxSizeMode.AutoSize
        Panel1.AutoScrollPosition = New Drawing.Point(StartPosx, StartPosy)
        Me.FillUnitsNew()

        MyTurnnr = CInt(str)
        If CNAME = "A" Then
            TurnNr = MyTurnnr
            country = 2
        Else
            TurnNr = MyTurnnr + 1
            country = 1
        End If
        txtTurn.Text = TurnNr.ToString()


        If country = 1 Then
            Turn = "Axis"
            Me.ShowAllUnitsAxis()
            Me.ShowCountries()
            Me.ShowHealth(1)
            lblTurn.Text = "axis"
            lblTurn.Location = New Point(lblTurn.Location.X + 5, lblTurn.Location.Y)

        Else
            Turn = "Allies"
            Me.ShowAllUnitsAllies()
            Me.ShowCountries()
            Me.ShowHealth(2)
            lblTurn.Text = "allies"

        End If

    End Sub

    Private Sub HandleGames()
        Dim Myform1 As New frmNewgame1
        Dim Myform2 As New frmNewgame2
        Dim Myform3 As New frmNewgame3


        Me.Text = MyTitle
        Me.ShowPlaces()
        'TypeofGame = 2

        Myform1.ShowDialog()
        If NeworLoadGame = 1 Then 'Start a new game. Ask type of game
            Myform2.ShowDialog()
            If TypeofGame = 1 Then 'human-human
                StartNewGame()
                cb02a.Visible = True
                cb02b.Visible = False
            ElseIf TypeofGame = 2 Then 'human-AI
                Myform3.ShowDialog()
                Me.FillAIPlaces1()
                Me.FillAIPlaces2()
                Me.FillAIPlaces3()
                AIPlaces = AIPlaces1

                StartNewGame()
                cb02a.Visible = False
                cb02b.Visible = True
            ElseIf TypeofGame = 4 Then 'Start PBEM
                StartNewGame()
                cb02a.Visible = True
                cb02b.Visible = False
            End If

        ElseIf NeworLoadGame = 3 Then
            Me.LoadNewGame()

        ElseIf NeworLoadGame = 4 Then
            Me.ContinuePBEM()

        End If


    End Sub

    Private Sub BattleForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim rc, country, MyTurnnr As Integer
        Dim filnam, MyFile, str, CNAME As String
        Dim Myform1 As New frmNewgame1
        Dim Myform2 As New frmNewgame2

        Me.HandleGames()


    End Sub

    Public Sub LoadNewGame()
        Dim str, MySettings, MyAxis, MyAllies As String
        Dim country, turnnum As Integer

        MyUnitsAxisDS = New DataSet
        MyUnitsAlliesDS = New DataSet
        MyCountriesDS = New DataSet
        MyCountriesDS.ReadXml(CurDir() + "\xml\S01_Poland_cities.xml")
        MySettingsDS = New DataSet

        MyGameField = Bitmap.FromFile(CurDir() + "\Maps\Scenario-Polen.bmp")
        MyGameFieldRaster = Bitmap.FromFile(CurDir() + "\Maps\Scenario-Polen-raster.bmp")
        PB1.Image = MyGameField.Clone()

        Panel1.AutoScroll = True
        PB1.SizeMode = PictureBoxSizeMode.AutoSize
        Panel1.AutoScrollPosition = New Drawing.Point(StartPosx, StartPosy)


        FBD1.InitialDirectory = CurDir() + "\Saves"
        FBD1.Filter = "XML Files|*Settings*.xml"
        FBD1.ShowDialog()

        MySettings = FBD1.FileName

        If MySettings = "OpenFileDialog1" Then 'Cancel
            Me.Close()
            Return
        End If

        str = MySettings.Substring(MySettings.Length - 6, 2)
        MySettingsDS.ReadXml(FBD1.FileName)
        country = MySettingsDS.Tables(0).Rows(0).Item(0)
        turnnum = MySettingsDS.Tables(0).Rows(0).Item(1)
        GameMode = MySettingsDS.Tables(0).Rows(0).Item(2)
        TypeofGame = MySettingsDS.Tables(0).Rows(0).Item(3)

        MyUnitsAxisDS.ReadXml(CurDir() + "\Saves\" + SavegameAxis + str + ".xml")
        MyUnitsAlliesDS.ReadXml(CurDir() + "\Saves\" + SavegameAllies + str + ".xml")
        Me.FillUnitsNew2()

        If TypeofGame = 2 Then 'human-AI
            Me.ChangeGameSettings()
        End If

        If turnnum > 0 Then
            TurnNr = turnnum
        Else
            TurnNr = 1
        End If
        txtTurn.Text = TurnNr.ToString()

        If country = 1 Then
            Turn = "Axis"
            Me.ShowAllUnitsAxis()
            Me.ShowCountries()
            Me.ShowHealth(1)
            lblTurn.Text = "axis"
            lblTurn.Location = New Point(lblTurn.Location.X + 5, lblTurn.Location.Y)

        Else
            Turn = "Allies"
            Me.ShowAllUnitsAllies()
            Me.ShowCountries()
            Me.ShowHealth(2)
            lblTurn.Text = "allies"

        End If

    End Sub

    Public Sub ChangeGameSettings()

        Select Case GameMode

            Case 0      'Easy GameMode

                PrestigeAxis = PrestigeAxisEasy
                PrestigeAllies = PrestigeAlliesEasy
                PrestigeCountryAxis = PrestigeCountryAxisEasy
                PrestigeCountryAllies = PrestigeCountryAlliesEasy
                PrestigeTurnAxis = PrestigeTurnAxisEasy
                PrestigeTurnAllies = PrestigeTurnAlliesEasy
                AttackBonusRough = 0
                DefendBonusRough = 0
                AttackBonusRiver = 0
                DefendBonusRiver = 0

                SAInfanteryAxis = 6
                SAInfanteryAllies = 3
                HAInfanteryAxis = 4
                HAInfanteryAllies = 2 '1
                GDInfanteryAxis = 6 '6
                GDInfanteryAllies = 3 '3
                ADInfanteryAxis = 8
                ADInfanteryAllies = 4

                SATankAxis = 3
                SATankAllies = 1
                HATankAxis = 8
                HATankAllies = 4
                GDTankAxis = 8
                GDTankAllies = 4
                ADTankAxis = 8
                ADTankAllies = 4

                SAArtilleryAxis = 7 '11
                SAArtilleryAllies = 3 '11
                HAArtilleryAxis = 7
                HAArtilleryAllies = 3
                AAArtilleryAxis = 2
                AAArtilleryAllies = 2
                GDArtilleryAxis = 4
                GDArtilleryAllies = 2
                ADArtilleryAxis = 6
                ADArtilleryAllies = 3

                SAAntitankAxis = 3
                SAAntitankAllies = 1
                HAAntitankAxis = 8
                HAAntitankAllies = 4
                GDAntitankAxis = 8
                GDAntitankAllies = 4
                ADAntitankAxis = 8
                ADAntitankAllies = 4

                SAFighterAxis = 3
                SAFighterAllies = 1
                HAFighterAxis = 3
                HAFighterAllies = 1
                AAFighterAxis = 10 '14
                AAFighterAllies = 5
                GDFighterAxis = 6
                GDFighterAllies = 4
                ADFighterAxis = 10
                ADFighterAllies = 4

                SABomberAxis = 9
                SABomberAllies = 5
                HABomberAxis = 9
                HABomberAllies = 5
                AABomberAxis = 4 '4
                AABomberAllies = 2 '3
                GDBomberAxis = 5
                GDBomberAllies = 4
                ADBomberAxis = 5
                ADBomberAllies = 3 '4

            Case 1      'Average GameMode

                PrestigeAxis = PrestigeAxisAverage
                PrestigeAllies = PrestigeAlliesAverage
                PrestigeCountryAxis = PrestigeCountryAxisAverage
                PrestigeCountryAllies = PrestigeCountryAlliesAverage
                PrestigeTurnAxis = PrestigeTurnAxisAverage
                PrestigeTurnAllies = PrestigeTurnAlliesAverage


            Case 2      'Hard GameMode

                PrestigeAxis = PrestigeAxisHard
                PrestigeAllies = PrestigeAlliesHard
                PrestigeCountryAxis = PrestigeCountryAxisHard
                PrestigeCountryAllies = PrestigeCountryAlliesHard
                PrestigeTurnAxis = PrestigeTurnAxisHard
                PrestigeTurnAllies = PrestigeTurnAlliesHard


        End Select

    End Sub


    Public Sub StartNewGame()

        Dim rownum, px, py, unit As Integer

        'Me.Location = New Point(0, 0)

        MyUnitsAxisDS = New DataSet
        MyUnitsAxisDS.ReadXml(CurDir() + "\xml\" + UnitsAxisXML)

        MyUnitsAlliesDS = New DataSet
        MyUnitsAlliesDS.ReadXml(CurDir() + "\xml\" + UnitsAlliesXML)

        MyCountriesDS = New DataSet
        MyCountriesDS.ReadXml(CurDir() + "\xml\" + CitiesXML)

        MySettingsDS = New DataSet
        MySettingsDS.ReadXml(CurDir() + "\xml\" + SettingsXML)

        MyGameFieldNoRaster = Bitmap.FromFile(CurDir() + "\Maps\" + GameMap)
        MyGameFieldRaster = Bitmap.FromFile(CurDir() + "\Maps\Scenario-Polen-raster.bmp")

        MyGameField = MyGameFieldNoRaster.Clone()
        PB1.Image = MyGameField.Clone()

        Panel1.AutoScroll = True
        PB1.SizeMode = PictureBoxSizeMode.AutoSize
        Panel1.AutoScrollPosition = New Drawing.Point(StartPosx, StartPosy)

        'Me.CopyBigImage()

        'Me.FillUnitsNew()
        Me.FillUnitsNew2()
        If TypeofGame = 2 Then 'human-AI
            Me.ChangeGameSettings()
        End If


        Me.ShowAllUnitsAxis()

        Me.ShowCountries()
        Me.UpdatePrestigeCountries("Axis")

        'Me.ShowAllUnitsAllies()

        Me.ShowHealth(1)
        'Me.ShowHealth(2)

        lblTurn.Location = New Point(lblTurn.Location.X + 5, lblTurn.Location.Y)

        lblTurn.Text = "axis"


    End Sub



    Private Sub CopyBigImage()

        Dim bm As New Bitmap(UnitsAreaW, UnitsAreaH)
        Using gr As Graphics = Graphics.FromImage(bm)
            Dim src_rect As New Rectangle(UnitsAreaX, UnitsAreaY, UnitsAreaW, UnitsAreaH)
            Dim dst_rect As New Rectangle(0, 0, UnitsAreaW, UnitsAreaH)

            gr.DrawImage(PB1.Image, dst_rect, src_rect, GraphicsUnit.Pixel)
        End Using
        UnitsArea = bm

    End Sub

    Private Sub CopyImage2(ByVal unitnr As Integer, ByVal px As Integer, ByVal py As Integer)

        Dim posx1, posy1 As Integer

        posx1 = HexesPosx + ((px - 1) * RasterWidth)
        posy1 = HexesPosy - ((py - 1) * 2 * RasterHeight2)
        'posy1 = HexesPosy - ((py - 1) * 2 * RasterHeight) - RasterHeight

        Dim bm As New Bitmap(ImageCopyW, ImageCopyH)
        Using gr As Graphics = Graphics.FromImage(bm)
            Dim src_rect As New Rectangle(posx1, posy1, ImageCopyW, ImageCopyH)
            Dim dst_rect As New Rectangle(0, 0, ImageCopyW, ImageCopyH)

            gr.DrawImage(PB1.Image, dst_rect, src_rect, GraphicsUnit.Pixel)
        End Using
        UnitArea2(unitnr) = bm

    End Sub

    Private Sub CopyImageRaster(ByVal px As Integer, ByVal py As Integer)

        Dim posx1, posy1 As Integer

        posx1 = HexesPosx + ((px - 1) * RasterWidth)
        posy1 = HexesPosy - ((py - 1) * 2 * RasterHeight2)
        'posy1 = HexesPosy - ((py - 1) * 2 * RasterHeight) - RasterHeight

        Dim bm As New Bitmap(ImageCopyW, ImageCopyH)
        Using gr As Graphics = Graphics.FromImage(bm)
            Dim src_rect As New Rectangle(posx1, posy1, ImageCopyW, ImageCopyH)
            Dim dst_rect As New Rectangle(0, 0, ImageCopyW, ImageCopyH)

            gr.DrawImage(MyGameFieldRaster, dst_rect, src_rect, GraphicsUnit.Pixel)
        End Using
        UnitAreasRaster(px, py) = bm

    End Sub

    Private Sub CopyImageNoRaster(ByVal px As Integer, ByVal py As Integer)
        Dim posx1, posy1 As Integer

        posx1 = HexesPosx + ((px - 1) * RasterWidth)
        posy1 = HexesPosy - ((py - 1) * 2 * RasterHeight2)
        'posy1 = HexesPosy - ((py - 1) * 2 * RasterHeight) - RasterHeight

        Dim bm As New Bitmap(ImageCopyW, ImageCopyH)
        Using gr As Graphics = Graphics.FromImage(bm)
            Dim src_rect As New Rectangle(posx1, posy1, ImageCopyW, ImageCopyH)
            Dim dst_rect As New Rectangle(0, 0, ImageCopyW, ImageCopyH)

            gr.DrawImage(MyGameField, dst_rect, src_rect, GraphicsUnit.Pixel)
        End Using
        UnitAreasNoRaster(px, py) = bm

    End Sub

    Private Sub CopyImageNew(ByVal px As Integer, ByVal py As Integer)

        Dim posx1, posy1 As Integer

        posx1 = HexesPosx + ((px - 1) * RasterWidth)
        posy1 = HexesPosy - ((py - 1) * 2 * RasterHeight2)
        'posy1 = HexesPosy - ((py - 1) * 2 * RasterHeight) - RasterHeight

        Dim bm As New Bitmap(ImageCopyW, ImageCopyH)
        Using gr As Graphics = Graphics.FromImage(bm)
            Dim src_rect As New Rectangle(posx1, posy1, ImageCopyW, ImageCopyH)
            Dim dst_rect As New Rectangle(0, 0, ImageCopyW, ImageCopyH)

            gr.DrawImage(MyGameField, dst_rect, src_rect, GraphicsUnit.Pixel)
        End Using
        UnitAreas(px, py) = bm

    End Sub


    Private Sub CopyImage(ByVal unitnr As Integer, ByVal px As Integer, ByVal py As Integer)
        Dim posx1, posy1 As Integer


        posx1 = HexesPosx + ((px - 1) * RasterWidth)
        posy1 = HexesPosy - ((py - 1) * 2 * RasterHeight2)
        'posy1 = HexesPosy - ((py - 1) * 2 * RasterHeight) - RasterHeight

        Dim bm As New Bitmap(ImageCopyW, ImageCopyH)
        Using gr As Graphics = Graphics.FromImage(bm)
            Dim src_rect As New Rectangle(posx1, posy1, ImageCopyW, ImageCopyH)
            Dim dst_rect As New Rectangle(0, 0, ImageCopyW, ImageCopyH)

            gr.DrawImage(PB1.Image, dst_rect, src_rect, GraphicsUnit.Pixel)
        End Using
        UnitArea(unitnr) = bm


    End Sub

    Private Sub RemoveBigUnits()

        Dim bm1 As New Bitmap(UnitsAreaW, UnitsAreaH)
        Dim bmp As Bitmap

        bmp = PB1.Image
        bm1 = UnitsArea.Clone()

        bmp = Me.AddSpriteTransWhite(bmp, bm1, UnitsAreaX, UnitsAreaY)
        PB1.Image = bmp


    End Sub

    Private Sub RemoveUnit2(ByVal unitnr As Integer, ByVal px As Integer, ByVal py As Integer)
        Dim posx1, posy1 As Integer


        posx1 = HexesPosx + ((px - 1) * RasterWidth)
        posy1 = HexesPosy - ((py - 1) * 2 * RasterHeight2)
        'posy1 = HexesPosy - ((py - 1) * 2 * RasterHeight) - RasterHeight

        Dim bm1 As New Bitmap(ImageCopyW, ImageCopyH)
        Dim bmp As Bitmap

        bmp = PB1.Image
        bm1 = UnitArea2(unitnr).Clone()

        bmp = Me.AddSpriteTransWhite(bmp, bm1, posx1, posy1)
        PB1.Image = bmp



    End Sub

    Private Sub RemoveLastUnit(ByVal unitnr As Integer, ByVal px As Integer, ByVal py As Integer)
        Dim posx1, posy1 As Integer


        posx1 = HexesPosx + ((px - 1) * RasterWidth)
        posy1 = HexesPosy - ((py - 1) * 2 * RasterHeight2)
        'posy1 = HexesPosy - ((py - 1) * 2 * RasterHeight) - RasterHeight

        Dim bm1 As New Bitmap(ImageCopyW, ImageCopyH)
        Dim bmp As Bitmap

        bmp = PB1.Image
        bm1 = UnitAreaLast(unitnr).Clone()

        bmp = Me.AddSpriteTransWhite(bmp, bm1, posx1, posy1)
        PB1.Image = bmp

    End Sub

    Private Sub RemoveUnitNew(ByVal px As Integer, ByVal py As Integer)
        Dim posx1, posy1 As Integer


        posx1 = HexesPosx + ((px - 1) * RasterWidth)
        posy1 = HexesPosy - ((py - 1) * 2 * RasterHeight2)
        'posy1 = HexesPosy - ((py - 1) * 2 * RasterHeight) - RasterHeight

        Dim bm1 As New Bitmap(ImageCopyW, ImageCopyH)
        Dim bmp As Bitmap

        bmp = PB1.Image
        bm1 = UnitAreas(px, py).Clone()

        bmp = Me.AddSpriteTransWhite(bmp, bm1, posx1, posy1)
        PB1.Image = bmp

        'restore old unit
        Try
            If UnitsField(px, py) > 0 Then
                Me.ShowUnitAxis(px, py)
            End If
            If UnitsField2(px, py) > 0 Then
                Me.ShowUnitAllies(px, py)
            End If


        Catch ex As Exception

        End Try



    End Sub

    Private Sub RemoveUnit(ByVal unitnr As Integer, ByVal px As Integer, ByVal py As Integer)
        Dim posx1, posy1 As Integer


        posx1 = HexesPosx + ((px - 1) * RasterWidth)
        posy1 = HexesPosy - ((py - 1) * 2 * RasterHeight2)
        'posy1 = HexesPosy - ((py - 1) * 2 * RasterHeight) - RasterHeight

        Dim bm1 As New Bitmap(ImageCopyW, ImageCopyH)
        Dim bmp As Bitmap

        bmp = PB1.Image
        bm1 = UnitArea(unitnr).Clone()

        bmp = Me.AddSpriteTransWhite(bmp, bm1, posx1, posy1)
        PB1.Image = bmp


    End Sub


    Private Sub FillUnitsNew2()

        Dim rownum, country, pnum, px, py, unit, myhealth, entrench, pammo, pfuel, pmove, psa, pha, paa, pna, pgd, pad, ptrans As Integer

        Try

            'All Units
            For i = 0 To MaxRasterX
                For j = 0 To MaxRasterY
                    UnitsField(i, j) = 0
                    UnitsFieldAir(i, j) = 0
                    UnitsField2(i, j) = 0
                    UnitsFieldAir2(i, j) = 0
                    Me.CopyImageNoRaster(i, j)
                    Me.CopyImageRaster(i, j)


                Next
            Next

            For i = 0 To 99
                Units(i) = 0
                Units2(i) = 0
                UnitsX(i) = 0
                UnitsY(i) = 0
                UnitsX2(i) = 0
                UnitsY2(i) = 0
                MA(i) = 0
                MA2(i) = 0
                MAMax(i) = 0
                MAMax2(i) = 0
                Health(i) = 10
                Health2(i) = 10
                Entrenched(i) = 0
                Entrenched2(i) = 0
                Ammo(i) = 0
                Ammo2(i) = 0
                Fuel(i) = 0
                Fuel2(i) = 0
                Movement(i) = 0
                Movement2(i) = 0
                Trans(i) = 0
                Trans2(i) = 0
                Mount(i) = 0
                Mount2(i) = 0
                Attacked(i) = 0
                Attacked2(i) = 0

            Next

            For i = 0 To MyUnitsAxisDS.Tables(0).Rows.Count - 1

                country = MyUnitsAxisDS.Tables(0).Rows(i).Item(0)
                px = MyUnitsAxisDS.Tables(0).Rows(i).Item(2)
                py = MyUnitsAxisDS.Tables(0).Rows(i).Item(3)
                pnum = MyUnitsAxisDS.Tables(0).Rows(i).Item(1)
                unit = MyUnitsAxisDS.Tables(0).Rows(i).Item(4)

                myhealth = MyUnitsAxisDS.Tables(0).Rows(i).Item(5)
                pmove = MyUnitsAxisDS.Tables(0).Rows(i).Item(9)
                ptrans = MyUnitsAxisDS.Tables(0).Rows(i).Item(10)

                If TypeofGame = 2 And GameMode = 0 Then
                    entrench = 0
                    pfuel = 999
                    pammo = 99
                Else
                    entrench = MyUnitsAxisDS.Tables(0).Rows(i).Item(6)
                    pfuel = MyUnitsAxisDS.Tables(0).Rows(i).Item(8)
                    pammo = MyUnitsAxisDS.Tables(0).Rows(i).Item(7)
                End If

                If country = 1 Then
                    If unit = TypeFighter Or unit = TypeBomber Then
                        UnitsFieldAir(px, py) = pnum
                    Else
                        UnitsField(px, py) = pnum
                    End If
                    If pnum > 0 Then
                        UnitsX(pnum) = px
                        UnitsY(pnum) = py
                        Me.CopyImage(pnum, px, py)
                        Units(pnum) = unit
                        MAMax(pnum) = pmove
                        Health(pnum) = myhealth
                        Entrenched(pnum) = entrench
                        Ammo(pnum) = pammo
                        Fuel(pnum) = pfuel
                        Movement(pnum) = pmove
                        Trans(pnum) = ptrans
                    End If

                End If

            Next

            For i = 0 To MyUnitsAlliesDS.Tables(0).Rows.Count - 1

                country = MyUnitsAlliesDS.Tables(0).Rows(i).Item(0)
                px = MyUnitsAlliesDS.Tables(0).Rows(i).Item(2)
                py = MyUnitsAlliesDS.Tables(0).Rows(i).Item(3)
                pnum = MyUnitsAlliesDS.Tables(0).Rows(i).Item(1)
                unit = MyUnitsAlliesDS.Tables(0).Rows(i).Item(4)

                myhealth = MyUnitsAlliesDS.Tables(0).Rows(i).Item(5)
                pmove = MyUnitsAlliesDS.Tables(0).Rows(i).Item(9)
                ptrans = MyUnitsAlliesDS.Tables(0).Rows(i).Item(10)

                If TypeofGame = 2 And GameMode = 0 Then
                    entrench = 0
                    pfuel = 999
                    pammo = 99
                Else
                    entrench = MyUnitsAlliesDS.Tables(0).Rows(i).Item(6)
                    pammo = MyUnitsAlliesDS.Tables(0).Rows(i).Item(7)
                    pfuel = MyUnitsAlliesDS.Tables(0).Rows(i).Item(8)
                End If

                If country = 2 Then
                    If unit = TypeFighter Or unit = TypeBomber Then
                        UnitsFieldAir2(px, py) = pnum
                    Else
                        UnitsField2(px, py) = pnum
                    End If
                    If pnum > 0 Then
                        UnitsX2(pnum) = px
                        UnitsY2(pnum) = py
                        Me.CopyImage2(pnum, px, py)
                        Units2(pnum) = unit
                        MAMax2(pnum) = pmove
                        Health2(pnum) = myhealth
                        Entrenched2(pnum) = entrench
                        Ammo2(pnum) = pammo
                        Fuel2(pnum) = pfuel
                        Movement2(pnum) = pmove
                        Trans2(pnum) = ptrans
                    End If

                End If

            Next
            UnitAreas = UnitAreasNoRaster

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try


    End Sub


    Private Sub FillUnitsNew()
        Dim rownum, country, pnum, px, py, unit, myhealth, entrench, pammo, pfuel, pmove, psa, pha, paa, pna, pgd, pad, ptrans As Integer

        Try

            'All Units
            For i = 0 To MaxRasterX
                For j = 0 To MaxRasterY
                    UnitsField(i, j) = 0
                    UnitsFieldAir(i, j) = 0
                    UnitsField2(i, j) = 0
                    UnitsFieldAir2(i, j) = 0
                    'UnitAreas(i, j) = New Bitmap(ImageCopyW, ImageCopyH)
                    'Me.CopyImageNew(i, j)
                    Me.CopyImageNoRaster(i, j)
                    Me.CopyImageRaster(i, j)


                Next
            Next

            For i = 0 To 99
                Units(i) = 0
                Units2(i) = 0
                UnitsX(i) = 0
                UnitsY(i) = 0
                UnitsX2(i) = 0
                UnitsY2(i) = 0
                MA(i) = 0
                MA2(i) = 0
                MAMax(i) = 0
                MAMax2(i) = 0
                Health(i) = 10
                Health2(i) = 10
                Entrenched(i) = 0
                Entrenched2(i) = 0
                Ammo(i) = 0
                Ammo2(i) = 0
                Fuel(i) = 0
                Fuel2(i) = 0
                Movement(i) = 0
                Movement2(i) = 0
                Trans(i) = 0
                Trans2(i) = 0
                Mount(i) = 0
                Mount2(i) = 0
                Attacked(i) = 0
                Attacked2(i) = 0

            Next

            For i = 0 To MyUnitsAxisDS.Tables(0).Rows.Count - 1

                country = MyUnitsAxisDS.Tables(0).Rows(i).Item(0)
                pnum = MyUnitsAxisDS.Tables(0).Rows(i).Item(1)
                px = MyUnitsAxisDS.Tables(0).Rows(i).Item(2)
                py = MyUnitsAxisDS.Tables(0).Rows(i).Item(3)
                unit = MyUnitsAxisDS.Tables(0).Rows(i).Item(4)
                myhealth = MyUnitsAxisDS.Tables(0).Rows(i).Item(5)
                entrench = MyUnitsAxisDS.Tables(0).Rows(i).Item(6)
                pammo = MyUnitsAxisDS.Tables(0).Rows(i).Item(7)
                pfuel = MyUnitsAxisDS.Tables(0).Rows(i).Item(8)
                pmove = MyUnitsAxisDS.Tables(0).Rows(i).Item(9)
                ptrans = MyUnitsAxisDS.Tables(0).Rows(i).Item(10)

                'MsgBox("px: " + px.ToString() + ",py: " + py.ToString() + ",Unit: " + unit.ToString())
                If country = 1 Then
                    If unit = TypeFighter Or unit = TypeBomber Then
                        UnitsFieldAir(px, py) = pnum
                    Else
                        UnitsField(px, py) = pnum
                    End If
                    UnitsX(pnum) = px
                    UnitsY(pnum) = py
                    Me.CopyImage(pnum, px, py)
                    Units(pnum) = unit
                    MAMax(pnum) = pmove
                    'If unit = TypeFighter Or unit = TypeBomber Then
                    '    MAMax(pnum) = MAPlane
                    'ElseIf unit = TypeInfantery Then
                    '    MAMax(pnum) = MAInfanterie
                    'ElseIf unit = TypeTank Then
                    '    MAMax(pnum) = MATank
                    'ElseIf unit = TypeArtillery Then
                    '    MAMax(pnum) = MAArtillery
                    'ElseIf unit = TypeAntitank Then
                    '    MAMax(pnum) = MAAntitank
                    'End If
                    Health(pnum) = myhealth
                    Entrenched(pnum) = entrench
                    Ammo(pnum) = pammo
                    Fuel(pnum) = pfuel
                    Movement(pnum) = pmove
                    Trans(pnum) = ptrans

                End If

            Next

            For i = 0 To MyUnitsAlliesDS.Tables(0).Rows.Count - 1

                country = MyUnitsAlliesDS.Tables(0).Rows(i).Item(0)
                pnum = MyUnitsAlliesDS.Tables(0).Rows(i).Item(1)
                px = MyUnitsAlliesDS.Tables(0).Rows(i).Item(2)
                py = MyUnitsAlliesDS.Tables(0).Rows(i).Item(3)
                unit = MyUnitsAlliesDS.Tables(0).Rows(i).Item(4)
                myhealth = MyUnitsAlliesDS.Tables(0).Rows(i).Item(5)
                entrench = MyUnitsAlliesDS.Tables(0).Rows(i).Item(6)
                pammo = MyUnitsAlliesDS.Tables(0).Rows(i).Item(7)
                pfuel = MyUnitsAlliesDS.Tables(0).Rows(i).Item(8)
                pmove = MyUnitsAlliesDS.Tables(0).Rows(i).Item(9)
                ptrans = MyUnitsAlliesDS.Tables(0).Rows(i).Item(10)

                If country = 2 Then
                    'pnum = pnum - 100
                    If unit = TypeFighter Or unit = TypeBomber Then
                        UnitsFieldAir2(px, py) = pnum
                    Else
                        UnitsField2(px, py) = pnum
                    End If
                    UnitsX2(pnum) = px
                    UnitsY2(pnum) = py
                    Me.CopyImage2(pnum, px, py)
                    Units2(pnum) = unit
                    MAMax2(pnum) = pmove
                    Health2(pnum) = myhealth
                    Entrenched2(pnum) = entrench
                    Ammo2(pnum) = pammo
                    Fuel2(pnum) = pfuel
                    Movement2(pnum) = pmove
                    Trans2(pnum) = ptrans

                End If

            Next
            UnitAreas = UnitAreasNoRaster

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try



    End Sub


    Private Sub ShowHex(ByVal unitname As String, ByVal px As Integer, ByVal py As Integer)

        Dim bmp As Bitmap


        bmp = PB1.Image
        Raster = Bitmap.FromFile(CurDir() + "\Units\" + unitname + ".bmp")
        'bmp = Me.AddSpriteTransWhite(bmp, Raster, px, py)
        bmp = Me.AddSpriteTransBlack(bmp, Raster, px, py)

        PB1.Image = bmp
        Me.Refresh()




    End Sub

    Private Sub ShowUnitAxisFOG(ByVal px As Integer, ByVal py As Integer)

        Dim unitname As String
        Dim bmp As Bitmap
        Dim posx, posy, unitnr, unit, transport As Integer
        Dim UnitPresent As Boolean = False
        Dim enemy, enemyair As Integer

        For i = -FOGSpaces To FOGSpaces
            For j = -FOGSpaces To FOGSpaces
                If ((px + i > 0) And (py + j > 0) And (px + i < MaxRasterX) And (py + j < MaxRasterY + 1)) Then
                    enemy = UnitsField2(px + i, py + j)
                    If enemy > 0 Then
                        UnitPresent = True
                    Else
                        enemyair = UnitsFieldAir2(px + i, py + j)
                        If enemyair > 0 Then
                            UnitPresent = True
                        End If
                    End If

                End If
            Next
        Next

        If UnitPresent = True Then

            posx = HexesPosx + ((px - 1) * RasterWidth)
            posy = HexesPosy - ((py - 1) * 2 * RasterHeight2)

            If px <= MaxRasterX Then

                unitnr = UnitsField(px, py)
                DeltaY = DeltaYUnit

                If unitnr > 0 Then
                    unit = Units(unitnr)
                    transport = Mount(unitnr)

                    Select Case unit

                        Case 0
                            unitname = ""
                        Case 1
                            If transport = 0 Then
                                unitname = "Infantery-001"
                            ElseIf transport = 1 Then
                                unitname = "transport-001"
                            ElseIf transport = 2 Then
                                unitname = "transport-002"
                            End If

                        Case 2
                            unitname = "tank-001" '"PZIIF"

                        Case 3
                            If transport = 0 Then
                                unitname = "artillery-001"
                            ElseIf transport = 1 Then
                                unitname = "transport-001"
                            ElseIf transport = 2 Then
                                unitname = "transport-002"
                            End If

                        Case 4
                            If transport = 0 Then
                                unitname = "antitank-001"
                            ElseIf transport = 1 Then
                                unitname = "transport-001"
                            ElseIf transport = 2 Then
                                unitname = "transport-002"
                            End If

                        Case 5
                            If transport = 0 Then
                                unitname = "airdefence-001"

                            ElseIf transport = 1 Then
                                unitname = "transport-001"

                            ElseIf transport = 2 Then
                                unitname = "transport-002"

                            End If


                        Case 9
                            unitname = "fighter-001" '"BF109E"

                        Case 10
                            unitname = "bomber-001" '"JU87B"

                    End Select
                    If unitname <> "" Then
                        bmp = PB1.Image
                        Raster = Bitmap.FromFile(CurDir() + "\Units\" + unitname + ".bmp")
                        bmp = Me.AddSpriteTransWhite(bmp, Raster, posx, posy + DeltaY)

                        PB1.Image = bmp
                        Me.Refresh()
                    End If


                End If
                unitnr = UnitsFieldAir(px, py)
                DeltaY = DeltaYAir
                If unitnr > 0 Then
                    unit = Units(unitnr)
                    transport = Mount(unitnr)

                    Select Case unit
                        Case 0
                            unitname = ""

                        Case 1
                            If transport = 0 Then
                                unitname = "Infantery-001"
                            ElseIf transport = 1 Then
                                unitname = "transport-001"
                            ElseIf transport = 2 Then
                                unitname = "transport-002"
                            End If

                        Case 2
                            unitname = "tank-001" '"PZIIF"

                        Case 3
                            If transport = 0 Then
                                unitname = "artillery-001"
                            ElseIf transport = 1 Then
                                unitname = "transport-001"
                            ElseIf transport = 2 Then
                                unitname = "transport-002"
                            End If

                        Case 4
                            unitname = "antitank-001"

                        Case 5
                            If transport = 0 Then
                                unitname = "airdefence-001"

                            ElseIf transport = 1 Then
                                unitname = "transport-001"

                            ElseIf transport = 2 Then
                                unitname = "transport-002"

                            End If


                        Case 9
                            unitname = "fighter-001" '"BF109E"

                        Case 10
                            unitname = "bomber-001" '"JU87B"

                    End Select
                    If unitname <> "" Then
                        bmp = PB1.Image
                        Raster = Bitmap.FromFile(CurDir() + "\Units\" + unitname + ".bmp")
                        bmp = Me.AddSpriteTransWhite(bmp, Raster, posx, posy + DeltaY)

                        PB1.Image = bmp
                        Me.Refresh()

                    End If

                End If

            End If


        End If

    End Sub

    Private Sub ShowEnvironmentAllies(ByVal px As Integer, ByVal py As Integer)
        Dim unitname As String
        Dim bmp As Bitmap
        Dim posx, posy, unitnr, unit As Integer
        Dim UnitPresent As Boolean = False
        Dim enemy, enemyair As Integer

        For i = -FOGSpaces To FOGSpaces
            For j = -FOGSpaces To FOGSpaces
                If ((px + i > 0) And (py + j > 0) And (px + i < 23) And (py + j < 18)) Then
                    enemy = UnitsField(px + i, py + j)
                    If enemy > 0 Then
                        Me.ShowUnitAxis(px + i, py + j)
                        Me.ShowHealthUnit(1, px + i, py + j, enemy)
                        'UnitPresent = True
                    Else
                        enemyair = UnitsFieldAir(px + i, py + j)
                        If enemyair > 0 Then
                            Me.ShowUnitAxis(px + i, py + j)
                            Me.ShowHealthUnit(1, px + i, py + j, enemyair)
                            'UnitPresent = True
                        End If
                    End If

                End If
            Next
        Next


    End Sub

    Private Sub ShowEnvironmentAxisAll()
        Dim unit, enemy As Integer
        Dim UnitPresent As Boolean = False

        For i = 0 To MaxRasterX
            For j = 0 To MaxRasterY
                unit = UnitsField2(i, j)
                If unit > 0 Then
                    For k = -FOGSpaces To FOGSpaces
                        For l = -FOGSpaces To FOGSpaces
                            If ((i + k > 0) And (j + l > 0) And (i + k < 23) And (j + l < 18)) Then
                                enemy = UnitsField(i + k, j + l)
                                If enemy > 0 Then
                                    UnitPresent = True
                                End If

                            End If
                        Next
                    Next
                    If UnitPresent = True Then
                        Me.ShowUnitAllies(i, j)
                        'Me.ShowHealthUnit(2, i, j, enemy)
                    End If
                End If


                UnitPresent = False
            Next
        Next



    End Sub

    Private Sub ShowEnvironmentAxis(ByVal px As Integer, ByVal py As Integer)
        Dim unitname As String
        Dim bmp As Bitmap
        Dim posx, posy, unitnr, unit As Integer
        Dim UnitPresent As Boolean = False
        Dim enemy, enemyair As Integer

        For i = -FOGSpaces To FOGSpaces
            For j = -FOGSpaces To FOGSpaces
                If ((px + i > 0) And (py + j > 0) And (px + i < 23) And (py + j < 18)) Then
                    enemy = UnitsField2(px + i, py + j)
                    If enemy > 0 Then
                        Me.ShowUnitAllies(px + i, py + j)
                        Me.ShowHealthUnit(2, px + i, py + j, enemy)
                        'UnitPresent = True
                    Else
                        enemyair = UnitsFieldAir2(px + i, py + j)
                        If enemyair > 0 Then
                            Me.ShowUnitAllies(px + i, py + j)
                            Me.ShowHealthUnit(2, px + i, py + j, enemyair)
                            'UnitPresent = True
                        End If
                    End If

                End If
            Next
        Next


    End Sub

    Private Sub ShowUnitAlliesFOG(ByVal px As Integer, ByVal py As Integer)

        Dim unitname As String
        Dim bmp As Bitmap
        Dim posx, posy, unitnr, unitnr2, unit, transport As Integer
        Dim UnitPresent As Boolean = False
        Dim enemy, enemyair As Integer

        For i = -FOGSpaces To FOGSpaces
            For j = -FOGSpaces To FOGSpaces
                If ((px + i > 0) And (py + j > 0) And (px + i < MaxRasterX) And (py + j < MaxRasterY + 1)) Then
                    enemy = UnitsField(px + i, py + j)
                    If enemy > 0 Then
                        'Me.ShowUnitAllies(px + i, py + j)
                        UnitPresent = True
                    Else
                        enemyair = UnitsFieldAir(px + i, py + j)
                        If enemyair > 0 Then
                            'Me.ShowUnitAllies(px + i, py + j)
                            UnitPresent = True
                        End If
                    End If

                End If
            Next
        Next

        If UnitPresent = True Then
            posx = AlliesPosx + ((px - 1) * RasterWidth)
            posy = AlliesPosy - ((py - 1) * 2 * RasterHeight2)

            If px <= MaxRasterX Then
                unitnr = UnitsField2(px, py)
                DeltaY = DeltaYUnit
                If unitnr > 0 Then
                    unit = Units2(unitnr)
                    transport = Mount2(unitnr)

                    Select Case unit

                        Case 0
                            unitname = ""
                        Case 1
                            If transport = 0 Then
                                unitname = "Infantery-POL"
                            ElseIf transport = 1 Then
                                unitname = "transport-001"
                            ElseIf transport = 2 Then
                                unitname = "transport-001"
                            End If

                        Case 2
                            unitname = "tank-POL"

                        Case 3
                            If transport = 0 Then
                                unitname = "artillery-POL"
                            ElseIf transport = 1 Then
                                unitname = "transport-001"
                            ElseIf transport = 2 Then
                                unitname = "transport-001"
                            End If

                        Case 4
                            If transport = 0 Then
                                unitname = "antitank-POL"
                            ElseIf transport = 1 Then
                                unitname = "transport-001"
                            ElseIf transport = 2 Then
                                unitname = "transport-001"
                            End If


                        Case 5
                            If transport = 0 Then
                                unitname = "airdefence-POL"
                            ElseIf transport = 1 Then
                                unitname = "transport-001"
                            ElseIf transport = 2 Then
                                unitname = "transport-001"
                            End If


                        Case 9
                            unitname = "fighter-POL"

                        Case 10
                            unitname = "bomber-POL"

                    End Select
                    If unitname <> "" Then
                        bmp = PB1.Image
                        Raster = Bitmap.FromFile(CurDir() + "\Units2\" + unitname + ".bmp")
                        bmp = Me.AddSpriteTransWhite(bmp, Raster, posx, posy)

                        PB1.Image = bmp
                        Me.Refresh()
                    End If

                End If
                unitnr = UnitsFieldAir2(px, py)
                DeltaY = DeltaYAir
                If unitnr > 0 Then
                    unit = Units2(unitnr)
                    transport = Mount2(unitnr)

                    Select Case unit

                        Case 0
                            unitname = ""
                        Case 1
                            If transport = 0 Then
                                unitname = "Infantery-POL"
                            ElseIf transport = 1 Then
                                unitname = "transport-001"
                            End If

                        Case 2
                            unitname = "tank-POL"

                        Case 3
                            If transport = 0 Then
                                unitname = "artillery-POL"
                            ElseIf transport = 1 Then
                                unitname = "transport-001"
                            End If

                        Case 4
                            If transport = 0 Then
                                unitname = "antitank-POL"
                            ElseIf transport = 1 Then
                                unitname = "transport-001"
                            End If

                        Case 5
                            If transport = 0 Then
                                unitname = "airdefence-POL"
                            ElseIf transport = 1 Then
                                unitname = "transport-001"
                            End If


                        Case 9
                            unitname = "fighter-POL"

                        Case 10
                            unitname = "bomber-POL"

                    End Select
                    If unitname <> "" Then
                        bmp = PB1.Image
                        Raster = Bitmap.FromFile(CurDir() + "\Units2\" + unitname + ".bmp")
                        bmp = Me.AddSpriteTransWhite(bmp, Raster, posx, posy)

                        PB1.Image = bmp
                        Me.Refresh()
                    End If

                End If

            End If
        End If

    End Sub

    Private Sub ShowUnitAllies(ByVal px As Integer, ByVal py As Integer)

        Dim unitname As String
        Dim bmp As Bitmap
        Dim posx, posy, unitnr, unitnrair, unit, unitair, transport As Integer

        posx = AlliesPosx + ((px - 1) * RasterWidth)
        posy = AlliesPosy - ((py - 1) * 2 * RasterHeight2)
        'posy = AlliesPosy - ((py - 1) * 2 * RasterHeight2)


        If px <= MaxRasterX Then
            unitnr = UnitsField2(px, py)
            unitnrair = UnitsFieldAir2(px, py)
            If unitnr > 0 Then
                unit = Units2(unitnr)
                transport = Mount2(unitnr)

            End If
            If unitnrair > 0 Then
                unitair = Units2(unitnrair)
            End If

            If unitnr > 0 Then
                Select Case unit
                    Case 0
                        unitname = ""

                    Case 1
                        unitname = "Infantery-POL"

                        If transport = 0 Then
                            unitname = "Infantery-POL"
                        ElseIf transport = 1 Then
                            unitname = "transport-001"
                        ElseIf transport = 2 Then
                            unitname = "transport-001"
                        End If


                    Case 2
                        unitname = "tank-POL"

                    Case 3
                        unitname = "artillery-POL"

                        If transport = 0 Then
                            unitname = "artillery-POL"
                        ElseIf transport = 1 Then
                            unitname = "transport-001"
                        ElseIf transport = 2 Then
                            unitname = "transport-001"
                        End If

                    Case 4
                        If transport = 0 Then
                            unitname = "antitank-POL"
                        ElseIf transport = 1 Then
                            unitname = "transport-001"
                        ElseIf transport = 2 Then
                            unitname = "transport-001"
                        End If


                    Case 5
                        unitname = "airdefence-POL"


                    Case 9
                        unitname = "fighter-POL"

                    Case 10
                        unitname = "bomber-POL"

                End Select

                If unitnr > 0 And unitname <> "" Then
                    bmp = PB1.Image
                    Raster = Bitmap.FromFile(CurDir() + "\Units2\" + unitname + ".bmp")
                    bmp = Me.AddSpriteTransWhite(bmp, Raster, posx, posy)
                    PB1.Image = bmp

                    Me.ShowHealthUnit(2, px, py, unitnr)
                    Me.Refresh()

                End If

            End If

            If unitair > 0 Then
                Select Case unitair

                    Case 0
                        unitname = ""

                    Case 1
                        unitname = "Infantery-POL"

                    Case 2
                        unitname = "tank-POL"

                    Case 3
                        unitname = "artillery-POL"

                    Case 4
                        unitname = "antitank-POL"

                    Case 5
                        unitname = "airdefence-POL"


                    Case 9
                        unitname = "fighter-POL"

                    Case 10
                        unitname = "bomber-POL"

                End Select

                If unitair > 0 And unitname <> "" Then
                    bmp = PB1.Image
                    Raster = Bitmap.FromFile(CurDir() + "\Units2\" + unitname + ".bmp")
                    bmp = Me.AddSpriteTransWhite(bmp, Raster, posx, posy)
                    PB1.Image = bmp

                    Me.ShowHealthUnit(2, px, py, unitnrair)
                    Me.Refresh()

                End If

            End If

        End If

    End Sub

    Private Sub ShowUnit2Old(ByVal px As Integer, ByVal py As Integer)

        Dim unitname As String
        Dim bmp As Bitmap
        Dim posx, posy, unitnr, unit As Integer

        If px Mod 2 = 1 Then
            posx = AlliesPosx + ((px - 1) * RasterWidth)
            posy = AlliesPosy - ((py - 1) * 2 * RasterHeight)
        Else
            posx = AlliesPosx + ((px - 1) * RasterWidth)
            posy = AlliesPosy - ((py - 1) * 2 * RasterHeight) - RasterHeight
        End If

        If px <= MaxRasterX Then

            unitnr = UnitsField2(px, py)
            If unitnr > 100 Then
                unit = Units2(unitnr)
            Else

            End If

            Select Case unit

                Case 1
                    unitname = "Infantery-POL"

                Case 2
                    unitname = "tank-POL"

                Case 3
                    unitname = "artillery-POL"

                Case 4
                    unitname = "fighter-POL"

                Case 5
                    unitname = "antitank-POL"

            End Select


            If unitnr > 0 Then
                bmp = PB1.Image
                Raster = Bitmap.FromFile(CurDir() + "\Units2\" + unitname + ".bmp")
                bmp = Me.AddSpriteTransWhite(bmp, Raster, posx, posy)

                PB1.Image = bmp
                Me.Refresh()

            End If


        End If

    End Sub

    'Private Sub ShowUnitSel(ByVal px As Integer, ByVal py As Integer)

    '    Dim unitname As String
    '    Dim bmp As Bitmap
    '    Dim posx, posy, unitnr, unit As Integer
    '    Dim DeltaY As Integer

    '    posx = HexesPosx + ((px - 1) * RasterWidth)
    '    'posy = HexesPosy - ((py - 1) * 2 * RasterHeight)
    '    posy = HexesPosy - ((py - 1) * 2 * RasterHeight2)


    '    If px <= MaxRasterX Then
    '        unitnr = UnitsField(px, py)
    '        DeltaY = DeltaYUnit
    '        If unitnr = 0 Then
    '            unitnr = UnitsFieldAir(px, py)
    '            DeltaY = DeltaYAir
    '        End If
    '        unit = Units(unitnr)

    '        Select Case unit

    '            Case 1
    '                unitname = "Infantery-001sel"

    '            Case 2
    '                unitname = "PZIIFsel"

    '            Case 3
    '                unitname = "artillery-001sel"

    '            Case 4
    '                unitname = "BF109E"

    '            Case 5
    '                unitname = "JU87B"

    '        End Select


    '        If unitnr > 0 Then
    '            bmp = PB1.Image
    '            Raster = Bitmap.FromFile(CurDir() + "\Units\" + unitname + ".bmp")
    '            bmp = Me.AddSpriteTransWhite(bmp, Raster, posx, posy + DeltaY)

    '            PB1.Image = bmp
    '            Me.Refresh()

    '        End If
    '    End If

    'End Sub


    Private Sub ShowUnitAxis(ByVal px As Integer, ByVal py As Integer)
        Dim unitname As String
        Dim bmp As Bitmap
        Dim DeltaY, DeltaY2 As Integer
        Dim posx, posy, unitnr, unit, unitnrair, unitair, transport As Integer

        posx = HexesPosx + ((px - 1) * RasterWidth)
        posy = HexesPosy - ((py - 1) * 2 * RasterHeight2)

        If px <= MaxRasterX Then
            unitnr = UnitsField(px, py)
            unitnrair = UnitsFieldAir(px, py)
            If unitnr > 0 Then
                unit = Units(unitnr)
                transport = Mount(unitnr)
                DeltaY = DeltaYUnit
            End If
            If unitnrair > 0 Then
                unitair = Units(unitnrair)
                DeltaY2 = DeltaYAir
            End If

            If unitnr > 0 Then
                Select Case unit

                    Case 0
                        unitname = ""

                    Case 1
                        If transport = 0 Then
                            unitname = "Infantery-001"
                        ElseIf transport = 1 Then
                            unitname = "transport-001"
                        ElseIf transport = 2 Then
                            unitname = "transport-002"
                        End If


                    Case 2
                        unitname = "Tank-001" 'PZIIF

                    Case 3
                        If transport = 0 Then
                            unitname = "artillery-001"
                        ElseIf transport = 1 Then
                            unitname = "transport-001"
                        ElseIf transport = 2 Then
                            unitname = "transport-002"
                        End If

                    Case 4
                        If transport = 0 Then
                            unitname = "antitank-001"

                        ElseIf transport = 1 Then
                            unitname = "transport-001"

                        ElseIf transport = 2 Then
                            unitname = "transport-002"
                        End If


                    Case 5
                        If transport = 0 Then
                            unitname = "airdefence-001"

                        ElseIf transport = 1 Then
                            unitname = "transport-001"

                        ElseIf transport = 2 Then
                            unitname = "transport-002"

                        End If

                    Case 9
                        unitname = "fighter-001" '"BF109E"

                    Case 10
                        unitname = "bomber-001" '"JU87B"

                End Select


                If unitnr > 0 And unitname <> "" Then
                    bmp = PB1.Image
                    Raster = Bitmap.FromFile(CurDir() + "\Units\" + unitname + ".bmp")
                    bmp = Me.AddSpriteTransWhite(bmp, Raster, posx, posy + DeltaY)
                    PB1.Image = bmp

                    Me.ShowHealthUnit(1, px, py, unitnr)

                    Me.Refresh()
                End If
            End If

            If unitnrair > 0 Then
                Select Case unitair

                    Case 0
                        unitname = ""

                    Case 1
                        unitname = "Infantery-001"

                    Case 2
                        unitname = "Tank-001" 'PZIIF

                    Case 3
                        unitname = "artillery-001"

                    Case 4
                        unitname = "antitank-001"

                    Case 5
                        unitname = "airdefence-001"

                    Case 9
                        unitname = "fighter-001" '"BF109E"

                    Case 10
                        unitname = "bomber-001" '"JU87B"


                End Select


                If unitnrair > 0 And unitname <> "" Then
                    bmp = PB1.Image
                    Raster = Bitmap.FromFile(CurDir() + "\Units\" + unitname + ".bmp")
                    bmp = Me.AddSpriteTransWhite(bmp, Raster, posx, posy + DeltaY)
                    PB1.Image = bmp

                    Me.ShowHealthUnit(1, px, py, unitnrair)

                    Me.Refresh()
                End If


            End If

        End If

    End Sub

    Private Sub ShowUnitOld(ByVal px As Integer, ByVal py As Integer)
        Dim unitname As String
        Dim bmp As Bitmap
        Dim posx, posy, unitnr, unit As Integer

        If px Mod 2 = 1 Then
            posx = HexesPosx + ((px - 1) * RasterWidth)
            posy = HexesPosy - ((py - 1) * 2 * RasterHeight)
        Else
            posx = HexesPosx + ((px - 1) * RasterWidth)
            posy = HexesPosy - ((py - 1) * 2 * RasterHeight) - RasterHeight
        End If

        If px <= MaxRasterX Then

            unitnr = UnitsField(px, py)
            unit = Units(unitnr)

            Select Case unit

                Case 1
                    unitname = "Infantery-001"

                Case 2
                    unitname = "PZIIF"

                Case 3
                    unitname = "artillery-001"

                Case 4
                    unitname = "BF109E"

                Case 5
                    unitname = "JU87B"

            End Select


            If unitnr > 0 Then
                bmp = PB1.Image
                Raster = Bitmap.FromFile(CurDir() + "\Units\" + unitname + ".bmp")
                bmp = Me.AddSpriteTransWhite(bmp, Raster, posx, posy)

                PB1.Image = bmp
                Me.Refresh()

            End If




        End If

    End Sub

    Private Sub ShowUnits2(ByVal unitnr As Integer, ByVal px As Integer, ByVal py As Integer)
        Dim unitname As String
        Dim bmp As Bitmap


        Select Case unitnr

            Case 1
                unitname = "Infantery-POL"

            Case 2
                unitname = "tank-POL"

            Case 3
                unitname = "artillery-POL"

            Case 4
                unitname = "antitank-POL"

            Case 5
                unitname = "airdefence-POL"

            Case 9
                unitname = "fighter-POL"

            Case 10
                unitname = "bomber-POL"

        End Select




        If unitnr > 0 Then
            bmp = PB1.Image
            Raster = Bitmap.FromFile(CurDir() + "\Units2\" + unitname + ".bmp")
            bmp = Me.AddSpriteTransWhite(bmp, Raster, px, py)

            PB1.Image = bmp
            Me.Refresh()

        End If

    End Sub


    Public Function AddSpriteTransWhite(ByRef mypict As Bitmap, ByVal sprite As Bitmap, ByVal xpos As Integer, ByVal ypos As Integer) As Bitmap

        Dim MyColor As Color
        Dim lx, ly, i As Integer
        Dim rect As RectangleF

        'pixfmt = sprite.GetPixelFormatSize()
        rect = sprite.GetBounds(GraphicsUnit.Pixel)
        lx = rect.Width
        ly = rect.Height
        'sprite.MakeTransparent()
        For x = 0 To lx - 1
            For y = 0 To ly - 1
                MyColor = sprite.GetPixel(x, y)
                If MyColor.R = 255 And MyColor.B = 255 And MyColor.G = 255 Then
                    i = 1
                    'nothing
                Else
                    mypict.SetPixel(xpos + x, ypos + y, MyColor)
                End If
            Next
        Next

        Return mypict

    End Function


    Public Function AddSpriteTransBlack(ByRef mypict As Bitmap, ByVal sprite As Bitmap, ByVal xpos As Integer, ByVal ypos As Integer) As Bitmap

        Dim MyColor As Color
        Dim lx, ly, i As Integer
        Dim px, py As Integer
        Dim rect1, rect2 As RectangleF

        Try

            rect1 = sprite.GetBounds(GraphicsUnit.Pixel)
            lx = rect1.Width
            ly = rect1.Height
            rect2 = mypict.GetBounds(GraphicsUnit.Pixel)
            px = rect2.Width
            py = rect2.Height
            If lx + xpos > px Then
                MsgBox("Width from picture " + sprite.Tag + " to big")
            End If
            If ly + ypos > py Then
                MsgBox("Height from picture " + sprite.Tag + " to big")
            End If

            For x = 0 To lx - 1
                For y = 0 To ly - 1
                    MyColor = sprite.GetPixel(x, y)
                    If MyColor.R = 0 And MyColor.B = 0 And MyColor.G = 0 Then
                        i = 1
                        'nothing
                    Else
                        mypict.SetPixel(xpos + x, ypos + y, MyColor)
                    End If
                Next
            Next

            Return mypict

        Catch ex As Exception

            Return mypict

        End Try


    End Function

    Public Function AddSprite(ByRef mypict As Bitmap, ByVal sprite As Bitmap, ByVal xpos As Integer, ByVal ypos As Integer) As Bitmap
        Dim MyColor As Color
        Dim lx, ly As Integer
        Dim px, py As Integer
        Dim rect1, rect2 As RectangleF

        Try
            rect1 = sprite.GetBounds(GraphicsUnit.Pixel)
            lx = rect1.Width
            ly = rect1.Height
            rect2 = mypict.GetBounds(GraphicsUnit.Pixel)
            px = rect2.Width
            py = rect2.Height

            If lx + xpos > px Then
                MsgBox("Width from picture " + sprite.Tag + " to big")
            End If
            If ly + ypos > py Then
                MsgBox("Height from picture " + sprite.Tag + " to big")
            End If

            For x = 0 To lx - 1
                For y = 0 To ly - 1
                    MyColor = sprite.GetPixel(x, y)
                    mypict.SetPixel(xpos + x, ypos + y, MyColor)
                Next
            Next

            Return mypict

        Catch ex As Exception

            Return mypict

        End Try

    End Function


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

    End Sub


    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick


    End Sub



    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click


        CurPosx = RasterStartx
        CurPosy = RasterStarty
        NrofMoves = 0


        For j = 0 To 16

            For i = 0 To 29
                'Me.ShowUnits("hexagon-001", Curposx, CurPosy)
                Me.ShowHex("hexagon-001", CurPosx, CurPosy)


                NrofMoves = NrofMoves + 1

                If NrofMoves Mod 2 = 0 Then
                    CurPosx = CurPosx + RasterWidth
                    CurPosy = RasterStarty '- RasterHeight

                Else
                    CurPosx = CurPosx + RasterWidth
                    CurPosy = RasterStarty - RasterHeight
                End If
            Next
            NrofMoves = 0
            CurPosx = RasterStartx
            RasterStarty = RasterStarty - RasterHeight - RasterHeight
            CurPosy = RasterStarty

        Next


    End Sub

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        PB1.Image.Save(CurDir() + "\Maps\Scenario-Polen.jpg")



    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged

    End Sub

    Private Sub CheckBox1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.Click

        If RasterShown = False Then
            MyGameField = MyGameFieldRaster.Clone()
            PB1.Image = MyGameField.Clone()
            UnitAreas = UnitAreasRaster
            RasterShown = True
        Else

            MyGameField = MyGameFieldNoRaster.Clone()
            PB1.Image = MyGameField.Clone()
            UnitAreas = UnitAreasNoRaster
            RasterShown = False
        End If

        Me.ShowAllUnitsAxis()
        Me.ShowCountries()
        Me.ShowHealth(1)

        Me.Refresh()


    End Sub

    Private Sub Button4_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click



    End Sub

    Private Sub ShowAllUnitsAllies()

        Dim px, py As Integer

        PB1.Image = MyGameField.Clone()
        For i = 0 To MaxRasterX
            For j = 0 To MaxRasterY
                'Me.ShowUnitAxis(i, j)
                Me.ShowUnitAxisFOG(i, j)

            Next
        Next

        For i = 0 To MaxRasterX
            For j = 0 To MaxRasterY
                Me.ShowUnitAllies(i, j)
                'Me.ShowUnitAlliesFOG(i, j)

            Next
        Next



    End Sub

    Private Sub ShowAllUnitsAxis()

        Dim px, py As Integer

        PB1.Image = MyGameField.Clone()
        'If CheckBox1.Checked = False Then
        '    PB1.Image = MyGameField.Clone()
        'Else
        '    PB1.Image = MyGameFieldRaster.Clone()
        'End If
        For i = 0 To MaxRasterX
            For j = 0 To MaxRasterY
                Me.ShowUnitAxis(i, j)

            Next
        Next

        For i = 0 To MaxRasterX
            For j = 0 To MaxRasterY
                'Me.ShowUnitAllies(i, j)
                Me.ShowUnitAlliesFOG(i, j)

            Next
        Next



    End Sub

    Private Function GetUnitName(ByVal unitnum As Integer) As String
        Dim uname As String

        Select Case unitnum

            Case TypeInfantery
                uname = "Infantery"

            Case TypeTank
                uname = "Tank"

            Case TypeArtillery
                uname = "Artillery"

            Case TypeAntitank
                uname = "Antitank"

            Case TypeAirdefense
                uname = "Airdefense"

            Case TypeRecon
                uname = "Recon"

            Case TypeFighter
                uname = "Fighter"

            Case TypeBomber
                uname = "Bomber"

        End Select

        Return uname

    End Function
    Public Function GetParameter(ByVal cid As Integer, ByVal parnr As Integer, ByVal Unitnr As Integer)
        Dim param As String
        Dim paramnr As Integer

        If cid = 1 Then

            Select Case parnr
                Case 1

                    Select Case Unitnr
                        Case 1
                            paramnr = HAInfanteryAxis
                        Case 2
                            paramnr = HATankAxis
                        Case 3
                            paramnr = HAArtilleryAxis
                        Case 4
                            paramnr = HAAntitankAxis
                        Case 5

                        Case 9
                            paramnr = HAFighterAxis
                        Case 10
                            paramnr = HABomberAxis
                    End Select

                Case 2
                    Select Case Unitnr
                        Case 1
                            paramnr = SAInfanteryAxis
                        Case 2
                            paramnr = SATankAxis
                        Case 3
                            paramnr = SAArtilleryAxis
                        Case 4
                            paramnr = SAAntitankAxis
                        Case 5

                        Case 9
                            paramnr = SAFighterAxis
                        Case 10
                            paramnr = SABomberAxis
                    End Select

                Case 3
                    Select Case Unitnr
                        Case 1
                            paramnr = AAInfanteryAxis
                        Case 2
                            paramnr = AATankAxis
                        Case 3
                            paramnr = AAArtilleryAxis
                        Case 4
                            paramnr = AAAntitankAxis
                        Case 5

                        Case 9
                            paramnr = AAFighterAxis
                        Case 10
                            paramnr = AABomberAxis
                    End Select

                Case 4
                    Select Case Unitnr
                        Case 1
                            paramnr = NAInfanteryAxis
                        Case 2
                            paramnr = NATankAxis
                        Case 3
                            paramnr = NAArtilleryAxis
                        Case 4
                            paramnr = NAAntitankAxis
                        Case 5

                        Case 9
                            paramnr = NAFighterAxis
                        Case 10
                            paramnr = NABomberAxis
                    End Select

                Case 5
                    Select Case Unitnr
                        Case 1
                            paramnr = GDInfanteryAxis
                        Case 2
                            paramnr = GDTankAxis
                        Case 3
                            paramnr = GDArtilleryAxis
                        Case 4
                            paramnr = GDAntitankAxis
                        Case 5

                        Case 9
                            paramnr = GDFighterAxis
                        Case 10
                            paramnr = GDBomberAxis
                    End Select

                Case 6
                    Select Case Unitnr
                        Case 1
                            paramnr = ADInfanteryAxis
                        Case 2
                            paramnr = ADTankAxis
                        Case 3
                            paramnr = ADArtilleryAxis
                        Case 4
                            paramnr = ADAntitankAxis
                        Case 5

                        Case 9
                            paramnr = ADFighterAxis
                        Case 10
                            paramnr = ADBomberAxis
                    End Select


            End Select

        Else

            Select Case parnr
                Case 1

                    Select Case Unitnr
                        Case 1
                            paramnr = HAInfanteryAllies
                        Case 2
                            paramnr = HATankAllies
                        Case 3
                            paramnr = HAArtilleryAllies
                        Case 4
                            paramnr = HAAntitankAllies
                        Case 5

                        Case 9
                            paramnr = HAFighterAllies
                        Case 10
                            paramnr = HABomberAllies
                    End Select

                Case 2
                    Select Case Unitnr
                        Case 1
                            paramnr = SAInfanteryAllies
                        Case 2
                            paramnr = SATankAllies
                        Case 3
                            paramnr = SAArtilleryAllies
                        Case 4
                            paramnr = SAAntitankAllies
                        Case 5

                        Case 9
                            paramnr = SAFighterAllies
                        Case 10
                            paramnr = SABomberAllies
                    End Select

                Case 3
                    Select Case Unitnr
                        Case 1
                            paramnr = AAInfanteryAllies
                        Case 2
                            paramnr = AATankAllies
                        Case 3
                            paramnr = AAArtilleryAllies
                        Case 4
                            paramnr = AAAntitankAllies
                        Case 5

                        Case 9
                            paramnr = AAFighterAllies
                        Case 10
                            paramnr = AABomberAllies
                    End Select

                Case 4
                    Select Case Unitnr
                        Case 1
                            paramnr = NAInfanteryAllies
                        Case 2
                            paramnr = NATankAllies
                        Case 3
                            paramnr = NAArtilleryAllies
                        Case 4
                            paramnr = NAAntitankAllies
                        Case 5

                        Case 9
                            paramnr = NAFighterAllies
                        Case 10
                            paramnr = NABomberAllies
                    End Select

                Case 5
                    Select Case Unitnr
                        Case 1
                            paramnr = GDInfanteryAllies
                        Case 2
                            paramnr = GDTankAllies
                        Case 3
                            paramnr = GDArtilleryAllies
                        Case 4
                            paramnr = GDAntitankAllies
                        Case 5

                        Case 9
                            paramnr = GDFighterAllies
                        Case 10
                            paramnr = GDBomberAllies
                    End Select

                Case 6
                    Select Case Unitnr
                        Case 1
                            paramnr = ADInfanteryAllies
                        Case 2
                            paramnr = ADTankAllies
                        Case 3
                            paramnr = ADArtilleryAllies
                        Case 4
                            paramnr = ADAntitankAllies
                        Case 5

                        Case 9
                            paramnr = ADFighterAllies
                        Case 10
                            paramnr = ADBomberAllies
                    End Select

            End Select

        End If
        param = paramnr.ToString()
        Return param


    End Function

    Private Sub ShowParameters()

        If Turn = "Axis" Then

            txtType.Text = Me.GetUnitName(Units(CurUnit))
            txtPosx.Text = Curx.ToString()
            txtPosy.Text = Cury.ToString()
            txtFuel.Text = Fuel(CurUnit).ToString()
            txtAmmo.Text = Ammo(CurUnit).ToString()
            txtEntrench.Text = Entrenched(CurUnit).ToString()
            txtHA.Text = GetParameter(1, 1, Units(CurUnit)) 'HA(CurUnit).ToString()
            txtSA.Text = GetParameter(1, 2, Units(CurUnit)) 'SA(CurUnit).ToString()
            txtAA.Text = GetParameter(1, 3, Units(CurUnit))
            txtNA.Text = GetParameter(1, 4, Units(CurUnit))
            txtGD.Text = GetParameter(1, 5, Units(CurUnit))
            txtAD.Text = GetParameter(1, 6, Units(CurUnit))
        Else
            txtType.Text = Me.GetUnitName(Units2(CurUnit))
            txtPosx.Text = Curx.ToString()
            txtPosy.Text = Cury.ToString()
            txtFuel.Text = Fuel2(CurUnit).ToString()
            txtAmmo.Text = Ammo2(CurUnit).ToString()
            txtEntrench.Text = Entrenched2(CurUnit).ToString()
            txtHA.Text = GetParameter(2, 1, Units2(CurUnit)) 'HA2(CurUnit).ToString()
            txtSA.Text = GetParameter(2, 2, Units2(CurUnit))
            txtAA.Text = GetParameter(2, 3, Units2(CurUnit))
            txtNA.Text = GetParameter(2, 4, Units2(CurUnit))
            txtGD.Text = GetParameter(2, 5, Units2(CurUnit))
            txtAD.Text = GetParameter(2, 6, Units2(CurUnit))

        End If



    End Sub

    Private Sub PB1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PB1.MouseDown

        Dim px, py, cx, cy, lx, ly, theunit, theunit2, utype As Integer
        Dim posx, posy As Integer
        Dim CurMA As Integer


        BMount.Visible = False
        BDismount.Visible = False

        px = e.X
        py = e.Y
        TextBox4.Text = px.ToString()
        TextBox5.Text = py.ToString()

        cx = (px - HexesPosx + DeltaHexesX) / RasterWidth
        cy = ((HexesPosy + DeltaHexesY - py) / (2 * RasterHeight2)) + 1
        TextBox1.Text = cx.ToString()
        TextBox2.Text = cy.ToString()
        PRECT.Visible = False

        Dim str1 As String

        If cx > 0 And cy > 0 And cx < 23 And cy < 18 Then
            str1 = FieldSurface(cy, cx).ToString()
            TextBox8.Text = GetSurface(FieldSurface(cy, cx))
        Else
            Return

        End If

        If Turn = "Axis" Then
            If UnitsField(cx, cy) = 0 Then 'no units on field
                If UnitsFieldAir(cx, cy) = 0 Then 'no planes on field
                    If UnitsField2(cx, cy) > 0 And UnitsField(LastPosx, LastPosy) > 0 Then 'clicked on an enemy ! attack ?
                        theunit = UnitsField(LastPosx, LastPosy)
                        utype = Units(theunit)
                        If utype = TypeArtillery Then
                            'MsgBox(utype)
                            If LastPosx >= cx - 2 And LastPosx <= cx + 2 And LastPosy >= cy - 2 And LastPosy <= cy + 2 Then 'right spot ?
                                If Attacked(theunit) = 0 Then
                                    Me.AttackAxisArtillery(LastPosx, LastPosy, cx, cy)
                                    theunit2 = UnitsField2(cx, cy)
                                    Attacked(theunit) = 1

                                    If cbAnimation.Checked = True Then
                                        Me.ShowArtilleryAxis(1, LastPosx, LastPosy)
                                        Me.ShowAnimationAllies(1, cx, cy)
                                        System.Threading.Thread.Sleep(300)
                                        Me.ShowArtilleryAxis(2, LastPosx, LastPosy)
                                        Me.ShowAnimationAllies(2, cx, cy)
                                        Me.ShowCountries()

                                    End If

                                    'Me.ShowAlliesReturn()

                                End If
                                Moved = True
                                Return
                            End If
                        ElseIf utype = TypeTank Then
                            If LastPosx >= cx - 1 And LastPosx <= cx + 1 And LastPosy >= cy - 1 And LastPosy <= cy + 1 Then 'right spot ?
                                theunit = UnitsField(LastPosx, LastPosy)
                                If Attacked(theunit) = 0 Then
                                    Me.AttackAxis(LastPosx, LastPosy, cx, cy)
                                    Attacked(theunit) = 1
                                    If cbAnimation.Checked = True Then
                                        Me.ShowTankAxis(1, LastPosx, LastPosy)
                                        Me.ShowAnimationAllies(1, cx, cy)
                                        System.Threading.Thread.Sleep(300)
                                        Me.ShowAnimationAllies(2, cx, cy)
                                        Me.ShowTankAxis(2, LastPosx, LastPosy)
                                        Me.ShowCountries()

                                    End If

                                End If
                            End If
                        ElseIf utype = TypeInfantery Then
                            If LastPosx >= cx - 1 And LastPosx <= cx + 1 And LastPosy >= cy - 1 And LastPosy <= cy + 1 Then 'right spot ?
                                theunit = UnitsField(LastPosx, LastPosy)
                                If Attacked(theunit) = 0 Then
                                    Me.AttackAxis(LastPosx, LastPosy, cx, cy)
                                    Attacked(theunit) = 1
                                    If cbAnimation.Checked = True Then
                                        Me.ShowAnimationAllies(1, cx, cy)
                                        Me.ShowInfanteryAxis(1, LastPosx, LastPosy)
                                        System.Threading.Thread.Sleep(300)
                                        Me.ShowInfanteryAxis(2, LastPosx, LastPosy)
                                        Me.ShowAnimationAllies(2, cx, cy)
                                        Me.ShowCountries()

                                    End If

                                End If
                            End If

                        ElseIf utype = TypeAntitank Then
                            If LastPosx >= cx - 1 And LastPosx <= cx + 1 And LastPosy >= cy - 1 And LastPosy <= cy + 1 Then 'right spot ?
                                theunit = UnitsField(LastPosx, LastPosy)
                                If Attacked(theunit) = 0 Then
                                    Me.AttackAxis(LastPosx, LastPosy, cx, cy)
                                    Attacked(theunit) = 1
                                    If cbAnimation.Checked = True Then
                                        Me.ShowAnimationAllies(1, cx, cy)
                                        Me.ShowAntitankAxis(1, LastPosx, LastPosy)
                                        System.Threading.Thread.Sleep(300)
                                        Me.ShowAntitankAxis(2, LastPosx, LastPosy)
                                        Me.ShowAnimationAllies(2, cx, cy)
                                        Me.ShowCountries()

                                    End If

                                End If
                            End If

                        Else
                            LastPosx = 0
                            LastPosy = 0
                            Return

                        End If
                        posx = HexesPosx + ((LastPosx - 1) * RasterWidth)
                        posy = HexesPosy - ((LastPosy - 1) * 2 * RasterHeight2)


                    ElseIf UnitsFieldAir2(cx, cy) > 0 And UnitsFieldAir(LastPosx, LastPosy) > 0 Then 'clicked on an enemy plane!  attack ?
                        theunit = UnitsFieldAir(LastPosx, LastPosy)
                        utype = Units(theunit)

                        If utype = TypeFighter Then
                            If LastPosx >= cx - 1 And LastPosx <= cx + 1 And LastPosy >= cy - 1 And LastPosy <= cy + 1 Then 'right spot ?
                                If Attacked(theunit) = 0 Then
                                    Me.AttackAxisAir(LastPosx, LastPosy, cx, cy)
                                    Attacked(theunit) = 1
                                    If cbAnimation.Checked = True Then
                                        Me.ShowAnimationAllies(1, cx, cy)
                                        Me.ShowFighterAxis(1, LastPosx, LastPosy)
                                        System.Threading.Thread.Sleep(300)
                                        Me.ShowFighterAxis(2, LastPosx, LastPosy)
                                        Me.ShowAnimationAllies(2, cx, cy)
                                        Me.ShowCountries()

                                    End If
                                End If
                                Moved = True
                                Return

                            Else
                                LastPosx = cx
                                LastPosy = cy
                                Return

                            End If
                        ElseIf utype = TypeBomber Then
                            If LastPosx >= cx - 1 And LastPosx <= cx + 1 And LastPosy >= cy - 1 And LastPosy <= cy + 1 Then 'right spot ?
                                If Attacked(theunit) = 0 Then
                                    Me.AttackAxisAir(LastPosx, LastPosy, cx, cy)
                                    Attacked(theunit) = 1
                                    If cbAnimation.Checked = True Then
                                        Me.ShowAnimationAllies(1, cx, cy)
                                        Me.ShowBomberAxis(1, LastPosx, LastPosy)
                                        System.Threading.Thread.Sleep(300)
                                        Me.ShowBomberAxis(2, LastPosx, LastPosy)
                                        Me.ShowAnimationAllies(2, cx, cy)
                                        Me.ShowCountries()

                                    End If

                                End If
                            Else
                                LastPosx = cx
                                LastPosy = cy
                                Return
                            End If
                        Else
                            LastPosx = cx
                            LastPosy = cy
                            Return
                        End If

                        posx = HexesPosx + ((LastPosx - 1) * RasterWidth)
                        posy = HexesPosy - ((LastPosy - 1) * 2 * RasterHeight2)

                    Else
                        LastPosx = cx
                        LastPosy = cy
                        Return
                    End If
                Else
                    If UnitsField2(cx, cy) > 0 Then 'is there an enemy unit under plane ?

                        theunit = UnitsFieldAir(cx, cy)
                        utype = Units(theunit)
                        If utype = TypeFighter Then
                            If Attacked(theunit) = 0 Then
                                Me.AttackAxisAirGround(cx, cy)
                                Attacked(theunit) = 1
                                If cbAnimation.Checked = True Then
                                    Me.ShowAnimationAllies(1, cx, cy)
                                    Me.ShowFighterAxis(1, cx, cy)
                                    System.Threading.Thread.Sleep(300)
                                    Me.ShowFighterAxis(2, cx, cy)
                                    Me.ShowAnimationAllies(2, cx, cy)
                                    Me.ShowCountries()

                                End If
                            End If

                        ElseIf utype = TypeBomber Then
                            If Attacked(theunit) = 0 Then
                                Me.AttackAxisAirGround(cx, cy)
                                Attacked(theunit) = 1
                                If cbAnimation.Checked = True Then
                                    Me.ShowAnimationAllies(1, cx, cy)
                                    Me.ShowBomberAxis(3, cx, cy)
                                    System.Threading.Thread.Sleep(300)
                                    Me.ShowBomberAxis(2, cx, cy)
                                    Me.ShowAnimationAllies(2, cx, cy)
                                    Me.ShowCountries()

                                End If
                            End If

                        End If
                        Moved = True
                    End If
                    posx = HexesPosx + ((cx - 1) * RasterWidth)
                    posy = HexesPosy - ((cy - 1) * 2 * RasterHeight2)

                    CurUnit = UnitsFieldAir(cx, cy)
                    CurUnitSort = "Plane"
                    'Me.ShowParameters()

                End If
            Else

                If LastPosx = cx And LastPosy = cy Then 'click off or plane is present
                    If UnitsFieldAir(cx, cy) > 0 Then
                        CurUnit = UnitsFieldAir(cx, cy)
                        CurUnitSort = "Plane"
                        PRECT.Visible = True
                        posx = HexesPosx + ((cx - 1) * RasterWidth)
                        posy = HexesPosy - ((cy - 1) * 2 * RasterHeight2)

                        'Me.ShowParameters()

                    Else
                        PRECT.Visible = False
                        LastPosx = 0
                        LastPosy = 0

                        Me.ShowUnitAxis(cx, cy)
                        ShowHealthUnit(1, cx, cy, CurUnit)

                        Return
                    End If

                Else
                    CurUnit = UnitsField(cx, cy)
                    CurUnitType = Units(CurUnit)
                    CurUnitSort = "Ground"
                    'Me.ShowParameters()

                    posx = HexesPosx + ((cx - 1) * RasterWidth)
                    posy = HexesPosy - ((cy - 1) * 2 * RasterHeight2)
                    txtEntr.Text = Entrenched(CurUnit).ToString()



                End If

            End If
            CurMA = MA(CurUnit)
            'posx = HexesPosx + ((cx - 1) * RasterWidth)
            'posy = HexesPosy - ((cy - 1) * 2 * RasterHeight2)

            If CurMA >= MAMax(CurUnit) Then
                Curx = cx
                Cury = cy
                LastPosx = cx
                LastPosy = cy
                PRECT.Location = New Point(posx + Panel1.AutoScrollPosition.X + DeltaHexesX, posy + Panel1.AutoScrollPosition.Y)
                PRECT.Visible = True
                Me.ShowParameters()
                Return
            End If
            'If LastPosx = cx And LastPosy = cy Then 'click off
            '    PRECT.Visible = False
            '    Me.ShowUnitAxis(cx, cy)
            '    Return
            'End If

            CurPosx = posx
            CurPosy = posy
            Me.ShowUnitAxis(cx, cy)
            'ShowHealthUnit(1, cx, cy, CurUnit)
            PRECT.Location = New Point(posx + Panel1.AutoScrollPosition.X + DeltaHexesX, posy + Panel1.AutoScrollPosition.Y)
            PRECT.Visible = True

        Else 'Allies
            If UnitsField2(cx, cy) = 0 Then 'no units on field
                If UnitsFieldAir2(cx, cy) = 0 Then 'no planes on field
                    If UnitsField(cx, cy) > 0 Then 'clicked on an enemy !
                        If UnitsField2(LastPosx, LastPosy) > 0 Then 'this is an attack
                            theunit = UnitsField2(LastPosx, LastPosy)
                            utype = Units2(theunit)
                            If utype = TypeArtillery Then
                                'MsgBox(utype)
                                If LastPosx >= cx - 2 And LastPosx <= cx + 2 And LastPosy >= cy - 2 And LastPosy <= cy + 2 Then 'right spot ?
                                    theunit = UnitsField2(LastPosx, LastPosy)
                                    If Attacked2(theunit) = 0 Then
                                        Me.AttackAlliesArtillery(LastPosx, LastPosy, cx, cy)
                                        Attacked2(theunit) = 1
                                        If cbAnimation.Checked = True Then
                                            Me.ShowAnimationAxis(1, cx, cy)
                                            Me.ShowArtilleryAllies(1, LastPosx, LastPosy)
                                            System.Threading.Thread.Sleep(300)
                                            Me.ShowArtilleryAllies(2, LastPosx, LastPosy)
                                            Me.ShowAnimationAxis(2, cx, cy)
                                            Me.ShowCountries()


                                        End If
                                    End If
                                    Moved = True
                                    Return
                                End If
                            ElseIf utype = TypeTank Then
                                If LastPosx >= cx - 1 And LastPosx <= cx + 1 And LastPosy >= cy - 1 And LastPosy <= cy + 1 Then 'right spot ?
                                    theunit = UnitsField2(LastPosx, LastPosy)
                                    If Attacked2(theunit) = 0 Then
                                        Me.AttackAllies(LastPosx, LastPosy, cx, cy)
                                        Attacked2(theunit) = 1
                                        If cbAnimation.Checked = True Then
                                            Me.ShowAnimationAxis(1, cx, cy)
                                            Me.ShowTankAllies(1, LastPosx, LastPosy)
                                            System.Threading.Thread.Sleep(300)
                                            Me.ShowTankAllies(2, LastPosx, LastPosy)
                                            Me.ShowAnimationAxis(2, cx, cy)
                                            Me.ShowCountries()

                                        End If
                                    End If
                                End If
                            ElseIf utype = TypeInfantery Then
                                theunit = UnitsField2(LastPosx, LastPosy)
                                If Attacked2(theunit) = 0 Then
                                    Me.AttackAllies(LastPosx, LastPosy, cx, cy)
                                    Attacked2(theunit) = 1
                                    If cbAnimation.Checked = True Then
                                        Me.ShowAnimationAxis(1, cx, cy)
                                        Me.ShowInfanteryAllies(1, LastPosx, LastPosy)
                                        System.Threading.Thread.Sleep(300)
                                        Me.ShowInfanteryAllies(2, LastPosx, LastPosy)
                                        Me.ShowAnimationAxis(2, cx, cy)
                                        Me.ShowCountries()

                                    End If
                                End If

                            ElseIf utype = TypeAntitank Then
                                If LastPosx >= cx - 1 And LastPosx <= cx + 1 And LastPosy >= cy - 1 And LastPosy <= cy + 1 Then 'right spot ?
                                    theunit = UnitsField2(LastPosx, LastPosy)
                                    If Attacked2(theunit) = 0 Then
                                        Me.AttackAllies(LastPosx, LastPosy, cx, cy)
                                        Attacked2(theunit) = 1
                                        If cbAnimation.Checked = True Then
                                            Me.ShowAnimationAxis(1, cx, cy)
                                            Me.ShowAntitankAllies(1, LastPosx, LastPosy)
                                            System.Threading.Thread.Sleep(300)
                                            Me.ShowAntitankAllies(2, LastPosx, LastPosy)
                                            Me.ShowAnimationAxis(2, cx, cy)
                                            Me.ShowCountries()

                                        End If
                                    End If
                                End If

                            Else
                                LastPosx = 0
                                LastPosy = 0
                                Return

                            End If

                        Else
                            LastPosx = cx
                            LastPosy = cy
                            Return
                        End If

                    ElseIf UnitsFieldAir(cx, cy) > 0 Then 'clicked on an enemy plane!
                        If UnitsFieldAir2(LastPosx, LastPosy) > 0 Then 'this is an air attack
                            theunit = UnitsFieldAir2(LastPosx, LastPosy)
                            utype = Units2(theunit)
                            If utype = TypeFighter Then
                                If LastPosx >= cx - 1 And LastPosx <= cx + 1 And LastPosy >= cy - 1 And LastPosy <= cy + 1 Then 'right spot ?
                                    If Attacked2(theunit) = 0 Then
                                        Me.AttackAlliesAir(LastPosx, LastPosy, cx, cy)
                                        Attacked2(theunit) = 1
                                        If cbAnimation.Checked = True Then
                                            Me.ShowAnimationAxis(1, cx, cy)
                                            Me.ShowFighterAllies(1, LastPosx, LastPosy)
                                            System.Threading.Thread.Sleep(300)
                                            Me.ShowFighterAllies(2, LastPosx, LastPosy)
                                            Me.ShowAnimationAxis(2, cx, cy)
                                            Me.ShowCountries()

                                        End If
                                    End If
                                    Moved = True
                                    Return
                                Else
                                    LastPosx = cx
                                    LastPosy = cy
                                    Return
                                End If

                            ElseIf utype = TypeBomber Then
                                If LastPosx >= cx - 1 And LastPosx <= cx + 1 And LastPosy >= cy - 1 And LastPosy <= cy + 1 Then 'right spot ?
                                    If Attacked2(theunit) = 0 Then
                                        Me.AttackAlliesAir(LastPosx, LastPosy, cx, cy)
                                        Attacked2(theunit) = 1
                                        If cbAnimation.Checked = True Then
                                            Me.ShowAnimationAxis(1, cx, cy)
                                            Me.ShowBomberAllies(1, LastPosx, LastPosy)
                                            System.Threading.Thread.Sleep(300)
                                            Me.ShowBomberAllies(2, LastPosx, LastPosy)
                                            Me.ShowAnimationAxis(2, cx, cy)
                                            Me.ShowCountries()

                                        End If
                                    End If
                                Else
                                    LastPosx = cx
                                    LastPosy = cy
                                    Return
                                End If


                            Else
                                LastPosx = cx
                                LastPosy = cy
                                Return
                            End If

                        End If


                    Else
                        LastPosx = cx
                        LastPosy = cy
                        Return

                    End If
                Else
                    If UnitsField(cx, cy) > 0 Then 'is there an enemy unit under plane ?

                        theunit = UnitsFieldAir2(cx, cy)
                        utype = Units2(theunit)
                        If utype = TypeFighter Then
                            If Attacked2(theunit) = 0 Then
                                Me.AttackAlliesAirGround(cx, cy)
                                Attacked2(theunit) = 1
                                If cbAnimation.Checked = True Then
                                    Me.ShowAnimationAxis(1, cx, cy)
                                    Me.ShowFighterAllies(1, LastPosx, LastPosy)
                                    System.Threading.Thread.Sleep(300)
                                    Me.ShowFighterAllies(2, LastPosx, LastPosy)
                                    Me.ShowAnimationAxis(2, cx, cy)
                                    Me.ShowCountries()

                                End If
                            End If

                        ElseIf utype = TypeBomber Then
                            If Attacked2(theunit) = 0 Then
                                Me.AttackAlliesAirGround(cx, cy)
                                Attacked2(theunit) = 1
                                If cbAnimation.Checked = True Then
                                    Me.ShowAnimationAxis(1, cx, cy)
                                    Me.ShowBomberAllies(3, LastPosx, LastPosy)
                                    System.Threading.Thread.Sleep(300)
                                    Me.ShowBomberAllies(2, LastPosx, LastPosy)
                                    Me.ShowAnimationAxis(2, cx, cy)
                                    Me.ShowCountries()

                                End If
                            End If
                        End If
                        Moved = True
                    End If

                    CurUnit = UnitsFieldAir2(cx, cy)
                    CurUnitSort = "Plane"
                    'Me.ShowParameters()

                End If
            Else
                If LastPosx = cx And LastPosy = cy Then 'click off or plane is present
                    If UnitsFieldAir2(cx, cy) > 0 Then
                        CurUnit = UnitsFieldAir2(cx, cy)
                        CurUnitSort = "Plane"
                        PRECT.Visible = True
                        'Me.ShowParameters()

                    Else
                        PRECT.Visible = False
                        Me.ShowUnitAllies(cx, cy)
                        ShowHealthUnit(2, cx, cy, CurUnit)
                        LastPosx = 0
                        LastPosy = 0

                        Return
                    End If

                Else
                    CurUnit = UnitsField2(cx, cy)
                    CurUnitType = Units2(CurUnit)
                    CurUnitSort = "Ground"
                    'Me.ShowParameters()

                    txtEntr.Text = Entrenched2(CurUnit).ToString()


                End If
                posx = HexesPosx + ((cx - 1) * RasterWidth)
                posy = HexesPosy - ((cy - 1) * 2 * RasterHeight2)
                PRECT.Location = New Point(posx + Panel1.AutoScrollPosition.X + DeltaHexesX2, posy + Panel1.AutoScrollPosition.Y)
                PRECT.Visible = True

            End If
            'adjust for Allies
            CurMA = MA2(CurUnit)
            posx = HexesPosx + ((cx - 1) * RasterWidth)
            posy = HexesPosy - ((cy - 1) * 2 * RasterHeight2)
            If CurMA >= MAMax2(CurUnit) Then
                Curx = cx
                Cury = cy

                LastPosx = cx
                LastPosy = cy
                PRECT.Location = New Point(posx + Panel1.AutoScrollPosition.X + DeltaHexesX2, posy + Panel1.AutoScrollPosition.Y)
                PRECT.Visible = True
                Me.ShowParameters()
                Return
            End If
            ''adjust for Allies
            'If LastPosx = cx And LastPosy = cy Then 'click off
            '    PRECT.Visible = False
            '    Me.ShowUnitAllies(cx, cy)
            '    Return
            'End If

            CurPosx = posx
            CurPosy = posy
            Me.ShowUnitAllies(cx, cy)
            'ShowHealthUnit(2, cx, cy, CurUnit)
            PRECT.Location = New Point(posx + Panel1.AutoScrollPosition.X + DeltaHexesX2, posy + Panel1.AutoScrollPosition.Y)
            PRECT.Visible = True
            'Me.ShowControls2(cx, cy)

        End If

        Curx = cx
        Cury = cy
        Me.ShowParameters()

        LastPosx = cx
        LastPosy = cy
        LastUnit = CurUnit
        'UnitAreaLast(CurUnit) = UnitArea(CurUnit)


    End Sub



    Private Sub Panel1_Scroll1(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles Panel1.Scroll


        If e.ScrollOrientation = ScrollOrientation.VerticalScroll Then
            CurPosy = e.NewValue
            Panel1.AutoScrollPosition = New Drawing.Point(StartPosx, (CurPosy))
            TextBox3.Text = CurPosy.ToString()
            StartPosy = CurPosy

        ElseIf e.ScrollOrientation = ScrollOrientation.HorizontalScroll Then
            CurPosx = e.NewValue
            Panel1.AutoScrollPosition = New Drawing.Point((CurPosx), StartPosy)
            'TextBox3.Text = CurPosy.ToString()
            StartPosx = CurPosx


        End If


    End Sub


    Private Sub cbxNorth_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxNorth.Click

        If cbxNorth.Checked = False Then
            cbxNorth.Checked = False
            cbxSouth.Checked = False
        Else
            cbxNorth.Checked = True
            cbxSouth.Checked = False
        End If

    End Sub


    Private Sub cbxSouth_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxSouth.Click

        If cbxSouth.Checked = False Then
            cbxSouth.Checked = False
            cbxNorth.Checked = False
        Else
            cbxSouth.Checked = True
            cbxNorth.Checked = False
        End If



    End Sub

    Private Sub MoveUp2(ByVal direction As String)
        Dim posx, posy As Integer
        Dim CurMA As Integer
        Dim unittype, unitnr2, unittype2 As Integer

        If Cury = MaxRasterY Then
            Return
        End If
        If Fuel2(CurUnit) = 1 Then
            unittype = Units2(CurUnit)
            If (unittype = TypeFighter Or unittype = TypeBomber) Then
                If Me.CheckAirportReached(2, Curx, Cury, CurUnit) = 0 Then 'airplane crashed
                    Return
                End If

            End If
        End If

        If UnitsField(Curx, Cury + 1) > 0 Or UnitsField2(Curx, Cury + 1) Or UnitsFieldAir(Curx, Cury + 1) > 0 Or UnitsFieldAir2(Curx, Cury + 1) > 0 Then 'Place occupied

            unittype = Units2(CurUnit)
            If unittype <> TypeFighter And unittype <> TypeBomber Then
                'ground unit is moved
                unitnr2 = UnitsFieldAir2(Curx, Cury + 1)
                If unitnr2 > 0 Then
                    unittype2 = Units2(unitnr2)

                Else
                    Return
                End If

            Else 'Plane
                Dim EnemyNr, EnemyType As Integer
                EnemyNr = UnitsFieldAir(Curx, Cury + 1)
                EnemyType = Units(EnemyNr)

                If EnemyType = TypeFighter Or EnemyType = TypeBomber Then 'Eenemy Plane
                    Return
                End If
            End If

        End If

        PRECT.Visible = False

        If Me.CheckAlliesMA(Curx, Cury + 1) = 1 Then
            Return
        End If

        'Me.RemoveUnit2(CurUnit, Curx, Cury)
        'Me.RemoveUnitNew(Curx, Cury)

        If CurUnitSort = "Ground" Then
            UnitsField2(Curx, Cury + 1) = UnitsField2(Curx, Cury)
            UnitsField2(Curx, Cury) = 0
        Else
            UnitsFieldAir2(Curx, Cury + 1) = UnitsFieldAir2(Curx, Cury)
            UnitsFieldAir2(Curx, Cury) = 0
        End If

        LastPositionx = Curx
        LastPositiony = Cury
        Curx = Curx
        Cury = Cury + 1
        LastPosx = Curx
        LastPosy = Cury
        Moved = True


        UnitsX2(CurUnit) = Curx
        UnitsY2(CurUnit) = Cury
        Entrenched2(CurUnit) = 0
        If Fuel2(CurUnit) > 0 Then
            Fuel2(CurUnit) = Fuel2(CurUnit) - 1
        End If
        Me.ShowParameters()

        MyUnitsAlliesDS.Tables(0).Rows(CurUnit - 1).Item(2) = Curx
        MyUnitsAlliesDS.Tables(0).Rows(CurUnit - 1).Item(3) = Cury
        Me.CheckCountries(2, Curx, Cury)

        Me.RemoveUnitNew(Curx, Cury - 1)

        Me.ShowUnitAllies(Curx, Cury - 1)
        Me.ShowUnitAllies(Curx, Cury)
        Me.ShowCountries()

        posx = HexesPosx + ((Curx - 1) * RasterWidth) + DeltaHexesX2
        posy = HexesPosy - ((Cury - 1) * 2 * RasterHeight2)
        PRECT.Location = New Point(posx + Panel1.AutoScrollPosition.X, posy + Panel1.AutoScrollPosition.Y)
        PRECT.Visible = True

    End Sub

    Private Sub MoveUp(ByVal direction As String)
        Dim unittype, unitnr2, unittype2 As Integer
        Dim posx, posy As Integer
        Dim CurMA As Integer

        If Cury = MaxRasterY Then
            Return
        End If
        If Fuel(CurUnit) = 1 Then
            unittype = Units(CurUnit)
            If (unittype = TypeFighter Or unittype = TypeBomber) Then
                If Me.CheckAirportReached(1, Curx, Cury, CurUnit) = 0 Then 'airplane crashed
                    Return
                End If

            End If
        End If

        If UnitsField(Curx, Cury + 1) > 0 Or UnitsField2(Curx, Cury + 1) Or UnitsFieldAir(Curx, Cury + 1) > 0 Or UnitsFieldAir2(Curx, Cury + 1) > 0 Then 'Place occupied

            unittype = Units(CurUnit)
            If unittype <> TypeFighter And unittype <> TypeBomber Then
                'ground unit is moved
                unitnr2 = UnitsFieldAir(Curx, Cury + 1)
                If unitnr2 > 0 Then
                    unittype2 = Units2(unitnr2)

                Else
                    Return
                End If

            Else 'Plane
                Dim EnemyNr, EnemyType As Integer
                EnemyNr = UnitsFieldAir2(Curx, Cury + 1)
                EnemyType = Units2(EnemyNr)

                If EnemyType = TypeFighter Or EnemyType = TypeBomber Then 'Eenemy Plane
                    Return
                End If
            End If

        End If

        PRECT.Visible = False

        If Me.CheckAxisMA(Curx, Cury + 1) = 1 Then
            Return
        End If


        If CurUnitSort = "Ground" Then
            UnitsField(Curx, Cury + 1) = UnitsField(Curx, Cury)
            UnitsField(Curx, Cury) = 0
        Else
            UnitsFieldAir(Curx, Cury + 1) = UnitsFieldAir(Curx, Cury)
            UnitsFieldAir(Curx, Cury) = 0
        End If

        LastPositionx = Curx
        LastPositiony = Cury
        Curx = Curx
        Cury = Cury + 1
        LastPosx = Curx
        LastPosy = Cury
        Moved = True

        UnitsX(CurUnit) = Curx
        UnitsY(CurUnit) = Cury
        Entrenched(CurUnit) = 0

        If Fuel(CurUnit) > 0 Then
            Fuel(CurUnit) = Fuel(CurUnit) - 1
        End If
        Me.ShowParameters()

        MyUnitsAxisDS.Tables(0).Rows(CurUnit - 1).Item(2) = Curx
        MyUnitsAxisDS.Tables(0).Rows(CurUnit - 1).Item(3) = Cury
        Me.CheckCountries(1, Curx, Cury)

        Me.RemoveUnitNew(Curx, Cury - 1)

        Me.ShowUnitAxis(Curx, Cury - 1)
        Me.ShowUnitAxis(Curx, Cury)
        Me.ShowCountries()

        posx = HexesPosx + ((Curx - 1) * RasterWidth) + DeltaHexesX
        posy = HexesPosy - ((Cury - 1) * 2 * RasterHeight2)
        PRECT.Location = New Point(posx + Panel1.AutoScrollPosition.X, posy + Panel1.AutoScrollPosition.Y)
        PRECT.Visible = True

    End Sub

    Private Function GetSurface(ByVal surfid As String) As String
        Dim surface As String

        Select Case surfid

            Case 0
                surface = "normal"
            Case 1
                surface = "water"

            Case 2
                surface = "forest"

            Case 3
                surface = "rough"

            Case Else
                surface = "6"



        End Select


        Return surface


    End Function
    Private Sub BUP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUP.Click

        Me.MoveArmy("Up")
        Dim str1 As String

        str1 = FieldSurface(Cury, Curx).ToString()
        TextBox8.Text = GetSurface(FieldSurface(Cury, Curx))

    End Sub

    Private Sub MoveDown2(ByVal direction As String)
        Dim posx, posy As Integer
        Dim CurMA As Integer
        Dim unittype, unitnr2, unittype2 As Integer

        If Cury = 1 Then
            Return
        End If
        If Fuel2(CurUnit) = 1 Then
            unittype = Units2(CurUnit)
            If (unittype = TypeFighter Or unittype = TypeBomber) Then
                If Me.CheckAirportReached(2, Curx, Cury, CurUnit) = 0 Then 'airplane crashed
                    Return
                End If

            End If
        End If

        If UnitsField(Curx, Cury - 1) > 0 Or UnitsField2(Curx, Cury - 1) > 0 Or UnitsFieldAir(Curx, Cury - 1) > 0 Or UnitsFieldAir2(Curx, Cury - 1) > 0 Then 'Place occupied

            unittype = Units2(CurUnit)
            If unittype <> TypeFighter And unittype <> TypeBomber Then
                'ground unit is moved
                unitnr2 = UnitsFieldAir2(Curx, Cury - 1)
                If unitnr2 > 0 Then
                    unittype2 = Units2(unitnr2)

                Else
                    Return
                End If

            Else 'Plane
                Dim EnemyNr, EnemyType As Integer
                EnemyNr = UnitsFieldAir(Curx, Cury - 1)
                EnemyType = Units(EnemyNr)

                If EnemyType = TypeFighter Or EnemyType = TypeBomber Then 'Eenemy Plane
                    Return
                End If
            End If

        End If

        PRECT.Visible = False

        If Me.CheckAlliesMA(Curx, Cury - 1) = 1 Then
            Return
        End If

        If CurUnitSort = "Ground" Then
            UnitsField2(Curx, Cury - 1) = UnitsField2(Curx, Cury)
            UnitsField2(Curx, Cury) = 0
        Else
            UnitsFieldAir2(Curx, Cury - 1) = UnitsFieldAir2(Curx, Cury)
            UnitsFieldAir2(Curx, Cury) = 0
        End If

        LastPositionx = Curx
        LastPositiony = Cury
        Curx = Curx
        Cury = Cury - 1
        LastPosx = Curx
        LastPosy = Cury
        Moved = True

        UnitsX2(CurUnit) = Curx
        UnitsY2(CurUnit) = Cury
        Entrenched2(CurUnit) = 0
        If Fuel2(CurUnit) > 0 Then
            Fuel2(CurUnit) = Fuel2(CurUnit) - 1
        End If
        Me.ShowParameters()

        MyUnitsAlliesDS.Tables(0).Rows(CurUnit - 1).Item(2) = Curx
        MyUnitsAlliesDS.Tables(0).Rows(CurUnit - 1).Item(3) = Cury
        Me.CheckCountries(2, Curx, Cury)

        Me.RemoveUnitNew(Curx, Cury + 1)

        Me.ShowUnitAllies(Curx, Cury + 1)
        Me.ShowUnitAllies(Curx, Cury)
        Me.ShowCountries()

        posx = HexesPosx + ((Curx - 1) * RasterWidth) + DeltaHexesX2
        posy = HexesPosy - ((Cury - 1) * 2 * RasterHeight2)
        PRECT.Location = New Point(posx + Panel1.AutoScrollPosition.X, posy + Panel1.AutoScrollPosition.Y)
        PRECT.Visible = True

    End Sub

    Private Sub MoveDown(ByVal direction As String)

        Dim posx, posy As Integer
        Dim CurMA As Integer
        Dim unittype, unitnr2, unittype2 As Integer

        If Cury = 1 Then
            Return
        End If
        If Fuel(CurUnit) = 1 Then
            unittype = Units(CurUnit)
            If (unittype = TypeFighter Or unittype = TypeBomber) Then
                If Me.CheckAirportReached(1, Curx, Cury, CurUnit) = 0 Then 'airplane crashed
                    Return
                End If

            End If
        End If

        If UnitsField(Curx, Cury - 1) > 0 Or UnitsField2(Curx, Cury - 1) > 0 Or UnitsFieldAir(Curx, Cury - 1) > 0 Or UnitsFieldAir2(Curx, Cury - 1) > 0 Then 'Place occupied

            unittype = Units(CurUnit)
            If unittype <> TypeFighter And unittype <> TypeBomber Then
                'ground unit is moved
                unitnr2 = UnitsFieldAir(Curx, Cury - 1)
                If unitnr2 > 0 Then
                    unittype2 = Units2(unitnr2)

                Else
                    Return
                End If

            Else 'Plane
                Dim EnemyNr, EnemyType As Integer
                EnemyNr = UnitsFieldAir2(Curx, Cury - 1)
                EnemyType = Units2(EnemyNr)

                If EnemyType = TypeFighter Or EnemyType = TypeBomber Then 'Eenemy Plane
                    Return
                End If
            End If

        End If

        PRECT.Visible = False

        If Me.CheckAxisMA(Curx, Cury - 1) = 1 Then
            Return
        End If

        'Me.RemoveUnit(CurUnit, Curx, Cury)
        'Me.RemoveUnitNew(Curx, Cury)

        If CurUnitSort = "Ground" Then
            UnitsField(Curx, Cury - 1) = UnitsField(Curx, Cury)
            UnitsField(Curx, Cury) = 0
        Else
            UnitsFieldAir(Curx, Cury - 1) = UnitsFieldAir(Curx, Cury)
            UnitsFieldAir(Curx, Cury) = 0
        End If

        LastPositionx = Curx
        LastPositiony = Cury
        Curx = Curx
        Cury = Cury - 1
        LastPosx = Curx
        LastPosy = Cury
        Moved = True

        UnitsX(CurUnit) = Curx
        UnitsY(CurUnit) = Cury
        Entrenched(CurUnit) = 0
        If Fuel(CurUnit) > 0 Then
            Fuel(CurUnit) = Fuel(CurUnit) - 1
        End If
        Me.ShowParameters()

        MyUnitsAxisDS.Tables(0).Rows(CurUnit - 1).Item(2) = Curx
        MyUnitsAxisDS.Tables(0).Rows(CurUnit - 1).Item(3) = Cury
        Me.CheckCountries(1, Curx, Cury)

        Me.RemoveUnitNew(Curx, Cury + 1)

        Me.ShowUnitAxis(Curx, Cury + 1)
        Me.ShowUnitAxis(Curx, Cury)
        Me.ShowCountries()

        posx = HexesPosx + ((Curx - 1) * RasterWidth) + DeltaHexesX
        posy = HexesPosy - ((Cury - 1) * 2 * RasterHeight2)
        PRECT.Location = New Point(posx + Panel1.AutoScrollPosition.X, posy + Panel1.AutoScrollPosition.Y)
        PRECT.Visible = True

    End Sub

    Private Sub BDOWN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BDOWN.Click

        Me.MoveArmy("Down")
        Dim str1 As String

        str1 = FieldSurface(Cury, Curx).ToString()
        TextBox8.Text = GetSurface(FieldSurface(Cury, Curx))

    End Sub


    Private Sub ShowRect(ByVal px As Integer, ByVal py As Integer)
        Dim posx, posy As Integer

        'posx = RasterStartx + ((px - 1) * RasterWidth2)
        posx = RasterStartx + ((px - 1) * RasterWidth)


        'posy = RasterStarty - ((py - 1) * 2 * RasterHeight)
        posy = RasterStarty - ((py - 1) * 2 * RasterHeight2)


        Me.ShowHex("vierkant", posx, posy)

    End Sub



    Private Sub Button6_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click


        PB1.Image = MyGameFieldRaster.Clone()

        Me.ShowAllUnitsAxis()
        Me.ShowCountries()
        Me.ShowHealth(1)

        Me.ShowAllUnitsAllies()
        Me.ShowHealth(2)

        'CurPosx = RasterStartx
        'CurPosy = RasterStarty
        'NrofMoves = 0


        'For j = 1 To MaxRasterY
        '    For i = 1 To MaxRasterX
        '        Me.ShowHex("vierkant", CurPosx, CurPosy)
        '        'Me.ShowRect(i, j)
        '        NrofMoves = NrofMoves + 1
        '        CurPosx = CurPosx + RasterWidth2 - 2
        '    Next
        '    NrofMoves = 0
        '    CurPosx = RasterStartx
        '    RasterStarty = RasterStarty - RasterHeight - RasterHeight + 2
        '    CurPosy = RasterStarty

        'Next

    End Sub



    Private Sub MoveUpRight2(ByVal direction As String)
        Dim posx, posy As Integer
        Dim CurMA As Integer
        Dim unittype, unitnr2, unittype2 As Integer

        If Curx = MaxRasterX Or Cury = MaxRasterY Then
            Return
        End If
        If Fuel2(CurUnit) = 1 Then
            unittype = Units2(CurUnit)
            If (unittype = TypeFighter Or unittype = TypeBomber) Then
                If Me.CheckAirportReached(2, Curx, Cury, CurUnit) = 0 Then 'airplane crashed
                    Return
                End If

            End If
        End If

        PRECT.Visible = False

        If Me.CheckAlliesMADiag(Curx + 1, Cury + 1) = 1 Then
            Return
        End If

        If UnitsField(Curx + 1, Cury + 1) > 0 Or UnitsField2(Curx + 1, Cury + 1) Or UnitsFieldAir(Curx + 1, Cury + 1) > 0 Or UnitsFieldAir2(Curx + 1, Cury + 1) > 0 Then 'Place occupied

            unittype = Units2(CurUnit)
            If unittype <> TypeFighter And unittype <> TypeBomber Then
                'ground unit is moved
                unitnr2 = UnitsFieldAir2(Curx, Cury - 1)
                If unitnr2 > 0 Then
                    unittype2 = Units2(unitnr2)

                Else
                    Return
                End If

            Else 'Plane
                Dim EnemyNr, EnemyType As Integer
                EnemyNr = UnitsFieldAir(Curx, Cury - 1)
                EnemyType = Units(EnemyNr)

                If EnemyType = TypeFighter Or EnemyType = TypeBomber Then 'Eenemy Plane
                    Return
                End If
            End If

        End If

        'Me.RemoveUnit2(CurUnit, Curx, Cury)
        'Me.RemoveUnitNew(Curx, Cury)

        If CurUnitSort = "Ground" Then
            UnitsField2(Curx + 1, Cury + 1) = UnitsField2(Curx, Cury)
            UnitsField2(Curx, Cury) = 0
        Else
            UnitsFieldAir2(Curx + 1, Cury + 1) = UnitsFieldAir2(Curx, Cury)
            UnitsFieldAir2(Curx, Cury) = 0
        End If

        LastPositionx = Curx
        LastPositiony = Cury
        Curx = Curx + 1
        Cury = Cury + 1
        LastPosx = Curx
        LastPosy = Cury
        Moved = True


        UnitsX2(CurUnit) = Curx
        UnitsY2(CurUnit) = Cury
        Entrenched2(CurUnit) = 0
        If Fuel2(CurUnit) > 0 Then
            Fuel2(CurUnit) = Fuel2(CurUnit) - 1
        End If
        Me.ShowParameters()

        MyUnitsAlliesDS.Tables(0).Rows(CurUnit - 1).Item(2) = Curx
        MyUnitsAlliesDS.Tables(0).Rows(CurUnit - 1).Item(3) = Cury
        Me.CheckCountries(2, Curx, Cury)


        Me.RemoveUnitNew(Curx - 1, Cury - 1)

        Me.ShowUnitAllies(Curx - 1, Cury - 1)
        Me.ShowUnitAllies(Curx, Cury)
        Me.ShowCountries()

        posx = HexesPosx + ((Curx - 1) * RasterWidth) + DeltaHexesX2
        posy = HexesPosy - ((Cury - 1) * 2 * RasterHeight2)
        PRECT.Location = New Point(posx + Panel1.AutoScrollPosition.X, posy + Panel1.AutoScrollPosition.Y)
        PRECT.Visible = True


    End Sub
    Private Sub MoveUpRight(ByVal direction As String)

        Dim posx, posy As Integer
        Dim CurMA As Integer
        Dim unittype, unitnr2, unittype2 As Integer

        If Curx = MaxRasterX Or Cury = MaxRasterY Then
            Return
        End If
        If Fuel(CurUnit) = 1 Then
            unittype = Units(CurUnit)
            If (unittype = TypeFighter Or unittype = TypeBomber) Then
                If Me.CheckAirportReached(1, Curx, Cury, CurUnit) = 0 Then 'airplane crashed
                    Return
                End If

            End If
        End If


        PRECT.Visible = False

        If Me.CheckAxisMADiag(Curx + 1, Cury + 1) = 1 Then
            Return
        End If

        If UnitsField(Curx + 1, Cury + 1) > 0 Or UnitsField2(Curx + 1, Cury + 1) > 0 Or UnitsFieldAir(Curx + 1, Cury + 1) > 0 Or UnitsFieldAir2(Curx + 1, Cury + 1) > 0 Then 'Place occupied

            unittype = Units(CurUnit)
            If unittype <> TypeFighter And unittype <> TypeBomber Then
                'ground unit is moved
                unitnr2 = UnitsFieldAir(Curx + 1, Cury + 1)
                If unitnr2 > 0 Then
                    unittype2 = Units2(unitnr2)

                Else
                    Return
                End If

            Else 'Plane
                Dim EnemyNr, EnemyType As Integer
                EnemyNr = UnitsFieldAir2(Curx + 1, Cury + 1)
                EnemyType = Units2(EnemyNr)

                If EnemyType = TypeFighter Or EnemyType = TypeBomber Then 'Eenemy Plane
                    Return
                End If
            End If

        End If


        'Me.RemoveUnit(CurUnit, Curx, Cury)
        'Me.RemoveUnitNew(Curx, Cury)

        If CurUnitSort = "Ground" Then
            UnitsField(Curx + 1, Cury + 1) = UnitsField(Curx, Cury)
            UnitsField(Curx, Cury) = 0
        Else
            UnitsFieldAir(Curx + 1, Cury + 1) = UnitsFieldAir(Curx, Cury)
            UnitsFieldAir(Curx, Cury) = 0
        End If

        LastPositionx = Curx
        LastPositiony = Cury
        Curx = Curx + 1
        Cury = Cury + 1
        LastPosx = Curx
        LastPosy = Cury
        Moved = True


        UnitsX(CurUnit) = Curx
        UnitsY(CurUnit) = Cury
        Entrenched(CurUnit) = 0
        If Fuel(CurUnit) > 0 Then
            Fuel(CurUnit) = Fuel(CurUnit) - 1
        End If
        Me.ShowParameters()

        MyUnitsAxisDS.Tables(0).Rows(CurUnit - 1).Item(2) = Curx
        MyUnitsAxisDS.Tables(0).Rows(CurUnit - 1).Item(3) = Cury
        Me.CheckCountries(1, Curx, Cury)

        Me.RemoveUnitNew(Curx - 1, Cury - 1)

        Me.ShowUnitAxis(Curx - 1, Cury - 1)
        Me.ShowUnitAxis(Curx, Cury)
        Me.ShowCountries()

        posx = HexesPosx + ((Curx - 1) * RasterWidth) + DeltaHexesX
        posy = HexesPosy - ((Cury - 1) * 2 * RasterHeight2)
        PRECT.Location = New Point(posx + Panel1.AutoScrollPosition.X, posy + Panel1.AutoScrollPosition.Y)
        PRECT.Visible = True


    End Sub
    Private Sub BUPR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUPR.Click


        Me.MoveArmy("UpRight")
        Dim str1 As String

        str1 = FieldSurface(Cury, Curx).ToString()
        TextBox8.Text = GetSurface(FieldSurface(Cury, Curx))

    End Sub

    Private Sub MoveRight2(ByVal direction As String)
        Dim posx, posy As Integer
        Dim CurMA As Integer
        Dim unittype, unitnr2, unittype2 As Integer

        If Curx = MaxRasterX Then
            Return
        End If
        If Fuel2(CurUnit) = 1 Then
            unittype = Units2(CurUnit)
            If (unittype = TypeFighter Or unittype = TypeBomber) Then
                If Me.CheckAirportReached(2, Curx, Cury, CurUnit) = 0 Then 'airplane crashed
                    Return
                End If

            End If
        End If

        If UnitsField(Curx + 1, Cury) > 0 Or UnitsField2(Curx + 1, Cury) > 0 And UnitsFieldAir(Curx + 1, Cury) > 0 Or UnitsFieldAir2(Curx + 1, Cury) > 0 Then 'Place occupied

            unittype = Units2(CurUnit)
            If unittype <> TypeFighter And unittype <> TypeBomber Then
                'ground unit is moved
                unitnr2 = UnitsFieldAir2(Curx + 1, Cury)
                If unitnr2 > 0 Then
                    unittype2 = Units2(unitnr2)

                Else
                    Return
                End If

            Else 'Plane
                Dim EnemyNr, EnemyType As Integer
                EnemyNr = UnitsFieldAir(Curx + 1, Cury)
                EnemyType = Units(EnemyNr)

                If EnemyType = TypeFighter Or EnemyType = TypeBomber Then 'Eenemy Plane
                    Return
                End If
            End If

        End If
        PRECT.Visible = False

        If Me.CheckAlliesMA(Curx + 1, Cury) = 1 Then
            Return
        End If

        'Me.RemoveUnit2(CurUnit, Curx, Cury)
        'Me.RemoveUnitNew(Curx, Cury)

        If CurUnitSort = "Ground" Then
            UnitsField2(Curx + 1, Cury) = UnitsField2(Curx, Cury)
            UnitsField2(Curx, Cury) = 0
        Else
            UnitsFieldAir2(Curx + 1, Cury) = UnitsFieldAir2(Curx, Cury)
            UnitsFieldAir2(Curx, Cury) = 0
        End If

        LastPositionx = Curx
        LastPositiony = Cury
        Curx = Curx + 1
        Cury = Cury
        LastPosx = Curx
        LastPosy = Cury
        Moved = True

        UnitsX2(CurUnit) = Curx
        UnitsY2(CurUnit) = Cury
        Entrenched2(CurUnit) = 0
        If Fuel2(CurUnit) > 0 Then
            Fuel2(CurUnit) = Fuel2(CurUnit) - 1
        End If
        Me.ShowParameters()

        MyUnitsAlliesDS.Tables(0).Rows(CurUnit - 1).Item(2) = Curx
        MyUnitsAlliesDS.Tables(0).Rows(CurUnit - 1).Item(3) = Cury
        Me.CheckCountries(2, Curx, Cury)

        'Get new Image
        'Me.CopyImage2(CurUnit, Curx, Cury)
        Me.RemoveUnitNew(Curx - 1, Cury)

        Me.ShowUnitAllies(Curx - 1, Cury)
        Me.ShowUnitAllies(Curx, Cury)
        Me.ShowCountries()

        posx = HexesPosx + ((Curx - 1) * RasterWidth) + DeltaHexesX2
        posy = HexesPosy - ((Cury - 1) * 2 * RasterHeight2)
        PRECT.Location = New Point(posx + Panel1.AutoScrollPosition.X, posy + Panel1.AutoScrollPosition.Y)
        PRECT.Visible = True

    End Sub

    Private Function CheckAlliesMADiag(ByVal newx As Integer, ByVal newy As Integer) As Integer
        Dim rc, surfaceNow, surfaceNew As Integer
        Dim CurMA As Integer
        Dim transport As Integer

        transport = Mount(CurUnit)
        CurMA = MA2(CurUnit)
        'Check Surface is rough or river
        surfaceNow = FieldSurface(Cury, Curx)
        surfaceNew = FieldSurface(newy, newx)

        If ((surfaceNow > 0) Or (surfaceNew > 0)) And CurMA < MAMax2(CurUnit) And CurUnitSort <> "Plane" Then
            If transport = 0 Then
                CurMA = MAMax2(CurUnit) '- 1
                MA2(CurUnit) = CurMA
            Else
                CurMA = MATransport '2
                MA2(CurUnit) = CurMA
            End If
        End If

        If CurMA < MAMax2(CurUnit) Then
            If MAMax2(CurUnit) < MAPlane Then
                If CurMA = 0 Then
                    CurMA = CurMA + 2
                    MA2(CurUnit) = CurMA
                    Return 0
                Else
                    MA2(CurUnit) = 3
                    Return 1
                End If
            Else
                CurMA = CurMA + 2
                MA2(CurUnit) = CurMA
                Return 0
            End If
        Else
            Return 1
        End If


    End Function

    Private Function CheckAxisMADiag(ByVal newx As Integer, ByVal newy As Integer) As Integer
        Dim rc, surfaceNow, surfaceNew As Integer
        Dim CurMA As Integer
        Dim transport As Integer

        transport = Mount(CurUnit)
        CurMA = MA(CurUnit)
        'Check Surface is rough or river
        surfaceNow = FieldSurface(Cury, Curx)
        surfaceNew = FieldSurface(newy, newx)
        If ((surfaceNow > 0) Or (surfaceNew > 0)) And CurMA < MAMax(CurUnit) And CurUnitSort <> "Plane" Then
            If transport = 0 Then
                CurMA = MAMax(CurUnit) '- 1
                MA(CurUnit) = CurMA
            Else
                CurMA = MATransport '2
                MA(CurUnit) = CurMA
            End If

            Return 0
        End If

        If CurMA < MAMax(CurUnit) Then
            If MAMax(CurUnit) < MAPlane Then
                If CurMA = 0 Then
                    CurMA = CurMA + 2
                    MA(CurUnit) = CurMA
                    Return 0
                Else
                    MA(CurUnit) = 3
                    Return 1
                End If
            Else
                CurMA = CurMA + 2
                MA(CurUnit) = CurMA
                Return 0
            End If
        Else
            Return 1
        End If


    End Function

    Private Function CheckAlliesMA(ByVal newx As Integer, ByVal newy As Integer) As Integer
        Dim rc, surfaceNow, surfaceNew As Integer
        Dim CurMA As Integer
        Dim transport As Integer

        transport = Mount2(CurUnit)
        CurMA = MA2(CurUnit)
        'Check Surface is rough or river
        surfaceNow = FieldSurface(Cury, Curx)
        surfaceNew = FieldSurface(newy, newx)

        If ((surfaceNow > 0) Or (surfaceNew > 0)) And CurMA < MAMax2(CurUnit) And CurUnitSort <> "Plane" Then
            If transport = 0 Then
                CurMA = MAMax2(CurUnit) '- 1
                MA2(CurUnit) = CurMA
            Else
                CurMA = MATransport '2
                MA2(CurUnit) = CurMA
            End If
            Return 0
        End If

        If transport = 0 Then
            If CurMA < MAMax2(CurUnit) Then
                CurMA = CurMA + 1
                MA2(CurUnit) = CurMA
                Return 0
            Else
                Return 1
            End If

        Else
            If CurMA < MATransport Then
                CurMA = CurMA + 1
                MA2(CurUnit) = CurMA
                Return 0
            Else
                Return 1
            End If

        End If

    End Function

    Private Function CheckAxisMA(ByVal newx As Integer, ByVal newy As Integer) As Integer
        Dim rc, surfaceNow, surfaceNew As Integer
        Dim CurMA As Integer
        Dim transport As Integer

        transport = Mount(CurUnit)
        CurMA = MA(CurUnit)
        'Check Surface is rough or river
        surfaceNow = FieldSurface(Cury, Curx)
        surfaceNew = FieldSurface(newy, newx)

        If ((surfaceNow > 0) Or (surfaceNew > 0)) And CurMA < MAMax(CurUnit) And CurUnitSort <> "Plane" Then
            If transport = 0 Then
                CurMA = MAMax(CurUnit) '- 1
                MA(CurUnit) = CurMA
            Else
                CurMA = MATransport '2
                MA(CurUnit) = CurMA
            End If
            Return 0
        End If

        If transport = 0 Then
            If CurMA < MAMax(CurUnit) Then
                CurMA = CurMA + 1
                MA(CurUnit) = CurMA
                Return 0
            Else
                Return 1
            End If
        Else
            If CurMA < MATransport Then
                CurMA = CurMA + 1
                MA(CurUnit) = CurMA
                Return 0
            Else
                Return 1
            End If

        End If


    End Function

    Private Sub MoveRight(ByVal direction As String)

        Dim posx, posy As Integer
        Dim CurMA As Integer
        Dim unittype, unitnr2, unittype2 As Integer

        If Curx = MaxRasterX Then
            Return
        End If

        If Fuel(CurUnit) = 1 Then
            unittype = Units(CurUnit)
            If (unittype = TypeFighter Or unittype = TypeBomber) Then
                If Me.CheckAirportReached(1, Curx, Cury, CurUnit) = 0 Then 'airplane crashed
                    Return
                End If

            End If
        End If

        If UnitsField(Curx + 1, Cury) > 0 Or UnitsField2(Curx + 1, Cury) > 0 Or UnitsFieldAir(Curx + 1, Cury) > 0 Or UnitsFieldAir2(Curx + 1, Cury) > 0 Then 'Place occupied

            unittype = Units(CurUnit)
            If unittype <> TypeFighter And unittype <> TypeBomber Then
                'ground unit is moved
                unitnr2 = UnitsFieldAir(Curx + 1, Cury)
                If unitnr2 > 0 Then
                    unittype2 = Units2(unitnr2)

                Else
                    Return
                End If

            Else 'Plane
                Dim EnemyNr, EnemyType As Integer
                EnemyNr = UnitsFieldAir2(Curx + 1, Cury)
                EnemyType = Units2(EnemyNr)

                If EnemyType = TypeFighter Or EnemyType = TypeBomber Then 'Eenemy Plane
                    Return
                End If
            End If

        End If

        PRECT.Visible = False

        If Me.CheckAxisMA(Curx + 1, Cury) = 1 Then
            Return
        End If

        If CurUnitSort = "Ground" Then
            UnitsField(Curx + 1, Cury) = UnitsField(Curx, Cury)
            UnitsField(Curx, Cury) = 0
        Else
            UnitsFieldAir(Curx + 1, Cury) = UnitsFieldAir(Curx, Cury)
            UnitsFieldAir(Curx, Cury) = 0

        End If
        LastPositionx = Curx
        LastPositiony = Cury

        Curx = Curx + 1
        Cury = Cury

        LastPosx = Curx
        LastPosy = Cury
        Moved = True

        UnitsX(CurUnit) = Curx
        UnitsY(CurUnit) = Cury
        Entrenched(CurUnit) = 0
        If Fuel(CurUnit) > 0 Then
            Fuel(CurUnit) = Fuel(CurUnit) - 1
        End If
        Me.ShowParameters()

        MyUnitsAxisDS.Tables(0).Rows(CurUnit - 1).Item(2) = Curx
        MyUnitsAxisDS.Tables(0).Rows(CurUnit - 1).Item(3) = Cury

        Me.CheckCountries(1, Curx, Cury)

        Me.RemoveUnitNew(Curx - 1, Cury)

        Me.ShowUnitAxis(Curx - 1, Cury)
        Me.ShowUnitAxis(Curx, Cury)
        Me.ShowCountries()
        Me.Refresh()

        posx = HexesPosx + ((Curx - 1) * RasterWidth) + DeltaHexesX
        posy = HexesPosy - ((Cury - 1) * 2 * RasterHeight2)
        PRECT.Location = New Point(posx + Panel1.AutoScrollPosition.X, posy + Panel1.AutoScrollPosition.Y)
        PRECT.Visible = True


    End Sub

    Private Sub BRIGHT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BRIGHT.Click
        Dim str1 As String

        ' y,x  1,6  1,7

        Me.MoveArmy("Right")

        str1 = FieldSurface(Cury, Curx).ToString()
        TextBox8.Text = GetSurface(FieldSurface(Cury, Curx))



    End Sub

    Private Function CheckAirportReached(ByVal countrynr As Integer, ByVal lx As Integer, ByVal ly As Integer, ByVal cu As Integer) As Integer
        Dim found As Boolean = False
        Dim owner, px, py, ptype, rc As Integer

        For i = 0 To MyCountriesDS.Tables(0).Rows.Count - 1
            owner = MyCountriesDS.Tables(0).Rows(i).Item(0)
            px = MyCountriesDS.Tables(0).Rows(i).Item(2)
            py = MyCountriesDS.Tables(0).Rows(i).Item(3)
            ptype = MyCountriesDS.Tables(0).Rows(i).Item(4)

            If countrynr = owner And ptype = 2 Then 'is this friendly airport ?
                If px = lx And py = ly Then
                    found = True
                    Return 1

                End If

            End If
        Next

        If countrynr = 1 Then
            UnitsFieldAir(lx, ly) = 0
            Health(cu) = 0
            Me.RemoveUnitNew(lx, ly)

        Else
            UnitsFieldAir2(lx, ly) = 0
            Health2(cu) = 0
            Me.RemoveUnitNew(lx, ly)
        End If

        Return 0


    End Function

    Private Function CheckAirport(ByVal countrynr As Integer, ByVal lx As Integer, ByVal ly As Integer) As Integer
        Dim found As Boolean = False
        Dim owner, px, py, ptype, rc As Integer

        For i = 0 To MyCountriesDS.Tables(0).Rows.Count - 1
            Owner = MyCountriesDS.Tables(0).Rows(i).Item(0)
            px = MyCountriesDS.Tables(0).Rows(i).Item(2)
            py = MyCountriesDS.Tables(0).Rows(i).Item(3)
            ptype = MyCountriesDS.Tables(0).Rows(i).Item(4)

            If countrynr = Owner And ptype = 2 Then 'is this friendly airport ?
                If px = lx And py = ly Then
                    found = True
                    Return 1
                End If
            End If
        Next

        Return 0


    End Function

    Private Sub CheckCountries(ByVal countrynr As Integer, ByVal posx As Integer, ByVal posy As Integer)
        Dim owner, px, py, unit, unittype As Integer
        Dim found As Boolean = False

        For i = 0 To MyCountriesDS.Tables(0).Rows.Count - 1


            owner = MyCountriesDS.Tables(0).Rows(i).Item(0)
            px = MyCountriesDS.Tables(0).Rows(i).Item(2)
            py = MyCountriesDS.Tables(0).Rows(i).Item(3)
            If px = posx And py = posy Then
                If countrynr <> owner And CurUnitSort <> "Plane" Then
                    MyCountriesDS.Tables(0).Rows(i).Item(0) = countrynr
                    Me.ShowFlag(countrynr, px, py)

                End If

            End If


        Next

    End Sub


    Private Sub MoveDownRight2(ByVal direction As String)
        Dim posx, posy As Integer
        Dim CurMA As Integer
        Dim unittype, unitnr2, unittype2 As Integer

        If Curx = MaxRasterX Or Cury = 1 Then
            Return
        End If
        If Fuel2(CurUnit) = 1 Then
            unittype = Units2(CurUnit)
            If (unittype = TypeFighter Or unittype = TypeBomber) Then
                If Me.CheckAirportReached(2, Curx, Cury, CurUnit) = 0 Then 'airplane crashed
                    Return
                End If

            End If
        End If

        PRECT.Visible = False

        If Me.CheckAlliesMADiag(Curx + 1, Cury - 1) = 1 Then
            Return
        End If

        If UnitsField(Curx + 1, Cury - 1) > 0 Or UnitsField2(Curx + 1, Cury - 1) > 0 Or UnitsFieldAir(Curx + 1, Cury - 1) > 0 Or UnitsFieldAir2(Curx + 1, Cury - 1) > 0 Then 'Place occupied

            unittype = Units2(CurUnit)
            If unittype <> TypeFighter And unittype <> TypeBomber Then
                'ground unit is moved
                unitnr2 = UnitsFieldAir2(Curx + 1, Cury - 1)
                If unitnr2 > 0 Then
                    unittype2 = Units2(unitnr2)

                Else
                    Return
                End If

            Else 'Plane
                Dim EnemyNr, EnemyType As Integer
                EnemyNr = UnitsFieldAir(Curx + 1, Cury - 1)
                EnemyType = Units(EnemyNr)

                If EnemyType = TypeFighter Or EnemyType = TypeBomber Then 'Eenemy Plane
                    Return
                End If
            End If

        End If

        If CurUnitSort = "Ground" Then
            UnitsField2(Curx + 1, Cury - 1) = UnitsField2(Curx, Cury)
            UnitsField2(Curx, Cury) = 0
        Else
            UnitsFieldAir2(Curx + 1, Cury - 1) = UnitsFieldAir2(Curx, Cury)
            UnitsFieldAir2(Curx, Cury) = 0
        End If

        LastPositionx = Curx
        LastPositiony = Cury
        Curx = Curx + 1
        Cury = Cury - 1
        LastPosx = Curx
        LastPosy = Cury
        Moved = True

        UnitsX2(CurUnit) = Curx
        UnitsY2(CurUnit) = Cury
        Entrenched2(CurUnit) = 0
        If Fuel2(CurUnit) > 0 Then
            Fuel2(CurUnit) = Fuel2(CurUnit) - 1
        End If
        Me.ShowParameters()

        MyUnitsAlliesDS.Tables(0).Rows(CurUnit - 1).Item(2) = Curx
        MyUnitsAlliesDS.Tables(0).Rows(CurUnit - 1).Item(3) = Cury
        Me.CheckCountries(2, Curx, Cury)

        Me.RemoveUnitNew(Curx - 1, Cury + 1)

        Me.ShowUnitAllies(Curx - 1, Cury + 1)
        Me.ShowUnitAllies(Curx, Cury)
        Me.ShowCountries()

        posx = HexesPosx + ((Curx - 1) * RasterWidth) + DeltaHexesX2
        posy = HexesPosy - ((Cury - 1) * 2 * RasterHeight2)
        PRECT.Location = New Point(posx + Panel1.AutoScrollPosition.X, posy + Panel1.AutoScrollPosition.Y)
        PRECT.Visible = True


    End Sub

    Private Sub MoveDownRight(ByVal direction As String)
        Dim posx, posy As Integer
        Dim CurMA As Integer
        Dim unittype, unitnr2, unittype2 As Integer

        If Curx = MaxRasterX Or Cury = 1 Then
            Return
        End If
        If Fuel(CurUnit) = 1 Then
            unittype = Units(CurUnit)
            If (unittype = TypeFighter Or unittype = TypeBomber) Then
                If Me.CheckAirportReached(1, Curx, Cury, CurUnit) = 0 Then 'airplane crashed
                    Return
                End If

            End If
        End If

        PRECT.Visible = False

        If Me.CheckAxisMADiag(Curx + 1, Cury - 1) = 1 Then
            Return
        End If

        If UnitsField(Curx + 1, Cury - 1) > 0 Or UnitsField2(Curx + 1, Cury - 1) > 0 Or UnitsFieldAir(Curx + 1, Cury - 1) > 0 Or UnitsFieldAir2(Curx + 1, Cury - 1) > 0 Then 'Place occupied

            unittype = Units(CurUnit)
            If unittype <> TypeFighter And unittype <> TypeBomber Then
                'ground unit is moved
                unitnr2 = UnitsFieldAir(Curx + 1, Cury - 1)
                If unitnr2 > 0 Then
                    unittype2 = Units2(unitnr2)

                Else
                    Return
                End If

            Else 'Plane
                Dim EnemyNr, EnemyType As Integer
                EnemyNr = UnitsFieldAir2(Curx + 1, Cury - 1)
                EnemyType = Units2(EnemyNr)

                If EnemyType = TypeFighter Or EnemyType = TypeBomber Then 'Eenemy Plane
                    Return
                End If
            End If

        End If

        'Me.RemoveUnit(CurUnit, Curx, Cury)
        'Me.RemoveUnitNew(Curx, Cury)

        If CurUnitSort = "Ground" Then
            UnitsField(Curx + 1, Cury - 1) = UnitsField(Curx, Cury)
            UnitsField(Curx, Cury) = 0
        Else
            UnitsFieldAir(Curx + 1, Cury - 1) = UnitsFieldAir(Curx, Cury)
            UnitsFieldAir(Curx, Cury) = 0
        End If

        LastPositionx = Curx
        LastPositiony = Cury
        Curx = Curx + 1
        Cury = Cury - 1
        LastPosx = Curx
        LastPosy = Cury
        Moved = True


        UnitsX(CurUnit) = Curx
        UnitsY(CurUnit) = Cury
        Entrenched(CurUnit) = 0
        If Fuel(CurUnit) > 0 Then
            Fuel(CurUnit) = Fuel(CurUnit) - 1
        End If
        Me.ShowParameters()

        MyUnitsAxisDS.Tables(0).Rows(CurUnit - 1).Item(2) = Curx
        MyUnitsAxisDS.Tables(0).Rows(CurUnit - 1).Item(3) = Cury
        Me.CheckCountries(1, Curx, Cury)

        Me.RemoveUnitNew(Curx - 1, Cury + 1)

        Me.ShowUnitAxis(Curx - 1, Cury + 1)
        Me.ShowUnitAxis(Curx, Cury)
        Me.ShowCountries()

        posx = HexesPosx + ((Curx - 1) * RasterWidth) + DeltaHexesX
        posy = HexesPosy - ((Cury - 1) * 2 * RasterHeight2)
        PRECT.Location = New Point(posx + Panel1.AutoScrollPosition.X, posy + Panel1.AutoScrollPosition.Y)
        PRECT.Visible = True


    End Sub

    Private Sub BDOWNR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BDOWNR.Click

        Me.MoveArmy("DownRight")
        Dim str1 As String

        str1 = FieldSurface(Cury, Curx).ToString()
        TextBox8.Text = GetSurface(FieldSurface(Cury, Curx))

    End Sub

    Private Sub MoveUpLeft2(ByVal direction As String)
        Dim posx, posy As Integer
        Dim CurMA As Integer
        Dim unittype, unitnr2, unittype2 As Integer

        If Curx = 1 Or Cury = MaxRasterY Then
            Return
        End If
        If Fuel2(CurUnit) = 1 Then
            unittype = Units2(CurUnit)
            If (unittype = TypeFighter Or unittype = TypeBomber) Then
                If Me.CheckAirportReached(2, Curx, Cury, CurUnit) = 0 Then 'airplane crashed
                    Return
                End If

            End If
        End If

        PRECT.Visible = False

        If Me.CheckAlliesMADiag(Curx - 1, Cury + 1) = 1 Then
            Return
        End If

        If UnitsField(Curx - 1, Cury + 1) > 0 Or UnitsField2(Curx - 1, Cury + 1) > 0 Or UnitsFieldAir(Curx - 1, Cury + 1) > 0 Or UnitsFieldAir2(Curx - 1, Cury + 1) > 0 Then 'Place occupied

            unittype = Units2(CurUnit)
            If unittype <> TypeFighter And unittype <> TypeBomber Then
                'ground unit is moved
                unitnr2 = UnitsFieldAir2(Curx - 1, Cury + 1)
                If unitnr2 > 0 Then
                    unittype2 = Units2(unitnr2)

                Else
                    Return
                End If

            Else 'Plane
                Dim EnemyNr, EnemyType As Integer
                EnemyNr = UnitsFieldAir(Curx - 1, Cury + 1)
                EnemyType = Units(EnemyNr)

                If EnemyType = TypeFighter Or EnemyType = TypeBomber Then 'Eenemy Plane
                    Return
                End If
            End If

        End If

        'Me.RemoveUnit2(CurUnit, Curx, Cury)
        'Me.RemoveUnitNew(Curx, Cury)

        If CurUnitSort = "Ground" Then
            UnitsField2(Curx - 1, Cury + 1) = UnitsField2(Curx, Cury)
            UnitsField2(Curx, Cury) = 0
        Else
            UnitsFieldAir2(Curx - 1, Cury + 1) = UnitsFieldAir2(Curx, Cury)
            UnitsFieldAir2(Curx, Cury) = 0
        End If

        LastPositionx = Curx
        LastPositiony = Cury
        Curx = Curx - 1
        Cury = Cury + 1
        LastPosx = Curx
        LastPosy = Cury
        Moved = True

        UnitsX2(CurUnit) = Curx
        UnitsY2(CurUnit) = Cury
        Entrenched2(CurUnit) = 0
        If Fuel2(CurUnit) > 0 Then
            Fuel2(CurUnit) = Fuel2(CurUnit) - 1
        End If
        Me.ShowParameters()

        MyUnitsAlliesDS.Tables(0).Rows(CurUnit - 1).Item(2) = Curx
        MyUnitsAlliesDS.Tables(0).Rows(CurUnit - 1).Item(3) = Cury
        Me.CheckCountries(2, Curx, Cury)

        Me.RemoveUnitNew(Curx + 1, Cury - 1)

        Me.ShowUnitAllies(Curx + 1, Cury - 1)
        Me.ShowUnitAllies(Curx, Cury)
        Me.ShowCountries()

        posx = HexesPosx + ((Curx - 1) * RasterWidth) + DeltaHexesX2
        posy = HexesPosy - ((Cury - 1) * 2 * RasterHeight2)
        PRECT.Location = New Point(posx + Panel1.AutoScrollPosition.X, posy + Panel1.AutoScrollPosition.Y)
        PRECT.Visible = True

    End Sub

    Private Sub MoveUpLeft(ByVal direction As String)
        Dim posx, posy As Integer
        Dim CurMA As Integer
        Dim unittype, unitnr2, unittype2 As Integer

        If Curx = 1 Or Cury = MaxRasterY Then
            Return
        End If
        If Fuel(CurUnit) = 1 Then
            unittype = Units(CurUnit)
            If (unittype = TypeFighter Or unittype = TypeBomber) Then
                If Me.CheckAirportReached(1, Curx, Cury, CurUnit) = 0 Then 'airplane crashed
                    Return
                End If

            End If
        End If


        PRECT.Visible = False

        If Me.CheckAxisMADiag(Curx - 1, Cury + 1) = 1 Then
            Return
        End If

        If UnitsField(Curx - 1, Cury + 1) > 0 Or UnitsField2(Curx - 1, Cury + 1) > 0 Or UnitsFieldAir(Curx - 1, Cury + 1) > 0 Or UnitsFieldAir2(Curx - 1, Cury + 1) > 0 Then 'Place occupied

            unittype = Units(CurUnit)
            If unittype <> TypeFighter And unittype <> TypeBomber Then
                'ground unit is moved
                unitnr2 = UnitsFieldAir(Curx - 1, Cury + 1)
                If unitnr2 > 0 Then
                    unittype2 = Units2(unitnr2)

                Else
                    Return
                End If

            Else 'Plane
                Dim EnemyNr, EnemyType As Integer
                EnemyNr = UnitsFieldAir2(Curx - 1, Cury + 1)
                EnemyType = Units2(EnemyNr)

                If EnemyType = TypeFighter Or EnemyType = TypeBomber Then 'Eenemy Plane
                    Return
                End If
            End If


        End If

        'Me.RemoveUnit(CurUnit, Curx, Cury)
        'Me.RemoveUnitNew(Curx, Cury)

        If CurUnitSort = "Ground" Then
            UnitsField(Curx - 1, Cury + 1) = UnitsField(Curx, Cury)
            UnitsField(Curx, Cury) = 0
        Else
            UnitsFieldAir(Curx - 1, Cury + 1) = UnitsFieldAir(Curx, Cury)
            UnitsFieldAir(Curx, Cury) = 0
        End If

        LastPositionx = Curx
        LastPositiony = Cury
        Curx = Curx - 1
        Cury = Cury + 1
        LastPosx = Curx
        LastPosy = Cury
        Moved = True


        UnitsX(CurUnit) = Curx
        UnitsY(CurUnit) = Cury
        Entrenched(CurUnit) = 0
        If Fuel(CurUnit) > 0 Then
            Fuel(CurUnit) = Fuel(CurUnit) - 1
        End If
        Me.ShowParameters()

        MyUnitsAxisDS.Tables(0).Rows(CurUnit - 1).Item(2) = Curx
        MyUnitsAxisDS.Tables(0).Rows(CurUnit - 1).Item(3) = Cury
        Me.CheckCountries(1, Curx, Cury)

        Me.RemoveUnitNew(Curx + 1, Cury - 1)

        Me.ShowUnitAxis(Curx + 1, Cury - 1)
        Me.ShowUnitAxis(Curx, Cury)
        Me.ShowCountries()

        posx = HexesPosx + ((Curx - 1) * RasterWidth) + DeltaHexesX
        posy = HexesPosy - ((Cury - 1) * 2 * RasterHeight2)
        PRECT.Location = New Point(posx + Panel1.AutoScrollPosition.X, posy + Panel1.AutoScrollPosition.Y)
        PRECT.Visible = True

    End Sub

    Private Sub BUPL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUPL.Click

        Me.MoveArmy("UpLeft")
        Dim str1 As String

        str1 = FieldSurface(Cury, Curx).ToString()
        TextBox8.Text = GetSurface(FieldSurface(Cury, Curx))

    End Sub

    Private Sub MoveLeft2(ByVal direction As String)
        Dim posx, posy As Integer
        Dim CurMA As Integer
        Dim unittype, unitnr2, unittype2 As Integer

        If Curx = 1 Then
            Return
        End If
        If Fuel2(CurUnit) = 1 Then
            unittype = Units2(CurUnit)
            If (unittype = TypeFighter Or unittype = TypeBomber) Then
                If Me.CheckAirportReached(2, Curx, Cury, CurUnit) = 0 Then 'airplane crashed
                    Return
                End If

            End If
        End If

        If UnitsField(Curx - 1, Cury) > 0 Or UnitsField2(Curx - 1, Cury) > 0 Or UnitsFieldAir(Curx - 1, Cury) > 0 Or UnitsFieldAir2(Curx - 1, Cury) > 0 Then 'Place occupied

            unittype = Units2(CurUnit)
            If unittype <> TypeFighter And unittype <> TypeBomber Then
                'ground unit is moved
                unitnr2 = UnitsFieldAir2(Curx - 1, Cury)
                If unitnr2 > 0 Then
                    unittype2 = Units2(unitnr2)

                Else
                    Return
                End If

            Else 'Plane
                Dim EnemyNr, EnemyType As Integer
                EnemyNr = UnitsFieldAir(Curx - 1, Cury)
                EnemyType = Units(EnemyNr)

                If EnemyType = TypeFighter Or EnemyType = TypeBomber Then 'Eenemy Plane
                    Return
                End If
            End If

        End If

        PRECT.Visible = False

        If Me.CheckAlliesMA(Curx - 1, Cury) = 1 Then
            Return
        End If

        'Me.RemoveUnit2(CurUnit, Curx, Cury)
        'Me.RemoveUnitNew(Curx, Cury)

        If CurUnitSort = "Ground" Then
            UnitsField2(Curx - 1, Cury) = UnitsField2(Curx, Cury)
            UnitsField2(Curx, Cury) = 0
        Else
            UnitsFieldAir2(Curx - 1, Cury) = UnitsFieldAir2(Curx, Cury)
            UnitsFieldAir2(Curx, Cury) = 0
        End If

        LastPositionx = Curx
        LastPositiony = Cury
        Curx = Curx - 1
        Cury = Cury
        LastPosx = Curx
        LastPosy = Cury
        Moved = True

        UnitsX2(CurUnit) = Curx
        UnitsY2(CurUnit) = Cury
        Entrenched2(CurUnit) = 0
        If Fuel2(CurUnit) > 0 Then
            Fuel2(CurUnit) = Fuel2(CurUnit) - 1
        End If
        Me.ShowParameters()

        MyUnitsAlliesDS.Tables(0).Rows(CurUnit - 1).Item(2) = Curx
        MyUnitsAlliesDS.Tables(0).Rows(CurUnit - 1).Item(3) = Cury
        Me.CheckCountries(2, Curx, Cury)

        'Get new Image
        'Me.CopyImage2(CurUnit, Curx, Cury)
        Me.RemoveUnitNew(Curx + 1, Cury)

        Me.ShowUnitAllies(Curx + 1, Cury)
        Me.ShowUnitAllies(Curx, Cury)
        Me.ShowCountries()

        posx = HexesPosx + ((Curx - 1) * RasterWidth) + DeltaHexesX2
        posy = HexesPosy - ((Cury - 1) * 2 * RasterHeight2)
        PRECT.Location = New Point(posx + Panel1.AutoScrollPosition.X, posy + Panel1.AutoScrollPosition.Y)
        PRECT.Visible = True

    End Sub

    Private Sub MoveLeft(ByVal direction As String)

        Dim posx, posy As Integer
        Dim CurMA As Integer
        Dim unittype, unitnr2, unittype2 As Integer

        If Curx = 1 Then
            Return
        End If
        If Fuel(CurUnit) = 1 Then
            unittype = Units(CurUnit)
            If (unittype = TypeFighter Or unittype = TypeBomber) Then
                If Me.CheckAirportReached(1, Curx, Cury, CurUnit) = 0 Then 'airplane crashed
                    Return
                End If

            End If
        End If

        If UnitsField(Curx - 1, Cury) > 0 Or UnitsField2(Curx - 1, Cury) > 0 Or UnitsFieldAir(Curx - 1, Cury) > 0 Or UnitsFieldAir2(Curx - 1, Cury) > 0 Then 'Place occupied

            unittype = Units(CurUnit)
            If unittype <> TypeFighter And unittype <> TypeBomber Then
                'ground unit is moved
                unitnr2 = UnitsFieldAir(Curx - 1, Cury)
                If unitnr2 > 0 Then
                    unittype2 = Units2(unitnr2)

                Else
                    Return
                End If

            Else 'Plane
                Dim EnemyNr, EnemyType As Integer
                EnemyNr = UnitsFieldAir2(Curx - 1, Cury)
                EnemyType = Units2(EnemyNr)

                If EnemyType = TypeFighter Or EnemyType = TypeBomber Then 'Eenemy Plane
                    Return
                End If
            End If

        End If

        PRECT.Visible = False

        If Me.CheckAxisMA(Curx - 1, Cury) = 1 Then
            Return
        End If

        'Me.RemoveUnit(CurUnit, Curx, Cury)
        'Me.RemoveUnitNew(Curx, Cury)

        If CurUnitSort = "Ground" Then
            UnitsField(Curx - 1, Cury) = UnitsField(Curx, Cury)
            UnitsField(Curx, Cury) = 0
        Else
            UnitsFieldAir(Curx - 1, Cury) = UnitsFieldAir(Curx, Cury)
            UnitsFieldAir(Curx, Cury) = 0
        End If

        LastPositionx = Curx
        LastPositiony = Cury
        Curx = Curx - 1
        Cury = Cury
        LastPosx = Curx
        LastPosy = Cury
        Moved = True

        UnitsX(CurUnit) = Curx
        UnitsY(CurUnit) = Cury
        Entrenched(CurUnit) = 0
        If Fuel(CurUnit) > 0 Then
            Fuel(CurUnit) = Fuel(CurUnit) - 1
        End If
        Me.ShowParameters()

        MyUnitsAxisDS.Tables(0).Rows(CurUnit - 1).Item(2) = Curx
        MyUnitsAxisDS.Tables(0).Rows(CurUnit - 1).Item(3) = Cury
        Me.CheckCountries(1, Curx, Cury)

        Me.RemoveUnitNew(Curx + 1, Cury)

        Me.ShowUnitAxis(Curx + 1, Cury)
        Me.ShowUnitAxis(Curx, Cury)
        Me.ShowCountries()

        posx = HexesPosx + ((Curx - 1) * RasterWidth) + DeltaHexesX
        posy = HexesPosy - ((Cury - 1) * 2 * RasterHeight2)
        PRECT.Location = New Point(posx + Panel1.AutoScrollPosition.X, posy + Panel1.AutoScrollPosition.Y)
        PRECT.Visible = True

    End Sub

    Private Sub BLEFT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BLEFT.Click

        Me.MoveArmy("Left")
        Dim str1 As String

        str1 = FieldSurface(Cury, Curx).ToString()
        TextBox8.Text = GetSurface(FieldSurface(Cury, Curx))

    End Sub

    Private Sub MoveDownLeft2(ByVal direction As String)
        Dim posx, posy As Integer
        Dim CurMA As Integer
        Dim unittype, unitnr2, unittype2 As Integer

        If Curx = 1 Or Cury = 1 Then
            Return
        End If
        If Fuel2(CurUnit) = 1 Then
            unittype = Units2(CurUnit)
            If (unittype = TypeFighter Or unittype = TypeBomber) Then
                If Me.CheckAirportReached(2, Curx, Cury, CurUnit) = 0 Then 'airplane crashed
                    Return
                End If

            End If
        End If

        PRECT.Visible = False

        If Me.CheckAlliesMADiag(Curx - 1, Cury - 1) = 1 Then
            Return
        End If

        If UnitsField(Curx - 1, Cury - 1) > 0 Or UnitsField2(Curx - 1, Cury - 1) Or UnitsFieldAir(Curx - 1, Cury - 1) > 0 Or UnitsFieldAir2(Curx - 1, Cury - 1) > 0 Then 'Place occupied

            unittype = Units2(CurUnit)
            If unittype <> TypeFighter And unittype <> TypeBomber Then
                'ground unit is moved
                unitnr2 = UnitsFieldAir2(Curx - 1, Cury - 1)
                If unitnr2 > 0 Then
                    unittype2 = Units2(unitnr2)

                Else
                    Return
                End If

            Else 'Plane
                Dim EnemyNr, EnemyType As Integer
                EnemyNr = UnitsFieldAir(Curx - 1, Cury - 1)
                EnemyType = Units(EnemyNr)

                If EnemyType = TypeFighter Or EnemyType = TypeBomber Then 'Eenemy Plane
                    Return
                End If
            End If
        End If


        If CurUnitSort = "Ground" Then
            UnitsField2(Curx - 1, Cury - 1) = UnitsField2(Curx, Cury)
            UnitsField2(Curx, Cury) = 0
        Else
            UnitsFieldAir2(Curx - 1, Cury - 1) = UnitsFieldAir2(Curx, Cury)
            UnitsFieldAir2(Curx, Cury) = 0
        End If

        LastPositionx = Curx
        LastPositiony = Cury
        Curx = Curx - 1
        Cury = Cury - 1
        LastPosx = Curx
        LastPosy = Cury
        Moved = True

        UnitsX2(CurUnit) = Curx
        UnitsY2(CurUnit) = Cury
        Entrenched2(CurUnit) = 0
        If Fuel2(CurUnit) > 0 Then
            Fuel2(CurUnit) = Fuel2(CurUnit) - 1
        End If
        Me.ShowParameters()

        MyUnitsAlliesDS.Tables(0).Rows(CurUnit - 1).Item(2) = Curx
        MyUnitsAlliesDS.Tables(0).Rows(CurUnit - 1).Item(3) = Cury
        Me.CheckCountries(2, Curx, Cury)

        Me.RemoveUnitNew(Curx + 1, Cury + 1)

        Me.ShowUnitAllies(Curx + 1, Cury + 1)
        Me.ShowUnitAllies(Curx, Cury)
        Me.ShowCountries()

        posx = HexesPosx + ((Curx - 1) * RasterWidth) + DeltaHexesX2
        posy = HexesPosy - ((Cury - 1) * 2 * RasterHeight2)
        PRECT.Location = New Point(posx + Panel1.AutoScrollPosition.X, posy + Panel1.AutoScrollPosition.Y)
        PRECT.Visible = True

    End Sub

    Private Sub MoveDownLeft(ByVal direction As String)
        Dim posx, posy As Integer
        Dim CurMA As Integer
        Dim unittype, unitnr2, unittype2 As Integer

        If Curx = 1 Or Cury = 1 Then
            Return
        End If
        If Fuel(CurUnit) = 1 Then
            unittype = Units(CurUnit)
            If (unittype = TypeFighter Or unittype = TypeBomber) Then
                If Me.CheckAirportReached(1, Curx, Cury, CurUnit) = 0 Then 'airplane crashed
                    Return
                End If

            End If
        End If

        PRECT.Visible = False

        If Me.CheckAxisMADiag(Curx - 1, Cury - 1) = 1 Then
            Return
        End If

        If UnitsField(Curx - 1, Cury - 1) > 0 Or UnitsField2(Curx - 1, Cury - 1) > 0 Or UnitsFieldAir(Curx - 1, Cury - 1) > 0 Or UnitsFieldAir2(Curx - 1, Cury - 1) > 0 Then 'Place occupied

            unittype = Units(CurUnit)
            If unittype <> TypeFighter And unittype <> TypeBomber Then
                'ground unit is moved
                unitnr2 = UnitsFieldAir(Curx - 1, Cury - 1)
                If unitnr2 > 0 Then
                    unittype2 = Units2(unitnr2)

                Else
                    Return
                End If

            Else 'Plane
                Dim EnemyNr, EnemyType As Integer
                EnemyNr = UnitsFieldAir2(Curx - 1, Cury - 1)
                EnemyType = Units2(EnemyNr)

                If EnemyType = TypeFighter Or EnemyType = TypeBomber Then 'Eenemy Plane
                    Return
                End If
            End If

        End If

        'Me.RemoveUnit(CurUnit, Curx, Cury)
        'Me.RemoveUnitNew(Curx, Cury)

        If CurUnitSort = "Ground" Then
            UnitsField(Curx - 1, Cury - 1) = UnitsField(Curx, Cury)
            UnitsField(Curx, Cury) = 0
        Else
            UnitsFieldAir(Curx - 1, Cury - 1) = UnitsFieldAir(Curx, Cury)
            UnitsFieldAir(Curx, Cury) = 0
        End If

        LastPositionx = Curx
        LastPositiony = Cury
        Curx = Curx - 1
        Cury = Cury - 1
        LastPosx = Curx
        LastPosy = Cury
        Moved = True

        UnitsX(CurUnit) = Curx
        UnitsY(CurUnit) = Cury
        Entrenched(CurUnit) = 0
        If Fuel(CurUnit) > 0 Then
            Fuel(CurUnit) = Fuel(CurUnit) - 1
        End If
        Me.ShowParameters()

        MyUnitsAxisDS.Tables(0).Rows(CurUnit - 1).Item(2) = Curx
        MyUnitsAxisDS.Tables(0).Rows(CurUnit - 1).Item(3) = Cury
        Me.CheckCountries(1, Curx, Cury)

        Me.RemoveUnitNew(Curx + 1, Cury + 1)

        Me.ShowUnitAxis(Curx + 1, Cury + 1)
        Me.ShowUnitAxis(Curx, Cury)
        Me.ShowCountries()

        posx = HexesPosx + ((Curx - 1) * RasterWidth) + DeltaHexesX
        posy = HexesPosy - ((Cury - 1) * 2 * RasterHeight2)
        PRECT.Location = New Point(posx + Panel1.AutoScrollPosition.X, posy + Panel1.AutoScrollPosition.Y)
        PRECT.Visible = True


    End Sub

    Private Sub BDOWNL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BDOWNL.Click

        Me.MoveArmy("DownLeft")
        Dim str1 As String

        str1 = FieldSurface(Cury, Curx).ToString()
        TextBox8.Text = GetSurface(FieldSurface(Cury, Curx))

    End Sub

    Private Sub PB1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PB1.Click

    End Sub

    Private Function CheckVictory() As Boolean

        Dim owner, px, py, victory As Integer
        Dim GotVictory As Boolean = True

        For i = 0 To MyCountriesDS.Tables(0).Rows.Count - 1

            owner = MyCountriesDS.Tables(0).Rows(i).Item(0)
            px = MyCountriesDS.Tables(0).Rows(i).Item(2)
            py = MyCountriesDS.Tables(0).Rows(i).Item(3)
            victory = MyCountriesDS.Tables(0).Rows(i).Item(6)

            If victory = 1 And owner = 2 Then 'this country is not won
                GotVictory = False
            End If

        Next

        Return GotVictory

    End Function

    Private Sub UpdateEntrenchment(ByVal cname As String)
        Dim unittype, posx, posy, entrench, MyMA, mysurface As Integer

        If GameMode = 0 Then 'no entrenchment
            Return
        End If

        If cname = "Axis" Then
            For i = 0 To 99
                unittype = Units(i)
                If unittype > 0 Then
                    posx = UnitsX(i)
                    posy = UnitsY(i)
                    'check surface = 1 !
                    mysurface = FieldSurface(posy, posx)
                    MyMA = MA(i)
                    If MyMA > 0 Or mysurface = 1 Then 'moved or placed on river
                        Entrenched(i) = 0
                    Else
                        entrench = Entrenched(i)
                        If unittype = TypeInfantery Then
                            entrench = entrench + 1
                            If entrench > EntrenchInfanterie Then
                                entrench = EntrenchInfanterie
                            End If
                        ElseIf unittype = TypeTank Then
                            entrench = entrench + 1
                            If entrench > EntrenchTank Then
                                entrench = EntrenchTank
                            End If
                        ElseIf unittype = TypeArtillery Then
                            entrench = entrench + 1
                            If entrench > EntrenchArtillery Then
                                entrench = EntrenchArtillery
                            End If
                        ElseIf unittype = TypeAntitank Then
                            entrench = entrench + 1
                            If entrench > EntrenchAntitank Then
                                entrench = EntrenchAntitank
                            End If
                        ElseIf unittype = TypeFighter Or unittype = TypeBomber Then
                            entrench = 0

                        End If
                        Entrenched(i) = entrench

                    End If
                End If
            Next
        Else 'Allies
            For i = 0 To 99
                unittype = Units2(i)
                If unittype > 0 Then
                    posx = UnitsX2(i)
                    posy = UnitsY2(i)
                    'check surface = 1 !
                    mysurface = FieldSurface(posy, posx)
                    MyMA = MA2(i)
                    If MyMA > 0 Or mysurface = 1 Then 'moved or placed on river
                        Entrenched2(i) = 0
                    Else
                        entrench = Entrenched2(i)
                        If unittype = TypeInfantery Then
                            entrench = entrench + 1
                            If entrench > EntrenchInfanterie Then
                                entrench = EntrenchInfanterie
                            End If
                        ElseIf unittype = TypeTank Then
                            entrench = entrench + 1
                            If entrench > EntrenchTank Then
                                entrench = EntrenchTank
                            End If
                        ElseIf unittype = TypeArtillery Then
                            entrench = entrench + 1
                            If entrench > EntrenchArtillery Then
                                entrench = EntrenchArtillery
                            End If
                        ElseIf unittype = TypeAntitank Then
                            entrench = entrench + 1
                            If entrench > EntrenchAntitank Then
                                entrench = EntrenchAntitank
                            End If
                        ElseIf unittype = TypeFighter Or unittype = TypeBomber Then
                            entrench = 0

                        End If
                        Entrenched2(i) = entrench

                    End If
                End If
            Next

        End If

    End Sub

    Private Sub MoveUnit(ByVal px1 As Integer, ByVal py1 As Integer, ByVal px2 As Integer, ByVal py2 As Integer, ByVal unitnum As Integer)

        If Units2(unitnum) = TypeFighter Or Units2(unitnum) = TypeBomber Then
            UnitsFieldAir2(px1, py1) = 0
            UnitsFieldAir2(px2, py2) = unitnum
        Else
            UnitsField2(px1, py1) = 0
            UnitsField2(px2, py2) = unitnum
        End If
        UnitsX2(unitnum) = px2
        UnitsY2(unitnum) = py2
        MyUnitsAlliesDS.Tables(0).Rows(unitnum - 1).Item(2) = px2
        MyUnitsAlliesDS.Tables(0).Rows(unitnum - 1).Item(3) = py2

    End Sub

    Private Sub ShowArtilleryAllies(ByVal artnr As Integer, ByVal px As Integer, ByVal py As Integer)
        Dim bmp, artillery As Bitmap
        Dim posx, posy As Integer

        Me.RemoveUnitNew(px, py)
        bmp = PB1.Image
        If artnr = 1 Then
            artillery = Bitmap.FromFile(CurDir() + "\Units2\artillery-POL2.bmp")
        Else
            artillery = Bitmap.FromFile(CurDir() + "\Units2\artillery-POL.bmp")
        End If
        posx = HexesPosx + ((px - 1) * RasterWidth)
        posy = HexesPosy - ((py - 1) * 2 * RasterHeight2)

        bmp = Me.AddSpriteTransWhite(bmp, artillery, posx, posy)

        PB1.Image = bmp
        Me.Refresh()

    End Sub

    Private Sub ShowAnimationAxis(ByVal artnr As Integer, ByVal px As Integer, ByVal py As Integer)
        Dim theunit, utype, attacked As Integer


        theunit = UnitsField(px, py)
        If theunit = 0 Then
            theunit = UnitsFieldAir(px, py)
        End If
        utype = Units(theunit)

        If utype = TypeInfantery Then
            Me.ShowInfanteryAxis(artnr, px, py)

        ElseIf utype = TypeTank Then
            Me.ShowTankAxis(artnr, px, py)

        ElseIf utype = TypeArtillery Then
            Me.ShowArtilleryAxis(artnr, px, py)

        ElseIf utype = TypeAntitank Then
            Me.ShowAntitankAxis(artnr, px, py)

        ElseIf utype = TypeFighter Then
            Me.ShowFighterAxis(artnr, px, py)

        ElseIf utype = TypeBomber Then
            Me.ShowBomberAxis(artnr, px, py)

        End If


    End Sub


    Private Sub ShowAnimationAllies(ByVal artnr As Integer, ByVal px As Integer, ByVal py As Integer)
        Dim theunit, utype, attacked As Integer


        theunit = UnitsField2(px, py)
        If theunit = 0 Then
            theunit = UnitsFieldAir2(px, py)
        End If
        utype = Units2(theunit)

        If utype = TypeInfantery Then

            Me.ShowInfanteryAllies(artnr, px, py)

        ElseIf utype = TypeTank Then

            Me.ShowTankAllies(artnr, px, py)

        ElseIf utype = TypeArtillery Then

            Me.ShowArtilleryAllies(artnr, px, py)

        ElseIf utype = TypeAntitank Then

            Me.ShowAntitankAllies(artnr, px, py)

        ElseIf utype = TypeFighter Then

            Me.ShowFighterAllies(artnr, px, py)

        ElseIf utype = TypeBomber Then

            Me.ShowBomberAllies(artnr, px, py)

        End If



    End Sub

    Private Sub ShowTankAllies(ByVal artnr As Integer, ByVal px As Integer, ByVal py As Integer)
        Dim bmp, tank As Bitmap
        Dim posx, posy As Integer

        Me.RemoveUnitNew(px, py)
        bmp = PB1.Image
        If artnr = 1 Then
            tank = Bitmap.FromFile(CurDir() + "\Units2\Tank-POL2.bmp")
        Else
            tank = Bitmap.FromFile(CurDir() + "\Units2\Tank-POL.bmp")
        End If
        posx = HexesPosx + ((px - 1) * RasterWidth)
        posy = HexesPosy - ((py - 1) * 2 * RasterHeight2)

        bmp = Me.AddSpriteTransWhite(bmp, tank, posx, posy)

        PB1.Image = bmp
        Me.Refresh()


    End Sub

    Private Sub ShowInfanteryAllies(ByVal artnr As Integer, ByVal px As Integer, ByVal py As Integer)

        Dim bmp, infantery As Bitmap
        Dim posx, posy As Integer


        Me.RemoveUnitNew(px, py)
        bmp = PB1.Image
        If artnr = 1 Then
            infantery = Bitmap.FromFile(CurDir() + "\Units2\Infantery-POL2.bmp")
        Else
            infantery = Bitmap.FromFile(CurDir() + "\Units2\Infantery-POL.bmp")
        End If
        posx = HexesPosx + ((px - 1) * RasterWidth)
        posy = HexesPosy - ((py - 1) * 2 * RasterHeight2)

        bmp = Me.AddSpriteTransWhite(bmp, infantery, posx, posy)

        PB1.Image = bmp
        Me.Refresh()


    End Sub

    Private Sub ShowAntitankAllies(ByVal artnr As Integer, ByVal px As Integer, ByVal py As Integer)
        Dim bmp, antitank As Bitmap
        Dim posx, posy As Integer


        Me.RemoveUnitNew(px, py)
        bmp = PB1.Image
        If artnr = 1 Then
            antitank = Bitmap.FromFile(CurDir() + "\Units2\antitank-POL2.bmp")
        Else
            antitank = Bitmap.FromFile(CurDir() + "\Units2\antitank-POL.bmp")
        End If
        posx = HexesPosx + ((px - 1) * RasterWidth)
        posy = HexesPosy - ((py - 1) * 2 * RasterHeight2)

        bmp = Me.AddSpriteTransWhite(bmp, antitank, posx, posy)

        PB1.Image = bmp
        Me.Refresh()

    End Sub

    Private Sub ShowFighterAllies(ByVal artnr As Integer, ByVal px As Integer, ByVal py As Integer)

        Dim bmp, fighter As Bitmap
        Dim posx, posy As Integer


        Me.RemoveUnitNew(px, py)
        bmp = PB1.Image
        If artnr = 1 Then
            fighter = Bitmap.FromFile(CurDir() + "\Units2\fighter-POL2.bmp")
        Else
            fighter = Bitmap.FromFile(CurDir() + "\Units2\fighter-POL.bmp")
        End If
        posx = HexesPosx + ((px - 1) * RasterWidth)
        posy = HexesPosy - ((py - 1) * 2 * RasterHeight2)

        bmp = Me.AddSpriteTransWhite(bmp, fighter, posx, posy)

        PB1.Image = bmp
        Me.Refresh()


    End Sub

    Private Sub ShowBomberAllies(ByVal artnr As Integer, ByVal px As Integer, ByVal py As Integer)

        Dim bmp, bomber As Bitmap
        Dim posx, posy As Integer

        Me.RemoveUnitNew(px, py)
        bmp = PB1.Image
        If artnr = 1 Then
            bomber = Bitmap.FromFile(CurDir() + "\Units2\bomber-POL2.bmp")
        ElseIf artnr = 2 Then
            bomber = Bitmap.FromFile(CurDir() + "\Units2\bomber-POL.bmp")
        ElseIf artnr = 3 Then
            bomber = Bitmap.FromFile(CurDir() + "\Units\bomber-POL3.bmp")
        End If
        posx = HexesPosx + ((px - 1) * RasterWidth)
        posy = HexesPosy - ((py - 1) * 2 * RasterHeight2)

        bmp = Me.AddSpriteTransWhite(bmp, bomber, posx, posy)

        PB1.Image = bmp
        Me.Refresh()

    End Sub

    Private Sub ShowBomberAxis(ByVal artnr As Integer, ByVal px As Integer, ByVal py As Integer)

        Dim bmp, bomber As Bitmap
        Dim posx, posy As Integer

        Me.RemoveUnitNew(px, py)
        bmp = PB1.Image
        If artnr = 1 Then
            bomber = Bitmap.FromFile(CurDir() + "\Units\bomber-002.bmp")
        ElseIf artnr = 2 Then
            bomber = Bitmap.FromFile(CurDir() + "\Units\bomber-001.bmp")
        ElseIf artnr = 3 Then
            bomber = Bitmap.FromFile(CurDir() + "\Units\bomber-003.bmp")
        End If
        posx = HexesPosx + ((px - 1) * RasterWidth)
        posy = HexesPosy - ((py - 1) * 2 * RasterHeight2)

        bmp = Me.AddSpriteTransWhite(bmp, bomber, posx, posy)

        PB1.Image = bmp
        Me.Refresh()


    End Sub

    Private Sub ShowFighterAxis(ByVal artnr As Integer, ByVal px As Integer, ByVal py As Integer)
        Dim bmp, fighter As Bitmap
        Dim posx, posy As Integer

        Me.RemoveUnitNew(px, py)
        bmp = PB1.Image
        If artnr = 1 Then
            fighter = Bitmap.FromFile(CurDir() + "\Units\fighter-002.bmp")
        Else
            fighter = Bitmap.FromFile(CurDir() + "\Units\fighter-001.bmp")
        End If
        posx = HexesPosx + ((px - 1) * RasterWidth)
        posy = HexesPosy - ((py - 1) * 2 * RasterHeight2)

        bmp = Me.AddSpriteTransWhite(bmp, fighter, posx, posy)

        PB1.Image = bmp
        Me.Refresh()


    End Sub


    Private Sub ShowAntitankAxis(ByVal artnr As Integer, ByVal px As Integer, ByVal py As Integer)
        Dim bmp, antitank As Bitmap
        Dim posx, posy As Integer


        Me.RemoveUnitNew(px, py)
        bmp = PB1.Image
        If artnr = 1 Then
            antitank = Bitmap.FromFile(CurDir() + "\Units\antitank-002.bmp")
        Else
            antitank = Bitmap.FromFile(CurDir() + "\Units\antitank-001.bmp")
        End If
        posx = HexesPosx + ((px - 1) * RasterWidth)
        posy = HexesPosy - ((py - 1) * 2 * RasterHeight2)

        bmp = Me.AddSpriteTransWhite(bmp, antitank, posx, posy)

        PB1.Image = bmp
        Me.Refresh()

    End Sub

    Private Sub ShowInfanteryAxis(ByVal artnr As Integer, ByVal px As Integer, ByVal py As Integer)
        Dim bmp, infantery As Bitmap
        Dim posx, posy As Integer


        Me.RemoveUnitNew(px, py)
        bmp = PB1.Image
        If artnr = 1 Then
            infantery = Bitmap.FromFile(CurDir() + "\Units\Infantery-002.bmp")
        Else
            infantery = Bitmap.FromFile(CurDir() + "\Units\Infantery-001.bmp")
        End If
        posx = HexesPosx + ((px - 1) * RasterWidth)
        posy = HexesPosy - ((py - 1) * 2 * RasterHeight2)

        bmp = Me.AddSpriteTransWhite(bmp, infantery, posx, posy)

        PB1.Image = bmp
        Me.Refresh()


    End Sub
    Private Sub ShowTankAxis(ByVal artnr As Integer, ByVal px As Integer, ByVal py As Integer)
        Dim bmp, tank As Bitmap
        Dim posx, posy As Integer


        Me.RemoveUnitNew(px, py)
        bmp = PB1.Image
        If artnr = 1 Then
            tank = Bitmap.FromFile(CurDir() + "\Units\tank-002.bmp")
        Else
            tank = Bitmap.FromFile(CurDir() + "\Units\tank-001.bmp")
        End If
        posx = HexesPosx + ((px - 1) * RasterWidth)
        posy = HexesPosy - ((py - 1) * 2 * RasterHeight2)

        bmp = Me.AddSpriteTransWhite(bmp, tank, posx, posy)

        PB1.Image = bmp
        Me.Refresh()

    End Sub

    Private Sub ShowArtilleryAxis(ByVal artnr As Integer, ByVal px As Integer, ByVal py As Integer)
        Dim bmp, artillery As Bitmap
        Dim posx, posy As Integer


        Me.RemoveUnitNew(px, py)
        bmp = PB1.Image
        If artnr = 1 Then
            artillery = Bitmap.FromFile(CurDir() + "\Units\artillery-002.bmp")
        Else
            artillery = Bitmap.FromFile(CurDir() + "\Units\artillery-001.bmp")
        End If
        posx = HexesPosx + ((px - 1) * RasterWidth)
        posy = HexesPosy - ((py - 1) * 2 * RasterHeight2)

        bmp = Me.AddSpriteTransWhite(bmp, artillery, posx, posy)

        PB1.Image = bmp
        Me.Refresh()

    End Sub

    Private Sub FireAlliesArtilleries()
        Dim enemy, rownum, country, pnum, px, py, unit, myhealth, entrench, pammo, pfuel, pmove, ptrans, pattack As Integer
        Dim AttackPosx, AttackPosy As Integer
        Dim UnitPresent As Boolean = False

        For k = 0 To MyUnitsAlliesDS.Tables(0).Rows.Count - 1

            country = MyUnitsAlliesDS.Tables(0).Rows(k).Item(0)
            pnum = MyUnitsAlliesDS.Tables(0).Rows(k).Item(1)
            px = MyUnitsAlliesDS.Tables(0).Rows(k).Item(2)
            py = MyUnitsAlliesDS.Tables(0).Rows(k).Item(3)
            unit = MyUnitsAlliesDS.Tables(0).Rows(k).Item(4)
            myhealth = MyUnitsAlliesDS.Tables(0).Rows(k).Item(5)
            entrench = MyUnitsAlliesDS.Tables(0).Rows(k).Item(6)
            pammo = MyUnitsAlliesDS.Tables(0).Rows(k).Item(7)
            pfuel = MyUnitsAlliesDS.Tables(0).Rows(k).Item(8)
            pmove = MyUnitsAlliesDS.Tables(0).Rows(k).Item(9)
            ptrans = MyUnitsAlliesDS.Tables(0).Rows(k).Item(10)
            pattack = Attacked2(pnum)

            If country = 2 And unit = TypeArtillery And pattack = 0 Then
                UnitPresent = False
                For i = -2 To 2
                    For j = -2 To 2
                        If ((px + i > 0) And (py + j > 0) And (px + i < MaxRasterX) And (py + j < MaxRasterY + 1)) Then
                            enemy = UnitsField(px + i, py + j)
                            If enemy > 0 Then 'Axis unit found fire !
                                UnitPresent = True
                                AttackPosx = px + i
                                AttackPosy = py + j
                            End If

                        End If
                    Next
                Next
                If UnitPresent = True Then
                    Me.ShowArtilleryAllies(1, px, py)
                    Me.AttackAlliesArtillery(px, py, AttackPosx, AttackPosy)
                    Attacked2(pnum) = 1
                    System.Threading.Thread.Sleep(300)
                    Me.ShowArtilleryAllies(2, px, py)

                End If
            End If
        Next



    End Sub
    Private Sub ComputerMove()
        Dim endofunits As Boolean = False
        Dim Priceunit, Typeunit, UnitPosx, UnitPosy As Integer

        'Check if Artilleries can Fire
        Me.FireAlliesArtilleries()

        'Check if units can attack


        'Check if Units can be moved
        If UnitsFieldAir2(22, 15) = 18 And TurnNr > 1 Then
            Me.MoveUnit(22, 15, 10, 7, 18) 'plane siedice lodz
        End If
        If UnitsFieldAir2(22, 14) = 36 And TurnNr > 2 Then
            Me.MoveUnit(22, 14, 16, 15, 36) 'plane siedice warsaw
        End If
        If UnitsFieldAir2(23, 15) = 45 And TurnNr > 3 Then
            Me.MoveUnit(23, 15, 22, 16, 45) 'plane siedice city
        End If

        If TurnNr = 1 Then 'antitank artillery warsaw North modlin 17
            'Me.MoveUnit(15, 16, 14, 17, 12)
            'Me.MoveUnit(16, 16, 16, 17, 14)

        ElseIf TurnNr = 2 Then
            'Me.MoveUnit(14, 17, 14, 18, 12) 'antitank artillery modlin 18
            'Me.MoveUnit(16, 17, 16, 18, 14)
            'Me.MoveUnit(17, 15, 16, 16, 17) 'artillery antitank modlin 17
            'Me.MoveUnit(15, 15, 14, 16, 11)
        ElseIf TurnNr = 3 Then
            'Me.MoveUnit(16, 16, 16, 17, 17) 'artillery antitank modlin 18
            'Me.MoveUnit(14, 16, 14, 17, 11)
            'Me.MoveUnit(16, 18, 15, 18, 14) 'artillery modlin 1 links
        ElseIf TurnNr = 4 Then
            'Me.MoveUnit(16, 17, 16, 18, 17) 'artillery modlin 18

        ElseIf TurnNr = 5 Then
            'Me.MoveUnit(17, 16, 16, 17, 28) 'artillery warsaw modlin 17 28 Klopt niet !!!
        ElseIf TurnNr = 6 Then

        ElseIf TurnNr = 7 Then
            'Me.MoveUnit(22, 14, 16, 15, 30) 'artillery warsaw modlin 17
        End If


        'Check if units can be restored


        'Place New Units
        Do Until endofunits = True
            Priceunit = AIPlaces(CurrentAIMove, 4)
            If Priceunit < PrestigeAllies And Priceunit > 0 Then
                UnitPosx = AIPlaces(CurrentAIMove, 1)
                UnitPosy = AIPlaces(CurrentAIMove, 2)
                Typeunit = AIPlaces(CurrentAIMove, 3)
                If (UnitsField(UnitPosx, UnitPosy) > 0 Or UnitsField2(UnitPosx, UnitPosy) > 0) And Typeunit <> TypeFighter And Typeunit <> TypeBomber Then
                    'Ground force placed on this spot
                    CurrentAIMove = CurrentAIMove + 1
                    Return
                End If
                If (UnitsFieldAir(UnitPosx, UnitPosy) > 0 Or UnitsFieldAir2(UnitPosx, UnitPosy) > 0) And (Typeunit = TypeFighter Or Typeunit = TypeBomber) Then
                    'Plane is placed on this spot
                    CurrentAIMove = CurrentAIMove + 1
                    Return
                End If

                Me.BuyAlliesUnit(Priceunit, Typeunit, UnitPosx, UnitPosy)
                PrestigeAllies = PrestigeAllies - Priceunit

                CurrentAIMove = CurrentAIMove + 1
            Else
                endofunits = True
            End If
        Loop


    End Sub

    Private Sub ComputerMoveOld()
        Dim costs As Integer

        Select Case TurnNr

            Case 1

                Me.BuyAlliesUnit(PriceAntitank, TypeAntitank, AIPosx1, AIPosy1)
                costs = costs + PriceAntitank
                Me.BuyAlliesUnit(PriceAntitank, TypeAntitank, AIPosx2, AIPosy2)
                costs = costs + PriceAntitank

            Case 2

                Me.BuyAlliesUnit(PriceAirdefense, TypeAirdefense, AIPosx3, AIPosy3)
                costs = costs + PriceAirdefense
                'Me.BuyAlliesUnit(PriceAntitank, TypeAntitank, AIPosx4, AIPosy4)
                'costs = costs + PriceAntitank

            Case 3

                Me.BuyAlliesUnit(PriceArtillery, TypeArtillery, AIPosx5, AIPosy5)
                costs = costs + PriceArtillery

                Me.BuyAlliesUnit(PriceArtillery, TypeArtillery, AIPosx6, AIPosy6)
                costs = costs + PriceArtillery

            Case 4

                If UnitsField(AIPosx4, AIPosy4) = 0 Then
                    Me.BuyAlliesUnit(PriceAntitank, TypeAntitank, AIPosx4, AIPosy4)
                    costs = costs + PriceAntitank
                    Me.BuyAlliesUnit(PriceAntitank, TypeAntitank, AIPosx4 + 2, AIPosy4)
                    costs = costs + PriceAntitank
                Else
                    Me.BuyAlliesUnit(PriceAntitank, TypeAntitank, AIPosx4 + 2, AIPosy4)
                    costs = costs + PriceAntitank
                    Me.BuyAlliesUnit(PriceAntitank, TypeAntitank, AIPosx4 + 3, AIPosy4)
                    costs = costs + PriceAntitank
                End If

            Case 5
                Me.BuyAlliesUnit(PriceAirdefense, TypeAirdefense, AIPosx7, AIPosy7)
                costs = costs + PriceAirdefense
                Me.BuyAlliesUnit(PriceAirdefense, TypeAirdefense, AIPosx8, AIPosy8)
                costs = costs + PriceAirdefense

            Case 6
                Me.BuyAlliesUnit(PriceFighter, TypeFighter, AIPosx9, AIPosy9)
                costs = costs + PriceFighter

            Case 7

            Case 8

            Case 9

            Case 10

        End Select

        PrestigeAllies = PrestigeAllies - costs
        txtPrestige.Text = PrestigeAllies.ToString()

        'Me.UpdateAllHealth()



    End Sub


    Private Sub ChangePlayer()

        Dim str As String

        If Turn = "Axis" Then
            If Me.CheckVictory() = True Then
                MsgBox("The AXIS forces have won this Battle !!!", MsgBoxStyle.Information)
                Me.Close()
            End If
            Me.UpdateEntrenchment("Axis")

            Turn = "Allies"
            MySettingsDS.Tables(0).Rows(0).Item(0) = 2

            PRECT.Visible = False
            For i = 0 To 99
                MA2(i) = 0
                Attacked2(i) = 0
            Next
            Me.ShowAllUnitsAllies()
            Me.ShowHealth(2)
            Moved = False
            PrestigeAllies = PrestigeAllies + PrestigeTurnAllies
            Me.UpdatePrestigeCountries("Allies")
            txtPrestige.Text = PrestigeAllies.ToString()
            txtCities.Text = CitiesAxis.ToString()
            Me.CheckStartPlaces()

            LastPosx = 0
            LastPosy = 0
            lblTurn.Location = New Point(lblTurn.Location.X - 5, lblTurn.Location.Y)
            lblTurn.Text = "allies"
            Player01.BackColor = Color.FromName("GradientInactiveCaption")
            Player02.BackColor = Color.LightYellow

            If TypeofGame = 2 Then 'Allies = AI
                Me.ComputerMove()
            ElseIf TypeofGame = 4 Then 'PBEM make export files
                str = TurnNr.ToString() + "A"
                If str.Length() = 2 Then
                    str = "0" + str
                End If
                Me.ExportGame(str)
                Me.Close()

            End If



        Else

            Me.UpdateEntrenchment("Allies")
            Turn = "Axis"
            MySettingsDS.Tables(0).Rows(0).Item(0) = 1
            If TypeofGame = 4 Then 'Allies = AI
                str = TurnNr.ToString() + "B"
                If str.Length() = 2 Then
                    str = "0" + str
                End If
                Me.ExportGame(str)
                Me.Close()
            End If


            PRECT.Visible = False
            For i = 0 To 99
                MA(i) = 0
                Attacked(i) = 0
            Next

            TurnNr = TurnNr + 1
            txtTurn.Text = TurnNr.ToString()
            MySettingsDS.Tables(0).Rows(0).Item(1) = TurnNr

            If TurnNr = MaxTurns + 1 Then
                MsgBox("The brave Polish forces have won this Battle !!!", MsgBoxStyle.Information)
                Me.Close()
            End If

            Me.ShowAllUnitsAxis()
            'ShowEnvironmentAxisAll()
            Me.ShowHealth(1)

            Moved = False
            PrestigeAxis = PrestigeAxis + PrestigeTurnAxis
            Me.UpdatePrestigeCountries("Axis")

            txtPrestige.Text = PrestigeAxis.ToString()
            txtCities.Text = CitiesAllies.ToString()

            Me.CheckStartPlaces()

            LastPosx = 0
            LastPosy = 0
            lblTurn.Location = New Point(lblTurn.Location.X + 5, lblTurn.Location.Y)
            lblTurn.Text = "axis"

            Player01.BackColor = Color.LightYellow
            Player02.BackColor = Color.FromName("GradientInactiveCaption")


        End If



    End Sub

    Private Sub Button7_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click

        Me.ChangePlayer()
        If TypeofGame = 2 Then
            Me.ChangePlayer()
        End If


    End Sub

    Private Sub CheckStartPlaces()

        If Turn = "Axis" Then

            cbAxis1.Visible = True
            cbAxis2.Visible = True

            cbAllies1.Visible = False
            cbAllies2.Visible = False
            cbAllies3.Visible = False

        Else

            cbAxis1.Visible = False
            cbAxis2.Visible = False

            cbAllies1.Visible = True
            cbAllies2.Visible = True
            cbAllies3.Visible = True

        End If

    End Sub

    Private Sub PRECT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PRECT.Click


    End Sub

    Private Sub Button8_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If Moved = True Then
            MsgBox("You can only save at the start of a new turn !", MsgBoxStyle.Information)

        Else
            MyUnitsAxisDS.WriteXml(CurDir() + "\Saves\S01_Poland_UnitsAxis" + TextBox7.Text + ".xml")
            MyUnitsAlliesDS.WriteXml(CurDir() + "\Saves\S01_Poland_UnitsAllies" + TextBox7.Text + ".xml")
        End If



    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        MyUnitsAxisDS.Reset()
        MyUnitsAlliesDS.Reset()
        MyUnitsAxisDS.ReadXml(CurDir() + "\Saves\S01_Poland_UnitsAxis" + TextBox7.Text + ".xml")
        MyUnitsAlliesDS.ReadXml(CurDir() + "\Saves\S01_Poland_UnitsAllies" + TextBox7.Text + ".xml")


        'Dim Myreader = System.Xml.XmlReader

        'Dim xmlString = "<Book id=""bk102"">" & vbCrLf & "  <Author>Garcia, Debra</Author>" & vbCrLf & "  <Title>Writing Code</Title>" & vbCrLf & "  <Price>5.95</Price>" & vbCrLf & "</Book>"
        'Dim xmlElem = XElement.Parse(xmlString)
        'Console.WriteLine(xmlElem)
        'Dim xmlDoc = XDocument.Parse(xmlString)
        'Console.WriteLine(xmlDoc)
        'Dim reader = System.Xml.XmlReader.Create(CurDir() + "\xml\S01_Poland_cities.xml")
        'reader.MoveToContent()
        'Dim inputXml = XDocument.ReadFrom(reader)
        'Dim xmlElem2 = XElement.Parse(inputXml)
        'Dim xmlDoc1 As System.Xml.XmlDocument
        'Dim xmlNodeRdr As New System.Xml.XmlNodeReader(xmlDoc1) 'xmlDoc is your XmlDocument
        'Dim ds2 As New DataSet()
        'ds2.ReadXml(xmlNodeRdr)



        Me.FillUnitsNew()
        Me.ShowAllUnitsAxis()
        Me.ShowCountries()
        Me.ShowHealth(1)

    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click

        Health(CurUnit) = 5
        Me.ShowHealth(1)



    End Sub

    Private Sub SupplyAmmoAlliesPlane(ByVal px As Integer, ByVal py As Integer, ByVal theunit As Integer)
        Dim unittype, MyMA, Myammo, Myattack, count, newammo As Integer
        Dim n1, n2, n3, n4, n5, n6, n7, n8

        unittype = Units2(theunit)
        MyMA = MA2(theunit)
        Myammo = Ammo2(theunit)
        Myattack = Attacked2(theunit)

        Select Case unittype

            Case TypeFighter
                Ammo2(theunit) = AmmoPlane

            Case TypeBomber
                Ammo2(theunit) = AmmoPlane

        End Select
        MA2(theunit) = MAMax2(theunit)

    End Sub

    Private Sub SupplyAmmoAllies(ByVal px As Integer, ByVal py As Integer, ByVal theunit As Integer)
        Dim unittype, MyMA, Myammo, Myattack, count, newammo As Integer
        Dim n1, n2, n3, n4, n5, n6, n7, n8

        unittype = Units2(theunit)
        MyMA = MA2(theunit)
        Myammo = Ammo2(theunit)
        Myattack = Attacked2(theunit)

        If MyMA > 0 Or Myattack > 0 Then
            Return
        End If

        If px > 1 Then
            If UnitsField(px - 1, py) > 0 Then
                n2 = 1
            End If
        End If
        If px > 1 And py > 1 Then
            If UnitsField(px - 1, py - 1) > 0 Then
                n1 = 1
            End If
        End If
        If px > 1 And Cury < 23 Then
            If UnitsField(px - 1, py + 1) > 0 Then
                n3 = 1
            End If
        End If
        If Cury > 1 Then
            If UnitsField(px, py - 1) > 0 Then
                n4 = 1
            End If
        End If
        If Cury < 23 Then
            If UnitsField(px, py + 1) > 0 Then
                n5 = 1
            End If
        End If
        If px < 23 And Cury > 1 Then
            If UnitsField(px + 1, py - 1) > 0 Then
                n6 = 1
            End If
        End If
        If px < 23 Then
            If UnitsField(px + 1, py) > 0 Then
                n7 = 1
            End If
        End If
        If px < 23 And Cury < 23 Then
            If UnitsField(px + 1, py + 1) > 0 Then
                n8 = 1
            End If
        End If
        count = n1 + n2 + n3 + n4 + n5 + n6 + n7 + n8

        If count >= 3 Then
            count = 3
        End If
        If count = 3 Then
            newammo = 2
        ElseIf count = 2 Then
            newammo = 3
        ElseIf count = 1 Then
            newammo = 4
        End If

        If count > 0 Then
            Ammo2(theunit) = Math.Min(Myammo + newammo, 10)
        Else
            Select Case unittype

                Case TypeInfantery
                    Ammo2(theunit) = AmmoInfanterie

                Case TypeTank
                    Ammo2(theunit) = AmmoTank

                Case TypeArtillery
                    Ammo2(theunit) = AmmoArtillery

                Case TypeAntitank
                    Ammo2(theunit) = AmmoAntitank

                Case TypeFighter
                    Ammo2(theunit) = AmmoPlane

                Case TypeBomber
                    Ammo2(theunit) = AmmoPlane

            End Select
        End If
        MA2(theunit) = MAMax2(theunit)


    End Sub

    Private Sub SupplyFuelAlliesPlane(ByVal px As Integer, ByVal py As Integer, ByVal theunit As Integer)
        Dim unittype, MyMA, Myfuel, Myattack, count, newfuel As Integer
        Dim n1, n2, n3, n4, n5, n6, n7, n8

        unittype = Units2(theunit)
        MyMA = MA2(theunit)
        Myattack = Attacked2(theunit)
        Myfuel = Fuel2(theunit)

        Select Case unittype

            Case TypeFighter
                Fuel2(theunit) = FuelPlane

            Case TypeBomber
                Fuel2(theunit) = FuelPlane

        End Select

    End Sub

    Private Sub SupplyFuelAllies(ByVal px As Integer, ByVal py As Integer, ByVal theunit As Integer)
        Dim unittype, MyMA, Myfuel, Myattack, count, newfuel As Integer
        Dim n1, n2, n3, n4, n5, n6, n7, n8

        unittype = Units2(theunit)
        MyMA = MA2(theunit)
        Myattack = Attacked2(theunit)
        Myfuel = Fuel2(theunit)

        If MyMA > 0 Or Myattack > 0 Then
            Return
        End If

        If px > 1 Then
            If UnitsField(px - 1, py) > 0 Then
                n2 = 1
            End If
        End If
        If px > 1 And py > 1 Then
            If UnitsField(px - 1, py - 1) > 0 Then
                n1 = 1
            End If
        End If
        If px > 1 And Cury < 23 Then
            If UnitsField(px - 1, py + 1) > 0 Then
                n3 = 1
            End If
        End If
        If Cury > 1 Then
            If UnitsField(px, py - 1) > 0 Then
                n4 = 1
            End If
        End If
        If Cury < 23 Then
            If UnitsField(px, py + 1) > 0 Then
                n5 = 1
            End If
        End If
        If px < 23 And Cury > 1 Then
            If UnitsField(px + 1, py - 1) > 0 Then
                n6 = 1
            End If
        End If
        If px < 23 Then
            If UnitsField(px + 1, py) > 0 Then
                n7 = 1
            End If
        End If
        If px < 23 And Cury < 23 Then
            If UnitsField(px + 1, py + 1) > 0 Then
                n8 = 1
            End If
        End If
        count = n1 + n2 + n3 + n4 + n5 + n6 + n7 + n8

        If count >= 3 Then
            count = 3
        End If
        If count = 3 Then
            newfuel = 20
        ElseIf count = 2 Then
            newfuel = 25
        ElseIf count = 1 Then
            newfuel = 30
        End If

        If count > 0 Then
            If unittype = TypeTank Or unittype = TypeFighter Or unittype = TypeBomber Then
                Fuel2(theunit) = Math.Max(newfuel, Myfuel)
            End If
        Else
            Select Case unittype

                Case TypeTank
                    Fuel2(theunit) = FuelTank

                Case TypeFighter
                    Fuel2(theunit) = FuelPlane

                Case TypeBomber
                    Fuel2(theunit) = FuelPlane

            End Select

        End If

    End Sub

    Private Sub SupplyFuelAxisPlane(ByVal px As Integer, ByVal py As Integer, ByVal theunit As Integer)
        Dim unittype, MyMA, Myfuel, Myattack, count, newfuel As Integer
        Dim n1, n2, n3, n4, n5, n6, n7, n8

        unittype = Units(theunit)
        MyMA = MA(theunit)
        Myattack = Attacked(theunit)
        Myfuel = Fuel(theunit)

        Select Case unittype

            Case TypeFighter
                Fuel(theunit) = FuelPlane

            Case TypeBomber
                Fuel(theunit) = FuelPlane

        End Select


    End Sub

    Private Sub SupplyFuelAxis(ByVal px As Integer, ByVal py As Integer, ByVal theunit As Integer)
        Dim unittype, MyMA, Myfuel, Myattack, count, newfuel As Integer
        Dim n1, n2, n3, n4, n5, n6, n7, n8

        unittype = Units(theunit)
        MyMA = MA(theunit)
        Myattack = Attacked(theunit)
        Myfuel = Fuel(theunit)

        If MyMA > 0 Or Myattack > 0 Then
            Return
        End If

        If px > 1 Then
            If UnitsField2(px - 1, py) > 0 Then
                n2 = 1
            End If
        End If
        If px > 1 And py > 1 Then
            If UnitsField2(px - 1, py - 1) > 0 Then
                n1 = 1
            End If
        End If
        If px > 1 And Cury < 23 Then
            If UnitsField2(px - 1, py + 1) > 0 Then
                n3 = 1
            End If
        End If
        If Cury > 1 Then
            If UnitsField2(px, py - 1) > 0 Then
                n4 = 1
            End If
        End If
        If Cury < 23 Then
            If UnitsField2(px, py + 1) > 0 Then
                n5 = 1
            End If
        End If
        If px < 23 And Cury > 1 Then
            If UnitsField2(px + 1, py - 1) > 0 Then
                n6 = 1
            End If
        End If
        If px < 23 Then
            If UnitsField2(px + 1, py) > 0 Then
                n7 = 1
            End If
        End If
        If px < 23 And Cury < 23 Then
            If UnitsField2(px + 1, py + 1) > 0 Then
                n8 = 1
            End If
        End If
        count = n1 + n2 + n3 + n4 + n5 + n6 + n7 + n8

        If count >= 3 Then
            count = 3
        End If
        If count = 3 Then
            newfuel = 20
        ElseIf count = 2 Then
            newfuel = 25
        ElseIf count = 1 Then
            newfuel = 30
        End If

        If count > 0 Then
            If unittype = TypeTank Or unittype = TypeFighter Or unittype = TypeBomber Then
                Fuel(theunit) = Math.Max(newfuel, Myfuel)
            End If
        Else
            Select Case unittype

                Case TypeTank
                    Fuel(theunit) = FuelTank

                Case TypeFighter
                    Fuel(theunit) = FuelPlane

                Case TypeBomber
                    Fuel(theunit) = FuelPlane

            End Select

        End If

    End Sub

    Private Sub SupplyAmmoAxisPlane(ByVal px As Integer, ByVal py As Integer, ByVal theunit As Integer)
        Dim unittype, MyMA, Myammo, Myattack, count, newammo As Integer
        Dim n1, n2, n3, n4, n5, n6, n7, n8

        unittype = Units(theunit)
        MyMA = MA(theunit)
        Myammo = Ammo(theunit)
        Myattack = Attacked(theunit)

        Select Case unittype

            Case TypeInfantery
                Ammo(theunit) = AmmoInfanterie

            Case TypeTank
                Ammo(theunit) = AmmoTank

            Case TypeArtillery
                Ammo(theunit) = AmmoArtillery

            Case TypeAntitank
                Ammo(theunit) = AmmoAntitank

            Case TypeFighter
                Ammo(theunit) = AmmoPlane

            Case TypeBomber
                Ammo(theunit) = AmmoPlane

        End Select
        MA(theunit) = MAMax(theunit)

    End Sub

    Private Sub SupplyAmmoAxis(ByVal px As Integer, ByVal py As Integer, ByVal theunit As Integer)
        Dim unittype, MyMA, Myammo, Myattack, count, newammo As Integer
        Dim n1, n2, n3, n4, n5, n6, n7, n8

        unittype = Units(theunit)
        MyMA = MA(theunit)
        Myammo = Ammo(theunit)
        Myattack = Attacked(theunit)

        If MyMA > 0 Or Myattack > 0 Then
            Return
        End If

        If px > 1 Then
            If UnitsField2(px - 1, py) > 0 Then
                n2 = 1
            End If
        End If
        If px > 1 And py > 1 Then
            If UnitsField2(px - 1, py - 1) > 0 Then
                n1 = 1
            End If
        End If
        If px > 1 And Cury < 23 Then
            If UnitsField2(px - 1, py + 1) > 0 Then
                n3 = 1
            End If
        End If
        If Cury > 1 Then
            If UnitsField2(px, py - 1) > 0 Then
                n4 = 1
            End If
        End If
        If Cury < 23 Then
            If UnitsField2(px, py + 1) > 0 Then
                n5 = 1
            End If
        End If
        If px < 23 And Cury > 1 Then
            If UnitsField2(px + 1, py - 1) > 0 Then
                n6 = 1
            End If
        End If
        If px < 23 Then
            If UnitsField2(px + 1, py) > 0 Then
                n7 = 1
            End If
        End If
        If px < 23 And Cury < 23 Then
            If UnitsField2(px + 1, py + 1) > 0 Then
                n8 = 1
            End If
        End If
        count = n1 + n2 + n3 + n4 + n5 + n6 + n7 + n8

        If count >= 3 Then
            count = 3
        End If
        If count = 3 Then
            newammo = 2
        ElseIf count = 2 Then
            newammo = 3
        ElseIf count = 1 Then
            newammo = 4
        End If

        If count > 0 Then
            Ammo(theunit) = Math.Min(Myammo + newammo, 10)
        Else
            Select Case unittype

                Case TypeInfantery
                    Ammo(theunit) = AmmoInfanterie

                Case TypeTank
                    Ammo(theunit) = AmmoTank

                Case TypeArtillery
                    Ammo(theunit) = AmmoArtillery

                Case TypeAntitank
                    Ammo(theunit) = AmmoAntitank

                Case TypeFighter
                    Ammo(theunit) = AmmoPlane

                Case TypeBomber
                    Ammo(theunit) = AmmoPlane

            End Select
        End If
        MA(theunit) = MAMax(theunit)


    End Sub

    Private Sub RepairAlliesPlane(ByVal px As Integer, ByVal py As Integer, ByVal theunit As Integer)
        Dim unittype, MyMA, Myhealth, Myattack, count, newhealth As Integer

        unittype = Units2(theunit)
        MyMA = MA2(theunit)
        Myhealth = Health2(theunit)
        Myattack = Attacked2(theunit)
        Health2(theunit) = 10
        'Me.ShowHealth(2, px, py)


    End Sub

    Private Sub RepairAllies(ByVal px As Integer, ByVal py As Integer, ByVal theunit As Integer)
        Dim unittype, MyMA, Myhealth, Myattack, count, newhealth As Integer
        Dim n1, n2, n3, n4, n5, n6, n7, n8

        unittype = Units2(theunit)
        MyMA = MA2(theunit)
        Myhealth = Health2(theunit)
        Myattack = Attacked2(theunit)

        If MyMA > 0 Or Myattack > 0 Then
            Return
        End If
        'MsgBox(Myattack.ToString())

        If px > 1 Then
            If UnitsField(px - 1, py) > 0 Then
                n2 = 1
            End If
        End If
        If px > 1 And py > 1 Then
            If UnitsField(px - 1, py - 1) > 0 Then
                n1 = 1
            End If
        End If
        If px > 1 And Cury < 23 Then
            If UnitsField(px - 1, py + 1) > 0 Then
                n3 = 1
            End If
        End If
        If Cury > 1 Then
            If UnitsField(px, py - 1) > 0 Then
                n4 = 1
            End If
        End If
        If Cury < 23 Then
            If UnitsField(px, py + 1) > 0 Then
                n5 = 1
            End If
        End If
        If px < 23 And Cury > 1 Then
            If UnitsField(px + 1, py - 1) > 0 Then
                n6 = 1
            End If
        End If
        If px < 23 Then
            If UnitsField(px + 1, py) > 0 Then
                n7 = 1
            End If
        End If
        If px < 23 And Cury < 23 Then
            If UnitsField(px + 1, py + 1) > 0 Then
                n8 = 1
            End If
        End If
        count = n1 + n2 + n3 + n4 + n5 + n6 + n7 + n8

        If count >= 3 Then
            count = 3
        End If
        If count = 3 Then
            newhealth = 1
        ElseIf count = 2 Then
            newhealth = 2
        ElseIf count = 1 Then
            newhealth = 3
        End If

        If newhealth > 0 Then
            Myhealth = Myhealth + newhealth
            If Myhealth > 10 Then
                Myhealth = 10
            End If
            Health2(theunit) = Myhealth
        Else
            Health2(theunit) = 10
        End If
        'MA2(theunit) = MAMax2(theunit)
        'Me.ShowHealth(2, px, py)
        Moved = True


    End Sub

    Private Sub RepairAxisPlane(ByVal px As Integer, ByVal py As Integer, ByVal theunit As Integer)
        Dim unittype, MyMA, Myhealth, Myattack, count, newhealth As Integer

        unittype = Units(theunit)
        MyMA = MA(theunit)
        Myhealth = Health(theunit)
        Myattack = Attacked(theunit)
        Health(theunit) = 10
        'Me.ShowHealth(1, px, py)



    End Sub
    Private Sub RepairAxis(ByVal px As Integer, ByVal py As Integer, ByVal theunit As Integer)

        Dim unittype, MyMA, Myhealth, Myattack, count, newhealth As Integer
        Dim n1, n2, n3, n4, n5, n6, n7, n8

        unittype = Units(theunit)
        MyMA = MA(theunit)
        Myhealth = Health(theunit)
        Myattack = Attacked(theunit)

        If MyMA > 0 Or Myattack > 0 Then
            Return
        End If
        'MsgBox(Myattack.ToString())

        If px > 1 Then
            If UnitsField2(px - 1, py) > 0 Then
                n2 = 1
            End If
        End If
        If px > 1 And py > 1 Then
            If UnitsField2(px - 1, py - 1) > 0 Then
                n1 = 1
            End If
        End If
        If px > 1 And Cury < 23 Then
            If UnitsField2(px - 1, py + 1) > 0 Then
                n3 = 1
            End If
        End If
        If Cury > 1 Then
            If UnitsField2(px, py - 1) > 0 Then
                n4 = 1
            End If
        End If
        If Cury < 23 Then
            If UnitsField2(px, py + 1) > 0 Then
                n5 = 1
            End If
        End If
        If px < 23 And Cury > 1 Then
            If UnitsField2(px + 1, py - 1) > 0 Then
                n6 = 1
            End If
        End If
        If px < 23 Then
            If UnitsField2(px + 1, py) > 0 Then
                n7 = 1
            End If
        End If
        If px < 23 And Cury < 23 Then
            If UnitsField2(px + 1, py + 1) > 0 Then
                n8 = 1
            End If
        End If
        count = n1 + n2 + n3 + n4 + n5 + n6 + n7 + n8

        If count >= 3 Then
            count = 3
        End If
        If count = 3 Then
            newhealth = 1
        ElseIf count = 2 Then
            newhealth = 2
        ElseIf count = 1 Then
            newhealth = 3
        End If

        If newhealth > 0 Then
            Myhealth = Myhealth + newhealth
            If Myhealth > 10 Then
                Myhealth = 10
            End If
            Health(theunit) = Myhealth
        Else
            Health(theunit) = 10
        End If
        'MA(theunit) = MAMax(theunit)
        'Me.ShowHealth(1, px, py)
        Moved = True


    End Sub
    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Dim myunittype As Integer


        If Turn = "Axis" Then
            myunittype = Units(CurUnit)
            If myunittype = TypeFighter Or myunittype = TypeBomber Then
                If Me.CheckAirport(1, Curx, Cury) = 0 Then 'Only rest/supply on airport
                    Return
                End If
            End If
            Me.RepairAxis(Curx, Cury, CurUnit)
            Me.SupplyFuelAxis(Curx, Cury, CurUnit)
            Me.SupplyAmmoAxis(Curx, Cury, CurUnit)
            Me.ShowHealthUnit(1, Curx, Cury, CurUnit)
            Me.ShowParameters()

        Else
            myunittype = Units2(CurUnit)
            If myunittype = TypeFighter Or myunittype = TypeBomber Then
                If Me.CheckAirport(2, Curx, Cury) = 0 Then 'Only rest/supply on airport
                    Return
                End If
            End If
            Me.RepairAllies(Curx, Cury, CurUnit)
            Me.SupplyFuelAllies(Curx, Cury, CurUnit)
            Me.SupplyAmmoAllies(Curx, Cury, CurUnit)
            Me.ShowHealthUnit(2, Curx, Cury, CurUnit)
            Me.ShowParameters()

        End If




    End Sub

    Private Sub ImportGame(ByVal gamenum As String)

        Dim txtAxisDecipheredFile, txtAxisCiphertextFile, txtAllieDecipheredFile, txtAlliesCiphertextFile As String
        Dim txtSettingsDecipheredFile, txtSettingsCiphertextFile As String
        Dim country, turnnum As Integer

        MySettingsDS.Reset()
        MyUnitsAxisDS.Reset()
        MyUnitsAlliesDS.Reset()

        txtSettingsCiphertextFile = CurDir() + "\PBEM\" + SavegameSettings + gamenum + ".sav"
        txtSettingsDecipheredFile = CurDir() + "\PBEM\" + SavegameSettings + gamenum + ".xml"

        txtAxisCiphertextFile = CurDir() + "\PBEM\" + SavegameAxis + gamenum + ".sav"
        txtAxisDecipheredFile = CurDir() + "\PBEM\" + SavegameAxis + gamenum + ".xml"

        txtAlliesCiphertextFile = CurDir() + "\PBEM\" + SavegameAllies + gamenum + ".sav"
        txtAllieDecipheredFile = CurDir() + "\PBEM\" + SavegameAllies + gamenum + ".xml"


        CryptoStuff.DecryptFile(MyPassword, txtSettingsCiphertextFile, txtSettingsDecipheredFile)
        'txtPlaintext.Text = File.ReadAllText(txtAxisCiphertextFile)
        CryptoStuff.DecryptFile(MyPassword, txtAxisCiphertextFile, txtAxisDecipheredFile)
        'txtCiphertext.Text = File.ReadAllText(txtAxisDecipheredFile)

        'txtPlaintext.Text = File.ReadAllText(txtAlliesCiphertextFile)
        CryptoStuff.DecryptFile(MyPassword, txtAlliesCiphertextFile, txtAllieDecipheredFile)
        'txtCiphertext.Text = File.ReadAllText(txtAllieDecipheredFile)


        MySettingsDS.ReadXml(CurDir() + "\PBEM\" + SavegameSettings + gamenum + ".xml")
        MyUnitsAxisDS.ReadXml(CurDir() + "\PBEM\" + SavegameAxis + gamenum + ".xml")
        MyUnitsAlliesDS.ReadXml(CurDir() + "\PBEM\" + SavegameAllies + gamenum + ".xml")

        country = MySettingsDS.Tables(0).Rows(0).Item(0)
        turnnum = MySettingsDS.Tables(0).Rows(0).Item(1)
        If turnnum > 0 Then
            TurnNr = turnnum
        Else
            TurnNr = 1
        End If
        txtTurn.Text = TurnNr.ToString()

        If country = 1 Then
            Turn = "Axis"
            Me.FillUnitsNew()
            Me.ShowAllUnitsAxis()
            Me.ShowCountries()
            Me.ShowHealth(1)
            lblTurn.Text = "axis"
            lblTurn.Location = New Point(lblTurn.Location.X + 5, lblTurn.Location.Y)

        Else
            Turn = "Allies"
            Me.FillUnitsNew()
            Me.ShowAllUnitsAllies()
            Me.ShowCountries()
            Me.ShowHealth(2)
            lblTurn.Text = "allies"

        End If


    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Me.ImportGame(TextBox7.Text)


    End Sub

    Private Sub ExportGame(ByVal gamenum As String)

        Dim txtAxisPlaintextFile, txtAxisCiphertextFile, txtAlliesPlaintextFile, txtAlliesCiphertextFile As String
        Dim txtSettingsPlaintextFile, txtSettingsCiphertextFile As String
        Dim Army As String
        Dim country, rc As Integer


        If File.Exists(CurDir() + "\PBEM\" + SavegameSettings + gamenum + ".sav") Then
            rc = MsgBox("The Savegame " + SavegameSettings + gamenum + " already exists! Do you want to overwrite?", MsgBoxStyle.YesNo)
            If rc = MsgBoxResult.No Then
                Return
            End If
        End If

        If Turn = "Axis" Then
            country = 1
        Else
            country = 2
        End If
        MySettingsDS.Tables(0).Rows(0).Item(0) = country
        MySettingsDS.Tables(0).Rows(0).Item(1) = TurnNr
        MySettingsDS.Tables(0).Rows(0).Item(2) = GameMode.ToString()
        MySettingsDS.Tables(0).Rows(0).Item(3) = TypeofGame.ToString()

        'Axis Encrypted Save Files
        MySettingsDS.WriteXml(CurDir() + "\PBEM\" + SavegameSettings + gamenum + ".xml")
        MyUnitsAxisDS.WriteXml(CurDir() + "\PBEM\" + SavegameAxis + gamenum + ".xml")
        MyUnitsAlliesDS.WriteXml(CurDir() + "\PBEM\" + SavegameAllies + gamenum + ".xml")

        txtSettingsPlaintextFile = CurDir() + "\PBEM\" + SavegameSettings + gamenum + ".xml"
        txtSettingsCiphertextFile = CurDir() + "\PBEM\" + SavegameSettings + gamenum + ".sav"

        txtAxisPlaintextFile = CurDir() + "\PBEM\" + SavegameAxis + gamenum + ".xml"
        txtAxisCiphertextFile = CurDir() + "\PBEM\" + SavegameAxis + gamenum + ".sav"

        txtAlliesPlaintextFile = CurDir() + "\PBEM\" + SavegameAllies + gamenum + ".xml"
        txtAlliesCiphertextFile = CurDir() + "\PBEM\" + SavegameAllies + gamenum + ".sav"


        CryptoStuff.EncryptFile(MyPassword, txtSettingsPlaintextFile, txtSettingsCiphertextFile)
        'txtPlaintext.Text = File.ReadAllText(txtAxisPlaintextFile)
        CryptoStuff.EncryptFile(MyPassword, txtAxisPlaintextFile, txtAxisCiphertextFile)
        'txtCiphertext.Text = File.ReadAllText(txtAxisCiphertextFile)

        'txtPlaintext.Text = File.ReadAllText(txtAlliesPlaintextFile)
        CryptoStuff.EncryptFile(MyPassword, txtAlliesPlaintextFile, txtAlliesCiphertextFile)
        'txtCiphertext.Text = File.ReadAllText(txtAlliesCiphertextFile)

    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click

        Me.ExportGame(TextBox7.Text)


    End Sub

    Private Sub PRECT_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PRECT.MouseDown

        If e.Button = MouseButtons.Right Then 'show menu
            'MsgBox("Right Button Clicked")
            If PRECT.Visible = True Then

                If CurUnitType = TypeInfantery Or CurUnitType = TypeArtillery Or CurUnitType = TypeAirdefense Or CurUnitType = TypeAntitank Then
                    BMount.Location = New Point(PRECT.Location.X, PRECT.Location.Y + 70)
                    BDismount.Location = New Point(BMount.Location.X, BMount.Location.Y + BMount.Height)

                    BMount.Visible = True
                    BDismount.Visible = True

                End If
            End If

        Else
            If PRECT.Visible = True Then
                PRECT.Visible = False
                'LastPosx = 0
                'LastPosy = 0

            Else
                PRECT.Visible = True
            End If


        End If


    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Dim country, rc As Integer
        Dim filnam As String

        If Moved = True Then
            MsgBox("You can only save at the start of a new turn !", MsgBoxStyle.Information)
            Return

        Else

            If File.Exists(CurDir() + "\Saves\" + SavegameSettings + TextBox7.Text + ".xml") Then
                rc = MsgBox("The Savegame " + SavegameSettings + TextBox7.Text + " already exists! Do you want to overwrite?", MsgBoxStyle.YesNo)
                If rc = MsgBoxResult.No Then
                    Return
                End If
            End If

            If Turn = "Axis" Then
                country = 1
            Else
                country = 2
            End If
            MySettingsDS.Tables(0).Rows(0).Item(0) = country
            MySettingsDS.Tables(0).Rows(0).Item(1) = TurnNr
            MySettingsDS.Tables(0).Rows(0).Item(2) = GameMode.ToString()
            MySettingsDS.Tables(0).Rows(0).Item(3) = TypeofGame.ToString()

            MySettingsDS.WriteXml(CurDir() + "\Saves\" + SavegameSettings + TextBox7.Text + ".xml")
            MyUnitsAxisDS.WriteXml(CurDir() + "\Saves\" + SavegameAxis + TextBox7.Text + ".xml")
            MyUnitsAlliesDS.WriteXml(CurDir() + "\Saves\" + SavegameAllies + TextBox7.Text + ".xml")


        End If



    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        Dim country, turnnum As Integer
        Dim savename As String

        savename = CurDir() + "\Saves\" + SavegameSettings + TextBox7.Text + ".xml"
        If Not My.Computer.FileSystem.FileExists(savename) Then
            MsgBox("Savefile: " + TextBox7.Text + " does not exist!")
            Return
        End If

        MySettingsDS.Reset()
        MyUnitsAxisDS.Reset()
        MyUnitsAlliesDS.Reset()
        MySettingsDS.ReadXml(CurDir() + "\Saves\" + SavegameSettings + TextBox7.Text + ".xml")
        country = MySettingsDS.Tables(0).Rows(0).Item(0)
        turnnum = MySettingsDS.Tables(0).Rows(0).Item(1)
        GameMode = MySettingsDS.Tables(0).Rows(0).Item(2)
        TypeofGame = MySettingsDS.Tables(0).Rows(0).Item(3)

        MyUnitsAxisDS.ReadXml(CurDir() + "\Saves\" + SavegameAxis + TextBox7.Text + ".xml")
        MyUnitsAlliesDS.ReadXml(CurDir() + "\Saves\" + SavegameAllies + TextBox7.Text + ".xml")
        Me.FillUnitsNew2()

        If TypeofGame = 2 Then 'human-AI
            Me.ChangeGameSettings()
        End If

        If turnnum > 0 Then
            TurnNr = turnnum
        Else
            TurnNr = 1
        End If
        txtTurn.Text = TurnNr.ToString()

        If country = 1 Then
            Turn = "Axis"
            Me.ShowAllUnitsAxis()
            Me.ShowCountries()
            Me.ShowHealth(1)
            lblTurn.Text = "axis"
            lblTurn.Location = New Point(lblTurn.Location.X + 5, lblTurn.Location.Y)

        Else
            Turn = "Allies"
            Me.ShowAllUnitsAllies()
            Me.ShowCountries()
            Me.ShowHealth(2)
            lblTurn.Text = "allies"

        End If

    End Sub

    Private Sub Undo()
        Dim CurMA, NewMA As Integer

        If LastPositionx <> Curx And LastPositiony <> Cury Then
            NewMA = 2
        Else
            NewMA = 1
        End If



        If LastPositionx <> Curx Or LastPositiony <> Cury Then

            If Turn = "Axis" Then
                'Move to position LastPositionx,LastPositiony
                UnitsField(LastPositionx, LastPositiony) = UnitsField(Curx, Cury)
                UnitsField(Curx, Cury) = 0

                UnitsX(CurUnit) = LastPositionx
                UnitsY(CurUnit) = LastPositiony
                MyUnitsAxisDS.Tables(0).Rows(CurUnit - 1).Item(2) = LastPositionx
                MyUnitsAxisDS.Tables(0).Rows(CurUnit - 1).Item(3) = LastPositiony

                'Remove Position Curx,Cury
                Me.RemoveUnitNew(Curx, Cury)

                Curx = LastPositionx
                Cury = LastPositiony
                CurMA = MA(CurUnit)
                CurMA = CurMA - NewMA
                If CurMA < 0 Then
                    CurMA = 0
                End If
                MA(CurUnit) = CurMA

                Me.ShowUnitAxis(Curx, Cury)

            Else
                'Move to position LastPositionx,LastPositiony
                UnitsField2(LastPositionx, LastPositiony) = UnitsField2(Curx, Cury)
                UnitsField2(Curx, Cury) = 0

                UnitsX2(CurUnit) = LastPositionx
                UnitsY2(CurUnit) = LastPositiony
                MyUnitsAlliesDS.Tables(0).Rows(CurUnit - 1).Item(2) = LastPositionx
                MyUnitsAlliesDS.Tables(0).Rows(CurUnit - 1).Item(3) = LastPositiony

                'Remove Position Curx,Cury
                Me.RemoveUnitNew(Curx, Cury)

                Curx = LastPositionx
                Cury = LastPositiony
                CurMA = MA2(CurUnit)
                If CurMA > 0 Then
                    CurMA = CurMA - 1
                    MA2(CurUnit) = CurMA
                End If

                Me.ShowUnitAllies(Curx, Cury)

            End If
            PRECT.Visible = False

        End If


    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click

        Me.Undo()

    End Sub


    Private Sub cbPosen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbAxis1.Click

        cbAxis1.Checked = True
        cbAxis2.Checked = False
        cbAxis3.Checked = False


    End Sub


    Private Sub cbBreslau_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbAxis2.Click

        cbAxis1.Checked = False
        cbAxis2.Checked = True
        cbAxis3.Checked = False



    End Sub

    Private Sub BuyAirAllies(ByVal costs As Integer, ByVal unittype As Integer)
        Dim PlacePosx, PlacePosy, pnum As Integer
        Dim BuyUnit, Town As String


        If ((Turn = "Allies" And costs > PrestigeAllies)) Then

            MsgBox("You dont have enough money to buy this unit!", MsgBoxStyle.Information)
            Return

        Else

            If cbAllies1.Checked = True Then
                Town = AlliesTown1
                MsgBox("You can not place airplanes in " + Town + "!", MsgBoxStyle.Information)
                Return

            ElseIf cbAllies2.Checked = True Then
                Town = AlliesTown2
                MsgBox("You can not place airplanes in " + Town + "!", MsgBoxStyle.Information)
                Return

            ElseIf cbAllies3.Checked = True Then
                Town = AlliesTown3
                PlacePosx = AlliesAirportPosx1
                PlacePosy = AlliesAirportPosy1
            End If

            If UnitsFieldAir2(PlacePosx, PlacePosy) = 0 Then
                PlacePosx = PlacePosx
                PlacePosy = PlacePosy
            ElseIf UnitsFieldAir2(PlacePosx - 1, PlacePosy) = 0 Then
                PlacePosx = PlacePosx - 1
                PlacePosy = PlacePosy
            ElseIf UnitsFieldAir2(PlacePosx - 1, PlacePosy - 1) = 0 Then
                PlacePosx = PlacePosx - 1
                PlacePosy = PlacePosy - 1
            ElseIf UnitsFieldAir2(PlacePosx - 1, PlacePosy + 1) = 0 Then
                PlacePosx = PlacePosx - 1
                PlacePosy = PlacePosy + 1
            ElseIf UnitsFieldAir2(PlacePosx, PlacePosy - 1) = 0 Then
                PlacePosx = PlacePosx
                PlacePosy = PlacePosy - 1
            ElseIf UnitsFieldAir2(PlacePosx, PlacePosy + 1) = 0 Then
                PlacePosx = PlacePosx
                PlacePosy = PlacePosy + 1
            Else
                MsgBox("You can not place an airplane around " + Town + " anymore!", MsgBoxStyle.Information)
                Return

            End If

            PrestigeAllies = PrestigeAllies - costs
            txtPrestige.Text = PrestigeAllies.ToString()

            pnum = MyUnitsAlliesDS.Tables(0).Rows.Count
            UnitsFieldAir2(PlacePosx, PlacePosy) = pnum

            UnitsX2(pnum) = PlacePosx
            UnitsY2(pnum) = PlacePosy
            Units2(pnum) = unittype
            If unittype = TypeFighter Then
                MA2(pnum) = MAPlane
                MAMax2(pnum) = MAPlane
                Entrenched(pnum) = 0
                Ammo2(pnum) = AmmoPlane
                Fuel2(pnum) = FuelPlane
                Trans(pnum) = TransPlane
            ElseIf unittype = TypeBomber Then
                MA2(pnum) = MAPlane
                MAMax2(pnum) = MAPlane
                Entrenched(pnum) = 0
                Ammo2(pnum) = AmmoPlane
                Fuel2(pnum) = FuelPlane
                Trans(pnum) = TransPlane

            End If
            Health2(pnum) = 10

            'Place in the Dataset MyUnitsAlliesDS !!!
            Dim myrow As DataRow = MyUnitsAlliesDS.Tables(0).NewRow()
            myrow.Item(0) = 2
            myrow.Item(1) = MyUnitsAlliesDS.Tables(0).Rows.Count + 1
            myrow.Item(2) = PlacePosx
            myrow.Item(3) = PlacePosy
            myrow.Item(4) = unittype
            myrow.Item(5) = Health2(pnum)
            myrow.Item(6) = Entrenched2(pnum)
            myrow.Item(7) = Ammo2(pnum)
            myrow.Item(8) = Fuel2(pnum)
            myrow.Item(9) = Movement2(pnum)
            myrow.Item(10) = Trans2(pnum)
            MyUnitsAlliesDS.Tables(0).Rows.Add(myrow)

            Me.ShowUnitAllies(PlacePosx, PlacePosy)
            Me.ShowHealthUnit(2, PlacePosx, PlacePosy, pnum)


        End If
    End Sub

    Private Sub BuyAxisUnit(ByVal costs As Integer, ByVal unittype As Integer, ByVal posx As Integer, ByVal posy As Integer)
        Dim PlacePosx, PlacePosy, pnum As Integer
        Dim BuyUnit, Town As String


        PrestigeAxis = PrestigeAxis - costs
        txtPrestige.Text = PrestigeAxis.ToString()

        pnum = MyUnitsAxisDS.Tables(0).Rows.Count + 1

        If unittype = TypeFighter Or unittype = TypeBomber Then
            UnitsFieldAir(posx, posy) = pnum
        Else
            UnitsField(posx, posy) = pnum
        End If
        UnitsX(pnum) = posx
        UnitsY(pnum) = posy
        Units(pnum) = unittype

        If unittype = TypeInfantery Then
            Entrenched(pnum) = 2
            Ammo(pnum) = AmmoInfanterie
            Fuel(pnum) = FuelInfanterie
            If cbTransport.Text = "No Transport" Then
                MA(pnum) = MAInfanterie
                MAMax(pnum) = MAInfanterie
                Trans(pnum) = TransInfanterie
                Mount(pnum) = 0
            ElseIf cbTransport.Text = "Transport (25)" Then
                MA(pnum) = MATransport
                MAMax(pnum) = MATransport
                Trans(pnum) = TransOpel
                Mount(pnum) = 0
            ElseIf cbTransport.Text = "Armed (60)" Then
                MA(pnum) = MATransport
                MAMax(pnum) = MATransport
                Trans(pnum) = TransArmed
                Mount(pnum) = 0
            End If

        ElseIf unittype = TypeTank Then
            MA(pnum) = MATank
            MAMax(pnum) = MATank
            Entrenched(pnum) = 0
            Ammo(pnum) = AmmoTank
            Fuel(pnum) = FuelTank
            Trans(pnum) = TransTank
            Mount(pnum) = 0

        ElseIf unittype = TypeArtillery Then
            Entrenched(pnum) = 0
            Ammo(pnum) = AmmoArtillery
            Fuel(pnum) = FuelArtillery
            If cbTransport.Text = "No Transport" Then
                MA(pnum) = MAArtillery
                MAMax(pnum) = MAArtillery
                Trans(pnum) = TransInfanterie
            ElseIf cbTransport.Text = "Transport (25)" Then
                MA(pnum) = MATransport
                MAMax(pnum) = MATransport
                Trans(pnum) = TransOpel
            ElseIf cbTransport.Text = "Armed (60)" Then
                MA(pnum) = MATransport
                MAMax(pnum) = MATransport
                Trans(pnum) = TransArmed
            End If
            Mount(pnum) = 0

        ElseIf unittype = TypeAntitank Then
            Entrenched(pnum) = 0
            Ammo(pnum) = AmmoAntitank
            Fuel(pnum) = FuelAntitank
            If cbTransport.Text = "No Transport" Then
                MA(pnum) = MAAntitank
                MAMax(pnum) = MAAntitank
                Trans(pnum) = TransInfanterie
            ElseIf cbTransport.Text = "Transport (25)" Then
                MA(pnum) = MATransport
                MAMax(pnum) = MATransport
                Trans(pnum) = TransOpel
            ElseIf cbTransport.Text = "Armed (60)" Then
                MA(pnum) = MATransport
                MAMax(pnum) = MATransport
                Trans(pnum) = TransArmed
            End If
            Mount(pnum) = 0

        ElseIf unittype = TypeFighter Then
            MA(pnum) = MAPlane
            MAMax(pnum) = MAPlane
            Entrenched(pnum) = 0
            Ammo(pnum) = AmmoPlane
            Fuel(pnum) = FuelPlane
            Trans(pnum) = TransPlane
            Mount2(pnum) = 0

        ElseIf unittype = TypeBomber Then
            MA(pnum) = MAPlane
            MAMax(pnum) = MAPlane
            Entrenched(pnum) = 0
            Ammo(pnum) = AmmoPlane
            Fuel(pnum) = FuelPlane
            Trans(pnum) = TransPlane
            Mount2(pnum) = 0

        End If
        Health(pnum) = 10
        'Movement(pnum) = pmove


        'Place in the Dataset MyUnitsAxisDS !!!
        Dim myrow As DataRow = MyUnitsAxisDS.Tables(0).NewRow()
        myrow.Item(0) = 1
        myrow.Item(1) = pnum  'MyUnitsAxisDS.Tables(0).Rows.Count + 1
        myrow.Item(2) = posx
        myrow.Item(3) = posy
        myrow.Item(4) = unittype
        myrow.Item(5) = Health(pnum)
        myrow.Item(6) = Entrenched(pnum)
        myrow.Item(7) = Ammo(pnum)
        myrow.Item(8) = Fuel(pnum)
        myrow.Item(9) = Movement(pnum)
        myrow.Item(10) = Trans(pnum)

        MyUnitsAxisDS.Tables(0).Rows.Add(myrow)

        Me.ShowUnitAxis(posx, posy)
        Me.ShowHealthUnit(1, posx, posy, pnum)


    End Sub

    Private Sub BuyAlliesUnit(ByVal costs As Integer, ByVal unittype As Integer, ByVal posx As Integer, ByVal posy As Integer)
        Dim PlacePosx, PlacePosy, pnum As Integer
        Dim BuyUnit, Town As String


        PrestigeAllies = PrestigeAllies - costs
        txtPrestige.Text = PrestigeAllies.ToString()

        pnum = MyUnitsAlliesDS.Tables(0).Rows.Count + 1
        If unittype = TypeFighter Or unittype = TypeBomber Then
            UnitsFieldAir2(posx, posy) = pnum
        Else
            UnitsField2(posx, posy) = pnum
        End If
        UnitsX2(pnum) = posx
        UnitsY2(pnum) = posy
        Units2(pnum) = unittype


        If unittype = TypeInfantery Then
            Entrenched2(pnum) = 2
            Ammo2(pnum) = AmmoInfanterie
            Fuel2(pnum) = FuelInfanterie
            If cbTransport.Text = "No Transport" Then
                MA2(pnum) = MAInfanterie
                MAMax2(pnum) = MAInfanterie
                Trans2(pnum) = TransInfanterie
                Mount2(pnum) = 0
            ElseIf cbTransport.Text = "Transport (25)" Then
                MA2(pnum) = MATransport
                MAMax2(pnum) = MATransport
                Trans2(pnum) = TransOpel
                Mount2(pnum) = 0
            ElseIf cbTransport.Text = "Armed (60)" Then
                MA2(pnum) = MATransport
                MAMax2(pnum) = MATransport
                Trans2(pnum) = TransArmed
                Mount2(pnum) = 0
            End If

        ElseIf unittype = TypeTank Then
            MA2(pnum) = MATank
            MAMax2(pnum) = MATank
            Entrenched2(pnum) = 0
            Ammo2(pnum) = AmmoTank
            Fuel2(pnum) = FuelTank
            Trans2(pnum) = TransTank
            Mount2(pnum) = 0

        ElseIf unittype = TypeArtillery Then
            Entrenched2(pnum) = 0
            Ammo2(pnum) = AmmoArtillery
            Fuel2(pnum) = FuelArtillery

            If cbTransport.Text = "No Transport" Then
                MA2(pnum) = MAArtillery
                MAMax2(pnum) = MAArtillery
                Trans2(pnum) = TransArtillery
                Mount2(pnum) = 0
            ElseIf cbTransport.Text = "Transport (25)" Then
                MA2(pnum) = MATransport
                MAMax2(pnum) = MATransport
                Trans2(pnum) = TransOpel
                Mount2(pnum) = 0
            ElseIf cbTransport.Text = "Armed (60)" Then
                MA2(pnum) = MATransport
                MAMax2(pnum) = MATransport
                Trans2(pnum) = TransArmed
                Mount2(pnum) = 0
            End If


        ElseIf unittype = TypeAntitank Then
            Entrenched2(pnum) = 0
            Ammo2(pnum) = AmmoAntitank
            Fuel2(pnum) = FuelAntitank

            If cbTransport.Text = "No Transport" Then
                MA2(pnum) = MAAntitank
                MAMax2(pnum) = MAAntitank
                Trans2(pnum) = TransAntitank
                Mount2(pnum) = 0
            ElseIf cbTransport.Text = "Transport (25)" Then
                MA2(pnum) = MATransport
                MAMax2(pnum) = MATransport
                Trans2(pnum) = TransOpel
                Mount2(pnum) = 0
            ElseIf cbTransport.Text = "Armed (60)" Then
                MA2(pnum) = MATransport
                MAMax2(pnum) = MATransport
                Trans2(pnum) = TransArmed
                Mount2(pnum) = 0
            End If

        ElseIf unittype = TypeAirdefense Then
            Entrenched2(pnum) = 0
            Ammo2(pnum) = AmmoAirdefense
            Fuel2(pnum) = FuelAirdefense

            If cbTransport.Text = "No Transport" Then
                MA2(pnum) = MAAirdefense
                MAMax2(pnum) = MAAirdefense
                Trans2(pnum) = TransAirdefense
                Mount2(pnum) = 0
            ElseIf cbTransport.Text = "Transport (25)" Then
                MA2(pnum) = MATransport
                MAMax2(pnum) = MATransport
                Trans2(pnum) = TransOpel
                Mount2(pnum) = 0
            ElseIf cbTransport.Text = "Armed (60)" Then
                MA2(pnum) = MATransport
                MAMax2(pnum) = MATransport
                Trans2(pnum) = TransArmed
                Mount2(pnum) = 0
            End If

        ElseIf unittype = TypeFighter Then
            MA2(pnum) = MAPlane
            MAMax2(pnum) = MAPlane
            Entrenched(pnum) = 0
            Ammo2(pnum) = AmmoPlane
            Fuel2(pnum) = FuelPlane
            Trans(pnum) = TransPlane
        ElseIf unittype = TypeBomber Then
            MA2(pnum) = MAPlane
            MAMax2(pnum) = MAPlane
            Entrenched(pnum) = 0
            Ammo2(pnum) = AmmoPlane
            Fuel2(pnum) = FuelPlane
            Trans(pnum) = TransPlane

        End If
        Health2(pnum) = 10

        'Place in the Dataset MyUnitsAxisDS !!!

        Dim myrow As DataRow = MyUnitsAlliesDS.Tables(0).NewRow()

        myrow.Item(0) = 2
        myrow.Item(1) = pnum 'MyUnitsAlliesDS.Tables(0).Rows.Count + 1
        myrow.Item(2) = posx
        myrow.Item(3) = posy
        myrow.Item(4) = unittype
        myrow.Item(5) = Health2(pnum)
        myrow.Item(6) = Entrenched2(pnum)
        myrow.Item(7) = Ammo2(pnum)
        myrow.Item(8) = Fuel2(pnum)
        myrow.Item(9) = Movement2(pnum)
        myrow.Item(10) = Trans2(pnum)

        MyUnitsAlliesDS.Tables(0).Rows.Add(myrow)

        Me.ShowUnitAllies(posx, posy)
        Me.ShowHealthUnit(2, posx, posy, pnum)

    End Sub
    Private Sub BuyGroundAllies(ByVal costs As Integer, ByVal unittype As Integer)
        Dim PlacePosx, PlacePosy, pnum As Integer
        Dim BuyUnit, Town As String

        If ((Turn = "Allies" And costs > PrestigeAllies)) Then

            MsgBox("You dont have enough money to buy this unit!", MsgBoxStyle.Information)
            Return

        Else

            If cbAllies1.Checked = True Then
                Town = AlliesTown1
                PlacePosx = AlliesPosx1
                PlacePosy = AlliesPosy1
            ElseIf cbAllies2.Checked = True Then
                Town = AlliesTown2
                PlacePosx = AlliesPosx2
                PlacePosy = AlliesPosy2
            ElseIf cbAllies3.Checked = True Then
                Town = AlliesTown3
                PlacePosx = AlliesPosx3
                PlacePosy = AlliesPosy3
            End If


            If UnitsField2(PlacePosx, PlacePosy) = 0 Then
                PlacePosx = PlacePosx
                PlacePosy = PlacePosy
            ElseIf UnitsField2(PlacePosx - 1, PlacePosy - 1) = 0 Then
                PlacePosx = PlacePosx - 1
                PlacePosy = PlacePosy - 1
            ElseIf UnitsField2(PlacePosx - 1, PlacePosy) = 0 Then
                PlacePosx = PlacePosx - 1
                PlacePosy = PlacePosy
            ElseIf UnitsField2(PlacePosx - 1, PlacePosy + 1) = 0 Then
                PlacePosx = PlacePosx - 1
                PlacePosy = PlacePosy + 1
            ElseIf UnitsField2(PlacePosx, PlacePosy - 1) = 0 Then
                PlacePosx = PlacePosx
                PlacePosy = PlacePosy - 1
            ElseIf UnitsField2(PlacePosx, PlacePosy + 1) = 0 Then
                PlacePosx = PlacePosx
                PlacePosy = PlacePosy + 1
            ElseIf UnitsField2(PlacePosx + 1, PlacePosy - 1) = 0 Then
                PlacePosx = PlacePosx + 1
                PlacePosy = PlacePosy - 1
            ElseIf UnitsField2(PlacePosx + 1, PlacePosy) = 0 Then
                PlacePosx = PlacePosx + 1
                PlacePosy = PlacePosy
            ElseIf UnitsField2(PlacePosx + 1, PlacePosy + 1) = 0 Then
                PlacePosx = PlacePosx + 1
                PlacePosy = PlacePosy + 1
            Else
                MsgBox("You can not place a unit around " + Town + " anymore!", MsgBoxStyle.Information)
                Return

            End If
        End If

        Me.BuyAlliesUnit(costs, unittype, PlacePosx, PlacePosy)


    End Sub

    Private Sub BuyAirAxis(ByVal costs As Integer, ByVal unittype As Integer)
        Dim PlacePosx, PlacePosy, pnum As Integer
        Dim BuyUnit, Town As String


        If ((Turn = "Axis" And costs > PrestigeAxis)) Then
            MsgBox("You dont have enough money to buy this unit!", MsgBoxStyle.Information)
            Return

        Else

            If cbAxis1.Checked = True Then
                Town = AxisTown1
                PlacePosx = AxisAirportPosx1
                PlacePosy = AxisAirportPosy1
            Else
                Town = AxisTown2
                PlacePosx = AxisAirportPosx2
                PlacePosy = AxisAirportPosy2
            End If

            If UnitsFieldAir(PlacePosx, PlacePosy) = 0 Then
                PlacePosx = PlacePosx
                PlacePosy = PlacePosy
            ElseIf UnitsFieldAir(PlacePosx, PlacePosy + 1) = 0 Then
                PlacePosx = PlacePosx
                PlacePosy = PlacePosy + 1
            ElseIf UnitsFieldAir(PlacePosx, PlacePosy - 1) = 0 And Town <> "Breslau" Then
                PlacePosx = PlacePosx
                PlacePosy = PlacePosy - 1
            ElseIf UnitsFieldAir(PlacePosx + 1, PlacePosy + 1) = 0 Then
                PlacePosx = PlacePosx + 1
                PlacePosy = PlacePosy + 1
            ElseIf UnitsFieldAir(PlacePosx + 1, PlacePosy) = 0 Then
                PlacePosx = PlacePosx + 1
                PlacePosy = PlacePosy
            ElseIf UnitsFieldAir(PlacePosx + 1, PlacePosy - 1) = 0 And Town <> "Breslau" Then
                PlacePosx = PlacePosx + 1
                PlacePosy = PlacePosy - 1
            Else
                MsgBox("You can not place an airplane around " + Town + " anymore!", MsgBoxStyle.Information)
                Return

            End If

            Me.BuyAxisUnit(costs, unittype, PlacePosx, PlacePosy)

        End If

    End Sub

    Private Sub PlaceAllies(ByVal unittype As Integer)
        Dim PlacePosx, PlacePosy, pnum As Integer
        Dim BuyUnit, Town As String

        PlacePosx = TextAlliesx
        PlacePosy = TextAlliesy

        pnum = MyUnitsAlliesDS.Tables(0).Rows.Count

        If unittype = TypeFighter Or unittype = TypeBomber Then
            UnitsFieldAir2(PlacePosx, PlacePosy) = pnum
        Else
            UnitsField2(PlacePosx, PlacePosy) = pnum
        End If
        UnitsX2(pnum) = PlacePosx
        UnitsY2(pnum) = PlacePosy
        Units2(pnum) = unittype

        If unittype = TypeInfantery Then
            MA2(pnum) = 0
            MAMax2(pnum) = MAInfanterie
            Entrenched2(pnum) = txtEnt2.Text.ToString()
            Ammo2(pnum) = AmmoInfanterie
            Fuel2(pnum) = FuelInfanterie
            Trans2(pnum) = TransInfanterie
            Mount2(pnum) = 0

        ElseIf unittype = TypeTank Then
            MA2(pnum) = 0
            MAMax2(pnum) = MATank
            Entrenched2(pnum) = txtEnt2.Text.ToString()
            Ammo2(pnum) = AmmoTank
            Fuel2(pnum) = FuelTank
            Trans2(pnum) = TransTank
            Mount2(pnum) = 0

        ElseIf unittype = TypeArtillery Then
            MA2(pnum) = 0
            MAMax2(pnum) = MAArtillery
            Entrenched2(pnum) = txtEnt2.Text.ToString()
            Ammo2(pnum) = AmmoArtillery
            Fuel2(pnum) = FuelArtillery
            Trans2(pnum) = TransArtillery
            Mount2(pnum) = 0

        ElseIf unittype = TypeAntitank Then
            MA2(pnum) = 0
            MAMax2(pnum) = MAAntitank
            Entrenched2(pnum) = txtEnt2.Text.ToString()
            Ammo2(pnum) = AmmoAntitank
            Fuel2(pnum) = FuelAntitank
            Trans2(pnum) = TransAntitank
            Mount2(pnum) = 0
        ElseIf unittype = TypeFighter Then
            MA2(pnum) = 0
            MAMax2(pnum) = MAPlane
            Entrenched(pnum) = 0
            Ammo2(pnum) = AmmoPlane
            Fuel2(pnum) = FuelPlane
            Trans(pnum) = TransPlane
        ElseIf unittype = TypeBomber Then
            MA2(pnum) = 0
            MAMax2(pnum) = MAPlane
            Entrenched(pnum) = 0
            Ammo2(pnum) = AmmoPlane
            Fuel2(pnum) = FuelPlane
            Trans(pnum) = TransPlane


        End If
        Health2(pnum) = 10

        'Place in the Dataset MyUnitsAxisDS !!!

        Dim myrow As DataRow = MyUnitsAlliesDS.Tables(0).NewRow()

        myrow.Item(0) = 2
        myrow.Item(1) = MyUnitsAlliesDS.Tables(0).Rows.Count + 1
        myrow.Item(2) = PlacePosx
        myrow.Item(3) = PlacePosy
        myrow.Item(4) = unittype
        myrow.Item(5) = Health2(pnum)
        myrow.Item(6) = Entrenched2(pnum)
        myrow.Item(7) = Ammo2(pnum)
        myrow.Item(8) = Fuel2(pnum)
        myrow.Item(9) = Movement2(pnum)
        myrow.Item(10) = Trans2(pnum)

        MyUnitsAlliesDS.Tables(0).Rows.Add(myrow)

        Me.ShowUnitAllies(PlacePosx, PlacePosy)
        Me.ShowHealthUnit(2, PlacePosx, PlacePosy, pnum)


    End Sub

    Private Sub PlaceAxis(ByVal unittype As Integer)
        Dim PlacePosx, PlacePosy, pnum As Integer
        Dim BuyUnit, Town As String

        PlacePosx = TextAxisx
        PlacePosy = TextAxisy

        pnum = MyUnitsAxisDS.Tables(0).Rows.Count

        If unittype = TypeFighter Or unittype = TypeBomber Then
            UnitsFieldAir(PlacePosx, PlacePosy) = pnum
        Else
            UnitsField(PlacePosx, PlacePosy) = pnum
        End If
        UnitsX(pnum) = PlacePosx
        UnitsY(pnum) = PlacePosy
        Units(pnum) = unittype

        If unittype = TypeInfantery Then
            MA(pnum) = 0
            MAMax(pnum) = MAInfanterie
            Entrenched(pnum) = txtEnt.Text.ToString()
            Ammo(pnum) = AmmoInfanterie
            Fuel(pnum) = FuelInfanterie
            If cbTransport.Text = "No Transport" Then
                Trans(pnum) = TransInfanterie
                Mount(pnum) = 0
            ElseIf cbTransport.Text = "Transport (25)" Then
                Trans(pnum) = TransOpel
                Mount(pnum) = 0
            ElseIf cbTransport.Text = "Armed (60)" Then
                Trans(pnum) = TransArmed
                Mount(pnum) = 0
            End If

        ElseIf unittype = TypeTank Then
            MA(pnum) = 0
            MAMax(pnum) = MATank
            Entrenched(pnum) = txtEnt.Text.ToString()
            Ammo(pnum) = AmmoTank
            Fuel(pnum) = FuelTank
            Trans(pnum) = TransTank
            Mount(pnum) = 0

        ElseIf unittype = TypeArtillery Then
            MA(pnum) = 0
            MAMax(pnum) = MAArtillery
            Entrenched(pnum) = txtEnt.Text.ToString()
            Ammo(pnum) = AmmoArtillery
            Fuel(pnum) = FuelArtillery
            If cbTransport.Text = "No Transport" Then
                Trans(pnum) = TransInfanterie
            ElseIf cbTransport.Text = "Transport (25)" Then
                Trans(pnum) = TransOpel
            ElseIf cbTransport.Text = "Armed (60)" Then
                Trans(pnum) = TransArmed
            End If
            Mount(pnum) = 0

        ElseIf unittype = TypeAntitank Then
            MA(pnum) = 0
            MAMax(pnum) = MAAntitank
            Entrenched(pnum) = txtEnt.Text.ToString()
            Ammo(pnum) = AmmoAntitank
            Fuel(pnum) = FuelAntitank
            If cbTransport.Text = "No Transport" Then
                Trans(pnum) = TransInfanterie
            ElseIf cbTransport.Text = "Transport (25)" Then
                Trans(pnum) = TransOpel
            ElseIf cbTransport.Text = "Armed (60)" Then
                Trans(pnum) = TransArmed
            End If
            Mount(pnum) = 0

        ElseIf unittype = TypeFighter Then
            MA(pnum) = 0
            MAMax(pnum) = MAPlane
            Entrenched(pnum) = 0
            Ammo(pnum) = AmmoPlane
            Fuel(pnum) = FuelPlane
            Trans(pnum) = TransPlane
            Mount2(pnum) = 0

        ElseIf unittype = TypeBomber Then
            MA(pnum) = 0
            MAMax(pnum) = MAPlane
            Entrenched(pnum) = 0
            Ammo(pnum) = AmmoPlane
            Fuel(pnum) = FuelPlane
            Trans(pnum) = TransPlane
            Mount2(pnum) = 0

        End If
        Health(pnum) = 10
        'Movement(pnum) = pmove


        'Place in the Dataset MyUnitsAxisDS !!!
        Dim myrow As DataRow = MyUnitsAxisDS.Tables(0).NewRow()
        myrow.Item(0) = 1
        myrow.Item(1) = MyUnitsAxisDS.Tables(0).Rows.Count + 1
        myrow.Item(2) = PlacePosx
        myrow.Item(3) = PlacePosy
        myrow.Item(4) = unittype
        myrow.Item(5) = Health(pnum)
        myrow.Item(6) = Entrenched(pnum)
        myrow.Item(7) = Ammo(pnum)
        myrow.Item(8) = Fuel(pnum)
        myrow.Item(9) = Movement(pnum)
        myrow.Item(10) = Trans(pnum)

        MyUnitsAxisDS.Tables(0).Rows.Add(myrow)

        Me.ShowUnitAxis(PlacePosx, PlacePosy)
        Me.ShowHealthUnit(1, PlacePosx, PlacePosy, pnum)



    End Sub
    Private Sub BuyGroundAxis(ByVal costs As Integer, ByVal unittype As Integer)
        Dim PlacePosx, PlacePosy, pnum As Integer
        Dim BuyUnit, Town As String

        If ((Turn = "Axis" And costs > PrestigeAxis)) Then
            MsgBox("You dont have enough money to buy this unit!", MsgBoxStyle.Information)
            Return
        Else 'buy Unit

            If cbAxis1.Checked = True Then
                Town = AxisTown1
                PlacePosx = AxisPosx1
                PlacePosy = AxisPosy1
            Else
                Town = AxisTown2
                PlacePosx = AxisPosx2
                PlacePosy = AxisPosy2
            End If

            If UnitsField(PlacePosx, PlacePosy) = 0 Then
                PlacePosx = PlacePosx
                PlacePosy = PlacePosy
            ElseIf UnitsField(PlacePosx, PlacePosy + 1) = 0 Then
                PlacePosx = PlacePosx
                PlacePosy = PlacePosy + 1

            ElseIf UnitsField(PlacePosx + 1, PlacePosy + 1) = 0 Then
                PlacePosx = PlacePosx + 1
                PlacePosy = PlacePosy + 1

            ElseIf UnitsField(PlacePosx + 1, PlacePosy) = 0 Then
                PlacePosx = PlacePosx + 1
                PlacePosy = PlacePosy

            ElseIf UnitsField(PlacePosx + 1, PlacePosy - 1) = 0 Then
                PlacePosx = PlacePosx + 1
                PlacePosy = PlacePosy - 1

            ElseIf UnitsField(PlacePosx, PlacePosy - 1) = 0 Then
                PlacePosx = PlacePosx
                PlacePosy = PlacePosy - 1

            Else
                MsgBox("You can not place a unit around " + Town + " anymore!", MsgBoxStyle.Information)
                Return

            End If

            Me.BuyAxisUnit(costs, unittype, PlacePosx, PlacePosy)

        End If

    End Sub


    Private Sub Button8_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim thecosts, PlacePosx, PlacePosy, pnum, theunittype As Integer
        Dim BuyUnit, Town As String

        BuyUnit = cbUnits.Text
        Select Case BuyUnit

            Case "Infantery (100)"
                thecosts = 100
                theunittype = TypeInfantery
                If cbTransport.Text = "Transport (25)" Then
                    thecosts = thecosts + 25
                ElseIf cbTransport.Text = "Armed (60)" Then
                    thecosts = thecosts + 60
                End If

            Case "Tank (200)"
                thecosts = 200
                theunittype = TypeTank

            Case "Artillery (200)"
                thecosts = 200
                theunittype = TypeArtillery
                If cbTransport.Text = "Transport (25)" Then
                    thecosts = thecosts + 25
                ElseIf cbTransport.Text = "Armed (60)" Then
                    thecosts = thecosts + 60
                End If

            Case "Anti-tank (200)"
                thecosts = 200
                theunittype = TypeAntitank

            Case "Air-Defense (200)"
                thecosts = 200
                theunittype = TypeAirdefense

            Case "Fighter (300)"
                thecosts = 300
                theunittype = TypeFighter

            Case "Bomber (300)"
                thecosts = 300
                theunittype = TypeBomber

        End Select

        If Turn = "Axis" Then
            If theunittype = TypeFighter Or theunittype = TypeBomber Then
                Me.BuyAirAxis(thecosts, theunittype)
            Else
                Me.BuyGroundAxis(thecosts, theunittype)
            End If
        Else
            If theunittype = TypeFighter Or theunittype = TypeBomber Then
                Me.BuyAirAllies(thecosts, theunittype)
            Else
                Me.BuyGroundAllies(thecosts, theunittype)

            End If

        End If

    End Sub


    Private Sub cbLodz_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbAllies1.Click

        cbAllies1.Checked = True
        cbAllies2.Checked = False
        cbAllies3.Checked = False

    End Sub


    Private Sub cbWarsaw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbAllies2.Click

        cbAllies1.Checked = False
        cbAllies2.Checked = True
        cbAllies3.Checked = False

    End Sub

    Private Sub cbSiedice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbAllies3.Click

        cbAllies1.Checked = False
        cbAllies2.Checked = False
        cbAllies3.Checked = True

    End Sub

    Private Sub Button10_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

        MyUnitsAxisDS.Reset()
        MyUnitsAlliesDS.Reset()
        MyUnitsAxisDS.ReadXml(CurDir() + "\Saves\S01_Poland_UnitsAxis" + TextBox7.Text + ".xml")
        MyUnitsAlliesDS.ReadXml(CurDir() + "\Saves\S01_Poland_UnitsAllies" + TextBox7.Text + ".xml")

        Me.FillUnitsNew()

        Turn = "Allies"
        PRECT.Visible = False
        For i = 0 To 99
            MA2(i) = 0
        Next
        Me.ShowAllUnitsAllies()
        Me.ShowCountries()
        Me.ShowHealth(2)

    End Sub

    Private Sub BMount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BMount.Click
        Dim transport, newtrans, unit, thecosts As Integer

        If cbTransport.Text = "Armed (60)" Then
            thecosts = 60
            newtrans = 2
        Else
            thecosts = 25
            newtrans = 1
        End If

        If Turn = "Axis" Then
            transport = Trans(CurUnit)
            If transport = 0 Then
                If thecosts < PrestigeAxis Then
                    PrestigeAxis = PrestigeAxis - thecosts
                    txtPrestige.Text = PrestigeAxis.ToString()
                    Trans(CurUnit) = newtrans
                    Mount(CurUnit) = newtrans
                    Me.RemoveUnitNew(Curx, Cury)
                    Me.ShowUnitAxis(Curx, Cury)
                    Me.ShowHealthUnit(1, Curx, Cury, CurUnit)
                Else
                    MsgBox("You dont have enough money to buy this transport!")
                    Return
                End If
            Else
                Mount(CurUnit) = newtrans
                Me.RemoveUnitNew(Curx, Cury)
                Me.ShowUnitAxis(Curx, Cury)
                Me.ShowHealthUnit(1, Curx, Cury, CurUnit)

            End If
        Else 'Allies
            transport = Trans2(CurUnit)
            If transport = 0 Then
                If thecosts < PrestigeAllies Then
                    PrestigeAllies = PrestigeAllies - thecosts
                    txtPrestige.Text = PrestigeAllies.ToString()
                    Trans2(CurUnit) = newtrans
                    Mount2(CurUnit) = newtrans
                    Me.RemoveUnitNew(Curx, Cury)
                    Me.ShowUnitAllies(Curx, Cury)
                    Me.ShowHealthUnit(2, Curx, Cury, CurUnit)

                Else
                    MsgBox("You dont have enough money to buy this transport!")
                    Return
                End If
            Else
                Mount2(CurUnit) = newtrans
                Me.RemoveUnitNew(Curx, Cury)
                Me.ShowUnitAllies(Curx, Cury)
                Me.ShowHealthUnit(2, Curx, Cury, CurUnit)

            End If
        End If
        BMount.Visible = False
        BDismount.Visible = False

    End Sub

    Private Sub BDismount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BDismount.Click
        Dim transport, newtrans, unit As Integer


        If Turn = "Axis" Then
            transport = Trans(CurUnit)
        Else
            transport = Trans2(CurUnit)
        End If
        newtrans = 0

        If transport = 1 Or transport = 2 Then

            If Turn = "Axis" Then
                Mount(CurUnit) = newtrans
                Me.RemoveUnitNew(Curx, Cury)
                Me.ShowUnitAxis(Curx, Cury)
                Me.ShowHealthUnit(1, Curx, Cury, CurUnit)
            Else
                Mount2(CurUnit) = newtrans
                Me.RemoveUnitNew(Curx, Cury)
                Me.ShowUnitAllies(Curx, Cury)
                Me.ShowHealthUnit(2, Curx, Cury, CurUnit)

            End If

        End If

        BMount.Visible = False
        BDismount.Visible = False


    End Sub

    Private Sub PerformAttack(ByVal attack As Integer, ByVal defend As Integer, ByRef h As Integer)
        Dim attack1, defense1 As Integer

        If defend > attack + 5 Then
            attack1 = Rand(0, 1)

        ElseIf defend > attack + 4 Then
            attack1 = Rand(0, 1)

        ElseIf defend > attack + 3 Then
            attack1 = Rand(0, 1)

        ElseIf defend > attack + 2 Then
            attack1 = Rand(0, 1)

        ElseIf defend > attack + 1 Then
            attack1 = Rand(0, 2)

        ElseIf defend > attack Then
            attack1 = Rand(0, 2)

        ElseIf defend > attack - 1 Then
            attack1 = Rand(1, 2)

        ElseIf defend > attack - 2 Then
            attack1 = Rand(1, 3)

        ElseIf defend > attack - 3 Then
            attack1 = Rand(1, 4)

        ElseIf defend > attack - 4 Then
            attack1 = Rand(2, 5)

        ElseIf defend > attack - 5 Then
            attack1 = Rand(2, 6)

        ElseIf defend > attack - 6 Then
            attack1 = Rand(3, 7)

        ElseIf defend > attack - 7 Then
            attack1 = Rand(3, 8)

        ElseIf defend > attack - 8 Then
            attack1 = Rand(4, 8)
        Else
            attack1 = Rand(5, 8)
        End If
        If h - attack1 > 0 Then
            h = h - attack1
        Else
            h = 0
        End If


    End Sub

    Private Sub PerformAttackV01(ByVal attack As Integer, ByVal defend As Integer, ByRef h As Integer)
        Dim attack1, defense1 As Integer

        attack1 = Rand(1, attack)
        defense1 = Rand(1, defend)
        If attack1 >= defense1 Then
            h = h + defense1 - attack1
        End If

    End Sub

    Private Sub BattleResolution(ByVal cid As Integer, ByVal unitnr1 As Integer, ByVal unitnr2 As Integer, ByRef h1 As Integer, ByRef h2 As Integer, ByVal a1 As Integer, ByVal a2 As Integer)
        Dim healthunit1, healthunit2, ent1, ent2 As Integer
        Dim myaxisunit, myalliesunit As Integer
        Dim sa1, ha1, aa1, gd1, ad1, sa02, ha02, aa02, gd02, ad02 As Integer
        Dim attack1, defense1, attack2, defense2 As Integer
        Dim AttackTotal, DefendTotal As Integer
        Dim surfaceAxis, surfaceAllies As Integer
        Dim AxisPosx, AxisPosy, AlliesPosx, AlliesPosy As Integer
        Dim AttackBonusAxis, DefendBonusAxis, AttackBonusAllies, DefendBonusAllies As Integer

        myaxisunit = Units(unitnr1)
        myalliesunit = Units2(unitnr2)
        healthunit1 = Health(unitnr1)
        healthunit2 = Health2(unitnr2)
        ent1 = Entrenched(unitnr1)
        ent2 = Entrenched2(unitnr2)

        h1 = healthunit1
        h2 = healthunit2

        AxisPosx = UnitsX(unitnr1)
        AxisPosy = UnitsY(unitnr1)
        AlliesPosx = UnitsX2(unitnr2)
        AlliesPosy = UnitsY2(unitnr2)

        surfaceAxis = FieldSurface(AxisPosy, AxisPosx)
        surfaceAllies = FieldSurface(AlliesPosy, AlliesPosx)

        If surfaceAxis = 1 Then 'river
            AttackBonusAxis = AttackBonusRiver
            DefendBonusAxis = DefendBonusRiver
        ElseIf surfaceAxis = 2 Or surfaceAxis = 3 Then
            DefendBonusAxis = DefendBonusRough
        Else
            AttackBonusAxis = 0
            DefendBonusAxis = 0
        End If
        If surfaceAllies = 1 Then 'river
            AttackBonusAllies = AttackBonusRiver
            DefendBonusAllies = DefendBonusRiver
        ElseIf surfaceAllies = 2 Or surfaceAllies = 3 Then
            DefendBonusAllies = DefendBonusRough
        Else
            AttackBonusAllies = 0
            DefendBonusAllies = 0
        End If

        If a1 > 0 Then
            If myaxisunit = TypeInfantery And myalliesunit = TypeInfantery Then 'inf x inf
                AttackTotal = SAInfanteryAxis + AttackBonusAxis
                DefendTotal = GDInfanteryAllies + DefendBonusAllies + ent2
            ElseIf myaxisunit = TypeInfantery And myalliesunit = TypeTank Then 'inf x tank
                AttackTotal = HAInfanteryAxis + AttackBonusAxis
                DefendTotal = GDTankAllies + DefendBonusAllies + ent2
            ElseIf myaxisunit = TypeInfantery And myalliesunit = TypeArtillery Then 'inf x art
                AttackTotal = HAInfanteryAxis + AttackBonusAxis
                DefendTotal = GDArtilleryAllies + DefendBonusAllies + ent2
            ElseIf myaxisunit = TypeInfantery And myalliesunit = TypeAntitank Then
                AttackTotal = HAInfanteryAxis + AttackBonusAxis
                DefendTotal = GDAntitankAllies + DefendBonusAllies + ent2
            ElseIf myaxisunit = TypeInfantery And myalliesunit = TypeFighter Then
                AttackTotal = AAInfanteryAxis
                DefendTotal = GDFighterAllies
            ElseIf myaxisunit = TypeInfantery And myalliesunit = TypeBomber Then
                AttackTotal = AAInfanteryAxis
                DefendTotal = GDBomberAllies
            ElseIf myaxisunit = TypeTank And myalliesunit = TypeInfantery Then
                AttackTotal = SATankAxis + AttackBonusAxis
                DefendTotal = GDInfanteryAllies + DefendBonusAllies + ent2
            ElseIf myaxisunit = TypeTank And myalliesunit = TypeTank Then
                AttackTotal = HATankAxis + AttackBonusAxis
                DefendTotal = GDTankAllies + DefendBonusAllies + ent2
            ElseIf myaxisunit = TypeTank And myalliesunit = TypeArtillery Then
                AttackTotal = HATankAxis + AttackBonusAxis
                DefendTotal = GDArtilleryAllies + DefendBonusAllies + ent2
            ElseIf myaxisunit = TypeTank And myalliesunit = TypeAntitank Then
                AttackTotal = HATankAxis + AttackBonusAxis
                DefendTotal = GDAntitankAllies + DefendBonusAllies + ent2
            ElseIf myaxisunit = TypeTank And myalliesunit = TypeFighter Then
                AttackTotal = AATankAxis
                DefendTotal = GDFighterAllies
            ElseIf myaxisunit = TypeTank And myalliesunit = TypeBomber Then
                AttackTotal = AATankAxis
                DefendTotal = GDBomberAllies
            ElseIf myaxisunit = TypeArtillery And myalliesunit = TypeInfantery Then
                AttackTotal = SAArtilleryAxis + AttackBonusAxis
                DefendTotal = GDInfanteryAllies + DefendBonusAllies + ent2
            ElseIf myaxisunit = TypeArtillery And myalliesunit = TypeTank Then
                AttackTotal = HAArtilleryAxis + AttackBonusAxis
                DefendTotal = GDTankAllies + DefendBonusAllies + ent2
            ElseIf myaxisunit = TypeArtillery And myalliesunit = TypeArtillery Then
                AttackTotal = HAArtilleryAxis + AttackBonusAxis
                DefendTotal = GDArtilleryAllies + DefendBonusAllies + ent2
            ElseIf myaxisunit = TypeArtillery And myalliesunit = TypeAntitank Then
                AttackTotal = HAArtilleryAxis + AttackBonusAxis
                DefendTotal = GDAntitankAllies + DefendBonusAllies + ent2
            ElseIf myaxisunit = TypeArtillery And myalliesunit = TypeFighter Then
                AttackTotal = AAArtilleryAxis
                DefendTotal = GDFighterAllies
            ElseIf myaxisunit = TypeArtillery And myalliesunit = TypeBomber Then
                AttackTotal = AAArtilleryAxis
                DefendTotal = GDBomberAllies
            ElseIf myaxisunit = TypeAntitank And myalliesunit = TypeInfantery Then
                AttackTotal = SAAntitankAxis + AttackBonusAxis
                DefendTotal = GDInfanteryAllies + DefendBonusAllies + ent2
            ElseIf myaxisunit = TypeAntitank And myalliesunit = TypeTank Then
                AttackTotal = HAAntitankAxis + AttackBonusAxis
                DefendTotal = GDTankAllies + DefendBonusAllies + ent2
            ElseIf myaxisunit = TypeAntitank And myalliesunit = TypeArtillery Then
                AttackTotal = HAAntitankAxis + AttackBonusAxis
                DefendTotal = GDArtilleryAllies + DefendBonusAllies + ent2
            ElseIf myaxisunit = TypeAntitank And myalliesunit = TypeAntitank Then
                AttackTotal = HAAntitankAxis + AttackBonusAxis
                DefendTotal = GDAntitankAllies + DefendBonusAllies + ent2
            ElseIf myaxisunit = TypeAntitank And myalliesunit = TypeFighter Then
                AttackTotal = AAAntitankAxis
                DefendTotal = GDFighterAllies
            ElseIf myaxisunit = TypeAntitank And myalliesunit = TypeBomber Then
                AttackTotal = AAAntitankAxis
                DefendTotal = GDBomberAllies
            ElseIf myaxisunit = TypeFighter And myalliesunit = TypeInfantery Then
                AttackTotal = SAFighterAxis
                DefendTotal = ADInfanteryAllies
            ElseIf myaxisunit = TypeFighter And myalliesunit = TypeTank Then
                AttackTotal = HAFighterAxis
                DefendTotal = ADTankAllies
            ElseIf myaxisunit = TypeFighter And myalliesunit = TypeArtillery Then
                AttackTotal = HAFighterAxis
                DefendTotal = ADArtilleryAllies
            ElseIf myaxisunit = TypeFighter And myalliesunit = TypeAntitank Then
                AttackTotal = HAFighterAxis
                DefendTotal = ADAntitankAllies
            ElseIf myaxisunit = TypeFighter And myalliesunit = TypeFighter Then
                AttackTotal = AAFighterAxis
                DefendTotal = ADFighterAllies
            ElseIf myaxisunit = TypeFighter And myalliesunit = TypeBomber Then
                AttackTotal = AAFighterAxis
                DefendTotal = ADBomberAllies
            ElseIf myaxisunit = TypeBomber And myalliesunit = TypeInfantery Then
                AttackTotal = SABomberAxis
                DefendTotal = ADInfanteryAllies
            ElseIf myaxisunit = TypeBomber And myalliesunit = TypeTank Then
                AttackTotal = HABomberAxis
                DefendTotal = ADTankAllies
            ElseIf myaxisunit = TypeBomber And myalliesunit = TypeArtillery Then
                AttackTotal = HABomberAxis
                DefendTotal = ADArtilleryAllies
            ElseIf myaxisunit = TypeBomber And myalliesunit = TypeAntitank Then
                AttackTotal = HABomberAxis
                DefendTotal = ADAntitankAllies
            ElseIf myaxisunit = TypeBomber And myalliesunit = TypeFighter Then
                AttackTotal = AABomberAxis
                DefendTotal = ADFighterAllies
            ElseIf myaxisunit = TypeBomber And myalliesunit = TypeBomber Then
                AttackTotal = AABomberAxis
                DefendTotal = ADBomberAllies
            End If
            If AttackTotal > 0 Then
                Me.PerformAttack(AttackTotal, DefendTotal, h2)
            End If

        End If
        If a2 > 0 Then
            If myaxisunit = TypeInfantery And myalliesunit = TypeInfantery Then 'inf x inf
                AttackTotal = SAInfanteryAllies + AttackBonusAllies
                DefendTotal = GDInfanteryAxis + DefendBonusAxis + ent1
            ElseIf myaxisunit = TypeInfantery And myalliesunit = TypeTank Then 'inf x tank
                AttackTotal = SATankAllies + AttackBonusAllies
                DefendTotal = GDInfanteryAxis + DefendBonusAxis + ent1
            ElseIf myaxisunit = TypeInfantery And myalliesunit = TypeArtillery Then 'inf x art
                AttackTotal = SAArtilleryAllies + AttackBonusAllies
                DefendTotal = GDInfanteryAxis + DefendBonusAxis + ent1
            ElseIf myaxisunit = TypeInfantery And myalliesunit = TypeAntitank Then
                AttackTotal = SAAntitankAllies + AttackBonusAllies
                DefendTotal = GDInfanteryAxis + DefendBonusAxis + ent1
            ElseIf myaxisunit = TypeInfantery And myalliesunit = TypeFighter Then
                AttackTotal = SAFighterAllies
                DefendTotal = ADInfanteryAxis
            ElseIf myaxisunit = TypeInfantery And myalliesunit = TypeBomber Then
                AttackTotal = SABomberAllies
                DefendTotal = ADInfanteryAxis
            ElseIf myaxisunit = TypeTank And myalliesunit = TypeInfantery Then
                AttackTotal = HAInfanteryAllies + AttackBonusAllies
                DefendTotal = GDTankAxis + DefendBonusAxis + ent1
            ElseIf myaxisunit = TypeTank And myalliesunit = TypeTank Then
                AttackTotal = HATankAllies + AttackBonusAllies
                DefendTotal = GDTankAxis + DefendBonusAxis + ent1
            ElseIf myaxisunit = TypeTank And myalliesunit = TypeArtillery Then
                AttackTotal = HAArtilleryAllies + AttackBonusAllies
                DefendTotal = GDTankAxis + DefendBonusAxis + ent1
            ElseIf myaxisunit = TypeTank And myalliesunit = TypeAntitank Then
                AttackTotal = HAAntitankAllies + AttackBonusAllies
                DefendTotal = GDTankAxis + DefendBonusAxis + ent1
            ElseIf myaxisunit = TypeTank And myalliesunit = TypeFighter Then
                AttackTotal = HAFighterAllies
                DefendTotal = ADTankAxis
            ElseIf myaxisunit = TypeTank And myalliesunit = TypeBomber Then
                AttackTotal = HABomberAllies
                DefendTotal = ADTankAxis
            ElseIf myaxisunit = TypeArtillery And myalliesunit = TypeInfantery Then
                AttackTotal = HAInfanteryAllies + AttackBonusAllies
                DefendTotal = GDArtilleryAxis + DefendBonusAxis + ent1
            ElseIf myaxisunit = TypeArtillery And myalliesunit = TypeTank Then
                AttackTotal = HATankAllies + AttackBonusAllies
                DefendTotal = GDArtilleryAxis + DefendBonusAxis + ent1
            ElseIf myaxisunit = TypeArtillery And myalliesunit = TypeArtillery Then
                AttackTotal = HAArtilleryAllies + AttackBonusAllies
                DefendTotal = GDArtilleryAxis + DefendBonusAxis + ent1
            ElseIf myaxisunit = TypeArtillery And myalliesunit = TypeAntitank Then
                AttackTotal = HAAntitankAllies + AttackBonusAllies
                DefendTotal = GDArtilleryAxis + DefendBonusAxis + ent1
            ElseIf myaxisunit = TypeArtillery And myalliesunit = TypeFighter Then
                AttackTotal = HAFighterAllies
                DefendTotal = ADArtilleryAxis
            ElseIf myaxisunit = TypeArtillery And myalliesunit = TypeBomber Then
                AttackTotal = HABomberAllies
                DefendTotal = ADArtilleryAxis
            ElseIf myaxisunit = TypeAntitank And myalliesunit = TypeInfantery Then
                AttackTotal = HAInfanteryAllies + AttackBonusAllies
                DefendTotal = GDAntitankAxis + DefendBonusAxis + ent1
            ElseIf myaxisunit = TypeAntitank And myalliesunit = TypeTank Then
                AttackTotal = HATankAllies + AttackBonusAllies
                DefendTotal = GDAntitankAxis + DefendBonusAxis + ent1
            ElseIf myaxisunit = TypeAntitank And myalliesunit = TypeArtillery Then
                AttackTotal = HAArtilleryAllies + AttackBonusAllies
                DefendTotal = GDAntitankAxis + DefendBonusAxis + ent1
            ElseIf myaxisunit = TypeAntitank And myalliesunit = TypeAntitank Then
                AttackTotal = HAAntitankAllies + AttackBonusAllies
                DefendTotal = GDAntitankAxis + DefendBonusAxis + ent1
            ElseIf myaxisunit = TypeAntitank And myalliesunit = TypeFighter Then
                AttackTotal = HAFighterAllies
                DefendTotal = ADAntitankAxis
            ElseIf myaxisunit = TypeAntitank And myalliesunit = TypeBomber Then
                AttackTotal = HABomberAllies
                DefendTotal = ADAntitankAxis
            ElseIf myaxisunit = TypeFighter And myalliesunit = TypeInfantery Then
                AttackTotal = AAInfanteryAllies
                DefendTotal = GDFighterAxis
            ElseIf myaxisunit = TypeFighter And myalliesunit = TypeTank Then
                AttackTotal = AATankAllies
                DefendTotal = GDFighterAxis
            ElseIf myaxisunit = TypeFighter And myalliesunit = TypeArtillery Then
                AttackTotal = AAArtilleryAllies
                DefendTotal = GDFighterAxis
            ElseIf myaxisunit = TypeFighter And myalliesunit = TypeAntitank Then
                AttackTotal = AAAntitankAllies
                DefendTotal = GDFighterAxis
            ElseIf myaxisunit = TypeFighter And myalliesunit = TypeFighter Then
                AttackTotal = AAFighterAllies
                DefendTotal = ADFighterAxis
            ElseIf myaxisunit = TypeFighter And myalliesunit = TypeBomber Then
                AttackTotal = AABomberAllies
                DefendTotal = ADFighterAxis
            ElseIf myaxisunit = TypeBomber And myalliesunit = TypeInfantery Then
                AttackTotal = AAInfanteryAllies
                DefendTotal = GDBomberAxis
            ElseIf myaxisunit = TypeBomber And myalliesunit = TypeTank Then
                AttackTotal = AATankAllies
                DefendTotal = GDBomberAxis
            ElseIf myaxisunit = TypeBomber And myalliesunit = TypeArtillery Then
                AttackTotal = AAArtilleryAllies
                DefendTotal = GDBomberAxis
            ElseIf myaxisunit = TypeBomber And myalliesunit = TypeAntitank Then
                AttackTotal = AAAntitankAllies
                DefendTotal = GDBomberAxis
            ElseIf myaxisunit = TypeBomber And myalliesunit = TypeFighter Then
                AttackTotal = AAFighterAllies
                DefendTotal = ADBomberAxis
            ElseIf myaxisunit = TypeBomber And myalliesunit = TypeBomber Then
                AttackTotal = AABomberAllies
                DefendTotal = ADBomberAxis
            End If
            If AttackTotal > 0 Then
                Me.PerformAttack(AttackTotal, DefendTotal, h1)
            End If

        End If

    End Sub

    Public Function Rand(ByVal Low As Long, ByVal High As Long) As Long

        Randomize()
        Rand = Int((High - Low + 1) * Rnd()) + Low

    End Function

    Private Sub AttackAxisArtillery(ByVal x1 As Integer, ByVal y1 As Integer, ByVal x2 As Integer, ByVal y2 As Integer)
        Dim myaxisnr, myalliesnr, i As Integer
        Dim myaxisunit, myalliesunit As Integer
        Dim h1, h2 As Integer

        myaxisnr = UnitsField(x1, y1)
        myalliesnr = UnitsField2(x2, y2)
        myaxisunit = Units(myaxisnr)
        myalliesunit = Units2(myalliesnr)

        Dim a1, a2 As Integer

        a1 = Ammo(myaxisnr)
        a2 = Ammo2(myalliesnr)
        If a1 <= 0 Then
            Return
        End If

        Me.BattleResolution(1, myaxisnr, myalliesnr, h1, h2, a1, a2)
        If a1 > 0 Then
            Ammo(myaxisnr) = a1 - 1
        End If
        'Health(myaxisnr) = h1
        Health2(myalliesnr) = h2

        If Health2(myalliesnr) <= 0 Then 'Unit Allies is lost
            'Me.RemoveUnit2(myalliesnr, x2, y2)

            UnitsField2(x2, y2) = 0
            Units2(myalliesnr) = 0

            MyUnitsAlliesDS.Tables(0).Rows(myalliesnr - 1).Item(4) = 0

            Me.RemoveUnitNew(x2, y2)
            Return
        Else
            Me.ShowHealthUnit(2, x2, y2, myalliesnr)
        End If
        Me.ShowParameters()

    End Sub

    Private Sub AttackAxis(ByVal x1 As Integer, ByVal y1 As Integer, ByVal x2 As Integer, ByVal y2 As Integer)
        Dim myaxisnr, myalliesnr, i As Integer
        Dim myaxisunit, myalliesunit As Integer
        Dim h1, h2 As Integer

        myaxisnr = UnitsField(x1, y1)
        myalliesnr = UnitsField2(x2, y2)
        myaxisunit = Units(myaxisnr)
        myalliesunit = Units2(myalliesnr)

        Dim a1, a2 As Integer

        a1 = Ammo(myaxisnr)
        a2 = Ammo2(myalliesnr)
        If a1 <= 0 And a2 <= 0 Then
            Return
        End If

        Me.BattleResolution(1, myaxisnr, myalliesnr, h1, h2, a1, a2)
        If a1 > 0 Then
            Ammo(myaxisnr) = a1 - 1
        End If
        If a2 > 0 Then
            Ammo2(myalliesnr) = a2 - 1
        End If

        Health(myaxisnr) = h1
        Health2(myalliesnr) = h2


        If Health(myaxisnr) <= 0 Then 'Unit Axis is lost
            'Me.RemoveUnit(myaxisnr, x1, y1)
            UnitsField(x1, y1) = 0
            Units(myaxisnr) = 0

            MyUnitsAxisDS.Tables(0).Rows(myaxisnr - 1).Item(4) = 0

            Me.RemoveUnitNew(x1, y1)

            If Health2(myalliesnr) <= 0 Then 'Unit Allies is lost
                'Me.RemoveUnit2(myalliesnr, x2, y2)

                UnitsField2(x2, y2) = 0
                Units2(myalliesnr) = 0
                MyUnitsAlliesDS.Tables(0).Rows(myalliesnr - 1).Item(4) = 0


                Me.RemoveUnitNew(x2, y2)
                Return
            Else
                Me.ShowHealthUnit(2, x2, y2, myalliesnr)
            End If
        Else
            If Health2(myalliesnr) <= 0 Then 'Unit Allies is lost
                'Me.RemoveUnit2(myalliesnr, x2, y2)

                UnitsField2(x2, y2) = 0
                Units2(myalliesnr) = 0
                MyUnitsAlliesDS.Tables(0).Rows(myalliesnr - 1).Item(4) = 0

                Me.RemoveUnitNew(x2, y2)

                Me.ShowHealthUnit(1, x1, y1, myaxisnr)
                Return
            Else
                Me.ShowHealthUnit(1, x1, y1, myaxisnr)
                Me.ShowHealthUnit(2, x2, y2, myalliesnr)
            End If

        End If
        Me.ShowParameters()

        'MsgBox(myaxisunit.ToString() + "," + myalliesunit.ToString())

    End Sub

    Private Sub AttackAxisAir(ByVal x1 As Integer, ByVal y1 As Integer, ByVal x2 As Integer, ByVal y2 As Integer)
        Dim myaxisnr, myalliesnr, i As Integer
        Dim myaxisunit, myalliesunit As Integer
        Dim h1, h2 As Integer

        myaxisnr = UnitsFieldAir(x1, y1)
        myalliesnr = UnitsFieldAir2(x2, y2)
        myaxisunit = Units(myaxisnr)
        myalliesunit = Units2(myalliesnr)

        Dim a1, a2 As Integer

        a1 = Ammo(myaxisnr)
        a2 = Ammo2(myalliesnr)
        If a1 <= 0 And a2 <= 0 Then
            Return
        End If

        Me.BattleResolution(1, myaxisnr, myalliesnr, h1, h2, a1, a2)
        If a1 > 0 Then
            Ammo(myaxisnr) = a1 - 1
        End If
        If a2 > 0 Then
            Ammo2(myalliesnr) = a2 - 1
        End If

        Health(myaxisnr) = h1
        Health2(myalliesnr) = h2

        If Health(myaxisnr) <= 0 Then 'Unit Axis is lost
            'Me.RemoveUnit(myaxisnr, x1, y1)
            UnitsFieldAir(x1, y1) = 0
            Units(myaxisnr) = 0
            MyUnitsAxisDS.Tables(0).Rows(myaxisnr - 1).Item(4) = 0


            Me.RemoveUnitNew(x1, y1)
            If Health2(myalliesnr) <= 0 Then 'Unit Allies is lost
                'Me.RemoveUnit2(myalliesnr, x2, y2)
                UnitsFieldAir2(x2, y2) = 0
                Units2(myalliesnr) = 0
                MyUnitsAlliesDS.Tables(0).Rows(myalliesnr - 1).Item(4) = 0

                Return
            Else
                Me.ShowHealthUnit(2, x2, y2, myalliesnr)
            End If
        Else
            If Health2(myalliesnr) <= 0 Then 'Unit Allies is lost
                'Me.RemoveUnit2(myalliesnr, x2, y2)
                UnitsFieldAir2(x2, y2) = 0
                Units2(myalliesnr) = 0
                MyUnitsAlliesDS.Tables(0).Rows(myalliesnr - 1).Item(4) = 0

                Me.RemoveUnitNew(x2, y2)
                Me.ShowHealthUnit(1, x1, y1, myaxisnr)
                Return
            Else
                Me.ShowHealthUnit(1, x1, y1, myaxisnr)
                Me.ShowHealthUnit(2, x2, y2, myalliesnr)
            End If

        End If

    End Sub

    Private Sub AttackAxisAirGround(ByVal x1 As Integer, ByVal y1 As Integer)
        Dim myaxisnr, myalliesnr, i As Integer
        Dim myaxisunit, myalliesunit As Integer
        Dim h1, h2 As Integer

        myaxisnr = UnitsFieldAir(x1, y1)
        myalliesnr = UnitsField2(x1, y1)
        myaxisunit = Units(myaxisnr)
        myalliesunit = Units2(myalliesnr)

        Dim a1, a2 As Integer

        a1 = Ammo(myaxisnr)
        a2 = Ammo2(myalliesnr)
        If a1 <= 0 And a2 <= 0 Then
            Return
        End If

        Me.BattleResolution(1, myaxisnr, myalliesnr, h1, h2, a1, a2)
        If a1 > 0 Then
            Ammo(myaxisnr) = a1 - 1
        End If
        If a2 > 0 Then
            Ammo2(myalliesnr) = a2 - 1
        End If

        Health(myaxisnr) = h1
        Health2(myalliesnr) = h2

        If Health(myaxisnr) <= 0 Then 'Unit Axis is lost
            'Me.RemoveUnit(myaxisnr, x1, y1)
            UnitsFieldAir(x1, y1) = 0
            Units(myaxisnr) = 0
            MyUnitsAxisDS.Tables(0).Rows(myaxisnr - 1).Item(4) = 0


            Me.RemoveUnitNew(x1, y1)
            If Health2(myalliesnr) <= 0 Then 'Unit Allies is lost
                'Me.RemoveUnit2(myalliesnr, x1, y1)
                'Me.RemoveUnitNew(x1, y1)
                UnitsField2(x1, y1) = 0
                Units2(myalliesnr) = 0
                MyUnitsAlliesDS.Tables(0).Rows(myalliesnr - 1).Item(4) = 0

                Return
            Else
                Me.ShowHealthUnit(2, x1, y1, myalliesnr)
            End If
        Else
            If Health2(myalliesnr) <= 0 Then 'Unit Allies is lost
                'Me.RemoveUnit2(myalliesnr, x1, y1)
                UnitsField2(x1, y1) = 0
                Units2(myalliesnr) = 0
                MyUnitsAlliesDS.Tables(0).Rows(myalliesnr - 1).Item(4) = 0

                Me.RemoveUnitNew(x1, y1)
                Return
            Else
                Me.ShowHealthUnit(1, x1, y1, myaxisnr)
                Me.ShowHealthUnit(2, x1, y1, myalliesnr)
            End If
        End If

    End Sub

    Private Sub AttackAlliesArtillery(ByVal x1 As Integer, ByVal y1 As Integer, ByVal x2 As Integer, ByVal y2 As Integer)
        Dim myaxisnr, myalliesnr, i As Integer
        Dim myaxisunit, myalliesunit As Integer
        Dim h1, h2 As Integer

        myaxisnr = UnitsField(x2, y2)
        myalliesnr = UnitsField2(x1, y1)
        myaxisunit = Units(myaxisnr)
        myalliesunit = Units2(myalliesnr)

        Dim a1, a2 As Integer

        a1 = Ammo(myaxisnr)
        a2 = Ammo2(myalliesnr)
        If a2 <= 0 Then
            Return
        End If

        Me.BattleResolution(1, myaxisnr, myalliesnr, h1, h2, a1, a2)
        If a2 > 0 Then
            Ammo2(myalliesnr) = a2 - 1
        End If

        Health(myaxisnr) = h1

        If Health(myaxisnr) <= 0 Then 'Unit Axis is lost
            UnitsField(x2, y2) = 0
            Units(myaxisnr) = 0
            MyUnitsAxisDS.Tables(0).Rows(myaxisnr - 1).Item(4) = 0

            Me.RemoveUnitNew(x2, y2)
            If Health2(myalliesnr) <= 0 Then 'Unit Allies is lost
                'Me.RemoveUnit2(myalliesnr, x1, y1)
                UnitsField2(x1, y1) = 0
                Units2(myalliesnr) = 0
                MyUnitsAlliesDS.Tables(0).Rows(myalliesnr - 1).Item(4) = 0

                Me.RemoveUnitNew(x1, y1)
                Return
            Else
                Me.ShowHealthUnit(2, x1, y1, myalliesnr)
            End If
        End If
        Me.ShowParameters()

    End Sub

    Private Sub AttackAllies(ByVal x1 As Integer, ByVal y1 As Integer, ByVal x2 As Integer, ByVal y2 As Integer)
        Dim myaxisnr, myalliesnr, i As Integer
        Dim myaxisunit, myalliesunit As Integer
        Dim h1, h2 As Integer

        myaxisnr = UnitsField(x2, y2)
        myalliesnr = UnitsField2(x1, y1)
        myaxisunit = Units(myaxisnr)
        myalliesunit = Units2(myalliesnr)

        Dim a1, a2 As Integer

        a1 = Ammo(myaxisnr)
        a2 = Ammo2(myalliesnr)
        If a1 <= 0 And a2 <= 0 Then
            Return
        End If

        Me.BattleResolution(1, myaxisnr, myalliesnr, h1, h2, a1, a2)
        If a1 > 0 Then
            Ammo(myaxisnr) = a1 - 1
        End If
        If a2 > 0 Then
            Ammo2(myalliesnr) = a2 - 1
        End If

        Health(myaxisnr) = h1
        Health2(myalliesnr) = h2

        If Health(myaxisnr) <= 0 Then 'Unit Axis is lost
            'Me.RemoveUnit(myaxisnr, x2, y2)
            UnitsField(x2, y2) = 0
            Units(myaxisnr) = 0
            MyUnitsAxisDS.Tables(0).Rows(myaxisnr - 1).Item(4) = 0

            Me.RemoveUnitNew(x2, y2)
            If Health2(myalliesnr) <= 0 Then 'Unit Allies is lost
                'Me.RemoveUnit2(myalliesnr, x1, y1)
                UnitsField2(x1, y1) = 0
                Units2(myalliesnr) = 0
                MyUnitsAlliesDS.Tables(0).Rows(myalliesnr - 1).Item(4) = 0

                Me.RemoveUnitNew(x1, y1)
                Return
            Else
                Me.ShowHealthUnit(2, x1, y1, myalliesnr)
            End If
        Else
            If Health2(myalliesnr) <= 0 Then 'Unit Allies is lost
                'Me.RemoveUnit2(myalliesnr, x1, y1)
                UnitsField2(x1, y1) = 0
                Units2(myalliesnr) = 0
                MyUnitsAlliesDS.Tables(0).Rows(myalliesnr - 1).Item(4) = 0

                Me.RemoveUnitNew(x1, y1)
                Me.ShowHealthUnit(1, x2, y2, myaxisnr)
                Return
            Else
                Me.ShowHealthUnit(1, x2, y2, myaxisnr)
                Me.ShowHealthUnit(2, x1, y1, myalliesnr)

            End If

        End If
        Me.ShowParameters()

    End Sub

    Private Sub AttackAlliesAir(ByVal x1 As Integer, ByVal y1 As Integer, ByVal x2 As Integer, ByVal y2 As Integer)
        Dim myaxisnr, myalliesnr, i As Integer
        Dim myaxisunit, myalliesunit As Integer
        Dim h1, h2 As Integer

        myaxisnr = UnitsFieldAir(x2, y2)
        myalliesnr = UnitsFieldAir2(x1, y1)
        myaxisunit = Units(myaxisnr)
        myalliesunit = Units2(myalliesnr)

        Dim a1, a2 As Integer

        a1 = Ammo(myaxisnr)
        a2 = Ammo2(myalliesnr)
        If a1 <= 0 And a2 <= 0 Then
            Return
        End If

        Me.BattleResolution(1, myaxisnr, myalliesnr, h1, h2, a1, a2)
        If a1 > 0 Then
            Ammo(myaxisnr) = a1 - 1
        End If
        If a2 > 0 Then
            Ammo2(myalliesnr) = a2 - 1
        End If

        Health(myaxisnr) = h1
        Health2(myalliesnr) = h2

        If Health(myaxisnr) <= 0 Then 'Unit Axis is lost
            'Me.RemoveUnit(myaxisnr, x2, y2)

            UnitsFieldAir(x2, y2) = 0
            Units(myaxisnr) = 0
            MyUnitsAxisDS.Tables(0).Rows(myaxisnr - 1).Item(4) = 0


            Me.RemoveUnitNew(x2, y2)
            If Health2(myalliesnr) <= 0 Then 'Unit Allies is lost
                'Me.RemoveUnit2(myalliesnr, x1, y1)

                UnitsFieldAir2(x1, y1) = 0
                Units2(myalliesnr) = 0
                MyUnitsAlliesDS.Tables(0).Rows(myalliesnr - 1).Item(4) = 0

                Me.RemoveUnitNew(x1, y1)
                Return
            Else
                Me.ShowHealthUnit(2, x1, y1, myalliesnr)
            End If
        Else
            If Health2(myalliesnr) <= 0 Then 'Unit Allies is lost
                'Me.RemoveUnit2(myalliesnr, x1, y1)
                UnitsFieldAir2(x1, y1) = 0
                Units2(myalliesnr) = 0
                MyUnitsAlliesDS.Tables(0).Rows(myalliesnr - 1).Item(4) = 0

                Me.RemoveUnitNew(x1, y1)
                Return
            Else
                Me.ShowHealthUnit(1, x2, y2, myaxisnr)
                Me.ShowHealthUnit(2, x1, y1, myalliesnr)

            End If

        End If

    End Sub

    Private Sub AttackAlliesAirGround(ByVal x1 As Integer, ByVal y1 As Integer)
        Dim myaxisnr, myalliesnr, i As Integer
        Dim myaxisunit, myalliesunit As Integer
        Dim h1, h2 As Integer

        myaxisnr = UnitsField(x1, y1)
        myalliesnr = UnitsFieldAir2(x1, y1)
        myaxisunit = Units(myaxisnr)
        myalliesunit = Units2(myalliesnr)

        Dim a1, a2 As Integer

        a1 = Ammo(myaxisnr)
        a2 = Ammo2(myalliesnr)
        If a1 <= 0 And a2 <= 0 Then
            Return
        End If

        Me.BattleResolution(1, myaxisnr, myalliesnr, h1, h2, a1, a2)
        If a1 > 0 Then
            Ammo(myaxisnr) = a1 - 1
        End If
        If a2 > 0 Then
            Ammo2(myalliesnr) = a2 - 1
        End If

        Health(myaxisnr) = h1
        Health2(myalliesnr) = h2

        If Health(myaxisnr) <= 0 Then 'Unit Axis is lost
            UnitsField(x1, y1) = 0
            Units(myaxisnr) = 0
            MyUnitsAxisDS.Tables(0).Rows(myaxisnr - 1).Item(4) = 0

            Me.RemoveUnitNew(x1, y1)
            If Health2(myalliesnr) <= 0 Then 'Unit Allies is lost
                UnitsFieldAir2(x1, y1) = 0
                Units2(myalliesnr) = 0
                MyUnitsAlliesDS.Tables(0).Rows(myalliesnr - 1).Item(4) = 0

                Return
            Else
                Me.ShowHealthUnit(2, x1, y1, myalliesnr)
            End If
        Else
            If Health2(myalliesnr) <= 0 Then 'Unit Allies is lost
                UnitsFieldAir2(x1, y1) = 0
                Units2(myalliesnr) = 0
                MyUnitsAlliesDS.Tables(0).Rows(myalliesnr - 1).Item(4) = 0

                Me.RemoveUnitNew(x1, y1)
                Return
            Else
                Me.ShowHealthUnit(1, x1, y1, myaxisnr)
                Me.ShowHealthUnit(2, x1, y1, myalliesnr)
            End If

        End If

    End Sub



    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click

        Dim thecosts, PlacePosx, PlacePosy, pnum, unittype As Integer
        Dim BuyUnit, Town As String

        BuyUnit = cbUnits.Text
        Select Case BuyUnit

            Case "Infantery (100)"
                unittype = TypeInfantery

            Case "Tank (200)"
                unittype = TypeTank

            Case "Artillery (200)"
                unittype = TypeArtillery

            Case "Anti-tank (200)"
                unittype = TypeAntitank

            Case "Air-Defense (200)"
                unittype = TypeAirdefense

            Case "Fighter (300)"
                unittype = TypeFighter

            Case "Bomber (300)"
                unittype = TypeBomber

        End Select

        Me.PlaceAxis(unittype)
        'Me.BuyGroundAxis(thecosts, CurUnitType)
        'Me.BuyAirAxis(thecosts, CurUnitType)


    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click

        Dim thecosts, PlacePosx, PlacePosy, pnum, unittype As Integer
        Dim BuyUnit, Town As String

        BuyUnit = cbUnits.Text
        Select Case BuyUnit

            Case "Infantery (100)"
                unittype = TypeInfantery

            Case "Tank (200)"
                unittype = TypeTank

            Case "Artillery (200)"
                unittype = TypeArtillery

            Case "Anti-tank (200)"
                unittype = TypeAntitank

            Case "Air-Defense (200)"
                unittype = TypeAirdefense

            Case "Fighter (300)"
                unittype = TypeFighter

            Case "Bomber (300)"
                unittype = TypeBomber

        End Select

        Me.PlaceAllies(unittype)
        'Me.BuyGroundAllies(thecosts, CurUnitType)
        'Me.BuyAirAllies(thecosts, CurUnitType)



    End Sub


    Private Sub cbAxis3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbAxis3.Click

        cbAxis1.Checked = False
        cbAxis2.Checked = False
        cbAxis3.Checked = True


    End Sub

    Private Sub Button10_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click

        Me.Close()

    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbHide.Click

        If WholeScreen = False Then
            Panel1.Height = Panel1.Height + Panel2.Height - 10 '+ 10
            Panel2.Visible = False
            cbHide.Text = "show"
            WholeScreen = True
        Else
            Panel1.Height = Panel1.Height - Panel2.Height + 10 '- 10
            Panel2.Visible = True
            cbHide.Text = "hide"

            WholeScreen = False
        End If

    End Sub

    Public Sub FillAIPlaces3()

        For i = 0 To 30
            AIPlaces3(i, 1) = 0
            AIPlaces3(i, 2) = 0
            AIPlaces3(i, 3) = 0
            AIPlaces3(i, 4) = 0
        Next

        'Defense Lotz. Finished
        AIPlaces3(1, 1) = 9 '21
        AIPlaces3(1, 2) = 7
        AIPlaces3(1, 3) = TypeInfantery
        AIPlaces3(1, 4) = PriceInfantery

        AIPlaces3(2, 1) = 10 '22
        AIPlaces3(2, 2) = 6
        AIPlaces3(2, 3) = TypeAntitank
        AIPlaces3(2, 4) = PriceAntitank

        AIPlaces3(3, 1) = 11 '23
        AIPlaces3(3, 2) = 8
        AIPlaces3(3, 3) = TypeArtillery
        AIPlaces3(3, 4) = PriceArtillery

        AIPlaces3(4, 1) = 10 '24
        AIPlaces3(4, 2) = 8
        AIPlaces3(4, 3) = TypeArtillery
        AIPlaces3(4, 4) = PriceArtillery

        AIPlaces3(5, 1) = 11 '25
        AIPlaces3(5, 2) = 7
        AIPlaces3(5, 3) = TypeArtillery
        AIPlaces3(5, 4) = PriceArtillery

        AIPlaces3(6, 1) = 9 '26
        AIPlaces3(6, 2) = 6
        AIPlaces3(6, 3) = TypeAntitank
        AIPlaces3(6, 4) = PriceAntitank

        AIPlaces3(7, 1) = 11 '27
        AIPlaces3(7, 2) = 6
        AIPlaces3(7, 3) = TypeInfantery
        AIPlaces3(7, 4) = PriceInfantery

        'Defense Lotz. Finished

        'Defense warsaw Part1.
        'These are now fixed units

        'AIPlaces3(8, 1) = 15 '28
        'AIPlaces3(8, 2) = 14
        'AIPlaces3(8, 3) = TypeAntitank
        'AIPlaces3(8, 4) = PriceAntitank

        'AIPlaces3(9, 1) = 16 '29
        'AIPlaces3(9, 2) = 14
        'AIPlaces3(9, 3) = TypeAntitank
        'AIPlaces3(9, 4) = PriceAntitank

        'AIPlaces3(10, 1) = 15 '30
        'AIPlaces3(10, 2) = 15
        'AIPlaces3(10, 3) = TypeAntitank
        'AIPlaces3(10, 4) = PriceAntitank


        AIPlaces3(8, 1) = 17 '28
        AIPlaces3(8, 2) = 14
        AIPlaces3(8, 3) = TypeInfantery
        AIPlaces3(8, 4) = PriceInfantery

        AIPlaces3(9, 1) = 17 '29
        AIPlaces3(9, 2) = 15
        AIPlaces3(9, 3) = TypeArtillery
        AIPlaces3(9, 4) = PriceArtillery


        'This is now fixed unit
        'AIPlaces3(13, 1) = 17 '33
        'AIPlaces3(13, 2) = 16
        'AIPlaces3(13, 3) = TypeArtillery
        'AIPlaces3(13, 4) = TypeArtillery

        AIPlaces3(10, 1) = 16 '30
        AIPlaces3(10, 2) = 16
        AIPlaces3(10, 3) = TypeArtillery
        AIPlaces3(10, 4) = PriceArtillery

        AIPlaces3(11, 1) = 15 '31
        AIPlaces3(11, 2) = 16
        AIPlaces3(11, 3) = TypeInfantery
        AIPlaces3(11, 4) = PriceInfantery


        'SIEDICE(Mounts)
        AIPlaces3(12, 1) = 22 '32
        AIPlaces3(12, 2) = 14
        AIPlaces3(12, 3) = TypeFighter
        AIPlaces3(12, 4) = PriceFighter

        AIPlaces3(13, 1) = 21 '33
        AIPlaces3(13, 2) = 15
        AIPlaces3(13, 3) = TypeAntitank
        AIPlaces3(13, 4) = PriceAntitank

        AIPlaces3(14, 1) = 22 '34
        AIPlaces3(14, 2) = 15
        AIPlaces3(14, 3) = TypeAntitank
        AIPlaces3(14, 4) = PriceAntitank

        AIPlaces3(15, 1) = 21 '35
        AIPlaces3(15, 2) = 16
        AIPlaces3(15, 3) = TypeInfantery
        AIPlaces3(15, 4) = PriceAntitank

        AIPlaces3(16, 1) = 21 '36
        AIPlaces3(16, 2) = 17
        AIPlaces3(16, 3) = TypeAntitank
        AIPlaces3(16, 4) = PriceAntitank

        AIPlaces3(17, 1) = 23 '37
        AIPlaces3(17, 2) = 15
        AIPlaces3(17, 3) = TypeAntitank
        AIPlaces3(17, 4) = PriceAntitank

        AIPlaces3(18, 1) = 23 '38
        AIPlaces3(18, 2) = 16
        AIPlaces3(18, 3) = TypeArtillery
        AIPlaces3(18, 4) = PriceArtillery

        AIPlaces3(19, 1) = 23 '39
        AIPlaces3(19, 2) = 17
        AIPlaces3(19, 3) = TypeArtillery
        AIPlaces3(19, 4) = PriceArtillery

        AIPlaces3(20, 1) = 22 '40
        AIPlaces3(20, 2) = 17
        AIPlaces3(20, 3) = TypeArtillery
        AIPlaces3(20, 4) = PriceArtillery


        AIPlaces3(21, 1) = 23 '41
        AIPlaces3(21, 2) = 15
        AIPlaces3(21, 3) = TypeFighter
        AIPlaces3(21, 4) = PriceFighter

    End Sub

    Public Sub FillAIPlaces2()

        For i = 0 To 30
            AIPlaces2(i, 1) = 0
            AIPlaces2(i, 2) = 0
            AIPlaces2(i, 3) = 0
            AIPlaces2(i, 4) = 0
        Next

        'Defense Lotz. Finished
        AIPlaces2(1, 1) = 9 '21
        AIPlaces2(1, 2) = 7
        AIPlaces2(1, 3) = TypeInfantery
        AIPlaces2(1, 4) = PriceInfantery

        AIPlaces2(2, 1) = 10 '22
        AIPlaces2(2, 2) = 6
        AIPlaces2(2, 3) = TypeInfantery
        AIPlaces2(2, 4) = PriceInfantery

        AIPlaces2(3, 1) = 11 '23
        AIPlaces2(3, 2) = 8
        AIPlaces2(3, 3) = TypeArtillery
        AIPlaces2(3, 4) = PriceArtillery

        AIPlaces2(4, 1) = 10 '24
        AIPlaces2(4, 2) = 8
        AIPlaces2(4, 3) = TypeArtillery
        AIPlaces2(4, 4) = PriceArtillery

        AIPlaces2(5, 1) = 11 '25
        AIPlaces2(5, 2) = 7
        AIPlaces2(5, 3) = TypeArtillery
        AIPlaces2(5, 4) = PriceArtillery

        AIPlaces2(6, 1) = 9 '26
        AIPlaces2(6, 2) = 6
        AIPlaces2(6, 3) = TypeInfantery
        AIPlaces2(6, 4) = PriceInfantery

        AIPlaces2(7, 1) = 11 '27
        AIPlaces2(7, 2) = 6
        AIPlaces2(7, 3) = TypeInfantery
        AIPlaces2(7, 4) = PriceInfantery

        'Defense Lotz. Finished

        AIPlaces2(8, 1) = 17 '28
        AIPlaces2(8, 2) = 14
        AIPlaces2(8, 3) = TypeInfantery
        AIPlaces2(8, 4) = PriceInfantery

        AIPlaces2(9, 1) = 17 '29
        AIPlaces2(9, 2) = 15
        AIPlaces2(9, 3) = TypeArtillery
        AIPlaces2(9, 4) = PriceArtillery

        AIPlaces2(10, 1) = 16 '30
        AIPlaces2(10, 2) = 16
        AIPlaces2(10, 3) = TypeArtillery
        AIPlaces2(10, 4) = PriceArtillery

        AIPlaces2(11, 1) = 15 '31
        AIPlaces2(11, 2) = 16
        AIPlaces2(11, 3) = TypeInfantery
        AIPlaces2(11, 4) = PriceInfantery


        'SIEDICE(Mounts)
        AIPlaces2(12, 1) = 22 '32
        AIPlaces2(12, 2) = 14
        AIPlaces2(12, 3) = TypeFighter
        AIPlaces2(12, 4) = PriceFighter

        AIPlaces2(13, 1) = 21 '33
        AIPlaces2(13, 2) = 15
        AIPlaces2(13, 3) = TypeInfantery
        AIPlaces2(13, 4) = PriceInfantery

        AIPlaces2(14, 1) = 22 '34
        AIPlaces2(14, 2) = 15
        AIPlaces2(14, 3) = TypeInfantery
        AIPlaces2(14, 4) = PriceInfantery

        AIPlaces2(15, 1) = 21 '35
        AIPlaces2(15, 2) = 16
        AIPlaces2(15, 3) = TypeInfantery
        AIPlaces2(15, 4) = PriceInfantery

        AIPlaces2(16, 1) = 21 '36
        AIPlaces2(16, 2) = 17
        AIPlaces2(16, 3) = TypeInfantery
        AIPlaces2(16, 4) = PriceInfantery

        AIPlaces2(17, 1) = 23 '37
        AIPlaces2(17, 2) = 15
        AIPlaces2(17, 3) = TypeInfantery
        AIPlaces2(17, 4) = PriceInfantery

        AIPlaces2(18, 1) = 23 '38
        AIPlaces2(18, 2) = 16
        AIPlaces2(18, 3) = TypeArtillery
        AIPlaces2(18, 4) = PriceArtillery

        AIPlaces2(19, 1) = 23 '39
        AIPlaces2(19, 2) = 17
        AIPlaces2(19, 3) = TypeArtillery
        AIPlaces2(19, 4) = PriceArtillery

        AIPlaces2(20, 1) = 22 '40
        AIPlaces2(20, 2) = 17
        AIPlaces2(20, 3) = TypeArtillery
        AIPlaces2(20, 4) = PriceArtillery


        AIPlaces2(21, 1) = 23 '41
        AIPlaces2(21, 2) = 15
        AIPlaces2(21, 3) = TypeFighter
        AIPlaces2(21, 4) = PriceFighter

    End Sub

    Public Sub FillAIPlaces1()

        For i = 0 To 30
            AIPlaces1(i, 1) = 0
            AIPlaces1(i, 2) = 0
            AIPlaces1(i, 3) = 0
            AIPlaces1(i, 4) = 0
        Next

        'Defense Lotz. Finished
        AIPlaces1(1, 1) = 9 '21
        AIPlaces1(1, 2) = 7
        AIPlaces1(1, 3) = TypeAntitank
        AIPlaces1(1, 4) = PriceAntitank

        AIPlaces1(2, 1) = 10 '22
        AIPlaces1(2, 2) = 6
        AIPlaces1(2, 3) = TypeAntitank
        AIPlaces1(2, 4) = PriceAntitank

        AIPlaces1(3, 1) = 11 '23
        AIPlaces1(3, 2) = 8
        AIPlaces1(3, 3) = TypeArtillery
        AIPlaces1(3, 4) = PriceArtillery

        AIPlaces1(4, 1) = 10 '24
        AIPlaces1(4, 2) = 8
        AIPlaces1(4, 3) = TypeArtillery
        AIPlaces1(4, 4) = PriceArtillery

        AIPlaces1(5, 1) = 11 '25
        AIPlaces1(5, 2) = 7
        AIPlaces1(5, 3) = TypeArtillery
        AIPlaces1(5, 4) = PriceArtillery

        AIPlaces1(6, 1) = 9 '26
        AIPlaces1(6, 2) = 6
        AIPlaces1(6, 3) = TypeAntitank
        AIPlaces1(6, 4) = PriceAntitank

        AIPlaces1(7, 1) = 11 '27
        AIPlaces1(7, 2) = 6
        AIPlaces1(7, 3) = TypeAntitank
        AIPlaces1(7, 4) = PriceAntitank

        'Defense Lotz. Finished

        'Defense warsaw Part1.
        'These are now fixed units

        'AIPlaces1(8, 1) = 15 '28
        'AIPlaces1(8, 2) = 14
        'AIPlaces1(8, 3) = TypeAntitank
        'AIPlaces1(8, 4) = PriceAntitank

        'AIPlaces1(9, 1) = 16 '29
        'AIPlaces1(9, 2) = 14
        'AIPlaces1(9, 3) = TypeAntitank
        'AIPlaces1(9, 4) = PriceAntitank

        'AIPlaces1(10, 1) = 15 '30
        'AIPlaces1(10, 2) = 15
        'AIPlaces1(10, 3) = TypeAntitank
        'AIPlaces1(10, 4) = PriceAntitank


        AIPlaces1(8, 1) = 17 '28
        AIPlaces1(8, 2) = 14
        AIPlaces1(8, 3) = TypeAntitank
        AIPlaces1(8, 4) = PriceAntitank

        AIPlaces1(9, 1) = 17 '29
        AIPlaces1(9, 2) = 15
        AIPlaces1(9, 3) = TypeArtillery
        AIPlaces1(9, 4) = PriceArtillery


        'This is now fixed unit
        'AIPlaces1(13, 1) = 17 '33
        'AIPlaces1(13, 2) = 16
        'AIPlaces1(13, 3) = TypeArtillery
        'AIPlaces1(13, 4) = TypeArtillery

        AIPlaces1(10, 1) = 16 '30
        AIPlaces1(10, 2) = 16
        AIPlaces1(10, 3) = TypeArtillery
        AIPlaces1(10, 4) = PriceArtillery

        AIPlaces1(11, 1) = 15 '31
        AIPlaces1(11, 2) = 16
        AIPlaces1(11, 3) = TypeAntitank
        AIPlaces1(11, 4) = PriceAntitank


        'SIEDICE(Mounts)
        AIPlaces1(12, 1) = 22 '32
        AIPlaces1(12, 2) = 14
        AIPlaces1(12, 3) = TypeFighter
        AIPlaces1(12, 4) = PriceFighter

        AIPlaces1(13, 1) = 21 '33
        AIPlaces1(13, 2) = 15
        AIPlaces1(13, 3) = TypeAntitank
        AIPlaces1(13, 4) = PriceAntitank

        AIPlaces1(14, 1) = 22 '34
        AIPlaces1(14, 2) = 15
        AIPlaces1(14, 3) = TypeAntitank
        AIPlaces1(14, 4) = PriceAntitank

        AIPlaces1(15, 1) = 21 '35
        AIPlaces1(15, 2) = 16
        AIPlaces1(15, 3) = TypeAntitank
        AIPlaces1(15, 4) = PriceAntitank

        AIPlaces1(16, 1) = 21 '36
        AIPlaces1(16, 2) = 17
        AIPlaces1(16, 3) = TypeAntitank
        AIPlaces1(16, 4) = PriceAntitank

        AIPlaces1(17, 1) = 23 '37
        AIPlaces1(17, 2) = 15
        AIPlaces1(17, 3) = TypeAntitank
        AIPlaces1(17, 4) = PriceAntitank

        AIPlaces1(18, 1) = 23 '38
        AIPlaces1(18, 2) = 16
        AIPlaces1(18, 3) = TypeArtillery
        AIPlaces1(18, 4) = PriceArtillery

        AIPlaces1(19, 1) = 23 '39
        AIPlaces1(19, 2) = 17
        AIPlaces1(19, 3) = TypeArtillery
        AIPlaces1(19, 4) = PriceArtillery

        AIPlaces1(20, 1) = 22 '40
        AIPlaces1(20, 2) = 17
        AIPlaces1(20, 3) = TypeArtillery
        AIPlaces1(20, 4) = PriceArtillery


        AIPlaces1(21, 1) = 23 '41
        AIPlaces1(21, 2) = 15
        AIPlaces1(21, 3) = TypeFighter
        AIPlaces1(21, 4) = PriceFighter


    End Sub



    Private Sub CheckBox3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbAnimation.Click

        If AnimationShown = False Then

            AnimationShown = True

        Else

            AnimationShown = False

        End If
    End Sub
End Class