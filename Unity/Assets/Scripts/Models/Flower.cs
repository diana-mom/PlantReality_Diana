using System;

namespace PlantReality
{
    [Serializable]
    public class Flower : Model
    {
        public int id;
        public string name;
        public string type;
        public string createdAt;
        public string updatedAt;
    }
}