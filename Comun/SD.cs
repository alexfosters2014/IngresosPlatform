﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun
{
    public class SD
    {
        public const string LocalUsuario = "UsuarioLocalLogin";
        public enum TipoUsuario
        {
            ProveedorIngPlt,
            OperadorIngPlt,
            PorteriaIngPlt
        }
        public enum TipoAutIng
        {
            PENDIENTE,
            AUTORIZADO,
            NOAUTORIZADO
        }
        public enum IngresosReturn
        {
            OK,
            REVISAR,
            ERROR
        }
    }
}
