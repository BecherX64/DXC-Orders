using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemandTrackerForm
{
	public partial class Form1 : Form
	{

		private Order currentDemandItem = new Order();

		public Form1()
		{
			
			InitializeComponent();
			button1.Location = new Point(12,12);
			
			dataGridView1.Location = new Point(button1.Location.X, button1.Location.Y + 2*12);
			label1.Location = new Point(button1.Location.X, dataGridView1.Height);
			button2.Enabled = false;
			button3.Enabled = false;
			DataGridViewCellStyle headerStyle = dataGridView1.ColumnHeadersDefaultCellStyle;
			headerStyle.BackColor = Color.Navy;
			headerStyle.ForeColor = Color.White;
			headerStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
			ResizeForm1Objects();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			LoadDataFromDB();
		}

		private void LoadDataFromDB()
		{
			var dbContext = new DemandTrackerDBModelNew();
			//TODO - replace Load method using DataTables

			//load data
			try
			{
				var myGridSource = from db in dbContext.Orders select db;

				DataTable myDataTable = new DataTable();


				//Add columns into GridView
				var newColumn = myGridSource.FirstOrDefault();
				int i = 1;
				foreach (var item in newColumn.GetType().GetProperties())
				{
					DataColumn dataColumnToAdd = new DataColumn(item.Name);
					myDataTable.Columns.Add(dataColumnToAdd);
					i++;
				}

				//Add data into GridView
				foreach (var currentRow in myGridSource)
				{
					object[] newRow = new object[currentRow.GetType().GetProperties().Count()];
					int index = 0;
					foreach (var item in currentRow.GetType().GetProperties())
					{
						newRow[index] = currentRow.GetType().GetProperty(item.Name).GetValue(currentRow, null);
						index++;
					}
					myDataTable.LoadDataRow(newRow, true);
					showStatusForm1("New Row Added:" + newRow);
				}

				dataGridView1.DataSource = myDataTable;

				dataGridView1.ReadOnly = true;
				
				showStatusForm1(dataGridView1.RowCount.ToString() + " row(s) loaded from DB.");
				button2.Enabled = true;
				button3.Enabled = true;
				this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
			}
			catch (Exception ex)
			{

				showStatusForm1("Unlable to load data from DB. Error message:" + ex.Message);
			}
			

			
			//Old way
			/*
			try
			{
				showStatusForm1("Trying load data from DB ...");
				var queryDB = from db in dbContext.Orders select db;
				dataGridView1.DataSource = queryDB.ToList();
				showStatusForm1(queryDB.Count().ToString() + ": items successfully loaded.");
				button2.Enabled = true;
				button3.Enabled = true;

			}
			catch (Exception ex)
			{

				showStatusForm1("Unable to load data: " + ex.Message);
			}
			*/
		}

		private void Form1_SizeChanged(object sender, EventArgs e)
		{
			ResizeForm1Objects();
		}
		private void ResizeForm1Objects()
		{

			var formSize = this.Size;
			
			dataGridView1.Size = new Size(formSize.Width - (3*button1.Location.X),formSize.Height - button1.Location.Y - (8*label1.Height));
			label1.Location = new Point(button1.Location.X, dataGridView1.Height + 4*label1.Height );
			button4.Location = new Point(button1.Location.X + dataGridView1.Width - button4.Width , button1.Location.Y);

			if (formSize.Width < 5 * button1.Width)
			{
				this.ClientSize = new Size(5 * button1.Width, formSize.Height);
			}
			
			/*
			showStatusForm1( "Button4:" + button4.Location.ToString() + " - " + 
				"Form:" + formSize.ToString() + " - " + 
				"DataGrid:" + dataGridView1.Size.ToString());
			*/

		}

		//Modify Selected Record
		private void button2_Click(object sender, EventArgs e)
		{
			this.updateRecord();
			
		}

		private void f_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.IsAccessible = true;
			this.Show();
			LoadDataFromDB();
			
		}

		//Add new record
		private void button3_Click(object sender, EventArgs e)
		{
			this.AddRecord();

		}

		private void AddRecord()
		{
			this.Hide();
			//this.IsAccessible = false;
			Form2 form2 = new Form2();
			form2.Show();
			form2.FormClosed += f_FormClosed;
		}
		
		private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			showStatusForm1("Selected Row: " + ((int)dataGridView1.CurrentCell.RowIndex + 1).ToString());
			
		}

		private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (button2.Enabled)
			{
				this.updateRecord();
			}
		}

		private void updateRecord()
		{
			//int currentId = (int)dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();

			int currentId = Int32.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString());

			var dbContext = new DemandTrackerDBModelNew();
			try
			{
				var selectedDemandRecord = (from db in dbContext.Orders where db.Id == currentId select db).SingleOrDefault();

				//showStatusForm1(selectedDemandRecord.LockStatus.Value.ToString());

				if (!(selectedDemandRecord.LockStatus.Value))
				{
					selectedDemandRecord.LockStatus = true;
					try
					{
						dbContext.SaveChanges();
					}
					catch (Exception ex2)
					{

						showStatusForm1("Unable to Lock row:" + dataGridView1.CurrentCell.RowIndex + " in DB. Error:" + ex2.Message);
					}
					this.Hide();
					Form2 form2 = new Form2(selectedDemandRecord);
					form2.Show();
					form2.FormClosed += f_FormClosed;

				}
				else
				{
					showStatusForm1("Row:" + (dataGridView1.CurrentCell.RowIndex + 1) + " is locked by another user or process.");
				}
			}
			catch (Exception ex1)
			{
				showStatusForm1("Error while loading selected row from DB to modify:" + ex1.Message);
			}

		}



		private void showStatusForm1(string statusFrom1)
		{
			label1.Text = statusFrom1;
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void button4_Click(object sender, EventArgs e)
		{
			//close Form1
			this.Close();
		}
	}


}
