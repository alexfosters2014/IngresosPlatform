﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun
{
    public class SD
    {
        public enum TipoUsuario
        {
            ProveedorIngPlt,
            OperadorIngPlt,
            PorteriaIngPlt
        }
        public enum TipoAutIng
        {
            Pendiente,
            Autorizado,
            NoAutorizado
        }
    }
}
