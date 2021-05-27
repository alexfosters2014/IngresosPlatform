using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngresosPlatform.Client.Helper
{
    public static class IJSRuntimeExtension
    {
        public static async ValueTask MsgExitoso(this IJSRuntime js, string mensaje)
        {
            await js.InvokeVoidAsync("MostrarMsg", "success",mensaje);
        }
        public static async ValueTask MsgError(this IJSRuntime js, string mensaje)
        {
            await js.InvokeVoidAsync("MostrarMsg", "error", mensaje);
        }
        public static async ValueTask<bool> MsgConfirmacion(this IJSRuntime js, string pregunta)
        {
            return await js.InvokeAsync<bool>("ConfirmarOperacion", pregunta);
        }
        
    }
}
