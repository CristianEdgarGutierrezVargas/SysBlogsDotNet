using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysBlogs_Autor.Dominio
{
    public class Autor
    {
        public long? ID { get; set; }
        [Required]
        public string? Nombres { get; set; }
        [Required]
        public string? ApPaterno { get; set; }
        [Required]
        public string? ApMaterno { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        public string? PaisResidencia { get; set; }
        [Required]
        public string? CorreoElectronico { get; set; }
    }
}
