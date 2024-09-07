using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysBlogs_Comentario.Dominio
{
    public class Comentario
    {
        public long? ID { get; set; }
        [Required]
        public long? IdBlog { get; set; }
        [Required]
        public string? ComentarioTexto { get; set; }
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public string? Correo { get; set; }
        [Required]
        public string? PaisResidencia { get; set; }
        [Required]
        public int? Puntaje { get; set; }
    }
}
