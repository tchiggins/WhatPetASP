@ModelType IEnumerable(Of WhatPetASP.PetClass)
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
            @Html.DisplayNameFor(Function(model) model.ClassName)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.ClassName)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.PetClassID }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.PetClassID }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.PetClassID })
        </td>
    </tr>
Next

</table>