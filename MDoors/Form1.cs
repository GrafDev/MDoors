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
            this.checkBox2.Checked = Start.flagsPlace;
            this.checkBox1.Checked = Start.dialogBoxShow;
        }

        private void button_Ok_Click(object sender, EventArgs e)
        {
            Start.flagsPlace= this.checkBox2.Checked;
            Start.dialogBoxShow = this.checkBox1.Checked;
            this.Close();
        }

    }
}
