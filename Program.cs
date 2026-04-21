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
    decimal salary = (request.Hours * request.Rate) + request.Bonus;
    
    return new 
    { 
        Hours = request.Hours, 
        Rate = request.Rate, 
        Bonus = request.Bonus,
        TotalSalary = salary 
    };
});

app.Run();

// Модель должна быть ОПРЕДЕЛЕНА ПОСЛЕ app.Run()?
// НЕТ! В C# record должен быть определён ДО app.Run() в той же области видимости.
// Но правильнее вынести его ЗА ПРЕДЕЛЫ Main, то есть ПОСЛЕ app.Run()? В минимальном API можно определить record после, но некоторые компиляторы ругаются.
// Самый надёжный способ: определить record перед вызовом app.Run() и внутри того же файла, но вне основного тела.

// Проще всего: объявить record прямо перед app.Run():

record ShiftRequest(decimal Hours, decimal Rate, decimal Bonus);