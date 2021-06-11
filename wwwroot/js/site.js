// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function abrirModal(cerrar) {
    $('#modal1').css('background-color', 'transparent');
    $('#modal1').css('width', '90px');
    $('#modal1').css('height', '70px');
    $('#modal1').css('margin-top', '15%');
    $('#modal1').css('box-shadow', 'none');

    var elem = document.querySelector('#modal1');
    var instance = M.Modal.getInstance(elem);
    instance.open();

    if (cerrar)
        setTimeout(function () { instance.close() }, 5000);
};