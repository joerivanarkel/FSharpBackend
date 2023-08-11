namespace joerivanarkel.Data

open Microsoft.EntityFrameworkCore
open joerivanarkel.Data.Models
open EntityFrameworkCore.FSharp
open Microsoft.EntityFrameworkCore.Design
open Microsoft.Extensions.DependencyInjection

type Database() =
    inherit DbContext()

    [<DefaultValue>]
    val mutable countries : DbSet<Country>
    
    member this.Countries
        with get() = this.countries
        and set(value) = this.countries <- value

    override _.OnConfiguring(optionsBuilder : DbContextOptionsBuilder) =
        optionsBuilder.UseSqlite("Data Source=database.db") |> ignore

    override _.OnModelCreating(modelBuilder : ModelBuilder) =
        modelBuilder.Entity<Country>().ToTable("countries") |> ignore

type DesignTimeServices() =
    interface IDesignTimeServices with 
        member __.ConfigureDesignTimeServices(serviceCollection: IServiceCollection) = 
            let fSharpServices= EFCoreFSharpServices() :> IDesignTimeServices
            fSharpServices.ConfigureDesignTimeServices serviceCollection
            ()