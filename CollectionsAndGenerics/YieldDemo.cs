using System.Collections.Generic;

namespace CollectionsAndGenerics
{
    class YieldDemo
    {
        public IEnumerable<string> GetWeekDays()
        {
            yield return "Mon";
            yield return "Tue";
            yield return "Wed";
            yield return "Thu";
            yield return "Fri";
            yield return "Sat";
            yield return "Sun";
        }
    }
}