using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ZeldaOnline;

namespace ZeldaOnline {
    public class ColoredString {
        public String Text;
        public Vector2 Position;
        public Color TextColor;
        public ColoredString(String Text, Vector2 Position, Color TextColor) {
            this.Text = Text;
            this.Position = Position;
            this.TextColor = TextColor;
        }
    }
    public class TextWindow {
        private String _Text;
        public String Text {
            get {
                return _Text;
            }
            set {
                _Text = value;
                Carat = 0;
                Millis = 0;
            }
        }
        public String RawText { get; private set; }
        public int Carat { get; set; }
        public bool CaratEnd { get; private set; }
        public Rectangle Box;
        public int CaratDelay { get; set; }
        public Color TextColor { get; set; }
        private int _Lines;
        public int Lines {
            get {
                return _Lines;
            }
            set {
                _Lines = value;
                this.Recalc();
            }
        }
        private int _Page;
        public int Page {
            get {
                return _Page;
            }
            set {
                _Page = value;
                if (_Page >= this.PageCount) {
                    _Page = 0;
                    if (this.Done != null) this.Done(this, new EventArgs());
                }
                this.Carat = 0;
                this.CaratEnd = false;
                GenerateRaw();
                if (this.PageChange != null) this.PageChange(this, new EventArgs());
            }
        }
        public int PageCount { get; set; }
        private bool _Show;
        public bool Show {
            get {
                return _Show;
            }
            set {
                _Show = value;
                if (this.Shown != null) if (value == true) this.Shown(this, new EventArgs()); else if (this.Hidden != null) this.Hidden(this, new EventArgs());
            }
        }
        public bool FastCarat { get; set; }

        public event EventHandler<EventArgs> Done;
        public event EventHandler<EventArgs> PageChange;
        public event EventHandler<EventArgs> Shown;
        public event EventHandler<EventArgs> Hidden;

        private int Millis;
        private List<List<ColoredString>> Pages;
        private Stack<Color> Colors;
        public void Draw(SpriteBatch spriteBatch) {
            // Draw the background rectangle.
            spriteBatch.Draw(S.White, new Rectangle(this.Box.X + 4, this.Box.Y + 4, this.Box.Width - 8, this.Box.Height - 8), Color.FromNonPremultiplied(16, 16, 16, 192));
            // Draw the corners.
            spriteBatch.Draw(S.UITiles, new Rectangle(this.Box.X, this.Box.Y, 8, 8), new Rectangle(96, 0, 8, 8), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
            spriteBatch.Draw(S.UITiles, new Rectangle(this.Box.X + this.Box.Width, this.Box.Y, 8, 8), new Rectangle(96, 0, 8, 8), Color.White, S.Deg90, new Vector2(0, 0), SpriteEffects.None, 0f);
            spriteBatch.Draw(S.UITiles, new Rectangle(this.Box.X + this.Box.Width, this.Box.Y + this.Box.Height, 8, 8), new Rectangle(96, 0, 8, 8), Color.White, S.Deg180, new Vector2(0, 0), SpriteEffects.None, 0f);
            spriteBatch.Draw(S.UITiles, new Rectangle(this.Box.X, this.Box.Y + this.Box.Height, 8, 8), new Rectangle(96, 0, 8, 8), Color.White, S.Deg270, new Vector2(0, 0), SpriteEffects.None, 0f);
            // Draw the edges.
            spriteBatch.Draw(S.UITiles, new Rectangle(this.Box.X + 8, this.Box.Y, this.Box.Width - 16, 8), new Rectangle(104, 0, 8, 8), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
            spriteBatch.Draw(S.UITiles, new Rectangle(this.Box.X + this.Box.Width - 8, this.Box.Y + this.Box.Height, this.Box.Width - 16, 8), new Rectangle(104, 0, 8, 8), Color.White, S.Deg180, new Vector2(0, 0), SpriteEffects.None, 0f);
            spriteBatch.Draw(S.UITiles, new Rectangle(this.Box.X, this.Box.Y + 8, 8, this.Box.Height - 16), new Rectangle(120, 0, 8, 8), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
            spriteBatch.Draw(S.UITiles, new Rectangle(this.Box.X + this.Box.Width, this.Box.Y + this.Box.Height - 8, 8, this.Box.Height - 16), new Rectangle(120, 0, 8, 8), Color.White, S.Deg180, new Vector2(0, 0), SpriteEffects.None, 0f);
            // Draw the next arrow.
            if (this.Carat >= this.RawText.Length)
                spriteBatch.Draw(S.UITiles, new Rectangle((int)(this.Box.X + this.Box.Width - 32), (int)(this.Box.Y + this.Box.Height - 8), 8, 8), new Rectangle(136, 0, 8, 8), Color.Multiply(Color.White, DateTime.Now.Millisecond % 500 < 250 ? 1f : 0f), 0f, new Vector2(0, 0), SpriteEffects.None, 0f);

            int CaratInc = 0;
            foreach (ColoredString Segment in Pages[this.Page]) {
                if (CaratInc + Segment.Text.Length > this.Carat) {
                    DrawString(spriteBatch, Segment.Text.Substring(0, this.Carat - CaratInc), new Vector2(Segment.Position.X + this.Box.X + 8, Segment.Position.Y + this.Box.Y + 8), 1, Segment.TextColor);
                    break;
                } else {
                    DrawString(spriteBatch, Segment.Text, new Vector2(Segment.Position.X + this.Box.X + 8, Segment.Position.Y + this.Box.Y + 8), 1, Segment.TextColor);
                }
                CaratInc += Segment.Text.Length;
            }
        }
        public void Recalc() {
            this.Pages = new List<List<ColoredString>>();
            List<ColoredString> NewPage = new List<ColoredString>();
            Vector2 CaratPos = new Vector2(0, 0);
            float LineHeight = (S.GameFont.LineSpacing + 8);
            String[] Lines = this.Text.Split('\n');
            int Line = 0;
            Colors = new Stack<Color>();
            Colors.Push(this.TextColor);

            for (int li = 0; li < Lines.Length; li++) {
                String[] FormatChanges = Lines[li].Split('`');
                for (int sf = 0; sf < FormatChanges.Length; sf++) {
                    if (FormatChanges[sf].Length > 0) {
                        if (sf > 0) switch (FormatChanges[sf][0]) {
                                case '<':
                                    Colors.Pop();
                                    FormatChanges[sf] = FormatChanges[sf].Remove(0, 1);
                                    break;
                                case '^':
                                    Colors.Clear();
                                    Colors.Push(this.TextColor);
                                    FormatChanges[sf] = FormatChanges[sf].Remove(0, 1);
                                    break;
                                case '>':
                                    Colors.Push(new Color(Convert.ToInt16(FormatChanges[sf][1].ToString(), 16) * 16, Convert.ToInt16(FormatChanges[sf][2].ToString(), 16) * 16, Convert.ToInt16(FormatChanges[sf][3].ToString(), 16) * 16));
                                    FormatChanges[sf] = FormatChanges[sf].Remove(0, 4);
                                    break;
                            }
                    }
                    Queue<String> Words = new Queue<string>(FormatChanges[sf].Split(' '));
                    String Rebuild = "";
NextWord:
                    if (S.GameFont.MeasureString(Words.Peek()).X + CaratPos.X > this.Box.Width - 16) {
                        CaratPos.X = 0;
                        CaratPos.Y += LineHeight;
                        Line++;
                        if (Line >= this.Lines) {
                            this.Pages.Add(NewPage);
                            NewPage = new List<ColoredString>();
                            CaratPos.Y = 0;
                            Line = 0;
                        }
                    }
                    NewPage.Add(new ColoredString(Words.Peek(), CaratPos, Colors.Peek()));
                    Rebuild += Words.Peek();
                    if (Rebuild.Length < FormatChanges[sf].Length && FormatChanges[sf][Rebuild.Length] == ' ') {
                        Rebuild += " ";
                        CaratPos.X += S.GameFont.MeasureString(Words.Dequeue() + " ").X;
                    } else {
                        CaratPos.X += S.GameFont.MeasureString(Words.Dequeue()).X;
                    }
                    if (Words.Count > 0) goto NextWord;
                }
                if (Line < this.Lines) {
                    CaratPos.X = 0;
                    CaratPos.Y += LineHeight;
                    Line++;
                } else {
                    this.Pages.Add(NewPage);
                    NewPage = new List<ColoredString>();
                    CaratPos.X = 0;
                    CaratPos.Y = 0;
                    Line = 0;
                }
            }
            this.Pages.Add(NewPage);
            this.PageCount = Pages.Count;
            GenerateRaw();
        }
        private void GenerateRaw() {
            this.RawText = "";
            if (this.Pages == null || this.Pages.Count <= 0) return;
            float py = Pages[this.Page][0].Position.Y;
            foreach (ColoredString Str in Pages[this.Page]) {
                if (Math.Floor(py) != Math.Floor(Str.Position.Y)) {
                    this.RawText += '\n';
                    py = Str.Position.Y;
                }
                this.RawText += Str.Text;
            }
        }
        private void DrawString(SpriteBatch spriteBatch, String Text, Vector2 Position, float Scale, Color Col) {
            spriteBatch.DrawString(S.GameFont, Text, new Vector2(Position.X - Scale, Position.Y), Color.Multiply(Col, 0.2f), 0f, new Vector2(0, 0), Scale, SpriteEffects.None, 0);
            spriteBatch.DrawString(S.GameFont, Text, new Vector2(Position.X, Position.Y - Scale), Color.Multiply(Col, 0.2f), 0f, new Vector2(0, 0), Scale, SpriteEffects.None, 0);
            spriteBatch.DrawString(S.GameFont, Text, new Vector2(Position.X + Scale, Position.Y), Color.Multiply(Col, 0.2f), 0f, new Vector2(0, 0), Scale, SpriteEffects.None, 0);
            spriteBatch.DrawString(S.GameFont, Text, new Vector2(Position.X, Position.Y + Scale), Color.Multiply(Col, 0.2f), 0f, new Vector2(0, 0), Scale, SpriteEffects.None, 0);
            spriteBatch.DrawString(S.GameFont, Text, new Vector2(Position.X, Position.Y), Col, 0f, new Vector2(0, 0), Scale, SpriteEffects.None, 0);
        }
        private void Tick(object sender, GameEventArgs e) {
            if (!this.Show) return;
            if (this.RawText == null || this.RawText.Length < 1) return;
            while (this.CaratDelay < Millis) {
                Millis -= this.CaratDelay;
                if (Carat < this.RawText.Length) {
                    this.Carat++;
                } else {
                    Millis = 0;
                    this.CaratEnd = true;
                }
            }
            Millis += e.Time.ElapsedGameTime.Milliseconds * (this.FastCarat ? 3 : 1);
        }
        public TextWindow(String Text, int CaratDelay, Rectangle Box, Color TextColor, int Lines) {
            this.Text = Text;
            this.CaratDelay = CaratDelay;
            this.Carat = 0;
            this.Millis = 0;
            this.Box = Box;
            this.TextColor = TextColor;
            _Lines = Lines;
            this.Page = 0;
            this.PageCount = 0;
            this.Show = true;
            this.FastCarat = false;
            this.CaratEnd = false;

            this.Recalc();
            S.Tick += this.Tick;
        }
        public TextWindow(String Text, int CaratDelay, Rectangle Box, int Lines) : this(Text, CaratDelay, Box, Color.White, Lines) { }
        public TextWindow(String Text, Rectangle Box, Color TextColor, int Lines) : this(Text, 50, Box, TextColor, Lines) { }
        public TextWindow(String Text, Rectangle Box, int Lines) : this(Text, 50, Box, Color.White, Lines) { }
    }
}
