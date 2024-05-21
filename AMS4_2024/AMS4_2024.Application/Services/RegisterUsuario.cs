using AMS4_2024.Application.DTOs;
using AMS4_2024.Domain.Interfaces;
using AutoMapper;
using AMS4_2024.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS4_2024.Application.Services
{
    public class RegisterUsuario
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public RegisterUsuario(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task ExecuteAsyncs(UserDto userDto)
        {
            var user = _mapper.Map<Usuario>(userDto);
            user.Senha = BCrypt.Net.BCrypt.HashPassword(userDto.Senha);
            await _usuarioRepository.AddUserAsync(user);
        }
    }
}
