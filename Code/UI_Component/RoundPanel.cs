using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CS_VPOS
{
    //https://blog.csdn.net/breakbridge/article/details/113403842
    public partial class RoundPanel : Panel
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hwnd);
        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);
        Color _borderColor = Color.Black;
        int _borderWidth = 1;


        [Description("组件的边框颜色。"), Category("Appearance")]
        public Color BorderColor
        {
            get
            {
                return _borderColor;
            }
            set
            {
                _borderColor = value;
                this.Invalidate();
            }
        }


        [Description("组件的边框宽度。"), Category("Appearance")]
        public int BorderWidth
        {
            get
            {
                return _borderWidth;
            }
            set
            {
                _borderWidth = value;
                this.Invalidate();
            }
        }

        Color _currBColor = Color.Empty;
        public void SetBackColorImg(Color color)
        {
            if (_currBColor != color)
            {
                drawBackColor(100, color);
                _currBColor = color;
            }
        }

        public RoundPanel()
        {
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, false);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Paint += new PaintEventHandler(PanelEx_Paint);
        }

        /// <summary>
        /// 绘制背景色，以图片填充
        /// </summary>
        void drawBackColor(int percentage, Color drawColor)
        {
            percentage = Math.Min(percentage, 100);
            Bitmap b = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(b);
            int height = (int)(this.Height * (double)((100 - percentage) / (double)100));
            Rectangle rectangle = new Rectangle(new Point(0, height), new Size(this.Width, this.Height));
            g.DrawRectangle(new Pen(drawColor), rectangle);
            g.FillRectangle(new SolidBrush(drawColor), rectangle);
            g.Dispose();
            this.BackgroundImage = b;
        }

        [Browsable(true)]
        [Category("外观"), Description("生成该颜色的图片填充背景图片")]
        public Color BackImgColor
        {
            get { return _currBColor; }
            set
            {
                SetBackColorImg(value);
            }
        }

        void PanelEx_Paint(object sender, PaintEventArgs e)
        {
            if (this.BorderStyle == BorderStyle.FixedSingle)
            {
                IntPtr hDC = GetWindowDC(this.Handle);
                Graphics g = Graphics.FromHdc(hDC);
                ControlPaint.DrawBorder(
                 g,
                 new Rectangle(0, 0, this.Width, this.Height),
                 _borderColor,
                 _borderWidth,
                 ButtonBorderStyle.Solid,
                 _borderColor,
                 _borderWidth,
                 ButtonBorderStyle.Solid,
                 _borderColor,
                 _borderWidth,
                 ButtonBorderStyle.Solid,
                 _borderColor,
                 _borderWidth,
                 ButtonBorderStyle.Solid);
                g.Dispose();
                ReleaseDC(Handle, hDC);
            }
        }

        //<summary>
        //解决加载闪烁，背景透明等问题
        //</summary>
        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000;
                return parms;
            }
        }

        uint _topLeftRadius;
        [Browsable(true)]
        [Category("倒角"), Description("左上圆角弧度(0为不要圆角)")]
        public uint TopLeftRadius
        {
            get
            {
                return _topLeftRadius;
            }
            set
            {
                _topLeftRadius = value;
                radiusChanged(value);
            }
        }

        uint _topRightRadius;
        [Browsable(true)]
        [Category("倒角"), Description("右上圆角弧度(0为不要圆角)")]
        public uint TopRightRadius
        {
            get
            {
                return _topRightRadius;
            }
            set
            {
                _topRightRadius = value;
                radiusChanged(value);
            }
        }

        uint _bottomLeftRadius;
        [Browsable(true)]
        [Category("倒角"), Description("左下圆角弧度(0为不要圆角)")]
        public uint BottomLeftRadius
        {
            get
            {
                return _bottomLeftRadius;
            }
            set
            {
                _bottomLeftRadius = value;
                radiusChanged(value);
            }
        }

        uint _bottomRightRadius;
        [Browsable(true)]
        [Category("倒角"), Description("右下圆角弧度(0为不要圆角)")]
        public uint BottomRightRadius
        {
            get
            {
                return _bottomRightRadius;
            }
            set
            {
                _bottomRightRadius = value;
                radiusChanged(value);
            }
        }

        void radiusChanged(uint radius)
        {
            base.Refresh();
        }

        uint _allRound = 0;
        [Browsable(true)]
        [Category("倒角"), Description("统一圆角弧度")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public uint AllRound
        {
            get { return _allRound; }
            set
            {
                _allRound = value;
                if (value != 0)
                {
                    _topLeftRadius = _allRound;
                    _topRightRadius = _allRound;
                    _bottomLeftRadius = _allRound;
                    _bottomRightRadius = _allRound;
                    radiusChanged(value);
                }
            }
        }

        // 圆角代码
        public void Round(System.Drawing.Region region)
        {
            System.Drawing.Drawing2D.GraphicsPath oPath = new System.Drawing.Drawing2D.GraphicsPath();
            int x = 0;
            int y = 0;
            int thisWidth = this.Width;
            int thisHeight = this.Height;
            if (_topLeftRadius > 0)
            {
                oPath.AddArc(x, y, _topLeftRadius, _topLeftRadius, 180, 90);                                 // 左上角
            }
            oPath.AddLine(x + _topLeftRadius, y, thisWidth - _topRightRadius, y);                         // 顶端
            if (_topRightRadius > 0)
            {
                oPath.AddArc(thisWidth - _topRightRadius, y, _topRightRadius, _topRightRadius, 270, 90);                 // 右上角
            }
            oPath.AddLine(thisWidth, y + _topRightRadius, thisWidth, thisHeight - _bottomRightRadius);        // 右边
            if (_bottomRightRadius > 0)
            {
                oPath.AddArc(thisWidth - _bottomRightRadius, thisHeight - _bottomRightRadius, _bottomRightRadius, _bottomRightRadius, 0, 90);  // 右下角
            }
            oPath.AddLine(thisWidth - _bottomRightRadius, thisHeight, x + _bottomLeftRadius, thisHeight);       // 底边
            if (_bottomLeftRadius > 0)
            {
                oPath.AddArc(x, thisHeight - _bottomLeftRadius, _bottomLeftRadius, _bottomLeftRadius, 90, 90);                 // 左下角
            }
            oPath.AddLine(x, thisHeight - _bottomLeftRadius, x, y + _topLeftRadius);                        // 左边

            oPath.CloseAllFigures();
            this.Region = new System.Drawing.Region(oPath);
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            Round(this.Region);  // 圆角
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            base.Refresh();
        }
    }
}
