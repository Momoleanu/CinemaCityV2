// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function loadFilme() {
    fetch('../filme/')
        .then(response => response.text())
        .then(data => {
            document.getElementById('content').innerHTML = data;
        });
}

function loadCumpara() {
    fetch('../cumpara.html')
        .then(response => response.text())
        .then(data => {
            document.getElementById('content').innerHTML = data;
        });
}
function redirectToAlegeLocul() {
    window.location.href = "alege_locul.html";
}

function rezervaScaun(scaun) {
    if (scaun.className === "scaun_liber") {
        scaun.className = "scaun_selectat";
        console.log("bhbkbbhk");
    } else if (scaun.classList.contains("selectat")) {
        scaun.classList.remove("selectat");
        scaun.classList.add("liber");
    }

}