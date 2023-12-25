using System.Text;

namespace Main
{
	internal class Program
	{
		static void Main(string[] args)
		{
			dialoger.Dialog dialog = new dialoger.Dialog();
			dialog.statements.Add(new dialoger.Statement());
			dialog.statements.Add(new dialoger.Statement());
			dialog.statements.Add(new dialoger.Statement());
			dialog.statements.Add(new dialoger.Statement());

			dialog[0].message = "Hello, have you heard of GNU/Linux?";
			dialog[0].responses.Add(new dialoger.Response("Yes", 2));
			dialog[0].responses.Add(new dialoger.Response("No", 1));

			dialog[1].message = "Wdym, it's the greatest OS family in the world! It provides more functionality than Window$ and MacOS, without wasting CPU and RAM in the process! It runs on anything! Linux on a 2003 ThinkPad is more capable and useful than Window$ on an RTX 4090!";
			dialog[1].responses.Add(new dialoger.Response("Ok, ima check it out.", 2));
			dialog[1].responses.Add(new dialoger.Response("No, Windows best", 3));

			dialog[2].message = "Hell yea, team Linux!";
			dialog[2].responses.Add(new dialoger.Response("Hell Yeah!", -1));

			dialog[3].message = "*Prepares files named CON*";
			dialog[3].responses.Add(new dialoger.Response("Your computer ran into a problem (Error code 0x959548964515189465165, Something happened). Your windows installation is kaputt and must be reinstalled. Say goodbye to any data that wasn't backed up.\nPro tip: On Linux, the error messages tell you exactly what went wrong, allowing you to fix your installation without reinstalling and save your data.", -1));

			using (FileStream writer = new FileStream("dialog.json", FileMode.Create))
			{
				writer.Write(Encoding.UTF8.GetBytes(dialog.Json()));
			}

			while (dialog.Talking())
			{
				Console.WriteLine(dialog.Now());
				for (int i = 0; i < dialog.Now().responses.Count; i++)
				{
					Console.WriteLine(i + ": " + dialog.Now()[i]);
				}
				int response = int.Parse(Console.ReadLine());
				dialog.Answer(response);
			}
		}
	}
}