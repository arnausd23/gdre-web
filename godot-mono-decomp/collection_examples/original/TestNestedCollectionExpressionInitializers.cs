using System.Collections.Generic;

namespace ConsoleApp2;

public static class TestNestedCollectionExpressionInitializers
{


    public class ElemClassWithCollection
    {
        public int intField;
        public List<int> intListField;

        public string StringProp { get; set; }

        public int outOfOrderField;

    }

	// example of a class that has a list of objects that are initialized with collection expressions
    public class ParentClassWithCollection
    {

        public readonly int bar = 2;

        public List<ElemClassWithCollection> elems =[
            new ElemClassWithCollection { intField = 1, intListField = [1, 2, 3, 4, 5], StringProp = "string_1", outOfOrderField = 2 },
            new ElemClassWithCollection { intField = 2, intListField = [6, 7, 8, 9, 10], StringProp = "string_2", outOfOrderField = 3 },
            new ElemClassWithCollection { intField = 3, intListField = [11, 12, 13, 14, 15], StringProp = "string_3", outOfOrderField = 4 },
            new ElemClassWithCollection { intField = 4, intListField = [16, 17, 18, 19, 20], StringProp = "string_4", outOfOrderField = 5 },
            new ElemClassWithCollection { intField = 5, intListField = [21, 22, 23, 24, 25], StringProp = "string_5", outOfOrderField = 6 },
        ];
    }
}
