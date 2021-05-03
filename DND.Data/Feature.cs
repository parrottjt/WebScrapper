using System;

namespace DND.Data
{
    public class Feature
    {
        public int Level { get; private set; }
        public string Description { get; private set; }

        public Feature()
        {
            
        }
        public Feature(int level, string description)
        {
            Level = Math.Clamp(level, 1, 20);
            Description = description;
        }
    }
}
