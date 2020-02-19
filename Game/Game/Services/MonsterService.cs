using System;
using System.Collections.Generic;
using System.Linq;
using Game.Helpers;
namespace Game.Services
{
    static class MonsterService
    {
        // Return the Default Image URI for the Local Image for a Monster.
        public static string DefaultImageURI = "https://i.ibb.co/kHmLkdR/01403.png";

        // A predefined list for Monster image URIs. When a Monster gets created, a random URI from this list is assigned to the Monster.
        public static List<string> MonsterURIs = new List<string>()
        {
            "https://i.ibb.co/nfVF1qg/01543.png",
            "https://i.ibb.co/37J2j7m/01545.png",
            "https://i.ibb.co/KG3KFbp/01546.png",
            "https://i.ibb.co/5kmbn1h/01548.png",
            "https://i.ibb.co/FHcf1wF/01550.png",
            "https://i.ibb.co/VQDNHyY/01554.png",
            "https://i.ibb.co/HVKhVg3/01555.png"

        };

        // A predefined list for Monster descriptions. When a Monster gets created, a random description from this list is assigned to the Monster.
        public static List<string> MonsterDescriptions = new List<string>()
        {
            "Incarnation of the horse god",
            "Incarnation of the pig god",
            "Incarnation of the rabbit god",
            "Incarnation of the dragon god",
            "Incarnation of the snake god",
            "Incarnation of the goat god",
            "Incarnation of the monkey god",
        };

        /// <summary>
        /// Return a tuple of two strings, the first string is the Monster image URI, the second string is the Monster description.
        /// </summary>
        /// <returns></returns>
        public static Tuple<string, string> GetRandomURIDescription()
        {
            int idx = RandomNumberGeneratorHelper.GetRandomNumberInRange(0, MonsterURIs.Count());
            var res = new Tuple<string, string>(MonsterURIs[idx], MonsterDescriptions[idx]);
            return res;
        }
    }
}
