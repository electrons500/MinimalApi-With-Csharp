using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minimal_Api.Models;
using Minimal_Api.Models.Data.ApiModels;
using Minimal_Api.Models.Data.Service;
using Minimal_Api.Models.Data.UsersManagementDBContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UsersManagementDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("Conn"));
});

builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<EmployeeService>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/api/Department/GetDepartments", ([FromServices] DepartmentService departmentService ) =>
{
    return Results.Ok(departmentService.GetDepartments());
})
.WithName("GetDepartments")
.WithTags("Department")
.WithOpenApi();

app.MapGet("/api/Department/GetDepartment/{id}", ([FromServices] DepartmentService departmentService,int id ) =>
{
    return Results.Ok(departmentService.GetDepartmentById(id));
})
.WithName("GetDepartment")
.WithTags("Department")
.WithOpenApi();

app.MapPost("/api/Department/AddDepartment", ([FromServices] DepartmentService departmentService, [FromBody] DepartmentApiModel model) =>
{
	try
	{
        bool result = departmentService.AddDepartment(model);
        if (result)
        {
            return Results.Ok("New department successfully saved!");
        }

        throw new Exception();
        
    }
	catch (Exception)
	{

		throw;
	}
})
.WithName("AddDepartment")
.WithTags("Department")
.WithOpenApi();

app.MapPut("/api/Department/UpdateDepartment", ([FromServices] DepartmentService departmentService, [FromBody] DepartmentApiModel model) =>
{
	try
	{
        bool result = departmentService.UpdateDepartment(model);
        if (result)
        {
            return Results.Ok("Department successfully updated!");
        }

        throw new Exception();
        
    }
	catch (Exception)
	{

		throw;
	}
})
.WithName("UpdateDepartment")
.WithTags("Department")
.WithOpenApi();

app.MapDelete("/api/Department/DeleteDepartment/{id}", ([FromServices] DepartmentService departmentService, int id) =>
{
	try
	{
        bool result = departmentService.DeleteDepartment(id); 
        if (result) 
        {
            return Results.Ok("Department successfully deleted!");
        }

        throw new Exception();
        
    }
	catch (Exception)
	{

		throw;
	}
})
.WithName("DeleteDepartment")
.WithTags("Department")
.WithOpenApi();

//Employees endpoints
app.MapGet("/api/Employee/GetEmployees", ([FromServices] EmployeeService employeeService) =>
{
    return Results.Ok(employeeService.GetEmployees());
})
.WithName("GetEmployees")
.WithTags("Employees")
.WithOpenApi();

app.MapGet("/api/Employee/GetEmployees/{id}", ([FromServices] EmployeeService employeeService,int id) =>
{
    return Results.Ok(employeeService.GetEmployeeById(id));
})
.WithName("GetEmployeeById")
.WithTags("Employees")
.WithOpenApi();

app.MapPost("/api/Employee/AddEmployee", ([FromServices] EmployeeService employeeService, [FromBody] AddEmployeeApiModel model) =>
{
    try
    {
        bool result = employeeService.AddEmployee(model);
        if (result)
        {
            return Results.Ok("Employee successfully added!");
        }

        throw new Exception();

    }
    catch (Exception)
    {

        throw;
    }
})
.WithName("AddEmployee")
.WithTags("Employees")
.Produces(200)
.Produces(400)
.WithOpenApi();


app.MapPut("/api/Employee/UpdateEmployee", ([FromServices] EmployeeService employeeService, [FromBody] EmployeeModel model) =>
{
    try
    {
        bool result = employeeService.UpdateEmployee(model);
        if (result)
        {
            return Results.Ok("Employee successfully updated!");
        }

        throw new Exception();

    }
    catch (Exception)
    {

        throw;
    }
})
.WithName("UpdateEmployee")
.WithTags("Employees")
.WithOpenApi();


app.MapDelete("/api/Employee/DeleteEmployee", ([FromServices] EmployeeService employeeService,int id) =>
{
    try
    {
        bool result = employeeService.DeleteEmployee(id);
        if (result)
        {
            return Results.Ok("Employee successfully deleted!");
        }

        throw new Exception();

    }
    catch (Exception)
    {

        throw;
    }
})
.WithName("DeleteEmployee")
.WithTags("Employees")
.WithOpenApi();



app.Run();
