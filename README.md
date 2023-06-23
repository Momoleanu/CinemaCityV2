# ProiectIP

CinemaCity v2

Proiect realizat in ASP.NET ce simuleaza un site pentru rezervari de cinema. Utilizatorul intra pe site isi alege un film dintr-o lista ( preluata dintr-o baza de date MSSQL ) dupa care are optiunea 
de a da subscribe sau nu. Este redirectionat catre o pagina de confirmare.


Daca acesta da subscribe va fi notificat pe un cont de email ( emailul pus la inregistrarea biletului) atunci cand adminul adauga sau sterge un film din baza de date ( Implementare Design Pattern Observer + SMTP ).


Adminul are posibilitatea de logare, parola fiind hashuita cu BCrypt. Are access in a adauga sau sterge filme.
