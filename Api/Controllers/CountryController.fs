namespace joerivanarkel.Api.Controllers

open Microsoft.Extensions.Logging
open Microsoft.AspNetCore.Mvc
open joerivanarkel.Data.Repositories
open joerivanarkel.Api.Models
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
    member _.Post([<FromBody>] country: CountryRequest): IActionResult =
        logger.LogInformation("Create country")
        countryRepository.CreateCountry(country.Map()) |> ignore
        base.Ok() :> IActionResult

    [<HttpPut>]
    member _.Put([<FromBody>] country) : IActionResult =
        logger.LogInformation("Update country")
        countryRepository.UpdateCountry(country) |> ignore
        base.Ok() :> IActionResult

    [<HttpDelete>]
    member _.Delete([<FromBody>] country) : IActionResult =
        logger.LogInformation("Delete country")
        countryRepository.DeleteCountry(country) |> ignore
        base.Ok() :> IActionResult