# ChessBurgas64
 
Web application with ASP.NET Core designed for chess club "Burgas 64". This is my first ASP.NET Core project and it corresponds to the 2020 September SoftUni ASP.NET Core course final defense project task.

https://chessburgas64.azurewebsites.net/

## Functionalities:
* Register / Login;
* Download, edit and delete personal data for each user;
* Create, edit and delete training groups (admins and trainers only);
* Add and remove students from training groups (admins and trainers only);
* Create, edit and delete lessons (admins and trainers only);
* Add and remove lessons from training groups (admins and trainers only);
* Mark student attendances for every lesson (admins and trainers only);
* Edit, delete, search and order by selected column all table data (thanks to jQuery Datatables);
* Email Sending (thanks to SendGrid);
* Formatting text (thanks to Trumbowyg WYSIWYG editor and HTML Sanitizer);
* Responsive design (thanks to Bootstrap);
* Upload, edit and delete announcements with multiple images, different categories and formatted text (admins and trainers only);
* Upload, edit and delete puzzles (admins and trainers only) with position image, difficulty, points (according to difficulty), category, objective and solution (visible for trainers only);
* Upload, edit and delete videos (admins and trainers only) with embedded link, title, description, category;
* Search by categories and text for announcements, puzzles and videos;


## Used technologies
* ASP.NET Core 6.0;
* AutoMapper;
* Bootstrap 5.0.2;
* Entity Framework Core;
* HTML Sanitizer;
* JavaScript;
* jQuery Datatables;
* Microsoft Azure;
* Moment.js;
* Moq;
* MSSQL;
* Open Street Maps;
* reCAPTCHA;
* SendGrid;
* Trumbowyg WYSIWYG editor;
* XUnit;

## Database Diagram
![image](https://user-images.githubusercontent.com/64807656/169153259-7ad9be15-a286-4e50-b391-b4f1ff6d93e4.png)

## Screenshots:
Home View (Guest View)
![image](https://user-images.githubusercontent.com/64807656/169145499-98001430-91d3-41ee-8789-15d7715eeef2.png)

Announcements
![image](https://user-images.githubusercontent.com/64807656/169146255-07b653d6-49f3-4dd5-89ad-2833fcbdc3ee.png)

Contacts
![image](https://user-images.githubusercontent.com/64807656/169146856-e4309cf3-1431-415f-bc82-c69f5afdd755.png)

Profile (Admin View)
![image](https://user-images.githubusercontent.com/64807656/169148231-13bde718-fef4-4d30-aeb6-11c879693898.png)

Create Announcement (Admin View)
![image](https://user-images.githubusercontent.com/64807656/169148415-8ad6fd6f-5676-45a4-9a46-ab5da61cc6b8.png)

Puzzles - filled with dummy data (Admin View)
![image](https://user-images.githubusercontent.com/64807656/169148636-8ecf75c2-3f42-4ff5-a77b-09fcabc122bf.png)

Videos (Admin View)
![image](https://user-images.githubusercontent.com/64807656/169148812-2bd991aa-3c7a-4f9e-8cef-79327563a371.png)

Search Puzzles (Guest View)
![image](https://user-images.githubusercontent.com/64807656/169149111-9d45d36f-1b8f-4b13-bca2-7fd2026081dc.png)

Registered users - filled with dummy data (Admin View)
![image](https://user-images.githubusercontent.com/64807656/169149683-ad389dc8-80f5-462b-947f-ac061f5745b6.png)

Groups - filled with dummy data (Admin View)
![image](https://user-images.githubusercontent.com/64807656/169149908-b62962df-fd16-465c-bd37-a0095069f79b.png)

Lesson - filled with dummy data (Admin View)
![image](https://user-images.githubusercontent.com/64807656/169150216-dbc6609b-fd1f-4b22-81bf-8261acfbf237.png)

Marking lesson attendances (Admin View)
![image](https://user-images.githubusercontent.com/64807656/169150822-940c640e-188d-4409-8269-9e01d36ba60f.png)

Governance (Admin View)
![image](https://user-images.githubusercontent.com/64807656/169151167-bcdf431d-bad4-4768-9b8e-48555c103822.png)

## Credits
* Using ASP.NET MVC Template (developed originally by Nikolay Kostov);
