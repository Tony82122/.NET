using EntityRepository;
using FileRepositories;
using Microsoft.EntityFrameworkCore;
using AppContext = EfcRepositories.AppContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUserRepo, UserFileRepository>();
builder.Services.AddTransient<ICommentRepository, CommentFileRepository>();
builder.Services.AddTransient<IPostRepo, PostFileRepository>();
builder.Services.AddDbContext<AppContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();