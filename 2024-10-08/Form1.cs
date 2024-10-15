using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace _2024_10_08
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public struct listaElemek
        {
            public int id;
            public string nev;
            public string tantargy;
            public int tanitasKezdete;
            public int tanitasVege;
        }

        public listaElemek tanarokFeltoltese = new listaElemek();
        public List<listaElemek> tanarok = new List<listaElemek>();

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";


            FileStream folyam = new FileStream("adatoktanarok.txt", FileMode.Open);
            StreamReader olvas = new StreamReader(folyam, Encoding.UTF8);

            
            string[] resz;

            while (!olvas.EndOfStream)
            {
                string elso = olvas.ReadLine();

                resz = elso.Split(';');
                tanarokFeltoltese.id = Convert.ToInt32(resz[0]);
                tanarokFeltoltese.nev = resz[1];
                tanarokFeltoltese.tantargy = resz[2];
                tanarokFeltoltese.tanitasKezdete = Convert.ToInt32(resz[3]);
                tanarokFeltoltese.tanitasVege = Convert.ToInt32(resz[4]);
                tanarok.Add(tanarokFeltoltese);
            }

            int seged = tanarok[0].tanitasKezdete;
            int counter = 0;
            for (int i = 0; i < tanarok.Count; i++)
            {
                if (seged > tanarok[i].tanitasKezdete)
                {
                    seged = tanarok[i].tanitasKezdete;
                }
            }

            for (int i = 0; i < tanarok.Count; i++)
            {
                if (seged == tanarok[i].tanitasKezdete)
                {
                    counter++;
                }
            }

            //label1.Text = counter.ToString() + "\n";

            // irja ki azoknak a tanaroknak a nevet akik 86 vagy 87 ben leptek be az iskolaba

            var lek1 =
                from x in tanarok
                where x.tanitasKezdete == 1986 || x.tanitasKezdete == 1987
                select new { x.nev};

            foreach (var item in lek1)
            {
                label1.Text += "1: " + item.nev + "\n";
            }

            var lek2 = tanarok.Where(i => i.tanitasKezdete == 1986 || i.tanitasKezdete == 1987);

            foreach (var item in lek2)
            {
                label2.Text += "2: " + item.nev + "\n";
            }

            // vegytisztan matematika tanar

            var lek3 =
                from x in tanarok
                where x.tantargy == "matematika"
                select new { x.nev };

            foreach (var item in lek3)
            {
                label3.Text += "3: " + item.nev + "\n";
            }

            // mennyi időt tanitott 
            
            var lek4 =
                from x in tanarok
                where ((x.tanitasVege - x.tanitasKezdete) == tanarok.Max(y => y.tanitasVege - y.tanitasKezdete))
                select new { x.nev };

            foreach (var item in lek4)
            {
                label4.Text += "4: " + item.nev + "\n";
            }

            // hanyan tanitottak oroszt
        }
    }
}
