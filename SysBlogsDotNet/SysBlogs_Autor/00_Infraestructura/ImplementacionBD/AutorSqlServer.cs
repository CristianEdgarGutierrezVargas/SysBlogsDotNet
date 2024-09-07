using SysBlogs_Autor.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;

namespace SysBlogs_Autor.Infraestructura.ImplementacionBD
{
    internal class AutorSqlServer : IAutorRepositorio
    {
        private readonly IConfiguration _configuration;
        //private readonly ILogger _logger;
        public AutorSqlServer(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        private DataSet Ejecutar(string query)
        {
            try
            {
                string FirmadorConnectionString = _configuration.GetConnectionString("BISAEXAM");
                using (SqlConnection connection = new SqlConnection(FirmadorConnectionString))
                {
                    connection.Open();
                    String sql = query;
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataAdapter adp = new SqlDataAdapter();
                    DataSet dset = new DataSet();
                    {
                        adp.SelectCommand = command;
                        adp.Fill(dset, "Results");
                    }
                    return dset;
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public void ActualizarAutor(Autor _autor)
        {
            string query = @"SELECT 'Probando la flujo completo'";
            DataSet respuesta = Ejecutar(query);
            var response = respuesta.Tables["Results"].Rows[0][0].ToString();
            //return response;
        }

        public Autor CrearAutor(Autor _autor)
        {
            string query = @"INSERT INTO [dbo].[AUTOR]
                               ([NOMBRES]
                               ,[AP_PATERNO]
                               ,[AP_MATERNO]
                               ,[FECHA_NACIMIENTO]
                               ,[PAIS_RESIDENCIA]
                               ,[CORREO_ELECTRONICO]
                               ,[USUARIO_INSERT]
                               ,[USUARIO_MODIF]
                               ,[FECHA_INSERT]
                               ,[FECHA_MODIF])
                         VALUES
                               ('"+ _autor.Nombres+"'"+
                               ",'"+ _autor.ApPaterno+"'"+
                               ",'" + _autor.ApMaterno + "'" +
                               ",'" + _autor.FechaNacimiento.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" +//2024-06-04 00:19:33.000
                               ",'" + _autor.PaisResidencia + "'" +
                               ",'" + _autor.CorreoElectronico + "'" +
                               ",'CODIGO'" +
                               ",NULL" +
                               ",GETDATE()" +
                               ",NULL) SELECT SCOPE_IDENTITY() AS ID";
            DataSet resultado = Ejecutar(query);
            var response = resultado.Tables["Results"];
            if (response.Rows.Count == 0)
            {
                return null;
            }
            DataRow item = response.Rows[0];
            Autor autor = new Autor();
            autor.ID = Convert.ToInt64(item["ID"]);
            autor.Nombres = _autor.Nombres;
            autor.ApPaterno = _autor.ApPaterno;
            autor.ApMaterno = _autor.ApMaterno;
            autor.FechaNacimiento = _autor.FechaNacimiento;
            autor.PaisResidencia = _autor.PaisResidencia;
            autor.CorreoElectronico = _autor.CorreoElectronico;
            return autor;
        }

        public Autor ObtenerAutor(long _id)
        {
            string query = @"SELECT [ID]
                                    ,[NOMBRES]
                                    ,[AP_PATERNO]
                                    ,[AP_MATERNO]
                                    ,[FECHA_NACIMIENTO]
                                    ,[PAIS_RESIDENCIA]
                                    ,[CORREO_ELECTRONICO]
                                    ,[USUARIO_INSERT]
                                    ,[USUARIO_MODIF]
                                    ,[FECHA_INSERT]
                                    ,[FECHA_MODIF]
                              FROM [BISAEXAM].[dbo].[AUTOR]
                              WHERE ID="+ _id.ToString();
            DataSet resultado = Ejecutar(query);
            var response = resultado.Tables["Results"];
            if (response.Rows.Count == 0) 
            {
                return null;
            }
            DataRow item = response.Rows[0];
            Autor autor = new Autor();
            autor.ID = Convert.ToInt64(item["ID"]);
            autor.Nombres = item["NOMBRES"].ToString();
            autor.ApPaterno = item["AP_PATERNO"].ToString();
            autor.ApMaterno = item["AP_MATERNO"].ToString();
            autor.FechaNacimiento = Convert.ToDateTime(item["FECHA_NACIMIENTO"]);
            autor.PaisResidencia = item["PAIS_RESIDENCIA"].ToString();
            autor.CorreoElectronico = item["CORREO_ELECTRONICO"].ToString();
            
            return autor;
        }
        public List<Autor> ObtenerListadoAutores()
        {
            string query = @"SELECT [ID]
                                    ,[NOMBRES]
                                    ,[AP_PATERNO]
                                    ,[AP_MATERNO]
                                    ,[FECHA_NACIMIENTO]
                                    ,[PAIS_RESIDENCIA]
                                    ,[CORREO_ELECTRONICO]
                                    ,[USUARIO_INSERT]
                                    ,[USUARIO_MODIF]
                                    ,[FECHA_INSERT]
                                    ,[FECHA_MODIF]
                              FROM [BISAEXAM].[dbo].[AUTOR]";
            DataSet resultado = Ejecutar(query);
            var response = resultado.Tables["Results"];

            List<Autor> respuesta = new List<Autor>();
            foreach (DataRow item in response.Rows)
            {
                Autor autor = new Autor();
                autor.ID =Convert.ToInt64(item["ID"]);
                autor.Nombres = item["NOMBRES"].ToString();
                autor.ApPaterno = item["AP_PATERNO"].ToString();
                autor.ApMaterno = item["AP_MATERNO"].ToString();
                autor.FechaNacimiento =Convert.ToDateTime( item["FECHA_NACIMIENTO"]);
                autor.PaisResidencia = item["PAIS_RESIDENCIA"].ToString();
                autor.CorreoElectronico = item["CORREO_ELECTRONICO"].ToString();
                respuesta.Add(autor);
            }

            return respuesta;
        }
    }
}
