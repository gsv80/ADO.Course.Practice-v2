﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task1_4_StateChange
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.connection.StateChange += new StateChangeEventHandler(this.connection_StateChange);
        }

        private void connection_StateChange(object sender, StateChangeEventArgs e)
        {
            ToolStripMenuItem.Enabled = (e.CurrentState == ConnectionState.Closed);

        }

        OleDbConnection connection = new OleDbConnection();

        

        string testConnect = @"Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Northwind;Data Source=(local)";

        private void connectDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = testConnect;
                    connection.Open();
                    MessageBox.Show("Соединение с базой данных выполнено успешно");
                }
                else
                    MessageBox.Show("Соединение с базой данных уже установлено");
            }
            catch (OleDbException XcpSQL)
            {
                foreach (OleDbError se in XcpSQL.Errors)
                {
                    MessageBox.Show(se.Message,
                    "SQL Error code " + se.NativeError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }
            }

            catch (Exception Xcp)
            {
                MessageBox.Show(Xcp.Message, "Unexpected Exception",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void disconnectDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                MessageBox.Show("Соединение с базой данных закрыто");
            }
            else
                MessageBox.Show("Соединение с базой данных уже закрыто");
        }

        private void connectDBToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {

        }
    }
}
