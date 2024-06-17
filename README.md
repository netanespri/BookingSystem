# Booking System
* This application provides users with available slots that can be scheduled to arrange private appointments.
* No data is persisted. A HTTP service is used to retrieve slots and book appointments: https:/ draliatest.azurewebsites.net/api/availability
* The code repository can be found at: https://github.com/netanespri/BookingSystem
* Main components are:
** Swagger as UI interface to check the Booking API.
** C# Web API that provides two endpoints:
*** /api/schedule/availability/week/<date>
**** GET endpoint to retrieve the available time slots in a week.
**** Date should follow the format: YYYY-MM-DD. For instance, 2024-06-10
*** /api/schedule/appointment
**** POST endpoint to book a slot for a specific patient, date, time and facility.

## How to run Booking System
* Build the solution by running the "dotnet build" command at the root directory.
* Run the solution by executing the "dotnet run" command in the BookingSystem.Api project directory.
* The API can be tested via Swagger: https://localhost:7109/swagger/index.html
* To use the system, firstly get the slot availability using a date whose day is Monday. For example: https://localhost:7109/api/Schedule/availability/week/2024-06-10
* JSON successful response (not matching up output of above URL): 
```
{
  "succeeded": true,
  "message": "",
  "data": {
    "facilityId": "90c9f71c-685f-48e7-a6d5-7898775209ce",
    "dailyAvailableSlots": {
      "Monday": [
        {
          "start": "2024-06-10T10:20:00",
          "end": "2024-06-10T10:30:00"
        },
        {
          "start": "2024-06-10T10:30:00",
          "end": "2024-06-10T10:40:00"
        }
	}
   }
}   
```
* Secondly, to book an appointment on the week that has been queried, just use the "start" and "end" dates along with the "facilityId" field.
* Then add any comment and the patient information as it is shown in the following request and JSON body: https://localhost:7109/api/Schedule/appointment
* JSON body:
```
{
  "facilityId": "90c9f71c-685f-48e7-a6d5-7898775209ce",
  "start": "2024-06-17T08:18:45.434",
  "end": "2024-06-17T08:18:55.434",
  "comments": "Any comment",
  "patient": {
    "name": "Michael",
    "secondName": "Smith",
    "email": "email@gmail.com",
    "phone": "644586272"
  }
} 
```
* Expected successful output: "Appointment has been scheduled successfully"

## Technical details

### Main patterns
* Clean Architecture.
* Inversion of control - Dependency injection.
* Command Query Responsibility Segregation (CQRS).
* Mediator pattern to remove direct communication between objects and improve code readability and maintainability.
* REST API endpoints.
* Fluent validation on MediatR pipeline.
* Presentation middleware to handle errors.
* Typed HTTP client.

### Tests
* Main unit tests provided to cover both successful and failure paths.
* Integration tests performed manually via Swagger and PostMan.

## Suggested improvements
* Improve validation. For example, to check input fields like email or phone has a correct format.
* Consider security: Authentication and authorisation.
* Add API versioning.
* Add configuration per each environment (development, QA and production).
* Improve approach to arrange slots per day as currently the calculation is linear adding the slot duration from the start period to end.
* Use AutoMapper for a cleaner approach to map objects.
* Extend logging across the application to improve observability/monitoring/debugging.
* Set up the logging to source a log monitor management platform like DataDog.
* Increase code coverage with automated tests at different levels: integration, contract and functional/behaviour.
* Use BDD tests to check behaviour.
* Set up Continuous Integration and Continouous delivery pipelines to deploy to each environment automatically including production.