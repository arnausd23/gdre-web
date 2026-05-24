using System.Collections.Generic;

namespace ConsoleApp2;

public static class TestCollectionExpressionInitializers
{

    public class TestClassBase
    {

        public readonly int parentIntField = 1;

        public readonly List<int> numbers;

        public readonly string secondField;

        public TestClassBase(List<int> numbers)
        {
            this.numbers = numbers;
            this.secondField = "second string";
        }

    }

    public class ElemClass
    {
        public int elem;
    }
    public class TestClass1 : TestClassBase
    {

        public readonly int bar = 2;

        public readonly List<string> strings = ["one", "two", "three", "four", "five"];

        public static readonly List<ElemClass> elems = [new ElemClass { elem = 1 }, new ElemClass { elem = 2 }, new ElemClass { elem = 3 }, new ElemClass { elem = 4 }, new ElemClass { elem = 5 }];

        public readonly Dictionary<string, int> dict = new Dictionary<string, int> { ["one"] = 1, ["two"] = 2, ["three"] = 3, ["four"] = 4, ["five"] = 5 };

        public int randomField;

        public string anotherRandomField;

        public List<string> fieldInitializedInConstructor;

        public int IntProp1 { get; set; } = 1;

        public List<int> ListProp1 { get; set; } = [4,5,6,7,8];

        public int IntProp2 { get; set; } = 2;

        // field initialized after properties
        public readonly HashSet<int> set = [1, 2, 3, 4, 5];

        public List<string> StringListProp1 { get; set; } = ["nine", "ten", "eleven", "twelve", "thirteen"];

        public TestClass1() : base([1, 2, 3, 4, 5])
        {
            foo();
            fieldInitializedInConstructor = [
                "string_1",
                "string_2",
                "string_3",
                "string_4",
                "string_5"
            ];
            anotherRandomField = "another string";
            
        }

        void foo(){
            randomField = 1;

        }
    }
}
