using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
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
            .Where(points => points.Count > 1)
            .Subscribe(points => this.DrawLine(points[0], points[1]));
        }

        private void DrawLine(Point point1, Point point2)
        {
            using (var g = this.CreateGraphics())
            {
                g.DrawLine(Pens.ForestGreen, point1, point2);
            }
        }
    }
}
