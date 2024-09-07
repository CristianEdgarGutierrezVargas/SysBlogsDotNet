using SysBlogs_Blogs.Dominio;
using SysBlogs_Comentario.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysBlogs_Blogs.Dominio
{
    public interface IAccionesComentario
    {
        string CrearComentario(Comentario _comentario);
        string ObtenerComentario(long ID);
        //void ActualizarComentario(Comentario _comentario);
        string ObtenerListadoComentario();
    }
}
