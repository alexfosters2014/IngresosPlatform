using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IngresosPlatform.Client.Services
{
    public interface IServiceArchivo
    {
        public Task<string> Subir(Stream fileStream, string fileName);
    }
}
