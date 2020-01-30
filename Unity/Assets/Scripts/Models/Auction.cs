using System;

namespace PlantReality
{
    [Serializable]
    public class Auction : Model
    {
        public int id;
        public string start;
        public string end;
        public int clock;
        public AuctionFlower[] items;
        public string createdAt;
        public string updatedAt;
    }
}