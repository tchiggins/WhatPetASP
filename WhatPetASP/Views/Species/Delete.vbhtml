@ModelType WhatPetASP.Species
@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
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
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    End Using
</div>