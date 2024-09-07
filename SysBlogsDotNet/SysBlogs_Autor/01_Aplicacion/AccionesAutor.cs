using Azure;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SysBlogs_Autor.Dominio;
using SysBlogs_Autor.Infraestructura.ImplementacionBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SysBlogs_Autor.Aplicacion
{
    public class AccionesAutor:IAccionesAutor
    {
        internal IAutorRepositorio _repositorio;
        public AccionesAutor(IConfiguration configuration) 
        {
            _repositorio = new AutorSqlServer(configuration);
        }

        public string CrearAutor(Autor _autor)
        {
            var objeto = _repositorio.CrearAutor(_autor);
            string respuestaJson = JsonConvert.SerializeObject(objeto);
            return respuestaJson;
        }
        public string ObtenerAutor(long _id)
        {
            var objeto = _repositorio.ObtenerAutor(_id);
            if (objeto == null) 
                 return null; 
            string respuestaJson = JsonConvert.SerializeObject(objeto);
            return respuestaJson;
        }
        public void ActualizarAutor(Autor _autor)
        {
            _repositorio.ActualizarAutor(_autor);
        }

        public string ObtenerListadoAutores()
        {
            var lista = _repositorio.ObtenerListadoAutores();
            if (lista == null)
                return null;
            string respuestaJson= JsonConvert.SerializeObject(lista);
            return respuestaJson;
        }
    }
}
