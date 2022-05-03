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

        private void button1_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            listBox1.Items.Add($"Max = {nums.Max()}");
            listBox1.Items.Add($"Min = {nums.Min()}");
            listBox1.Items.Add($"Sum = {nums.Sum()}");
            listBox1.Items.Add($"Average = {nums.Average()}");
            listBox1.Items.Add($"Count = {nums.Count()}");

            //var q = nums.Where(n => n % 2 == 0);
            //listBox1.Items.Add($"Min Even = {q.Min()}");
            listBox1.Items.Add($"Min Even = {nums.Where(n=>n%2==0).Min()}");

            //==============================================================

            productsTableAdapter1.Fill(nwDataSet1.Products);
            listBox1.Items.Add($"Max UnitsInStock = {nwDataSet1.Products.Max(p => p.UnitsInStock)}");
            listBox1.Items.Add($"Min UnitsInStock = {nwDataSet1.Products.Min(p => p.UnitsInStock)}");
            listBox1.Items.Add($"Sum UnitsInStock = {nwDataSet1.Products.Sum(p => p.UnitsInStock)}");
            listBox1.Items.Add($"Average UnitsInStock = {nwDataSet1.Products.Average(p => p.UnitsInStock)}");

        }
    }
}