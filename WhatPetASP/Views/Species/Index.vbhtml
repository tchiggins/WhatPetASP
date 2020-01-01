@ModelType IEnumerable(Of WhatPetASP.Species)
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
            @Html.DisplayNameFor(Function(model) model.SpeciesName)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.PetClassID)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.SpeciesName)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.PetClassID)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.SpeciesID }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.SpeciesID }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.SpeciesID })
        </td>
    </tr>
Next

</table>
