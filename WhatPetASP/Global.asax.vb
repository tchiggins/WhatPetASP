Imports System.Web.Optimization

Public Class MvcApplication
    Inherits System.Web.HttpApplication
    Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
        ' Setup the Datbase
        ' DataSetup.CreateTables()
        ' Add static data
        ' DataSetup.InsertIntoDB()

        ' Clear the tables
        DataSetup.ClearPetClassTable()
        DataSetup.ClearSpeciesTable()
        DataSetup.ClearPetTypeTable()

        ' Populate them from the csv files (TO BE DONE)
        Dim PetClassData As New PetClass With {
            .ClassName = "Mammal"
        }
        DataSetup.PopulatePetClassData(PetClassData)

        ' Read the ID
        Dim PetClassID = DataSetup.GetPetClassID("Mammal")

        Dim SpeciesData As New Species With {
            .SpeciesName = "Cat",
            .PetClassPetClassID = PetClassID
        }
        DataSetup.PopulateSpeciesData(SpeciesData)
    End Sub
End Class