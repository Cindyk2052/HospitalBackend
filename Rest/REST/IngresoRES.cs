using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comun.ViewModels;
using Newtonsoft.Json;
using RestSharp;


namespace Rest.REST
{
    public class IngresoRES
    {
        private const string url = "https://minegocioefectivo.com/hospital/api";

        public static object Method{ get; private set; }

        public static SalaCamaVMR ObtenerSalaCama()
        {
            SalaCamaVMR resultado = null;

            RestClient rest = new RestClient(url);

            var request = new RestRequest("/hospital", Method.Get);

            var response = rest.Execute(request);

            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Error al obtener la respuesta del servicio.");
            }

            try
            {
                var salaCamaTem = JsonConvert.DeserializeObject<RespuestaVMR<SalaCamaVMR>>(response.Content);
                if (salaCamaTem.codigo != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(string.Join(", ", salaCamaTem.mensajes));
                }

                resultado = salaCamaTem.datos;

            }
            catch (Exception e)
            {
                throw new Exception($"Error al deserealizar.[{response.Content}]", e);
            }

            return resultado;
        }
    }
}
