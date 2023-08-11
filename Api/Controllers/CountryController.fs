namespace joerivanarkel.Api.Controllers

open Microsoft.Extensions.Logging
open Microsoft.AspNetCore.Mvc
open joerivanarkel.Data.Repositories
open joerivanarkel.Data.Models

[<ApiController>]
[<Route("[controller]")>]
type CountryController (logger: ILogger<CountryController>, countryRepository:ICountryRepository) =
    inherit ControllerBase()

    [<HttpGet>]
    member _.Get() : Country list =
        logger.LogInformation("Get all countries")
        countryRepository.GetCountries()

    [<HttpGet("{id}")>]
    member _.Get(id) : Country =
        logger.LogInformation("Get country by id")
        countryRepository.GetCountry(id) 

    [<HttpPost>]
    member _.Post(country) =
        logger.LogInformation("Create country")
        countryRepository.CreateCountry(country) |> Ok

    [<HttpPut>]
    member _.Put(country) =
        logger.LogInformation("Update country")
        countryRepository.UpdateCountry(country) |> Ok

    [<HttpDelete("{id}")>]
    member _.Delete(id) =
        logger.LogInformation("Delete country")
        countryRepository.DeleteCountry(id) |> Ok