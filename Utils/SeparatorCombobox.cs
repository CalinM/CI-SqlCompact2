/****************************************************************************************************************
(C) Copyright 2007 Zuoliu Ding.  All Rights Reserved.
SeparatorComboBox:	Implementation class
Created by:			05/15/2004, Zuoliu Ding
Note:				For a Combo box with Separators
****************************************************************************************************************/

using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Utils
{
    public class SeparatorComboBox : ComboBox
    {
        #region Constructor
        public SeparatorComboBox()
        {
            DrawMode = DrawMode.OwnerDrawVariable;
            _separatorStyle = DashStyle.Solid;
            _separators = new ArrayList();

            _separatorStyle = DashStyle.Solid;
            _separatorColor = Color.Black;
            _separatorMargin = 1;
            _separatorWidth = 1;
            _autoAdjustItemHeight = false;
        }
        #endregion

        #region Medthods

        public void AddString(string s)
        {
            Items.Add(s);
        }

        public void AddStringWithSeparator(string s)
        {
            Items.Add(s);
            _separators.Add(s);
        }

        public void SetSeparator(int pos)
        {
            _separators.Add(pos);
        }

        #endregion

        #region Properties

        [Description("Gets or sets the Separator Style"), Category("Separator")]
        public DashStyle SeparatorStyle
        {
            get { return _separatorStyle; }
            set { _separatorStyle = value; }
        }

        [Description("Gets or sets the Separator Color"), Category("Separator")]
        public Color SeparatorColor
        {
            get { return _separatorColor; }
            set { _separatorColor = value; }
        }

        [Description("Gets or sets the Separator Width"), Category("Separator")]
        public int SeparatorWidth
        {
            get { return _separatorWidth; }
            set { _separatorWidth = value; }
        }

        [Description("Gets or sets the Separator Margin"), Category("Separator")]
        public int SeparatorMargin
        {
            get { return _separatorMargin; }
            set { _separatorMargin = value; }
        }

        [Description("Gets or sets Auto Adjust Item Height"), Category("Separator")]
        public bool AutoAdjustItemHeight
        {
            get { return _autoAdjustItemHeight; }
            set { _autoAdjustItemHeight = value; }
        }

        #endregion

        #region Overrides

        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            if (_autoAdjustItemHeight)
                e.ItemHeight += _separatorWidth;

            base.OnMeasureItem(e);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (-1 == e.Index) return;

            var sep = false;

            for (var i = 0; !sep && i < _separators.Count; i++)
            {
                var o = _separators[i];

                if (o is string)
                {
                    if ((string)Items[e.Index] == o as string)
                        sep = true;
                }
                else
                {
                    var pos = (int)o;
                    if (pos < 0) pos += Items.Count;

                    if (e.Index == pos) sep = true;
                }
            }

            e.DrawBackground();

            var g = e.Graphics;
            var y = e.Bounds.Location.Y + _separatorWidth - 1;

            if (sep && (e.Bounds.Bottom > ClientRectangle.Bottom))
            {
                var pen = new Pen(_separatorColor, _separatorWidth) { DashStyle = _separatorStyle };

                if (DroppedDown)
                {
                    g.DrawLine(pen,
                                e.Bounds.Location.X + _separatorMargin, y,
                                e.Bounds.Location.X + e.Bounds.Width - _separatorMargin, y);
                    y++;
                }
            }

            //CMA: 01.2020 - to prevent the focus set after closing the combobox (selecting an item)

            var br = DrawItemState.Selected == (DrawItemState.Selected & e.State)
                         ? SystemBrushes.HighlightText
                         : new SolidBrush(e.ForeColor);

            g.DrawString(GetItemText(Items[e.Index]), e.Font, br, e.Bounds.Left, y + 1);

            //using (var brush = new SolidBrush(e.ForeColor))
            //{
            //    e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            //    e.Graphics.DrawString(GetItemText(Items[e.Index]), e.Font, brush, e.Bounds);
            //}

            base.OnDrawItem(e);
        }

        #endregion

        #region Data members

        ArrayList _separators;
        DashStyle _separatorStyle;
        Color _separatorColor;
        int _separatorWidth;
        int _separatorMargin;
        bool _autoAdjustItemHeight;

        #endregion
    }
}
