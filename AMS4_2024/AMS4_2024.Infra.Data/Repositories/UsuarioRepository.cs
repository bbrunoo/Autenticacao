using AMS4_2024.Domain.Interfaces;
using AMS4_2024.Infra.Data.Context;
using AMS4_2024.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS4_2024.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(Usuario usuario)
        {
            await _context.Usuario.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UsuarioExistenteAsync(string email)
        {
            return await _context.Usuario.AnyAsync(u => u.Email == email);
        }
        public async Task<Usuario> GetUsuarioByEmail(string email)
        {
            return await _context.Usuario.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await _context.Usuario.ToListAsync();
        }

        public async Task<bool> RemoveUsuarioByEmail(string email)
        {
            var user = await _context.Usuario.FirstOrDefaultAsync(e => e.Email == email);
            if (user == null)
            {
                return false;
            }

            _context.Usuario.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
