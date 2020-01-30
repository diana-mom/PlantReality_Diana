using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlantReality
{
    
    // Main logic for the auction and controllers.
    public class Main : MonoBehaviour
    {

        bool check_auction = true;
        bool check_flowers = true;

        public Queue<AuctionFlower> flowers = new Queue<AuctionFlower>();
        AuctionFlower currentFlower;

        public AuctionController aucController;
        public FlowerController flowerController;

        public Clock clock;
        public FlowerInfo flowerInfo;


        // Find and link other scripts (linked with unity objects).
        void Start()
        {
            aucController = GetComponentInChildren<AuctionController>();
            clock = GetComponentInChildren<Clock>();
            flowerInfo = GetComponentInChildren<FlowerInfo>();
            flowerController = GetComponentInChildren<FlowerController>();
            Debug.Log(aucController.auctions);
        }

        // Update is called once per frame.
        void Update()
        {
            // If any, check for active auctions.
            if (aucController.auctions.Count() > 0 && check_auction)
            {
                aucController.SetActiveAuctions();
                check_auction = false;
            }

            // If any, check for flowers attached to auctions.
            if (aucController.aucFlowerController.auctionsFlowers.Count() > 0 && check_flowers)
            {
                aucController.items = aucController.aucFlowerController.GetAuctionsItems(aucController.activeAuctions[0].id);
                check_flowers = false;

                // Add found flowers to the queue.
                foreach(AuctionFlower flower in aucController.items)
                {
                    flowers.Enqueue(flower);
                }
            }

            if(flowers.Count() > 0 || currentFlower != null)
            {
                // Remove current flower if it's past it's lowest price.
                if(currentFlower != null)
                {
                    if(clock.MinimumReached)
                    {
                        currentFlower = null;
                    }
                }

                // Set flower if not set.
                if(currentFlower == null && flowers.Count > 0)
                {
                    // Get next flower.
                    currentFlower = flowers.Dequeue();

                    // Set clock.
                    clock.SetPrices(currentFlower.minPrice, currentFlower.startPrice);
                    clock.CancelInvoke("GoDown");
                    clock.InvokeRepeating("GoDown", 1, 0.1f);

                    // Display flower information (name).
                    flowerInfo.SetText(currentFlower.id + ": " + flowerController.GetFlower(currentFlower.flowerId)[0].name);
                }
            }
            else
            {
                // Stop auction if all flowers are dealt with.
                clock.CancelInvoke("GoDown");
            }
        }
    }
}
