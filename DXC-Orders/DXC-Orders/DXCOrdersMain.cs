using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXCOrders
{
	class OrdersMain
	{
		static void Main(string[] args)
		{
			var menu = new OrdersManager();
			char key;



			do
			{
				menu.ShowMenu();
				key = Console.ReadKey(true).KeyChar;
					
				switch (key.ToString().ToUpper())
				{
					case "A":
						//Add record
						menu.AddRecord();
						break;
					case "R":
						//Remove record
						menu.RemoveRecord();
						break;
					case "L":
						//List all records
						menu.ListAllRecords();
						menu.WaitForKeyPress();
						break;
					default:
						break;
				}


			} while (!(key.ToString().ToUpper() == (ConsoleKey.X).ToString()));
			

			menu.WaitForKeyPress();
			
		}

		#region Method and Functions

		#endregion
	}
	
}
