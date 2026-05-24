using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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
			secondField = "second string";
		}
	}

	public class ElemClass
	{
		public int elem;
	}

	public class TestClass1 : TestClassBase
	{
		public readonly int bar = 2;

		public readonly List<string> strings;

		public static readonly List<ElemClass> elems;

		public readonly Dictionary<string, int> dict;

		public int randomField;

		public string anotherRandomField;

		public List<string> fieldInitializedInConstructor;

		// 'set' not in original order; was originally after `IntProp2`
		public readonly HashSet<int> set;

		public int IntProp1 { get; set; }

		public List<int> ListProp1 { get; set; }

		public int IntProp2 { get; set; }

		public List<string> StringListProp1 { get; set; }

		public TestClass1()
		{
			// `strings` initialization
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
			// `dict` initialization
			dict = new Dictionary<string, int>
			{
				["one"] = 1,
				["two"] = 2,
				["three"] = 3,
				["four"] = 4,
				["five"] = 5
			};
			// `IntProp1` initialization
			IntProp1 = 1;
			// `ListProp1` initialization
			num2 = 5;
			List<int> list2 = new List<int>(num2);
			CollectionsMarshal.SetCount(list2, num2);
			Span<int> span2 = CollectionsMarshal.AsSpan(list2);
			num = 0;
			span2[num] = 4;
			num++;
			span2[num] = 5;
			num++;
			span2[num] = 6;
			num++;
			span2[num] = 7;
			span2[num + 1] = 8;
			ListProp1 = list2;
			// `IntProp2` initialization
			IntProp2 = 2;
			// `set` initialization
			// Even though `set` is not listed in the class members in the original order, `set` initialization still happens in original order (i.e. after `IntProp2`)
			set = new HashSet<int> { 1, 2, 3, 4, 5 };
			// `StringListProp1` initialization
			num = 5;
			List<string> list3 = new List<string>(num);
			CollectionsMarshal.SetCount(list3, num);
			Span<string> span3 = CollectionsMarshal.AsSpan(list3);
			num2 = 0;
			span3[num2] = "nine";
			num2++;
			span3[num2] = "ten";
			num2++;
			span3[num2] = "eleven";
			num2++;
			span3[num2] = "twelve";
			span3[num2 + 1] = "thirteen";
			StringListProp1 = list3;
			// initialization of the List parameter passed to the base TestClassBase constructor, which uses it to initialize `TestClassBase.numbers`
			num2 = 5;
			List<int> list4 = new List<int>(num2);
			CollectionsMarshal.SetCount(list4, num2);
			Span<int> span4 = CollectionsMarshal.AsSpan(list4);
			num = 0;
			span4[num] = 1;
			num++;
			span4[num] = 2;
			num++;
			span4[num] = 3;
			num++;
			span4[num] = 4;
			span4[num + 1] = 5;
			// call to base constructor, indicates that we've reached the end of the originally inlined field/property initializers
			base._002Ector(list4);

			// Stuff that was in the original constructor body
			foo();
			num = 5;
			List<string> list5 = new List<string>(num);
			CollectionsMarshal.SetCount(list5, num);
			Span<string> span5 = CollectionsMarshal.AsSpan(list5);
			num2 = 0;
			span5[num2] = "string_1";
			num2++;
			span5[num2] = "string_2";
			num2++;
			span5[num2] = "string_3";
			num2++;
			span5[num2] = "string_4";
			num2++;
			span5[num2] = "string_5";
			fieldInitializedInConstructor = list5;
			anotherRandomField = "another string";
		}

		private void foo()
		{
			randomField = 1;
		}

		static TestClass1()
		{
			int num = 5;
			List<ElemClass> list = new List<ElemClass>(num);
			CollectionsMarshal.SetCount(list, num);
			Span<ElemClass> span = CollectionsMarshal.AsSpan(list);
			int num2 = 0;
			span[num2] = new ElemClass
			{
				elem = 1
			};
			num2++;
			span[num2] = new ElemClass
			{
				elem = 2
			};
			num2++;
			span[num2] = new ElemClass
			{
				elem = 3
			};
			num2++;
			span[num2] = new ElemClass
			{
				elem = 4
			};
			num2++;
			span[num2] = new ElemClass
			{
				elem = 5
			};
			elems = list;
		}
	}
}
