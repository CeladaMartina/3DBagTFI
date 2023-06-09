﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propiedades_BE
{
    public class Cliente
    {
        private int _idcliente;

        public int IdCliente
        {
            get { return _idcliente; }
            set { _idcliente = value; }
        }
        private string _dni;

        public string DNI
        {
            get { return _dni; }
            set { _dni = value; }
        }

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _apellido;

        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        private DateTime _fechanac;

        public DateTime FechaNac
        {
            get { return _fechanac; }
            set { _fechanac = value; }
        }

        private int _tel;

        public int Tel
        {
            get { return _tel; }
            set { _tel = value; }
        }

        private string _mail;

        public string Mail
        {
            get { return _mail; }
            set { _mail = value; }
        }

        private bool _baja;

        public bool BajaLogica
        {
            get { return _baja; }
            set { _baja = value; }
        }
    }
}
