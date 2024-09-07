using Microsoft.AspNetCore.Mvc;
using SysBlogs_Comentario.Dominio;
using SysBlogs_Comentario.Aplicacion;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using SysBlogs_Blogs.Dominio;

namespace SysBlogsDotNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComentarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAccionesComentario _accionesComentario;
        //private readonly ILogger<WeatherForecastController> _logger;

        public ComentarioController(IConfiguration configuration)
        {
            _configuration = configuration;
            _accionesComentario = new AccionesComentario(configuration);
        }

        [HttpGet]
        public IResult Get()
        {
            var respuesta = _accionesComentario.ObtenerListadoComentario();
            return respuesta == null ? Results.NotFound() : Results.Ok(respuesta);
        }
        [HttpGet("{id}")]
        public IResult Get(long id)
        {
            var respuesta = _accionesComentario.ObtenerComentario(id);
            return respuesta == null ? Results.NotFound() : Results.Ok(respuesta);
        }
        [HttpPost]
        public IResult Post([FromBody] Comentario request)
        {
            var respuesta = _accionesComentario.CrearComentario(request);
            return respuesta == null ? Results.NotFound() : Results.Ok(respuesta);
        }
    }
}
