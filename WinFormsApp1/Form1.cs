using Microsoft.Data.SqlClient;
using Supabase;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Net.Http.Headers;
using System.Windows.Forms.DataVisualization.Charting;
using static WinFormsApp1.Form1;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {


        private Supabase.Client supabaseClient;

        //r9P4JHfOmltBwvxL
        string url = "https://dcpufbgrhzxbvumbarmb.supabase.co";
        string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImRjcHVmYmdyaHp4YnZ1bWJhcm1iIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NzMzMDAxNjUsImV4cCI6MjA4ODg3NjE2NX0.g0BBStFaCv0_bg-EOzoSQl-lKfsJUzX3qKCQUZY5lHQ";
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-4E3FDQ6\MSSQLSERVER01; Initial Catalog=QUIZ; Integrated Security=True;Encrypt=True;;
");

        [Table("browsers")]
        public class Browsers: BaseModel
        {
            [PrimaryKey("BrowserID")]
            public int BrowserId { get; set; }

            [Column("Browser")]
            public string Browser { get; set; }

        }
        [Table("answers")]
        public class Answers: BaseModel{
            [PrimaryKey("answerid")]
            public int AnswerId { get; set; }

            [Column("answer")]
            public int BrowserId { get; set; }

            [Column("date")]
            public DateTime Date { get; set; }
        }
        public class BrowserStat
        {
            public string BrowserName { get; set; }
            public int VotesCount { get; set; }
            public double Percentage { get; set; }
        }
        void FillData()
        {
        }
        string selectSql2 = "select Browser as Браузер, count(Answer) as [Количество Голосов], ROUND(\r\n        COUNT(Answer) * 100.0 / NULLIF((SELECT COUNT(*) FROM Answers), 0), \r\n        2\r\n    ) as Процент from dbo.Browsers LEFT JOIN \r\n    dbo.Answers ON dbo.Answers.Answer=dbo.Browsers.BrowserID\r\ngroup by Browser ";

        public Form1()
        {
            InitializeComponent();

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)

        {
            // Создаем новый ответ
            var newAnswer = new Answers
            {
                BrowserId = comboBox1.SelectedIndex,
                Date = DateTime.Now
            };
            var response = await supabaseClient.From<Answers>().Insert(newAnswer);
            await RefreshAnswersData();
            await RefreshAnswersSates();
            await RefreshChart();





        }
     
        private async Task RefreshChart()
        {
            var stats = await GetStatsAsync();
            chart1.Series[0].Points.Clear();

            int count = stats.Count;
            int counter = 1;
            foreach(var stat in stats) {
                chart1.Series[0].Points.AddXY(counter, stat.VotesCount);
                chart1.Series[0].Points[counter-1].AxisLabel = stat.BrowserName;
                counter++;
            }


        }
        private async Task RefreshAnswersData()
        {
            var response = await supabaseClient.From<Answers>().Get();//select * form Answers
            var answers = response.Models;
            DataTable dt = new DataTable();
            dt.Columns.Add("AnswerID", typeof(int));
            dt.Columns.Add("Answer", typeof(int));
            dt.Columns.Add("Date", typeof(DateTime));
            foreach(var ans in answers)
            {
                dt.Rows.Add(ans.AnswerId, ans.BrowserId, ans.Date);
            }
            dataGridView2.DataSource = dt;
        }

        private async Task RefreshAnswersSates()
        {
            var states = await GetStatsAsync();
            DataTable dt=new DataTable();
            dt.Columns.Add("Браузер");
            dt.Columns.Add("Количество голосов");
            dt.Columns.Add("Процент");
            foreach (var browser in states)
            {
                dt.Rows.Add(browser.BrowserName, browser.VotesCount, browser.Percentage);
            }
            dataGridView1.DataSource = dt;
        }
        private async Task<List<BrowserStat>> GetStatsAsync()
        {
            var AnsResponse = await supabaseClient.From<Answers>().Get();//Models=[{a=1,b=1, ..}, {a=1,b=1, ..}. {a=1,b=1, ..}]
            var answers = AnsResponse.Models;//[a=1,b=1, ..,a=1,b=1, ..,a=1,b=1, ..]

            var BrowResponse = await supabaseClient.From<Browsers>().Get();
            var browsers = BrowResponse.Models;

            var stats = new List<BrowserStat>();
            int totalVotes = answers.Count;//сколько голосов было
            foreach (var browser in browsers) {
                int votesCount = answers.Count(a => a.BrowserId == browser.BrowserId);//для каждого браузера считаем количество голосов
                double prcentage = totalVotes > 0 ? Math.Round(votesCount * 100.0 / totalVotes, 2) : 0;
                stats.Add(new BrowserStat
                {
                    BrowserName = browser.Browser,
                    VotesCount = votesCount,
                    Percentage = prcentage
                });

        }
            return stats;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            supabaseClient = new Supabase.Client(url, key);
            await RefreshAnswersData();
            await RefreshAnswersSates();
            Series s = chart1.Series[0];
            s.ChartType = SeriesChartType.Column;
            ChartArea area = chart1.ChartAreas[0];//область в которой рисуем график
            area.AxisX.Title = "Браузеры";
            area.AxisY.Title = "Количество голосов";
            
            
            await RefreshChart();
            


        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await supabaseClient.From<Answers>().Filter("answerid", Supabase.Postgrest.Constants.Operator.GreaterThan, 0)
            .Delete();
            await RefreshAnswersData(); 
            await RefreshAnswersSates();
            await RefreshChart();

           

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}

