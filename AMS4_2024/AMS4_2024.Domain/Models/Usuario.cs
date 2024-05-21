using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AMS4_2024.Domain.Models;

public partial class Usuario
{
    [Key]
    public Guid Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public bool Isadmin { get; set; }

    public Usuario(string nome, string email, string senha, bool isadmin)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Email = email;
        Senha = senha;
        Isadmin = isadmin;
    }

    public Usuario()
    {
    }
}
