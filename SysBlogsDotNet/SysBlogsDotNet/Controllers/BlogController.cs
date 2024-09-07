using Microsoft.AspNetCore.Mvc;
using SysBlogs_Blogs.Dominio;
using SysBlogs_Autor.Aplicacion;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using SysBlogs_Blogs.Aplicacion;

namespace SysBlogsDotNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAccionesBlog _accionesBlog;
        //private readonly ILogger<WeatherForecastController> _logger;

        public BlogController(IConfiguration configuration)
        {
            _configuration = configuration;
            _accionesBlog = new AccionesBlog(configuration);
        }

        [HttpGet]
        public IResult Get()
        {
            var respuesta = _accionesBlog.ObtenerListadoBlog();
            return respuesta == null ? Results.NotFound() : Results.Ok(respuesta);
        }
        [HttpGet("{id}")]
        public IResult Get(long id)
        {
            var respuesta = _accionesBlog.ObtenerBlog(id);
            return respuesta == null ? Results.NotFound() : Results.Ok(respuesta);
        }
        [HttpPost]
        public IResult Post([FromBody] Blog request)
        {
            var respuesta = _accionesBlog.CrearBlog(request);
            return respuesta == null ? Results.NotFound() : Results.Ok(respuesta);
        }

        [HttpPut("{id}")]
        public IResult Put(long id, [FromBody] Blog request)
        {
            request.ID = id;
            _accionesBlog.ActualizarBlog(request);
            return Results.Ok();
        }
    }
}
