var isAdmin = true; 

var adminLabel = document.getElementById("admin-label");

if (isAdmin) {
  adminLabel.textContent = "Cont Administrator";
} else {
  adminLabel.textContent = "";
}
function login(event) {
  event.preventDefault();

  var username = document.getElementById("username").value;
  var password = document.getElementById("password").value;

  if (username === "admin" && password === "admin") {
    sessionStorage.setItem("isAdmin", "true"); 
    window.location.href = "index.html";
  } else {
    alert("Numele de utilizator sau parola introduse sunt incorecte. Vă rugăm să încercați din nou.");
  }
}

function loadFilme() {
  fetch('../filme.html')
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
  var movie = document.getElementById("movie").value;
  var tickets = document.getElementById("tickets").value;

  if (movie && tickets) {
    var url = "alege_locul.html?movie=" + encodeURIComponent(movie) + "&tickets=" + encodeURIComponent(tickets);
    window.location.href = url;
  } else {
    alert("Vă rugăm să alegeți un film și să introduceți numărul de bilete.");
  }
}
