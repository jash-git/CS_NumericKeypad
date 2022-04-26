using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_VPOS
{
    //https://www.cnblogs.com/JiYF/p/9047559.html
    public partial class PanelEx : Panel
    {
        private Color _BorderColor = Color.Black;

        [Browsable(true), Description("边框颜色"), Category("自定义分组")]
        public Color BorderColor
        {
            get { return _BorderColor; }
            set
            {
                _BorderColor = value;
                this.Invalidate();
            }
        }

        private int _BorderSize = 1;

        [Browsable(true), Description("边框粗细"), Category("自定义分组")]
        public int BorderSize
        {
            get { return _BorderSize; }
            set
            {
                _BorderSize = value;
                this.Invalidate();
            }
        }


        public PanelEx()
        {
            InitializeComponent();
        }

        public PanelEx(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// 重写OnPaint方法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                            this.ClientRectangle,
                            this._BorderColor,
                            this._BorderSize,
                            ButtonBorderStyle.Solid,
                            this._BorderColor,
                            this._BorderSize,
                            ButtonBorderStyle.Solid,
                           this._BorderColor,
                            this._BorderSize,
                            ButtonBorderStyle.Solid,
                            this._BorderColor,
                            this._BorderSize,
                            ButtonBorderStyle.Solid);
        }



    }
}
