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

        Dim PetClassData As New PetClass With {
            .ClassName = "Mammal"
        }
        DataSetup.PopulatePetClassData(PetClassData)

        Dim SpeciesData As New Species With {
            .SpeciesName = "Cat",
            .PetClassPetClassID = 1
        }
    End Sub
End Class