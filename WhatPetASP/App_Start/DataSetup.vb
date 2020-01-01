Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Public Class DataSetup
    Inherits System.Web.UI.Page
    Private myConn As SqlConnection
    Private Function SendToDB(myConn As SqlConnection, Command As String) As Integer
        Dim myCmd As SqlCommand
        myCmd = myConn.CreateCommand
        myCmd.CommandText = Command
        'Execute the command.
        Dim count As Integer = myCmd.ExecuteNonQuery()
        Return count
    End Function
    Private Function SendToDBReturn(myConn As SqlConnection, Command As String, Item As String) As Guid
        Dim myCmd As SqlCommand
        Dim myReader As SqlDataReader
        Dim results As Guid
        myCmd = myConn.CreateCommand
        myCmd.CommandText = Command
        'Execute the command.
        myReader = myCmd.ExecuteReader()
        'Concatenate the query result into a string.
        Do While myReader.Read()
            results = myReader.Item(Item)
        Loop
        myReader.Close()
        Return results
    End Function
    Public Function PC_CSVImport(FileName As String) As Boolean
        'Create a Connection object
        myConn = New SqlConnection("Initial Catalog=Pets;Data Source=tcp:mssqluk18.prosql.net;User ID=oliver;Password=Vintage12!$;")

        'Connect to DB
        myConn.Open()
        Dim Cmd As String

        'Upload and save the file
        Dim CSVPath As String = Server.MapPath("~/Files/") + Path.GetFileName(FileName)

        'Read the contents of CSV file
        Dim csvData As String = File.ReadAllText(CSVPath)
        Dim i As Integer = 0

        'Execute a loop over the rows
        For Each row As String In csvData.Split(ControlChars.Cr)
            If Not String.IsNullOrEmpty(row) Then
                'Execute a loop over the columns
                For Each cell As String In row.Split(","c)
                    If cell.Length > 1 Then
                        Cmd = "INSERT INTO pets.dbo.PetClass (PetClassID, ClassName) VALUES (NEWID(), '"
                        Cmd += Replace(cell, vbLf, "")
                        Cmd += "')"
                        SendToDB(myConn, Cmd)
                    End If
                    i += 1
                Next
            End If
        Next
        myConn.Close()
        Return True
    End Function
    Public Function S_CSVImport(FileName As String) As Boolean
        'Create a Connection object
        myConn = New SqlConnection("Initial Catalog=Pets;Data Source=tcp:mssqluk18.prosql.net;User ID=oliver;Password=Vintage12!$;")

        'Connect to DB
        myConn.Open()

        Dim Cmd As String
        Dim ID As Guid
        'Upload and save the file  
        Dim CSVPath As String = Server.MapPath("~/Files/") + Path.GetFileName(FileName)

        'Create a DataTable
        Dim dt As New DataTable()
        dt.Columns.AddRange(New DataColumn(1) {New DataColumn("ClassName", GetType(String)), New DataColumn("SpeciesName", GetType(String))})

        'Read the contents of CSV file
        Dim csvData As String = File.ReadAllText(CSVPath)
        Dim cell As String
        Dim colNum As Integer = 0
        Const numCols As Integer = 2
        Dim rowNum As Integer = 0
        'Execute a loop over the rows
        For Each row As String In csvData.Split(ControlChars.Cr)
            If Not String.IsNullOrEmpty(row) Then
                dt.Rows.Add()
                colNum = 0
                'Execute a loop over the columns
                For Each cell In row.Split(","c)
                    If colNum < numCols Then
                        'Fixes problem with linefeed exception
                        dt.Rows(rowNum)(colNum) = Replace(cell, vbLf, "")
                    End If
                    colNum += 1
                Next
            End If
            rowNum += 1
        Next
        For readRow As Integer = 0 To (rowNum - 1) Step 1
            If dt.Rows(readRow)(0).Length > 1 Then
                Cmd = "SELECT TOP (1) PetClassID FROM pets.dbo.PetClass WHERE ClassName = '"
                Cmd += dt.Rows(readRow)(0)
                Cmd += "' ;"
                ID = SendToDBReturn(myConn, Cmd, "PetClassID")
                Cmd = "INSERT INTO pets.dbo.Species (SpeciesID, SpeciesName, PetClassID) VALUES (NEWID(), '"
                Cmd += dt.Rows(readRow)(1)
                Cmd += "', '"
                Cmd += ID.ToString()
                Cmd += "' );"
                SendToDB(myConn, Cmd)
            End If
        Next
        myConn.Close()
        Return True
    End Function
    Public Function PT_CSVImport(FileName As String) As Boolean
        Dim SQLNum As UShort 'Variable to represent column number currently being fed into SQL query

        'Create a Connection object
        myConn = New SqlConnection("Initial Catalog=Pets;Data Source=tcp:mssqluk18.prosql.net;User ID=oliver;Password=Vintage12!$;")

        'Connect to DB
        myConn.Open()

        Dim Cmd As String
        Dim ID As Guid
        'Upload and save the file  
        Dim CSVPath As String = Server.MapPath("~/Files/") + Path.GetFileName(FileName)

        'Create a DataTable
        Dim dt As New DataTable()
        dt.Columns.AddRange(New DataColumn(8) {New DataColumn("SpeciesName", GetType(String)), New DataColumn("TypeName", GetType(String)), New DataColumn("PetSize", GetType(String)), New DataColumn("PetSolitary", GetType(Boolean)), New DataColumn("PetIndoors", GetType(Boolean)), New DataColumn("PetOutdoors", GetType(Boolean)), New DataColumn("PetWalk", GetType(Boolean)), New DataColumn("PetDiet", GetType(String)), New DataColumn("PetImage", GetType(String))})

        'Read the contents of CSV file
        Dim csvData As String = File.ReadAllText(CSVPath)
        Dim cell As String
        Dim colNum As Integer = 0
        Const numCols As Integer = 9
        Dim rowNum As Integer = 0

        'Execute a loop over the rows
        For Each row As String In csvData.Split(ControlChars.Cr)
            If Not String.IsNullOrEmpty(row) Then
                dt.Rows.Add()
                colNum = 0
                'Execute a loop over the columns
                For Each cell In row.Split(","c)
                    If colNum < numCols Then
                        'Fixes problem with linefeed exception
                        dt.Rows(rowNum)(colNum) = Replace(cell, vbLf, "")
                    End If
                    colNum += 1
                Next
            End If
            rowNum += 1
        Next
        For readRow As Integer = 0 To (rowNum - 1) Step 1
            SQLNum = 0
            If dt.Rows(readRow)(0).Length > 1 Then
                Cmd = "SELECT TOP (1) SpeciesID FROM pets.dbo.Species WHERE SpeciesName = '"
                Cmd += dt.Rows(readRow)(SQLNum) 'Column 0
                SQLNum += 1
                Cmd += "' ;"
                ID = SendToDBReturn(myConn, Cmd, "SpeciesID")
                Cmd = "INSERT INTO pets.dbo.PetType (TypeID, SpeciesID, TypeName, PetSize, PetSolitary, PetIndoors, PetOutdoors, PetWalk, PetDiet, PetImage) VALUES (NEWID(), '"
                Cmd += ID.ToString()
                Cmd += "', '"
                Cmd += dt.Rows(readRow)(SQLNum) 'Column 1
                SQLNum += 1
                Cmd += "', '"
                Cmd += dt.Rows(readRow)(SQLNum) 'Column 2
                SQLNum += 1
                Cmd += "', '"
                Cmd += Str(dt.Rows(readRow)(SQLNum)) 'Column 3
                SQLNum += 1
                Cmd += "', '"
                Cmd += Str(dt.Rows(readRow)(SQLNum)) 'Column 4
                SQLNum += 1
                Cmd += "', '"
                Cmd += Str(dt.Rows(readRow)(SQLNum)) 'Column 5
                SQLNum += 1
                Cmd += "', '"
                Cmd += Str(dt.Rows(readRow)(SQLNum)) 'Column 6
                SQLNum += 1
                Cmd += "', '"
                Cmd += dt.Rows(readRow)(SQLNum) 'Column 7
                SQLNum += 1
                Cmd += "', '"
                Cmd += dt.Rows(readRow)(SQLNum) 'Column 8
                Cmd += "' ) ;"
                SendToDB(myConn, Cmd)
            End If
        Next
        Return True
    End Function
End Class