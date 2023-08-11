namespace joerivanarkel.Data.Repositories

open joerivanarkel.Data
open joerivanarkel.Data.Models

exception CountryRepositoryException of string * exn

type ICountryRepository =
    abstract member GetCountries : unit -> Country list
    abstract member GetCountry : int -> Country
    abstract member CreateCountry : Country -> bool
    abstract member UpdateCountry : Country -> bool
    abstract member DeleteCountry : Country -> bool

type CountryRepository(database: Database) = 
    interface ICountryRepository with
        member this.GetCountries() : Country list =
            try
                database.Countries
                |> Seq.toList
            with ex 
                -> raise (CountryRepositoryException("Could not find countries", ex))

        member this.GetCountry(id: int) : Country =
            try
                database.Countries
                |> Seq.find (fun c -> c.Id = id)
            with ex
                -> raise (CountryRepositoryException(sprintf "Could not find country with id %d" id, ex))

        member this.CreateCountry(country: Country) : bool =
            try
                database.Countries.Add(country) |> ignore
                database.SaveChanges() > 0 |> ignore
                true
            with ex
                -> raise (CountryRepositoryException(sprintf "Could not create country %A" country, ex))

        member this.UpdateCountry(country: Country) : bool =
            try
                database.Countries.Update(country) |> ignore
                database.SaveChanges() > 0 |> ignore
                true
            with ex
                -> raise (CountryRepositoryException(sprintf "Could not update country %A" country, ex))
        
        member this.DeleteCountry(country: Country) : bool =
            try
                database.Countries.Remove(country) |> ignore
                database.SaveChanges() > 0 |> ignore
                true
            with ex
                -> raise (CountryRepositoryException(sprintf "Could not delete country %A" country, ex))
