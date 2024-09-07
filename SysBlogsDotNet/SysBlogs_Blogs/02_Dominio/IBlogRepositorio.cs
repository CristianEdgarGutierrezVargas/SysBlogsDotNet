using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysBlogs_Blogs.Dominio
{
    internal interface IBlogRepositorio
    {
        Blog CrearBlog(Blog _blog);
        Blog ObtenerBlog(long ID);
        void ActualizarBlog(Blog _blog);
        List<Blog> ObtenerListadoBlogs();
    }
}
