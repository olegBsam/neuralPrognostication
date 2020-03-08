using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagDiplom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.DragDrop += Button1_DragDrop;
            button1.MouseDown += Button1_MouseDown;
            DragEnter += Form1_DragEnter;
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            var form = sender as Control;
            var type =(Button) e.Data.GetData(typeof(Button));
            type.Location = new Point(Cursor.Position.X - form.Location.X,Cursor.Position.Y - form.Location.Y);
        }

        private void Button1_MouseDown(object sender, MouseEventArgs e)
        {
            DoDragDrop(sender as Control, DragDropEffects.Move);
        }

        private void Button1_DragDrop(object sender, DragEventArgs e)
        {
            var g = 12;
        }

    }
}
