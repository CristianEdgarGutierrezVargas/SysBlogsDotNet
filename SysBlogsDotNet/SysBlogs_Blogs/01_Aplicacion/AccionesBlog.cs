using Azure;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SysBlogs_Blogs.Dominio;
using SysBlogs_Blogs.Infraestructura.ImplementacionBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SysBlogs_Blogs.Aplicacion
{
    public class AccionesBlog:IAccionesBlog
    {
        internal IBlogRepositorio _repositorio;
        public AccionesBlog(IConfiguration configuration) 
        {
            _repositorio = new BlogSqlServer(configuration);
        }

        public string CrearBlog(Blog _autor)
        {
            var objeto = _repositorio.CrearBlog(_autor);
            string respuestaJson = JsonConvert.SerializeObject(objeto);
            return respuestaJson;
        }
        public string ObtenerBlog(long _id)
        {
            var objeto = _repositorio.ObtenerBlog(_id);
            if (objeto == null) 
                 return null; 
            string respuestaJson = JsonConvert.SerializeObject(objeto);
            return respuestaJson;
        }
        public void ActualizarBlog(Blog _autor)
        {
            _repositorio.ActualizarBlog(_autor);
        }

        public string ObtenerListadoBlog()
        {
            var lista = _repositorio.ObtenerListadoBlogs();
            if (lista == null)
                return null;
            string respuestaJson= JsonConvert.SerializeObject(lista);
            return respuestaJson;
        }
    }
}
