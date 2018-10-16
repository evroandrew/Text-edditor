using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.Menu
{
    public partial class ColorChose : Form
    {
        int btnSize;
        int btnMargin = 10;
        Color[] myColor = new Color[] {Color.Blue, Color.Black, Color.Brown, Color.Red,
                                        Color.White, Color.Green, Color.Gray, Color.Yellow,
                                        Color.YellowGreen, Color.HotPink, Color.Violet, Color.Tan,
                                        Color.SteelBlue, Color.SkyBlue, Color.RoyalBlue, Color.Indigo};
        public Color temp;
        public ColorChose()
        {
            InitializeComponent();

            btnSize = ClientSize.Width > ClientSize.Height ?
               (ClientSize.Height - btnMargin) / 4 - btnMargin :
               (ClientSize.Width - btnMargin) / 4 - btnMargin;
        }

        private void ColorChose_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    new Button
                    {
                        Parent = this,
                        Text = "",
                        BackColor = myColor[j * 4 + i],
                        Width = btnSize,
                        Height = btnSize,
                        Location = new Point
                        {
                            X = btnMargin + (btnSize + btnMargin) * i,
                            Y = btnMargin + (btnSize + btnMargin) * j
                        }
                    }.Click += btnClick;
                }
        }
        private void btnClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Button btn = sender as Button;
            temp = btn.BackColor;
            Close();
        }
    }
}
