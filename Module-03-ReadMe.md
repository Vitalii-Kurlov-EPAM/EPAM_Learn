**Explain the difference between terms: REST and RESTful.**  
REST (**RE**presentational **S**tate **T**ransfer) is basically an architectural style of development having some principles:  
 - It should be stateless
 - It should access all the resources from the server using only URI
 - It does not have inbuilt encryption
 - It does not have session
 - For performing CRUD operations, it should use HTTP verbs such as get, post, put and delete
 - It should return the result only in the form of JSON or XML, atom, OData etc. (lightweight data)  

**REST based** services follow some of the above principles and not all  
**RESTful** services means it follows all the above principles.


**What are the six constraints?**  
1. **Uniform Interface**  
   *It is a key constraint that differentiate between a REST API and Non-REST API. It suggests that there should be an uniform way of interacting with a given server irrespective of device or type of application (website, mobile app).*  
**There are four guidelines principle of Uniform Interface are:**

   - **Resource-Based:**  
     *Individual resources are identified in requests. For example: API/users.*  

   - **Manipulation of Resources Through Representations:**  
	 *Client has representation of resource and it contains enough information to modify or delete the resource on the server, provided it has permission to do so. Example: Usually user get a user id when user request for a list of users and then use that id to delete or modify that particular user.*  

   - **Self-descriptive Messages:**  
	 *Each message includes enough information to describe how to process the message so that server can easily analyses the request.*  

   - **Hypermedia as the Engine of Application State (HATEOAS):**  
	 *It need to include links for each response so that client can discover other resources easily.*

  
2. **Stateless**  
*It means that the necessary state to handle the request is contained within the request itself and server would not store anything related to the session. In REST, the client must include all information for the server to fulfill the request whether as a part of query params, headers or URI. Statelessness enables greater availability since the server does not have to maintain, update or communicate that session state. There is a drawback when the client need to send too much data to the server so it reduces the scope of network optimization and requires more bandwidth.*

3. **Cacheable**  
*Every response should include whether the response is cacheable or not and for how much duration responses can be cached at the client side. Client will return the data from its cache for any subsequent request and there would be no need to send the request again to the server. A well-managed caching partially or completely eliminates some client–server interactions, further improving availability and performance. But sometime there are chances that user may receive stale data.*  

4. **Client-Server**  
*REST application should have a client-server architecture. A Client is someone who is requesting resources and are not concerned with data storage, which remains internal to each server, and server is someone who holds the resources and are not concerned with the user interface or user state. They can evolve independently. Client doesn’t need to know anything about business logic and server doesn’t need to know anything about frontend UI.*  

5. **Layered System**  
*An application architecture needs to be composed of multiple layers. Each layer doesn’t know any thing about any layer other than that of immediate layer and there can be lot of intermediate servers between client and the end server. Intermediary servers may improve system availability by enabling load-balancing and by providing shared caches.*  

6. **Code on Demand**  
*It is an optional feature. According to this, servers can also provide executable code to the client. The examples of code on demand may include the compiled components such as Java Servlets and Server-Side Scripts such as JavaScript.*


**HTTP Request Methods (the difference) and HTTP Response codes.**  

- **POST** 
  *The **POST** method submits an entity to the specified resource, often causing a change in state or side effects on the server.*  

- **GET**  
  *The **GET** method requests a representation of the specified resource. Requests using GET should only retrieve data.*  

- **PUT**  
  *The **PUT** method replaces all current representations of the target resource with the request payload.*  

- **DELETE**  
  *The **DELETE** method deletes the specified resource.*

- **HEAD**  
  *The **HEAD** method asks for a response identical to a GET request, but without the response body.*  

- **OPTIONS**  
  *The **OPTIONS** method describes the communication options for the target resource.*

- **PATCH**  
  *The **PATCH** method applies partial modifications to a resource.*  
---  
  - **GET**:  
    1. Can be cached.
	2. Remain in the browser history.
	3. Can be bookmarked.
	4. Should never be used when dealing with sensitive data.
	5. Have length restrictions.
	6. Is only used to request data (not modify).

  - **HEAD**:  
    *HEAD is almost identical to GET, but without the response body.*
---  
  - **POST**:
    1. Is never cached
	2. Do not remain in the browser history.
	3. Cannot be bookmarked.
	4. Have no restrictions on data length.
	
  - **PUT**:  
	*The difference between **POST** and **PUT** is that **PUT** requests are **idempotent**.* 
---

**Response codes**    


- Informational responses (100 – 199)  
- Successful responses (200 – 299)  
- Redirection messages (300 – 399)  
- Client error responses (400 – 499)  
- Server error responses (500 – 599)  


**What is idempotency?**   
*That is, calling the same request multiple times will always produce the same result.*

**Is HTTP the only protocol supported by the REST?**  
*The use of HTTP is not required for a RESTful system. It just so happens that HTTP is a good starting because it exhibits many RESTful qualities.*  


**What are the advantages of statelessness in RESTful services?**  

- Statelessness helps in scaling the APIs to millions of concurrent users by deploying it to multiple servers. Any server can handle any request because there is no session related dependency.  

- Being stateless makes REST APIs less complex – by removing all server-side state synchronization logic.  

- A stateless API is also easy to cache as well. Specific softwares can decide whether or not to cache the result of an HTTP request just by looking at that one request. There’s no nagging uncertainty that state from a previous request might affect the cacheability of this one. It improves the performance of applications.  

- The server never loses track of “where” each client is in the application because the client sends all necessary information with each request.

**How can caching be organized in RESTful services?**  
*Caching refers to storing the server response in the client itself, so that a client need not make a server request for the same resource again and again. A server response should have information about how caching is to be done, so that a client caches the response for a time-period or never caches the server response.*  

**Following are the headers which a server response can have in order to configure a client's caching −**
---
- **Date**  
*Date and Time of the resource when it was created.*

- **Last Modified**  
*Date and Time of the resource when it was last modified.*  

- **Cache-Control**  
*Primary header to control caching.*  

- **Expires**
*Expiration date and time of caching.*  

- **Age**  
*Duration in seconds from when resource was fetched from the server.*  
---  
  

   
**Cache-Control Header**

---

- **Public**  
*Indicates that resource is cacheable by any component.*

- **Private**  
*Indicates that resource is cacheable only by the client and the server, no intermediary can cache the resource.*  


- **no-cache/no-store**  
*Indicates that a resource is not cacheable.*

- **max-age**  
*Indicates the caching is valid up to max-age in seconds. After this, client has to make another request.*  


- **must-revalidate**  
*Indication to server to revalidate resource if max-age has passed.*
---

**How can versioning be organized in RESTful services?**  
1. **Versioning through URI Path**  
*http://www.example.com/api/ **v1** /products*

2. **Versioning through query parameters**  
*http://www.example.com/api/products? **version=1***  

3. **Versioning through custom headers**   
*curl -H “Accepts-version: 1.0” http://www.example.com/api/products*  

4. **Versioning through content negotiation**  
*curl -H “Accept: application/vnd.xm.device+json; **version=1**” http://www.example.com/api/products*


**What are the best practices of resource naming?**  

- Use nouns to represent resources
- Use the “plural” name to denote the collection resource archetype.
- Use forward slash (/) to indicate hierarchical relationships
- Use hyphens (-) to improve the readability of URIs
- Do not use underscores ( _ )
- Use lowercase letters in URIs
- Do not use file extensions
- We should not use URIs to indicate a CRUD function. URIs should only be used to identify the resources and not any action upon them uniquely.
- Use query component to filter URI collection
- Do not Use Verbs in the URI

**What are OpenAPI and Swagger?**  
- **OpenAPI** -> Specification  
- **Swagger** -> Tools for implementing the specification

**What implementations/libraries for .NET exist?**  
- *Swashbuckle.AspNetCore*

**When would you prefer to generate API docs automatically and when manually?**
- **Automatically** - Functionality for testing purposes when we need something like swagger.
- **Manually** - Information what parameters/endpoint mean (comments in other words)

**What is OData?**  
*OData is a data access protocol. It provides a uniform way to query and manipulate data sets.*

**When will you choose to follow it?**  
 Support for generic queries against the service data. 
 OData supports **IQueryable** so that you can decide on the client side 
 on how to filter the data that the service provides. 
 So you do not have to implement various actions or use query parameters to provide 
 filtered data. This also means that if you need a new filter for your client, 
 it is very likely that you do not have to change the server and can just put up the 
 query on the client side. 

**What is Richardson Maturity Model?**  
*The Richardson Maturity Model is a way to grade your API according to the constraints of REST. The better your API adheres to these constraints, the higher its score is. The Richardson Maturity Model knows 4 levels (0-3), where level 3 designates a truly RESTful API.*  


**Is it always a good idea to reach the 3rd level of maturity?**
No. This level is not so popular as the second one.


**What does pros and cons REST have in comparison with other web API types?**  

---
**SOAP**  
- **PROS**  
	1. Standardized protocol and formal contract
	2. Built-in error handling and security features
	3. Support for ACID transactions
	4. Wide support for different languages/platforms
- **CONS**  
	1. Higher overhead due to its XML-based message format 
	2. Slower performance compared to REST API
	3. More complex and harder to implement
	4. Limited support for mobile devices and web browsers

---

**REST**  
- **PROS**  
	1. Lightweight and faster performance
	2. Easier to implement and maintain
	3. Better support for mobile devices/web browsers
	4. Scalable and flexible architecture
- **CONS**  
	1. Lack of standardized protocol and formal contract
	2. No built-in error handling and security features
	3. Limited support for ACID transactions
	4. Limited support for different languages/platforms
---