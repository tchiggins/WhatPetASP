Imports System.Data.SqlClient
Imports System.IO

Public Class DataSetup

    'Import PetClass data from .csv file
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

    'Import Species data from .csv file
    Shared Function SP_CSVImport(FileName As String) As Boolean
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

    'Import PetType data from .csv file
    Shared Function PT_CSVImport(FileName As String) As Boolean
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
        Next
        Return True
    End Function

    'Clear data from PetClass data table
    Shared Function ClearPetClassTable()
        Dim db As New PetClassDBContext

        db.AllPetClasses.RemoveRange(db.AllPetClasses)
        db.SaveChanges()

        Return True
    End Function

    'Clear data from Species data table
    Shared Function ClearSpeciesTable()
        Dim db As New SpeciesDBContext

        db.AllSpecies.RemoveRange(db.AllSpecies)
        db.SaveChanges()

        Return True
    End Function

    'Clear data from PetType data table
    Shared Function ClearPetTypeTable()
        Dim db As New PetTypeDBContext

        db.AllPetTypes.RemoveRange(db.AllPetTypes)
        db.SaveChanges()

        Return True
    End Function

    'Populate PetClass data table
    Shared Function PopulatePetClassData(Data As PetClass)
        Dim db As New PetClassDBContext
        db.AllPetClasses.Add(Data)
        db.SaveChanges()

        Return True
    End Function

    'Populate PetType data table
    Shared Function PopulatePetTypeData(Data As PetType)
        Dim db As New PetTypeDBContext
        db.AllPetTypes.Add(Data)
        db.SaveChanges()

        Return True
    End Function

    'Populate Species data table
    Shared Function PopulateSpeciesData(Data As Species)
        Dim db As New SpeciesDBContext
        db.AllSpecies.Add(Data)
        db.SaveChanges()

        Return True
    End Function

    'Get PetClassID from the chosen ClassName
    Shared Function GetPetClassID(PCID As String) As Integer
        Dim db As New PetClassDBContext
        Dim MyPetClass = From PetClasses In db.AllPetClasses
                         Where PetClasses.ClassName = PCID
                         Select PetClasses.PetClassID
        Return MyPetClass.FirstOrDefault()
    End Function

    'Get SpeciesID from the chosen SpeciesName
    Shared Function GetSpeciesID(SPID As String) As Integer
        Dim db As New SpeciesDBContext
        Dim MySpecies = From Species In db.AllSpecies
                        Where Species.SpeciesName = SPID
                        Select Species.SpeciesID
        Return MySpecies.FirstOrDefault()
    End Function
End Class