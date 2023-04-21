using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Models;
using Tasks.Repositorios.Interfaces;

namespace Tasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;

        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioModel>>> BuscarTodosUsuarios()
        {
            List<UsuarioModel> usuarios =  await _usuarioRepositorio.BuscarTodosUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioModel>> BuscarUsuarioPorId(int id)
        {
            UsuarioModel usuario = await _usuarioRepositorio.BuscarUsuarioPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> CadastrarUsuario([FromBody] UsuarioModel usuario)
        {
            UsuarioModel usuarioCadastrado = await _usuarioRepositorio.Adicionar(usuario);
            return Ok(usuarioCadastrado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioModel>> AtualizarUsuario([FromBody] UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioAtualizado = await _usuarioRepositorio.Atualizar(usuario, id);
            if (usuarioAtualizado == null)
            {
                return NotFound();
            }
            return Ok(usuarioAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> ApagarUsuario(int id)
        {
            bool usuarioApagado = await _usuarioRepositorio.Apagar(id);
            if (usuarioApagado == false)
            {
                return NotFound();
            }
            return Ok(usuarioApagado);
        }
    }
}
