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
		private string[] header = new string[] { "Id", "Creator", "TaskName", "TaskDescription", "CreatedOn", "Assignee", "Status", "Note" };
		//private Order currentDemandItemForm2 = new Order();

		public Form2()
		{
			
			
			InitializeComponent();
			var createdOn = DateTime.Now;
			
			button1.Enabled = false;
			button3.Enabled = true;
			button4.Enabled = false;
			dataGridView2.RowCount = 1;
			dataGridView2.ColumnCount = 8;
			dataGridView2.ReadOnly = false;
			for (int i = 0; i < header.Length; i++)
			{

				dataGridView2.Columns[i].Name = header[i];
				if (i == 0 || i == 4)
				{
					dataGridView2.Rows[0].Cells[i].ReadOnly = true;
				}
			}
			dataGridView2.Rows[0].Cells[4].Value = createdOn;
		}

		public Form2(DataGridViewRow selectedRow)
			: this()
		{
			button3.Enabled = true;
			button4.Enabled = true;
			dataGridView2.RowCount = 1;
			dataGridView2.ColumnCount = selectedRow.Cells.Count;
			dataGridView2.ReadOnly = false;
			

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

				if (i == 0 || i == 4)
				{
					dataGridView2.Rows[0].Cells[i].ReadOnly = true;
				}
			}
		}

		public Form2(Order currentDemandItem)
			: this()
		{
			
			button1.Enabled = true;
			button3.Enabled = false;
			button4.Enabled = false;

			ShowDBRecordInGridView(currentDemandItem);
		}

		private void button1_Click(object sender, EventArgs e)
		{

			//todo Modify Current Record
			//create new method - Get Order from GridView ex: private Order GetDemandFromGridView()
			ShowStatusForm2("Button 1 - Clicked");
		}

		private void button2_Click(object sender, EventArgs e)
		{
			//this.IsAccessible = false;
			//Form2 form2 = new Form2();
			//form2.Show();
			this.Close();
			
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
			dataGridView2.Size = new Size (formSize.Width - (3*button1.Location.X), dataGridView2.Size.Height);
			ShowStatusForm2("FormSize:" + formSize.ToString() + " - DataGridSize:" + dataGridView2.Size.ToString());
		}

		private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			ShowStatusForm2("Current Cell Edited:" +dataGridView2.CurrentCell.ColumnIndex.ToString());
			
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (UpdateDBRecord())
			{
				ShowStatusForm2("DB record updated successfuly");
			}

		}

		private bool UpdateDBRecord()
		{
			//rename method to private bool GetGridViewFromDemand(Order DemandToShow)
			//save order class into DB as separate method private bool SaveDemandIntoDB(Order DemandToSave)


			var dbContext = new DemandTrackerDBModel();
			int maxId = (from db in dbContext.Orders select db.Id).Max();
			var demandToSave = new Order();
			
			demandToSave.Id = maxId+1;
			demandToSave.Creator = dataGridView2.Rows[0].Cells[1].Value.ToString();
			demandToSave.TaskName = dataGridView2.Rows[0].Cells[2].Value.ToString();
			demandToSave.TaskDescription = dataGridView2.Rows[0].Cells[3].Value.ToString();
			demandToSave.CreatedOn = DateTime.Parse(dataGridView2.Rows[0].Cells[4].Value.ToString());
			if (!(dataGridView2.Rows[0].Cells[5].Value == null))
			{
				demandToSave.Assignee = dataGridView2.Rows[0].Cells[5].Value.ToString();
			}
			else
			{
				demandToSave.Assignee = "";
			}
			if (!(dataGridView2.Rows[0].Cells[6].Value == null))
			{
				demandToSave.Status = dataGridView2.Rows[0].Cells[6].Value.ToString();
			}
			else
			{
				demandToSave.Status = "New";
			}
			if (!(dataGridView2.Rows[0].Cells[7].Value == null))
			{
				demandToSave.Note = dataGridView2.Rows[0].Cells[7].Value.ToString();
			}
			else
			{
				demandToSave.Note = "";
			}

			ShowDBRecordInGridView(demandToSave);
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


		private void ShowDBRecordInGridView(Order orderToShowInGridView)
		{
			dataGridView2.RowCount = 1;

			dataGridView2.ReadOnly = false;

			int index = 0;
			foreach (var item in orderToShowInGridView.GetType().GetProperties())
			{

				object objValue = orderToShowInGridView.GetType().GetProperty(item.Name).GetValue(orderToShowInGridView, null);
				richTextBox1.Text += "PropertyName:" + item.Name + " - "
					+ "Value:" + objValue.ToString() + "\n";
				dataGridView2.Rows[0].Cells[index].Value = objValue;
				++index;
			}
		}

	}
}
