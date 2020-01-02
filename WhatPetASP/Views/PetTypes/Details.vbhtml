@ModelType WhatPetASP.PetType
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>PetType</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.SpeciesSpeciesID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.SpeciesSpeciesID)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.TypeName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.TypeName)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PetSize)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PetSize)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PetSolitary)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PetSolitary)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PetIndoors)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PetIndoors)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PetOutdoors)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PetOutdoors)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PetWalk)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PetWalk)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PetDiet)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PetDiet)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PetImage)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PetImage)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.PetTypeID }) |
    @Html.ActionLink("Back to List", "Index")
</p>
