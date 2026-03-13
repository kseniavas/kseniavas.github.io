namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        float sinX;
        float sum;//сумма ряда
       
        float e_;//точность (Eps)
        float x;//аргумет функции
        int n;//количество членов ряда
        //вычисление факториала
        int factorial(int n)
        {
            if (n < 0) return 0;
            if (n == 0) return 1;
            return n * factorial(n - 1);
        }

        //вычисление n-ного элемента ряда
        float inRes(float x, int n)
        {
            return (float)((Math.Pow((-1), n) * Math.Pow(x, 2 * n)) / (factorial(2 * n + 1)));
        }

        //вычисление суммы ряда и количества элементов ряда
        //принимает аргумент x и точность e
        //возвращает кортеж (сумма, количество элементов)
        (float sumres , int nres) sumOfRow(float x, float e)
        {
            float sum = 0;
            int n = 2;
            float elem1 = inRes(x, 0);
            float elem2 = inRes(x, 1);
            sum += (elem1 + elem2);
            while (elem1 - elem2 < e)
            {

                elem1 = inRes(x, n);
                elem2 = inRes(x, n+1);
                sum += (elem1 + elem2);
                n++;
            }
            return (sum * x, n);
        }
       
        //кнопка "вычислить"
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength!=0 && textBox2.TextLength != 0&&!float.IsInfinity(x))//проверка на заполненность полей
            {
                (float, int) sumCount = sumOfRow(x, e_);
                sum = sumOfRow(x, e_).sumres;//вычисление суммы ряда
                n = sumOfRow(x, e_).nres;
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
            
            if(float.TryParse(textBox1.Text, out float res))
                e_ = float.Parse(textBox1.Text);

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(textBox1.Text, out float res))
                x = float.Parse(textBox2.Text);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && !(e.KeyChar == '-' && textBox1.TextLength == 0) && !(e.KeyChar == ',' ))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && !(e.KeyChar == ',' ))//нельзя ввести отрицательное число, т.к. знак "-" не подразумевается
            {
                e.Handled = true;
            }
        }
    }
}
