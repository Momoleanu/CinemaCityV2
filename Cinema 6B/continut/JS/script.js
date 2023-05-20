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
    window.location.href = "alege_locul.html";
  }
  var trailerContainer = document.getElementById("trailer-container");

    // URL-ul încorporat al trailerului video
    var trailerUrl = "https://www.youtube.com/embed/VIDEO_ID";

    // Generați codul iframe pentru încorporarea trailerului
    var iframe = document.createElement("iframe");
    iframe.src = trailerUrl;
    iframe.width = "560";
    iframe.height = "315";
    iframe.frameborder = "0";
    iframe.allowfullscreen = true;

    // Adăugați iframe-ul în containerul trailerului
    trailerContainer.appendChild(iframe);