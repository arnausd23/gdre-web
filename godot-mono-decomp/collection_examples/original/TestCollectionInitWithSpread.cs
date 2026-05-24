using System.Collections.Generic;

namespace ConsoleApp2;

public static class TestCollectionInitWithSpread
{

    private static IEnumerable<string> stringListConst1 => [ "one", "two", "three", "four", "five" ];

    private static IEnumerable<string> stringListConst2 => ["six", "seven", "eight", "nine", "ten" ];

    private static IEnumerable<string> stringListConst3 => ["eleven", "twelve", "thirteen", "fourteen", "fifteen"];


    public class TestStaticHashSetMemberSpreadInit 
    {
        public static readonly HashSet<string> strings = [..stringListConst1, ..stringListConst2, ..stringListConst3, "sixteen", "seventeen", "eighteen", "nineteen", "twenty"];

    }


    public class TestStaticListMemberSpreadInit 
    {
        public static readonly List<string> strings = [..stringListConst1, ..stringListConst2, ..stringListConst3, "sixteen", "seventeen", "eighteen", "nineteen", "twenty"];

    }


    public class TestStaticReadOnlySetMemberSpreadInit 
    {

        public static readonly IReadOnlySet<string> strings = new HashSet<string>([..stringListConst1, ..stringListConst2, ..stringListConst3, "sixteen", "seventeen", "eighteen", "nineteen", "twenty"]);

    }
    public class TestStaticReadOnlyListMemberSpreadInit 
    {
        public static readonly IReadOnlyList<string> strings = [..stringListConst1, ..stringListConst2, ..stringListConst3, "sixteen", "seventeen", "eighteen", "nineteen", "twenty"];

    }

}
