using ControleDeContatos.Data;
using ControleDeContatos.Helper;
using ControleDeContatos.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Baixar extenção Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
            // Para atualizar sem ter que pausar a aplicação
            // SUBSTITUIR: builder.Services.AddControllersWithViews(); --> Versões anteriores
            // POR: builder.Services.AddRazorPages().AddRazorRuntimeCompilation(); --> Versões atuais
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

            // Conexão com o Banco
            builder.Services.AddDbContext<BancoContext>
                (options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

            // Faz que quando for chamado a interface ("IClass"), chame a classe ("Class")
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<ISessao, Sessao>();
            builder.Services.AddScoped<IEmail, Email>();

            // Cookies
            builder.Services.AddSession(o =>
            {
                o.Cookie.HttpOnly = true;
                o.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            //Rota padrão
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

/*
    *Migrations*
    ----------------------------------------------------------------------------------------
    ->Comando para criar Migration de Contatos:
    Add-Migration  CriandoTabelaContatos -Context BancoContext

    ->Comando para criar Migration de Usuários:
    Add-Migration  CriandoTabelaContatos -Context BancoContext

    ->Comando para atualizar o banco, criando as tabelas através das Migrations:
    Update-Database -Context BancoContext

    ----------------------------------------------------------------------------------------
    ->Comando que criou a Migration de relacionamento OneToMany entre Usuários1 -- *Contatos:
    Add-Migration CriandoVinculoUsuarioNaContato -Context BancoContext

    ->Comando para atualizar as tabela do banco com o relacionamento OneToMany:
    Update-Database -Context BancoContext
*/
