# BlogApi
 This is just a simple test application, that implements Web API using ASP.Net Core MVC
 
 ## Requirements
 - ASP.Net Core 2.1
 - MongoDB 4
 
 ## Configuration
 - Setup Mongo connection in MongoConfig section of appsettings.json
 
 ## Usage
 ### Authorize
 API uses JWT Bearer Authorization. So you need to get token
 ```
 GET http://localhost:2450/api/system/token?user=[userId]
 ```
 
 ### Init
 Now you can init mongo collection and fill it with some data
 ```
 GET http://localhost:2450/api/system/init
 ```
 
 ### Create new post
 ```
 POST http://localhost:2450/api/posts
 {
   tags: ["tag1", "tag2"],
   body: "some body text"
 }
 ```
 
 ### Search for posts
 All search options are optional and combined with AND operator. This operation doesn't require authorization.
 ```
 POST http://localhost:2450/api/posts/find
 {
   user: "user",
   tag: "tag",
   text: "search"
 }
 ```
