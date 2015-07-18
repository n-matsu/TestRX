using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Reactive;
using System.Reactive.Linq;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

		private void Form1_Load(object sender, EventArgs e)
		{
			IObservable<MouseEventArgs> mousedown =
				Observable.FromEvent<MouseEventArgs>(
					h => this.MouseDown += (s, args) => h(args),
					h => this.MouseDown -= (s, args) => h(args)
					);
			IObservable<MouseEventArgs> mousemove =
				Observable.FromEvent<MouseEventArgs>(
					h => this.MouseMove += (s, args) => h(args),
					h => this.MouseMove -= (s, args) => h(args)
					);
			IObservable<MouseEventArgs> mouseup =
				Observable.FromEvent<MouseEventArgs>(
					h => this.MouseUp += (s, args) => h(args),
					h => this.MouseUp -= (s, args) => h(args)
					);
			mousemove
			.SkipUntil(mousedown)
			.TakeUntil(mouseup)
			.Select(evt => evt.Location)
			.Buffer(2, 1)
			.Repeat()
			.Where(points => rdoLine.Checked && points.Count > 1)
			.Subscribe(points => this.DrawLine(points[0], points[1]));

			var mousedrag = 
			mousemove
			.SkipUntil(mousedown)
			.CombineLatest(mousedown, (evt, evt2) => new { downAt = evt2.Location, moveAt = evt.Location })
			.TakeUntil(mouseup);

			mousedrag
			.Select(p => new { downAt = p.downAt, moveAt = p.moveAt, shape = this.GetSelectedShape() })
			.Where(p => p.shape != tShape.None)
			.Buffer(2, 1)
			.Where(points => points.Count > 1)
			.Do(points => this.EraseShape(points[0].shape, points[0].downAt, points[0].moveAt))
			.Do(points => this.DrawShape(points[0].shape, points[1].downAt, points[1].moveAt))
			.TakeUntil(mouseup)
			.Repeat()
			.Subscribe(points => this.DrawShape(points[1].shape, points[1].downAt, points[1].moveAt));
		}

        private void DrawLine(Point point1, Point point2)
        {
            using (var g = this.CreateGraphics())
            {
                g.DrawLine(Pens.ForestGreen, point1, point2);
            }
        }

		enum tShape
		{
			None,
			Rect,
			Ellipse,
		}

		private tShape GetSelectedShape()
		{
			if (rdoRect.Checked) return tShape.Rect;
			if (rdoEllipse.Checked) return tShape.Ellipse;
			return tShape.None;
		}

		private void DrawShape(tShape shape, Point point1, Point point2)
		{
			this.DrawShape(shape, point1, point2, Pens.MidnightBlue);
        }
		private void EraseShape(tShape shape, Point point1, Point point2)
		{
			this.DrawShape(shape, point1, point2, Pens.White);
		}
		private void DrawShape(tShape shape, Point point1, Point point2, Pen pen)
		{
			using (var g = this.CreateGraphics())
			{
				this.GetDrawAction(shape, g)(pen,
					Math.Min(point1.X, point2.X),
					Math.Min(point1.Y, point2.Y),
					Math.Abs(point1.X - point2.X),
					Math.Abs(point1.Y - point2.Y));
			}
		}
		private Action<Pen,float, float, float, float> GetDrawAction(tShape shape, Graphics g)
		{
			switch (shape)
			{
				case tShape.Rect:
					return g.DrawRectangle;
				case tShape.Ellipse:
					return g.DrawEllipse;
				default:
					throw new Exception("");
			}
		}
	}
}
