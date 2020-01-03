Imports System.Data.SqlClient
Imports System.IO

Public Class DataSetup
    Shared Function CreateTables() As Boolean
        'Create a Connection object
        Dim myConn = New SqlConnection("Initial Catalog=Pets;Data Source=tcp:mssqluk18.prosql.net;User ID=oliver;Password=Vintage12!$;")

        'Connect to DB
        myConn.Open()

        Dim myCmd As SqlCommand
        Dim Count

        myCmd = myConn.CreateCommand
        myCmd.CommandText = Command()

        'Users table
        'UserID represents the DB's ID code for the user (user will never see)
        'RegisteredDate will track the date upon which that user registered
        'PassHash will store a hashed version of the user's password to add security in case of DB compromise
        myCmd.CommandText = "IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Users')
        PRINT 'Table Exists'
        ELSE CREATE TABLE dbo.Users (UserID uniqueidentifier ROWGUIDCOL NOT NULL PRIMARY KEY,
        FirstName varchar(128) NOT NULL, LastName varchar(128) NOT NULL, Gender varchar (128)
        NOT NULL, Email varchar(128) NOT NULL, RegisteredDate datetime NOT NULL,
        PassHash varchar(512) NOT NULL);"

        'Execute the command.
        Count = myCmd.ExecuteNonQuery()

        'Class table (mammal, bird, reptile etc.)
        myCmd.CommandText = "IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'PetClass')
        PRINT 'Table Exists'
        ELSE CREATE TABLE dbo.PetClass (PetClassID uniqueidentifier ROWGUIDCOL NOT NULL PRIMARY KEY,
        ClassName varchar(128) NOT NULL);"

        'Execute the command.
        Count = myCmd.ExecuteNonQuery()

        'Species table (cat, dog etc.)
        myCmd.CommandText = "IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Species')
        PRINT 'Table Exists'
        ELSE CREATE TABLE dbo.Species (SpeciesID uniqueidentifier ROWGUIDCOL NOT NULL PRIMARY KEY,
        SpeciesName varchar(128) NOT NULL,
        PetClassID uniqueidentifier NOT NULL);"

        'Execute the command.
        Count = myCmd.ExecuteNonQuery()

        'PetType table (characteristics of specific breed)
        'PetSize will work on values of either Small, Average, or Large (as determined by the average for that particular species)
        myCmd.CommandText = "IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'PetType')
        PRINT 'Table Exists'
        ELSE CREATE TABLE dbo.PetType (TypeID uniqueidentifier ROWGUIDCOL NOT NULL PRIMARY KEY,
        SpeciesID uniqueidentifier NOT NULL, TypeName varchar(128) NOT NULL, PetSize varchar(128) NOT NULL,
        PetSolitary varchar(128) NOT NULL, PetIndoors varchar(128) NOT NULL, PetOutdoors varchar(128) NOT NULL,
        PetWalk varchar(128) NOT NULL, PetDiet varchar(128) NOT NULL, PetImage varchar(512) NOT NULL);"

        'Execute the command.
        Count = myCmd.ExecuteNonQuery()

        Return True
    End Function

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
    Shared Function PC_CSVImport(FileName As String) As Boolean
        'Upload and save the file
        Dim CSVPath As String = HttpContext.Current.Server.MapPath("~/Files/") + Path.GetFileName(FileName)

        'Read the contents of CSV file
        Dim csvData As String = File.ReadAllText(CSVPath)

        'Execute a loop over the rows
        For Each row As String In csvData.Split(ControlChars.Cr)
            If Not String.IsNullOrEmpty(row) Then
                'Execute a loop over the columns
                For Each cell As String In row.Split(","c)
                    If cell.Length > 1 Then
                        Dim PetClassData As New PetClass With {
                            .ClassName = Replace(cell, vbLf, "")
                        }
                        DataSetup.PopulatePetClassData(PetClassData)
                    End If
                Next
            End If
        Next
        Return True
    End Function
    Shared Function S_CSVImport(FileName As String) As Boolean
        'Upload and save the file  
        Dim CSVPath As String = HttpContext.Current.Server.MapPath("~/Files/") + Path.GetFileName(FileName)

        'Create a DataTable
        Dim dt As New DataTable()
        dt.Columns.AddRange(New DataColumn(1) {New DataColumn("ClassName", GetType(String)), New DataColumn("SpeciesName", GetType(String))})

        'Read the contents of CSV file
        Dim csvData As String = File.ReadAllText(CSVPath)
        Dim cell As String
        Dim colNum As Integer = 0
        Const numCols As Integer = 2
        'Execute a loop over the rows
        For Each row As String In csvData.Split(ControlChars.Cr)
            If Not String.IsNullOrEmpty(row) Then
                dt.Rows.Add()
                colNum = 0
                'Execute a loop over the columns
                For Each cell In row.Split(","c)
                    If colNum < numCols Then
                        Dim SpeciesData As New Species With {
                            .SpeciesName = Replace(cell, vbLf, "")
                        }
                        DataSetup.PopulateSpeciesData(SpeciesData)
                    End If
                    colNum += 1
                Next
            End If
        Next
        Return True
    End Function
    Shared Function PT_CSVImport(FileName As String) As Boolean
        Dim SQLNum As UShort 'Variable to represent column number currently being fed into SQL query

        'Create a Connection object
        Dim myConn = New SqlConnection("Initial Catalog=Pets;Data Source=tcp:mssqluk18.prosql.net;User ID=oliver;Password=Vintage12!$;")

        'Connect to DB
        myConn.Open()

        Dim Cmd As String
        Dim ID As Guid
        'Upload and save the file  
        Dim CSVPath As String = HttpContext.Current.Server.MapPath("~/Files/") + Path.GetFileName(FileName)

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
                        Dim PetTypeData As New PetType With {
                            .TypeName = Replace(cell, vbLf, "")
                        }
                        DataSetup.PopulatePetTypeData(PetTypeData)
                    End If
                    colNum += 1
                Next
            End If
            rowNum += 1
        Next
        Return True
    End Function

    Shared Function ClearPetClassTable()
        Dim db As New PetClassDBContext

        db.AllPetClasses.RemoveRange(db.AllPetClasses)
        db.SaveChanges()

        Return True
    End Function
    Shared Function ClearSpeciesTable()
        Dim db As New SpeciesDBContext

        db.AllSpecies.RemoveRange(db.AllSpecies)
        db.SaveChanges()

        Return True
    End Function
    Shared Function ClearPetTypeTable()
        Dim db As New PetTypeDBContext

        db.AllPetTypes.RemoveRange(db.AllPetTypes)
        db.SaveChanges()

        Return True
    End Function
    Shared Function PopulatePetClassData(Data As PetClass)
        Dim db As New PetClassDBContext
        db.AllPetClasses.Add(Data)
        db.SaveChanges()

        Return True
    End Function
    Shared Function PopulatePetTypeData(Data As PetType)
        Dim db As New PetTypeDBContext
        db.AllPetTypes.Add(Data)
        db.SaveChanges()

        Return True
    End Function
    Shared Function PopulateSpeciesData(Data As Species)
        Dim db As New SpeciesDBContext
        db.AllSpecies.Add(Data)
        db.SaveChanges()

        Return True
    End Function

    Shared Function GetPetClassID(PCID As String) As Integer
        Dim db As New PetClassDBContext
        Dim MyPetClass = From PetClasses In db.AllPetClasses
                         Where PetClasses.ClassName = PCID
                         Select PetClasses.PetClassID
        Return MyPetClass.FirstOrDefault()
    End Function

    Shared Function GetSpeciesID(SPID As String) As Integer
        Dim db As New SpeciesDBContext
        Dim MySpecies = From Species In db.AllSpecies
                        Where Species.SpeciesName = SPID
                        Select Species.SpeciesID
        Return MySpecies.FirstOrDefault()
    End Function
End Class