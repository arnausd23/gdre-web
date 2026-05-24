using System.Collections.Generic;

namespace ConsoleApp2;

public static class TestInterleavedStaticCollectionInit
{

    public class ElemClass
    {
        public int elem;
    }
    public class InterlevedTestClass
    {

        public readonly int bar = 2;

        public readonly List<string> strings = ["one", "two", "three", "four", "five"];


        public List<int> ListProp1 { get; set; } = [4,5,6,7,8];

        public List<int> oOOIntListField = [1, 2, 3, 4, 5];

        public List<string> StringListProp1 { get; set; } = ["nine", "ten", "eleven", "twelve", "thirteen"];


        public static readonly List<ElemClass> staticElemClassListField = [new ElemClass { elem = 1 }, new ElemClass { elem = 2 }, new ElemClass { elem = 3 }, new ElemClass { elem = 4 }, new ElemClass { elem = 5 }];

        public static List<int> StaticIntProp1 { get; } = [1, 2, 3, 4, 5];
        public static readonly List<string> staticStringListField = ["one", "two", "three", "four", "five"];
    }
}
