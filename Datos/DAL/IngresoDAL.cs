using Comun.ViewModels;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DAL
{
    public class IngresoDAL
    {
        public static ListadoPaginadoVMR<IngresoVMR> LeerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            ListadoPaginadoVMR<IngresoVMR> resultado = new ListadoPaginadoVMR<IngresoVMR>();

            using (var db = DbConexion.Create())
            {
                var query = db.Ingreso.Where(x => !x.borrado).Select(x => new IngresoVMR
                {
                    id = x.id,
                    fecha = x.fecha,
                    numeroCama = x.numeroCama,
                    numeroSala = x.numeroSala,
                    diagnostico = x.diagnostico,
                    medico = (from m in db.Medico
                              where
                                  m.id == x.medicoId
                              select new MedicoVMR
                              {
                                  cedula = m.cedula,
                                  nombre = m.nombre + " " + m.apellidoPaterno + (m.apellidoMaterno != null ? " " + m.apellidoMaterno : ""),
                              }).FirstOrDefault(),

                    paciente = (from p in db.Paciente
                                where
                                    p.id == x.pacienteId
                                select new PacienteVMR
                                {
                                    cedula = p.cedula,
                                    nombre = p.nombre + " " + p.apellidoPaterno + (p.apellidoMaterno != null ? " " + p.apellidoMaterno : ""),
                                }).FirstOrDefault()

                });

                if (!string.IsNullOrEmpty(textoBusqueda))
                {
                    query = query.Where(x => x.medico.cedula.Contains(textoBusqueda) || x.medico.nombre.Contains(textoBusqueda) || x.paciente.cedula.Contains(textoBusqueda) || x.paciente.nombre.Contains(textoBusqueda));
                }

                resultado.cantidadTotal = query.Count();

                resultado.elemento = query
                    .OrderBy(x => x.id)
                    .Skip(pagina * cantidad)
                    .Take(cantidad)
                    .ToList();
            }
            return resultado;

        }

        public static IngresoVMR LeerUno(long id)
        {
            IngresoVMR item = null;

            using (var db = DbConexion.Create())
            {
                item = db.Ingreso.Where(x => !x.borrado && x.id == id).Select(x => new IngresoVMR
                {
                    id = x.id,
                    fecha = x.fecha,
                    numeroCama = x.numeroCama,
                    numeroSala = x.numeroSala,
                    diagnostico = x.diagnostico,
                    medicoId = x.medicoId,
                    pacienteId = x.pacienteId,
                }).FirstOrDefault();

            }

            return item;


        }

        public static long Crear(Ingreso item)
        {
            
            using (var db = DbConexion.Create())
            {
                item.borrado = false;
                item.fecha = DateTime.Now;
                db.Ingreso.Add(item);
                db.SaveChanges();

            }

            return item.id;

        }

        public static void Actualizar(IngresoVMR item )
        {
            using (var db = DbConexion.Create())
            {
                var itemUpdate = db.Ingreso.Find(item.id);

                itemUpdate.fecha = item.fecha;
                itemUpdate.numeroCama = item.numeroCama;
                itemUpdate.numeroSala = item.numeroSala;
                itemUpdate.diagnostico = item.diagnostico;
                

                db.Entry(itemUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

        }

        public static void Eliminar(List<long> ids)
        {
            using (var db = DbConexion.Create())
            {
                var items = db.Ingreso.Where(x => ids.Contains(x.id));

                foreach (var item in items)
                {
                    item.borrado = true;
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }

                db.SaveChanges();
            }

        }
    }
}
