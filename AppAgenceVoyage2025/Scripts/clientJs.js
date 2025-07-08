$(document).ready(function () {
    loadData();  // Charger les clients au démarrage
});


// Fonction pour charger les clients
function loadData() {
    $.ajax({
        url: "/Clients/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",  
        dataType: "json", 
        success: function (clients) {
            var html = '';
            $.each(clients, function (index, client) {
                html += '<tr>';
                html += '<td>' + client.IdUtilisateur + '</td>';
                html += '<td>' + client.NomUtilisateur + '</td>';
                html += '<td>' + client.PreomUtilisateur + '</td>';
                html += '<td>' + client.EmailUtilisateur + '</td>';
                html += '<td>' + client.TelephoneUtilisateur + '</td>';
                html += '<td>' + client.CNIClient + '</td>';
                html += '<td><a href="#" onclick="return getClientByID(' + client.IdUtilisateur + ')">Modifier</a> | <a href="#" onclick="deleteClient(' + client.IdUtilisateur + ')">Supprimer</a></td>';
                html += '</tr>';
            });
            $(".tbody").html(html);
        },
        error: function () {
            alert("Erreur lors du chargement des clients.");
        }
    });
}

function addClient() {
    if (!validateClient()) return false;  // Validation des champs

    var clientObj = {
        NomUtilisateur: $('#Nom').val(),
        PreomUtilisateur: $('#Prenom').val(),
        EmailUtilisateur: $('#Email').val(),
        TelephoneUtilisateur: $('#Telephone').val(),
        CNIClient: $('#CNI').val()
    };

    $.ajax({
        url: "/Clients/Add",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(clientObj),
        dataType: "json",
        success: function (response) {
            alert(response.message);
            if (response.success) {
                $('#clientModal').modal('hide');
                loadData();
            }
        },
        error: function () {
            alert("Erreur lors de l'ajout du client.");
        }
    });
}
function getClientByID(id) {
    console.log("Client ID:", id); // Vérifiez que l'ID est correct

    $.get("/Clients/GetByID/" + id)
        .done(function (response) {
            console.log("Réponse du serveur :", response); // Affichez la réponse complète

            if (!response || !response.success) {
                alert("Aucune donnée reçue ou réponse invalide.");
                return;
            }

            // Remplir le formulaire avec les données du client
            $('#ClientID').val(response.IdUtilisateur);
            $('#Nom').val(response.NomUtilisateur);
            $('#Prenom').val(response.PreomUtilisateur);
            $('#Email').val(response.EmailUtilisateur);
            $('#Telephone').val(response.TelephoneUtilisateur);
            $('#CNI').val(response.CNIClient);

            // Afficher le modal
            console.log("Affichage du modal...");
            $("#clientModal").modal("show");

            // Masquer le bouton Ajouter et afficher le bouton Modifier
            $("#btnAdd").hide();
            $("#btnUpdate").show();
        })
        .fail(function (error) {
            console.error("Erreur lors de la récupération des données du client :", error);
            alert("Erreur lors de la récupération des données du client.");
        });
}


function updateClient() {
    if (!validateClient()) return false;

    var clientObj = {
        IdUtilisateur: $('#ClientID').val(),
        NomUtilisateur: $('#Nom').val(),
        PreomUtilisateur: $('#Prenom').val(),
        EmailUtilisateur: $('#Email').val(),
        TelephoneUtilisateur: $('#Telephone').val(),
        CNIClient: $('#CNI').val()
    };

    $.ajax({
        url: "/Clients/Update",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(clientObj),
        dataType: "json",
        success: function (response) {
            alert(response.message);
            if (response.success) {
                $("#clientModal").modal("hide");  // Fermer le modal après mise à jour
                loadData();  // Recharger la liste des clients
            }
        },
        error: function () {
            alert("Erreur lors de la mise à jour du client.");
        }
    });
}

function deleteClient(id) {
    if (confirm("Voulez-vous vraiment supprimer ce client ?")) {
        $.ajax({
            url: "/Clients/Delete/" + id,
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (response) {
                alert(response.message);
                if (response.success) {
                    loadData();  // Recharger les clients après la suppression
                }
            },
            error: function () {
                alert("Erreur lors de la suppression du client.");
            }
        });
    }
}

function clearTextBox() {
    $('#ClientID').val('');
    $('#Nom').val('');
    $('#Prenom').val('');
    $('#Email').val('');
    $('#Telephone').val('');
    $('#CNI').val('');

    $(".form-control").css("border-color", "lightgrey");

    $("#btnAdd").show();
    $("#btnUpdate").hide();

    setTimeout(function () {
        $('#clientModal').modal('show');
    }, 100); // Pour tester l'affichage sans les données
}




function validateClient() {
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

function Close() {
    $('#clientModal').modal('hide');
}



