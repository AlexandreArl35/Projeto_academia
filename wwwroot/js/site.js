// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).on("keyup", ".th-pesquisar input", function () {
    var index = $(this).closest("th").index();

    var nth = $(this).closest("table").find("td:nth-child(" + (index + 1).toString() + ")");
    var valor = $(this).val().toUpperCase();
    $(this).closest("table").find("tbody tr").show();
    $(nth).each(function () {
        if ($(this).text().toUpperCase().indexOf(valor) < 0) {
            $(this).parent().hide();
        }
    });
});
