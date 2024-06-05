$(document).ready(function () {
    listele();

    $("#kategoriForm").submit(function (e) {
        e.preventDefault();
        var visable = $("#guncelle").is(":visible");
        if (visable) {
       
            guncelle();
            
  

        } else {
    
            ekle();
        }
    });
});

var selectedCategoryId = null;
var baseurl = "http://localhost:5056/";
var gettoken = localStorage.getItem("token");

function listele() {
    $.ajax({
        type: "GET",
        url: baseurl + "api/Categories",
        contentType: "application/json",
        headers: {
            "Authorization": "Bearer " + gettoken
        },
        success: function (response) {
            console.log(response);
            var html = "";
            $.each(response, function (index, item) {
                html += "<tr>";
                html += "<td>" + item.name + "</td>";
                html += "<td>" + item.isActive + "</td>";
                html += "<td>";
                html += "<button class='btn btn-primary' onclick='duzenle(\"" + item.id + "\", \"" + item.name + "\", \"" + item.isActive + "\")'>Düzenle</button> ";
                html += "<button class='btn btn-danger' onclick='sil(\"" + item.id + "\")'>Sil</button>";
                html += "</td>";
                html += "</tr>";
            });
            $("#kategoriListesi").html(html);
        },
        error: function (xhr, status, error) {
            console.error("Error: " + status + " " + error);
        }
    });
}

function sil(id) {
    $.ajax({
        type: "DELETE",
        url: baseurl + "api/Categories/" + id,
        contentType: "application/json",
        headers: {
            "Authorization": "Bearer " + gettoken
        },
        success: function (response) {
            console.log(response);
            listele(); // Refresh the list after deletion
            $("#divResult")
            .show()
            .removeClass()
            .addClass("alert alert-success")
            .html("Silme Başarılı")
            .fadeOut(5000);
        },
        error: function (xhr, status, error) {
            console.error("Error: " + status + " " + error);
        }
    });
}

function ekle() {
    var CategoryAddDto = {
        name: $("#name").val(),
        isActive: $("#isActive").val() === "true"
    };

    $.ajax({
        type: "POST",
        url: baseurl + "api/Categories",
        contentType: "application/json",
        data: JSON.stringify(CategoryAddDto),
        headers: {
            "Authorization": "Bearer " + gettoken
        },
        success: function (response) {
            console.log(response);
            $("#kategoriForm")[0].reset(); // Reset the form
            listele(); // Refresh the list
            $("#divResult")
                .show()
                .removeClass()
                .addClass("alert alert-success")
                .html("Kayıt Başarılı")
                .fadeOut(5000);
        },
        error: function (xhr, status, error) {
            console.error("Error: " + status + " " + error);
        }
    });
}

function duzenle(id, name, isActive) {
    selectedCategoryId = id;
    $("#name").val(name);
    $("#isActive").val(isActive === "true" ? "true" : "false");
    $("#ekle").hide();
    $("#guncelle").show();
    console.log("Düzenle button clicked. Category ID: " + id);
    console.log("Name: " + name + ", IsActive: " + isActive);
}

function guncelle() {
    var CategoryDto = {
        id: selectedCategoryId,
        name: $("#name").val(),
        isActive: $("#isActive").val() === "true"
    };

    $.ajax({
        type: "PUT",
        url: baseurl + "api/Categories",
        contentType: "application/json",
        data: JSON.stringify(CategoryDto),
        headers: {
            "Authorization": "Bearer " + gettoken
        },
        success: function (response) {
            console.log(response);
            $("#kategoriForm")[0].reset(); // Reset the form
            $("#ekle").show();
            $("#guncelle").hide();
            listele(); // Refresh the list
            $("#divResult")
            .show()
            .removeClass()
            .addClass("alert alert-success")
            .html("Güncelleme Başarılı")
            .fadeOut(5000);
        },
        error: function (xhr, status, error) {
            console.error("Error: " + status + " " + error);
        }
    });
}
