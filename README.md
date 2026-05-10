# PlanerProduktywnosci-INF04
Nazwa: Planer Produktywności
Technologie: .NET 8, ASP.NET Core Web API, Entity Framework Core, SQL Server, .NET MAUI (Mobile/Desktop).
Cel: System umożliwia użytkownikom efektywne zarządzanie codziennymi zadaniami poprzez wieloplatformową aplikację mobilną/desktopową połączoną z centralną bazą danych za pośrednictwem bezpiecznego interfejsu API.


Instrukcja Uruchamiania:
1. Backend/Mobilna/Desktop

- Uruchom projekt w Visual Studio
- Przed uruchomieniem backendu upewnij się że w appsettings.json Connection string wskazuje
poprawny port (powinien i tak zawsze być localhost:5290)
- Backend musi być w http
- Przed włączeniem mobilnej/desktopowej uruchom Backend
- Po uruchomieniu backendu, ustaw projekt desktopower/mobilnej jako startowy i uruchom
(najlepiej stworzyć profil uruchamiania co włącza wszystko na raz)
2. Webowa

- Uruchom Backend w Visual Studio
- Otwórz projekt najlepiej w Visual Studio Code
- Otwórz terminal
- Upewnij się że masz zainstalowanie wymagane składniki do uruchamiana angulara (npm install)
- wpisz "cd PlanerProduktywności", a potem "cd Web"
- Uruchom aplikację webową przez "ng serve"

(do logowania można użyć User: "Test" pass: "123")