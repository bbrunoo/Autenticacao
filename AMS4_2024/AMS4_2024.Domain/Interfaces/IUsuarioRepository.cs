using AMS4_2024.Domain.Models;

namespace AMS4_2024.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task AddUserAsync(Usuario usuario);
        Task<IEnumerable<Usuario>> GetAll();
        Task<Usuario> GetUsuarioByEmail(string email);
        Task<bool> RemoveUsuarioByEmail(string email);
        Task<bool> UsuarioExistenteAsync(string email);  
    }
}
