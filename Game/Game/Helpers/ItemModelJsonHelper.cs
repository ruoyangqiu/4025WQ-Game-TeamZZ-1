using Game.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Game.Helpers
{
    public static class ItemModelJsonHelper
    {
        /// <summary>
        /// ParseJson takes the raw stirng and parses it into valid ItemModel
        /// 
        /// The returned data will be a list of items.  Need to pull that list out
        /// </summary>
        /// <param name="myJsonData"></param>
        /// <returns></returns>
        public static List<ItemModel> ParseJson(string myJsonData)
        {
            var myData = new List<ItemModel>();

            try
            {
                JObject json;
                json = JObject.Parse(myJsonData);

                // Data is a List of Items, so need to pull them out one by one...

                var myTempList = json["ItemList"].ToObject<List<JObject>>();

                foreach (var myItem in myTempList)
                {
                    var myTempObject = ConvertFromJson(myItem);
                    if (myTempObject != null)
                    {
                        myData.Add(myTempObject);
                    }
                }

                return myData;
            }
            catch (Exception Ex)
            {
                Debug.WriteLine(Ex.ToString());
                return null;
            }
        }

        public static ItemModel ConvertFromJson(JObject json)
        {
            var myData = new ItemModel();
            return myData;
        }

    }
}
