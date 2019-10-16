# NetCoreRecipes
Demonstration code - as a poc solution.
Each topic is presented in seperate project located in particular folder.
Each topic has a dedicated branch on repository (also merged to master).
Each commit within one branch shows the different ways or extension of usage partical issue.
(todo: wll be listed below)

Demostration code present only simple poc so the projects are simplified  to not hidden the main topic.

1. Swagger (first commit on master only)
 - Install swagger from nuget and add initial configuration
 - Retrived description from XML comments (from summary section)
 - Using of ProducesResponseType. CustomConvention for response types presented.
 - Using DefaultApiConventions
 - Using ResponseType alongside responseCode
 
2. Configuration
 - Added initial project. Add configuration appsettings.json, Get particular setting by IConfiguration
 - Binding to poco
 - Register settings and inject them to controller by using IOptions
 
3. Serilog
 - Initial project from template
 - Install serilog and particular sinks (console, file) from nuget
 - Configure logger to use Console and File
 - Restrict logging to minimum level for console
 - Register and inject serilog logger into controller ctrol
 - Get serilog configuration from appsettings
 - Implement custom enricher. Use custom and/or nuget enricher in .cs code
 - Use custom and/or nuget enricher in appsetting.json
 - Add and configure ElasticSearch sink.
 - Add WithExceptionDetails
 
4. HttpClientFactory
 - Initial project from template
 - Added models and prepare sample API. Swagger added also
 - Present inproper using of httpClient (without httpClientFactory)
 - service with basic usage of httpClientFactory
 - service with named usage of httpClientFactory
 - service with typed usage of httpClientFactory
 
5. Polly
 - Initial project from template
 - Added simple service with HttpClient. Using onine service to mock response.
 - Install Polly library from nuGet. Added simple retry policy using custom handler extension
 - Add jitter to retry policy (simple. for production use Poly.Contrib)
 - Add Polly.Contrib.WaitAndRetry. Present delay with jitter from Poly.Contrib library
 - Logging when retry is executed
 - RetryAfter handler - when response code is 429
 - Refresh access token based on https://www.jerriepelser.com/blog/refresh-google-access-token-with-polly/
 - Register policy in PolicyRegistry

6. RestEase
 - Initial project from template
 - Install RestEase from nuget
 - Prepared Api and service Without RestEase
 - Using ReasEase in sample serivce
 - Present different variants to define Reast Ease Api
 - Add integration with httpClientFactory. Version 1 - not convenient
 - Add integration with httpClientFactory Version 2 (optionally with Polly)
 
7. MediatR
 - Initial project from template
 - Add MediatR + DI extension from nuget
 - Simple event handler (one way)
 - Simple requestHandler
 - One way Request Handler
 - MediatR pipeline
 
8. Secrets
 - Initial project from template
 - Set secret in secret manager tool
 - Use AzureKeyVault (App not hosted on Azure) using Self-signed certificate
 - AzureKeyVault Managed identities for Azure resources (App hosted on Azure)
 - KeyVaultClient
 
9. Cookies Auth
 - Initial project from template
 - Simple authentication
 - OnRedirectToLogin handled to return 401 instead 404
 - Set login and logout path
 - Add login
 - Add logout
 - Logout when user data are changed i.e. removed.
 - OnRedirectToLogin handler added
 - Persistance and total expiation of 15 min
 - Role-based authorization
 - Claim-based authorization
 - Policy-based auth. (using : IAuthorizationRequirement)
 - Aggregated policy requiremnt in configuration
 - Custom Policy Provider part 1
 - Custom policy provider part 2
 - Custom policy provider part 3
 - Scheme based authorization
 
10. Cache
 - Initial project from template
 - Add swagger and sample api +service
 - In-Memory cache ver.1
 - In Memory cache v2 (GetOrCreateAsync)
 - Distributed  cache (AddDistributedMemoryCache)
 - Distributed  cache (Redis)
 - Distributed Memory: add options
 
11. ProxyKit
 - Initial project from template
 - Install package from nuget
 - Simple proxy forward - host replacement
 - Handler implemented IProxyHandler
 - Conditional path
 - Example with Bilder extension to run proxy
 
12. Application Insight
 - Initial project from template
 - Add ApplicationInsights.AspNetCore from nuget
 - Register ApplicationInsightsTelemetry. Trazk event
 - Integration with ILogger (defaulted)
 - Integration with ILogger (change defaulted minimum log level collected by AI)
