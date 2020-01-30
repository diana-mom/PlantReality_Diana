using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using SimpleJSON;
using System.Linq;
using System;

namespace PlantReality
{
    // Getters for AuctionFlower table.
    public class AuctionFlowerController : RestController<AuctionFlower>
    {
        public AuctionFlower[] auctionsFlowers;
        public AuctionFlowerController() : base("api/auctionflowers"){}

        // Get all auctionFlowers. Deal with the result using GetAuctionsFlowers and ParseAuctionsFlowers.
        void Start()
        {
            StartCoroutine(Index(GetAuctionsFlowers, ParseAuctionsFlowers));
        }

        // Set the auctionFlowers array. Necessary for callback function in Start.
        private void GetAuctionsFlowers(AuctionFlower[] auctionsFlowers)
        {
            this.auctionsFlowers = auctionsFlowers;
        }

        // Returns all AuctionFlowers linked to the passed auctionId.
        public AuctionFlower[] GetAuctionsItems(int auctionId)
        {
            return this.auctionsFlowers.Where(af => af.auctionId == auctionId).ToArray();
        }

        // The ParseAuctionsFlowers Method parses a JSON string (representing an array) into AuctionFlower objects.
        public static AuctionFlower[] ParseAuctionsFlowers(string response)
        {
            var data = JSON.Parse(response);
            AuctionFlower[] items = new AuctionFlower[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                items[i] = ParseAuctionFlower(data[i]);
            }
            return items;
        }

        // The ParseAuctionFlower parses a single JSON-encoded object to a AuctionFlower object.
        public static AuctionFlower ParseAuctionFlower(JSONNode data)
        {
            AuctionFlower auctionFlower = new AuctionFlower();
            auctionFlower.id = data["id"];
            auctionFlower.amount = data["amount"];
            auctionFlower.auctionId = data["auction_id"];
            auctionFlower.flowerId = data["flower_id"];
            auctionFlower.flowersPerElement = data["flowers_per_element"];
            auctionFlower.minPrice = data["min_price"];
            auctionFlower.startPrice = data["start_price"];
            auctionFlower.price = data["price"];
            auctionFlower.createdAt = data["created_at"];
            auctionFlower.updatedAt = data["updated_at"];
            return auctionFlower;
        }
    }
}
