using System;
using System.Linq;

namespace DXCOrders
{
	class OrdersManager
	{
		
		private string[] menuItems = new string[] {"'A' - Add Record","'R' - Remove Record", "'L' - List All Records", "'X' - Exit application"};
		

		public OrdersManager()
		{
			
		}

		public void ShowMenu()
		{
			Console.Clear();
			foreach (var item in menuItems)
			{
				Console.WriteLine("{0}",item);
			}
		}

		public void ListAllRecords()
		{
			var dbContext = new OrdersDBModel();
			//var dbContext = new DXCOrdersDataModel();
			
			Console.Clear();
			Console.WriteLine("Listing all records");
			var queryDB = from db in dbContext.Orders select db;
			foreach (var item in queryDB)
			{
				Console.WriteLine("ID: {0} | Creator: {1} | TaskName: {2} | TaskDescription: {3} |" +
					"CreatedOn: {4:yyyy-MM-dd} | Assignee: {5} | Status: {6} | Notes: {7}",
					item.Id, item.Creator, item.TaskName, item.TaskDescription, item.CreatedOn, item.Assignee, item.Status,item.Note);
			}
		}

		public void AddRecord()
		{
			Console.Clear();
			var dbContext = new OrdersDBModel();

			int newId = (from db in dbContext.Orders select db.Id).Max();
			Console.WriteLine("Adding new New Record ID: {0}", newId+1);
			Console.WriteLine("Creator:");
			string newCreator = Console.ReadLine();
			Console.WriteLine("TaksName:");
			string newTaskName = Console.ReadLine();
			Console.WriteLine("TaskDescription:");
			string newTaskDescription = Console.ReadLine();
			Console.WriteLine("Status:");
			string newStatus = Console.ReadLine();
			Console.WriteLine("Notes:");
			string newNotes = Console.ReadLine();
			var newCreatedOn = DateTime.Now;
			string newAssignee = "";


			try
			{
				dbContext.Orders.Add(new Order()
				{
					Id = newId + 1,
					Creator = newCreator,
					TaskName = newTaskName,
					TaskDescription = newTaskDescription,
					Assignee = newAssignee,
					CreatedOn = newCreatedOn,
					Note = newNotes,
					Status = newStatus
				});
				dbContext.SaveChanges();
				Console.WriteLine("New Record with ID:{0} successfully added into DB", newId+1);
			}
			catch (Exception ex)
			{

				Console.WriteLine("Add new record ID:{0} failed with error: {1}",newId=1, ex.Message);
			}
			
			this.WaitForKeyPress();
		}


		public void RemoveRecord()
		{
			Console.Clear();
			var dbContext = new OrdersDBModel();

			Console.WriteLine("Remove One Record:");
			this.ListAllRecords();
			Console.WriteLine("Select Record ID to be removed:");
			int idToRemove = int.Parse(Console.ReadLine());
			var queryDB = (from db in dbContext.Orders where db.Id == idToRemove select db).SingleOrDefault();
			Console.WriteLine("Are you sure you want to remove record ID: {0} from DB ? Y/N", idToRemove);
			char key;
			key = Console.ReadKey(true).KeyChar;

			if (key.ToString().ToUpper() == ConsoleKey.Y.ToString())
			{
				try
				{
					dbContext.Orders.Remove(queryDB);
					dbContext.SaveChanges();
					Console.WriteLine("Record ID:{0} successfully removed from DB", idToRemove);
				}
				catch (Exception ex)
				{

					Console.WriteLine("Remove record ID:{0} failed with error: {1}", idToRemove, ex.Message);
				}
			}
			else
			{
				Console.WriteLine("Record ID:{0} not removed from DB",idToRemove);
			}
			
			this.WaitForKeyPress();
		}

		public void WaitForKeyPress()
		{
			Console.WriteLine("Press any key ...");
			Console.ReadKey(true);
		}

	}

}
