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
        //---
        //C# WinForm 控制項隨視窗大小變化而比例改變_start00
        // ~ https://dotblogs.com.tw/davidtalk/2018/06/03/182559
        // ~ https://www.itread01.com/content/1547625088.html
        //---C# WinForm 控制項隨視窗大小變化而比例改變_end00

        //---
        //C# WinForm 控制項隨視窗大小變化而比例改變_start01
        private float m_X;//當前窗體的寬度
        private float m_Y;//當前窗體的高度
        private bool m_isLoaded;  // 是否已設定各控制的尺寸資料到Tag屬性
        //---C# WinForm 控制項隨視窗大小變化而比例改變_end01

        //---
        //C# WinForm 控制項隨視窗大小變化而比例改變_start02
        //將控制項的寬，高，左邊距，頂邊距和字體大小暫存到tag屬性中
        private void SetTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    SetTag(con);
            }
        }
        //---C# WinForm 控制項隨視窗大小變化而比例改變_end02

        //---
        //C# WinForm 控制項隨視窗大小變化而比例改變_start03
        //根據窗體大小調整控制項大小
        private void SetControls(float newx, float newy, Control cons)
        {
            if (m_isLoaded)
            {
                //遍歷窗體中的控制項，重新設置控制項的值
                foreach (Control con in cons.Controls)
                {
                    string[] mytag = con.Tag.ToString().Split(new char[] { ':' });//獲取控制項的Tag屬性值，並分割後存儲字元串數組
                    float a = System.Convert.ToSingle(mytag[0]) * newx;//根據窗體縮放比例確定控制項的值，寬度
                    con.Width = (int)a;//寬度
                    a = System.Convert.ToSingle(mytag[1]) * newy;//高度
                    con.Height = (int)(a);
                    a = System.Convert.ToSingle(mytag[2]) * newx;//左邊距離
                    con.Left = (int)(a);
                    a = System.Convert.ToSingle(mytag[3]) * newy;//上邊緣距離
                    con.Top = (int)(a);
                    Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//字體大小
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    if (con.Controls.Count > 0)
                    {
                        SetControls(newx, newy, con);
                    }
                }
            }
        }
        //---C# WinForm 控制項隨視窗大小變化而比例改變_end03

        public bool m_blnRun=false;
        public String m_StrResult;
        public RoundButton[] m_ButNum = new RoundButton[11];
        public NumericKeypad()
        {
            InitializeComponent();
            //---
            //C# WinForm 控制項隨視窗大小變化而比例改變_start05
            m_isLoaded = false;//
            //---C# WinForm 控制項隨視窗大小變化而比例改變_end05
        }
        private void NumericKeypad_Load(object sender, EventArgs e)
        {
            //---
            //C# WinForm 控制項隨視窗大小變化而比例改變_start04
            m_X = this.Width;//獲取窗體的寬度
            m_Y = this.Height;//獲取窗體的高度
            m_isLoaded = true;// 已設定各控制項的尺寸到Tag屬性中
            SetTag(this);//調用方法
            //---C# WinForm 控制項隨視窗大小變化而比例改變_end04

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

        private void NumericKeypad_Shown(object sender, EventArgs e)//第一次顯示表單
        {
            //---
            //C# WinForm 控制項隨視窗大小變化而比例改變_start06
            this.WindowState = FormWindowState.Normal;//FormWindowState.Maximized;
            //---C# WinForm 控制項隨視窗大小變化而比例改變_end06
        }

        private void NumericKeypad_SizeChanged(object sender, EventArgs e)
        {
            //---
            //C# WinForm 控制項隨視窗大小變化而比例改變_start07
            if (m_isLoaded)
            {
                float newx = (this.Width) / m_X;
                float newy = (this.Height) / m_Y;
                SetControls(newx, newy, this);
            }
            //---C# WinForm 控制項隨視窗大小變化而比例改變_end07
        }
    }
}
