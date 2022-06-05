using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Alive
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            customizeDesign(); // ДОБАВЛЯЮ ЗАКРЫТИЕ SUB MENU ( ДОБАВОЧНОГО МЕНЮ СЛЕВА ) 
            chart1.MouseWheel += chart1_MouseWheel; // ДОБАВЛЯЮ СКРОЛЛ( МАСШТАБИРОВАНИЕ) МЫШЬЮ
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        // Прописывание процесса открытия доп окон слева 
        private void customizeDesign() 
        {
            panelgraphs.Visible = false;
            paneltheory.Visible = false;
            panelinfo.Visible = false;
        }

        private void hideSumMenu()
        {
            if (panelgraphs.Visible == true)
                panelgraphs.Visible = false;
            if (paneltheory.Visible == true)
                paneltheory.Visible = false;
            if (panelinfo.Visible == true)
                panelinfo.Visible = false;

        }

        private void showSubMenu ( Panel subMenu) 
        {
            if (subMenu.Visible == false)
            {
                hideSumMenu();
                subMenu.Visible = true;

            }
            else
                subMenu.Visible = false;
        }
        // конец 
        private void button1_Click(object sender, EventArgs e)
        {
            showSubMenu(panelgraphs);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            showSubMenu(paneltheory);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            showSubMenu(panelinfo);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage("krivie2");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage("main");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage("graphs");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
       
        private void button11_Click(object sender, EventArgs e) //Кривые второго порядка
        {
            double y1, y2;
            double x, Dis;
            int a11, a12, a22, a13, a23, a33, D, b, a;
            try
            {
                a11 = Convert.ToInt32(textBox1.Text);
            }
            catch (FormatException)
            {
                textBox1.Text = "0";
                a11 = 0;
            }

            try
            {
                a12 = Convert.ToInt32(textBox2.Text);
            }
            catch (FormatException)
            {
                textBox2.Text = "0";
                a12 = 0;
            }

            try
            {
                a22 = Convert.ToInt32(textBox3.Text);
            }
            catch (FormatException)
            {
                textBox3.Text = "0";
                a22 = 0;
            }

            try
            {
                a13 = Convert.ToInt32(textBox4.Text);
            }
            catch (FormatException)
            {
                textBox4.Text = "0"; a13 = 0;
            }

            try
            {
                a23 = Convert.ToInt32(textBox5.Text);
            }
            catch (FormatException) { textBox5.Text = "0"; a23 = 0; }

            try
            {
                a33 = Convert.ToInt32(textBox6.Text);
            }
            catch (FormatException)
            {
                textBox6.Text = "0"; a33 = 0;

            }

            try
            {
                a = Convert.ToInt32(textBox7.Text);
            }
            catch (FormatException) { textBox7.Text = "0"; a = 0; }

            try
            {
                b = Convert.ToInt32(textBox8.Text);
            }
            catch (FormatException) { textBox8.Text = "0"; b = 0; }


            if ((a11 == 0 & a12 == 0 & a22 == 0) == false)
            {
                D = a11 * a22 - a12 * a12;
                if (D == 0)
                    MessageBox.Show("ваша кривая параболического типа"); //проверка типа
                if (D > 0)
                    MessageBox.Show(" ваша кривая эллиптического типа ");
                if (D < 0)
                    MessageBox.Show(" ваша кривая гиперболического типа ");
            }




            this.chart1.Series[0].Points.Clear();



            for (x = a + 0.01; x <= b + 0.01; x += 0.01)
            {
                Dis = Math.Pow((2 * x * a12 + 2 * a23), 2) - 4 * a22 * (a11 * x * x + 2 * a13 * x + a33);
                y1 = -(2 * x * a12 + 2 * a23) + Math.Sqrt(Dis) / 2 * a22;
                y2 = -(2 * x * a12 + 2 * a23) - Math.Sqrt(Dis) / 2 * a22;




                chart1.Series[0].Points.AddXY(x, y1);
                chart1.Series[0].Points.AddXY(x, y2);







            }

            if ( a11==0 & a12==0 & a22 == 0)
            {
                this.chart1.Series[0].Points.Clear();
                MessageBox.Show("Введите другие значение перед x^2, y^2 и 2*xy. Они не должны быть одновременно равны нулю");
            }
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;


        }


        private void chart1_MouseMove(object sender, MouseEventArgs e) //движение мыши
        {
            lab_X_Axis.Location = new Point(e.X, 21);
            lab_Y_Axis.Location = new Point(76, e.Y);

            if (e.X <= 72 || e.Y >= 405 || e.Y <= 72 || e.X >= 640)   // e.X >= при уменьшении числа сдивагется влево; e.Y <= при уменьшении числа - линия Oy поднимается; e.Y >= при уменьшении числа Oy поднимается; 
            {
                lab_X_Axis.Visible = false;
                lab_Y_Axis.Visible = false;
                lab_X_Axis_Cur.Visible = false;

            }
            else
            {
                lab_X_Axis.Visible = true;
                lab_Y_Axis.Visible = true;
                lab_X_Axis_Cur.Visible = true;

            }

            try
            {
                double yValue = chart1.ChartAreas[0].AxisY2.PixelPositionToValue(e.Y);
                double xValue = chart1.ChartAreas[0].AxisX2.PixelPositionToValue(e.X);

                lab_X_Axis_Cur.Text = String.Concat(String.Concat(Math.Round(xValue, 2).ToString(), " , "), Math.Round(yValue, 2).ToString());
                lab_X_Axis_Cur.Location = new Point(640, e.Y - 5);

            }
            catch
            {

            }
            finally
            {

            }


        }



        private void chart1_MouseWheel(object sender, MouseEventArgs e) // приближение колесиком мыши
        {

            var xAxis = chart1.ChartAreas[0].AxisX;
            var yAxis = chart1.ChartAreas[0].AxisY;

            try
            {
                if (e.Delta < 0) // Scrolled down.
                {
                    xAxis.ScaleView.ZoomReset();
                    yAxis.ScaleView.ZoomReset();
                }
                else if (e.Delta > 0) // Scrolled up.
                {
                    var xMin = xAxis.ScaleView.ViewMinimum;
                    var xMax = xAxis.ScaleView.ViewMaximum;
                    var yMin = yAxis.ScaleView.ViewMinimum;
                    var yMax = yAxis.ScaleView.ViewMaximum;

                    var posXStart = xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                    var posXFinish = xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;
                    var posYStart = yAxis.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4;
                    var posYFinish = yAxis.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 4;

                    xAxis.ScaleView.Zoom(posXStart, posXFinish);
                    yAxis.ScaleView.Zoom(posYStart, posYFinish);
                }
            }
            catch { }   // кривые второго пр-ка конец

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPages1_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

        private void button5_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage("kriviepr");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox41_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage("historykrivie");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage("info");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://vk.com/d4rkbishop");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage("dopinfo");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            double y, x=-10;

            this.chart2.Series[0].Points.Clear();
            for ( x=-9.99; x<10+0.01; x+=0.01)
            {
                y = Math.Cos(x);
                this.chart2.Series[0].Points.AddXY(x, y);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            double y, x = -10;

            this.chart2.Series[1].Points.Clear();
            for (x = -9.99; x < 10 + 0.01; x += 0.01)
            {
                y = Math.Sin(x);
                this.chart2.Series[1].Points.AddXY(x, y);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.chart2.Series[1].Points.Clear();
            this.chart2.Series[0].Points.Clear();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage("help");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://matecos.ru/mat/matematika/krivye-vtorogo-poryadka.html");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.kontrolnaya-rabota.ru/diario/174-opredelit-vid-krivoj-2-go-poryadka-onlajn/");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://portal.tpu.ru/SHARED/r/ROZHKOVA/page-3/page-5/Tab1/RSV-HM_Lecture-14-16.pdf");
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://kvm.gubkin.ru/pub/dlb/metodichka.pdf");
        }


    }
}
