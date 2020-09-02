## Web Application Template On .Net Core 3.1 Using Clean Architecture 👋
The application comprises of complete user and role management dynamically handling the navigatino menus and roles together with user.
I made this template on my learning phase of the clear architecture, onion architecture , due to which i might have some implementations or assumptions wrongly made, i wish to receive lots of suggestion so as to make the template better further. You can reach me via my email address tuladhar.rashik@gmail.com

## Features

- 🏗 Built On .Net Core 3.1 - Complete User Management Module ( User Management And Dynamic Role Management)
- 🌱 Authentication Handled Using Asp.Net Core Identity , Identity Seeding
- 👯 Onion Architecure, Clean Architecture, Repository Pattern
- 🏝 Bootstrap 4, Free Version of Ablepro Theme Used, Sweet Alert, Toastr, Datatable
- 📫 Automapper, Serilog With Seq, Entity Framework + Dapper 

## How To
Since the application uses both the ef core and dapper as orm, i have used the ef core just to handle the identity part of the application, all the other functionality has been handled using dapper. Used the practice since one can use any orm on the basis of personal preference.

To use the application please follow the steps below
- First edit the appsettings.json file with the connection string of your database
- Open of the package manager console and select the project to Infrastructure\Infrastructure.Authentication
- Use the command update-databse
- Please add a new user on authusers table with username SUPERUSER and some default password.
- Run 2 Stored Procedures that is under the folder : DatabaseScripts on the database
- Now we are good to go

## Settings
- Under appsettings.json file you can customize serilog configuration as per your need
- Under appsettings.json and ApplicationData Node you can customize the root url, Company details and default password as well


### Connect with me:

[<img align="left" alt="Rashik Tuladhar" width="22px" src="https://raw.githubusercontent.com/iconic/open-iconic/master/svg/globe.svg" />][website]
[<img align="left" alt="Rashik Tuladhar | Twitter" width="22px" src="https://cdn.jsdelivr.net/npm/simple-icons@v3/icons/twitter.svg" />][twitter]
[<img align="left" alt="Rashik Tuladhar | LinkedIn" width="22px" src="https://cdn.jsdelivr.net/npm/simple-icons@v3/icons/linkedin.svg" />][linkedin]
[<img align="left" alt="Rashik Tuladhar | Instagram" width="22px" src="https://cdn.jsdelivr.net/npm/simple-icons@v3/icons/instagram.svg" />][instagram]

---

[website]: https://blog.rashik.com.np
[twitter]: https://twitter.com/RashikTuladhar
[instagram]: https://www.instagram.com/rashiktuladhar/
[linkedin]: https://www.linkedin.com/in/rashiktuladhar/
