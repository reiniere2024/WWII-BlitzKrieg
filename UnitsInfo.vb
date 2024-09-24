Module UnitsInfo

    Public AttackBonusRough As Integer = 1
    Public DefendBonusRough As Integer = 1
    Public AttackBonusRiver As Integer = -2
    Public DefendBonusRiver As Integer = -2

    Public TextAxisx As Integer = 5
    Public TextAxisy As Integer = 17
    Public TextAlliesx As Integer = 6
    Public TextAlliesy As Integer = 17

    Public CurUnitType As Integer
    Public CurUnitSort As String

    Public Units(100) As Integer
    Public Units2(100) As Integer

    Public UnitsX(100) As Integer
    Public UnitsX2(100) As Integer
    Public UnitsY(100) As Integer
    Public UnitsY2(100) As Integer

    Public MAMax(100) As Integer
    Public MAMax2(100) As Integer
    Public MA(100) As Integer
    Public MA2(100) As Integer
    Public Attacked(100) As Integer
    Public Attacked2(100) As Integer

    Public Health(100) As Integer
    Public Health2(100) As Integer

    Public Entrenched(100) As Integer
    Public Entrenched2(100) As Integer
    Public Ammo(100) As Integer
    Public Ammo2(100) As Integer
    Public Fuel(100) As Integer
    Public Fuel2(100) As Integer
    Public Movement(100) As Integer
    Public Movement2(100) As Integer

    Public SAInfanteryAxis As Integer = 6
    Public SAInfanteryAllies As Integer = 5
    Public HAInfanteryAxis As Integer = 2
    Public HAInfanteryAllies As Integer = 2 '1
    Public AAInfanteryAxis As Integer = 0
    Public AAInfanteryAllies As Integer = 0
    Public NAInfanteryAxis As Integer = 1
    Public NAInfanteryAllies As Integer = 1
    Public GDInfanteryAxis As Integer = 5 '6
    Public GDInfanteryAllies As Integer = 4 '3
    Public ADInfanteryAxis As Integer = 7
    Public ADInfanteryAllies As Integer = 7

    Public SATankAxis As Integer = 1
    Public SATankAllies As Integer = 1
    Public HATankAxis As Integer = 7
    Public HATankAllies As Integer = 6
    Public AATankAxis As Integer = 0
    Public AATankAllies As Integer = 0
    Public NATankAxis As Integer = 1
    Public NATankAllies As Integer = 1
    Public GDTankAxis As Integer = 8
    Public GDTankAllies As Integer = 7
    Public ADTankAxis As Integer = 8
    Public ADTankAllies As Integer = 7

    Public SAArtilleryAxis As Integer = 6 '11
    Public SAArtilleryAllies As Integer = 6 '11
    Public HAArtilleryAxis As Integer = 5
    Public HAArtilleryAllies As Integer = 5
    Public AAArtilleryAxis As Integer = 1
    Public AAArtilleryAllies As Integer = 1
    Public NAArtilleryAxis As Integer = 1
    Public NAArtilleryAllies As Integer = 1
    Public GDArtilleryAxis As Integer = 2
    Public GDArtilleryAllies As Integer = 2
    Public ADArtilleryAxis As Integer = 6
    Public ADArtilleryAllies As Integer = 6

    Public SAAntitankAxis As Integer = 1
    Public SAAntitankAllies As Integer = 1
    Public HAAntitankAxis As Integer = 7
    Public HAAntitankAllies As Integer = 7
    Public AAAntitankAxis As Integer = 0
    Public AAAntitankAllies As Integer = 0
    Public NAAntitankAxis As Integer = 1
    Public NAAntitankAllies As Integer = 1
    Public GDAntitankAxis As Integer = 8
    Public GDAntitankAllies As Integer = 8
    Public ADAntitankAxis As Integer = 8
    Public ADAntitankAllies As Integer = 8

    Public SAFighterAxis As Integer = 2
    Public SAFighterAllies As Integer = 1
    Public HAFighterAxis As Integer = 2
    Public HAFighterAllies As Integer = 1
    Public AAFighterAxis As Integer = 8 '14
    Public AAFighterAllies As Integer = 6
    Public NAFighterAxis As Integer = 1
    Public NAFighterAllies As Integer = 1
    Public GDFighterAxis As Integer = 7
    Public GDFighterAllies As Integer = 6
    Public ADFighterAxis As Integer = 10
    Public ADFighterAllies As Integer = 6

    Public SABomberAxis As Integer = 9
    Public SABomberAllies As Integer = 7
    Public HABomberAxis As Integer = 8
    Public HABomberAllies As Integer = 6
    Public AABomberAxis As Integer = 3 '4
    Public AABomberAllies As Integer = 2 '3
    Public NABomberAxis As Integer = 5
    Public NABomberAllies As Integer = 3
    Public GDBomberAxis As Integer = 6
    Public GDBomberAllies As Integer = 6
    Public ADBomberAxis As Integer = 4
    Public ADBomberAllies As Integer = 3 '4

    Public Trans(100) As Integer
    Public Trans2(100) As Integer
    Public Mount(100) As Integer
    Public Mount2(100) As Integer

    Public UnitsArea As Bitmap
    Public UnitAreas(MaxRasterX, MaxRasterY) As Bitmap

    Public UnitAreasNoRaster(MaxRasterX, MaxRasterY) As Bitmap
    Public UnitAreasRaster(MaxRasterX, MaxRasterY) As Bitmap

    Public UnitsAreaX As Integer = 50
    Public UnitsAreaY As Integer = 150
    Public UnitsAreaW As Integer = 400 '300
    Public UnitsAreaH As Integer = 400 '300

    Public TypeInfantery As Integer = 1
    Public PriceInfantery As Integer = 100
    Public TypeTank As Integer = 2
    Public PriceTank As Integer = 200
    Public TypeArtillery As Integer = 3
    Public PriceArtillery As Integer = 200
    Public TypeAntitank As Integer = 4
    Public PriceAntitank As Integer = 150
    Public TypeAirdefense As Integer = 5
    Public PriceAirdefense As Integer = 150
    Public TypeRecon As Integer = 6

    Public TypeFighter As Integer = 9
    Public PriceFighter As Integer = 300
    Public TypeBomber As Integer = 10
    Public PriceBomber As Integer = 300


    Public MAInfanterie As Integer = 1
    Public MATank As Integer = 3 '2
    Public MATransport As Integer = 3 '2
    Public MAArtillery As Integer = 1
    Public MAAntitank As Integer = 1
    Public MAAirdefense As Integer = 1
    Public MAPlane As Integer = 14

    Public AmmoInfanterie As Integer = 15 '7
    Public AmmoTank As Integer = 12 '8
    Public AmmoArtillery As Integer = 12 '8
    Public AmmoAntitank As Integer = 12 '9
    Public AmmoAirdefense As Integer = 12 '8
    Public AmmoPlane As Integer = 12 '7

    Public FuelInfanterie As Integer = 0
    Public FuelTank As Integer = 50 '40
    Public FuelArtillery As Integer = 0
    Public FuelAntitank As Integer = 0
    Public FuelAirdefense As Integer = 0
    Public FuelPlane As Integer = 60 '50

    Public TransInfanterie As Integer = 0
    Public TransTank As Integer = 0
    Public TransArtillery As Integer = 0
    Public TransAntitank As Integer = 0
    Public TransAirdefense As Integer = 0
    Public TransPlane As Integer = 0

    Public EntrenchInfanterie As Integer = 3
    Public EntrenchTank As Integer = 2
    Public EntrenchArtillery As Integer = 2
    Public EntrenchAntitank As Integer = 2
    Public EntrenchPlane As Integer = 0

    Public TransOpel As Integer = 1
    Public TransArmed As Integer = 2
    Public TransShip As Integer = 3
    Public TransAir As Integer = 4


End Module
