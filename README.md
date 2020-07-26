# FreeSecur

FreeSecur is an open source project with the goal to create a Password Manager for businesses.

### Main Features
* REST API that can be self hosted
  * Contains full database structure and endpoints to manage all password vaults and users
  * Fully documented using swagger.
  * Secured with JWT Tokens for API level
  * Different frontends can be securely connected to this API. Organisations have the option to build their own frontend against this API
* Standard Frontend applications which integrate the API
  * A Blazor SPA frontend
  * (chrome)browser extension to allow for autofill throughout the web
  * Overridable logic that allows to customize core behaviour like an overridable Encryption module
* Technical specifications
  * Will allow for users, organisations and teams within organisations
  * Vaults per user, and per organisation
  * Uses the latest techniques of .NET 5 and client side Blazor

  
