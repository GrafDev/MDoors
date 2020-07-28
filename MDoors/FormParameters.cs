using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MDoors
{
    public partial class FormParameters : Form
    {
        public FormParameters()
        {
            InitializeComponent();
            this.checkBoxShow.Checked = Start.dialogBoxShow;//Установка существующих значений
            this.checkBoxMarks.Checked = Start.flagsPlace;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start.dialogBoxShow = this.checkBoxShow.Checked;//Присвоение праметрам выбраных значений
            Start.flagsPlace = this.checkBoxMarks.Checked;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            FormAbout aboutForm = new FormAbout();// Задает член класса Форм
            aboutForm.Show();   // Показывает член формы

        }
    }
}
