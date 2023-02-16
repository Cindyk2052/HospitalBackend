using Comun.ViewModels;
using Logica.BLL;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PacienteController : ApiController
    {
        [HttpGet]
        public IHttpActionResult LeerTodo(int cantidad = 10, int pagina = 0, string textoBusqueda = null)
        {
            var respuesta = new RespuestaVMR<ListadoPaginadoVMR<PacienteVMR>>();

            try
            {
                respuesta.datos = PacienteBLL.LeerTodo(cantidad, pagina, textoBusqueda);

            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = null;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }
            return Content(respuesta.codigo, respuesta);

        }


        [HttpGet]
        public IHttpActionResult LeerUno(long id)
        {
            var respuesta = new RespuestaVMR<PacienteVMR>();

            try
            {
                respuesta.datos = PacienteBLL.LeerUno(id);

            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = null;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }

            if (respuesta.datos == null && respuesta.mensajes.Count() == 0)
            {
                respuesta.codigo = HttpStatusCode.NotFound;
                respuesta.mensajes.Add("Elemento no encontrado");
            }

            return Content(respuesta.codigo, respuesta);
        }

        [HttpPost]
        public IHttpActionResult Crear(Paciente item)
        {
            var respuesta = new RespuestaVMR<long?>();

            try
            {
                respuesta.datos = PacienteBLL.Crear(item);

            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = null;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }
            return Content(respuesta.codigo, respuesta);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(long id, PacienteVMR item)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                item.id = id;
                PacienteBLL.Actualizar(item);
                respuesta.datos = true;

            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = false;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }
            return Content(respuesta.codigo, respuesta);
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(List<long> ids)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                PacienteBLL.Eliminar(ids);
                respuesta.datos = true;

            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = false;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }
            return Content(respuesta.codigo, respuesta);
        }
    }
}
