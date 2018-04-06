using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXCOrderChecker
{
	class OrdersChecker
	{
		private string lastTaskName = "";
		private int lastId = 0;
		private int itemCount = 0;
		public OrdersChecker()
		{

		}

		public bool OrdersStatusChanged(Order Order, int _itemCount)
		{
			if (lastTaskName == "" && lastId.Equals(0) && itemCount.Equals(0))
			{
				lastTaskName = Order.TaskName;
				lastId = Order.Id;
				itemCount = _itemCount;
				Console.WriteLine("Initial loop");
				return false;
			}
			else
			{
				if (lastTaskName == Order.TaskName && lastId == Order.Id && itemCount == _itemCount)
				{
					Console.WriteLine("LastTaskName: {0} and LastId: {1} DONT changed",lastTaskName, lastId);
					Console.WriteLine("Order.TaskName: {0} and Order.Id: {1} DONT changed", Order.TaskName, Order.Id);
					return false;
				}
				else
				{
					if (_itemCount > itemCount)
					{
						Console.WriteLine("LastTaskName: {0} - LastId: {1}  - ItemCount: {2} changed -> New Record Added !!!", lastTaskName, lastId, itemCount);
						Console.WriteLine("Order.TaskName: {0} - Order.Id: {1} - ItemCount: {2} changed -> New Record Added !!!", Order.TaskName, Order.Id, _itemCount); 
					}
					if (_itemCount < itemCount)
					{
						Console.WriteLine("LastTaskName: {0} - LastId: {1} - ItemCount: {2}  changed -> Record Removed !!!", lastTaskName, lastId, itemCount);
						Console.WriteLine("Order.TaskName: {0} - Order.Id: {1} - ItemCount: {2}  changed -> Record Removed !!!", Order.TaskName, Order.Id, _itemCount);
					}
					lastTaskName = "";
					lastId = 0;
					itemCount = 0;
					return true;
				}
			}
			
		}

		public void ShowAllRecords()
		{
			var dbContext = new OrderCheckerDBModel();
			var queryDB = from db in dbContext.Orders orderby db.Id select db;
			foreach (var item in queryDB)
			{
				Console.WriteLine("ID: {0} | Creator: {1} | TaskName: {2} | TaskDescription: {3} |" +
					"CreatedOn: {4:yyyy-MM-dd} | Assignee: {5} | Status: {6} | Notes: {7}",
					item.Id, item.Creator, item.TaskName, item.TaskDescription, item.CreatedOn, item.Assignee, item.Status, item.Note);
			}
		}

		public void WaitForKeyPress()
		{
			Console.WriteLine("Press any key ...");
			Console.ReadKey(true);
		}
	}
}
