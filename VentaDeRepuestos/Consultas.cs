using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace VentaDeRepuestos
{
    class Consultas
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection">objeto de la clase SqlConnection que devuelve la conexion con la bd</param>
        /// <param name="commandText"></param>
        /// <param name="commandType">hay tres tipos de comando StoreProcedure,Text,TableDirect tableDirect es solo para OLE DB </param>
        /// <param name="parameters">parametros de sql</param>
        /// <returns>retorna la cantidad de filas afectadas</returns>
        public static async Task<Int32> ExecuteNonQuery(SqlConnection connection, string commandText
            , CommandType commandType, params SqlParameter[] parameters)
        {
            using (connection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(commandText, connection))
                {
                    sqlCommand.CommandType = commandType;
                    sqlCommand.Parameters.AddRange(parameters);
                    connection.Open();

                    return await sqlCommand.ExecuteNonQueryAsync();
                }
            }
        }

        /// <summary>
        /// metodo para hacer login
        /// </summary>
        /// <param name="email">email del usuario</param>
        /// <param name="password">password del usuario</param>
        /// <returns></returns>
        public static async Task<SqlDataReader> loginAsync(string email, string password)
        {
            var con = await Conexion.conectarAsync();

            using (SqlCommand cmd = new SqlCommand(null, con))
            {
                cmd.CommandText = $"Select u.EMAIL ,u.PASS , p.NOMBRE from USUARIOS as u inner join PERFIL as p on u.ID_PERFIL=p.ID_PERFIL where u.EMAIL='{email}' and u.PASS='{password}' ";

                return await cmd.ExecuteReaderAsync();
            }
        }

        /// <summary>
        /// ejecuta un excalar para devolver un solo objeto
        /// </summary>
        /// <param name="comandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns>Objet</returns>
        public static async Task<Object> executeScalar(string comandText, CommandType commandType, params SqlParameter[] parameters)
        {

            using (var con = await Conexion.conectarAsync())
            {
                using (var comando = new SqlCommand(comandText, con))
                {
                    comando.CommandType = commandType;
                    comando.Parameters.AddRange(parameters);
                    con.Open();
                    return await comando.ExecuteScalarAsync();
                }
            }
        }

        /// <summary>
        /// ejecuta el comando con query y retorna un reader
        /// </summary>
        /// <param name="commandText">query</param>
        /// <param name="commandType">tipo de comando</param>
        /// <param name="parameters">parametros de la consulta</param>
        /// <returns></returns>
        public static async Task<SqlDataReader> ExecuteReader(string commandText, CommandType commandType, params SqlParameter[]parameters)
        {
            using (var con = await Conexion.conectarAsync())
            {
                using (var cmd = new SqlCommand(commandText,con))
                {
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(parameters);
                    await con.OpenAsync();

                    return await cmd.ExecuteReaderAsync();
                }
            }
        }
    }
}
