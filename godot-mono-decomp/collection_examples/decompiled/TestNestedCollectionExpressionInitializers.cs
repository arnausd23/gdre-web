using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ConsoleApp2;

public static class TestNestedCollectionExpressionInitializers
{
	public class ElemClassWithCollection
	{
		public int intField;

		public List<int> intListField;

		public int outOfOrderField;

		public string StringProp { get; set; }
	}

	public class ParentClassWithCollection
	{
		public readonly int bar = 2;

		public List<ElemClassWithCollection> elems;

		public ParentClassWithCollection()
		{
			int num = 5;
			List<ElemClassWithCollection> list = new List<ElemClassWithCollection>(num);
			CollectionsMarshal.SetCount(list, num);
			Span<ElemClassWithCollection> span = CollectionsMarshal.AsSpan(list);
			int num2 = 0;
			ref ElemClassWithCollection reference = ref span[num2];
			ElemClassWithCollection obj = new ElemClassWithCollection
			{
				intField = 1
			};
			int num3 = 5;
			List<int> list2 = new List<int>(num3);
			CollectionsMarshal.SetCount(list2, num3);
			Span<int> span2 = CollectionsMarshal.AsSpan(list2);
			int num4 = 0;
			span2[num4] = 1;
			num4++;
			span2[num4] = 2;
			num4++;
			span2[num4] = 3;
			num4++;
			span2[num4] = 4;
			span2[num4 + 1] = 5;
			obj.intListField = list2;
			obj.StringProp = "string_1";
			obj.outOfOrderField = 2;
			reference = obj;
			num2++;
			ref ElemClassWithCollection reference2 = ref span[num2];
			ElemClassWithCollection obj2 = new ElemClassWithCollection
			{
				intField = 2
			};
			num4 = 5;
			List<int> list3 = new List<int>(num4);
			CollectionsMarshal.SetCount(list3, num4);
			Span<int> span3 = CollectionsMarshal.AsSpan(list3);
			num3 = 0;
			span3[num3] = 6;
			num3++;
			span3[num3] = 7;
			num3++;
			span3[num3] = 8;
			num3++;
			span3[num3] = 9;
			span3[num3 + 1] = 10;
			obj2.intListField = list3;
			obj2.StringProp = "string_2";
			obj2.outOfOrderField = 3;
			reference2 = obj2;
			num2++;
			ref ElemClassWithCollection reference3 = ref span[num2];
			ElemClassWithCollection obj3 = new ElemClassWithCollection
			{
				intField = 3
			};
			num3 = 5;
			List<int> list4 = new List<int>(num3);
			CollectionsMarshal.SetCount(list4, num3);
			Span<int> span4 = CollectionsMarshal.AsSpan(list4);
			num4 = 0;
			span4[num4] = 11;
			num4++;
			span4[num4] = 12;
			num4++;
			span4[num4] = 13;
			num4++;
			span4[num4] = 14;
			span4[num4 + 1] = 15;
			obj3.intListField = list4;
			obj3.StringProp = "string_3";
			obj3.outOfOrderField = 4;
			reference3 = obj3;
			num2++;
			ref ElemClassWithCollection reference4 = ref span[num2];
			ElemClassWithCollection obj4 = new ElemClassWithCollection
			{
				intField = 4
			};
			num4 = 5;
			List<int> list5 = new List<int>(num4);
			CollectionsMarshal.SetCount(list5, num4);
			Span<int> span5 = CollectionsMarshal.AsSpan(list5);
			num3 = 0;
			span5[num3] = 16;
			num3++;
			span5[num3] = 17;
			num3++;
			span5[num3] = 18;
			num3++;
			span5[num3] = 19;
			span5[num3 + 1] = 20;
			obj4.intListField = list5;
			obj4.StringProp = "string_4";
			obj4.outOfOrderField = 5;
			reference4 = obj4;
			ref ElemClassWithCollection reference5 = ref span[num2 + 1];
			ElemClassWithCollection obj5 = new ElemClassWithCollection
			{
				intField = 5
			};
			num3 = 5;
			List<int> list6 = new List<int>(num3);
			CollectionsMarshal.SetCount(list6, num3);
			Span<int> span6 = CollectionsMarshal.AsSpan(list6);
			num4 = 0;
			span6[num4] = 21;
			num4++;
			span6[num4] = 22;
			num4++;
			span6[num4] = 23;
			num4++;
			span6[num4] = 24;
			span6[num4 + 1] = 25;
			obj5.intListField = list6;
			obj5.StringProp = "string_5";
			obj5.outOfOrderField = 6;
			reference5 = obj5;
			elems = list;
			base._002Ector();
		}
	}
}
