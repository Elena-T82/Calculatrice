using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculatrice
{
    public partial class Accueil : Form
    {
        List<string> operateurs = new List<string>();
        List<string> nombres = new List<string>();

        double resultat = 0;

        public Accueil()
        {
            InitializeComponent();
        }
        private void Accueil_Load(object sender, EventArgs e)
        {
        }

        private void Chiffre_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (TextResultat.Text == "0")
            {
                TextResultat.Text = button.Text;
            }
            else
            {
                TextResultat.Text += button.Text;
            }            
        }

        private void Operateur_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (nombres.Count > 0)
            {
                operateurs.Insert(0, "-");
            }
            
            TextResultat.Text += button.Text;
          
            operateurs.Add(button.Text);
            
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            TextResultat.Clear();
            nombres.Clear();
            operateurs.Clear();
        }

        private void BtnEgal_Click(object sender, EventArgs e)
        {
            if (TextResultat.Text[0] == '-')
            {
                TextResultat.Text = 0 + TextResultat.Text;
                
            }

            nombres = TextResultat.Text.Split(new char[] { '+', '-', 'x', '/' }).ToList();

            for (int i = 0; i < nombres.Count; i++)
            {
                if (nombres[i][0] == '√')
                {
                    nombres[i] = Math.Sqrt(Convert.ToDouble(nombres[i].Substring(1))).ToString();
                    resultat = Convert.ToDouble(nombres[i]);
                }
            }


            while (operateurs.Count != 0)
            {                 
                for (int i = 0; i < operateurs.Count; i++)
                {

                    if (operateurs[i] == "x")
                    {
                        resultat = Convert.ToDouble(nombres[i]) * Convert.ToDouble(nombres[i + 1]);
                        nombres[i] = resultat.ToString();
                        nombres.RemoveAt(i + 1);
                        operateurs.RemoveAt(i);
                    }

                    else if (operateurs[i] == "/")
                    {
                         try
                         {
                            if (nombres[i + 1] == "0")
                            {
                                throw new Exception();
                            }
                                resultat = Convert.ToDouble(nombres[i]) / Convert.ToDouble(nombres[i + 1]);
                                nombres.RemoveAt(i);
                                operateurs.RemoveAt(i);
                                nombres[i] = resultat.ToString();                           
                         }

                        catch (Exception)
                        {
                            TextResultat.Clear();
                            nombres.Clear();
                            operateurs.Clear();
                            resultat = 0;
                            MessageBox.Show("Impossible de diviser par zéro !");
                        }
                    }
                }
                
                for (int i = 0; i < operateurs.Count; i++)
                {
                    if (operateurs[i] == "+")
                    {
                        resultat = Convert.ToDouble(nombres[i]) + Convert.ToDouble(nombres[i + 1]);
                        nombres.RemoveAt(i + 1);
                        operateurs.RemoveAt(i);
                        nombres[i] = resultat.ToString();
                    }
                    else if (operateurs[i] == "-")
                    {
                        resultat = Convert.ToDouble(nombres[i]) - Convert.ToDouble(nombres[i + 1]);
                        nombres.RemoveAt(i + 1);
                        operateurs.RemoveAt(i);
                        nombres[i] = resultat.ToString();
                    }
                }
            }           
                TextResultat.Text = resultat.ToString();
        }

        private void BtnPoint_Click(object sender, EventArgs e)
        {
            TextResultat.Text += ",";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TextResultat.Text += "√";
        }
    }
}
