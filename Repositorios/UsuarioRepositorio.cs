using Microsoft.EntityFrameworkCore;
using Tasks.Data;
using Tasks.Models;
using Tasks.Repositorios.Interfaces;

namespace Tasks.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly TasksSystemDBContext _systemTaskDbContext;
        public UsuarioRepositorio(TasksSystemDBContext systemTaskDbContext)
        {
            _systemTaskDbContext = systemTaskDbContext;
        }

        public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
        {
            return await _systemTaskDbContext.Usuario.ToListAsync();
        }

        public async Task<UsuarioModel> BuscarUsuarioPorId(int id)
        {
            return await _systemTaskDbContext.Usuario.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
             await _systemTaskDbContext.Usuario.AddAsync(usuario);
             await _systemTaskDbContext.SaveChangesAsync();

            return usuario;
        }

        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioPorId = await BuscarUsuarioPorId(id);
            if(usuarioPorId == null)
            {
                throw new Exception($"Usuario para o ID: {id} não encontrado");
            }

            usuarioPorId.Nome = usuario.Nome;
            usuarioPorId.Email = usuario.Email;

            _systemTaskDbContext.Usuario.Update(usuarioPorId);
            await _systemTaskDbContext.SaveChangesAsync();

            return usuarioPorId;

        }

        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioPorId = await BuscarUsuarioPorId(id);
            if (usuarioPorId == null)
            {
                throw new Exception($"Usuario para o ID: {id} não encontrado");
            }

            _systemTaskDbContext.Usuario.Remove(usuarioPorId);
            await _systemTaskDbContext.SaveChangesAsync();

            return true;
        }
    }
}
