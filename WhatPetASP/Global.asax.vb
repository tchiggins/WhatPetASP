﻿Imports System.Web.Optimization

Public Class MvcApplication
    Inherits System.Web.HttpApplication
    Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)

        'Clear the tables
        DataSetup.ClearPetClassTable()
        DataSetup.ClearSpeciesTable()
        DataSetup.ClearPetTypeTable()

        'Populate them from the csv files
        DataSetup.PC_CSVImport("PetClass.csv")
        DataSetup.SP_CSVImport("Species.csv")
        DataSetup.PT_CSVImport("PetType.csv")
    End Sub
End Class