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
    public class Character : Mobile {
        public GroupClass Class;
        public Dictionary<GroupClass, Boolean> HostileTo;
        public Character() {
            Class = GroupClass.None;
            HostileTo = new Dictionary<GroupClass, Boolean>();
        }
    }
    public enum GroupClass {
        None = 0,
        Human = 1,
        Monster = 2,
        Hero = 4,
        Villain = 8,
        SmallMonster = 16,
        Boss = 32,
        Insect = 64,
        Ethereal = 128
    }
}
