using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmLINQ架構介紹_InsideLINQ : Form
    {
        public FrmLINQ架構介紹_InsideLINQ()
        {
            InitializeComponent();
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button30_Click(object sender, EventArgs e)
        {
            System.Collections.ArrayList arrList = new System.Collections.ArrayList();
            arrList.Add(5);
            arrList.Add(4);
            arrList.Add(3);
            var q = from n in arrList.Cast<int>()
                    where n > 3
                    select new { N = n };
            dataGridView1.DataSource = q.ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null; //清空
            productsTableAdapter1.Fill(nwDataSet1.Products);
            var q = (from p in nwDataSet1.Products
                     orderby p.UnitsInStock descending
                     select p).Take(5);
            dataGridView1.DataSource = q.ToList();
        }
    }
}