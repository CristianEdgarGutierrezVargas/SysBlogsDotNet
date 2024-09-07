using Microsoft.AspNetCore.Mvc;
using SysBlogs_Autor.Dominio;
using SysBlogs_Autor.Aplicacion;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace SysBlogsDotNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutorController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAccionesAutor _accionesAutor;
        //private readonly ILogger<WeatherForecastController> _logger;

        public AutorController(IConfiguration configuration)
        {
            _configuration = configuration;
            _accionesAutor = new AccionesAutor(configuration);
        }

        [HttpGet]
        public IResult Get()
        {
            var respuesta = _accionesAutor.ObtenerListadoAutores();
            return respuesta == null ? Results.NotFound() : Results.Ok(respuesta);
        }
        [HttpGet("{id}")]
        public IResult Get(long id)
        {
            var respuesta = _accionesAutor.ObtenerAutor(id);
            return respuesta == null ? Results.NotFound() : Results.Ok(respuesta);
        }
        [HttpPost]
        public IResult Post([FromBody] Autor request)
        {
            var respuesta = _accionesAutor.CrearAutor(request);
            return respuesta == null ? Results.NotFound() : Results.Ok(respuesta);
        }
    }
}
