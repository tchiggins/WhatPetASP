Imports System.Web.Mvc

Namespace Controllers
    Public Class WhatPetController
        Inherits Controller

        ' GET: WhatPet
        Public Function Index() As ActionResult
            Return View()
        End Function

        Public Function Welcome() As String
            Return "This is the Welcome action method..."
        End Function
    End Class
End Namespace