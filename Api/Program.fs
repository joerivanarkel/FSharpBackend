namespace joerivanarkel.Api
#nowarn "20"
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.OpenApi.Models

module Program =
    open joerivanarkel.Data.Repositories
    open joerivanarkel.Data
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)

        builder.Services.AddControllers()
        builder.Services.AddSwaggerGen(fun c -> 
            c.SwaggerDoc("v1", OpenApiInfo(Title = "Api", Version = "v1"))
        )

        builder.Services.AddScoped<ICountryRepository, CountryRepository>()
        builder.Services.AddDbContext<Database>()

        let app = builder.Build()

        app.UseSwagger()
        app.UseSwaggerUI(fun c -> 
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1")
            c.RoutePrefix <- ""
        )

        app.UseHttpsRedirection()

        app.UseAuthorization()
        app.MapControllers()

        app.Run()

        exitCode
