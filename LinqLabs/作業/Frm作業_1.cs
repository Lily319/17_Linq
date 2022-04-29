using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        public Frm作業_1()
        {
            InitializeComponent();

            ordersTableAdapter1.Fill(nwDataSet1.Orders);
            order_DetailsTableAdapter1.Fill(nwDataSet2.Order_Details);

            CreateComboBoxItems();

            productsTableAdapter1.Fill(nwDataSet3.Products);
        }

        private void CreateComboBoxItems()
        {
            var q = from r in nwDataSet1.Orders
                    group r by r.OrderDate.Year into year
                    select year;
            foreach(IGrouping<int,LinqLabs.NWDataSet.OrdersRow> y in q)
            {
                comboBox1.Items.Add(y.Key);
            }
        }
        //System.Collections.Generic.List<LinqLabs.NWDataSet.OrdersRow> orders;
        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            var q = from od in nwDataSet2.Order_Details
                    where od.OrderID == /*orders*/(int)(dataGridView1.Rows[bindingSource1.Position].Cells["OrderID"].Value)//.OrderID
                    select od;
            dataGridView2.DataSource = q.ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files =  dir.GetFiles();

            //var 
            var q = from f in files
                    where f.Extension == ".log"
                    select f;

            this.dataGridView1.DataSource = q.ToList();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();
            var q = from f in files
                    where f.CreationTime.Year==2019
                    select f;

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();
            var q = from f in files
                    where f.Length>10000
                    select f;

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //bindingSource1.DataSource = nwDataSet1.Orders;
            //dataGridView1.DataSource = bindingSource1;
            var q = from o in nwDataSet1.Orders
                    select o;
            bindingSource1.DataSource = q.ToList();
            dataGridView1.DataSource = bindingSource1;
            //orders = q.ToList();
            bindingSource1.CurrentItemChanged += bindingSource1_CurrentChanged;

            var q1 = from od in nwDataSet2.Order_Details
                    where od.OrderID == (int)(dataGridView1.CurrentRow.Cells[0].Value)
                    select od;
            dataGridView2.DataSource = q1.ToList();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //int i = (int)(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

            //var q = from od in nwDataSet2.Order_Details
            //        where od.OrderID == (int)(dataGridView1.CurrentRow.Cells[0].Value)
            //        select od;
            //dataGridView2.DataSource = q.ToList();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("請選擇年份");
            }
            else
            {
                var q1 = from o in nwDataSet1.Orders
                         where !o.IsShippedDateNull() && !o.IsShipRegionNull() && !o.IsShipPostalCodeNull() && o.OrderDate.Year.ToString() == comboBox1.Text
                         select o;
                bindingSource1.DataSource = q1.ToList();
                dataGridView1.DataSource = bindingSource1;
                //orders = q1.ToList();
                bindingSource1.CurrentItemChanged += bindingSource1_CurrentChanged;
                var q = from od in nwDataSet2.Order_Details
                        where od.OrderID == (int)(dataGridView1.CurrentRow.Cells[0].Value)
                        select od;
                dataGridView2.DataSource = q.ToList();
            }
        }
        int pages;
        int begin = 0;
        private void button12_Click(object sender, EventArgs e)
        {
            bool IsNum = int.TryParse(textBox1.Text, out pages);
            begin -= pages;
            if (begin < 0)
            {
                var q = from p in nwDataSet3.Products.Take(pages)
                        select p;
                dataGridView1.DataSource = q.ToList();
                MessageBox.Show("已在最首頁");
            }
            else
            {
                if (IsNum)
                {
                    var q = from p in nwDataSet3.Products.Skip(begin).Take(pages)
                            select p;
                    dataGridView1.DataSource = q.ToList();
                }
                else
                {
                    MessageBox.Show("請輸入數值");
                }
            }
        }
        private void button13_Click(object sender, EventArgs e)
        {
            //this.nwDataSet1.Products.Take(10);//Top 10 Skip(10)

            //Distinct()
            bool IsNum = int.TryParse(textBox1.Text, out pages);
            begin += pages;
            if (begin < nwDataSet3.Products.Rows.Count)
            {
                if (IsNum)
                {
                    var q = from p in nwDataSet3.Products.Skip(begin).Take(pages)
                            select p;
                    dataGridView1.DataSource = q.ToList();
                }
                else
                {
                    MessageBox.Show("請輸入數值");
                }
            }
            else
            {
                MessageBox.Show("已是最末頁");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //begin = 0;
        }
    }
}
