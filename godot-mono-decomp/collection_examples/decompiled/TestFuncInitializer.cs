using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ConsoleApp2;

public static class TestFuncInitializer
{
	public class TestClass1
	{
		public readonly List<string> strings;

		private Func<string, bool> filter;

		public TestClass1()
		{
			int num = 5;
			List<string> list = new List<string>(num);
			CollectionsMarshal.SetCount(list, num);
			Span<string> span = CollectionsMarshal.AsSpan(list);
			int num2 = 0;
			span[num2] = "one";
			num2++;
			span[num2] = "two";
			num2++;
			span[num2] = "three";
			num2++;
			span[num2] = "four";
			span[num2 + 1] = "five";
			strings = list;
			filter = (string s) => s.Length > 3;
			base._002Ector();
		}
	}
}
