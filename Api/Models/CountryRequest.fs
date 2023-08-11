namespace joerivanarkel.Api.Models

open joerivanarkel.Data.Models

type CountryRequest = 
    { Name: string
      Code: string
      Capital: string
      Region: string
      Subregion: string }

    member this.Map() : Country =
        { Id = 0
          Name = this.Name
          Code = this.Code
          Capital = this.Capital
          Region = this.Region
          Subregion = this.Subregion }