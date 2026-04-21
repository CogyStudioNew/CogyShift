using Microsoft.EntityFrameworkCore;
using CogyShift.Data;
using CogyShift.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=cogyshift.db"));

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

app.MapGet("/history", async (AppDbContext db) =>
{
    var records = await db.ShiftRecords
        .OrderByDescending(r => r.CalculatedAt)
        .Take(20)
        .ToListAsync();
    
    return records;
});

app.MapPost("/shift/calculate", async (ShiftRequest request, AppDbContext db) =>
{
    var grossSalary = (request.Hours * request.Rate) + request.Bonus;
    var tax = grossSalary * 0.13m;
    var netSalary = grossSalary - tax;
    
    var record = new ShiftRecord
    {
        CalculatedAt = DateTime.Now,
        Hours = request.Hours,
        Rate = request.Rate,
        Bonus = request.Bonus,
        GrossSalary = grossSalary,
        Tax = tax,
        NetSalary = netSalary
    };
    
    db.ShiftRecords.Add(record);
    await db.SaveChangesAsync();
    
    return new
    {
        Hours = request.Hours,
        Rate = request.Rate,
        Bonus = request.Bonus,
        GrossSalary = grossSalary,
        Tax = tax,
        NetSalary = netSalary,
        SavedId = record.Id
    };
});

app.Run();

record ShiftRequest(decimal Hours, decimal Rate, decimal Bonus);