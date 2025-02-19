using Tax.Simulator;
using Tax.Simulator.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapGet("/api/tax/calculate",
        (string situationFamiliale, decimal salaireMensuel, decimal salaireMensuelConjoint, int nombreEnfants) =>
        {          
            try
            {
                Situation situation = new(situationFamiliale, salaireMensuel, salaireMensuelConjoint, nombreEnfants);
                return Results.Ok(
                    Simulateur.CalculerImpotsAnnuel(
                        situation)
                );
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        })
    .WithName("CalculateTax");

await app.RunAsync();

public partial class Program;