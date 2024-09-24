Module CountrySpecific

    Public BattleNr As Integer = 1
    Public BattleCountry As String = "Poland"
    Public MyTitle = "______________________________________________________________________________________________  WWIIBlitzkrieg Campaign 1 - The Polish Invasion   ________________________________________________________________________________________________"

    Public MaxTurns As Integer = 24
    Public Turn As String = "Axis" 'or Allies
    Public TurnNr As Integer = 1

    Public UnitsAxisXML As String = "S01_Poland_UnitsAxis.xml"
    Public UnitsAlliesXML As String = "S01_Poland_UnitsAllies.xml"
    Public CitiesXML As String = "S01_Poland_cities.xml"
    Public SettingsXML As String = "S01_Poland_settings.xml"
    Public GameMap As String = "Scenario-Polen.bmp"

    Public SavegameSettings As String = "S01_Poland_Settings"
    Public SavegameAxis As String = "S01_Poland_UnitsAxis"
    Public SavegameAllies As String = "S01_Poland_UnitsAllies"

    Public PrestigeAxis As Integer = 500
    Public PrestigeAxisEasy As Integer = 800
    Public PrestigeAxisAverage As Integer = 500
    Public PrestigeAxisHard As Integer = 300

    Public PrestigeAllies As Integer = 600
    Public PrestigeAlliesHard As Integer = 1200
    Public PrestigeAlliesEasy As Integer = 400
    Public PrestigeAlliesAverage As Integer = 800

    Public PrestigeTurnAxis As Integer = 100
    Public PrestigeTurnAxisEasy As Integer = 120
    Public PrestigeTurnAxisAverage As Integer = 100
    Public PrestigeTurnAxisHard As Integer = 80

    Public PrestigeTurnAllies As Integer = 100
    Public PrestigeTurnAlliesEasy As Integer = 80
    Public PrestigeTurnAlliesAverage As Integer = 100
    Public PrestigeTurnAlliesHard As Integer = 120




    Public PrestigeCountryAxis As Integer = 40 '40
    Public PrestigeCountryAxisHard As Integer = 40 '40
    Public PrestigeCountryAxisEasy As Integer = 50 '40
    Public PrestigeCountryAxisAverage As Integer = 45 '40

    Public PrestigeCountryAllies As Integer = 20 '25
    Public PrestigeCountryAlliesHard As Integer = 25 '25
    Public PrestigeCountryAlliesEasy As Integer = 15 '25
    Public PrestigeCountryAlliesAverage As Integer = 20 '25

    Public MaxRasterX As Integer = 23
    Public MaxRasterY As Integer = 18 '17

    'Public RasterStartx As Integer = 63
    'Public RasterStarty As Integer = 1007

    Public RasterStartx As Integer = 63
    Public RasterStarty As Integer = 1071

    Public DeltaHexesX As Integer = 8 '10
    Public DeltaHexesX2 As Integer = 8 '15
    Public DeltaHexesY As Integer = 20

    'Startup Place for Units Axis
    Public HexesPosx As Integer = 66
    Public HexesPosy As Integer = 1080
    'Startup Place for Units Axis
    Public AlliesPosx As Integer = 68 '70
    Public AlliesPosy As Integer = 1080 '1017

    Public UnitStartx As Integer = 66
    Public UnitStarty As Integer = 1010

    'Public RasterWidth As Integer = 56 '62
    Public RasterWidth As Integer = 73 '62
    Public RasterWidth2 As Integer = 75 '62
    Public RasterWidth3 As Integer = 73 '62

    'Public RasterHeight As Integer = 32
    Public RasterHeight As Integer = 32
    Public RasterHeight2 As Integer = 31
    Public RasterHeight3 As Integer = 33

    'Axis start places
    Public AxisTown1 As String = "Posen"
    Public AxisTown2 As String = "Breslau"
    Public AxisTown3 As String

    'Public PosenPosx As Integer = 1
    'Public PosenPosy As Integer = 13
    'Public PosenAirportPosx As Integer = 1
    'Public PosenAirportPosy As Integer = 12

    'Public BreslauPosx As Integer = 1
    'Public BreslauPosy As Integer = 2
    'Public BreslauAirportPosx As Integer = 1
    'Public BreslauAirportPosy As Integer = 1

    'Allies start places
    Public AlliesTown1 As String = "Lodz"
    Public AlliesTown2 As String = "Warsaw"
    Public AlliesTown3 As String = "Siedice"

    'Public LodzPosx As Integer = 10
    'Public LodzPosy As Integer = 7
    'Public WarsawPosx As Integer = 16
    'Public WarsawPosy As Integer = 15
    'Public SiedicePosx As Integer = 22
    'Public SiedicePosy As Integer = 16
    'Public SiediceAirPosx As Integer = 22
    'Public SiediceAirPosy As Integer = 15

    Public AxisPosx1 As Integer = 1
    Public AxisPosy1 As Integer = 13
    Public AxisPosx2 As Integer = 1
    Public AxisPosy2 As Integer = 2
    Public AxisPosx3 As Integer = 0
    Public AxisPosy3 As Integer = 0
    Public AxisAirportPosx1 As Integer = 1
    Public AxisAirportPosy1 As Integer = 12
    Public AxisAirportPosx2 As Integer = 1
    Public AxisAirportPosy2 As Integer = 1
    Public AxisAirportPosx3 As Integer = 0
    Public AxisAirportPosy3 As Integer = 0

    Public AlliesPosx1 As Integer = 10
    Public AlliesPosy1 As Integer = 7
    Public AlliesPosx2 As Integer = 16
    Public AlliesPosy2 As Integer = 15
    Public AlliesPosx3 As Integer = 22
    Public AlliesPosy3 As Integer = 16
    Public AlliesAirportPosx1 As Integer = 22
    Public AlliesAirportPosy1 As Integer = 15
    Public AlliesAirportPosx2 As Integer = 0
    Public AlliesAirportPosy2 As Integer = 0
    Public AlliesAirportPosx3 As Integer = 0
    Public AlliesAirportPosy3 As Integer = 0


End Module
