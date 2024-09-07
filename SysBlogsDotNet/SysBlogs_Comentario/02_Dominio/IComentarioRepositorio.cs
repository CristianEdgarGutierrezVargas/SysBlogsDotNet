using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysBlogs_Comentario.Dominio
{
    internal interface IComentarioRepositorio
    {
        Comentario CrearComentario(Comentario _comentario);
        Comentario ObtenerComentario(long ID);
        //void ActualizarComentario(Comentario _comentario);
        List<Comentario> ObtenerListadoComentario();
    }
}
