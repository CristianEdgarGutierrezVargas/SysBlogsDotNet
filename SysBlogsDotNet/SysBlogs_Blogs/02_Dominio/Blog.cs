using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysBlogs_Blogs.Dominio
{
    public class Blog
    {
        public long? ID { get; set; }
        [Required]
        public long? IdAutor { get; set; }
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public string? Tema { get; set; }
        [Required]
        public string? Contenido { get; set; }
        [Required]
        public string? Periodicidad { get; set; }
        [Required]
        public bool? HabilitaComentario { get; set; }
    }
}
