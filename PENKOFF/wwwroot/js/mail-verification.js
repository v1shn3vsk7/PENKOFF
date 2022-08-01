const codeForm = document.getElementById("form-code");
const emailForm = document.getElementById("form-email");
const btn = document.getElementById("btn-email");
btn.onclick = function () {
    emailForm.style.display = "none";
    codeForm.style.display = "block";
}
