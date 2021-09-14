using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestSolition.Entity;

namespace TestSolition
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            using (Model db = new Model()) {
                try
                {
                    sample01 tbl = db.sample01s.Single();
                    result_textBox.Text = "name : " + tbl.name + "\r\nupdated_time : " + tbl.updated_time;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            MessageBox.Show("feature");
        }

        private void update_btn_Click(object sender, EventArgs e)
        {
            using (Model db = new Model())
            {
                try
                {
                    sample01 tbl = new sample01();
                    tbl.name = "insName";
                    db.Add<sample01>(tbl);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("test!test!test!");
            MessageBox.Show("test!test!test!");
            MessageBox.Show("test!test!test!");
            MessageBox.Show("test!test!test!");
        }
    }
}
