using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemandTrackerForm
{
	public partial class Form2 : Form
	{
		private string[] header = new string[] { "Id", "Creator", "TaskName", "TaskDescription", "CreatedOn", "Assignee", "Status", "Note", "LockStatus" };
		//private Order currentDemandItemForm2 = new Order();
		
		private enum RecordOptions {AddRecord, ModifyRecord}
		RecordOptions actionToTake;

		private int currentIndex;

		public Form2()
		{
			
			
			InitializeComponent();
			var createdOn = DateTime.Now;

			actionToTake = RecordOptions.AddRecord;
			
			button1.Enabled = false;
			button3.Enabled = false;
			this.ControlBox = false;

			DataGridViewCellStyle headerStyle = dataGridView2.ColumnHeadersDefaultCellStyle;
			headerStyle.BackColor = Color.Navy;
			headerStyle.ForeColor = Color.White;
			headerStyle.Font = new Font(dataGridView2.Font, FontStyle.Bold);
			
			this.Text = RecordOptions.AddRecord.ToString();
			dataGridView2.RowCount = 1;
			dataGridView2.ColumnCount = 9;
			dataGridView2.ReadOnly = false;
			for (int i = 0; i < header.Length; i++)
			{

				dataGridView2.Columns[i].Name = header[i];
				if (i == 0 || i == 4 || i == 8)
				{
					dataGridView2.Rows[0].Cells[i].ReadOnly = true;
				}
			}
			dataGridView2.Rows[0].Cells[4].Value = createdOn;
			dataGridView2.Rows[0].Cells[8].Value = false;
			ShowStatusForm2(actionToTake.ToString());
			this.Activated += Form2_Activated;
			
		}

		private void Form2_Activated(object sender, EventArgs e)
		{
			ResizeDataGridColumns();
		}

		private void ResizeDataGridColumns()
		{
			dataGridView2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
			//ShowStatusForm2("Column Resize Done");
		}

		//not used !!!
		/*
		public Form2(DataGridViewRow selectedRow)
			: this()
		{
			
			button1.Enabled = false;
			button3.Enabled = false;
			button4.Enabled = false;
			dataGridView2.RowCount = 1;
			dataGridView2.ColumnCount = selectedRow.Cells.Count;
			dataGridView2.ReadOnly = false;
			currentIndex = -1;

			for (int i = 0; i < selectedRow.Cells.Count; i++)
			{
				dataGridView2.Columns[i].Name = header[i];

				if (selectedRow.Cells[i].Equals(""))
				{
					dataGridView2.Rows[0].Cells[i].Value = "";
					richTextBox1.Text += "Index:" + i + " - Value: null" + "\n";
				}
				else
				{
					dataGridView2.Rows[0].Cells[i].Value = selectedRow.Cells[i].Value;
					richTextBox1.Text += "Index:" + i + " - Value: " + selectedRow.Cells[i].Value + "\n";
				}

				if (i == 0 || i == 4 || i == 8)
				{
					dataGridView2.Rows[0].Cells[i].ReadOnly = true;
				}
			}
			ShowStatusForm2(actionToTake.ToString());
		}
		*/

		public Form2(Order currentDemandItem)
			: this()
		{
			actionToTake = RecordOptions.ModifyRecord;
			button1.Enabled = false;
			button3.Enabled = false;
			
			this.Text = RecordOptions.ModifyRecord.ToString();
			currentIndex = currentDemandItem.Id;

			ShowDBRecordInGridView(currentDemandItem);
			ShowStatusForm2(actionToTake.ToString());
		}

		private void button1_Click(object sender, EventArgs e)
		{

			ShowStatusForm2("ModifyRecord - In progress ...");
			Order orderToModify = GetOrderFromGridView();
			orderToModify.Id = currentIndex;
			orderToModify.LockStatus = true;
			//ShowDBRecordInGridView(orderToModify);

			if (ModifyDemandInDB(orderToModify))
			{
				ShowStatusForm2("Record was succesfully modified in DB.");
			}
			else
			{
				ShowStatusForm2("Record was NOT modified in DB.");
			}

		}

		private void button2_Click(object sender, EventArgs e)
		{
			
			switch (actionToTake)
			{
				case RecordOptions.AddRecord:
					this.Close();
					break;

				case RecordOptions.ModifyRecord:
					if (CloseForm2ModifyRecord())
					{
						this.Close();
					}
					break;

				default:
					break;
			}
			
			this.Close();

		}

		private bool CloseForm2ModifyRecord()
		{
			if (!(currentIndex == -1))
			{
				var dbContext = new DemandTrackerDBModelNew();

				try
				{
					var selectedDemandRecord = (from db in dbContext.Orders where db.Id == currentIndex select db).SingleOrDefault();
					selectedDemandRecord.LockStatus = false;
					dbContext.SaveChanges();
					//MessageBox.Show("Saved into DB");
					return true;
				}
				catch (Exception ex)
				{
					ShowStatusForm2("Unable to unlock item in DB with index:" + currentIndex + ". Error:" + ex.Message);
					return false;
				}
			}
			return false;
		}

		private void ShowStatusForm2(string statusForm2)
		{
			label8.Text = statusForm2;
		}

		private void Form2_SizeChanged(object sender, EventArgs e)
		{
			ResizeForm2Objects();
		}

		private void ResizeForm2Objects()
		{
			var formSize = this.Size;
			dataGridView2.Size = new Size (formSize.Width - 3*dataGridView2.Location.X, dataGridView2.Size.Height);
			//richTextBox1.Location = new Point(richTextBox1.Location.X , dataGridView2.Height);
			//label8.Location = new Point (label8.Location.X, dataGridView2.Height + richTextBox1.Height + 4 * label8.Height);
			button2.Location = new Point (button3.Location.X + dataGridView2.Width - button2.Width , button1.Location.Y);
			
			if (formSize.Width < 3 * button1.Width)
			{
				this.ClientSize = new Size(4 * button1.Width,formSize.Height);
			}

			ShowStatusForm2("Button1:" + button1.Location.ToString() + " - Button2:" + button2.Location.ToString() +" - FormSize:" + formSize.ToString() + " - DataGridSize:" + dataGridView2.Size.ToString());


		}

		private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			ShowStatusForm2("Current Cell Edited:" + dataGridView2.CurrentCell.ColumnIndex.ToString());
			ResizeDataGridColumns();
			switch (actionToTake)
			{
				case RecordOptions.AddRecord:
					if (GridViewDataConsistency())
					{
						//enable button3
						button3.Enabled = true;
					}
					else
					{
						button3.Enabled = false;
					}
					break;

				case RecordOptions.ModifyRecord:
					if (GridViewDataConsistency())
					{
						//enable button1
						button1.Enabled = true;
					}
					else
					{
						button1.Enabled = false;
					}
					break;

				default:
					break;
			}

			
		}

		private bool GridViewDataConsistency()
		{
			
			bool dataConsistencyStatus = true;
			richTextBox1.Text = "";
			for (int i = 1; i < dataGridView2.Rows[0].Cells.Count; i++)
			{

				if (i == 5 || i == 7 || i == 8)
				{
					//do nothing
				}
				else
				{
					if ((dataGridView2.Rows[0].Cells[i].Value == null))
					{
						dataConsistencyStatus = false;
						richTextBox1.Text += "False: DataGridView:" + 0 + "," + i + ":" + dataGridView2.Rows[0].Cells[i].Value + "\n";
					}
					else
					{
						richTextBox1.Text += "True: DataGridView:" + 0 + "," + i + ":" + dataGridView2.Rows[0].Cells[i].Value + "\n";
					}
				}
			}

			return dataConsistencyStatus;

		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (CloseForm2AddRecord())
			{
				this.Close();
			}
		}

		private bool CloseForm2AddRecord()
		{
			ShowDBRecordInGridView(GetOrderFromGridView());
			if (AddNewDemandIntoDB(GetOrderFromGridView()))
			{
				ShowStatusForm2("DB record Added successfuly");
				return true;
			}
			else
			{
				return false;
			}
			
		}

		private Order GetOrderFromGridView()
		{
			var orderFromGridView = new Order();

			
			orderFromGridView.Creator = dataGridView2.Rows[0].Cells[1].Value.ToString();
			orderFromGridView.TaskName = dataGridView2.Rows[0].Cells[2].Value.ToString();
			orderFromGridView.TaskDescription = dataGridView2.Rows[0].Cells[3].Value.ToString();
			orderFromGridView.CreatedOn = DateTime.Parse(dataGridView2.Rows[0].Cells[4].Value.ToString());
			if (!(dataGridView2.Rows[0].Cells[5].Value == null))
			{
				orderFromGridView.Assignee = dataGridView2.Rows[0].Cells[5].Value.ToString();
			}
			else
			{
				orderFromGridView.Assignee = "";
			}
			if (!(dataGridView2.Rows[0].Cells[6].Value == null))
			{
				orderFromGridView.Status = dataGridView2.Rows[0].Cells[6].Value.ToString();
			}
			else
			{
				orderFromGridView.Status = "New";
			}
			if (!(dataGridView2.Rows[0].Cells[7].Value == null))
			{
				orderFromGridView.Note = dataGridView2.Rows[0].Cells[7].Value.ToString();
			}
			else
			{
				orderFromGridView.Note = "";
			}



			if (dataGridView2.Rows[0].Cells[8].Value.ToString() == "True")
			{
				orderFromGridView.LockStatus = true;
			}
			else
			{
				orderFromGridView.LockStatus = false;
			}



			return orderFromGridView;
		}

		private bool AddNewDemandIntoDB(Order demandToSave)
		{
			
			var dbContext = new DemandTrackerDBModelNew();
			demandToSave.LockStatus = false;
			try
			{
				dbContext.Orders.Add(demandToSave);
				dbContext.SaveChanges();
				
				return true;
			}
			catch (Exception ex)
			{

				ShowStatusForm2("Error during save record: " + ex.Message);
			}
			return false;
		}

		private bool ModifyDemandInDB(Order demandToModify)
		{
			var dbContext = new DemandTrackerDBModelNew();
			var demandInDB = (from item in dbContext.Orders where item.Id == currentIndex select item).SingleOrDefault();

			
			int index = 0;
			foreach (var item in demandInDB.GetType().GetProperties())
			{

				object objValue = demandToModify.GetType().GetProperty(item.Name).GetValue(demandToModify, null);
				demandInDB.GetType().GetProperty(item.Name).SetValue(demandInDB, objValue);
				++index;
			}
			
			demandInDB.LockStatus = true;
			ShowDBRecordInGridView(demandInDB);			

			try
			{
				dbContext.SaveChanges();
				
				return true;
			}
			catch (Exception ex)
			{

				ShowStatusForm2("Error during modify record: " + ex.Message);
			}
			return false;
		}


		private void ShowDBRecordInGridView(Order orderToShowInGridView)
		{
			dataGridView2.RowCount = 1;
			dataGridView2.ReadOnly = false;
			

			richTextBox1.Text = "";

			int index = 0;
			foreach (var item in orderToShowInGridView.GetType().GetProperties())
			{

				object objValue = orderToShowInGridView.GetType().GetProperty(item.Name).GetValue(orderToShowInGridView, null);
				if (objValue == null)
				{
					objValue = "";
				}
				richTextBox1.Text += "PropertyName:" + item.Name + " - "
					+ "Value:" + objValue.ToString() + "\n";
				dataGridView2.Rows[0].Cells[index].Value = objValue;
				++index;
			}
			//dataGridView2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
		}

	}
}
