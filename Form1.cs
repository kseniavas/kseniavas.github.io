namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        float sinX;
        float sum;
        int cnt;
        float e_;
        float x;
        int factorial(int n)
        {
            if (n < 0) return 0;
            if (n == 0) return 1;
            return n * factorial(n - 1);
        }
        //float sumOfRow(float x, float e)
        //{
        //    float sum = 0;
        //    int n = 2;
        //    float elem1=(float)((Math.Pow((-1), 0)* Math.Pow(x, 2*0))/(factorial(2*0+1)));
        //    float elem2 = (float)((Math.Pow((-1), 1) * Math.Pow(x, 2 * 1)) / (factorial(2 * 1 + 1)));
        //    sum += (elem1 + elem2);
        //    while (elem1-elem2<e)
        //    {

        //        elem1 = (float)((Math.Pow((-1), n) * Math.Pow(x, 2 * n)) / (factorial(2 * n + 1)));
        //        elem2 = (float)((Math.Pow((-1), n+1) * Math.Pow(x, 2 * n+1)) / (factorial(2 * n+1 + 1))); 
        //        sum += (elem1 + elem2);
        //        n++;
        //    }
        //    return sum*x;
        //}
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength!=0 && textBox2.TextLength != 0)
            {
                float sum = 0;
                int n = 2;
                float elem1 = (float)((Math.Pow((-1), 0) * Math.Pow(x, 2 * 0)) / (factorial(2 * 0 + 1)));
                float elem2 = (float)((Math.Pow((-1), 1) * Math.Pow(x, 2 * 1)) / (factorial(2 * 1 + 1)));
                sum += (elem1 + elem2);
                while (elem1 - elem2 < e_)
                {

                    elem1 = (float)((Math.Pow((-1), n) * Math.Pow(x, 2 * n)) / (factorial(2 * n + 1)));
                    elem2 = (float)((Math.Pow((-1), n + 1) * Math.Pow(x, 2 * n + 1)) / (factorial(2 * n + 1 + 1)));
                    sum += (elem1 + elem2);
                    n++;
                }
                sum = sum * x;
                sinX = (float)Math.Sin(x);
                label4.Text = "Sin(x) = " + sinX;
                label5.Text = "Сумма ряда " + sum;
                label6.Text = "Количество членов ряда " + n;
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.TextLength>0)
                e_ = float.Parse(textBox1.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.TextLength > 0)
                x = float.Parse(textBox2.Text);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && !(e.KeyChar == '-' && textBox1.TextLength == 0) && !(e.KeyChar == ',' && textBox1.TextLength == 0))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && !(e.KeyChar == '-' && textBox2.TextLength == 0) && !(e.KeyChar == ',' && textBox1.TextLength == 0))
            {
                e.Handled = true;
            }
        }
    }
}
