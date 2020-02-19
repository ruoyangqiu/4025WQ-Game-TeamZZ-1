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
            "https://i.ibb.co/SJQ8cF6/01495.png",
            "https://i.ibb.co/WsBL7Nc/01500.png",
            "https://i.ibb.co/X27dHvn/01509.png",
            "https://i.ibb.co/rQn6bPS/01512.png",
            "https://i.ibb.co/418cr7n/01524.png",
            "https://i.ibb.co/wpHJhwK/01516.png",
            "https://i.ibb.co/zZj6PL4/01519.png"

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
