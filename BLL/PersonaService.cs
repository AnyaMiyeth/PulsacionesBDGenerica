using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;

namespace BLL
{
    public class PersonaService
    {
        private readonly ConnectionManager conexion;
        private readonly PersonaRepository repositorio;
        public PersonaService(string connectionString, string Proveedor)
        {
            conexion = new ConnectionManager(connectionString, Proveedor);
            repositorio = new PersonaRepository(conexion);
        }
        public string Guardar(Persona persona)
        {
            try
            {
               
                return $"Se guardo la Persona Satisfactoriamente";
            }
            catch (Exception e)
            {
                return $"Error de la Aplicacion: {e.Message}";
            }
            finally {  }
        }
        public ConsultaPersonaRespuesta ConsultarTodos()
        {
            ConsultaPersonaRespuesta respuesta = new ConsultaPersonaRespuesta();
            try
            {
                
                conexion.Open();
                respuesta.Personas = repositorio.ConsultarTodos();
                conexion.Close();
                if (respuesta.Personas.Count>0)
                {
                    respuesta.Mensaje = "Se consultan los Datos";
                }
                else
                {
                    respuesta.Mensaje = "No hay datos para consultar";
                }
                respuesta.Error = false;
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.Mensaje= $"Error de la Aplicacion: {e.Message}";
                respuesta.Error = true;
                return respuesta;
            }
            finally { conexion.Close(); }

        }
        public string Eliminar(string identificacion)
        {
            try
            {
                conexion.Open();
                var persona = repositorio.BuscarPorIdentificacion(identificacion);
                if (persona != null)
                {
                    repositorio.Eliminar(persona);
                    conexion.Close();
                    return ($"El registro {persona.Nombre} se ha eliminado satisfactoriamente.");
                }
                else
                {
                    return ($"Lo sentimos, {identificacion} no se encuentra registrada.");
                }
            }
            catch (Exception e)
            {

                return $"Error de la Aplicación: {e.Message}";
            }
            finally { conexion.Close(); }

        }
        public string Modificar(Persona personaNueva)
        {
            try
            {
                conexion.Open();
                var personaVieja = repositorio.BuscarPorIdentificacion(personaNueva.Identificacion);
                if (personaVieja != null)
                {
                    repositorio.Modificar(personaNueva);
                    conexion.Close();
                    return ($"El registro {personaNueva.Nombre} se ha modificado satisfactoriamente.");
                }
                else
                {
                    return ($"Lo sentimos, {personaNueva.Identificacion} no se encuentra registrada.");
                }
            }
            catch (Exception e)
            {

                return $"Error de la Aplicación: {e.Message}";
            }
            finally { conexion.Close(); }

        }
        public BusquedaPersonaRespuesta BuscarxIdentificacion(string identificacion)
        {
            BusquedaPersonaRespuesta respuesta = new BusquedaPersonaRespuesta();
            try
            {

                conexion.Open();
                respuesta.Persona = repositorio.BuscarPorIdentificacion(identificacion);
                conexion.Close();
                respuesta.Mensaje = (respuesta.Persona!=null)?  "Se encontró la Persona buscada" : "La persona buscada no existe";
                respuesta.Error = false;
                return respuesta;
            }
            catch (Exception e)
            {
                
                respuesta.Mensaje = $"Error de la Aplicacion: {e.Message}";
                respuesta.Error =true;
                return respuesta;
            }
            finally { conexion.Close(); }
        }
        public ConteoPersonaRespuesta Totalizar()
        {
            ConteoPersonaRespuesta respuesta = new ConteoPersonaRespuesta();
            try
            {

                conexion.Open();
                respuesta.Cuenta = repositorio.Totalizar(); ;
                conexion.Close();
                respuesta.Error = false;
                respuesta.Mensaje = "Se consultan los Datos";
                
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.Mensaje = $"Error de la Aplicacion: {e.Message}";
                respuesta.Error = true;
                return respuesta;
            }
            finally { conexion.Close(); }
           
        }
        public ConteoPersonaRespuesta TotalizarTipo(string tipo)
        {
            ConteoPersonaRespuesta respuesta = new ConteoPersonaRespuesta();
            try
            {

                conexion.Open();
                respuesta.Cuenta = repositorio.TotalizarTipo(tipo);
                conexion.Close();
                respuesta.Error = false;
                respuesta.Mensaje = "Se consultan los Datos";
               
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.Mensaje = $"Error de la Aplicacion: {e.Message}";
                respuesta.Error = true;
                return respuesta;
            }
            finally { conexion.Close(); }
           
        }
       
    }

    public class ConsultaPersonaRespuesta
    {
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public IList<Persona> Personas { get; set; }
    }


    public class BusquedaPersonaRespuesta
    {
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public Persona Persona { get; set; }
    }



    public class ConteoPersonaRespuesta
    {
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public int Cuenta { get; set; }
    }
}
