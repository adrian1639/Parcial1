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

namespace Parcial1
{
    public partial class Form1 : Form
    {
        List<Dep> dp = new List<Dep>();
        List<InfoDep> indp = new List<InfoDep>();

        internal List<Dep> Dp { get => dp; set => dp = value; }
        internal List<InfoDep> Indp { get => indp; set => indp = value; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Leer();
            Leer2();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Dp;
            dataGridView1.Refresh();

            dataGridView2.DataSource = null;
            dataGridView2.DataSource = Indp;
            dataGridView2.Refresh();
        }

        void Guardar()
        {
            FileStream stream = new FileStream("Departamento.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);


            foreach (var p in Dp)
            {
                writer.WriteLine(p.Departamento);
                writer.WriteLine(p.Identificacion);
            }
            writer.Close();
        }
        void Guardar2()
        {
            FileStream stream = new FileStream("InfoDep.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);


            foreach (var p in Indp)
            {
                writer.WriteLine(p.Temp);
                writer.WriteLine(p.Fecha);
                writer.WriteLine(p.Identificacion2);
            }
            writer.Close();
        }
        void Leer()
        {
            FileStream stream = new FileStream("Departamento.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Dep datos = new Dep();
                datos.Departamento = reader.ReadLine();
                datos.Identificacion = reader.ReadLine();
                Dp.Add(datos);
            }
            //Cerrar el archivo, esta linea es importante porque sino despues de correr varias veces el programa daría error de que el archivo quedó abierto muchas veces. Entonces es necesario cerrarlo despues de terminar de leerlo.
            reader.Close();
        }
        void Leer2()
        {
            FileStream stream = new FileStream("Infodep.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                InfoDep datos = new InfoDep();
                datos.Temp = reader.ReadLine();
                datos.Fecha = reader.ReadLine();
                datos.Identificacion2 = reader.ReadLine();
                Indp.Add(datos);
            }
            //Cerrar el archivo, esta linea es importante porque sino despues de correr varias veces el programa daría error de que el archivo quedó abierto muchas veces. Entonces es necesario cerrarlo despues de terminar de leerlo.
            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool existe = dp.Exists(v => v.Departamento == textBox1.Text);
            if (existe)
            {
                MessageBox.Show("Ese departamento ya fue ingresado", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Dep per = new Dep();
                per.Departamento = textBox1.Text;
                per.Identificacion = textBox2.Text;

                Dp.Add(per);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = Dp;
                dataGridView1.Refresh();

                Guardar();
            }
        }
    }
}
