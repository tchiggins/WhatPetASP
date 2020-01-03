@ModelType WhatPetASP.PetClass
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>PetClass</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.ClassName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.ClassName)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.PetClassID }) |
    @Html.ActionLink("Back to List", "Index")
</p>