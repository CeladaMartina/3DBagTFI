using Propiedades_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3DBag.PatternChain
{
    public class Webmaster : Aprobador
    {
        public override string Procesar(Usuario usuario)
        {
            if (Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Modificar_Articulo)            
                && Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Modificar_Usuario)               
                && Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Modificar_Familia)
                && Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Modificar_Patentes)                
                && Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Realizar_BackUp)
                && Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Realizar_Restore)
                && Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Recalcular_Digitos)
                && Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Ver_Bitacora)                
                && Propiedades_BE.SingletonLogin.GetInstance.IsInRole(Propiedades_BE.TipoPermiso.Gestionar_Venta))
            {
                return "Webmaster";
            }
            else
            {
                return _siguiente.Procesar(usuario);
            }
        }
    }
}