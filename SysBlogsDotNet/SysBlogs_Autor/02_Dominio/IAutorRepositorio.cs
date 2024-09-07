using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysBlogs_Autor.Dominio
{
    internal interface IAutorRepositorio
    {
        Autor CrearAutor(Autor _autor);
        Autor ObtenerAutor(long ID);
        void ActualizarAutor(Autor _autor);
        List<Autor> ObtenerListadoAutores();
    }
}
