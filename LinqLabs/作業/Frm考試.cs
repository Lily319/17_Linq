using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs
{
    public partial class Frm考試 : Form
    {
        public Frm考試()
        {
            InitializeComponent();

            students_scores = new List<Student>()
                                         {
                                            new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                                            new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                                            new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                                            new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                                            new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                                            new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },
                                          };
            Random r = new Random();
            for (int i = 0; i < scores.Length; i++)
            {
                scores[i] = r.Next(60, 100);
                Application.DoEvents();
            }
        }

        List<Student> students_scores;

        public class Student
        {
            public string Name { get; set; }
            public string Class { get;  set; }
            public int Chi { get; set; }
            public int Eng { get; internal set; }
            public int Math { get;  set; }
            public string Gender { get; set; }
        }
        void AllClean()
        {
            flag = false;
            students_scores = new List<Student>()
                                         {
                                            new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                                            new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                                            new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                                            new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                                            new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                                            new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },
                                          };
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            chart1.DataSource = null;
            chart1.Series.Clear();
            chart1.Series.Add("Series1");
            chart1.Series.Add("Series2");
            chart1.Series.Add("Series3");
        }
        private void button36_Click(object sender, EventArgs e)
        {
            AllClean();
            #region 搜尋 班級學生成績

            //// 共幾個 學員成績 ?
            //MessageBox.Show($"共{students_scores.Count()}個學員成績\n");

            //// 找出 前面三個 的學員所有科目成績	
            //dataGridView1.DataSource = students_scores.Take(3).ToList();
            //chart1.DataSource = dataGridView1.DataSource;


            //// 找出 後面兩個 的學員所有科目成績					
            //dataGridView1.DataSource = students_scores.Skip(students_scores.Count() - 2).Take(2).ToList();
            //chart1.DataSource = dataGridView1.DataSource;

            // 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績						
            //dataGridView1.DataSource = students_scores.Where(s => s.Name == "aaa" || s.Name == "bbb" || s.Name == "ccc").Select(s => new
            //{
            //    s.Name,
            //    s.Chi,
            //    s.Eng
            //}).ToList();
            //chart1.DataSource = dataGridView1.DataSource;

            //// 找出學員 'bbb' 的成績
            //dataGridView1.DataSource = students_scores.Where(s => s.Name == ("bbb")).ToList();
            //chart1.DataSource = dataGridView1.DataSource;

            //// 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	
            //students_scores.Remove(students_scores.Where(s => s.Name == "bbb").First());
            //dataGridView1.DataSource = students_scores.ToList();
            //chart1.DataSource = dataGridView1.DataSource;

            // 找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績  |				
            dataGridView1.DataSource = students_scores.Where(s => s.Name == "aaa" || s.Name == "bbb" || s.Name == "ccc").Select(s => new
            {
                s.Name,
                s.Chi,
                s.Math
            }).ToList();
            chart1.DataSource = dataGridView1.DataSource;

            //// 數學不及格 ... 是誰
            //dataGridView1.DataSource = students_scores.Where(s => s.Math < 60).Select(s => new { 數學不及格=s.Name }).ToList();

            ////======================Chart==================
            chart1.Series[0].Name = "Chi";
            chart1.Series[0].XValueMember = "Name";
            chart1.Series[0].YValueMembers = "Chi";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            //chart1.Series[1].Name = "Eng";
            //chart1.Series[1].XValueMember = "Name";
            //chart1.Series[1].YValueMembers = "Eng";
            //chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series[2].Name = "Math";
            chart1.Series[2].XValueMember = "Name";
            chart1.Series[2].YValueMembers = "Math";
            chart1.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            #endregion


        }

        private void button37_Click(object sender, EventArgs e)
        {
            AllClean();
            //個人 sum, min, max, avg
            //dataGridView1.DataSource = students_scores.Select(s => new
            //{
            //    s.Name,
            //    s.Class,
            //    s.Chi,
            //    s.Eng,
            //    s.Math,
            //    s.Gender,
            //    Sum = new int[] { s.Chi, s.Eng, s.Math }.Sum(),
            //    Min = new int[] { s.Chi, s.Eng, s.Math }.Min(),
            //    Max = new int[] { s.Chi, s.Eng, s.Math }.Max(),
            //    Avg = new int[] { s.Chi, s.Eng, s.Math }.Average()
            //}).ToList();

            //各科 sum, min, max, avg
            students_scores.Add(new Student
            {
                Name = "各科Sum",
                Chi = students_scores.Sum(s => s.Chi),
                Eng = students_scores.Sum(s => s.Eng),
                Math = students_scores.Sum(s => s.Math)
            });
            students_scores.Add(new Student
            {
                Name = "各科Min",
                Chi = students_scores.Min(s => s.Chi),
                Eng = students_scores.Min(s => s.Eng),
                Math = students_scores.Min(s => s.Math)
            });
            students_scores.Add(new Student
            {
                Name = "各科Max",
                Chi = students_scores.Max(s => s.Chi),
                Eng = students_scores.Max(s => s.Eng),
                Math = students_scores.Max(s => s.Math)
            });
            students_scores.Add(new Student
            {
                Name = "各科Avg",
                Chi = (int)(students_scores.Average(s => s.Chi)),
                Eng = (int)(students_scores.Average(s => s.Eng)),
                Math = (int)(students_scores.Average(s => s.Math))
            });
            dataGridView1.DataSource = students_scores.Select(s => new
            {
                s.Name,
                s.Class,
                s.Chi,
                s.Eng,
                s.Math,
                s.Gender,
                Sum = new int[] { s.Chi, s.Eng, s.Math }.Sum(),
                Min = new int[] { s.Chi, s.Eng, s.Math }.Min(),
                Max = new int[] { s.Chi, s.Eng, s.Math }.Max(),
                Avg = new int[] { s.Chi, s.Eng, s.Math }.Average()
            }).ToList();
        }
        int[] scores = new int[100];
        bool flag = false;
        private void button33_Click(object sender, EventArgs e)
        {
            AllClean();
            flag = true;
            // split=> 分成 三群 '待加強'(60~69) '佳'(70~89) '優良'(90~100) 

            // print 每一群是哪幾個 ? (每一群 sort by 分數 descending)

            dataGridView1.DataSource = scores.GroupBy(s => MyScoreKey(s)).Select(g => new
            {
                g.Key,
                Count=g.Count(),
                Group=g
            }).ToList();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (flag)
            {
                dataGridView2.DataSource = scores.Where(s => MyScoreKey(s) == dataGridView1.CurrentRow.Cells[0].Value.ToString()).Select(s => new
                {
                    scores = s
                }).OrderByDescending(s => s.scores).ToList();
            }
        }
        string MyScoreKey(double score) 
        {
            if (score >= 79 && score <= 89) return "佳";
            else if (score >= 90 && score <= 100) return "優良";
            else return "待加強";
        }
        private void button35_Click(object sender, EventArgs e)
        {
            // 統計 :　所有隨機分數出現的次數/比率; sort ascending or descending
            // 63     7.00%
            // 100    6.00%
            // 78     6.00%
            // 89     5.00%
            // 83     5.00%
            // 61     4.00%
            // 64     4.00%
            // 91     4.00%
            // 79     4.00%
            // 84     3.00%
            // 62     3.00%
            // 73     3.00%
            // 74     3.00%
            // 75     3.00%
            AllClean();
            dataGridView2.DataSource = scores.GroupBy(s => s).Select(g => new
            {
                scores = g.Key,
                次數 = g.Count(),
                比率 = ((double)g.Count()/(double)scores.Length).ToString("P")
            }).OrderByDescending(s => s.比率).ToList();
        }
        NorthwindEntities db = new NorthwindEntities();
        private void button34_Click(object sender, EventArgs e)
        {
            AllClean();
            // 年度最高銷售金額 年度最低銷售金額

            // 那一年總銷售最好 ? 那一年總銷售最不好 ?  
            int bestYear = db.Order_Details.AsEnumerable().GroupBy(od => od.Order.OrderDate.Value.Year).Select(g => new
            {
                Year = g.Key,
                Total = g.Sum(od => (double)od.UnitPrice * od.Quantity * (1 - od.Discount))
            }).OrderByDescending(g=>g.Total).Select(g=>g.Year).First();
            int lastYear = db.Order_Details.AsEnumerable().GroupBy(od => od.Order.OrderDate.Value.Year).Select(g => new
            {
                Year = g.Key,
                Total = g.Sum(od => (double)od.UnitPrice * od.Quantity * (1 - od.Discount))
            }).OrderByDescending(g => g.Total).Select(g => g.Year).Last();

            MessageBox.Show($"{bestYear}年總銷售最好, {lastYear}年總銷售最不好");

            // 那一個月總銷售最好 ? 那一個月總銷售最不好 ?
            string bestMonth = db.Order_Details.AsEnumerable().GroupBy(od => od.Order.OrderDate.Value.ToString("yyyy年MM月")).Select(g => new
            {
                月=g.Key,
                Total = g.Sum(od => (double)od.UnitPrice * od.Quantity * (1 - od.Discount))
            }).OrderByDescending(g => g.Total).Select(g => g.月).First();
            string lastMonth = db.Order_Details.AsEnumerable().GroupBy(od => od.Order.OrderDate.Value.ToString("yyyy年MM月")).Select(g => new
            {
                月 = g.Key,
                Total = g.Sum(od => (double)od.UnitPrice * od.Quantity * (1 - od.Discount))
            }).OrderByDescending(g => g.Total).Select(g => g.月).Last();

            MessageBox.Show($"{bestMonth}總銷售最好, {lastMonth}年總銷售最不好");

            // 每年 總銷售分析 圖
            chart1.DataSource = db.Order_Details.AsEnumerable().GroupBy(od => od.Order.OrderDate.Value.Year).Select(g => new
            {
                Year = g.Key,
                Total = g.Sum(od => (double)od.UnitPrice * od.Quantity * (1 - od.Discount))
            }).ToList();
            chart1.Series[0].Name = "年度銷售額";
            chart1.Series[0].XValueMember = "Year";
            chart1.Series[0].YValueMembers = "Total";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            // 每月 總銷售分析 圖
            chart1.DataSource = db.Order_Details.AsEnumerable().GroupBy(od => od.Order.OrderDate.Value.ToString("yyyy年MM月")).Select(g => new
            {
                月 = g.Key,
                Total = g.Sum(od => (double)od.UnitPrice * od.Quantity * (1 - od.Discount))
            }).ToList();
            chart1.Series[0].Name = "每月銷售額";
            chart1.Series[0].XValueMember = "月";
            chart1.Series[0].YValueMembers = "Total";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //年 銷售成長率
        }
    }
}
