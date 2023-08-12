namespace joerivanarkel.Api.Controllers

open Microsoft.Extensions.Logging
open Microsoft.AspNetCore.Mvc
open joerivanarkel.Data.Repositories
open joerivanarkel.Api.Models
open joerivanarkel.Data.Models

exception CountryControllerException of string * exn

[<ApiController>]
[<Route("[controller]")>]
type CountryController (logger: ILogger<CountryController>, countryRepository:ICountryRepository) =
    inherit ControllerBase()

    [<HttpGet>]
    member _.Get() : Country list =
        try
            logger.LogInformation("Get all countries")
            countryRepository.GetCountries()
        with ex
            -> logger.LogError(ex, "Error getting all countries")
               raise (CountryControllerException("Error getting all countries", ex))


    [<HttpGet("{id}")>]
    member _.Get(id) : Country =
        try
            logger.LogInformation("Get country by id")
            countryRepository.GetCountry(id) 
        with ex
            -> logger.LogError(ex, "Error getting country by id")
               raise (CountryControllerException("Error getting country by id", ex))

    [<HttpPost>]
    member _.Post([<FromBody>] country: CountryRequest): IActionResult =
        try
            logger.LogInformation("Create country")
            countryRepository.CreateCountry(country.Map()) |> ignore
            base.Ok() :> IActionResult
        with ex
            -> logger.LogError(ex, "Error creating country")
               raise (CountryControllerException("Error creating country", ex))

    [<HttpPut>]
    member _.Put([<FromBody>] country) : IActionResult =
        try
            logger.LogInformation("Update country")
            countryRepository.UpdateCountry(country) |> ignore
            base.Ok() :> IActionResult
        with ex
            -> logger.LogError(ex, "Error updating country")
               raise (CountryControllerException("Error updating country", ex))

    [<HttpDelete>]
    member _.Delete([<FromBody>] country) : IActionResult =
        try
            logger.LogInformation("Delete country")
            countryRepository.DeleteCountry(country) |> ignore
            base.Ok() :> IActionResult
        with ex
            -> logger.LogError(ex, "Error deleting country")
               raise (CountryControllerException("Error deleting country", ex))