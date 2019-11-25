h2. Introduction

The Suitsupply.Tailoring solution solves [the coding test](task.md).

h2. Structure description

Solution structure:

* src - the solution source code
* tests - projects with unit-tests for the solution

h3. Source code structure

* core - the core folder contains projects which incapsulate base interfaces, data objects (domain description), business logic services
* infrastructure - contains projects which depend on a specific infrastructure, for example, database access level
* presentation - contains projects on presentation level (Web API, UI and etc.)

h3. An **OrderPaid** message processing

The application Suitsupply.Tailoring.Web.Api contains hosted service. This hosted service listening Azure Topic, topic is configured in appsettings.json in TopicSubscriptionConfig section.
The example of message which is expected by application:

```json
{
    AlterationId: "123"
}
``` 
