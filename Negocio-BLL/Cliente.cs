using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio_BLL
{
    public class Cliente
    {
        Acceso_DAL.Cliente Mapper = new Acceso_DAL.Cliente();
        Propiedades_BE.Cliente ClienteTemp = new Propiedades_BE.Cliente();

        public int Alta(int IdCliente, string Nombre, string Apellido, string DNI, string Mail, int Tel, DateTime FechaNac)
        {
            ClienteTemp.IdCliente = IdCliente;
            ClienteTemp.Nombre = Nombre;
            ClienteTemp.Apellido = Apellido;
            ClienteTemp.DNI = DNI;
            ClienteTemp.Mail = Mail;
            ClienteTemp.Tel = Tel;
            ClienteTemp.FechaNac = FechaNac;

            return Mapper.Alta(ClienteTemp);
        }
    }
}
