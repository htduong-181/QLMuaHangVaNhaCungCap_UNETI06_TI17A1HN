document.addEventListener("DOMContentLoaded", function () {

    // Tooltip Bootstrap nếu View nào sử dụng data-bs-toggle="tooltip"
    const tooltipList =
        document.querySelectorAll('[data-bs-toggle="tooltip"]');

    [...tooltipList].forEach(element => {
        new bootstrap.Tooltip(element);
    });

});