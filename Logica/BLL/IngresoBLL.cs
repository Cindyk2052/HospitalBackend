using Comun.ViewModels;
using Datos.DAL;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.BLL
{
    public class IngresoBLL
    {
        public static ListadoPaginadoVMR<IngresoVMR> LeerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            return IngresoDAL.LeerTodo(cantidad, pagina, textoBusqueda);

        }

        public static IngresoVMR LeerUno(long id)
        {
            return IngresoDAL.LeerUno(id);
        }

        public static long Crear(Ingreso item)
        {
            return IngresoDAL.Crear(item);
        }

        public static void Actualizar(IngresoVMR item)
        {
            IngresoDAL.Actualizar(item);
        }

        public static void Eliminar(List<long> ids)
        {
            IngresoDAL.Eliminar(ids);
        }
    }
}
