using Dapper;
using ECU.APPLICATION.Interfaces.Respositorio;
using ECU.DOMAIN.DTOs.Usuario;
using ECU.DOMAIN.Entity.Precedure;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ECU.SQL.SERVER.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly IDbConnection connection;

        public UsuarioRepositorio(IDbConnection connection)
        {
            this.connection = connection;
        }

        public SEL_Result Create(UsuarioDto request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Opcion", "Insert", DbType.String);
            parameters.Add("@IdUsuario", request.IdUsuario, DbType.Int32);
            parameters.Add("@Identificador", request.Identificador, DbType.String);
            parameters.Add("@Usuario", request.Usuario, DbType.String);
            parameters.Add("@Rol", request.Rol, DbType.Byte);
            parameters.Add("@Ip", request.Ip, DbType.String);
            
            parameters.Add("@Error", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            parameters.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            connection.Execute("sp_Usuario_CRUD", parameters, commandType: CommandType.StoredProcedure);

            bool error = parameters.Get<bool>("@Error");
            string mensaje = parameters.Get<string>("@Mensaje");

            return new SEL_Result(error, mensaje);
        }

        public SEL_Result Delete(int IdUsuario)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Opcion", "Delete", DbType.String);
            parameters.Add("@IdUsuario", IdUsuario, DbType.Int32);

            parameters.Add("@Error", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            parameters.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            connection.Execute("sp_Usuario_CRUD", parameters, commandType: CommandType.StoredProcedure);

            bool error = parameters.Get<bool>("@Error");
            string mensaje = parameters.Get<string>("@Mensaje");

            return new SEL_Result(error, mensaje);
        }

        public async Task<SEL_UsuarioResult> Get(int IdUsuario)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@IdUsuario", IdUsuario, DbType.Int32, ParameterDirection.Input);
            
            var resut = await connection.QueryAsync<SEL_UsuarioResult>("SEL_UsuarioGetXId");

            var firt = resut.ToList().FirstOrDefault();

            return firt;
        }

        public async Task<List<SEL_UsuarioResult>> GetAll()
        {
            var resut = await connection.QueryAsync<SEL_UsuarioResult>("SEL_UsuarioGetAll");

            return resut.ToList();
        }

        public async Task<SEL_UsuarioAutenticationResult> GetUsuario(string UserName, string Passs)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserName", UserName, DbType.String);
            parameters.Add("@Pass", Passs, DbType.String);

            var result = await connection.QuerySingleOrDefaultAsync<SEL_UsuarioAutenticationResult>(
                      "AuthenticateUser",
                      parameters,
                      commandType: CommandType.StoredProcedure
                      );

            return result ?? new SEL_UsuarioAutenticationResult();
        }

        public SEL_Result Update(UsuarioDto request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Opcion", "Update", DbType.String);
            parameters.Add("@IdUsuario", request.IdUsuario, DbType.Int32);
            parameters.Add("@Identificador", request.Identificador, DbType.String);
            parameters.Add("@Usuario", request.Usuario, DbType.String);
            parameters.Add("@Rol", request.Rol, DbType.Byte);
            parameters.Add("@Ip", request.Ip, DbType.String);
            
            parameters.Add("@Error", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            parameters.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            connection.Execute("sp_Usuario_CRUD", parameters, commandType: CommandType.StoredProcedure);

            bool error = parameters.Get<bool>("@Error");
            string mensaje = parameters.Get<string>("@Mensaje");

            return new SEL_Result(error, mensaje);
        }

       
    }
}
