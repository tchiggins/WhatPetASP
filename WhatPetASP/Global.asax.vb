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
        DataSetup.PopulateData()
    End Sub
End Class