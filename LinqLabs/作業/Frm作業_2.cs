using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();
            productPhotoTableAdapter1.Fill(awDataSet1.ProductPhoto);
            comboBox3AddItems();
        }

        private void comboBox3AddItems()
        {
            var q = awDataSet1.ProductPhoto.OrderBy(p=>p.ModifiedDate.Year).Select(p => p.ModifiedDate.Year);
            foreach(var year in q.Distinct())
            {
                comboBox3.Items.Add(year);
            }
        }

        //System.Collections.Generic.List<LinqLabs.AWDataSet.ProductPhotoRow> productPhotos;

        private void button11_Click(object sender, EventArgs e)
        {
            //bindingSource1.DataSource= awDataSet1.ProductPhoto;
            //dataGridView1.DataSource = bindingSource1;

            dataGridView1.DataSource = null;
            pictureBox1.Image = null;
            var q = from p in awDataSet1.ProductPhoto
                    select p;
            //productPhotos = q.ToList();
            bindingSource1.DataSource = q.ToList();
            dataGridView1.DataSource = bindingSource1;
            LargePic();
        }
//        嚴重性 程式碼 說明 專案  檔案 行   隱藏項目狀態
//錯誤  CS0029 無法將類型 'System.Collections.Generic.List<LinqLabs.AWDataSet.ProductPhotoRow>' 隱含轉換成 'System.Data.EnumerableRowCollection<LinqLabs.AWDataSet.ProductPhotoRow>'	LinqLabs C:\資MSIT141_17\6.LINQ\Lily_Lab\LinqLabs(Solution)\LinqLabs\作業\Frm作業_2.cs	29	作用中

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            LargePic();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("查無資料");
            }
            else
            {
                dataGridView1.DataSource = null;
                pictureBox1.Image = null;
                var q = from p in awDataSet1.ProductPhoto
                        where p.ModifiedDate > dateTimePicker1.Value && p.ModifiedDate < dateTimePicker2.Value
                        select p;
                bindingSource1.DataSource = q.ToList();
                dataGridView1.DataSource = bindingSource1;
                LargePic();
            }
        }
        void LargePic()
        {
            try
            {
                var bytes = awDataSet1.ProductPhoto.Where(p => p.ProductPhotoID == (int)(dataGridView1.Rows[bindingSource1.Position].Cells["ProductPhotoID"].Value)).Select(p => p.LargePhoto);
                foreach (byte[] b in bytes)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    ms.Write(b, 0, Convert.ToInt32(b.Length));
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
            catch
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text == "")
            {
                MessageBox.Show("請選擇年分");
            }
            else
            {
                dataGridView1.DataSource = null;
                pictureBox1.Image = null;
                var q = awDataSet1.ProductPhoto.Where(p => p.ModifiedDate.Year.ToString() == comboBox3.Text);
                bindingSource1.DataSource = q.ToList();
                dataGridView1.DataSource = bindingSource1;
                LargePic();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "")
            {
                MessageBox.Show("請選擇季節");
            }
            else
            {
                dataGridView1.DataSource = null;
                pictureBox1.Image = null;
                int m1 = 0, m2 = 0;
                if (comboBox2.Text == "第一季") { m1 = 0; m2 = 4; }
                if (comboBox2.Text == "第二季") { m1 = 3; m2 = 7; }
                if (comboBox2.Text == "第三季") { m1 = 6; m2 = 10; }
                if (comboBox2.Text == "第四季") { m1 = 9; m2 = 13; }
                var q = awDataSet1.ProductPhoto.Where(p => p.ModifiedDate.Month > m1 && p.ModifiedDate.Month < m2);
                bindingSource1.DataSource = q.ToList();
                dataGridView1.DataSource = bindingSource1;
                LargePic();
                MessageBox.Show($"共有{dataGridView1.Rows.Count}筆");
            }
        }
    }
}

