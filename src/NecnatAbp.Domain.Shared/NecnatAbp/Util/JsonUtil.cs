using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace NecnatAbp.Util
{
    public static class JsonUtil
    {
        public static T Clone<T>(T obj)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj))!;
        }
        public static TTo CloneTo<T, TTo>(T obj)
        {
            return JsonConvert.DeserializeObject<TTo>(JsonConvert.SerializeObject(obj))!;
        }

        public static List<T> RemakeList<T>(List<T> recordList, List<T> remakeList)
        {
            var l = new List<T>();
            foreach (var iRemake in remakeList)
            {
                var substitute = recordList.Where(x => JsonConvert.SerializeObject(x) == JsonConvert.SerializeObject(iRemake)).FirstOrDefault();
                if (substitute != null)
                {
                    l.Remove(remakeList.Where(x => JsonConvert.SerializeObject(x) == JsonConvert.SerializeObject(iRemake)).First());
                    l.Add(substitute);
                }
                else
                    l.Add(iRemake);
            }

            return l;
        }
    }
}
