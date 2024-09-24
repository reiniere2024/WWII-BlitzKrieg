Module Globals


    Public GameMode As Integer = 0 '0 = easy, 1 =average, 2 = difficult
    'GM=0: Prestige, HA,SA,AA,GD,AD Ammo,Fuel, No entrench.

    Public CurAnimation As Integer = 1
    Public ArtilleryAlliesPosx As Integer = 0
    Public ArtilleryAlliesPosy As Integer = 0


    Public RasterShown As Boolean = False
    Public AnimationShown As Boolean = False

    Public WholeScreen As Boolean = False
    Public TypeofGame As Integer '1=HxH,2=HxAI,3=PBEM
    Public NeworLoadGame As Integer '1=new,3=load,4=loadpbem
    Public NrofSaveGame As String
    Public NewPBEMGame As Boolean = False


    Public MyGame As BattleForm
    Public MyOptions1 As frmNewgame1
    Public MyOptions2 As frmNewgame2

    Public FarawayX As Integer = 1130
    Public FarawayY As Integer = 560

    Public LastPosx As Integer
    Public LastPosy As Integer
    Public LastUnit As Integer
    Public LastPositionx As Integer
    Public LastPositiony As Integer

    Public CitiesAxis As Integer = 4
    Public CitiesAxisStart As Integer = 4

    Public CitiesAllies As Integer = 14
    Public CitiesAlliesStart As Integer = 14

    Public MyPassword As String = "Tapyxe_01"
    Public Moved As Boolean = False

    Public UnitArea(100) As Bitmap
    Public UnitAreaLast(100) As Bitmap

    Public UnitArea2(100) As Bitmap
    Public UnitArea2Last(100) As Bitmap

    Public DeltaHealthX As Integer = 25
    Public DeltaHealthXPlane As Integer = 3 '47

    Public DeltaHealthX2 As Integer = 30
    Public DeltaHealthX2Plane As Integer = 8 '52

    Public MyUnitsAxisDS As DataSet
    Public MyUnitsAlliesDS As DataSet
    Public MyCountriesDS As DataSet
    Public MySettingsDS As DataSet

    Public DeltaFlagX As Integer = -5
    Public DeltaFlagY As Integer = 38

    Public DeltaHealthY As Integer = 53

    Public FOGSpaces As Integer = 3

    Public MyGameField As Bitmap
    Public MyGameFieldNoRaster As Bitmap
    Public MyGameFieldRaster As Bitmap

    'Public ImageCopyW As Integer = 100
    'Public ImageCopyH As Integer = 50
    Public ImageCopyW As Integer = 80
    Public ImageCopyH As Integer = 68

    Public DeltaArrowsX As Integer = 50 '40
    Public DeltaArrowsY As Integer = 50
    Public DeltaArrowsYStart As Integer = 40 '30
    Public DeltaArrowsXStart As Integer = 30 '13

    Public NrofMoves As Integer = 0
    Public MaxMoves As Integer = 18

    Public StartPosx As Integer
    Public StartPosy As Integer
    Public DeltaX As Integer = 10
    Public DeltaY As Integer = 10
    Public DeltaYMax As Integer = 501
    Public AirDeltaY As Integer = 20

    Public UnitsField(MaxRasterX, MaxRasterY) As Integer
    Public UnitsFieldAir(MaxRasterX, MaxRasterY) As Integer

    Public UnitsField2(MaxRasterX, MaxRasterY) As Integer
    Public UnitsFieldAir2(MaxRasterX, MaxRasterY) As Integer

    Public SlideX As Integer = 57
    Public SlideY As Integer = 60

    Public Curx As Integer = 1
    Public Cury As Integer = 1


    'Public HexesPosy As Integer = 1015
    Public DeltaYUnit As Integer = 0
    Public DeltaYAir As Integer = 0

    Public CurPosx As Integer = 0
    Public CurPosy As Integer = 0
    Public CurUnit As Integer = 0


    Public RasterVertical As Integer = 60
    Public RasterHorizontal As Integer = 62


    Public Raster As Bitmap



End Module
