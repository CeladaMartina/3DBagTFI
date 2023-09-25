using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _3DBag
{
    /// <summary>
    /// Descripción breve de WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        Negocio_BLL.Producto GestorProducto = new Negocio_BLL.Producto();

        [WebMethod]
        public string TopListaProd()
        {
            List<Propiedades_BE.Articulo> articulo = GestorProducto.TopListaProd();
            string respuesta = "El producto " + articulo[0].Nombre + " " + articulo[0].Descripcion + " tiene un precio de $" + articulo[0].PUnit;
            return respuesta;
        }
    }
}
