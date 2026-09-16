using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator
{
    public partial class frmMain : Form
    {
        private Label resultLabel;
        static Color OPERATION_BG = Color.LightGray;
        static Color NUMBER_BG = Color.WhiteSmoke;
        static Color EQUAL_BG = Color.LightSeaGreen;

        private BtnStruct[,] buttons =
        {
            {new BtnStruct('%', OPERATION_BG), new BtnStruct('\u0152', OPERATION_BG), new BtnStruct('C', OPERATION_BG), new BtnStruct('\u232B', OPERATION_BG) },
            {new BtnStruct('\u215F', OPERATION_BG), new BtnStruct('\u00B2', OPERATION_BG), new BtnStruct('\u221A', OPERATION_BG), new BtnStruct('\u00F7', OPERATION_BG) },
            {new BtnStruct('7', NUMBER_BG), new BtnStruct('8', NUMBER_BG), new BtnStruct('9', NUMBER_BG), new BtnStruct('x', OPERATION_BG) },
            {new BtnStruct('4', NUMBER_BG), new BtnStruct('5', NUMBER_BG), new BtnStruct('6', NUMBER_BG), new BtnStruct('-', OPERATION_BG) },
            {new BtnStruct('1', NUMBER_BG), new BtnStruct('2', NUMBER_BG), new BtnStruct('3', NUMBER_BG), new BtnStruct('+', OPERATION_BG) },
            {new BtnStruct('\u00B1', NUMBER_BG), new BtnStruct('0', NUMBER_BG), new BtnStruct(',', NUMBER_BG), new BtnStruct('=', EQUAL_BG) }
        };

        public struct BtnStruct
        {
            public char Content;
            public Color BgColor;

            public BtnStruct(char content, Color bgColor)
            {
                this.Content = content;
                this.BgColor = bgColor;
            }
            
            public override string ToString()
            {
                return base.ToString();
            }
        }

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            MakeResultLabels();
            MakeButtons();
        }

        private void MakeResultLabels()
        {
            resultLabel = new Label()
            {
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight,
                AutoSize = false,
                Location = new Point(0, 0),
                Size = new Size(this.Width, 100)
            };
            Controls.Add(resultLabel);
        }

        private void MakeButtons()
        {
            int btnWidth = 80, btnHeight = 60;
            int posY = 106;
            for (int i = 0; i < buttons.GetLength(0); i++)   
            {
                int posX = 0;
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    Button btn = new Button();
                    btn.Width = btnWidth;
                    btn.Height = btnHeight;
                    btn.Left = posX;
                    btn.Top = posY;
                    btn.Font = new Font("Segoe UI", 16);
                    btn.Text = buttons[i,j].ToString();
                    btn.BackColor = buttons[i, j].BgColor;
                    btn.Click += Btn_Click1;
                    Controls.Add(btn);
                    posX += btnWidth;
                }
                posY += btnHeight;
            }
            
        }

        private void Btn_Click1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}
