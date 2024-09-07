using SysBlogs_Blogs.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysBlogs_Blogs.Dominio
{
    public interface IAccionesBlog
    {
        string CrearBlog(Blog _blog);
        string ObtenerBlog(long ID);
        void ActualizarBlog(Blog _blog);
        string ObtenerListadoBlog();
    }
}
