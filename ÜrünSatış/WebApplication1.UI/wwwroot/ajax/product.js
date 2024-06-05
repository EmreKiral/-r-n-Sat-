$(document).ready(function () {
    listele();
    resimlistele();
    kategorilistele();

    $("#kategoriForm").submit(function (e) {
        e.preventDefault();
        var visable = $("#guncelle").is(":visible");
        if (visable) {
            alert("Günnceniyor");
            guncelle();
            
  

        } else {
            alert("Ekleniyor");
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
        url: baseurl + "api/Products/GetList",
        contentType: "application/json",
        headers: {
            "Authorization": "Bearer " + gettoken
        },
        success: function (response) {
            console.log(response);
            var html = "";
            $.each(response, function (index, item) {
                html += "<tr>";
                html += "<td>" + item.id + "</td>";
                html += "<td>" + item.category.name + "</td>";
                html += "<td>" + item.productSpecs.id + "</td>";
                html += "<td>" + item.name + "</td>";
                html += "<td>" + item.price + "</td>";
                html += "<td>" + item.isActive + "</td>";
                html += "<td>";
                html += "<button class='btn btn-primary' onclick='duzenle(\"" + item.id + "\", \"" + item.category.name + "\", \"" + item.productSpecs.id + "\",\"" + item.name + "\",\"" + item.price + "\",\"" + item.isActive + "\")'>Düzenle</button> ";
                html += "<button class='btn btn-danger' onclick='sil(\"" + item.id + "\")'>Sil</button>";
                html += "</td>";
                html += "</tr>";
            });
            $("#urunlistesi").html(html);
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
    var ProductAddDto = {
        name: $("#name").val(),
        isActive: $("#isActive").val() === "true",
        categoryId: $("#kategoriListesi").val(),
        productSpecsId: $("#resimListesi").val(),
        price: parseFloat($("#fiyat").val())
    };

    $.ajax({
        type: "POST",
        url: baseurl + "api/Products", // Assuming the endpoint for products is "api/Products"
        contentType: "application/json",
        data: JSON.stringify(ProductAddDto),
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
                .html("Kayıt Başarılı Login Sayfasına Yönelndiriliyorsunuz.!")
                .fadeOut(5000);
        },
        error: function (xhr, status, error) {
            console.error("Error: " + status + " " + error);
        }
    });
}
function duzenle(id, name, isActive, price, categoryId, productSpecsId) {
    selectedCategoryId = id;
    $("#name").val(name);
    $("#isActive").val(isActive === "true" ? "true" : "false");
    $("#fiyat").val(price);
    $("#kategoriListesi").val(categoryId);
    $("#resimListesi").val(productSpecsId);
    $("#ekle").hide();
    $("#guncelle").show();
    console.log("Düzenle button clicked. Category ID: " + id);
    console.log("Name: " + name + ", IsActive: " + isActive);
}

function guncelle() {
    var productDto = {
        id: selectedCategoryId,
        name: $("#name").val(),
        isActive: $("#isActive").val() === "true",
        categoryId: $("#kategoriListesi").val(),
        productSpecsId: $("#resimListesi").val(),
        price: parseFloat($("#fiyat").val()),
    };

    $.ajax({
        type: "PUT",
        url: baseurl + "api/Products",
        contentType: "application/json",
        data: JSON.stringify(productDto),
        headers: {
            "Authorization": "Bearer " + gettoken
        },
        success: function (response) {
            console.log(response);
            $("#kategoriForm")[0].reset(); // Reset the form
            $("#ekle").show();
            $("#guncelle").hide();
            listele(); // Refresh the list
        },
        error: function (xhr, status, error) {
            console.error("Error: " + status + " " + error);
        }
    });
}

function kategorilistele() {
    $.ajax({
        type: "GET",
        url: baseurl + "api/Categories",
        contentType: "application/json",
        headers: {
            "Authorization": "Bearer " + gettoken
        },
        success: function (response) {
            // console.log("Response received:", response);
            var $kategoriListesi = $("#kategoriListesi");
            if ($kategoriListesi.length === 0) {
                console.error("#kategoriListesi element not found.");
                return;
            }
            $kategoriListesi.empty(); // Clear existing options
            $kategoriListesi.append("<option value=''>Kategori Seçin</option>");
            $.each(response, function (index, item) {
                // console.log("Appending item:", item);
                $kategoriListesi.append("<option value='" + item.id + "'>" + item.name + "</option>");
            });
        },
        error: function (xhr, status, error) {
            console.error("Error: " + status + " " + error);
        }
    });
}
function resimlistele() {
    $.ajax({
        type: "GET",
        url: baseurl + "api/ProductSpecs",
        contentType: "application/json",
        headers: {
            "Authorization": "Bearer " + gettoken
        },
        success: function (response) {
       
            // console.log("Response received:", response);
            var $resimListesi = $("#resimListesi");
            if ($resimListesi.length === 0) {
                console.error("#kategoriListesi element not found.");
                return;
            }
            $resimListesi.empty(); // Clear existing options
            $resimListesi.append("<option value=''>Resim Seçin</option>");
            $.each(response, function (index, item) {
                // console.log("Appending item:", item);
                $resimListesi.append("<option value='" + item.id + "'>" + item.id + "</option>");
            });
        },
        error: function (xhr, status, error) {
            console.error("Error: " + status + " " + error);
        }
    });
}


