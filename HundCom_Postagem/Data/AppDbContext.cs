using Microsoft.EntityFrameworkCore;
using HundCom_Postagem.Models.Entities;
using System.Reflection.Emit;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }


    //Sobrescrevendo o método OnModelCreating para configurar a relação entre objetos
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Postagem>()
            .HasOne(post => post.Topico)
            .WithMany(topic => topic.ListaPostagens)
            .HasForeignKey(post => post.TopicoId);

        builder.Entity<Comentario>()
            .HasOne(comment => comment.Postagem)
            .WithMany(post => post.Comentarios)
            .HasForeignKey(comment => comment.PostagemId);
    }

    public DbSet<Topico> Topicos { get; set; }
    public DbSet<Postagem> Postagens { get; set; }
    public DbSet<Comentario> Comentarios { get; set; }
}
