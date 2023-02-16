using Comun.ViewModels;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DAL
{
    public class EgresoDAL
    {
        public static ListadoPaginadoVMR<EgresoVMR> LeerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            ListadoPaginadoVMR<EgresoVMR> resultado = new ListadoPaginadoVMR<EgresoVMR>();

            using (var db = DbConexion.Create())
            {
                var query = db.Egreso.Where(e => !e.borrado).Select(e => new EgresoVMR
                {
                    id = e.id,
                    fecha = e.fecha,
                    monto = e.monto,
                    //LAMBDA
                    /*medico = new MedicoVMR
                    {
                        cedula = e.Medico.cedula,
                        nombre = e.Medico.nombre + " " + e.apellidoPaterno + (e.apellidoMaterno != null ? " " + e.apellidoMaterno : ""),,
                    },*/

                    //LINQ
                    medico = (from m in db.Medico
                              where
                                  m.id == e.medicoId
                              select new MedicoVMR
                              {
                                  cedula = m.cedula,
                                  nombre = m.nombre + " " + m.apellidoPaterno + (m.apellidoMaterno != null ? " " + m.apellidoMaterno : ""),
                              }).FirstOrDefault(),

                    paciente = (from p in db.Paciente
                                where
                                    p.id == e.ingresoId
                                select new PacienteVMR
                                {
                                    cedula = p.cedula,
                                    nombre = p.nombre + " " + p.apellidoPaterno + (p.apellidoMaterno != null ? " " + p.apellidoMaterno : ""),
                                }).FirstOrDefault()
                });

                if (!string.IsNullOrEmpty(textoBusqueda))
                {
                    query = query.Where(e => e.medico.cedula.Contains(textoBusqueda) || e.medico.nombre.Contains(textoBusqueda) || e.paciente.cedula.Contains(textoBusqueda) || e.paciente.nombre.Contains(textoBusqueda));
                }

                resultado.cantidadTotal = query.Count();
                resultado.elemento = query
                    .OrderBy(e => e.id)
                    .Skip(cantidad * pagina)
                    .Take(cantidad)
                    .ToList();

            }
            return resultado;
        }

        public static EgresoVMR LeerUno(long id)
        {
            EgresoVMR item = null;

            using (var db = DbConexion.Create())
            {
                item = db.Egreso.Where(x => !x.borrado && x.id == id).Select(x => new EgresoVMR
                {
                    id = x.id,
                    fecha = x.fecha,
                    tratamiento = x.tratamiento,
                    monto = x.monto,
                    medicoId = x.medicoId,
                    ingresoId = x.ingresoId
                }).FirstOrDefault();

            }

            return item;
        }

        public static long Crear(Egreso item)
        {

            using (var db = DbConexion.Create())
            {
                item.borrado = false;
                item.fecha = DateTime.Now;
                db.Egreso.Add(item);
                db.SaveChanges();

            }

            return item.id;

        }

        public static void Actualizar(EgresoVMR item)
        {
            using (var db = DbConexion.Create())
            {
                var itemUpdate = db.Egreso.Find(item.id);

                itemUpdate.fecha = item.fecha;
                itemUpdate.tratamiento = item.tratamiento;
                itemUpdate.monto = item.monto;
               
            
                db.Entry(itemUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

        }

        public static void Eliminar(List<long> ids)
        {
            using (var db = DbConexion.Create())
            {
                var items = db.Egreso.Where(e => ids.Contains(e.id));

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
