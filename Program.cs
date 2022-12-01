using FluentAPIDemo;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IValidator<CalcRequest>, CalcRequestValidator>();
builder.Services.AddScoped<IValidator<AddRequest>, AddRequestValidator>();
builder.Services.AddScoped<IValidator<DivRequest>, DivRequestValidator>();
builder.Services.AddScoped<IValidator<SubRequest>, SubRequestValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapPost("/calc/add", async ([FromServices] IValidator<AddRequest> validator, [FromBody] AddRequest req) =>
    {
        var valResults = await validator.ValidateAsync(req);
        if (valResults.IsValid == false)
        {
            return Results.ValidationProblem(valResults.ToDictionary());
        }

        return Results.Ok(req.A + req.B);
    })
    .WithName("CalcAdd")
    .ProducesValidationProblem(400)
    .Produces(200)
    .WithOpenApi();


app.MapPost("/calc/div", async ([FromServices] IValidator<DivRequest> validator, [FromBody] DivRequest req) =>
    {
        var valResults = await validator.ValidateAsync(req);
        if (valResults.IsValid == false)
        {
            return Results.ValidationProblem(valResults.ToDictionary());
        }

        return Results.Ok(req.A / req.B);
    })
    .WithName("CalcDiv")
    .ProducesValidationProblem(400)
    .Produces(200)
    .WithOpenApi();

app.MapPost("/calc/sub", async ([FromServices] IValidator<SubRequest> validator, [FromBody] SubRequest req) =>
    {
        var valResults = await validator.ValidateAsync(req);
        if (valResults.IsValid == false)
        {
            return Results.ValidationProblem(valResults.ToDictionary());
        }

        return Results.Ok(req.A - req.B);
    })
    .WithName("CalcSub")
    .ProducesValidationProblem(400)
    .Produces(200)
    .WithOpenApi();

app.Run();