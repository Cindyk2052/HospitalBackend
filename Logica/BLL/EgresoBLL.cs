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
    public class EgresoBLL
    {
        public static ListadoPaginadoVMR<EgresoVMR> LeerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            return EgresoDAL.LeerTodo(cantidad, pagina, textoBusqueda);
        }

        public static EgresoVMR LeerUno(long id)
        {
            return EgresoDAL.LeerUno(id);
        }

        public static long Crear(Egreso item)
        {
            return EgresoDAL.Crear(item);
        }

        public static void Actualizar(EgresoVMR item)
        {
            EgresoDAL.Actualizar(item);
        }

        public static void Eliminar(List<long> ids)
        {
            EgresoDAL.Eliminar(ids);
        }
    }
}
