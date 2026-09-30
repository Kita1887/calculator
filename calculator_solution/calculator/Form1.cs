using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator
{
    public partial class frmMain : Form
    {
        private Label resultLabel;

        static private readonly Color OPERATOR_BG = Color.LightGray;
        static private readonly Color NUMBER_BG = Color.WhiteSmoke;
        static private readonly Color EQUAL_BG = Color.MidnightBlue;

        public enum SymbolType
        {
            Number,
            Operator,
            EqualSign,
            DecimalPoint,
            PlusMinusSign,
            BackSpace,
            ClearAll,
            ClearEntry,
            Undefined
        }


        public struct BtnStruct
        {
            public char Content;
            public SymbolType Type;
            public BtnStruct(char content, SymbolType type = SymbolType.Undefined)
            {
                this.Content = content;
                this.Type = type;
            }

            public override string ToString()
            {
                return Content.ToString();
            }

        }
        private readonly BtnStruct[,] buttons =
        {
            { new BtnStruct('%'), new BtnStruct('\u0152', SymbolType.ClearEntry), new BtnStruct('C', SymbolType.ClearAll), new BtnStruct('\u232B', SymbolType.BackSpace) },
            { new BtnStruct('\u215F'), new BtnStruct('\u00B2'), new BtnStruct('\u221a'), new BtnStruct('\u00F7', SymbolType.Operator) },
            { new BtnStruct('7', SymbolType.Number), new BtnStruct('8', SymbolType.Number), new BtnStruct('9', SymbolType.Number), new BtnStruct('x', SymbolType.Operator) },
            {new BtnStruct('4', SymbolType.Number), new BtnStruct('5', SymbolType.Number), new BtnStruct('6', SymbolType.Number), new BtnStruct('-', SymbolType.Operator)},
            {new BtnStruct('1', SymbolType.Number), new BtnStruct('2', SymbolType.Number), new BtnStruct('3', SymbolType.Number), new BtnStruct('+', SymbolType.Operator)},
            {new BtnStruct('\u00B1', SymbolType.PlusMinusSign), new BtnStruct('0', SymbolType.Number), new BtnStruct(',', SymbolType.DecimalPoint), new BtnStruct('=', SymbolType.EqualSign)}

        };

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            MakeResultLabel();
            MakeButtons();
        }

        private void MakeResultLabel()
        {
            resultLabel = new Label()
            {
                Font = new Font("Segoe UI", 34, FontStyle.Bold),
                Text = "0",
                TextAlign = ContentAlignment.MiddleRight,
                AutoSize = false,
                Location = new Point(-10, 0),
                Size = new Size(this.Width, 100),
                BackColor = Color.Beige
            };
            resultLabel.TextChanged += ResultLabel_TextChanged;
            Controls.Add(resultLabel);
        }

        private void ResultLabel_TextChanged(object sender, EventArgs e)
        {
            if (resultLabel.Text.Length > 0)
            {
                decimal num = decimal.Parse(resultLabel.Text);
                NumberFormatInfo nfi = new CultureInfo("it-IT", false).NumberFormat;
                int decimalSeparatorPosition = resultLabel.Text.IndexOf(',');
                if (decimalSeparatorPosition == -1)
                    nfi.NumberDecimalDigits = 0;
                else
                    nfi.NumberDecimalDigits = resultLabel.Text.Length - decimalSeparatorPosition - 1;
                string stOut = num.ToString("N", nfi);
                if (decimalSeparatorPosition == resultLabel.Text.Length - 1)
                    stOut += ",";
                resultLabel.Text = stOut;
            }

            if (resultLabel.Text.Length > 16)
                resultLabel.Text = resultLabel.Text.Substring(0, 17);

            if (resultLabel.Text.Length > 11)
            {
                float delta = (resultLabel.Text.Length - 11) * (float)2.8;
                resultLabel.Font = new Font("Segoe UI", 34 - delta, FontStyle.Bold);
            }
            else
                resultLabel.Font = new Font("Segoe UI", 34, FontStyle.Bold);
        }

        private void MakeButtons()
        {
            int btnWidth = 80, btnHeight = 60;
            int posY = 118;
            for (int i = 0; i < buttons.GetLength(0); i++) //righe
            {
                int posX = 0;
                for (int j = 0; j < buttons.GetLength(1); j++) //colonne
                {
                    Button btn = new Button();
                    btn.Width = btnWidth;
                    btn.Height = btnHeight;
                    btn.Top = posY;
                    btn.Left = posX;
                    btn.Font = new Font("Segoe UI", 16);
                    btn.Text = buttons[i, j].ToString();
                    switch (buttons[i, j].Type)
                    {
                        case SymbolType.Number:
                        case SymbolType.DecimalPoint:
                        case SymbolType.PlusMinusSign:
                            btn.BackColor = NUMBER_BG;
                            break;
                        case SymbolType.Operator:
                        case SymbolType.BackSpace:
                        case SymbolType.ClearAll:
                        case SymbolType.ClearEntry:
                            btn.BackColor = OPERATOR_BG;
                            break;
                        case SymbolType.EqualSign:
                            btn.BackColor = EQUAL_BG;
                            btn.ForeColor = NUMBER_BG;
                            btn.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                            break;

                    }
                    btn.Tag = buttons[i, j];
                    btn.Click += Btn_Click;
                    Controls.Add(btn);
                    posX += btnWidth;

                }
                posY += btnHeight;
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            BtnStruct clickedButtonStruct = (BtnStruct)clickedButton.Tag;
            switch (clickedButtonStruct.Type)
            {
                case SymbolType.Number:
                    if (resultLabel.Text == "0") resultLabel.Text = "";
                    resultLabel.Text += clickedButtonStruct.Content;
                    break;
                case SymbolType.DecimalPoint:
                    if (!resultLabel.Text.Contains(","))
                        resultLabel.Text += clickedButtonStruct.Content;
                    break;
                case SymbolType.PlusMinusSign:
                    if (resultLabel.Text != "0")
                    {
                        if (!resultLabel.Text.Contains("-"))
                            resultLabel.Text = "-" + resultLabel.Text;
                        else
                            resultLabel.Text = resultLabel.Text.Replace("-", "");
                    }
                    break;
                case SymbolType.Operator:
                    break;
                case SymbolType.BackSpace:
                    if (resultLabel.Text.Length > 1 && resultLabel.Text[0] != '-' || resultLabel.Text[0] == '-' && resultLabel.Text.Length > 2)
                        resultLabel.Text = resultLabel.Text.Substring(0, resultLabel.Text.Length - 1);
                    else
                        resultLabel.Text = "0";
                    break;
                case SymbolType.EqualSign:
                    break;
                case SymbolType.Undefined:
                    break;
                case SymbolType.ClearAll:
                case SymbolType.ClearEntry:
                    resultLabel.Text = "0"; 
                    break;
            }

        }
    }
}
