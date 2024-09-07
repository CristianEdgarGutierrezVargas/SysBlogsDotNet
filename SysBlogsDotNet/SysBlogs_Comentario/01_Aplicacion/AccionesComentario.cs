using Azure;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SysBlogs_Blogs.Dominio;
using SysBlogs_Comentario.Dominio;
using SysBlogs_Comentario.Infraestructura.ImplementacionBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SysBlogs_Comentario.Aplicacion
{
    public class AccionesComentario:IAccionesComentario
    {
        internal IComentarioRepositorio _repositorio;
        public AccionesComentario(IConfiguration configuration) 
        {
            _repositorio = new ComentarioSqlServer(configuration);
        }

        public string CrearComentario(Comentario _autor)
        {
            var objeto = _repositorio.CrearComentario(_autor);
            string respuestaJson = JsonConvert.SerializeObject(objeto);
            return respuestaJson;
        }
        public string ObtenerComentario(long _id)
        {
            var objeto = _repositorio.ObtenerComentario(_id);
            if (objeto == null) 
                 return null; 
            string respuestaJson = JsonConvert.SerializeObject(objeto);
            return respuestaJson;
        }
        //public void ActualizarComentario(Comentario _autor)
        //{
        //    _repositorio.ActualizarComentario(_autor);
        //}

        public string ObtenerListadoComentario()
        {
            var lista = _repositorio.ObtenerListadoComentario();
            if (lista == null)
                return null;
            string respuestaJson= JsonConvert.SerializeObject(lista);
            return respuestaJson;
        }
    }
}
