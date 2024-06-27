using AutoMapper;
using Azure.Core;
using DOU.GestionOT.API2.Code.Exceptions;
using DOU.GestionOT.BL.Dto;
using DOU.GestionOT.DAL;
using DOU.GestionOT.DAL.Entities;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Threading;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GestionOTContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(DtoMappingProfile));

var app = builder.Build();

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();
        var isDevelopment = app.Environment.IsDevelopment();
        if (exceptionHandler!.Error is DomainException exception)
        {
            await Results.Problem(
                title: exception!.Message,
                detail: isDevelopment ? exception!.StackTrace : null,
                statusCode: 400
                )
            .ExecuteAsync(context);
        }
        else if (exceptionHandler!.Error.InnerException is JsonException jsonException)
        {
            await Results.Problem(
                title: jsonException.Message,
                detail: isDevelopment ? exceptionHandler!.Error.StackTrace : null,
                statusCode: 400
                )
            .ExecuteAsync(context);
        }
        else
        {
            await Results.Problem(
            title: exceptionHandler!.Error.InnerException != null ? exceptionHandler!.Error.InnerException.Message : exceptionHandler!.Error.Message,
            detail: isDevelopment ? exceptionHandler!.Error.StackTrace : null,
            statusCode: 500
            )
            .ExecuteAsync(context);
        }
    });
});

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseReDoc(c =>
{
    c.HideDownloadButton();
    c.DocumentTitle = "Nord Pirineus - PreOrders API Documentation";
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapGet("/api/Ots", async (GestionOTContext _context, IMapper _mapper, CancellationToken cancellationToken) =>
{
    IQueryable<Ot> query = _context.Ot;

    var ots = await query.ToListAsync(cancellationToken);

    var result = _mapper.Map<IEnumerable<OtDto>>(ots);

    return Results.Ok(result);
})
.Produces<IEnumerable<OtDto>>()
.WithName("GetOtDtos")
.WithDescription("Este método será llamado para consultar las OTs en BBDD")
.WithOpenApi();


app.MapGet("api/Ots/{id}", async (int id, GestionOTContext _context, IMapper _mapper, CancellationToken cancellationToken) =>
{

    IQueryable<Ot> query = _context.Ot.Where(x => x.Id == id);

    var ot = await query.FirstOrDefaultAsync(cancellationToken) ?? throw new InvalidOperationException($"Ot '{id}' does not exist");

    var otDto = _mapper.Map<OtDto>(ot);

    return otDto is not null ? Results.Ok(otDto) : Results.NotFound();

})
.Produces<OtDto>()
.Produces(StatusCodes.Status500InternalServerError)
.WithName("GetOtDtoById")
.WithDescription("Este método será llamado para consultar una Ot por su Id")
.WithOpenApi();

app.MapPost("api/Ots", async ([FromBody] OtDto otdto, GestionOTContext _context, IMapper _mapper, CancellationToken cancellationToken) =>
{
    Ot ot = new()
    {
        Estado = otdto.Estado,
        CodigoTipo = otdto.CodigoTipo,
        TipoParte = otdto.TipoParte,
        Ejercicio = otdto.Ejercicio,
        Serie = otdto.Serie,
        Numero = otdto.Numero,
        Tipo = otdto.Tipo,
        Cliente = otdto.Cliente,
        Direccion = otdto.Direccion,
        Fecha = otdto.Fecha
    };

    _context.Ot.Add(ot);
    await _context.SaveChangesAsync(cancellationToken);

    OtDto otdtoNew = _mapper.Map<OtDto>(ot);

    return Results.Created("GetOt", otdtoNew);

})
.Produces<OtDto>()
.WithName("PostOtDto")
.WithDescription("Este método será llamado para añadir a la BBDD una OtDto")
.WithOpenApi();

app.MapPut("api/Ots/{id}", async (int id, [FromBody] OtDto otdto, GestionOTContext _context, IMapper _mapper, CancellationToken cancellationToken) =>
{
    if (id != otdto.Id)
    {
        return Results.BadRequest();
    }

    var ot = await _context.Ot.SingleOrDefaultAsync((x => x.Id == id), cancellationToken: cancellationToken)
                    ?? throw new InvalidOperationException($"Ot with id: '{id}' does not exist");

    ot.Estado = otdto.Estado;
    ot.CodigoTipo = otdto.CodigoTipo;
    ot.TipoParte = otdto.TipoParte;
    ot.Ejercicio = otdto.Ejercicio;
    ot.Serie = otdto.Serie;
    ot.Numero = otdto.Numero;
    ot.Tipo = otdto.Tipo;
    ot.Cliente = otdto.Cliente;
    ot.Direccion = otdto.Direccion;
    ot.Fecha = otdto.Fecha;

    _context.Entry(ot).State = EntityState.Modified;

    await _context.SaveChangesAsync(cancellationToken);

    return Results.NoContent();

})
.Produces<OtDto>()
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status400BadRequest)
.Produces(StatusCodes.Status500InternalServerError)
.WithName("PutOtDtoById")
.WithDescription("Este método será llamado para modificar una Ot por su Id")
.WithOpenApi();

app.MapDelete("api/Ots/{id}", async (int id, GestionOTContext _context, IMapper _mapper, CancellationToken cancellationToken) =>
{
    var ot = await _context.Ot.SingleOrDefaultAsync((x => x.Id == id), cancellationToken: cancellationToken)
                    ?? throw new InvalidOperationException($"Ot with id: '{id}' does not exist");

    _context.Ot.Remove(ot);
    await _context.SaveChangesAsync(cancellationToken);

    return Results.NoContent();

})
.Produces<OtDto>()
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status500InternalServerError)
.WithName("DeleteOtDtoById")
.WithDescription("Este método será llamado para eliminar una Ot por su Id")
.WithOpenApi();

app.Run();

public partial class Program { }
