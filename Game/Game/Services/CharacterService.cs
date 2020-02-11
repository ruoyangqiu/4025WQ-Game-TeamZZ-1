using System;
using System.Collections.Generic;
using System.Linq;
using Game.Helpers;
namespace Game.Services
{
    static class CharacterService
    {
        // Return the Default Image URI for the Local Image for a character.
        public static string DefaultImageURI = "https://i.ibb.co/kHmLkdR/01403.png";

        // A predefined list for character image URIs. When a character gets created, a random URI from this list is assigned to the character.
        public static List<string> CharacterURIs = new List<string>()
        {
            "https://i.ibb.co/nfVF1qg/01543.png",
            "https://i.ibb.co/37J2j7m/01545.png",
            "https://i.ibb.co/KG3KFbp/01546.png",
        };

        // A predefined list for character descriptions. When a character gets created, a random description from this list is assigned to the character.
        public static List<string> CharacterDescriptions = new List<string>()
        {
            "Incarnation of the horse god",
            "Incarnation of the pig god",
            "Incarnation of the rabbit god.",
        };

        /// <summary>
        /// Return a tuple of two strings, the first string is the character image URI, the second string is the character description.
        /// </summary>
        /// <returns></returns>
        public static Tuple<string, string> GetRandomURIDescription()
        {
            int idx = RandomNumberGeneratorHelper.GetRandomNumberInRange(0, CharacterURIs.Count());
            var res = new Tuple<string, string>(CharacterURIs[idx], CharacterDescriptions[idx]);
            return res;
        }
    }
}
