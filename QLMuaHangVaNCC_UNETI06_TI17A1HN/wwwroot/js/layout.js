document.addEventListener("DOMContentLoaded", function () {

    // ==============================
    // Xác nhận khi xóa dữ liệu
    // ==============================

    const deleteButtons = document.querySelectorAll(".btn-delete");

    deleteButtons.forEach(function (button) {

        button.addEventListener("click", function (event) {

            const result = confirm(
                "Bạn có chắc chắn muốn xóa dữ liệu này không?"
            );

            if (!result) {
                event.preventDefault();
            }

        });

    });


    // ==============================
    // Tự động ẩn thông báo
    // ==============================

    const alerts = document.querySelectorAll(".alert-auto-close");

    alerts.forEach(function (alert) {

        setTimeout(function () {

            alert.style.transition = "opacity 0.5s";
            alert.style.opacity = "0";

            setTimeout(function () {
                alert.remove();
            }, 500);

        }, 3000);

    });

});