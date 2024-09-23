using Lab4;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("myConfig/books.json");
builder.Configuration.AddJsonFile("myConfig/users.json");
builder.Services.Configure<List<Book>>(builder.Configuration.GetSection("Books"));
builder.Services.Configure<List<User>>(builder.Configuration.GetSection("Users"));

var app = builder.Build();

app.MapGet("/", (HttpContext httpContext) =>
{
    var body = "<a href='library/'>/library/</a><br>"
                   + "<a href='library/books'>/library/books</a><br>"
                   + "<a href='library/profile/'>/library/profile</a>";
    
    httpContext.Response.WriteAsync(body);
});
    
app.MapGet("/library", () =>
{
    return "Hello from library!";
});

app.MapGet("/library/books", (IOptions<List<Book>> books, HttpContext httpContext) =>
{
    httpContext.Response.WriteAsync("<p>Books: </p>");
    
    httpContext.Response.WriteAsync("<ul>");
    foreach (var book in books.Value) { httpContext.Response.WriteAsync("<li>" + book.ToString() + "</li>"); }
    httpContext.Response.WriteAsync("</ul>");
    
    httpContext.Response.WriteAsync("<a href='/'>Back to the Main page</a>");
});

app.MapGet("/library/profile/{id:int:min(0):max(5)?}", (IOptions<List<User>> users, int? id) =>
{
    if (id == null)
    {
        return users.Value[0].ToString();
    }
    else
    {
        return users.Value[(int)id].ToString();
    }
});

app.Run();