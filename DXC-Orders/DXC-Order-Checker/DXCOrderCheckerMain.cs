using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DXCOrderChecker
{
	class DXCOrderCheckerMain
	{
		static void Main(string[] args)
		{
			var dbContext = new OrderCheckerDBModel();

			int items = 0;
			int maxID = -1;
			int index = 0;
			var orderCheck = new OrdersChecker();
			orderCheck.ShowAllRecords();
			//orderCheck.WaitForKeyPress();
			do
			{
				try
				{

					var queryDB1 = from db in dbContext.Orders select db.Id;
					maxID = queryDB1.Max();
					items = queryDB1.Count();

				}
				catch (Exception ex)
				{

					Console.WriteLine("Get max ID failed with error: {0}", ex.Message);
				}

				if (maxID >= 0)
				{
					var queryDB = (from db in dbContext.Orders where db.Id == maxID select db).SingleOrDefault();
					if (orderCheck.OrdersStatusChanged(queryDB,items))
					{
						Console.WriteLine("Status changed !!!");
					}
					else
					{
						Console.WriteLine("Status not changed");
					} 
				}
				Console.WriteLine("Itteration: {0}",++index);
				Thread.Sleep(5000);
				

			} while (index < 20);

		

		}

		#region Methods and Functions
		#endregion

	}
}
