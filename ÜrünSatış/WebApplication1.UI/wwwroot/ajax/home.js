$(document).ready(function () {
    listele();
});
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
                html += `
                <div class="col-md-4">
                    <div class="card mb-4 shadow-sm">
                        <div class="slick-slider">
                            <img src="${item.productSpecs.productImage1}" class="card-img-top" alt="${item.name}">
                           
                        </div>
                        <div class="card-body">
                            <h5 class="card-title">${item.name}</h5>
                            <p class="card-text">${item.category.name}</p>
                            <p class="card-text">Price: ${item.price}₺</p>
                            <p class="card-text">Active: ${item.isActive ? 'Yes' : 'No'}</p>
                        </div>
                    </div>
                </div>`;
            });
            $("#urunListesi").html(html);

            // Slick slider'ı etkinleştirme
            $('.slick-slider').slick({
                slidesToShow: 1,
                slidesToScroll: 1,
                autoplay: true,
                autoplaySpeed: 2000,
                dots: true,
                arrows: false
            });
        },
        error: function (xhr, status, error) {
            console.error("Error: " + status + " " + error);
        }
    });
}