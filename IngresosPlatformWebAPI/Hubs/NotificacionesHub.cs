using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngresosPlatformWebAPI.Hubs
{
    public class NotificacionesHub : Hub
    {
        public async Task EnviarNotificacion(string usuario, string notificacion)
        {
            await Clients.All.SendAsync("RecibirNotificacion", usuario, notificacion);
        }
        
    }
}
