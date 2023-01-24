# Car Pooling Service Challenge

Design/implement a system to manage car pooling.

At Cabify we provide the service of taking people from point A to point B.
So far we have done it without sharing cars with multiple groups of people.
This is an opportunity to optimize the use of resources by introducing car
pooling.

You have been assigned to build the car availability service that will be used
to track the available seats in cars.

Cars have a different amount of seats available, they can accommodate groups of
up to 4, 5 or 6 people.

People requests cars in groups of 1 to 6. People in the same group want to ride
on the same car. You can take any group at any car that has enough empty seats
for them. If it's not possible to accommodate them, they're willing to wait until 
there's a car available for them. Once a car is available for a group
that is waiting, they should ride. 

Once they get a car assigned, they will journey until the drop off, you cannot
ask them to take another car (i.e. you cannot swap them to another car to
make space for another group).

In terms of fairness of trip order: groups should be served as fast as possible,
but the arrival order should be kept when possible.
If group B arrives later than group A, it can only be served before group A
if no car can serve group A.

For example: a group of 6 is waiting for a car and there are 4 empty seats at
a car for 6; if a group of 2 requests a car you may take them in the car.
This may mean that the group of 6 waits a long time,
possibly until they become frustrated and leave.

## Evaluation rules

This challenge has a partially automated scoring system. This means that before
it is seen by the evaluators, it needs to pass a series of automated checks
and scoring.

### Checks

All checks need to pass in order for the challenge to be reviewed.

- The `acceptance` test step in the `.gitlab-ci.yml` must pass in master before you
submit your solution. We will not accept any solutions that do not pass or omit
this step. This is a public check that can be used to assert that other tests 
will run successfully on your solution. **This step needs to run without 
modification**
- _"further tests"_ will be used to prove that the solution works correctly. 
These are not visible to you as a candidate and will be run once you submit 
the solution

### Scoring

There is a number of scoring systems being run on your solution after it is 
submitted. It is ok if these do not pass, but they add information for the
reviewers.

## API

To simplify the challenge and remove language restrictions, this service must
provide a REST API which will be used to interact with it.

This API must comply with the following contract:

### GET /status

Indicate the service has started up correctly and is ready to accept requests.

Responses:

* **200 OK** When the service is ready to receive requests.

### PUT /cars

Load the list of available cars in the service and remove all previous data
(reset the application state). This method may be called more than once during
the life cycle of the service.

**Body** _required_ The list of cars to load.

**Content Type** `application/json`

Sample:

```json
[
  {
    "id": 1,
    "seats": 4
  },
  {
    "id": 2,
    "seats": 6
  }
]
```

Responses:

* **200 OK** When the list is registered correctly.
* **400 Bad Request** When there is a failure in the request format, expected
  headers, or the payload can't be unmarshalled.

### POST /journey

A group of people requests to perform a journey.

**Body** _required_ The group of people that wants to perform the journey

**Content Type** `application/json`

Sample:

```json
{
  "id": 1,
  "people": 4
}
```

Responses:

* **200 OK** or **202 Accepted** When the group is registered correctly
* **400 Bad Request** When there is a failure in the request format or the
  payload can't be unmarshalled.

### POST /dropoff

A group of people requests to be dropped off. Whether they traveled or not.

**Body** _required_ A form with the group ID, such that `ID=X`

**Content Type** `application/x-www-form-urlencoded`

Responses:

* **200 OK** or **204 No Content** When the group is unregistered correctly.
* **404 Not Found** When the group is not to be found.
* **400 Bad Request** When there is a failure in the request format or the
  payload can't be unmarshalled.

### POST /locate

Given a group ID such that `ID=X`, return the car the group is traveling
with, or no car if they are still waiting to be served.

**Body** _required_ A url encoded form with the group ID such that `ID=X`

**Content Type** `application/x-www-form-urlencoded`

**Accept** `application/json`

Responses:

* **200 OK** With the car as the payload when the group is assigned to a car. See below for the expected car representation 
```json
  {
    "id": 1,
    "seats": 4
  }
```

* **204 No Content** When the group is waiting to be assigned to a car.
* **404 Not Found** When the group is not to be found.
* **400 Bad Request** When there is a failure in the request format or the
  payload can't be unmarshalled.

## Tooling

At Cabify, we use Gitlab and Gitlab CI for our backend development work. 
In this repo you may find a [.gitlab-ci.yml](./.gitlab-ci.yml) file which
contains some tooling that would simplify the setup and testing of the
deliverable. This testing can be enabled by simply uncommenting the final
acceptance stage. Note that the image build should be reproducible within
the CI environment.

Additionally, you will find a basic Dockerfile which you could use a
baseline, be sure to modify it as much as needed, but keep the exposed port
as is to simplify the testing.

:warning: Avoid dependencies and tools that would require changes to the 
`acceptance` step of [.gitlab-ci.yml](./.gitlab-ci.yml), such as 
`docker-compose`

:warning: The challenge needs to be self-contained so we can evaluate it. 
If the language you are using has limitations that block you from solving this 
challenge without using a database, please document your reasoning in the 
readme and use an embedded one such as sqlite.

You are free to use whatever programming language you deem is best to solve the
problem but please bear in mind we want to see your best!

You can ignore the Gitlab warning "Cabify Challenge has exceeded its pipeline 
minutes quota," it will not affect your test or the ability to run pipelines on
Gitlab.

## Requirements

- The service should be as efficient as possible.
  It should be able to work reasonably well with at least $`10^4`$ / $`10^5`$ cars / waiting groups.
  Explain how you did achieve this requirement.
- You are free to modify the repository as much as necessary to include or remove
  dependencies, subject to tooling limitations above.
- Document your decisions using MRs or in this very README adding sections to it,
  the same way you would be generating documentation for any other deliverable.
  We want to see how you operate in a quasi real work environment.

## Feedback

In Cabify, we really appreciate your interest and your time. We are highly 
interested on improving our Challenge and the way we evaluate our candidates. 
Hence, we would like to beg five more minutes of your time to fill the 
following survey:

- https://forms.gle/EzPeURspTCLG1q9T7

Your participation is really important. Thanks for your contribution!

## Getting Started

In other to get started with the project in this repository, the following
should be considered:

- Install Visual Studio 2022 and .NET 6.0 SDK:
https://visualstudio.microsoft.com/vs/

- Install MongoDb which is a NoSQL document database. Connect your installed mongodb compass to a cloud instance or
local instance as you desire. For this project, a cloud instance is used in other to enable the project to be self-contained.
Finally on mongodb, ensure to set your security rule for database access to allow ip access from any location.
Optionally, you can also add or whitelist your current ip address in addition to the compulsory ip access from any location since the database
will also be accessed from a docker container. 
Here's a url to download and install mongodb: 
https://www.mongodb.com/docs/v4.2/installation/

- git clone the repository to get a copy on your local machine which is synced to the remote gitlab environment and you're all set.

## Performance Considerations

Because of the high traffic that can occur unavoidably in a car pooling system, the following were considered while building this project:
- The use of a NoSql database: MongoDb was chosen to accommodate for low latency calls on the database, faster reads and ease of scaling.
This choice therefore can handle million to billion data traffic on the system without much performance tuning efforts as opposed to using
an Sql database.

- The proper writing of algorithms to achieve linear BigO notations as opposed to Quadratic BigO notations especially in functions or methods
that are hot paths. An example of such hot path endpoint is the cars endpoint which fetches the list of available cars and can be called more
than once in a day. 
The BigO notation for the function called by the exposed endpoint has its worst case scenario to be a time and space complexity of O(3n).
The BigO notation if the function is not properly written can be O(n+n^2).
Quadratic BigO notations means a situation where there's a nested loop within the function. That can slow down operations on the function as traffic
increases on that path. In summary, algorithms impacts are more visible and benchmarked at scale.

- The creation of mappers manually rather than using automappers: Automapper is great for mapping data transfer objects(dtos) to models and vice versa.
However, it becomes a pain when mapping complex objects as the reflection upon which automapper relies on for mapping isn't robust enough to carry out such
operations. Reflection imposes some performance implication especially when dealing with systems at scale. Some school of thought believes the performance
impact is minimal but then you can agree that any performance gain that can be gotten is important and significant in the overall functioning of the system.
Having this in mind, the decision was made to create a mapper class for handling the mapping processes which in turn gains some performance improvement.
   
## Thoughts/ToDo

This project is quite interesting and I hope you have fun checking out the repository as much as I had fun while building the project.
Though the project or task is open ended in a way, I am penning down my thoughts which I had to keep off the codebase at the moment
since the task has defined rules to follow for simplicity sake and consistency to achieve ease of tasks evaluation.

The following are some of my thoughts on the features for a car pooling service I would love to see(perhaps, I build them and more in the future):
- Their can be further tables or collections created on the CarPoolingDb. Current tables includes: AvailableCars, CarDetails and JourneyDetails. 
The database can have further tables such as DriverJourneys, Drivers.etc
- The idea is as follows:
* A customer signs up on the app at first use of the app and is automatically logged-in. 
the login won't be time bound except the customer decides to log out at will. Two factor authentication
mechanism can be introduced if the app begins to accommodate features holding sensitive data such as a wallet system to perhaps encourage savings
and thereby have an option to pay for trips through the wallet system.etc. It can be scheduled, one off or recurring. The wallet can be linked to a customer's
account and rules set as to how and when money should move to the wallet for trips. This encourages upfront addition of trips as planning of expenses is carried out on
a monthly, quarterly or yearly basis.
* A customer can use the app as a non-first time user without having to login.
* A customer can create a journey as a group. A group can be from 1 to 6. In creating a journey, the pick up and destination locations should be specified, 
the number of passengers amongst other properties to be sent from the frontend client to the backend. Every created journey is tagged a journey status called created.
Their should be four statuses to be considered: created, ongoing, cancelled and completed. Every group has a uniquely generated id by the backend and stored in the database.
The journey details table should also have a userId property and/or other unique fields such as phone number, email.etc in other to link journeys to respective users or
customers.
* Car details are created on the CarDetails table or collection. That can be handled at the admin dashboard of the system.
* To end journeys, a journey can be cancelled by a customer maybe due to a delay in assigning a car to a group or whatever reasons. 
A journey can also be completed when the customer arrives at the expected destination.
* An endpoint returns available cars on a daily basis by resetting the database(removing records that are more than a day on the database). 
Availability of cars can be handled by checking the status of a journey.
A background job can be used to check for cancelled or completed journey statuses and then saves a record(journeyDetailsId.etc) on AvailableCars table.
Also, the available cars data to be returned for specific users is considered based on the radius between the customer's current location and car location.
An algorithm can be written to handle the decision of available cars to be returned for a user. 
* Assigning a car to a group for a journey happens at the point a journey is created. When the journey is created, 
a notification is sent to the driver's app to be accepted or declined. Once accepted, the userId, journeyDetailsId and driverId
are sent to the system for checks on the Drivers table and further creates a record on DriverJourneys table. 
Furthermore, the sent journeyDetailsId is used for checks on AvailableCars table and an update operation for found records tied to journeyDetailsId is carried out. 
Part of what is updated on the AvailableCars table is the carLocation of the driver. A webhook or an event messaging system can be used to achieve this feature.
It is important to note that the number of seats of a car has to be greater or equal to the number of passengers in other to assign the appriopriate car based on
customer's request. Also, DriverJourneyStatus for a journey is set to Accepted once accepted.
When a driver declines a request,all the above processes occurs apart from operations to be carried out on AvailableCars table. The DriverJourneyStatus is also set to Declined. 
* The status of a journey changes to ongoing the moment a driver starts the journey once the journey has commenced.
* The status of a journey changes to completed the moment a driver ends the journey once the customer's destination has been reached.
* The status of a journey changes to cancelled when the customer decides not to continue the journey.
* A customer can only have one created journey per time. A journey has to be cancelled or completed in other to create another journey and so on.

## Contributions

In other to contribute effectively to the project, it is important to consider the following:

- Consider creating a new branch off master branch for any work to be carried out on the codebase.
This will encourage code reviews and ensure the best code makes it to production.

- Consider naming feature branches in this manner: feature/<name of task in hiphen>

- Consider naming bug fix branches in this manner: bugfix/<name of task in hiphen>

- Consider naming hot fix branches in this manner: hotfix/<name of task in hiphen>

- Create your test conditions for features or fixes on the pipeline, make an edit on the gitlab ci yml file to accommodate running of the tests once your code is pushed 
to the pipeline. You don't need to do anything on the docker file except a new project is added to the car pooling service solution. See docker file in the project as a guide.
This process should be upheld to encourage TDD practices. Also, code becomes reviewed after all acceptance criteria has been fulfilled by the code on the docker image
being ran on the gitlab ci/cd pipeline.  