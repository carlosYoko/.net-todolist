using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TodoList.Automappers;
using TodoList.DTOs;
using TodoList.Models;
using TodoList.Repository;
using TodoList.Services;
using TodoList.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddKeyedScoped<ICommonService<TodoDto, TodoInsertDto, TodoUpdateDto>, TodoService>("todoService"); 

// Repositoryu
builder.Services.AddScoped<IRepository<ToDo>, TodoRepository>();

// Entity Framework
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});

// Validators
builder.Services.AddScoped<IValidator<TodoInsertDto>, TodoInsertValidator>();
builder.Services.AddScoped<IValidator<TodoUpdateDto>, TodoUpdateValidator>();

// Mappers
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();