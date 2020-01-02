@ModelType WhatPetASP.Species
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>Species</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.SpeciesName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.SpeciesName)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PetClassPetClassID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PetClassPetClassID)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.SpeciesID }) |
    @Html.ActionLink("Back to List", "Index")
</p>
