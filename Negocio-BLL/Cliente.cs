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

        public List<Propiedades_BE.Cliente> Listar()
        {
            List<Propiedades_BE.Cliente> Lista = Mapper.Listar();
            return Lista;
        }
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

        public int Modificar(int IdCliente, string Nombre, string Apellido, string DNI, string Mail, int Tel, DateTime FechaNac)
        {
            ClienteTemp.IdCliente = IdCliente;
            ClienteTemp.Nombre = Nombre;
            ClienteTemp.Apellido = Apellido;
            ClienteTemp.DNI = DNI;
            ClienteTemp.Mail = Mail;
            ClienteTemp.Tel = Tel;
            ClienteTemp.FechaNac = FechaNac;

            return Mapper.Modificar(ClienteTemp);
        }

        public int Baja(int IdCliente)
        {
            ClienteTemp.IdCliente = IdCliente;

            return Mapper.Baja(ClienteTemp);
        }

        public int SeleccionarID(string DNI)
        {
            return Mapper.SeleccionarID(DNI);
        }

        public List<string> DNIsClientes()
        {
            return Mapper.DNIsClientes();
        }

        public string SeleccionarNombreCliente(string DNI)
        {
            return Mapper.SeleccionarNombreCliente(DNI);
        }

        public int SeleccionarDNICliente(string NombreCliente)
        {
            return Mapper.SeleccionarDNICliente(NombreCliente);
        }

        public List<string> NombresClientes()
        {
            return Mapper.NombresClientes();
        }

    }
}
