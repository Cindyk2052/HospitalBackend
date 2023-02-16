using Comun.ViewModels;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DAL
{
    public class MedicoDAL
    {
        public static ListadoPaginadoVMR<MedicoVMR> LeerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            ListadoPaginadoVMR<MedicoVMR> resultado = new ListadoPaginadoVMR<MedicoVMR>();

            using (var db = DbConexion.Create())
            {
                var query = db.Medico.Where(x => !x.borrado).Select(x => new MedicoVMR
                {
                    id = x.id,
                    cedula = x.cedula,
                    nombre = x.nombre + " " + x.apellidoPaterno + (x.apellidoMaterno != null ? ("" + x.apellidoMaterno) : ""),
                    esEspecialista = x.esEspecialista,
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
        public static MedicoVMR LeerUno(long id)
        {
            MedicoVMR item = null;

            using (var db = DbConexion.Create())
            {
                item = db.Medico.Where(x => !x.borrado && x.id == id).Select(x => new MedicoVMR
                {
                    id = x.id,  
                    cedula = x.cedula,
                    nombre = x.nombre,
                    apellidoMaterno = x.apellidoMaterno,
                    apellidoPaterno = x.apellidoPaterno,
                    habilitado = x.habilitado,
                    esEspecialista = x.esEspecialista
                }).FirstOrDefault();

            }

            return item;

        }
        public static long Crear(Medico item)
        {
            using (var db = DbConexion.Create())
            {
                item.borrado = false;
                db.Medico.Add(item);
                db.SaveChanges();

            }

            return item.id;
        }
        public static void Actualizar(MedicoVMR item)
        {
            using (var db = DbConexion.Create())
            {
                var itemUpdate = db.Medico.Find(item.id);

                itemUpdate.cedula = item.cedula;
                itemUpdate.nombre = item.nombre;
                itemUpdate.apellidoMaterno = item.apellidoMaterno;
                itemUpdate.apellidoPaterno = item.apellidoPaterno;
                itemUpdate.habilitado = item.habilitado;
                itemUpdate.esEspecialista = item.esEspecialista;

                db.Entry(itemUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void Eliminar(List<long> ids)
        {
            using (var db = DbConexion.Create())
            {
                var items = db.Medico.Where(x => ids.Contains(x.id));

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
