using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sakk
{
    public partial class MainForm : Form
    {
        enum Fajta { Gyalog, Lo, Futo, Bastya, Kiralyno, Kiraly };
        enum Szin { Fekete, Feher };

        class Figura
        {
            public Szin Szin;
            public Fajta Fajta;
        }

        class DragSource
        {
            public int Sor;
            public int Oszlop;
        }

        private bool _down, _moved;
        private readonly Figura[,] _tabla = new Figura[8, 8];

        private void Alaptabla()
        {
            // Felső sor
            _tabla[0, 0] = new Figura { Szin = Szin.Feher, Fajta = Fajta.Bastya };
            _tabla[0, 1] = new Figura { Szin = Szin.Feher, Fajta = Fajta.Lo };
            _tabla[0, 2] = new Figura { Szin = Szin.Feher, Fajta = Fajta.Futo };
            _tabla[0, 3] = new Figura { Szin = Szin.Feher, Fajta = Fajta.Kiraly };
            _tabla[0, 4] = new Figura { Szin = Szin.Feher, Fajta = Fajta.Kiralyno };
            _tabla[0, 5] = new Figura { Szin = Szin.Feher, Fajta = Fajta.Futo };
            _tabla[0, 6] = new Figura { Szin = Szin.Feher, Fajta = Fajta.Lo };
            _tabla[0, 7] = new Figura { Szin = Szin.Feher, Fajta = Fajta.Bastya };

            // 1.sor
            for(int oszlop = 0; oszlop < 8; oszlop++)
                _tabla[1, oszlop] = new Figura { Szin = Szin.Feher, Fajta = Fajta.Gyalog };

            for(int sor = 2; sor < 6; sor++)
                for(int oszlop = 0; oszlop < 8; oszlop++)
                    _tabla[sor, oszlop] = null;

            for(int oszlop = 0; oszlop < 8; oszlop++)
                _tabla[6, oszlop] = new Figura { Szin = Szin.Fekete, Fajta = Fajta.Gyalog };

            _tabla[7, 0] = new Figura { Szin = Szin.Fekete, Fajta = Fajta.Bastya };
            _tabla[7, 1] = new Figura { Szin = Szin.Fekete, Fajta = Fajta.Lo };
            _tabla[7, 2] = new Figura { Szin = Szin.Fekete, Fajta = Fajta.Futo };
            _tabla[7, 3] = new Figura { Szin = Szin.Fekete, Fajta = Fajta.Kiraly };
            _tabla[7, 4] = new Figura { Szin = Szin.Fekete, Fajta = Fajta.Kiralyno };
            _tabla[7, 5] = new Figura { Szin = Szin.Fekete, Fajta = Fajta.Futo };
            _tabla[7, 6] = new Figura { Szin = Szin.Fekete, Fajta = Fajta.Lo };
            _tabla[7, 7] = new Figura { Szin = Szin.Fekete, Fajta = Fajta.Bastya };
        }


        private bool Mehet(int sor1, int oszlop1, int sor2, int oszlop2)
        {
            int dx = Math.Sign(oszlop2 - oszlop1);
            int dy = Math.Sign(sor2 - sor1);
            for(int s = sor1 + dy, o = oszlop1 + dx; s != sor2 || o != oszlop2; s += dy, o += dx)
            {
//System.Diagnostics.Debug.WriteLine($"s={s},o={o}");
                if(s < 0 || s > 7 || o < 0 || o > 7)
                    return false;
                if(_tabla[s, o] != null)
                    return false;
            }
            return true;
        }

        private bool Atlos(int sor1, int oszlop1, int sor2, int oszlop2)
        {
            return Math.Abs(oszlop2 - oszlop1) == Math.Abs(sor2 - sor1);
        }

        private bool Lephet(Figura f, int sor1, int oszlop1, int sor2, int oszlop2)
        {
            switch(f.Fajta)
            {
                case Fajta.Gyalog:
                    switch(f.Szin)
                    {
                        case Szin.Fekete:
                            if(oszlop1 == oszlop2 && sor2 == sor1 - 1 && _tabla[sor2, oszlop2] == null)
                                return true;
                            if(sor1 == 6 && oszlop1 == oszlop2 && sor2 == sor1 - 2 && _tabla[sor2, oszlop2] == null && _tabla[sor1 - 1, oszlop1] == null)
                                return true;
                            if(oszlop1 > 0 && oszlop2 == oszlop1 - 1 && sor2 == sor1 - 1 && _tabla[sor2, oszlop2] != null && _tabla[sor2, oszlop2].Szin == Szin.Feher)
                                return true;
                            if(oszlop1 < 7 && oszlop2 == oszlop1 + 1 && sor2 == sor1 - 1 && _tabla[sor2, oszlop2] != null && _tabla[sor2, oszlop2].Szin == Szin.Feher)
                                return true;
                            return false;

                        case Szin.Feher:
                            if(oszlop1 == oszlop2 && sor2 == sor1 + 1 && _tabla[sor2, oszlop2] == null)
                                return true;
                            if(sor1 == 1 && oszlop1 == oszlop2 && sor2 == sor1 + 2 && _tabla[sor2, oszlop2] == null && _tabla[sor1 + 1, oszlop1] == null)
                                return true;
                            if(oszlop1 > 0 && oszlop2 == oszlop1 - 1 && sor2 == sor1 + 1 && _tabla[sor2, oszlop2] != null && _tabla[sor2, oszlop2].Szin == Szin.Fekete)
                                return true;
                            if(oszlop1 < 7 && oszlop2 == oszlop1 + 1 && sor2 == sor1 + 1 && _tabla[sor2, oszlop2] != null && _tabla[sor2, oszlop2].Szin == Szin.Fekete)
                                return true;
                            return false;
                    }
                    return false;

                case Fajta.Futo:
                    return Atlos(sor1, oszlop1,sor2, oszlop2) && Mehet(sor1, oszlop1, sor2, oszlop2);

                case Fajta.Bastya:
                    if(sor1 == sor2 || oszlop1 == oszlop2)
                        return Mehet(sor1, oszlop1, sor2, oszlop2);
                    return false;

                case Fajta.Lo:
                    int dy = Math.Abs(sor2 - sor1);
                    int dx = Math.Abs(oszlop2 - oszlop1);
                    return dx == 2 && dy == 1 || dx == 1 && dy == 2;

                case Fajta.Kiralyno:
                    if(sor1 == sor2 || oszlop1 == oszlop2 || Atlos(sor1, oszlop1,sor2, oszlop2))
                        return Mehet(sor1, oszlop1, sor2, oszlop2);
                    return false;

                case Fajta.Kiraly:
                    return Math.Abs(sor2 - sor1) < 2 && Math.Abs(oszlop2 - oszlop1) < 2;
            }

            return false;
        }

        public MainForm()
        {
            InitializeComponent();
            Alaptabla();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle cr = this.ClientRectangle;
            int l = cr.Left + 5;
            int t = cr.Top + 5;
            int w = 68; // cr.Width / 8;
            int h = 68; // cr.Height / 8;
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            for(int sor = 0; sor < 8; sor++)
                for(int oszlop = 0; oszlop < 8; oszlop++)
                {
                    Rectangle r = new Rectangle(l + oszlop * w, t + sor * h, w, h);
                    if(!e.ClipRectangle.IntersectsWith(r))
                        continue;
                    SolidBrush brush = sor % 2 == oszlop % 2 ? whiteBrush : blackBrush;
                    e.Graphics.FillRectangle(brush, r);
                    Figura f = _tabla[sor, oszlop];
                    if(f == null)
                        continue;
                    int idx = (int) f.Szin * 6 + (int) f.Fajta;
                    FiguraKepek.Draw(e.Graphics, new Point(r.Left + 2, r.Top + 2), idx);
                }
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            _down = true;
            _moved = false;
        }

        private void MainForm_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            Point pt = PointToClient(new Point(e.X, e.Y));
            int sor = (pt.Y - 5) / 68;
            int oszlop = (pt.X - 5) / 68;
            if(sor > 7 || oszlop > 7)
                return;
            if(!e.Data.GetDataPresent(typeof(DragSource)))
                return;
            DragSource ds = (DragSource) e.Data.GetData(typeof(DragSource));
            Figura f = _tabla[ds.Sor, ds.Oszlop];
            if(f == null)
                return;
            Figura f2 = _tabla[sor, oszlop];
            if(f2 != null && f.Szin == f2.Szin)
                return;
            if(Lephet(f, ds.Sor, ds.Oszlop, sor, oszlop))
                e.Effect = DragDropEffects.Move;
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if(!_down)
                return;
            if(_moved)
                Capture = false;
            _down = false;
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            Point pt = PointToClient(new Point(e.X, e.Y));
            int sor = (pt.Y - 5) / 68;
            int oszlop = (pt.X - 5) / 68;
            if(sor > 7 || oszlop > 7)
                return;
            DragSource ds = (DragSource) e.Data.GetData(typeof(DragSource));
            Figura f = _tabla[ds.Sor, ds.Oszlop];
            if(f == null)
                return;
            _tabla[sor, oszlop] = f;
            _tabla[ds.Sor, ds.Oszlop] = null;
            Invalidate();
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if(!_down || _moved)
                return;

            _moved = true;
            int sor = (e.Location.Y - 5) / 68;
            int oszlop = (e.Location.X - 5) / 68;
            if(sor > 7 || oszlop > 7)
                return;
            Figura f = _tabla[sor, oszlop];
            if(f == null)
                return;
            Capture = true;
            DoDragDrop(new DragSource { Sor = sor, Oszlop = oszlop }, DragDropEffects.Move);
        }
    }
}
