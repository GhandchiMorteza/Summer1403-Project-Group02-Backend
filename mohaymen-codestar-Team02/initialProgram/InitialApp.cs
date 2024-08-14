using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using mohaymen_codestar_Team02.Data;

namespace mohaymen_codestar_Team02.initialProgram;

public class InitialApp
{
    public static void Init(WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen();
        /*
        builder.Services.AddSwaggerGen(c =>
            c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Mohaymen_Project_Group02", Version = "v1" })
        );*/

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddControllers();
        var app = builder.Build();
        //app.UseSwagger();
        //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "mohaymen-codestar-Team02.csproj"));

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseDeveloperExceptionPage();
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}