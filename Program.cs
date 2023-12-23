using EULS.Model;
using EULS.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Enable CORS.
builder.Services.AddCors(options => {
    options.AddPolicy("default", policy => {
        policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    });
});

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
app.UseCors("default");

app.MapGet("/", () => {
    return "EULS API. Created by Boyd. Developed in .NET";
});

app.MapGet("/api/html", async () =>
{
    // Create new Util object.
    Util util = new();

    // Obtain the list of subjects.
    List<Subject> subjects = await util.GetSubjects();

    // Return the list.
    return subjects;
});

app.MapPost("/api/pdf", async (context) =>
{
    using (StreamReader streamReader = new StreamReader(context.Request.Body, Encoding.UTF8)) {
        // Read the request body.
        string requestBody = await streamReader.ReadToEndAsync();

        // Extract the data from the request body.
        string[] pdfs = JsonSerializer.Deserialize<string[]>(requestBody);

        // Create new PDF object.
        PDF pdf = new();

        // Build the timetable.
        List<Timetable> timetables = await pdf.GeneratePDF(pdfs);

        // Return the timetable.
        await context.Response.WriteAsJsonAsync(timetables);
    }
});

app.Run();
