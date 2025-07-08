$(document).ready(function () {
    loadData();
});


// Charger les voyages
function loadData() {
    $.ajax({
        url: "/Voyages/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (voyages) {
            var html = '';
            $.each(voyages, function (index, voyage) {
                // Extraire le timestamp de la date
                var dateDebutTimestamp = voyage.DateDebut.match(/\/Date\((\d+)\)\//);
                var dateFinTimestamp = voyage.DateFin.match(/\/Date\((\d+)\)\//);

                // Convertir le timestamp en objet Date
                var dateDebut = dateDebutTimestamp ? new Date(parseInt(dateDebutTimestamp[1])) : null;
                var dateFin = dateFinTimestamp ? new Date(parseInt(dateFinTimestamp[1])) : null;

                // Formater les dates (par exemple, YYYY-MM-DD)
                var formattedDateDebut = dateDebut ? dateDebut.toISOString().split('T')[0] : '';
                var formattedDateFin = dateFin ? dateFin.toISOString().split('T')[0] : '';

                html += '<tr>';
                html += '<td>' + voyage.IdVoyage + '</td>';
                html += '<td>' + voyage.Destination + '</td>';
                html += '<td>' + formattedDateDebut + '</td>';
                html += '<td>' + formattedDateFin + '</td>';
                html += '<td>' + voyage.Prix + '</td>';
                html += '<td><a href="#" onclick="return getVoyageByID(' + voyage.IdVoyage + ')">Modifier</a> | <a href="#" onclick="deleteVoyage(' + voyage.IdVoyage + ')">Supprimer</a></td>';
                html += '</tr>';
            });
            $(".tbody").html(html);
        },
        error: function () {
            alert("Erreur lors du chargement des voyages.");
        }
    });
}



// Ajouter un voyage
function addVoyage() {
    if (!validateVoyage()) return false;

    var voyageObj = {
        Destination: $('#Destination').val(),
        DateDebut: $('#DateDebut').val(),
        DateFin: $('#DateFin').val(),
        Prix: $('#Prix').val()
    };

    $.ajax({
        url: "/Voyages/Add",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(voyageObj),
        dataType: "json",
        success: function (response) {
            alert(response.message);
            if (response.success) {
                $('#voyageModal').modal('hide');
                loadData();
            }
        },
        error: function () {
            alert("Erreur lors de l'ajout du voyage.");
        }
    });
}

// Modifier un voyage
function getVoyageByID(id) {
    $.get("/Voyages/GetByID/" + id)
        .done(function (response) {
            if (!response || !response.success) {
                alert("Voyage non trouvé.");
                return;
            }

            $('#VoyageID').val(response.IdVoyage);
            $('#Destination').val(response.Destination);
            $('#DateDebut').val(response.DateDebut);
            $('#DateFin').val(response.DateFin);
            $('#Prix').val(response.Prix);

            $("#btnAdd").hide();
            $("#btnUpdate").show();
            $("#voyageModal").modal("show");
        })
        .fail(function () {
            alert("Erreur lors de la récupération du voyage.");
        });
}

// Mettre à jour un voyage
function updateVoyage() {
    if (!validateVoyage()) return false;

    var voyageObj = {
        IdVoyage: $('#VoyageID').val(),
        Destination: $('#Destination').val(),
        DateDebut: $('#DateDebut').val(),
        DateFin: $('#DateFin').val(),
        Prix: $('#Prix').val()
    };

    $.ajax({
        url: "/Voyages/Update",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(voyageObj),
        dataType: "json",
        success: function (response) {
            alert(response.message);
            if (response.success) {
                $("#voyageModal").modal("hide");
                loadData();
            }
        },
        error: function () {
            alert("Erreur lors de la mise à jour du voyage.");
        }
    });
}

// Supprimer un voyage
function deleteVoyage(id) {
    if (confirm("Voulez-vous vraiment supprimer ce voyage ?")) {
        $.ajax({
            url: "/Voyages/Delete/" + id,
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

function clearTextBox() {
    $('#VoyageID, #Destination, #DateDebut, #DateFin, #Prix').val('');
    $("#btnAdd").show();
    $("#btnUpdate").hide();
    $('#voyageModal').modal('show');
}

function validateVoyage() {
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
