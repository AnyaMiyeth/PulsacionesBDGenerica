using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using Entity;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace DAL
{
    public static class DbParameterExtension
    {
        public static void AddParameter(this DbCommand command, string parameterName, object value)
        {
            DbParameter parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }

        public static void EnsureOracleBindByName(this DbCommand command)
        {
            //Oracle.DataAccess.Client only binds first parameter match unless BindByName=true
            //so we violate LiskovSP (in reflection to avoid dependency on ODP)
            #if !COREFX
            var bindByName = typeof(DbCommand).GetProperty("BindByName");
            #else
            var bindByName = typeof(DbCommand).GetTypeInfo().GetDeclaredProperty("BindByName");
            #endif           
            if (bindByName != null)
            {
                bindByName.SetValue(command, true, null);
            }
        }

    }
    public class PersonaRepository
    {
        private readonly DbConnection _connection;
        private readonly List<Persona> _personas = new List<Persona>();
        public PersonaRepository(ConnectionManager connection)
        {
            _connection = connection._conexion;
        }
        public int Guardar(Persona persona)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"Insert Into Persona (Identificacion,Nombre,Edad, Sexo, Pulsacion) 
                                      values (:Identificacion,:Nombre,:Edad,:Sexo,:Pulsacion)";
                
                command.EnsureOracleBindByName();

                /*DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@Nombre";
                parameter.Value = persona.Nombre;
                command.Parameters.Add(parameter);*/

                command.AddParameter(":Identificacion", persona.Identificacion);
                command.AddParameter(":Nombre",  persona.Nombre);
                command.AddParameter(":Edad", persona.Edad);
                command.AddParameter(":Sexo", persona.Sexo);
                command.AddParameter(":Pulsacion", persona.Pulsacion);
                var filas = command.ExecuteNonQuery();
                return filas;
             }
        }
      
        public int Eliminar(Persona persona)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "Delete from persona where Identificacion=@Identificacion";
                command.AddParameter("@Identificacion", persona.Identificacion);
                var filas = command.ExecuteNonQuery();
                return filas;
            }
        }

        public List<Persona> ConsultarTodos()
        {
            DbDataReader dataReader;
            List<Persona> personas = new List<Persona>();
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "Select * from persona ";
                dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Persona persona = DataReaderMapToPerson(dataReader);
                        personas.Add(persona);
                    }
                }
            }
            return personas;
        }
        public Persona BuscarPorIdentificacion(string identificacion)
        {
            DbDataReader dataReader;
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "select Identificacion,Nombre,Sexo,Edad,Pulsacion from persona where Identificacion=:Identificacion";
                command.AddParameter(":Identificacion", identificacion);
                dataReader = command.ExecuteReader();
                dataReader.Read();
                Persona persona = DataReaderMapToPerson(dataReader);
                return persona;
            }
        }

        public int Modificar(Persona persona)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "update persona set nombre=@Nombre, edad=@Edad, sexo=@Sexo, pulsacion=@Pulsacion where Identificacion=@Identificacion";
                command.AddParameter("@Identificacion", persona.Identificacion);
                command.AddParameter("@Nombre", persona.Nombre);
                command.AddParameter("@Sexo", persona.Sexo);
                command.AddParameter("@Edad", persona.Edad);
                command.AddParameter("@Pulsacion", persona.Pulsacion);
                var filas = command.ExecuteNonQuery();
                return filas;
            }
        }
        private Persona DataReaderMapToPerson(DbDataReader dataReader)
        {
            if (!dataReader.HasRows) return null;
            Persona persona = new Persona();
            persona.Identificacion = dataReader.GetString(0);
            persona.Nombre = dataReader.GetString(1);
            persona.Sexo = dataReader.GetString(2);
            persona.Edad = dataReader.GetInt32(3);
            persona.Pulsacion = dataReader.GetDecimal(4);
            return persona;

        }

  
        public int Totalizar()
        {
            
            return ConsultarTodos().Count();
        }
        public int TotalizarTipo(string tipo)
        {
           
            return ConsultarTodos().Where(p => p.Sexo.Equals(tipo)).Count();
        }
      
    }
}
