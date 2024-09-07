using SysBlogs_Autor.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysBlogs_Autor.Dominio
{
    public interface IAccionesAutor
    {
        string CrearAutor(Autor _autor);
        string ObtenerAutor(long ID);
        void ActualizarAutor(Autor _autor);
        string ObtenerListadoAutores();
    }
}
