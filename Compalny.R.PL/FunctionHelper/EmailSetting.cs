using Company.R.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Compalny.R.PL.FunctionHelper
{
	public static class EmailSetting
	{
		public static void SendEmail(Email email)
		{
			//Host Name 
			//var client = new SmtpClient("smtp.gmail.com", 587);
			//client.Credentials = new NetworkCredential("","");


			var client = new SmtpClient("smtp.live.com", 465);
			client.Credentials = new NetworkCredential("BGroup@hotmail.com", "AspaApiALu");
			client.Send("BBGroup_@hotmail.com",email.To,email.Subject,email.Body);
		}
	}
}
