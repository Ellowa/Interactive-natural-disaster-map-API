# Interactive-natural-disaster-map-API
<b><h3>Functional Requirements</h3></b>

<h4>Guest:</h4>

Function | Status 
|:----   |:------:
 View all confirmed events for the last year (with the ability to extend 1-5 years) | :heavy_check_mark:
 authorization/Registration | :small_orange_diamond:

<h4>User:</h4>

Function | Status 
|:----   |:------:
View all confirmed + their unconfirmed events for the last year(with the ability</br> to extend 1-5 years) | :x:
Adding a new event (with status unconfirmed), hazard unit is determined automatically</br> based on data from the database. Limit 50 per day. Client for adding web, telegram | :x:
Edit your event (with status unconfirmedd) | :x:
Deleting your event (with status unconfirmed) | :x:
Notifications (email, telegram) about events by selected filter (region, category,</br> danger level)| :x:
Creating collections of events | :x:
Adding events to collections | :x:
Ability to share a collection of events| :x:
Editing your profile | :x:
Complete deletion of your profile | :x:

<h4>Moderator(+ User functionality):</h4>

Function | Status 
|:----   |:------:
Verification of events (with status unconfirmed). Verification client web, telegram | :x:
Ability to prohibit user from adding events | :x:
View user collections | :x:
Edit any event | :x:
Delete any event| :x:
Add new event category| :x:
Edit event categoryt| :x:
Delete event category (Events get status = other)| :x:
Add new magnitude unit| :x:
Edit magnitude unit| :x:
Delete magnitude unit (Events get magnitude unit = undefined)| :x:
Add new hazard unit| :x:
Edit  hazard unit| :x:
Delet hazard unit (Events receive hazard unit = undefined)| :x:
