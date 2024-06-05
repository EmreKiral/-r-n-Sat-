$(document).ready(function () {
    listele();

    $("#kategoriForm").submit(function (e) {
        e.preventDefault();
        if ($("#guncelle").is(":visible")) {
            guncelle();
        } else {
            ekle();
        }
    });

    $("#guncelle").hide(); // Başlangıçta gizli olsun
});

var selectedCategoryId = null;
var baseurl = "http://localhost:5056/";
var gettoken = localStorage.getItem("token");

function listele() {
    $.ajax({
        type: "GET",
        url: baseurl + "api/Coupons",
        contentType: "application/json",
        headers: {
            "Authorization": "Bearer " + gettoken
        },
        success: function (response) {
            var html = "";
            $.each(response, function (index, item) {
                html += "<tr>";
                html += "<td>" + item.name + "</td>";
                html += "<td>" + item.description + "</td>";
                html += "<td>" + item.discount + "</td>";
                html += "<td>" + item.isActive + "</td>";
                html += "<td>";
                html += "<button class='btn btn-primary' onclick='duzenle(\"" + item.id + "\", \"" + item.name + "\", \"" + item.description + "\", \"" + item.discount + "\", \"" + item.isActive + "\")'>Düzenle</button> ";
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
        url: baseurl + "api/Coupons/" + id,
        contentType: "application/json",
        headers: {
            "Authorization": "Bearer " + gettoken
        },
        success: function (response) {
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
    var CouponAddDto = {
        name: $("#name").val(),
        description: $("#description").val(),
        discount: parseFloat($("#discount").val()), // Ensure discount is a number
        isActive: $("#isActive").val() === "true"
    };

    $.ajax({
        type: "POST",
        url: baseurl + "api/Coupons",
        contentType: "application/json",
        data: JSON.stringify(CouponAddDto),
        headers: {
            "Authorization": "Bearer " + gettoken
        },
        success: function (response) {
            $("#kategoriForm")[0].reset(); // Reset the form
            listele(); // Refresh the list
            $("#divResult")
                .show()
                .removeClass()
                .addClass("alert alert-success")
                .html("Kayıt Başarılı!")
                .fadeOut(5000);
        },
        error: function (xhr, status, error) {
            console.error("Error: " + status + " " + error);
        }
    });
}

function duzenle(id, name, description, discount, isActive) {
    selectedCategoryId = id;
    $("#name").val(name);
    $("#description").val(description);
    $("#discount").val(discount);
    $("#isActive").val(isActive === "true" ? "true" : "false");
    $("#ekle").hide();
    $("#guncelle").show();
    console.log("Düzenle button clicked. Category ID: " + id);
    console.log("Name: " + name + ", Description: " + description + ", Discount: " + discount + ", IsActive: " + isActive);
}

function guncelle() {
    var CouponDto = {
        id: selectedCategoryId,
        name: $("#name").val(),
        description: $("#description").val(),
        discount: parseFloat($("#discount").val()), // Ensure discount is a number
        isActive: $("#isActive").val() === "true"
    };

    $.ajax({
        type: "PUT",
        url: baseurl + "api/Coupons",
        contentType: "application/json",
        data: JSON.stringify(CouponDto),
        headers: {
            "Authorization": "Bearer " + gettoken
        },
        success: function (response) {
            $("#kategoriForm")[0].reset(); // Reset the form
            $("#ekle").show();
            $("#guncelle").hide();
            listele(); // Refresh the list
            $("#divResult")
                .show()
                .removeClass()
                .addClass("alert alert-success")
                .html("Güncelleme Başarılı!")
                .fadeOut(5000);
        },
        error: function (xhr, status, error) {
            console.error("Error: " + status + " " + error);
        }
    });
}