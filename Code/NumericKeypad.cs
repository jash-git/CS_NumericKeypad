using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_VPOS
{
    public partial class NumericKeypad : Form
    {
        public bool m_blnRun=false;
        public String m_StrResult;
        public RoundButton[] m_ButNum = new RoundButton[11];
        public NumericKeypad()
        {
            InitializeComponent();
        }
        private void NumericKeypad_Load(object sender, EventArgs e)
        {
            NKbut00.m_intSID = 0;
            NKbut01.m_intSID = 1;
            NKbut02.m_intSID = 2;
            NKbut03.m_intSID = 3;
            NKbut04.m_intSID = 4;
            NKbut05.m_intSID = 5;
            NKbut06.m_intSID = 6;
            NKbut07.m_intSID = 7;
            NKbut08.m_intSID = 8;
            NKbut09.m_intSID = 9;
            NKbut10.m_intSID = 10;

            m_ButNum[0] = NKbut00;
            m_ButNum[1] = NKbut01;
            m_ButNum[2] = NKbut02;
            m_ButNum[3] = NKbut03;
            m_ButNum[4] = NKbut04;
            m_ButNum[5] = NKbut05;
            m_ButNum[6] = NKbut06;
            m_ButNum[7] = NKbut07;
            m_ButNum[8] = NKbut08;
            m_ButNum[9] = NKbut09;
            m_ButNum[10] = NKbut10;//dot
            m_blnRun = false;
            m_StrResult = "1";
            NKbut13.Focus();

        }

        private void NKbut11_Click(object sender, EventArgs e)//AC
        {
            NKtxt001.Text="0";
        }

        private void Cancel_Click(object sender, EventArgs e)//X 取消
        {
            m_blnRun = false;
            this.Close();
        }

        private void OK_Click(object sender, EventArgs e)//確定
        {
            if(NKtxt001.Text!="0")
            {
                m_blnRun = true;
                m_StrResult = NKtxt001.Text;
                this.Close();
            }
            else
            {
                m_blnRun = false;
                this.Close();
            }
        }

        private void Num_Click(object sender, EventArgs e)
        {
            RoundButton RoundButtonBuf=(RoundButton)sender;
            int index = -1;
            for(int i = 0; i < m_ButNum.Length; i++)
            {
                if(m_ButNum[i]== RoundButtonBuf)
                {
                    index=i;
                    break;
                }
            }

            if(index!=10)
            {
                if ((NKtxt001.Text.Length == 1) && (NKtxt001.Text=="0"))
                {
                    NKtxt001.Text = ""+ index;
                }
                else
                {
                    NKtxt001.Text += ""+ index;
                }
            }

            for (int i = 0; i < m_ButNum.Length; i++)
            {
                m_ButNum[i].m_blnclicked = false;
            }
        }
    }
}
