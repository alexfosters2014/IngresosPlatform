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
        
        public string PassMail { get; set; }

        public bool EnvioAutentificacionProveedor(string destinatario, string passInicial)
        {
            try
            {
            SmtpClient smtp = new SmtpClient(DominioSmtp, Puerto);
            smtp.Credentials = new NetworkCredential(Correo,Encriptar.DesEncriptacion(PassMail));
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = Ssl;
            smtp.UseDefaultCredentials = false;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(Correo, Correo);
            mail.To.Add(new MailAddress(destinatario));
            mail.Subject = "Nuevo usuario de Ingresos Platform";
            mail.IsBodyHtml = false;
            mail.Body = $"Bienvenidos a la Ingresos Platform. Su usuario es su RUT y la contraseña inicial es:{passInicial}, la cual deberá cambiar una vez autentificado al sistema";

            smtp.Send(mail);
            mail.Dispose();

            return true;
            }catch (Exception e)
            {
                return false;
            }
        }

    }
}
