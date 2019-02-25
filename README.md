# Vidly - A Video Rental Application Using ASP.NET MVC and RestAPI

**DESCRIPTION:** 
  - An ASP.NET application for video rental stores which comprises of three users: admin, staff member and customers
  - The admin can add, update and delete information like name membershiptype and date of birth of the user and also the genre, number of Stock and release date of the movie.
  - The staff member is limited to manipulate the customer information only
  - The Admin or staff member can rent a movie to the customer by just assigning that movie to the customer account 
  
**FEATURES:**
  - In order to use this application's services, the customer has to sign up with this application.
  - The customer can also register into the system by using facebook.
  - Enabled **clientside-validations** to check whether the entered emailid,password and name are valid or not.
  - Designed **Custom validations** for the number In stock of a movie between 5 to 20 and the age to rent a movie to above 18 .
  - Enabled **Antiforgery-tokens** to restrict the **CROSS SITE REQUEST FORGERY**
  - All the user and movie information is stored in the Database.

**TECHNICAL FEATURES:**
   - The application interface renders depending upon the device screen(mobile or web)
   - Used **Bootbox.js** for making the application device screen independent 
   - Designed by using **MVC(Model-View-Component)** methodology
   - Designed **ASP.NET RestAPI** for manipulating data to the database and the exchanged data is in **JSON** format
   - Followed **Entity-Framework Code-first migartions** in designing the database
   - Used **JQuery and ajax** for calling the API and for displaying Information from the API
   - Used **DataTables Plugin** for pagenation, searching and sorting of the information
