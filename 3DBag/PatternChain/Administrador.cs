using Propiedades_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3DBag.PatternChain
{
    public class Administrador : Aprobador
    {
        public override string Procesar(Usuario usuario)
        {
            if (Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Modificar_Usuario)               
                && Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Modificar_Familia)
                && Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Modificar_Patentes)
                && Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Modificar_Articulo)
                && Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Modificar_Venta))
            {
                return "Administrador";
            }
            else
            {
                return _siguiente.Procesar(usuario);
            }
        }
    }
}