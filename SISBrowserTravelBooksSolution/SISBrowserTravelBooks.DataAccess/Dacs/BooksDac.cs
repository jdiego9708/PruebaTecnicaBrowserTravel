using System;
using System.Data.SqlClient;
using System.Data;
using SISBrowserTravelBooks.DataAccess.Interfaces;
using SISBrowserTravelBooks.Entities.Models;

namespace SISBrowserTravelBooks.DataAccess.Dacs
{
    public class BooksDac : IBooksDac
    {
        #region CONSTRUCTOR AND DEPENDENCY INJECTION
        private readonly IConnectionDac Connection;
        public BooksDac(IConnectionDac Connection)
        {
            this.Error_message = string.Empty;

            this.Connection = Connection;
        }
        #endregion

        #region SQL ERROR MESSAGE
        private void SqlCon_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            string mensaje_error = e.Message;
            if (e.Errors != null)
            {
                if (e.Errors.Count > 0)
                {
                    mensaje_error += string.Join("|", e.Errors);
                }
            }
            this.Error_message = mensaje_error;
        }
        #endregion

        #region PROPERTIES
        public string Error_message { get; set; }
        #endregion

        #region METHOD INSERT BOOKS
        public string InsertBook(Books book)
        {
            //Inicializamos la respuesta que vamos a devolver
            string rpta = "OK";
            SqlConnection SqlCon = new();
            try
            {
                //Asignamos un evento SqlInfoMessage para obtener errores con severidad < 10 y > 11 desde SQL
                SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
                SqlCon.FireInfoMessageEventOnUserErrors = true;
                //Asignamos la cadena de conexión desde un método estático que lee el archivo de configuracion
                SqlCon.ConnectionString = Connection.Cn();
                //Abrimos la conexión.
                SqlCon.Open();
                //Creamos un comando para ejecutar un procedimiento almacenado
                SqlCommand SqlCmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Books_i",
                    CommandType = CommandType.StoredProcedure
                };

                #region PARÁMETROS
                //Creamos cada parámetro y lo agregamos a la lista de parámetros del comando
                //El primer comando es el id del usuario que es parámetro de salida
                SqlParameter Id_book = new()
                {
                    ParameterName = "@Id_book",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output,
                };
                SqlCmd.Parameters.Add(Id_book);

                SqlParameter Id_editorial = new()
                {
                    ParameterName = "@Id_editorial",
                    SqlDbType = SqlDbType.Int,
                    Value = book.Id_editorial,
                };
                SqlCmd.Parameters.Add(Id_editorial);

                //Los parámetros varchar se les asigna una propiedad extra y es el Size
                SqlParameter Tittle_book = new()
                {
                    ParameterName = "@Tittle_book",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = book.Tittle_book,
                };
                SqlCmd.Parameters.Add(Tittle_book);

                SqlParameter Synopsis_book = new()
                {
                    ParameterName = "@Synopsis_book",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 500,
                    Value = book.Synopsis_book,
                };
                SqlCmd.Parameters.Add(Synopsis_book);
                #endregion

                //Ejecutamos nuestro comando cuando agreguemos todos los parámetros requeridos
                rpta = SqlCmd.ExecuteNonQuery() > 0 ? "OK" : "ERROR";

                //Comprobamos la variable de respuesta Mensaje_error que guarda el mensaje específico
                //De cualquier error generado en SQL procedimiento almacenado
                if (!rpta.Equals("OK"))
                    if (!string.IsNullOrEmpty(this.Error_message))
                    {
                        rpta = this.Error_message;
                        throw new Exception(rpta);
                    }
                //Obtenemos el id y lo asignamos a la propiedad existente para usarlo después
                book.Id_book = Convert.ToInt32(SqlCmd.Parameters["@Id_book"].Value);
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return rpta;
        }
        #endregion

        #region MÉTODO UPDATE BOOKS
        public string UpdateBook(Books book)
        {
            //Inicializamos la respuesta que vamos a devolver
            string rpta = "OK";
            SqlConnection SqlCon = new();
            try
            {
                //Asignamos un evento SqlInfoMessage para obtener errores con severidad < 10 y > 11 desde SQL
                SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
                SqlCon.FireInfoMessageEventOnUserErrors = true;
                //Asignamos la cadena de conexión desde un método estático que lee el archivo de configuracion
                SqlCon.ConnectionString = Connection.Cn();
                //Abrimos la conexión.
                SqlCon.Open();
                //Creamos un comando para ejecutar un procedimiento almacenado
                SqlCommand SqlCmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Books_u",
                    CommandType = CommandType.StoredProcedure
                };

                #region PARÁMETROS
                //Creamos cada parámetro y lo agregamos a la lista de parámetros del comando
                //El primer comando es el id del usuario que es parámetro de salida
                SqlParameter Id_book = new()
                {
                    ParameterName = "@Id_book",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output,
                };
                SqlCmd.Parameters.Add(Id_book);

                SqlParameter Id_editorial = new()
                {
                    ParameterName = "@Id_editorial",
                    SqlDbType = SqlDbType.Int,
                    Value = book.Id_editorial,
                };
                SqlCmd.Parameters.Add(Id_editorial);

                //Los parámetros varchar se les asigna una propiedad extra y es el Size
                SqlParameter Tittle_book = new()
                {
                    ParameterName = "@Tittle_book",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = book.Tittle_book,
                };
                SqlCmd.Parameters.Add(Tittle_book);

                SqlParameter Synopsis_book = new()
                {
                    ParameterName = "@Synopsis_book",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 500,
                    Value = book.Synopsis_book,
                };
                SqlCmd.Parameters.Add(Synopsis_book);
                #endregion

                //Ejecutamos nuestro comando cuando agreguemos todos los parámetros requeridos
                rpta = SqlCmd.ExecuteNonQuery() > 0 ? "OK" : "ERROR";

                //Comprobamos la variable de respuesta Mensaje_error que guarda el mensaje específico
                //De cualquier error generado en SQL procedimiento almacenado
                if (!rpta.Equals("OK"))
                    if (!string.IsNullOrEmpty(this.Error_message))
                        rpta = this.Error_message;
                //Obtenemos el id y lo asignamos a la propiedad existente para usarlo después
                book.Id_book = Convert.ToInt32(SqlCmd.Parameters["@Id_book"].Value);
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return rpta;
        }
        #endregion

        #region SEARCH BOOKS
        public string SearchBooks(string type_search, string value_search,
            out DataTable dt)
        {
            //Inicializamos la respuesta que vamos a devolver
            dt = new();
            string rpta = "OK";
            SqlConnection SqlCon = new();
            try
            {
                //Asignamos un evento SqlInfoMessage para obtener errores con severidad < 10 desde SQL
                SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
                SqlCon.FireInfoMessageEventOnUserErrors = true;
                //Asignamos la cadena de conexión desde un método estático que lee el archivo de configuracion
                SqlCon.ConnectionString = Connection.Cn();
                //Abrimos la conexión.
                SqlCon.Open();
                //Creamos un comando para ejecutar un procedimiento almacenado
                SqlCommand SqlCmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Books_g",
                    CommandType = CommandType.StoredProcedure
                };
                //Creamos cada parámetro y lo agregamos a la lista de parámetros del comando
                //El primer comando es el id del usuario que es parámetro de salida
                SqlParameter Type_search = new()
                {
                    ParameterName = "@Type_search",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = type_search
                };
                SqlCmd.Parameters.Add(Type_search);
                //Los parámetros varchar se les asigna una propiedad extra y es el Size
                SqlParameter Value_search = new()
                {
                    ParameterName = "@Value_search",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = value_search,
                };
                SqlCmd.Parameters.Add(Value_search);

                //Ejecutamos nuestro comando cuando agreguemos todos los parámetros requeridos
                SqlDataAdapter SqlData = new(SqlCmd);
                SqlData.Fill(dt);

                //Comprobamos la variable de respuesta Mensaje_error que guarda el mensaje específico
                //De cualquier error generado en SQL procedimiento almacenado
                if (dt == null)
                {
                    if (!string.IsNullOrEmpty(this.Error_message))
                        rpta = this.Error_message;
                }
                else
                {
                    if (dt.Rows.Count < 1)
                        dt = null;
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                dt = null;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return rpta;
        }
        #endregion
    }
}
