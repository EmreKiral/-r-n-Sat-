$(document).ready(function () {
    listele();

    document.getElementById('productImage1').addEventListener('change', function(event) {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function(e) {
                const base64String = e.target.result;
                document.getElementById('preview1').src = base64String;
            };
            reader.readAsDataURL(file);
        }
    });


    document.getElementById('productImage2').addEventListener('change', function(event) {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function(e) {
                const base64String = e.target.result;
                document.getElementById('preview2').src = base64String;
            };
            reader.readAsDataURL(file);
        }
    });


    document.getElementById('productImage3').addEventListener('change', function(event) {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function(e) {
                const base64String = e.target.result;
                document.getElementById('preview3').src = base64String;
            };
            reader.readAsDataURL(file);
        }
    });


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
        url: baseurl + "api/ProductSpecs",
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
                html += "<td><img src="+ item.productImage1 + " class='product-image' width='100'></td>";
                html += "<td><img src="+ item.productImage2 + " class='product-image' width='100'></td>";
                html += "<td><img src="+ item.productImage3 + " class='product-image' width='100'></td>";
                html += "<td>";
                html += "<button class='btn btn-primary' onclick='duzenle(\"" + item.id + "\", \"" + item.productImage1 + "\", \"" + item.productImage2 + "\", \"" + item.productImage3 + "\")'>Düzenle</button> ";
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
        url: baseurl + "api/ProductSpecs/" + id,
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
  
    var ProductSpecsAddDto = {
        productImage1: $("#preview1").attr("src"),
        productImage2: $("#preview2").attr("src"),
        productImage3: $("#preview3").attr("src"),
    };



    $.ajax({
        type: "POST",
        url: baseurl + "api/ProductSpecs",
        contentType: "application/json",
        data: JSON.stringify(ProductSpecsAddDto),
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
                .html("Kayıt Başarılı!")
                .fadeOut(5000);
        },
        error: function (xhr, status, error) {
            console.error("Error: " + status + " " + error);
        }
    });
}

function duzenle(id, preview1, preview2, preview3) {
    selectedCategoryId = id;
    $("#preview1").attr("src", preview1);
    $("#preview2").attr("src", preview2);
    $("#preview3").attr("src", preview3);
    $("#ekle").hide();
    $("#guncelle").show();
    console.log("Düzenle button clicked. Category ID: " + id);
    console.log("Düzenle button clicked. Category preview1: " + preview1);
    console.log("Düzenle button clicked. Category preview2: " + preview2);
    console.log("Düzenle button clicked. Category preview3: " + preview3);
  
}

function guncelle() {
    var ProductSpecsDto = {
        productImage1: $("#preview1").attr("src"),
        productImage2: $("#preview2").attr("src"),
        productImage3: $("#preview3").attr("src"),
        id: selectedCategoryId,
    };
    $.ajax({
        type: "PUT",
        url: baseurl + "api/ProductSpecs",
        contentType: "application/json",
        data: JSON.stringify(ProductSpecsDto),
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
