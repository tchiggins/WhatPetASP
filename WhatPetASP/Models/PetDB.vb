Imports System.Data.Entity

'Class table (mammal, bird, reptile etc.)
Public Class PetClass
    Public Property PetClassID() As Integer
    Public Property ClassName() As String
End Class

Public Class PetClassDBContext
    Inherits DbContext
    Public Property AllPetClasses() As DbSet(Of PetClass)
End Class

'Species table (cat, dog etc.)
Public Class Species
    Public Property SpeciesID() As Integer
    Public Property SpeciesName() As String
    Public Property PetClassPetClassID As Integer
End Class

Public Class SpeciesDBContext
    Inherits DbContext
    Public Property AllSpecies() As DbSet(Of Species)
End Class

'PetType table (specific breed + characteristics for breed)
Public Class PetType
    Public Property PetTypeID() As Integer
    Public Property SpeciesSpeciesID() As Integer
    Public Property TypeName() As String
    Public Property PetSize() As String
    Public Property PetSolitary() As String
    Public Property PetIndoors() As String
    Public Property PetOutdoors() As String
    Public Property PetWalk() As String
    Public Property PetDiet() As String
    Public Property PetImage() As String
End Class

Public Class PetTypeDBContext
    Inherits DbContext
    Public Property AllPetTypes() As DbSet(Of PetType)
End Class