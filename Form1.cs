using Mechanics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorLib;

namespace MechanicTest
{
    public partial class Form1 : Form
    {
        double scale = 100;
        List<Ball> balls = new List<Ball>();
        Graphics g;
        Coords[] edges;
        public Form1()
        {
            InitializeComponent();
            g = panel.CreateGraphics();
            edges = new Coords[]{ 
                new Coords(0, 0),
                new Coords(panel.Width  / scale, 0),
                new Coords(panel.Width / scale, panel.Height / scale),
                new Coords(0, panel.Height / scale),
            };
        }

        public void UpdateBalls()
        {
            g.Clear(panel.BackColor);
            foreach (Ball b in balls)
            {
                b.Move(timer.Interval / 1000.0, new Vector(0, 0));
                Collisions.Collide(b, edges);
                foreach (var b2 in balls)
                {
                    if(b == b2) continue;

                    //if (Collisions.IsColliding(b, b2))
                    //{
                    //    b.V.Paint(g, Pens.Pink, b.R.GetPoint());
                    //    b2.V.Paint(g, Pens.LightGreen, b.R.GetPoint());
                    
                    Collisions.Collide(b, b2);

                        //b.V.Paint(g, Pens.Red, b.R.GetPoint());
                        //b2.V.Paint(g, Pens.Green, b.R.GetPoint());
                        //Update();

                    //}
                }

                g.DrawEllipse(
                    Pens.Red,
                    (int)((b.R.X - b.Radius) * scale),
                    (int)((b.R.Y - b.Radius) * scale),
                    (int)(b.Radius * 2 * scale),
                    (int)( b.Radius * 2 * scale)
                );
            }
            Update();
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            balls.Add(new Ball()
            {
                M = 5,
                V = new Vector(4, -3),
                Radius = 0.5,
                R = new Vector(1, 5),
            });

            balls.Add(new Ball()
            {
                M = 3,
                V = new Vector(-2, -5),
                Radius = 0.3,
                R = new Vector(5, 5)
            });

            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            UpdateBalls();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Space == e.KeyCode)
            {
                bStart_Click(bStart, new EventArgs());
            }
        }
    }
}
