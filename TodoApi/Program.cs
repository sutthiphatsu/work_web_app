using TodoApi.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var todos = new List<TodoGetDto>
{
    new(1, "Learn Minimal API", false),
    new(2, "Learn Vue", false)
};

app.MapGet("/", () => "Hello Todo API");

app.MapGet("/api/todos", () =>
    Results.Ok(todos));

app.MapGet("/api/todos/{id}", (int id) =>
{
    var todo = todos.FirstOrDefault(x => x.Id == id);

    return todo is null ? Results.NotFound() : Results.Ok(todo);
});

app.MapPost("/api/todos", (TodoPostDto Dto) =>
{
    var nextId = todos.Count == 0 ? 1 : todos.Max(x => x.Id) + 1;
    var newTodo = new TodoGetDto(nextId, Dto.Title, false);
    todos.Add(newTodo);
    return Results.Created($"/api/todos/{newTodo.Id}", newTodo);
});

app.Run();