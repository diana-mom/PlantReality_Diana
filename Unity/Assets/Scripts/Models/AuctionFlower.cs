using System;

namespace PlantReality
{
    [Serializable]
    public class AuctionFlower : Model
    {
        public int id;
        public int amount;
        public int flowersPerElement;
        public int minPrice;
        public int startPrice;
        public int price;
        public int auctionId;
        public int flowerId;
        public string createdAt;
        public string updatedAt;
    }
}