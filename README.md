# Rocket Elevator REST API

Week 8 - Offering and exposing a service on the Internet

## Name of databases

`samuellaforme` is the name for both PostgreSQL and MySQL databases hosted on the Codeboxx aws account.

## How to connect to the databases with our service

To connect to a database, we use an environment variable (for informations about setting up an environment variable, view pages: [Linux](https://www.serverlab.ca/tutorials/linux/administration-linux/how-to-set-environment-variables-in-linux/), [MacOS](https://phoenixnap.com/kb/set-environment-variable-mac), [Windows](https://docs.oracle.com/en/database/oracle/machine-learning/oml4r/1.5.1/oread/creating-and-modifying-environment-variables-on-windows.html)). This variable is a connection string with the name `CONNECTION_STRING` and should looks something like this: 
``` 
 server=<HOST>;database=<DATABASE>;user=<USERNAME>;password=<PASSWORD>
```

## REST server endpoints

To test the endpoints of the REST api,  you can take the Postman collection right here: [https://www.getpostman.com/collections/a1a215065e260c873829](https://www.getpostman.com/collections/a1a215065e260c873829)

The server is hosted on an heroku platform which can be found at [https://rocketelevatorrestapih22.herokuapp.com/](https://rocketelevatorrestapih22.herokuapp.com/). What follows are the endpoints based of that url:

- GET `/api/batteries/<x>` where x is the id of the desired battery
- GET `/api/elevators/<x>` where x is the id of the desired elevator
- GET `/api/columns/<x>` where x is the id of the desired column
- GET `/api/elevators/inactive` show the list of elevators that are inactive
- GET `/api/buildings/intervention` show the list of buildings that contains a battery/column/elevator that is in intervention state
- GET `/api/leads/nonuser` show the list of leads that do not have an associated user and have been created in the last 30 days

The other requests are PUT requests and need a header of type `Content-Type` which contains `application/json`. In addition, it also need some information in the body:
```
{
    "id": "<ID_OF_THE_ELEMENT>",
    "status": "<NEW_STATUS_OF_THE_ELEMENT>"
}
```

Here to note, the id in the body AND the requested url (`<x>`) have to be the same, otherwise it won't work and return a status 400.

- PUT `/api/batteries/<x>` to modify the status value of the battery x
- PUT `/api/elevators/<x>` to modify the status value of the elevator x
- PUT `/api/columns/<x>` to modify the status value of the column x

## Explanatory video



https://www.postman.com/collections/5d3f43a20db118ff8e78