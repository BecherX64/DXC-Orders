using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemandTrackerv1._1
{
	public partial class PrimaryForm1 : Form
	{
		
		public PrimaryForm1()
		{
			InitializeComponent();
			this.MinimumSize = this.Size;
			this.dgwPrimary.AllowUserToAddRows = false;
			this.dgwPrimary.AllowUserToDeleteRows = false;
			this.dgwPrimary.AllowUserToResizeRows = false;
			this.dgwPrimary.AllowUserToOrderColumns = false;

			this.dgwSecondary.AllowUserToAddRows = false;
			this.dgwSecondary.AllowUserToDeleteRows = false;
			this.dgwSecondary.AllowUserToResizeRows = false;
			this.dgwSecondary.AllowUserToOrderColumns = false;
			this.dgwSecondary.Visible = false;

			this.dgwAddRecord.AllowUserToAddRows = false;
			this.dgwAddRecord.AllowUserToDeleteRows = false;
			this.dgwAddRecord.AllowUserToResizeRows = false;
			this.dgwAddRecord.AllowUserToOrderColumns = false;
			this.dgwAddRecord.Visible = false;


			this.dgwPrimary.ReadOnly = true;
			this.dgwPrimary.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

			btnAddRecord.Enabled = false;
			btnModifyRecord.Enabled = false;

			btnSaveRecordToDB.Enabled = false;
			btnSaveRecordToDB.Visible = false;
			btnCancelModify.Enabled = false;
			btnCancelModify.Visible = false;
			btnReloadData.Enabled = false;
			btnReloadData.Visible = false;

			btnAddNewRecord.Enabled = false;
			btnAddNewRecord.Visible = false;
			btnCancelAddNewRecord.Enabled = false;
			btnCancelAddNewRecord.Visible = false;

			this.label1.Text = "";
			this.label2.Text = "";

			

			this.FormClosing += PrimaryForm1_FormClosing;
		}

		private void PrimaryForm1_FormClosing(object sender, FormClosingEventArgs e)
		{
			//TODO - Add some checks if record in dgwSecondary exists to avoid execeptions
			clearSecondaryGrid();
			//clearAddRecordGrid();
		}

		private void btnLoadData_Click(object sender, EventArgs e)
		{

			if (loadData())
			{
				btnAddRecord.Enabled = true;
				btnModifyRecord.Enabled = true;
				dgwPrimarySizeAllColumns();
			}
		}

		private void showStatusOnForm1(string status)
		{
			label1.Text = status;
		}

		private void showStatusGdwSecondary(string status)
		{
			label2.Text = status;
		}

		private void addTextToRichBox(string textToAdd)
		{
			richTextBox1.Text += textToAdd;
		}

		private void addTextToRichBox()
		{
			richTextBox1.Text = "";
		}


		private void dgwPrimary_MouseClick(object sender, MouseEventArgs e)
		{
			showStatusOnForm1("Cell cliked:" + dgwPrimary.CurrentCell.RowIndex + "x" + dgwPrimary.CurrentCell.ColumnIndex);
		}

		private void btnModifyRecord_Click(object sender, EventArgs e)
		{

			int currentRowIndex = dgwPrimary.CurrentCell.RowIndex+1;
			var dbContext = new DXCOrdersDBEntities();

			if (modifyRecord(lockRecordInDB(dbContext, currentRowIndex), currentRowIndex))
			{
				btnAddRecord.Enabled = false;
				btnLoadData.Enabled = false;
				btnModifyRecord.Enabled = false;

				btnSaveRecordToDB.Enabled = true;
				btnSaveRecordToDB.Visible = true;
				btnCancelModify.Enabled = true;
				btnCancelModify.Visible = true;
				btnReloadData.Enabled = true;
				btnReloadData.Visible = true;

				dgwSecondary.Visible = true;
				dgwSecondarySizeAllColumns();
			}
			else
			{
				//showStatusGdwSecondary("Unable to read current Row or it is currently locked.");
			}

			/*
			if (modifyRecordNoDataTable(dgwPrimary.CurrentCell.RowIndex))
			{
				//TODO
				btnAddRecord.Enabled = false;
				btnLoadData.Enabled = false;
				btnModifyRecord.Enabled = false;
				btnCancelModify.Enabled = true;
				btnReloadData.Enabled = true;
				dgwSecondarySizeAllColumns();
			}
			*/

			/*
			if (modifyRecord(dgwPrimary.CurrentCell.RowIndex))
			{
				//TODO
				//btnSaveRecordToDB.Enabled = true;

				//ComboBox Events
				this.dgwSecondary.CellBeginEdit += new DataGridViewCellCancelEventHandler(dgwSecondary_CellBeginEdit);
				this.dgwSecondary.CellEndEdit += new DataGridViewCellEventHandler(dgwSecondary_CellEndEdit);

				//change source
				this.comboBox1.Items.Clear();
				this.comboBox1.Items.Add("Item1");
				this.comboBox1.Items.Add("item2");
				this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
				//this.comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			}
			*/
			
		}

		private bool modifyRecord(Order order, int currentCellRowIndex)
		{


			if (order != null)
			{
				string[] statusCBCell = readstatusCBCell();

				if (statusCBCell != null)
				{
					try
					{
						
						DataGridViewRow rowToAdd = new DataGridViewRow();

						DataGridViewCellStyle headerStyle = dgwSecondary.ColumnHeadersDefaultCellStyle;
						headerStyle.BackColor = Color.Navy;
						headerStyle.ForeColor = Color.White;
						headerStyle.Font = new Font(dgwSecondary.Font, FontStyle.Bold);

						dgwSecondary.ColumnCount = order.GetType().GetProperties().Count();

						dgwSecondary.ReadOnly = false;

						int index = 0;

						foreach (var sqlItem in order.GetType().GetProperties())
						{
							object objValue = order.GetType().GetProperty(sqlItem.Name).GetValue(order, null);
							dgwSecondary.Columns[index].Name = sqlItem.Name.ToString();
							switch (index)
							{
								case 0:
								case 1:
								case 2:
								case 3:
								case 4:
								case 5:
								case 7:
								case 8:
									DataGridViewTextBoxCell textCell = new DataGridViewTextBoxCell();
									textCell.Value = objValue;
									rowToAdd.Cells.Add(textCell);
									break;
								case 6:
									DataGridViewComboBoxCell cbCell = new DataGridViewComboBoxCell();
									cbCell.Items.AddRange(statusCBCell);
									cbCell.Value = objValue;
									rowToAdd.Cells.Add(cbCell);
									break;
								default:
									break;
							}
							++index;

						}

						dgwSecondary.Rows.Add(rowToAdd);

						dgwSecondary.Rows[0].Cells[0].ReadOnly = true;
						dgwSecondary.Rows[0].Cells[4].ReadOnly = true;
						dgwSecondary.Rows[0].Cells[8].ReadOnly = true;

						showStatusOnForm1("Modify Row:" + currentCellRowIndex);
						showStatusGdwSecondary("Mode: Modify Record");
						addTextToRichBox("Modify No DataTable - In progress ..." + "\n");

						return true;

					}
					catch (Exception ex)
					{

						addTextToRichBox("Error reading current Row from DB:" + ex.Message + "\n");
						return false;
					}
				}
				else
				{
					addTextToRichBox("Read DropDownList failed");
					return false;
				}

			}
			else
			{
				return false;
			}



		}

		private bool modifyRecord(int itemIndex)
		{
			//TODO
			

			DataTable myDataTable = new DataTable();
			var dbContext = new DXCOrdersDBEntities();

			var sqlData = from db in dbContext.Orders where db.Id == itemIndex+1 select db;
			//Add columns into GridView
			var newColumn = sqlData.FirstOrDefault();
			int i = 1;
			foreach (var item in newColumn.GetType().GetProperties())
			{
				DataColumn dataColumnToAdd = new DataColumn(item.Name);
				myDataTable.Columns.Add(dataColumnToAdd);
				i++;
			}

			//Disable Sort on Columns
			foreach (DataGridViewColumn column in dgwSecondary.Columns)
			{
				column.SortMode = DataGridViewColumnSortMode.NotSortable;
			}

			

			//Add data into GridView
			foreach (var currentRow in sqlData)
			{
				object[] newRow = new object[currentRow.GetType().GetProperties().Count()];
				int index = 0;
				foreach (var item in currentRow.GetType().GetProperties())
				{
					newRow[index] = currentRow.GetType().GetProperty(item.Name).GetValue(currentRow, null);
					index++;
				}
				myDataTable.LoadDataRow(newRow, true);
			}
			
			dgwSecondary.DataSource = myDataTable;

			dgwSecondary.Rows[0].Cells[0].ReadOnly = true;
			dgwSecondary.Rows[0].Cells[4].ReadOnly = true;
			dgwSecondary.Rows[0].Cells[8].ReadOnly = true;

			showStatusOnForm1("Modify Row:" + itemIndex);
			showStatusGdwSecondary("Mode: Modify Record");
			dgwSecondary.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
			return true;
		}

		private bool modifyRecordNoDataTable(int itemIndex)
		{

			string[] statusCBCell = readstatusCBCell();

			if (statusCBCell != null)
			{
				var dbContext = new DXCOrdersDBEntities();

				try
				{
					var sqlData = (from db in dbContext.Orders where db.Id == itemIndex + 1 select db).SingleOrDefault();

					

					if (sqlData.LockStatus == true)
					{
						addTextToRichBox("Status on row: " + itemIndex + " - value:" + sqlData.Status.ToString());
						return false;
					}
					else
					{

						//Add data as GridRow - TO DO
						DataGridViewRow rowToAdd = new DataGridViewRow();

						DataGridViewCellStyle headerStyle = dgwSecondary.ColumnHeadersDefaultCellStyle;
						headerStyle.BackColor = Color.Navy;
						headerStyle.ForeColor = Color.White;
						headerStyle.Font = new Font(dgwSecondary.Font, FontStyle.Bold);

						dgwSecondary.ColumnCount = sqlData.GetType().GetProperties().Count();

						dgwSecondary.ReadOnly = false;

						int index = 0;
						foreach (var sqlItem in sqlData.GetType().GetProperties())
						{
							object objValue = sqlData.GetType().GetProperty(sqlItem.Name).GetValue(sqlData, null);
							dgwSecondary.Columns[index].Name = sqlItem.Name.ToString();
							switch (index)
							{
								case 0:
								case 1:
								case 2:
								case 3:
								case 4:
								case 5:
								case 7:
								case 8:
									DataGridViewTextBoxCell textCell = new DataGridViewTextBoxCell();
									textCell.Value = objValue;
									rowToAdd.Cells.Add(textCell);
									break;
								case 6:
									DataGridViewComboBoxCell cbCell = new DataGridViewComboBoxCell();
									cbCell.Items.AddRange(statusCBCell);
									cbCell.Value = objValue;
									rowToAdd.Cells.Add(cbCell);
									break;
								default:
									break;
							}
							++index;

						}

						dgwSecondary.Rows.Add(rowToAdd);

						dgwSecondary.Rows[0].Cells[0].ReadOnly = true;
						dgwSecondary.Rows[0].Cells[4].ReadOnly = true;
						dgwSecondary.Rows[0].Cells[8].ReadOnly = true;

						showStatusOnForm1("Modify Row:" + itemIndex);
						showStatusGdwSecondary("Mode: Modify Record");
						addTextToRichBox("Modify No DataTable - In progress ..." + "\n");

						return true;
					}
				}
				catch (Exception ex)
				{

					addTextToRichBox("Error reading current Row from DB:" + ex.Message + "\n");
					return false;
				}
			}
			else
			{
				addTextToRichBox("Read DropDownList failed");
				return false;
			}
			
		}

		private void switchStatusOnRecord(Order sqlData, bool newStatus)
		{
			//TODO
			if (sqlData.LockStatus == true)
			{
				//TODO
			}
		}

		private Order lockRecordInDB(DXCOrdersDBEntities dbContext, int itemIndex)
		{
			var sqlData = new Order();
			var sqlDataTemp = (from db in dbContext.Orders where db.Id == itemIndex select db).SingleOrDefault();

			if (sqlDataTemp.LockStatus == false)
			{
				sqlDataTemp.LockStatus = true;
				dbContext.SaveChanges();

				foreach (var item in sqlDataTemp.GetType().GetProperties())
				{
					sqlData.GetType().GetProperty(item.Name).SetValue(sqlData, sqlDataTemp.GetType().GetProperty(item.Name).GetValue(sqlDataTemp, null));

				}
				return sqlData;
			}
			else
			{
				showStatusGdwSecondary("Row Locked !!!");
				return null;
			}
		}

		private bool unlockRecordInDB(DXCOrdersDBEntities dbContext, int currentIdIndex)
		{
			//TODO
			try
			{
				var sqlData = (from db in dbContext.Orders where db.Id == currentIdIndex select db).SingleOrDefault();
				sqlData.LockStatus = false;
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				addTextToRichBox("Unlock Row error:" + ex.Message);
				return false;
			}
			
		}


		private bool loadData()
		{
			DataTable myDataTable = new DataTable();
			var dbContext = new DXCOrdersDBEntities();

			var sqlData = from db in dbContext.Orders select db;
			//Add columns into GridView
			var newColumn = sqlData.FirstOrDefault();
			int i = 1;
			foreach (var item in newColumn.GetType().GetProperties())
			{
				DataColumn dataColumnToAdd = new DataColumn(item.Name);
				myDataTable.Columns.Add(dataColumnToAdd);
				i++;
			}

			//Add data into GridView
			foreach (var currentRow in sqlData)
			{
				object[] newRow = new object[currentRow.GetType().GetProperties().Count()];
				int index = 0;
				foreach (var item in currentRow.GetType().GetProperties())
				{
					newRow[index] = currentRow.GetType().GetProperty(item.Name).GetValue(currentRow, null);
					index++;
				}
				myDataTable.LoadDataRow(newRow, true);
				showStatusOnForm1("New Row Added:" + newRow);
			}

			dgwPrimary.DataSource = myDataTable;

			return true;
		}

		private string[] readstatusCBCell()
		{
			var statusContext = new DXCOrdersDBEntities();
			//TODO implement Try {}
			try
			{
				var statusCB = from db in statusContext.OrdersInfoes where db.dropdownmenu != null select db.dropdownmenu;
				int index = 0;
				string[] status = new string[statusCB.Count()];


				foreach (var item in statusCB)
				{
					if (item != null)
					{
						status[index] = item.ToString();
						index++;
					}
				}
				return status;
			}
			catch (Exception ex)
			{
				addTextToRichBox("Error reading \"status\" list from DB: " + ex.Message + "\n");
				return null;
			}
		}

		private void dgwSecondary_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			
			showStatusOnForm1("Cell changed:" + dgwSecondary.CurrentCell.ColumnIndex);

			//To Consider
			if (dgwSecondary.CurrentCell.Value == null)
			{
				dgwSecondary.CurrentCell.Value = "";
				addTextToRichBox("Current Cell:" + dgwSecondary.CurrentCell.ToString() + " - NULL value changed" + "\n");
			}
			//if (checkDataConsistency())
			if (checkDataConsistencyAdvanced())
			{
				btnSaveRecordToDB.Enabled = true;
			}
			else
			{
				btnSaveRecordToDB.Enabled = false;
			}
			showStatusGdwSecondary("Do NOT forget to save record in to DB.");
			dgwSecondarySizeAllColumns();
		}

		private void dgwAddRecord_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			//TODO
			showStatusOnForm1("Cell changed:" + dgwAddRecord.CurrentCell.ColumnIndex);
			if (dgwAddRecord.CurrentCell.Value == null)
			{
				dgwAddRecord.CurrentCell.Value = "";
			}

			if (checkDataConsistencyOnGrid(dgwAddRecord))
			{
				btnAddNewRecord.Enabled = true;
			}
			else
			{
				btnAddNewRecord.Enabled = false;
			}
			showStatusGdwSecondary("Do NOT forget to Add record in to DB.");
			dgwAddRecordsSizeAllColumns();

		}

		private bool checkDataConsistencyOnGrid(DataGridView dataGridViewToCheck)
		{

			bool dataConsistencyStatus = true;
			//richTextBox1.Text = "";

			int index = 0;
			while (index < dataGridViewToCheck.Rows[0].Cells.Count - 1)
			{
				switch (index)
				{
					case 0:
						//Id
						addTextToRichBox(dataGridViewToCheck.Columns[index].HeaderText + ": skipped" + "\n");
						break;
					case 1:
						//Creator NVarChar 20
						if ((dataGridViewToCheck.Rows[0].Cells[index].Value == null) || (dataGridViewToCheck.Rows[0].Cells[index].Value.ToString() == ""))
						{
							addTextToRichBox(dataGridViewToCheck.Columns[index].HeaderText + ": skipped" + "\n");
							dataConsistencyStatus = false;
						}
						else
						{
							if (dataGridViewToCheck.Rows[0].Cells[index].Value.ToString().Length > 20)
							{
								dataConsistencyStatus = false;
								addTextToRichBox(dataGridViewToCheck.Columns[index].HeaderText + " - Value:" + dataGridViewToCheck.Rows[0].Cells[index].Value.ToString() + " - Size:" +
									dataGridViewToCheck.Rows[0].Cells[index].Value.ToString().Length + " = NOK" + "\n");
							}
							else
							{
								addTextToRichBox(dataGridViewToCheck.Columns[index].HeaderText + " - Value:" + dataGridViewToCheck.Rows[0].Cells[index].Value.ToString() + " - Size:" +
									dataGridViewToCheck.Rows[0].Cells[index].Value.ToString().Length + " = OK" + "\n");
							}
						}
						break;
					case 2:
						//TaksName NVarChar 20
						if ((dataGridViewToCheck.Rows[0].Cells[index].Value == null) || (dataGridViewToCheck.Rows[0].Cells[index].Value.ToString() == ""))
						{
							addTextToRichBox(dataGridViewToCheck.Columns[index].HeaderText + ": skipped" + "\n");
							dataConsistencyStatus = false;
						}
						else
						{
							if (dataGridViewToCheck.Rows[0].Cells[index].Value.ToString().Length > 20)
							{
								dataConsistencyStatus = false;
								addTextToRichBox(dataGridViewToCheck.Columns[index].HeaderText + " - Value:" + dataGridViewToCheck.Rows[0].Cells[index].Value.ToString() + " - Size:" +
									dataGridViewToCheck.Rows[0].Cells[index].Value.ToString().Length + " = NOK" + "\n");
							}
							else
							{
								addTextToRichBox(dataGridViewToCheck.Columns[index].HeaderText + " - Value:" + dataGridViewToCheck.Rows[0].Cells[index].Value.ToString() + " - Size:" +
									dataGridViewToCheck.Rows[0].Cells[index].Value.ToString().Length + " = OK" + "\n");
							}
						}
						break;
					case 3:
						//TaksDescription NVarChar(MAX)
						if ((dataGridViewToCheck.Rows[0].Cells[index].Value == null) || (dataGridViewToCheck.Rows[0].Cells[index].Value.ToString() == ""))
						{
							addTextToRichBox(dataGridViewToCheck.Columns[index].HeaderText + ": skipped" + "\n");
							dataConsistencyStatus = false;
						}
						else
						{
							if (dataGridViewToCheck.Rows[0].Cells[index].Value.ToString().Length > 255)
							{
								dataConsistencyStatus = false;
								addTextToRichBox(dataGridViewToCheck.Columns[index].HeaderText + " - Value:" + dataGridViewToCheck.Rows[0].Cells[index].Value.ToString() + " - Size:" +
									dataGridViewToCheck.Rows[0].Cells[index].Value.ToString().Length + " = NOK" + "\n");
							}
							else
							{
								addTextToRichBox(dataGridViewToCheck.Columns[index].HeaderText + " - Value:" + dataGridViewToCheck.Rows[0].Cells[index].Value.ToString() + " - Size:" +
									dataGridViewToCheck.Rows[0].Cells[index].Value.ToString().Length + " = OK" + "\n");
							}
						}
						break;
					case 4:
						//DateTime
						addTextToRichBox(dataGridViewToCheck.Columns[index].HeaderText + ": skipped" + "\n");
						break;
					case 5:
						//Assignee NVarChar 20
						if (dataGridViewToCheck.Rows[0].Cells[index].Value == null)
						{
							addTextToRichBox(dataGridViewToCheck.Columns[index].HeaderText + ": skipped" + "\n");
							//dataConsistencyStatus = false;
						}
						else
						{
							if (dataGridViewToCheck.Rows[0].Cells[index].Value == null)
							{
								addTextToRichBox(dataGridViewToCheck.Columns[index].HeaderText + ": skipped" + "\n");
							}
							else
							{
								if (dataGridViewToCheck.Rows[0].Cells[index].Value.ToString().Length > 20)
								{
									dataConsistencyStatus = false;
									addTextToRichBox(dataGridViewToCheck.Columns[index].HeaderText + " - Value:" + dataGridViewToCheck.Rows[0].Cells[index].Value.ToString() + " - Size:" +
										dataGridViewToCheck.Rows[0].Cells[index].Value.ToString().Length + " = NOK" + "\n");
								}
								else
								{
									addTextToRichBox(dataGridViewToCheck.Columns[index].HeaderText + " - Value:" + dataGridViewToCheck.Rows[0].Cells[index].Value.ToString() + " - Size:" +
										dataGridViewToCheck.Rows[0].Cells[index].Value.ToString().Length + " = OK" + "\n");
								}
							}
						}
						break;
					case 6:
						//Status NVarChar 15
						if (dataGridViewToCheck.Rows[0].Cells[index].Value.ToString().Length > 15)
						{
							dataConsistencyStatus = false;
							addTextToRichBox(dataGridViewToCheck.Columns[index].HeaderText + " - Value:" + dataGridViewToCheck.Rows[0].Cells[index].Value.ToString() + " - Size:" +
								dataGridViewToCheck.Rows[0].Cells[index].Value.ToString().Length + " = NOK" + "\n");
						}
						else
						{
							addTextToRichBox(dataGridViewToCheck.Columns[index].HeaderText + " - Value:" + dataGridViewToCheck.Rows[0].Cells[index].Value.ToString() + " - Size:" +
								dataGridViewToCheck.Rows[0].Cells[index].Value.ToString().Length + " = OK" + "\n");
						}
						break;
					case 7:
						//Note NVarChar MAX
						if (dataGridViewToCheck.Rows[0].Cells[index].Value == null)
						{
							addTextToRichBox(dataGridViewToCheck.Columns[index].HeaderText + ": skipped" + "\n");
						}
						else
						{
							if (dataGridViewToCheck.Rows[0].Cells[index].Value.ToString().Length > 255)
							{
								dataConsistencyStatus = false;
								addTextToRichBox(dataGridViewToCheck.Columns[index].HeaderText + " - Value:" + dataGridViewToCheck.Rows[0].Cells[index].Value.ToString() + " - Size:" +
									dataGridViewToCheck.Rows[0].Cells[index].Value.ToString().Length + " = NOK" + "\n");
							}
							else
							{
								addTextToRichBox(dataGridViewToCheck.Columns[index].HeaderText + " - Value:" + dataGridViewToCheck.Rows[0].Cells[index].Value.ToString() + " - Size:" +
									dataGridViewToCheck.Rows[0].Cells[index].Value.ToString().Length + " = OK" + "\n");
							}
						}
						break;
					case 8:
						//LockStatus
						addTextToRichBox(dataGridViewToCheck.Columns[index].HeaderText + ": skipped" + "\n");
						break;
					default:
						break;
				}
				index++;
			}
			return dataConsistencyStatus;

		}

		private bool checkDataConsistency()
		{

			bool dataConsistencyStatus = true;
			richTextBox1.Text = "";
			for (int i = 0; i < dgwSecondary.Rows[0].Cells.Count - 1; i++)
			{

				if (i == 5 || i == 7 || i == 8)
				{
					//do nothing
					addTextToRichBox(dgwSecondary.Columns[i].HeaderText + ": skipped" + "\n");

				}
				else
				{
					if ((dgwSecondary.Rows[0].Cells[i].Value == null) || (dgwSecondary.Rows[0].Cells[i].Value.ToString() == ""))
					{
						dataConsistencyStatus = false;
						addTextToRichBox(dgwSecondary.Columns[i].HeaderText + ": NOK" + " - Value:" + dgwSecondary.Rows[0].Cells[i].Value.ToString() + "\n");
					}
					else
					{
						//TODO
						addTextToRichBox(dgwSecondary.Columns[i].HeaderText + ": OK" + " - Value:" + dgwSecondary.Rows[0].Cells[i].Value.ToString() + "\n");
					}
				}
			}

			return dataConsistencyStatus;
		}

		private bool checkDataConsistencyAdvanced()
		{
			bool dataConsistencyStatus = true;
			//richTextBox1.Text = "";

			int index = 0;
			while (index < dgwSecondary.Rows[0].Cells.Count - 1)
			{
				switch (index)
				{
					case 0:
						//Id
						addTextToRichBox(dgwSecondary.Columns[index].HeaderText + ": skipped" + "\n");
						break;
					case 1:
						//Creator NVarChar 20
						if ((dgwSecondary.Rows[0].Cells[index].Value == null) || (dgwSecondary.Rows[0].Cells[index].Value.ToString() == ""))
						{
							addTextToRichBox(dgwSecondary.Columns[index].HeaderText + ": skipped" + "\n");
							dataConsistencyStatus = false;
						}
						else
						{
							if (dgwSecondary.Rows[0].Cells[index].Value.ToString().Length > 20)
							{
								dataConsistencyStatus = false;
								addTextToRichBox(dgwSecondary.Columns[index].HeaderText + " - Value:" + dgwSecondary.Rows[0].Cells[index].Value.ToString() + " - Size:" +
									dgwSecondary.Rows[0].Cells[index].Value.ToString().Length + " = NOK" + "\n");
							}
							else
							{
								addTextToRichBox(dgwSecondary.Columns[index].HeaderText + " - Value:" + dgwSecondary.Rows[0].Cells[index].Value.ToString() + " - Size:" +
									dgwSecondary.Rows[0].Cells[index].Value.ToString().Length + " = OK" + "\n");
							} 
						}
						break;
					case 2:
						//TaksName NVarChar 20
						if ((dgwSecondary.Rows[0].Cells[index].Value == null) || (dgwSecondary.Rows[0].Cells[index].Value.ToString() == ""))
						{
							addTextToRichBox(dgwSecondary.Columns[index].HeaderText + ": skipped" + "\n");
							dataConsistencyStatus = false;
						}
						else
						{
							if (dgwSecondary.Rows[0].Cells[index].Value.ToString().Length > 20)
							{
								dataConsistencyStatus = false;
								addTextToRichBox(dgwSecondary.Columns[index].HeaderText + " - Value:" + dgwSecondary.Rows[0].Cells[index].Value.ToString() + " - Size:" +
									dgwSecondary.Rows[0].Cells[index].Value.ToString().Length + " = NOK" + "\n");
							}
							else
							{
								addTextToRichBox(dgwSecondary.Columns[index].HeaderText + " - Value:" + dgwSecondary.Rows[0].Cells[index].Value.ToString() + " - Size:" +
									dgwSecondary.Rows[0].Cells[index].Value.ToString().Length + " = OK" + "\n");
							} 
						}
						break;
					case 3:
						//TaksDescription NVarChar(MAX)
						if ((dgwSecondary.Rows[0].Cells[index].Value == null)  || (dgwSecondary.Rows[0].Cells[index].Value.ToString() == ""))
						{
							addTextToRichBox(dgwSecondary.Columns[index].HeaderText + ": skipped" + "\n");
							dataConsistencyStatus = false;
						}
						else
						{
							if (dgwSecondary.Rows[0].Cells[index].Value.ToString().Length > 255)
							{
								dataConsistencyStatus = false;
								addTextToRichBox(dgwSecondary.Columns[index].HeaderText + " - Value:" + dgwSecondary.Rows[0].Cells[index].Value.ToString() + " - Size:" +
									dgwSecondary.Rows[0].Cells[index].Value.ToString().Length + " = NOK" + "\n");
							}
							else
							{
								addTextToRichBox(dgwSecondary.Columns[index].HeaderText + " - Value:" + dgwSecondary.Rows[0].Cells[index].Value.ToString() + " - Size:" +
									dgwSecondary.Rows[0].Cells[index].Value.ToString().Length + " = OK" + "\n");
							} 
						}
						break;
					case 4:
						//DateTime
						addTextToRichBox(dgwSecondary.Columns[index].HeaderText + ": skipped" + "\n");
						break;
					case 5:
						//Assignee NVarChar 20
						if (dgwSecondary.Rows[0].Cells[index].Value == null)
						{
							addTextToRichBox(dgwSecondary.Columns[index].HeaderText + ": skipped" + "\n");
							//dataConsistencyStatus = false;
						}
						else
						{
							if (dgwSecondary.Rows[0].Cells[index].Value == null)
							{
								addTextToRichBox(dgwSecondary.Columns[index].HeaderText + ": skipped" + "\n");
							}
							else
							{
								if (dgwSecondary.Rows[0].Cells[index].Value.ToString().Length > 20)
								{
									dataConsistencyStatus = false;
									addTextToRichBox(dgwSecondary.Columns[index].HeaderText + " - Value:" + dgwSecondary.Rows[0].Cells[index].Value.ToString() + " - Size:" +
										dgwSecondary.Rows[0].Cells[index].Value.ToString().Length + " = NOK" + "\n");
								}
								else
								{
									addTextToRichBox(dgwSecondary.Columns[index].HeaderText + " - Value:" + dgwSecondary.Rows[0].Cells[index].Value.ToString() + " - Size:" +
										dgwSecondary.Rows[0].Cells[index].Value.ToString().Length + " = OK" + "\n");
								}
							}	
						}
						break;
					case 6:
						//Status NVarChar 15
						if (dgwSecondary.Rows[0].Cells[index].Value.ToString().Length > 15)
						{
							dataConsistencyStatus = false;
							addTextToRichBox(dgwSecondary.Columns[index].HeaderText + " - Value:" + dgwSecondary.Rows[0].Cells[index].Value.ToString() + " - Size:" +
								dgwSecondary.Rows[0].Cells[index].Value.ToString().Length + " = NOK" + "\n");
						}
						else
						{
							addTextToRichBox(dgwSecondary.Columns[index].HeaderText + " - Value:" + dgwSecondary.Rows[0].Cells[index].Value.ToString() + " - Size:" +
								dgwSecondary.Rows[0].Cells[index].Value.ToString().Length + " = OK" + "\n");
						}
						break;
					case 7:
						//Note NVarChar MAX
						if (dgwSecondary.Rows[0].Cells[index].Value == null)
						{
							addTextToRichBox(dgwSecondary.Columns[index].HeaderText + ": skipped" + "\n");
						}
						else
						{
							if (dgwSecondary.Rows[0].Cells[index].Value.ToString().Length > 255)
							{
								dataConsistencyStatus = false;
								addTextToRichBox(dgwSecondary.Columns[index].HeaderText + " - Value:" + dgwSecondary.Rows[0].Cells[index].Value.ToString() + " - Size:" +
									dgwSecondary.Rows[0].Cells[index].Value.ToString().Length + " = NOK" + "\n");
							}
							else
							{
								addTextToRichBox(dgwSecondary.Columns[index].HeaderText + " - Value:" + dgwSecondary.Rows[0].Cells[index].Value.ToString() + " - Size:" +
									dgwSecondary.Rows[0].Cells[index].Value.ToString().Length + " = OK" + "\n");
							}	
						}
						break;
					case 8:
						//LockStatus
						addTextToRichBox(dgwSecondary.Columns[index].HeaderText + ": skipped" + "\n");
						break;
					default:
						break;
				}
				index++;
			}
			return dataConsistencyStatus;
		}

			private void btnSaveRecordToDB_Click(object sender, EventArgs e)
		{
			if (saveRecordToDB())
			{
				
				clearSecondaryGrid();
				showStatusOnForm1("Record Save into DB.");
				showStatusGdwSecondary("Record Save into DB.");
				//btnLoadData.Enabled = true;
				//btnAddRecord.Enabled = true;
				//btnModifyRecord.Enabled = true;
				btnSaveRecordToDB.Enabled = false;
				btnSaveRecordToDB.Visible = false;
				btnReloadData.Enabled = false;
				btnReloadData.Visible = false;
				btnCancelModify.Enabled = false;
				btnCancelModify.Visible = false;
				dgwSecondary.Visible = false;

				//TODO - Reload data from DB
				if (loadData())
				{
					btnLoadData.Enabled = true;
					btnAddRecord.Enabled = true;
					btnModifyRecord.Enabled = true;
					dgwPrimarySizeAllColumns();
				}

			}
		}

		private bool saveRecordToDB()
		{
			var dbContext = new DXCOrdersDBEntities();
			int idToModify = Int32.Parse(dgwSecondary.Rows[0].Cells[0].Value.ToString());
			var recordToSave = (from db in dbContext.Orders where db.Id == idToModify select db).FirstOrDefault();

			recordToSave.Creator = dgwSecondary.Rows[0].Cells[1].Value.ToString();
			recordToSave.TaskName = dgwSecondary.Rows[0].Cells[2].Value.ToString();
			recordToSave.TaskDescription = dgwSecondary.Rows[0].Cells[3].Value.ToString();
			recordToSave.CreatedOn = DateTime.Parse(dgwSecondary.Rows[0].Cells[4].Value.ToString());
			recordToSave.Status = dgwSecondary.Rows[0].Cells[6].Value.ToString();
			
			if (!(dgwSecondary.Rows[0].Cells[5].Value == null))
			{
				recordToSave.Assignee = dgwSecondary.Rows[0].Cells[5].Value.ToString();
			}
			else
			{
				recordToSave.Assignee = "";
			}

			if (!(dgwSecondary.Rows[0].Cells[7].Value == null))
			{
				recordToSave.Note = dgwSecondary.Rows[0].Cells[7].Value.ToString();
			}
			else
			{
				recordToSave.Note = "";
			}

			if (dgwSecondary.Rows[0].Cells[8].Value.ToString() == "True")
			{
				recordToSave.LockStatus = true;
			}
			else
			{
				recordToSave.LockStatus = false;
			}

			try
			{
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{

				showStatusOnForm1("Error during Save record:" + ex.Message);
				addTextToRichBox("Inner exc:" +	ex.InnerException + "\n");
				return false;
			}
		}

		private bool addNewRecordIntoDB(DataGridView dataGridViewToSave)
		{
			var dbContext = new DXCOrdersDBEntities();
			var recordToAddIntoDB = new Order();
			//TODO modify save using recordToAddIntoDB.GetType().GetProperties()
			recordToAddIntoDB.Creator = dataGridViewToSave.Rows[0].Cells[1].Value.ToString();
			recordToAddIntoDB.TaskName = dataGridViewToSave.Rows[0].Cells[2].Value.ToString();
			recordToAddIntoDB.TaskDescription = dataGridViewToSave.Rows[0].Cells[3].Value.ToString();
			recordToAddIntoDB.CreatedOn = DateTime.Parse(dataGridViewToSave.Rows[0].Cells[4].Value.ToString());
			recordToAddIntoDB.Status = dataGridViewToSave.Rows[0].Cells[6].Value.ToString();
			recordToAddIntoDB.Assignee = dataGridViewToSave.Rows[0].Cells[5].Value.ToString();
			recordToAddIntoDB.Note = dataGridViewToSave.Rows[0].Cells[7].Value.ToString();
			recordToAddIntoDB.LockStatus = false;

			
			try
			{
				dbContext.Orders.Add(recordToAddIntoDB);
				dbContext.SaveChanges();
				showStatusGdwSecondary("New Record successfully added.");
				return true;
			}
			catch (Exception ex)
			{
				showStatusOnForm1("Error during Add New Record:" + ex.Message);
				addTextToRichBox("Inner exc:" + ex.InnerException + "\n");
				return false;
			}

		}

		private void dgwSecondary_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
		{
			//TODO
			/*
			if (e.ColumnIndex == 6)
			{
				this.comboBox1.SelectedIndex = 0;

				dgwSecondary.CancelEdit();
				
				Point cbLocationNew = new Point();
				cbLocationNew = this.dgwSecondary.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
				cbLocationNew.Y += this.dgwSecondary.Location.Y;
				cbLocationNew.X += this.dgwSecondary.Location.X;
				this.comboBox1.Location = cbLocationNew;
				this.comboBox1.Size = this.dgwSecondary.CurrentCell.Size;
				this.comboBox1.Visible = true;
				//this.comboBox1.Show();
				
				addTextToRichBox("CB Location:" + comboBox1.Location.ToString() + "\n");
				addTextToRichBox("Cell Location:" + dgwSecondary.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location.ToString() + "\n");
				addTextToRichBox("CB Location New:" + cbLocationNew.ToString() + "\n");
			}
			*/
		}

		private void dgwSecondary_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			//TODO
			/*
			if (this.comboBox1.SelectedItem != null)

			{

				this.dgwSecondary.CurrentCell.Value = this.comboBox1.SelectedItem.ToString();
			
			}
			
			//this.comboBox1.Visible = false;
			*/
		}


		private void comboBox1_Leave(object sender, EventArgs e)
		{
			//this.comboBox1.Visible = false;
		}

		private void dgwSecondary_DataError(object sender, DataGridViewDataErrorEventArgs anError)
		{
			addTextToRichBox("Data Error:" + anError + "\n");
		}


		private void dgwAddRecord_DataError(object sender, DataGridViewDataErrorEventArgs anError)
		{
			addTextToRichBox("Data Error:" + anError + "\n");
		}

		private void btnCancelModify_Click(object sender, EventArgs e)
		{
			//TODO

			if (clearSecondaryGrid())
			{
				
				showStatusGdwSecondary("Modify Canceled");
				btnAddRecord.Enabled = true;
				btnLoadData.Enabled = true;
				btnModifyRecord.Enabled = true;

				btnSaveRecordToDB.Enabled = false;
				btnSaveRecordToDB.Visible = false;
				btnCancelModify.Enabled = false;
				btnCancelModify.Visible = false;
				btnReloadData.Enabled = false;
				btnReloadData.Visible = false;
				dgwSecondary.Visible = false;
			}
		}

		private bool clearSecondaryGrid()
		{
			var dbContext = new DXCOrdersDBEntities();

			try
			{
				int currentIdIndex = Int32.Parse(dgwSecondary.Rows[0].Cells[0].Value.ToString());
				unlockRecordInDB(dbContext, currentIdIndex);
				dgwSecondary.Rows.RemoveAt(0);
				return true;
			}
			catch (Exception)
			{
				return false;
			}

		}

		private void PrimaryForm1_SizeChanged(object sender, EventArgs e)
		{
			reziseObjectsOnForm();
		}

		private void reziseObjectsOnForm()
		{
			var primaryFormNewSize = this.Size;
			//addTextToRichBox(primaryFormNewSize.ToString());
			

			label1.Location = new Point(label1.Location.X, this.Size.Height - 5 * label1.Size.Height);
			richTextBox1.Location = new Point(richTextBox1.Location.X, label1.Location.Y - richTextBox1.Size.Height - label1.Size.Height);
			label2.Location = new Point(label2.Location.X, richTextBox1.Location.Y - label2.Size.Height);
			dgwSecondary.Location = new Point(dgwSecondary.Location.X, label2.Location.Y - dgwSecondary.Size.Height - label2.Size.Height);
			dgwAddRecord.Location = dgwSecondary.Location;
			btnSaveRecordToDB.Location = new Point(btnSaveRecordToDB.Location.X, dgwSecondary.Location.Y - btnSaveRecordToDB.Size.Height);
			btnAddNewRecord.Location = btnSaveRecordToDB.Location;
			btnReloadData.Location = new Point(btnReloadData.Location.X, btnSaveRecordToDB.Location.Y);
			btnCancelModify.Location = new Point(btnCancelModify.Location.X, btnSaveRecordToDB.Location.Y);
			btnCancelAddNewRecord.Location = btnCancelModify.Location;

			dgwPrimary.Size = new Size(primaryFormNewSize.Width - 3 * dgwPrimary.Location.X,
				btnSaveRecordToDB.Location.Y - 2 * btnSaveRecordToDB.Size.Height);
				//this.Size.Height - label1.Size.Height - richTextBox1.Size.Height - label2.Size.Height - dgwSecondary.Size.Height - btnSaveRecordToDB.Size.Height);
			dgwSecondary.Size = new Size(primaryFormNewSize.Width - 3 * dgwSecondary.Location.X, dgwSecondary.Size.Height);
			dgwAddRecord.Size = dgwSecondary.Size;

			showStatusOnForm1("Form1 Size:" + primaryFormNewSize.ToString() + " - Grid Size:" + dgwPrimary.Size.ToString() + " Btn Location:" + btnSaveRecordToDB.Location.ToString());

		}

		private void PrimaryForm1_Shown(object sender, EventArgs e)
		{
			reziseObjectsOnForm();
		}

		private void btnReloadData_Click(object sender, EventArgs e)
		{
			//TODO
			int currentRowIndex = Int32.Parse(dgwSecondary.Rows[0].Cells[0].Value.ToString());
			if (clearSecondaryGrid())
			{
				//int currentRowIndex = dgwPrimary.CurrentCell.RowIndex + 1;
				
				var dbContext = new DXCOrdersDBEntities();

				if (modifyRecord(lockRecordInDB(dbContext, currentRowIndex), currentRowIndex))
				{
					//TODO
					btnAddRecord.Enabled = false;
					btnLoadData.Enabled = false;
					btnModifyRecord.Enabled = false;
					btnCancelModify.Enabled = true;
					btnReloadData.Enabled = true;
					dgwSecondarySizeAllColumns();
				} 
			}
		}

		private void dgwPrimary_Paint(object sender, PaintEventArgs e)
		{
			dgwPrimarySizeAllColumns();
		}

		private void dgwPrimarySizeAllColumns()
		{
			dgwPrimary.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
		}

		private void dgwSecondarySizeAllColumns()
		{
			dgwSecondary.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
		}

		private void dgwAddRecordsSizeAllColumns()
		{
			dgwAddRecord.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
		}

		private void btnAddRecord_Click(object sender, EventArgs e)
		{
			//TODO
			if (AddRecord())
			{
				btnAddRecord.Enabled = false;
				btnLoadData.Enabled = false;
				btnModifyRecord.Enabled = false;

				btnAddNewRecord.Enabled = false;
				btnAddNewRecord.Visible = true;
				btnCancelAddNewRecord.Enabled = true;
				btnCancelAddNewRecord.Visible = true;

				dgwAddRecord.Visible = true;
				dgwAddRecord.Enabled = true;
				dgwAddRecordsSizeAllColumns(); 
			}


		}

		private bool AddRecord()
		{
			//TODO - Fill GridView

			string[] statusCBCell = readstatusCBCell();

			if (statusCBCell != null)
			{
				try
				{

					DataGridViewRow rowToAdd = new DataGridViewRow();

					DataGridViewCellStyle headerStyle = dgwAddRecord.ColumnHeadersDefaultCellStyle;
					headerStyle.BackColor = Color.Navy;
					headerStyle.ForeColor = Color.White;
					headerStyle.Font = new Font(dgwAddRecord.Font, FontStyle.Bold);

					dgwAddRecord.ColumnCount = dgwPrimary.ColumnCount;

					dgwAddRecord.ReadOnly = false;

					
					for (int index = 0; index < dgwAddRecord.ColumnCount; index++)
					{
						//object objValue = order.GetType().GetProperty(sqlItem.Name).GetValue(order, null);
						dgwAddRecord.Columns[index].Name = dgwPrimary.Columns[index].Name;
						switch (index)
						{
							case 0:
							case 1:
							case 2:
							case 3:
							case 5:
							case 7:
							case 8:
								DataGridViewTextBoxCell textCell = new DataGridViewTextBoxCell();
								textCell.Value = "";
								rowToAdd.Cells.Add(textCell);
								break;
							case 4:
								var createdOn = DateTime.Now;
								DataGridViewTextBoxCell textCellCreatedOn = new DataGridViewTextBoxCell();
								textCellCreatedOn.Value = createdOn.ToString();
								rowToAdd.Cells.Add(textCellCreatedOn);
								break;
							case 6:
								DataGridViewComboBoxCell cbCell = new DataGridViewComboBoxCell();
								cbCell.Items.AddRange(statusCBCell);
								cbCell.Value = statusCBCell[0];
								rowToAdd.Cells.Add(cbCell);
								break;
							default:
								break;
						}
						
					}

					dgwAddRecord.Rows.Add(rowToAdd);

					dgwAddRecord.Rows[0].Cells[0].ReadOnly = true;
					dgwAddRecord.Rows[0].Cells[4].ReadOnly = true;
					dgwAddRecord.Rows[0].Cells[8].ReadOnly = true;

					showStatusOnForm1("Adding new Record:");
					showStatusGdwSecondary("Mode: Add New Record");
					addTextToRichBox("Add New Record - In progress ..." + "\n");

					return true;

				}
				catch (Exception ex)
				{

					addTextToRichBox("Error during Add New Record:" + ex.Message + "\n");
					return false;
				}
			}
			else
			{
				addTextToRichBox("Read DropDownList failed");
				return false;
			}

		}

		private void btnAddNewRecord_Click(object sender, EventArgs e)
		{
			if (addNewRecordIntoDB(dgwAddRecord))
			{
				//TODO - clear dgw

				clearAddNewRecordGrid(dgwAddRecord);				

				btnAddNewRecord.Visible = false;
				btnAddNewRecord.Enabled = false;

				btnCancelAddNewRecord.Visible = false;
				btnCancelAddNewRecord.Enabled = false;
				
				dgwAddRecord.Visible = false;

				if (loadData())
				{
					btnLoadData.Enabled = true;
					btnAddRecord.Enabled = true;
					btnModifyRecord.Enabled = true;
					dgwPrimarySizeAllColumns();
				}

			}
		}

		private bool clearAddNewRecordGrid(DataGridView dataGridViewToClear)
		{
			try
			{
				dataGridViewToClear.Rows.RemoveAt(0);
				return true;
			}
			catch (Exception)
			{
				//TO DO
				return false;
			}
		}

		private void btnCancelAddNewRecord_Click(object sender, EventArgs e)
		{
			if (clearAddNewRecordGrid(dgwAddRecord))
			{

				showStatusGdwSecondary("Add New Record Canceled");
				btnAddRecord.Enabled = true;
				btnLoadData.Enabled = true;
				btnModifyRecord.Enabled = true;

				btnAddNewRecord.Visible = false;
				btnAddNewRecord.Enabled = false;
				btnCancelAddNewRecord.Visible = false;
				btnCancelAddNewRecord.Enabled = false;

				dgwAddRecord.Visible = false;
			}
		}
	}
}
