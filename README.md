# Interactive-natural-disaster-map-API
Swagger: https://interactivenaturaldisastermapapi.azurewebsites.net/swagger/index.html
<br>
Frontend project: https://github.com/Ellowa/Interactive-natural-disaster-map
<br>
Site: https://ellowa.github.io/Interactive-natural-disaster-map

<b><h3>Functional Requirements</h3></b>

<h4>Guest:</h4>

Function | Status 
|:----   |:------:
View all confirmed events for the last year (with the ability to extend 1-5 years) | :heavy_check_mark:
Authentication/Registration  | :heavy_check_mark:

<h4>User:</h4>

Function | Status 
|:----   |:------:
View all confirmed + their unconfirmed events for the last year(with the ability</br> to extend 1-5 years) | :heavy_check_mark:
Adding a new event (with status unconfirmed), hazard unit is determined automatically</br> based on data from the database. Limit 50 per day. Client for adding web, telegram | :small_orange_diamond:
Edit your event (with status unconfirmedd) | :heavy_check_mark:
Deleting your event (with status unconfirmed) | :heavy_check_mark:
Notifications (email, telegram) about events by selected filter (region, category,</br> danger level)| :x:
Creating collections of events | :heavy_check_mark:
Editing collections of events | :heavy_check_mark:
Adding events to collection | :heavy_check_mark:
Deliting events from collection | :heavy_check_mark:
Ability to share a collection of events| :x:
Editing your profile | :heavy_check_mark:
Complete deletion of your profile | :heavy_check_mark:

<h4>Moderator(+ User functionality):</h4>

Function | Status 
|:----   |:------:
Verification of events (with status unconfirmed). Verification client web, telegram | :small_orange_diamond:
Ability to prohibit user from adding events | :x:
View user collections | :heavy_check_mark:
Edit any event | :heavy_check_mark:
Delete any event| :heavy_check_mark:
Add new event category| :heavy_check_mark:
Edit event categoryt| :heavy_check_mark:
Delete event category (Events get status = other)| :heavy_check_mark:
Add new magnitude unit| :heavy_check_mark:
Edit magnitude unit| :heavy_check_mark:
Delete magnitude unit (Events get magnitude unit = undefined)| :heavy_check_mark:
Add new hazard unit| :heavy_check_mark:
Edit  hazard unit| :heavy_check_mark:
Delet hazard unit (Events receive hazard unit = undefined)| :heavy_check_mark:
Add new event source| :heavy_check_mark:
Edit  event source| :heavy_check_mark:
Delet event source (Events receive hazard unit = unknown)| :heavy_check_mark:

<b><h3>Non-functional Requirements</h3></b>

Feature | Status 
|:----   |:------:
CI/CD | :heavy_check_mark:
JWT | :heavy_check_mark:
Clean Architecture | :heavy_check_mark:
Vertical slice | :heavy_check_mark:
CQRS + Mediator | :heavy_check_mark:
Role-based authorization  | :heavy_check_mark:
