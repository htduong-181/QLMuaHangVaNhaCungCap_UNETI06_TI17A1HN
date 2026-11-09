document.addEventListener("DOMContentLoaded", function () {
    const eyeIcon = document.getElementById("eyeIcon");
    const passwordInput = document.getElementById("MatKhau");

    if (eyeIcon && passwordInput) {
        eyeIcon.addEventListener("click", function () {
            if (passwordInput.type === "password") {
                passwordInput.type = "text";
                eyeIcon.textContent = "🙈";
            } else {
                passwordInput.type = "password";
                eyeIcon.textContent = "👁️";
            }
        });
    }
});