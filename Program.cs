var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api", () => "ShiftKeeper API works!");
app.MapGet("/shift", () => new { Message = "Hello from ShiftController" });

app.MapPost("/shift/calculate", (ShiftRequest request) =>
{
    decimal grossSalary = (request.Hours * request.Rate) + request.Bonus;
    decimal tax = grossSalary * 0.13m;
    decimal netSalary = grossSalary - tax;
    
    return new
    {
        Hours = request.Hours,
        Rate = request.Rate,
        Bonus = request.Bonus,
        GrossSalary = grossSalary,
        Tax = tax,
        NetSalary = netSalary
    };
});

app.Run();

record ShiftRequest(decimal Hours, decimal Rate, decimal Bonus);