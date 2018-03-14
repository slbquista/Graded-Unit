﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Connection string - note change the drive letter depending on the letter of the USB!
            //Unsure where the connection string settup for the DataSets created using Visual Studio is stored in code form, but it can be changed via "Project > Project Properties > Settings"

            //SqlConnection con = new SqlConnection("Data Source=(LocalDB)/MSSQLLocalDB;AttachDbFilename=F:/SQL Database/graded_unit.mdf;Integrated Security=True;Connect Timeout=30");
            //DataTable dt = new DataTable();
            //SqlCommand insert = new System.Data.SqlClient.SqlCommand();

            //DataSets created to read data from the database into DataGridViews
            this.gu_batchTableAdapter.Fill(this.graded_unitDataSet6.gu_batch);
            this.packagingDutyRecordsViewTableAdapter.Fill(this.graded_unitDataSet5.packagingDutyRecordsView);
            this.salesSoldViewTableAdapter.Fill(this.graded_unitDataSet4.salesSoldView);
            this.salesAvailableViewTableAdapter.Fill(this.graded_unitDataSet3.salesAvailableView);
            this.packagingViewTableAdapter.Fill(this.graded_unitDataSet2.packagingView);
            this.productionViewTableAdapter.Fill(this.graded_unitDataSet1.productionView);
        }

        //Updates the updateDate picker to use correct date format for the date datatype in SQL
        public void SetMyCustomFormat()
        {
            //TODO: Check if this is working later as it isn't displayed as such
            updateDate.Format = DateTimePickerFormat.Custom;
            updateDate.CustomFormat = "yyyy MMM dd";
        }

		//Button to insert input from the Production > Insert tab
		private void submitRecordsInsert_Click(object sender, EventArgs e)
		{
			insertRecords(insertNumberContainers.Text);
		}

		private void insertRecords (String location)
		{
			SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\SQL Database\graded_unit.mdf;Integrated Security=False;Connect Timeout=30");

			try
			{
				con.Open();
			}
			catch (Exception err)
			{
				MessageBox.Show(err.Message);
			}

			string sql = "INSERT INTO gu_batch (drink_id, gyle, container_id, number_of_items, storage_location) VALUES (2, 'ier', 1, 1, @storage_location); ";
			using (var cmd = new SqlCommand(sql, con))
			{
				//This lets you put values in without directly adding them. 
				//cmd.Parameters.AddWithValue("@fName", fName);
				//cmd.Parameters.AddWithValue("@lName", lName);
				//cmd.Parameters.AddWithValue("@password", password);
				cmd.Parameters.AddWithValue("@storage_location", location);

				try
				{
					cmd.ExecuteNonQuery();
					MessageBox.Show("The user was Added Successfully");
				}
				catch (Exception err)
				{
					MessageBox.Show(err.Message);
				}

				try
				{
					con.Close();
				}
				catch (Exception err)
				{
					MessageBox.Show(err.Message);
				}

			}
		}


	}
}
