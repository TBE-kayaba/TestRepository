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
                    sample01 tbl = db.sample01s.Single();
                    result_textBox.Text = "name : " + tbl.name + "\r\nupdated_time : " + tbl.updated_time;
                
            }
        }
    }
}
