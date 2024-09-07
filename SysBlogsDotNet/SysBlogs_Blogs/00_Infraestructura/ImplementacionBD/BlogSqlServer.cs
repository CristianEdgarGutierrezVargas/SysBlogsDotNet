using SysBlogs_Blogs.Dominio;
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

namespace SysBlogs_Blogs.Infraestructura.ImplementacionBD
{
    internal class BlogSqlServer : IBlogRepositorio
    {
        private readonly IConfiguration _configuration;
        //private readonly ILogger _logger;
        public BlogSqlServer(IConfiguration configuration) 
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
        public void ActualizarBlog(Blog _blog)
        {
            string query = @"INSERT INTO [dbo].[BLOG_HISTORICO]
                               ( [ID]
                                ,[ID_AUTOR]
                                ,[NOMBRE]
                                ,[TEMA]
                                ,[CONTENIDO]
                                ,[PERIODICIDAD]
                                ,[HABILITA_COMENTARIO]
                                ,[USUARIO_INSERT]
                                ,[USUARIO_MODIF]
                                ,[FECHA_INSERT]
                                ,[FECHA_MODIF])
                         SELECT  [ID]
                                ,[ID_AUTOR]
                                ,[NOMBRE]
                                ,[TEMA]
                                ,[CONTENIDO]
                                ,[PERIODICIDAD]
                                ,[HABILITA_COMENTARIO]
                                ,'CODIGO'
                                ,NULL
                                ,GETDATE()
                                ,NULL
                              FROM [BISAEXAM].[dbo].[BLOG]
                              WHERE ID=" + _blog.ID.ToString();
            DataSet resultado = Ejecutar(query);

            string query2 = @"UPDATE [dbo].[BLOG]
                             SET [ID_AUTOR]=" + _blog.IdAutor +
                                ",[NOMBRE]='" + _blog.Nombre + "'"+
                                ",[TEMA]='" + _blog.Tema + "'" +
                                ",[CONTENIDO]='" + _blog.Contenido + "'" +
                                ",[PERIODICIDAD]='" + _blog.Periodicidad + "'" +
                                ",[HABILITA_COMENTARIO]=" + Convert.ToInt32(_blog.HabilitaComentario).ToString() +
                                ",[USUARIO_MODIF]='CODIGO'" +
                                ",[FECHA_MODIF]=GETDATE()" +
                             " WHERE ID=" + _blog.ID.ToString();
            DataSet resultado2 = Ejecutar(query2);
        }

        public Blog CrearBlog(Blog _blog)
        {
            string query = @"INSERT INTO [dbo].[BLOG]
                               ([ID_AUTOR]
                                ,[NOMBRE]
                                ,[TEMA]
                                ,[CONTENIDO]
                                ,[PERIODICIDAD]
                                ,[HABILITA_COMENTARIO]
                                ,[USUARIO_INSERT]
                                ,[USUARIO_MODIF]
                                ,[FECHA_INSERT]
                                ,[FECHA_MODIF])
                         VALUES
                               (" + _blog.IdAutor.ToString()+
                               ",'" + _blog.Nombre + "'" +
                               ",'" + _blog.Tema+"'"+
                               ",'" + _blog.Contenido + "'" +
                               ",'" + _blog.Periodicidad + "'" +
                               "," + Convert.ToInt32(_blog.HabilitaComentario).ToString() + "" +
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
            Blog blog = new Blog();
            blog.ID = Convert.ToInt64(item["ID"]);
            blog.IdAutor = _blog.IdAutor;
            blog.Nombre = _blog.Nombre;
            blog.Tema = _blog.Tema;
            blog.Contenido = _blog.Contenido;
            blog.Periodicidad = _blog.Periodicidad;
            blog.HabilitaComentario = _blog.HabilitaComentario;
            return blog;
        }

        public Blog ObtenerBlog(long ID)
        {
            string query = @"SELECT    [ID]
                                      ,[ID_AUTOR]
                                      ,[NOMBRE]
                                      ,[TEMA]
                                      ,[CONTENIDO]
                                      ,[PERIODICIDAD]
                                      ,[HABILITA_COMENTARIO]
                                      ,[USUARIO_INSERT]
                                      ,[USUARIO_MODIF]
                                      ,[FECHA_INSERT]
                                      ,[FECHA_MODIF]
                              FROM [BISAEXAM].[dbo].[BLOG]
                              WHERE ID=" + ID.ToString();
            DataSet resultado = Ejecutar(query);
            var response = resultado.Tables["Results"];
            if (response.Rows.Count == 0) 
            {
                return null;
            }
            DataRow item = response.Rows[0];
            Blog blog = new Blog();
            blog.ID = Convert.ToInt64(item["ID"]);
            blog.IdAutor = Convert.ToInt64(item["ID_AUTOR"]);
            blog.Nombre = item["NOMBRE"].ToString();
            blog.Tema = item["TEMA"].ToString();
            blog.Contenido = item["CONTENIDO"].ToString();
            blog.Periodicidad = item["PERIODICIDAD"].ToString();
            blog.HabilitaComentario =Convert.ToBoolean( item["HABILITA_COMENTARIO"]);
            
            return blog;
        }
        public List<Blog> ObtenerListadoBlogs()
        {
            string query = @"SELECT [ID]
                                      ,[ID_AUTOR]
                                      ,[NOMBRE]
                                      ,[TEMA]
                                      ,[CONTENIDO]
                                      ,[PERIODICIDAD]
                                      ,[HABILITA_COMENTARIO]
                                      ,[USUARIO_INSERT]
                                      ,[USUARIO_MODIF]
                                      ,[FECHA_INSERT]
                                      ,[FECHA_MODIF]
                              FROM [BISAEXAM].[dbo].[BLOG]";
            DataSet resultado = Ejecutar(query);
            var response = resultado.Tables["Results"];

            List<Blog> respuesta = new List<Blog>();
            foreach (DataRow item in response.Rows)
            {
                Blog blog = new Blog();
                blog.ID = Convert.ToInt64(item["ID"]);
                blog.IdAutor = Convert.ToInt64(item["ID_AUTOR"]);
                blog.Nombre = item["NOMBRE"].ToString();
                blog.Tema = item["TEMA"].ToString();
                blog.Contenido = item["CONTENIDO"].ToString();
                blog.Periodicidad = item["PERIODICIDAD"].ToString();
                blog.HabilitaComentario = Convert.ToBoolean(item["HABILITA_COMENTARIO"]);
                respuesta.Add(blog);
            }

            return respuesta;
        }
    }
}
