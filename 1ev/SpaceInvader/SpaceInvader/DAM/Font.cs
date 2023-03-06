using System;
using System.Text.Json;

namespace DAM
{
    public class Font
    {
        private class FontLoad
        {
            public class Character
            {
                public int x { get; set; }
                public int y { get; set; }
                public int width { get; set; }
                public int height { get; set; }
                public int originX { get; set; }
                public int originY { get; set; }
                public int advance { get; set; }
            }

            public string? name { get; set; }
            public int size { get; set; }
            public bool bold { get; set; }
            public bool italic { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public Dictionary<string, Character>? characters { get; set; }
        }

        internal Font()
        {
        }


        internal void LoadFromFile(string path)
        {
            using (FileStream s = File.OpenRead(path))
            {
                var load = JsonSerializer.Deserialize<FontLoad>(s);
            }


        }
    }
}

