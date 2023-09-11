using Propiedades_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3DBag.PatternChain
{
    public class Empleado :Aprobador
    {
        public override string Procesar(Usuario usuario)
        {
            if (Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Modificar_Articulo) 
                && Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Modificar_Venta))
            {
                return "Empleado";
            }
            else
            {
                return _siguiente.Procesar(usuario);
            }
        }
    }
}