using Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MailDTO
    {
        public string DominioSmtp { get; set; }
     
        public int Puerto { get; set; }
        public bool Ssl { get; set; } = false;
      
        public string Correo { get; set; }
        
        public string Pass { get; set; }

        public async Task<bool> EnvioAutentificacionProveedor(string destinatario, string mensaje)
        {
            try
            {
            SmtpClient smtp = new SmtpClient(DominioSmtp, Puerto);
            smtp.Credentials = new NetworkCredential(Correo,Encriptacion.DesEncriptacion(Pass));
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = Ssl;
            smtp.UseDefaultCredentials = false;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(Correo, Correo);
            mail.To.Add(new MailAddress(destinatario));
            mail.Subject = "Nuevo usuario de Ingresos Platform";
            mail.IsBodyHtml = false;
            mail.Body = mensaje;

            await smtp.SendMailAsync(mail);
            mail.Dispose();
            return true;
            }catch (Exception e)
            {
                return false;
            }
        }

    }
}
