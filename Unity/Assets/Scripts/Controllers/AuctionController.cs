using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using SimpleJSON;
using System.Linq;
using System;

namespace PlantReality
{
    // Getters for AuctionFlower table.
    public class AuctionController : RestController<Auction>
    {
        // Only the first of the following arrays is absolutely necessary. The other arrays are there to improve loading times.
        public Auction[] auctions;
        public Auction[] activeAuctions;
        public AuctionFlower[] items;
        public AuctionFlowerController aucFlowerController;

        public AuctionController() : base("api/auctions"){}

        // On Initialisation, Find the linked AuctionFlowerController script. Also looks up the auctions list.
        void Start()
        {
            aucFlowerController = GetComponentInChildren<AuctionFlowerController>();
            StartCoroutine(Index(SetAuctions, ParseAuctions));
        }

        // Set the auctions array. Necessary for callback function in Start.
        private void SetAuctions(Auction[] auctions)
        {
            this.auctions = auctions;
        }

        // Set the active (ongoing) auctions in the activeAuctions array.
        public void SetActiveAuctions()
        {
            this.activeAuctions = auctions.Where(a => a.end == null).ToArray();
        }

        // The ParseAuctions Method parses a JSON string (representing an array) into Auction objects.
        public Auction[] ParseAuctions(string response)
        {
            var data = JSON.Parse(response);
            Auction[] auctions = new Auction[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                auctions[i] = parseAuction(data[i]);
            }
            return auctions;
        }

        // The ParseAuction parses a single JSON-encoded object to an Auction object.
        public Auction parseAuction(JSONNode data)
        {
            Auction auction = new Auction();
            auction.id = data["id"];
            auction.start = data["start"];
            auction.end = data["end"];
            auction.clock = data["clock"];
            auction.createdAt = data["created_at"];
            auction.updatedAt = data["updated_at"];
            return auction;
        }
    }
}
