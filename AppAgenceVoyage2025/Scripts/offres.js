$(document).ready(function () {
    loadData();
});

// Charger les offres
function loadData() {
    $.ajax({
        url: "/Offres/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (offres) {
            var html = '';
            $.each(offres, function (index, offre) {
                html += '<tr>';
                html += '<td>' + offre.IdOffre + '</td>';
                html += '<td>' + offre.DescriptionOffre + '</td>';
                html += '<td>' + offre.PrixJourOffre + '</td>';
                html += '<td>' + offre.Disponibilite + '</td>';
                html += '<td>' + (offre.Agence ? offre.Agence.AdresseAgence : "Non attribuée") + '</td>';
                html += '<td><button type="button" class="btn btn-warning" onclick="getOffreByID(' + offre.IdOffre + ')">Modifier</button> | <button type="button" class="btn btn-danger" onclick="deleteOffre(' + offre.IdOffre + ')">Supprimer</button></td>';
                html += '</tr>';
            });
            $("#offresTableBody").html(html);  // Remplir le tableau avec les offres récupérées
        },
        error: function () {
            alert("Erreur lors du chargement des offres.");
        }
    });
}

// Ajouter une offre
function addOffre() {
    if (!validateOffre()) return false;

    var offreObj = {
        DescriptionOffre: $('#Description').val(),
        PrixJourOffre: $('#PrixJour').val(),
        Disponibilite: $('#Disponibilite').val(),
        IdAgence: $('#IdAgence').val()
    };

    $.ajax({
        url: "/Offres/Add",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(offreObj),
        dataType: "json",
        success: function (response) {
            alert(response.message);
            if (response.success) {
                $('#offreModal').modal('hide');
                loadData();
            }
        },
        error: function () {
            alert("Erreur lors de l'ajout de l'offre.");
        }
    });
}

// Modifier une offre
function getOffreByID(id) {
    $.get("/Offres/GetByID/" + id)
        .done(function (response) {
            if (!response || !response.success) {
                alert("Offre non trouvée.");
                return;
            }

            $('#OffreID').val(response.offre.IdOffre);
            $('#Description').val(response.offre.DescriptionOffre);
            $('#PrixJour').val(response.offre.PrixJourOffre);
            $('#Disponibilite').val(response.offre.Disponibilite);
            $('#IdAgence').val(response.offre.IdAgence);  // Sélectionner l'agence de l'offre

            $("#btnAdd").hide();
            $("#btnUpdate").show();
            $("#offreModal").modal("show");
        })
        .fail(function () {
            alert("Erreur lors de la récupération de l'offre.");
        });
}

// Mettre à jour une offre
function updateOffre() {
    if (!validateOffre()) return false;

    var offreObj = {
        IdOffre: $('#OffreID').val(),
        DescriptionOffre: $('#Description').val(),
        PrixJourOffre: $('#PrixJour').val(),
        Disponibilite: $('#Disponibilite').val(),
        IdAgence: $('#IdAgence').val()
    };

    $.ajax({
        url: "/Offres/Update",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(offreObj),
        dataType: "json",
        success: function (response) {
            alert(response.message);
            if (response.success) {
                $("#offreModal").modal("hide");
                loadData();
            }
        },
        error: function () {
            alert("Erreur lors de la mise à jour de l'offre.");
        }
    });
}

// Supprimer une offre
function deleteOffre(id) {
    if (confirm("Voulez-vous vraiment supprimer cette offre ?")) {
        $.ajax({
            url: "/Offres/Delete/" + id,
            type: "POST",
            success: function (response) {
                alert(response.message);
                if (response.success) {
                    loadData();
                }
            }
        });
    }
}

// Réinitialiser le formulaire
function clearTextBox() {
    $('#OffreID, #Description, #PrixJour, #Disponibilite').val('');
    $('#IdAgence').val($("#IdAgence option:first").val()); // Sélectionner la première agence par défaut
    $("#btnAdd").show();
    $("#btnUpdate").hide();
    $('#offreModal').modal('show');
}

// Validation des champs du formulaire
function validateOffre() {
    let isValid = true;
    $(".form-control").each(function () {
        if ($(this).val().trim() === "") {
            $(this).css("border-color", "red");
            isValid = false;
        } else {
            $(this).css("border-color", "lightgrey");
        }
    });
    return isValid;
}
