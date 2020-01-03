Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports WhatPetASP

Namespace Controllers
    Public Class SpeciesController
        Inherits System.Web.Mvc.Controller

        Private db As New SpeciesDBContext

        'GET: Species
        Function Index() As ActionResult
            Return View(db.AllSpecies.ToList())
        End Function

        'GET: Species/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim species As Species = db.AllSpecies.Find(id)
            If IsNothing(species) Then
                Return HttpNotFound()
            End If
            Return View(species)
        End Function

        'GET: Species/Create
        Function Create() As ActionResult
            Return View()
        End Function

        'POST: Species/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="SpeciesID,SpeciesName,PetClassID")> ByVal species As Species) As ActionResult
            If ModelState.IsValid Then
                db.AllSpecies.Add(species)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(species)
        End Function

        'GET: Species/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim species As Species = db.AllSpecies.Find(id)
            If IsNothing(species) Then
                Return HttpNotFound()
            End If
            Return View(species)
        End Function

        'POST: Species/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="SpeciesID,SpeciesName,PetClassID")> ByVal species As Species) As ActionResult
            If ModelState.IsValid Then
                db.Entry(species).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(species)
        End Function

        'GET: Species/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim species As Species = db.AllSpecies.Find(id)
            If IsNothing(species) Then
                Return HttpNotFound()
            End If
            Return View(species)
        End Function

        'POST: Species/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim species As Species = db.AllSpecies.Find(id)
            db.AllSpecies.Remove(species)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace