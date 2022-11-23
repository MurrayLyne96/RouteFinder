# RouteFinder
This is the repository for the RouteFinder CoE project for Unosquare

# What is the application being made?
RouteFinder is a route generation tool designed to allow users to generate GPS routes for sport related activities, such as running and cycling. Routes can be generated by specifying parameters, such as a start location, distance, maximum/minimum elevation in route, type of terrain and whether the route should be an end to end sprint or a circuit. Users can generate routes and save them to their profile, which in turn can be shared via a URL link or be marked as public routes. Users can also search for routes or do an area search with optional filter parameters. To aggregate good routes, users can like and favourite routes so that quality routes are given more visibility. On completion of a route users should be able to rate the route or submit a review describing the experience of the route. At a bare minimum, users should be able specify the difficulty of the route but this can be expanded for other metrics such as views and fun factor.

# Who is this application for?
This application is intended for running and/or cycling atheletes who may use GPS tracking technology while exercising. However, the application can also be used for other sports, such as hiking. The point of this application is to give a potential user the means to generate routes in an area they may not be familiar with or generate potential ideas for a new route as part of a new training routine. Likewise it can be used to aggregate popular routes used in a certain area. A similar feature for generating routes exists in the fitness social media app Strava (https://www.strava.com/) however, this app focuses solely on the generation and sharing of routes, a routes database if you will, rather than being an athelete-orientated social media app. 

# How will this application be used?
Access and features available in the app will depend if the user has an account or not. If the user does not have an account, they can search for routes by typing in an address or city/town name and specify a maximum range around the aforementioned location. If the user is logged in, they can login to the app an access a user dashboard. This dashboard will show the user their saved routes and the option to generate new routes. When generating new routes, the user will pick a starting location, and whether the route will be a circuit (where it will end at the start point) or a sprint (where the route will end at a different location). If sprint is selected, the user can optionally specify where the route will end. After this, the user can specify parameters such as how hilly the route will be, the desired length and what sort of terrain to use. An example of this would be choosing to avoid main roads or focus on offroad tracks only.

## Creating a new route
- User clicks "Generate route"
- User is asked to select a start location. 
  - This can be done by typing in an address or dropping a pin on a map.
- User is shown radio button option to generate either a circuit route or a sprint route.
- User can also optionally specify if the route is meant for running or cycling.
- User is shown a map and a list of parameters to adjust route generation.
- User clicks "Generate route".
- The user can generate a route and make adjustments as they see fit until they are happy with the given route. 
- User clicks "Save route".
- The route is saved on the backend and stored as one of the user's route.

## Searching for a route
- The user goes to the root homepage.
- The user types into a search bar a location, which can also be an address.
- The user can optionally apply filters, such as a minimum length, maximum total elevation, running route or cycling route etc.
- The user will get a selection of routes based on their search criteria. 
  - The user can view these routes by clicking on them, which will bring them to the view route page.

# MVP
- Create user account and password
- Create route with the following parameters
  - Length
  - Circuit or route
  - Maximum elevation
- Store saved routes to a user's profile.
- View routes on a view route page.
## V1
- Allow users to specify either a cycling route or a running route
- Allow users to search for routes with filters
- Allow users to search or the following parameters
  - Terrain prioritization
  - Queue up different towns as "checkpoints"
  - - Allow users to rate routes on a likes system.
## V2
- Allow users to label routes with tags, ie: labelling a route as "hilly". Users can use this to filter routes.
- Upload photo for route as well as user.
- Allow users to report on routes upon completion using a review system.
- Allow users to upload custom routes using GPX files.
- Export routes from 3rd party services, such as strava.

# Dictionary
**User:** The user represents a person who has a registered account in the system. The user will have a role assigned which will grant them different access levels on the site. 

**Role:** The Role is a User's role, which grants users different permissions. To begin with, there will be only two tracked roles in the system.

**Route:** The Route represents information about a generated route in the system, which will be done procedurally. 

**Review:** The Review represent a user-submitted review of a particular route. The route can optionally be associated with a rating, but it is not required to submit a review.

**PlotPoint:** A plot-point is a coordinate that is associated with a route that when combined together will create a route. Plot-points will contstruct the route based on their order. A route will need many plotpoints to correctly display a route.

**Rating:** A rating is an review aggregator that allows users to rate the quality of a route. A route may have many ratings.

**Tag:** A tag is a label that can be applied to many different routes. Tags are used to label routes and can be used to search for specific types of routes in the system. 
# Domain Model
```mermaid
erDiagram
    USER ||--o{ ROLE : has
    USER ||--o{ ROUTE : creates
    USER ||..o{ REVIEW : posts
    USER ||..o{ RATING : posts
    ROUTE ||..o{ RATING : holds
    ROUTE ||..o{ REVIEW : has
    ROUTE ||--o{ PLOTPOINT: has
    USER }o..o{ TAG: creates
    ROUTE }o..o{ TAG: has
```

# Entity Relationship Diagram
```mermaid
erDiagram
    user ||--o{ route : creates
    user ||..o{ review : posts
    user ||..o{ rating : posts
    route ||..o{ rating : holds
    route ||..o{ review : has
    route ||--o{ plotpoint: has
    route ||--o{ type: has
    route }o..o{ route_tag: has
    tag }o..o{ route_tag: has

    user {
        guid id PK
        string email_address
        string first_name
        string last_name
        string password
        string role
        date date_of_birth
        timestamp created
        timestamp last_modified
    }

    type {
        serial id PK
        string name
        timestamp created
        timestamp last_modified
    }

    route {
        guid id PK
        guid user_id FK
        string route_name
        serial type_id
        timestamp created
        timestamp last_modified
    }

    review {
        guid id PK
        guid route_id FK
        guid user_id FK
        string content
        timestamp created
        timestamp last_modified
    }

    plotpoint {
        guid id PK
        guid route_id FK
        float x_coordinate
        float y_coordinate
        string description
        integer order
        timestamp created
        timestamp last_modified
    }

    rating {
        guid id PK
        guid user_id FK
        guid route_id FK
        double rating
        date created
        date last_modified
    }

    tag { 
        guid id PK
        guid name
        timestamp created
        timestamp last_modified
    }

    route_tag {
        serial id PK
        guid route_id FK
        guid tag_id FK
        timestamp created
        timestamp last_modified
    }


```
