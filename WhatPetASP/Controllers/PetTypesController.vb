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
    Public Class PetTypesController
        Inherits System.Web.Mvc.Controller

        Private db As New PetTypeDBContext

        'GET: PetTypes
        Function Index() As ActionResult
            Return View(db.AllPetTypes.ToList())
        End Function

        'GET: PetTypes/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim petType As PetType = db.AllPetTypes.Find(id)
            If IsNothing(petType) Then
                Return HttpNotFound()
            End If
            Return View(petType)
        End Function

        'GET: PetTypes/Create
        Function Create() As ActionResult
            Return View()
        End Function

        'POST: PetTypes/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="PetTypeID,SpeciesID,TypeName,PetSize,PetSolitary,PetIndoors,PetOutdoors,PetWalk,PetDiet,PetImage")> ByVal petType As PetType) As ActionResult
            If ModelState.IsValid Then
                db.AllPetTypes.Add(petType)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(petType)
        End Function

        'GET: PetTypes/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim petType As PetType = db.AllPetTypes.Find(id)
            If IsNothing(petType) Then
                Return HttpNotFound()
            End If
            Return View(petType)
        End Function

        'POST: PetTypes/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="PetTypeID,SpeciesID,TypeName,PetSize,PetSolitary,PetIndoors,PetOutdoors,PetWalk,PetDiet,PetImage")> ByVal petType As PetType) As ActionResult
            If ModelState.IsValid Then
                db.Entry(petType).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(petType)
        End Function

        'GET: PetTypes/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim petType As PetType = db.AllPetTypes.Find(id)
            If IsNothing(petType) Then
                Return HttpNotFound()
            End If
            Return View(petType)
        End Function

        'POST: PetTypes/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim petType As PetType = db.AllPetTypes.Find(id)
            db.AllPetTypes.Remove(petType)
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