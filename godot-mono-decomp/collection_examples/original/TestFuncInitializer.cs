using System.Collections.Generic;

namespace ConsoleApp2;

public static class TestFuncInitializer
{

    public class TestClass1
    {

        public readonly List<string> strings = ["one", "two", "three", "four", "five"];
        private Func<string, bool> filter = (string s) => s.Length > 3;
    }

}