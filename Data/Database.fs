namespace joerivanarkel.Data

open Microsoft.EntityFrameworkCore
open joerivanarkel.Data.Models

type Database() =
    inherit DbContext()

    [<DefaultValue>]
    val mutable countries : DbSet<Country>
    
    member this.Countries
        with get() = this.countries
        and set(value) = this.countries <- value

    override this.OnConfiguring(optionsBuilder : DbContextOptionsBuilder) =
        optionsBuilder.UseSqlite("Data Source=database.db") |> ignore

    override this.OnModelCreating(modelBuilder : ModelBuilder) =
        modelBuilder.Entity<Country>().ToTable("countries") |> ignore