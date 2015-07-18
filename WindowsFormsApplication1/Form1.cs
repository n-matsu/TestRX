using System;
using System.Data;
using System.Drawing;
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

            mousedown
            .Zip(mouseup, (evt, evt2) => new { pt1 = evt.Location, pt2 = evt2.Location })
            .Where(_ => rdoRect.Checked)
            .Subscribe(points => this.DrawRect(points.pt1, points.pt2));

            mousedown
            .Zip(mouseup, (evt, evt2) => new { pt1 = evt.Location, pt2 = evt2.Location })
            .Where(_ => rdoEllipse.Checked)
            .Subscribe(points => this.DrawElipse(points.pt1, points.pt2));
        }

        private void DrawLine(Point point1, Point point2)
        {
            using (var g = this.CreateGraphics())
            {
                g.DrawLine(Pens.ForestGreen, point1, point2);
            }
        }

        private void DrawRect(Point point1, Point point2)
        {
            using (var g = this.CreateGraphics())
            {
                g.DrawRectangle(Pens.MidnightBlue,
                    Math.Min(point1.X, point2.X),
                    Math.Min(point1.Y, point2.Y),
                    Math.Abs(point1.X - point2.X),
                    Math.Abs(point1.Y - point2.Y));
            }
        }

        private void DrawElipse(Point point1, Point point2)
        {
            using (var g = this.CreateGraphics())
            {
                g.DrawEllipse(Pens.MidnightBlue,
                    Math.Min(point1.X, point2.X),
                    Math.Min(point1.Y, point2.Y),
                    Math.Abs(point1.X - point2.X),
                    Math.Abs(point1.Y - point2.Y));
            }
        }
    }
}
