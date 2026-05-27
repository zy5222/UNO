using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UNO
{
    public partial class frmColorPicker : Form
    {
        public CardColor ChosenColor { get; private set; } = CardColor.Red;
        public frmColorPicker()
        {
            InitializeComponent();
        }

        private void btnRed_Click(object sender, EventArgs e)
        {
            ChosenColor = CardColor.Red;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnYellow_Click(object sender, EventArgs e)
        {
            ChosenColor = CardColor.Yellow;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnGreen_Click(object sender, EventArgs e)
        {
            ChosenColor = CardColor.Green;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnBlue_Click(object sender, EventArgs e)
        {
            ChosenColor = CardColor.Blue;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
