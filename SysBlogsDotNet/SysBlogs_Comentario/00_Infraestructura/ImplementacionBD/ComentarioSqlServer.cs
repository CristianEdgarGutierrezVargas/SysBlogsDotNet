using SysBlogs_Comentario.Dominio;
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

namespace SysBlogs_Comentario.Infraestructura.ImplementacionBD
{
    internal class ComentarioSqlServer : IComentarioRepositorio
    {
        private readonly IConfiguration _configuration;
        //private readonly ILogger _logger;
        public ComentarioSqlServer(IConfiguration configuration) 
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
        //public void ActualizarComentario(Comentario _blog)
        //{
        //    string query = @"INSERT INTO [dbo].[BLOG_HISTORICO]
        //                       ( [ID]
        //                        ,[ID_AUTOR]
        //                        ,[NOMBRE]
        //                        ,[TEMA]
        //                        ,[CONTENIDO]
        //                        ,[PERIODICIDAD]
        //                        ,[HABILITA_COMENTARIO]
        //                        ,[USUARIO_INSERT]
        //                        ,[USUARIO_MODIF]
        //                        ,[FECHA_INSERT]
        //                        ,[FECHA_MODIF])
        //                 SELECT  [ID]
        //                        ,[ID_AUTOR]
        //                        ,[NOMBRE]
        //                        ,[TEMA]
        //                        ,[CONTENIDO]
        //                        ,[PERIODICIDAD]
        //                        ,[HABILITA_COMENTARIO]
        //                        ,'CODIGO'
        //                        ,NULL
        //                        ,GETDATE()
        //                        ,NULL
        //                      FROM [dbo].[BLOG]
        //                      WHERE ID=" + _blog.ID.ToString();
        //    DataSet resultado = Ejecutar(query);

        //    string query2 = @"UPDATE [dbo].[BLOG]
        //                     SET [ID_AUTOR]=" + _blog.IdAutor +
        //                        ",[NOMBRE]='" + _blog.Nombre + "'"+
        //                        ",[TEMA]='" + _blog.Tema + "'" +
        //                        ",[CONTENIDO]='" + _blog.Contenido + "'" +
        //                        ",[PERIODICIDAD]='" + _blog.Periodicidad + "'" +
        //                        ",[HABILITA_COMENTARIO]=" + Convert.ToInt32(_blog.HabilitaComentario).ToString() +
        //                        ",[USUARIO_MODIF]='CODIGO'" +
        //                        ",[FECHA_MODIF]=GETDATE()" +
        //                     " WHERE ID=" + _blog.ID.ToString();
        //    DataSet resultado2 = Ejecutar(query2);
        //}

        public Comentario CrearComentario(Comentario _comentario)
        {
            string query = @"INSERT INTO [dbo].[COMENTARIO]
                                  ([ID_BLOG]
                                  ,[COMENTARIO]
                                  ,[NOMBRE]
                                  ,[CORREO]
                                  ,[PAIS_RESIDENCIA]
                                  ,[PUNTAJE]
                                  ,[USUARIO_INSERT]
                                  ,[USUARIO_MODIF]
                                  ,[FECHA_INSERT]
                                  ,[FECHA_MODIF])
                         VALUES
                               (" + _comentario.IdBlog.ToString()+
                               ",'" + _comentario.ComentarioTexto + "'" +
                               ",'" + _comentario.Nombre+"'"+
                               ",'" + _comentario.Correo + "'" +
                               ",'" + _comentario.PaisResidencia + "'" +
                               "," + _comentario.Puntaje + "" +
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
            Comentario comentario = new Comentario();
            comentario.ID = Convert.ToInt64(item["ID"]);
            comentario.IdBlog = _comentario.IdBlog;
            comentario.ComentarioTexto = _comentario.ComentarioTexto;
            comentario.Nombre = _comentario.Nombre;
            comentario.Correo = _comentario.Correo;
            comentario.PaisResidencia = _comentario.PaisResidencia;
            comentario.Puntaje = _comentario.Puntaje;
            return comentario;
        }

        public Comentario ObtenerComentario(long ID)
        {
            string query = @"SELECT    [ID]
                                      ,[ID_BLOG]
                                      ,[COMENTARIO]
                                      ,[NOMBRE]
                                      ,[CORREO]
                                      ,[PAIS_RESIDENCIA]
                                      ,[PUNTAJE]
                                      ,[USUARIO_INSERT]
                                      ,[USUARIO_MODIF]
                                      ,[FECHA_INSERT]
                                      ,[FECHA_MODIF]
                              FROM [dbo].[COMENTARIO]
                              WHERE ID=" + ID.ToString();
            DataSet resultado = Ejecutar(query);
            var response = resultado.Tables["Results"];
            if (response.Rows.Count == 0) 
            {
                return null;
            }
            DataRow item = response.Rows[0];
            Comentario comentario = new Comentario();
            comentario.ID = Convert.ToInt64(item["ID"]);
            comentario.IdBlog = Convert.ToInt64(item["ID_BLOG"]);
            comentario.ComentarioTexto = item["COMENTARIO"].ToString();
            comentario.Nombre = item["NOMBRE"].ToString();
            comentario.Correo = item["CORREO"].ToString();
            comentario.PaisResidencia = item["PAIS_RESIDENCIA"].ToString();
            comentario.Puntaje = Convert.ToInt32(item["PUNTAJE"]);

            return comentario;
        }
        public List<Comentario> ObtenerListadoComentario()
        {
            string query = @"SELECT [ID]
                              ,[ID_BLOG]
                              ,[COMENTARIO]
                              ,[NOMBRE]
                              ,[CORREO]
                              ,[PAIS_RESIDENCIA]
                              ,[PUNTAJE]
                              ,[USUARIO_INSERT]
                              ,[USUARIO_MODIF]
                              ,[FECHA_INSERT]
                              ,[FECHA_MODIF]
                          FROM [dbo].[COMENTARIO]";
            DataSet resultado = Ejecutar(query);
            var response = resultado.Tables["Results"];

            List<Comentario> respuesta = new List<Comentario>();
            foreach (DataRow item in response.Rows)
            {
                Comentario comentario = new Comentario();
                comentario.ID = Convert.ToInt64(item["ID"]);
                comentario.IdBlog = Convert.ToInt64(item["ID_BLOG"]);
                comentario.ComentarioTexto = item["COMENTARIO"].ToString();
                comentario.Nombre = item["NOMBRE"].ToString();
                comentario.Correo = item["CORREO"].ToString();
                comentario.PaisResidencia = item["PAIS_RESIDENCIA"].ToString();
                comentario.Puntaje = Convert.ToInt32(item["PUNTAJE"]);
                respuesta.Add(comentario);
            }

            return respuesta;
        }
    }
}
