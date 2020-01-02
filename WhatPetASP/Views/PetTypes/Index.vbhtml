@ModelType IEnumerable(Of WhatPetASP.PetType)
@Code
ViewData("Title") = "Index"
End Code

<h2>Index</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.SpeciesSpeciesID)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.TypeName)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.PetSize)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.PetSolitary)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.PetIndoors)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.PetOutdoors)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.PetWalk)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.PetDiet)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.PetImage)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.SpeciesSpeciesID)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.TypeName)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.PetSize)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.PetSolitary)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.PetIndoors)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.PetOutdoors)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.PetWalk)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.PetDiet)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.PetImage)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.PetTypeID }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.PetTypeID }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.PetTypeID })
        </td>
    </tr>
Next

</table>
