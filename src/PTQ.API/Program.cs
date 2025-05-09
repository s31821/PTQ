using PTQ.Application;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddSingleton<PTQService, PTQService>(ptqService => new PTQService(connectionString));
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/api/quizzes", (PTQService ptqService) =>
{
    try
    {
        return Results.Ok(ptqService.GetQuizzes());
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapGet("/api/quizzes/{id:int}", (int id, PTQService ptqService) =>
{
    try
    {
        return Results.Ok(ptqService.GetQuizById(id));
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
    
});
app.MapPost("/api/quizzes/", () => "Hello World!");

app.Run();