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
            this.checkBox2.Checked = Start.flagsPlace;//Установка параметра маркировки отзеркаленных дверей
            this.checkBox1.Checked = Start.dialogBoxShow;//Установка параметра показа диалоговых окон
        }// Инициализация компонентов формы и задание предварительных параметров

        private void button_Ok_Click(object sender, EventArgs e)//Дейстивя при нажания ОК.
        {
            Start.flagsPlace= this.checkBox2.Checked; // Возвращение из диалогового окна значения для параметра маркировки отзеркаленых дверей
            Start.dialogBoxShow = this.checkBox1.Checked;// Возвращение из диалогового окна занчения для параметра показа диалоговоых окон
            this.Close();
        }

    }
}
