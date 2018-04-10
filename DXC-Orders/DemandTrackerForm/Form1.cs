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
			ResizeForm1Objects();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			
			var dbContext = new DemandTrackerDBModel();
			var queryDB = from db in dbContext.Orders select db;
			try
			{
				dataGridView1.DataSource = queryDB.ToList();
				showStatusForm1(queryDB.Count().ToString() + ": items successfully loaded from DataSource: " 
					+ dataGridView1.DataSource.ToString() + 
					"Rows count:" + dataGridView1.Rows.Count );
				button2.Enabled = true;
				button3.Enabled = true;

			}
			catch (Exception ex)
			{

				showStatusForm1("Something went wrong: " + ex.Message);
			}
			
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
			 showStatusForm1( "Button:" + button1.Location.ToString() + " - " + 
				"Form:" + formSize.ToString() + " - " + 
				"DataGrid:" + dataGridView1.Size.ToString());
			
		}

		private void button2_Click(object sender, EventArgs e)
		{
			updateRecord();
		}

		private void f_FormClosed(object sender, FormClosedEventArgs e)
		{
			//throw new NotImplementedException();
			this.IsAccessible = true;
			this.Show();
		}

		private void button3_Click(object sender, EventArgs e)
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
			updateRecord();
		}

		private void updateRecord()
		{
			int count = dataGridView1.RowCount;
			int rowindex = dataGridView1.CurrentCell.RowIndex;
			int columnindex = dataGridView1.CurrentCell.ColumnIndex;
			var selectedRow = dataGridView1.Rows[rowindex];

			currentDemandItem.Id = (int)dataGridView1.Rows[rowindex].Cells[0].Value;
			currentDemandItem.Creator = dataGridView1.Rows[rowindex].Cells[1].Value.ToString();
			currentDemandItem.TaskName = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
			currentDemandItem.TaskDescription = dataGridView1.Rows[rowindex].Cells[3].Value.ToString();
			currentDemandItem.CreatedOn = DateTime.Parse(dataGridView1.Rows[rowindex].Cells[4].Value.ToString());
			if (!(dataGridView1.Rows[rowindex].Cells[5].Value == null))
			{
				currentDemandItem.Assignee = dataGridView1.Rows[rowindex].Cells[5].Value.ToString();
			}
			else
			{
				currentDemandItem.Assignee = "";
			}


			if (!(dataGridView1.Rows[rowindex].Cells[6].Value == null))
			{
				currentDemandItem.Status = dataGridView1.Rows[rowindex].Cells[6].Value.ToString();
			}
			else
			{
				currentDemandItem.Status = "";
			}
			if (!(dataGridView1.Rows[rowindex].Cells[7].Value == null))
			{
				currentDemandItem.Note = dataGridView1.Rows[rowindex].Cells[7].Value.ToString();
			}
			else
			{
				currentDemandItem.Note = "";
			}

			
			//label1.Text = dataRows.ToString();
			this.Hide();
			//this.IsAccessible = false;
			Form2 form2 = new Form2(currentDemandItem);
			//Form2 form2 = new Form2(selectedRow);
			form2.Show();
			form2.FormClosed += f_FormClosed;
		}

		private void showStatusForm1(string statusFrom1)
		{
			label1.Text = statusFrom1;
		}
	}


}
