using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Internship
{
    public partial class TestRoutesForm : Form
    {
        DataTable dt;
        // The program will assume that the grid is valid and test if it is not.
        bool validGrid = true;
        public TestRoutesForm()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            this.ReadFromFile();
        }

        //The purpose of this function is to read from file all the rows of the table as strings and send them to initialise the data table.
        private void ReadFromFile()
        {
            try
            {
                string filename = txtFileName.Text;
                List<string[]> listValues = new List<string[]>();
                using (var reader = new StreamReader(filename))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        listValues.Add(values);
                    }
                }
                this.InitialiseDataTable(listValues);
            }
            catch (Exception e) {
                MessageBox.Show("The file name is invalid, please enter another one !!!", "Invalid File Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // This function will get as parameter a list of strings, each string is a row from the file.
        // The purpose of this function is to initialise the data table with the values from the list, and set the data source for the data grid view.
        private void InitialiseDataTable(List<string[]> listValues) {
            dt = new DataTable();
            for (int i = 0; i < listValues[0].Length; i++) {
                dt.Columns.Add(i.ToString(), typeof(string));
            }
            for (int i = 0; i < listValues.Count; i++) {
                DataRow row1 = dt.NewRow();
                for (int j = 0; j < listValues[0].Length; j++)
                {
                    row1[j.ToString()] = listValues[i][j];
                }
                dt.Rows.Add(row1);
            }
            dataGridView1.DataSource = dt;
            dataGridView1.ReadOnly = true;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            this.GridIsValid();
            if(validGrid==true) MessageBox.Show("The grid is valid !!!", "Valid Grid", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // This function will test each pin if it is valid and set the boolean validGrid to false if at least one pin is not valid.
        // If a pin is not valid it will trigger the function ColourGrid, which will colour with blue all the route containing the invalid pin.
        private void GridIsValid() {
            for (int i = 0; i < dt.Rows.Count; i++)
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (!this.IsPinValid(i,j)) {
                        validGrid = false;
                        this.ColourGrid(i, j, dt.Rows[i][j.ToString()].ToString());
                    }
                }
        }

        // This function will take as parameters 2 coordinates (i,j), will test if they are valid and will return true if they are or false otherwise.
        // If the value of the pin is "0" there is no point in testing any further the coordinates of it.
        private bool IsPinValid(int i, int j) {
            string pinValue = dt.Rows[i][j.ToString()].ToString();
            if (pinValue == "0") return true;
            else {
                if (i-1>=0 && pinValue != dt.Rows[i - 1][j.ToString()].ToString() && dt.Rows[i - 1][j.ToString()].ToString() != "0") return false;
                if (i-1>=0 && j+1<dt.Columns.Count && pinValue != dt.Rows[i - 1][(j+1).ToString()].ToString() && dt.Rows[i - 1][(j+1).ToString()].ToString() != "0") return false;
                if (i-1>=0 && j-1>=0 && pinValue != dt.Rows[i - 1][(j-1).ToString()].ToString() && dt.Rows[i - 1][(j-1).ToString()].ToString() != "0") return false;
                if (j+1<dt.Columns.Count && pinValue != dt.Rows[i][(j+1).ToString()].ToString() && dt.Rows[i][(j+1).ToString()].ToString() != "0") return false;
                if (j-1>=0 && pinValue != dt.Rows[i][(j-1).ToString()].ToString() && dt.Rows[i][(j-1).ToString()].ToString() != "0") return false;
                if (i+1<dt.Rows.Count && pinValue != dt.Rows[i + 1][j.ToString()].ToString() && dt.Rows[i + 1][j.ToString()].ToString() != "0") return false;
                if (i + 1 < dt.Rows.Count && j+1<dt.Columns.Count && pinValue != dt.Rows[i + 1][(j+1).ToString()].ToString() && dt.Rows[i + 1][(j+1).ToString()].ToString() != "0") return false;
                if (i + 1 < dt.Rows.Count && j-1>=0 && pinValue != dt.Rows[i + 1][(j-1).ToString()].ToString() && dt.Rows[i + 1][(j-1).ToString()].ToString() != "0") return false;
            }
            return true;
        }
        // This function will take as paramters 2 coordinates (i,j) and a value of a pin and will colour recursively only the route which has the received pin.
        private void ColourGrid(int i, int j, string pinValue) {
            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Aqua;
            if (j-1>=0 && dt.Rows[i][(j - 1).ToString()].ToString() == pinValue && dataGridView1.Rows[i].Cells[j-1].Style.BackColor != Color.Aqua) this.ColourGrid(i, j - 1, pinValue);
            if (j+1<dt.Columns.Count && dt.Rows[i][(j + 1).ToString()].ToString() == pinValue && dataGridView1.Rows[i].Cells[j + 1].Style.BackColor != Color.Aqua) this.ColourGrid(i, j + 1, pinValue);
            if (i-1>=0 && dt.Rows[i-1][(j).ToString()].ToString() == pinValue && dataGridView1.Rows[i-1].Cells[j].Style.BackColor != Color.Aqua) this.ColourGrid(i-1, j, pinValue);
            if (i+1<dt.Rows.Count && dt.Rows[i+1][(j).ToString()].ToString() == pinValue && dataGridView1.Rows[i+1].Cells[j].Style.BackColor != Color.Aqua) this.ColourGrid(i+1, j, pinValue);
        }

    }
}
