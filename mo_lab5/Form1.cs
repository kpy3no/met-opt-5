using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mo_lab5
{
    public partial class Form1 : Form
    {
        double c1, c2, c3, c4, c5;
        double x, y;
        double eps;

        public Form1()
        {
            InitializeComponent();
        }

        double dx(double x, double y) //Производная по x
        {
            return ((c1 * 3 * x * x) + (c2 * 2 * x) + (c3 * y));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        double dy(double x, double y) //Производная по y
        {
            return ((c3 * x) + (c4 * 2 * y) + (c5 * 3 * y * y));
        }

        double ddxx(double x) //Две производные по x
        {
            return ((c1 * 6 * x) + (c2 * 2));
        }

        double ddyy(double y) //Две производные по y
        {
            return ((c4 * 2) + (c5 * 6 * y));
        }

        double det(double x, double y) //Определитель гессиана
        {
            return ((ddxx(x) * ddyy(y)) - (c3 * c3));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int k = 0, r = 0;
            double g1, g2;
            double prev_x, prev_y; //Предыдущие x, y
            double grad; //Длина вектора градиента

            c1 = Convert.ToDouble(textBox1.Text);
            c2 = Convert.ToDouble(textBox2.Text);
            c3 = Convert.ToDouble(textBox3.Text);
            c4 = Convert.ToDouble(textBox4.Text);
            c5 = Convert.ToDouble(textBox5.Text);

            x = Convert.ToDouble(textBox6.Text);
            y = Convert.ToDouble(textBox7.Text);
            eps = Convert.ToDouble(textBox8.Text);


            prev_x = x;
            prev_y = y;


            console.Clear();
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();

            chart1.Series[0].Points.AddXY(x, y);
            chart1.Series[1].Points.AddXY(x, y);

            while (k != 1000)
            {
                k++;

                g1 = dx(x, y);
                g2 = dy(x, y);

                r += 2;


                grad = Math.Sqrt(g1 * g1 + g2 * g2);
                //grad = 1;
                if (grad < eps) //Если градиент меньше eps, прерываем цикл
                {
                    console.AppendText("Количество итераций: " + k + "\n");
                    console.AppendText("Количество вычислений: " + r + "\n");
                    console.AppendText("Предыдущая точка: (" + Convert.ToString(prev_x) + "; " + Convert.ToString(prev_y) + ")\n");
                    /*
                        if(det(x, y) > 0 && grad != 0)
                        {
                            console.AppendText("Локальный минимум: (" + Convert.ToString(x) + "; " + Convert.ToString(y) + ")\n");
                        }
                        else if(det(x, y) < 0 && grad != 0)
                        {
                            console.AppendText("Локальный максимум: (" + Convert.ToString(x) + "; " + Convert.ToString(y) + ")\n");
                        }
                        else if(det(x, y) > 0 && grad == 0)
                        {
                            console.AppendText("Глобальный минимум: (" + Convert.ToString(x) + "; " + Convert.ToString(y) + ")\n");
                        }
                        else if(det(x, y) < 0 && grad == 0)
                        {
                            console.AppendText("Глобальный максимум: (" + Convert.ToString(x) + "; " + Convert.ToString(y) + ")\n");
                        }
                        else
                        {
                            console.AppendText("Седловая точка: (" + Convert.ToString(x) + "; " + Convert.ToString(y) + ")\n");
                        }
                        */

                    if (det(x, y) > 0 && grad != 0)
                    {
                        console.AppendText("Локальный минимум: (" + Convert.ToString(x) + "; " + Convert.ToString(y) + ")\n");
                    }
                    else if (det(x, y) < 0 && grad != 0)
                    {
                        console.AppendText("Локальный максимум: (" + Convert.ToString(x) + "; " + Convert.ToString(y) + ")\n");
                    }
                    else
                    {
                        console.AppendText("Точка минимума: (" + Convert.ToString(x) + "; " + Convert.ToString(y) + ")\n");
                    }

                    break;
                }

                prev_x = x;
                prev_y = y;

                //x = prev_x - ((ddyy(prev_y) * g1 - g2) / det(prev_x, prev_y));
                //y = prev_y - ((-g1 + ddxx(prev_x) * g2) / det(prev_x, prev_y));
                x = prev_x - ((ddyy(prev_y) * g1 - c3 * g2) / det(prev_x, prev_y)); //Вычисление новых значений x и y
                y = prev_y - ((-c3 * g1 + ddxx(prev_x) * g2) / det(prev_x, prev_y));

                chart1.Series[0].Points.Add(x, y);

                if (k < 21)
                {
                    console.AppendText("Итерация: " + k + "\n");
                    console.AppendText("Текущая точка: (" + Convert.ToString(x) + "; " + Convert.ToString(y) + ")\n");
                    console.AppendText("Гессиан функции: " + Convert.ToString(det(x, y)) + "\n");
                    console.AppendText("\n");
                }
                

                r += 2;
            }

            chart1.Series[0].Points.AddXY(x, y);
            chart1.Series[1].Points.AddXY(x, y);

            //chart1.Series[1].Points.AddXY(x, y - 1);

            if (k == 1000)
            {
                console.AppendText("Количество итераций: " + k + "\n");
                console.AppendText("Количество вычислений: " + r + "\n");
                console.AppendText("Последняя найденная точка: (" + Convert.ToString(x) + "; " + Convert.ToString(y) + ")\n");
                console.AppendText("Гессиан функции: " + Convert.ToString(det(x, y)) + "\n");
            }

            //console.AppendText("Точка минимума: (" + Convert.ToString(x) + "; " + Convert.ToString(y) + ")\n");
        }
    }
}
