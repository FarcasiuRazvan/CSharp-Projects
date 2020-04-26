using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace exempleAppSqlBasedForms
{
    public partial class Form1 : Form
    {
        //String comm = "Data Source=DESKTOP-6CRQK52;" + "Initial Catalog=FederationOfAthletics; Integrated Security=true";
        SqlConnection sqlconn = new SqlConnection("Data Source=DESKTOP-6CRQK52;" + "Initial Catalog=FederationOfAthletics; Integrated Security=true");

        //basically the database is saved here
        DataSet dset = new System.Data.DataSet();
        SqlDataAdapter parentAdapter;
        SqlDataAdapter childAdapter;
        BindingSource bsParent;
        BindingSource bsChild;

        
        string parentTable = ConfigurationManager.AppSettings["ParentTable"];
        string childTable = ConfigurationManager.AppSettings["ChildTable"];
        string parentID = ConfigurationManager.AppSettings["ParentKey"];
        string childID = ConfigurationManager.AppSettings["ChildKey"];
        string relation = ConfigurationManager.AppSettings["relationName"];
        string insertCommand = ConfigurationManager.AppSettings["insertCommand"];
        string deleteCommand = ConfigurationManager.AppSettings["deleteCommand"];
        string updateCommand = ConfigurationManager.AppSettings["updateCommand"];

        //list of the AppSetings to use in order to add/update/delete something general.
        List<string> arr = new List<string>();
        /*
        string parentTable = "CoachComitee";
        string childTable = "Coach";
        string parentID = "CCID";
        string childID = "CCID";
        string relation = "FK_COACH_CCID";
        */
        public Form1()
        { 
            InitializeComponent();
            this.FillData();
            binding();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void FillData()
        {
            //create an sql connection
            //create the data adapters (2) one for each table
            //create the data relation
            //create 2 fill(data into the) dataset


            //SqlCommand comand1 = new SqlCommand("Select CID,Name from Coach", sqlconn);        

            
            parentAdapter = new SqlDataAdapter("Select * from "+parentTable, sqlconn);
            parentAdapter.Fill(dset, parentTable);
            
            childAdapter = new SqlDataAdapter("Select * from "+childTable, sqlconn);
            childAdapter.Fill(dset, childTable);

            DataRelation rel = new DataRelation(relation, dset.Tables[parentTable].Columns[parentID], dset.Tables[childTable].Columns[childID]);
            dset.Relations.Add(rel);

            int contor = 0;
            foreach (string i in ConfigurationManager.AppSettings)
            {
                if (contor > 7) arr.Add(i);
                contor += 1;
            }

            //put in the datagrid the data from the tables METHOD 1
            //this.dataGridView1.DataSource = dset.Tables["CoachComitee"];
            //this.dataGridView2.DataSource = this.dataGridView1.DataSource;
            //this.dataGridView2.DataMember = "FK_COACH_CCID"; //chaining
        }
        private void binding()
        { 
            //put in the datagrid the data from the tables METHOD 2
            bsParent = new BindingSource();
            bsParent.DataSource = dset.Tables[parentTable];
            bsChild = new BindingSource(bsParent, relation);

            this.dataGridView1.DataSource = bsParent; //CoachComitee
            this.dataGridView2.DataSource = bsChild; //Coach

            //for the insert,update,delete use sql data adapter properties(for any sort of queries) or by using sql common builder(only for simple queries) 
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        //update button
        private void button2_Click(object sender, EventArgs e)
        {
            try
            { 
                sqlconn.Open();
                SqlCommand cmd = new SqlCommand(updateCommand, sqlconn);
                cmd.Parameters.Add("CID", SqlDbType.Int);
                cmd.Parameters["CID"].Value = Convert.ToInt32(textCID.Text);

                cmd.Parameters.Add("NAME", SqlDbType.VarChar);
                cmd.Parameters["NAME"].Value = textName.Text;

                cmd.Parameters.Add("AGE", SqlDbType.Int);
                cmd.Parameters["AGE"].Value = Convert.ToInt32(textAge.Text);

                cmd.Parameters.Add("SPECIALIZATION", SqlDbType.VarChar);
                cmd.Parameters["SPECIALIZATION"].Value = textSpecialization.Text;

                cmd.Parameters.Add("CCID", SqlDbType.Int);
                cmd.Parameters["CCID"].Value = Convert.ToInt32(textCCID.Text);

                
                childAdapter.UpdateCommand = cmd;
                childAdapter.UpdateCommand.ExecuteNonQuery();


                childAdapter = new SqlDataAdapter("Select * from "+childTable, sqlconn);
                dset.Tables[childTable].Clear();
                childAdapter.Fill(dset, childTable);
                binding();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally { sqlconn.Close(); }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                //"INSERT INTO Test.Dept (DeptNo, DName) " +"VALUES (DeptNo, DName)"
                sqlconn.Open();
                SqlCommand cmd = new SqlCommand(insertCommand, sqlconn);
                cmd.Parameters.Add("CID", SqlDbType.Int);
                cmd.Parameters["CID"].Value = Convert.ToInt32(textCID.Text);
                Console.WriteLine(textCID.Text);

                cmd.Parameters.Add("NAME", SqlDbType.VarChar);
                cmd.Parameters["NAME"].Value = textName.Text;
                Console.WriteLine(textName.Text);

                cmd.Parameters.Add("AGE", SqlDbType.Int);
                cmd.Parameters["AGE"].Value = Convert.ToInt32(textAge.Text);
                Console.WriteLine(textAge.Text);

                cmd.Parameters.Add("SPECIALIZATION", SqlDbType.VarChar);
                cmd.Parameters["SPECIALIZATION"].Value = textSpecialization.Text;
                Console.WriteLine(textSpecialization.Text);

                cmd.Parameters.Add("CCID", SqlDbType.Int);
                cmd.Parameters["CCID"].Value = Convert.ToInt32(textCCID.Text);
                Console.WriteLine(textCCID.Text);


                Console.WriteLine(cmd.Parameters.Count);
                childAdapter.InsertCommand = cmd;
                childAdapter.InsertCommand.ExecuteNonQuery();

                

                childAdapter = new SqlDataAdapter("Select * from "+childTable, sqlconn);
                dset.Tables[childTable].Clear();
                childAdapter.Fill(dset, childTable);
                binding();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally { sqlconn.Close(); }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                //"INSERT INTO Test.Dept (DeptNo, DName) " +"VALUES (DeptNo, DName)"
                sqlconn.Open();
                SqlCommand cmd = new SqlCommand(deleteCommand, sqlconn);
                cmd.Parameters.Add("CID", SqlDbType.Int);
                cmd.Parameters["CID"].Value = Convert.ToInt32(textCID.Text);


                childAdapter.DeleteCommand = cmd;
                childAdapter.DeleteCommand.ExecuteNonQuery();


                childAdapter = new SqlDataAdapter("Select * from "+childTable, sqlconn);
                dset.Tables[childTable].Clear();
                childAdapter.Fill(dset, childTable);
                binding();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally { sqlconn.Close(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder builder = new SqlCommandBuilder();
            builder.DataAdapter = childAdapter;
            childAdapter.Update(dset, childTable);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
