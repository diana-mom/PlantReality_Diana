using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using SimpleJSON;
using System.Linq;
using System;

namespace PlantReality
{
    public class FlowerController : RestController<Flower>
    {
        public Flower[] flowers;
        public FlowerController() : base("api/flowers")
        {}

        // Get all flowers on startup
        void Start()
        {
            StartCoroutine(Index(GetFlowersIndex, ParseFlowers));
        }

        // Set the flowers array. Necessary for callback function in Start.
        private void GetFlowersIndex(Flower[] Flowers)
        {
            this.flowers = Flowers;
        }

        // Returns all Flowers linked to the passed id.
        public Flower[] GetFlower(int id)
        {
            return this.flowers.Where(af => af.id == id).ToArray();
        }

        // The ParseFlowers Method parses a JSON string (representing an array) into Flower objects.
        public static Flower[] ParseFlowers(string response)
        {
            var data = JSON.Parse(response);
            Flower[] items = new Flower[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                items[i] = ParseFlower(data[i]);
            }
            return items;
        }

        // The ParseFlower parses a single JSON-encoded object to a Flower object.
        public static Flower ParseFlower(JSONNode data)
        {
            Flower flower = new Flower();
            flower.id = data["id"];
            flower.name = data["name"];
            flower.type = data["type"];
            flower.createdAt = data["created_at"];
            flower.updatedAt = data["updated_at"];
            return flower;
        }
    }
}
