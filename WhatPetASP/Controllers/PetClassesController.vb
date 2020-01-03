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
    Public Class PetClassesController
        Inherits System.Web.Mvc.Controller

        Private db As New PetClassDBContext

        'GET: PetClasses
        Function Index() As ActionResult
            Return View(db.AllPetClasses.ToList())
        End Function

        'GET: PetClasses/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim petClass As PetClass = db.AllPetClasses.Find(id)
            If IsNothing(petClass) Then
                Return HttpNotFound()
            End If
            Return View(petClass)
        End Function

        'GET: PetClasses/Create
        Function Create() As ActionResult
            Return View()
        End Function

        'POST: PetClasses/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="PetClassID,ClassName")> ByVal petClass As PetClass) As ActionResult
            If ModelState.IsValid Then
                db.AllPetClasses.Add(petClass)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(petClass)
        End Function

        'GET: PetClasses/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim petClass As PetClass = db.AllPetClasses.Find(id)
            If IsNothing(petClass) Then
                Return HttpNotFound()
            End If
            Return View(petClass)
        End Function

        'POST: PetClasses/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="PetClassID,ClassName")> ByVal petClass As PetClass) As ActionResult
            If ModelState.IsValid Then
                db.Entry(petClass).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(petClass)
        End Function

        'GET: PetClasses/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim petClass As PetClass = db.AllPetClasses.Find(id)
            If IsNothing(petClass) Then
                Return HttpNotFound()
            End If
            Return View(petClass)
        End Function

        'POST: PetClasses/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim petClass As PetClass = db.AllPetClasses.Find(id)
            db.AllPetClasses.Remove(petClass)
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