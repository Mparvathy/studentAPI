var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

var students = new List<Student>
{
    new Student(1, "Arjun",  "Computer Science", 85),
    new Student(2, "Priya",  "Mathematics",      90),
    new Student(3, "Rahul",  "Physics",          78),
};

app.MapGet("/students", () => students);

app.MapGet("/students/{id}", (int id) =>
{
    var student = students.FirstOrDefault(s => s.Id == id);
    return student is not null ? Results.Ok(student) : Results.NotFound("Student not found");
});

app.MapPost("/students", (Student s) =>
{
    students.Add(s);
    return Results.Created($"/students/{s.Id}", s);
});

app.MapDelete("/students/{id}", (int id) =>
{
    var student = students.FirstOrDefault(s => s.Id == id);
    if (student is null) return Results.NotFound("Student not found");
    students.Remove(student);
    return Results.Ok("Deleted!");
});

app.Run();

record Student(int Id, string Name, string Department, int Marks);