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
    public class ScriptableEntity {
        private Dictionary<String, Vector3> sVectors;
        private Dictionary<String, Int32> sIntegers;
        private Dictionary<String, float> sFloats;
        private Dictionary<String, String> sStrings;
        private Dictionary<String, object> sObjects;
        public ScriptableEntity() {
            sVectors = new Dictionary<string, Vector3>();
            sIntegers = new Dictionary<string, int>();
            sFloats = new Dictionary<string, float>();
            sStrings = new Dictionary<string, string>();
            sObjects = new Dictionary<string, object>(); 
        }
        public enum sType {
            Vector = 0,
            Integer = 1,
            Float = 2,
            String = 3,
            Object = 4
        }
    }
}
