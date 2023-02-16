using Comun.ViewModels;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DAL
{
    public class PacienteDAL
    {
        public static ListadoPaginadoVMR<PacienteVMR> LeerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            ListadoPaginadoVMR<PacienteVMR> resultado = new ListadoPaginadoVMR<PacienteVMR>();

            using (var db = DbConexion.Create())
            {
                var query = db.Paciente.Where(x => !x.borrado).Select(x => new PacienteVMR
                {
                    id = x.id,
                    cedula = x.cedula,
                    nombre = x.nombre + " " + x.apellidoPaterno + (x.apellidoMaterno != null ? ("" + x.apellidoMaterno) : ""),
                    correoElectronico = x.correoElectronico
                });

                if (!string.IsNullOrEmpty(textoBusqueda))
                {
                    query = query.Where(x => x.cedula.Contains(textoBusqueda) || x.nombre.Contains(textoBusqueda));
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

        public static PacienteVMR LeerUno(long id)
        {
            PacienteVMR item = null;
            using (var db = DbConexion.Create())
            {
                item = db.Paciente.Where(x => !x.borrado && x.id == id).Select(x => new PacienteVMR
                {
                    id = x.id,
                    cedula = x.cedula,
                    nombre = x.nombre,
                    apellidoMaterno = x.apellidoMaterno,
                    apellidoPaterno = x.apellidoPaterno,
                    direccion = x.direccion,
                    celular = x.celular,
                    correoElectronico = x.correoElectronico
                    
                }).FirstOrDefault();

            }

            return item;

        }

        public static long Crear(Paciente item)
        {
            
            using (var db = DbConexion.Create())
            {
                item.borrado = false;
                db.Paciente.Add(item);
                db.SaveChanges();

            }

            return item.id;

        }

        public static void Actualizar(PacienteVMR item)
        {
            using (var db = DbConexion.Create())
            {
                var itemUpdate = db.Paciente.Find(item.id);

                itemUpdate.cedula = item.cedula;
                itemUpdate.nombre = item.nombre;
                itemUpdate.apellidoMaterno = item.apellidoMaterno;
                itemUpdate.apellidoPaterno = item.apellidoPaterno;
                itemUpdate.direccion = item.direccion;
                itemUpdate.celular = item.celular;
                itemUpdate.correoElectronico = item.correoElectronico;

                db.Entry(itemUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void Eliminar(List<long> ids)
        {
            using (var db = DbConexion.Create())
            {
                var items = db.Paciente.Where(x => ids.Contains(x.id));

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
