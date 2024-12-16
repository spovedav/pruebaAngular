using Dapper;
using ECU.APPLICATION.Interfaces.Respositorio;
using ECU.DOMAIN.DTOs.Persona;
using ECU.DOMAIN.Entity.Precedure;
using System.Data;

namespace ECU.SQL.SERVER.Repositorio
{
    public class PersonaRepositorio : IPersonaRepositorio
    {
        private readonly IDbConnection connection;

        public PersonaRepositorio(IDbConnection connection) { this.connection = connection; }

        public SEL_Result Create(PersonaDto request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Opcion", "Insert", DbType.String);
            parameters.Add("@IdPersona", request.IdPersona, DbType.Int32);
            parameters.Add("@Nombres", request.Nombres, DbType.String);
            parameters.Add("@Apellidos", request.Apellidos, DbType.String);
            parameters.Add("@NumeroIdentificacion", request.NumeroIdentificacion, DbType.String);
            parameters.Add("@Email", request.Email, DbType.String);
            parameters.Add("@TipoIdentificacion", request.TipoIdentificacion, DbType.Byte);
            parameters.Add("@Ip", request.Ip, DbType.String);

            parameters.Add("@Error", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            parameters.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            connection.Execute("sp_Persona_CRUD", parameters, commandType: CommandType.StoredProcedure);

            bool error = parameters.Get<bool>("@Error");
            string mensaje = parameters.Get<string>("@Mensaje");

            return new SEL_Result(error, mensaje);
        }

        public SEL_Result Delete(int IdPersona)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Opcion", "Insert", DbType.String);
            parameters.Add("@IdPersona", IdPersona, DbType.Int32);

            parameters.Add("@Error", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            parameters.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            connection.Execute("sp_Persona_CRUD", parameters, commandType: CommandType.StoredProcedure);

            bool error = parameters.Get<bool>("@Error");
            string mensaje = parameters.Get<string>("@Mensaje");

            return new SEL_Result(error, mensaje);
        }

        public async Task<SEL_PersonaResult> Get(int IdPersona)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@IdPersona", IdPersona, DbType.Int32, ParameterDirection.Input);

            var resut = await connection.QueryAsync<SEL_PersonaResult>("SEL_PersonaGetXId");

            var firt = resut.ToList().FirstOrDefault();

            return firt;
        }

        public async Task<List<SEL_PersonaResult>> GetAll()
        {
            var resut = await connection.QueryAsync<SEL_PersonaResult>("SEL_PersonasGetAll");

            return resut.ToList();
        }

        public SEL_Result Update(PersonaDto request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Opcion", "Update", DbType.String);
            parameters.Add("@IdPersona", request.IdPersona, DbType.Int32);
            parameters.Add("@Nombres", request.Nombres, DbType.String);
            parameters.Add("@Apellidos", request.Apellidos, DbType.String);
            parameters.Add("@NumeroIdentificacion", request.NumeroIdentificacion, DbType.String);
            parameters.Add("@Email", request.Email, DbType.String);
            parameters.Add("@TipoIdentificacion", request.TipoIdentificacion, DbType.Byte);
            parameters.Add("@Ip", request.Ip, DbType.String);

            parameters.Add("@Error", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            parameters.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            connection.Execute("sp_Persona_CRUD", parameters, commandType: CommandType.StoredProcedure);

            bool error = parameters.Get<bool>("@Error");
            string mensaje = parameters.Get<string>("@Mensaje");

            return new SEL_Result(error, mensaje);
        }
    }
}
