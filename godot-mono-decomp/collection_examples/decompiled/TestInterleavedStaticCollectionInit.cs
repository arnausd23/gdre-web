using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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

		public readonly List<string> strings;

		public List<int> oOOIntListField;

		public static readonly List<ElemClass> staticElemClassListField;

		public static readonly List<string> staticStringListField;

		public List<int> ListProp1 { get; set; }

		public List<string> StringListProp1 { get; set; }

		public static List<int> StaticIntProp1 { get; }

		public InterlevedTestClass()
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
			num = 5;
			List<int> list3 = new List<int>(num);
			CollectionsMarshal.SetCount(list3, num);
			Span<int> span3 = CollectionsMarshal.AsSpan(list3);
			num2 = 0;
			span3[num2] = 1;
			num2++;
			span3[num2] = 2;
			num2++;
			span3[num2] = 3;
			num2++;
			span3[num2] = 4;
			span3[num2 + 1] = 5;
			oOOIntListField = list3;
			num2 = 5;
			List<string> list4 = new List<string>(num2);
			CollectionsMarshal.SetCount(list4, num2);
			Span<string> span4 = CollectionsMarshal.AsSpan(list4);
			num = 0;
			span4[num] = "nine";
			num++;
			span4[num] = "ten";
			num++;
			span4[num] = "eleven";
			num++;
			span4[num] = "twelve";
			span4[num + 1] = "thirteen";
			StringListProp1 = list4;
			base._002Ector();
		}

		static InterlevedTestClass()
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
			staticElemClassListField = list;
			num2 = 5;
			List<int> list2 = new List<int>(num2);
			CollectionsMarshal.SetCount(list2, num2);
			Span<int> span2 = CollectionsMarshal.AsSpan(list2);
			num = 0;
			span2[num] = 1;
			num++;
			span2[num] = 2;
			num++;
			span2[num] = 3;
			num++;
			span2[num] = 4;
			num++;
			span2[num] = 5;
			StaticIntProp1 = list2;
			num = 5;
			List<string> list3 = new List<string>(num);
			CollectionsMarshal.SetCount(list3, num);
			Span<string> span3 = CollectionsMarshal.AsSpan(list3);
			num2 = 0;
			span3[num2] = "one";
			num2++;
			span3[num2] = "two";
			num2++;
			span3[num2] = "three";
			num2++;
			span3[num2] = "four";
			num2++;
			span3[num2] = "five";
			staticStringListField = list3;
		}
	}
}
