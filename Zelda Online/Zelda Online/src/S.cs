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
using IrrKlang;

namespace ZeldaOnline {
    public class GameEventArgs : EventArgs {
        public GameTime Time;
        public GameEventArgs() {
            this.Time = new GameTime();
        }
        public GameEventArgs(GameTime Time) {
            this.Time = Time;
        }
    }
    public class S {
        public static Random R = new Random();
        public static SpriteFont GameFont;

        public static RenderTarget2D BufferTarget;
        public static Rectangle BufferRect;

        public static Texture2D Tileset;
        public static Texture2D UITiles;
        public static Texture2D White;

        public static float GameScale = 2;
        public static float UIScale = 2;

        public static float Deg90 = (float)(Math.PI / 2);
        public static float Deg180 = (float)(Math.PI);
        public static float Deg270 = (float)(Math.PI / 2 + Math.PI);

        public static KeyboardState KBO;
        public static KeyboardState KB;
        public static Dictionary<Keys, Boolean> KBH;

        public static event EventHandler<GameEventArgs> Tick;
        public static void Tock(object sender, GameEventArgs e) {
            if (Tick != null) Tick(sender, e);
        }
    }
    public class DialogueBox {
        public static TextWindow TW;
    }
    public class A {
        public static ISoundEngine SE = new ISoundEngine(SoundOutputDriver.DirectSound, SoundEngineOptionFlag.DefaultOptions);
        public static List<ISound> Sfx = new List<ISound>();
        public static List<ISound> Music = new List<ISound>();
        public static void PlaySfx(String Name, float Volume, float Rate) {
            ISound s = SE.Play2D("sfx/" + Name + ".ogg", false, true);
            s.Volume = Volume;
            s.PlaybackSpeed = Rate;
            s.PlayPosition = 0;
            s.Paused = false;
            Sfx.Add(s);
        }
        public static void PlaySfx(String Name, float Volume) {
            PlaySfx(Name, Volume, 1f);
        }
        public static void PlaySfx(String Name) {
            PlaySfx(Name, 1f, 1f);
        }
        public static void SetMusic(String Name, float Volume, float Rate) {

        }
        public static void Tick(object sender, GameEventArgs e) {
            try {
                foreach (ISound Sound in Sfx) {
                    if (Sound.Finished) {
                        Sound.Stop();
                        Sound.Dispose();
                        Sfx.Remove(Sound);
                    }
                }
            } catch { }
            try {
                foreach (ISound Sound in Music) {
                    if (Sound.Finished) {
                        Sound.Stop();
                        Sound.Dispose();
                        Sfx.Remove(Sound);
                    }
                }
            } catch { }
        }
    }
}
