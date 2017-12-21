# ModernWebExperience

An ASP.NET Core 2.0 MVC SPA application utilizing Vue.js 2 and a Web API endpoint.

## Build Dependencies

* .NET Core 2.0 SDK (download [link](https://www.microsoft.com/net/download/windows))
* Yarn package manager (<https://yarnpkg.com>)
* Node.js
* Nuget Access

## Run Requirements

* Web Server
* SQL Server ([schema](#sql-schema) below)

## Local Build & Run Steps

* Open command line and navigate to `SL.WebExperience.Test/SL.WebExperience.Test.Web`
* `yarn install`
  * this loads all required node modules
* `dotnet build`
  * this will restore all nuget packages & will run webpack commands to generate app entry point
* Configure `AssetsDatabase` connection string to SQL Server
* `dotnet run`
  * serves up Web application and Web API endpoint
* Browse to <http://localhost:[port]/> to view application

## Web API Endpoint

* Assets
  * List of assets: GET `/api/Assets`
    * Optional parameters
      * `country` [int] - the database id of the country to filter
      * `mimeType` [int] - the mime type id of the mime type to filter
      * `pageNumber` [int] - what page of results to return (default: 1)
      * `pageSize` [int] - what size page results should be returned (default: 10, max: 100)
    * Maximum Results Allowed: 2500

  * Single Asset: GET `/api/Assets/[id]`

  * Create new asset: POST `/api/Assets`
    * payload
        ```json
        {
            "fileName": "Quam1b.lzh",
            "createdBy": "kfullerzby",
            "email": "rbutlerzby@webs.com",
            "description": "Morbi porttitor lorem id ligula. Suspendisse ornare consequat lectus. In est risus, auctor sed, tristique in, tempus sit amet, sem.",
            "mimeTypeId": 44,
            "countryId": 125,
            "version": "1",
            "assetKey": "1206c428-6cee-45f6-9b8e-a9fe5811d410"
        }```
  * Update existing asset: PUT `/api/Assets/[id]`
    * payload
        ```json
        {
            "assetId": [id],
            "fileName": "Quam1b.lzh",
            "createdBy": "kfullerzby",
            "email": "rbutlerzby@webs.com",
            "description": "Morbi porttitor lorem id ligula. Suspendisse ornare consequat lectus. In est risus, auctor sed, tristique in, tempus sit amet, sem.",
            "mimeTypeId": 44,
            "countryId": 125,
            "version": "1",
            "assetKey": "1206c428-6cee-45f6-9b8e-a9fe5811d410"
        }```
    * `id` in url must match `id` in payload

  * Delete existing asset: DELETE `/api/Assets/[id]`
    * **Note**: this does not *actually* delete the record in this demo, it marks the record "deleted" and then it is excluded from the result set.

* Countries
  * List of Countries: GET `/api/Countries`
    * Optional parameters
      * `startsWith` [string] - the starting value of the name on which to search
      * `name` [string] - the value of the name on which an exact match is performed

  * Single Country:  GET `/api/Countries/[id]`

* MimeTypes
  * List of Mime Types: GET `/api/MimeTypes`
    * Optional parameters
      * `startsWith` [string] - the starting value of the name on which to search
      * `name` [string] - the value of the name on which an exact match is performed

  * Single Mime Type:  GET `/api/MimeTypes/[id]`

## Next Steps

* API
  * Secure endpoint with authentication & authorization (oath2, jwt, etc.)
  * Add versioning
  * Switch to using `key` values insted of `id` values
  * Add caching layer(s)
  * Evaluate paging token implementation

* Web App
  * Structure
    * Add CDN layer for assets
    * Optimize payload size for mobile
    * Add caching layer
    * Add user login capability for access to features
    * Evaluate use of queues for updates to records
  * UI
    * Add country & mime type filtering
    * ~~Add page size input~~ (Added basic functionality)
    * Add user account managment
  * Build
    * Move build steps out of .csproj for integration into devops pipeline (gulp or other)
    * Create devops pipeline for CI & CD into cloud assets (VSTS, Jenkins, TeamCity)

## SQL Schema

<http://svgshare.com/i/49a.svg>
![Database Diagram](http://svgshare.com/i/49a.svg)
