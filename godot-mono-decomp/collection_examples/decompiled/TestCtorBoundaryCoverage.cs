using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ConsoleApp2;

public static class TestCtorBoundaryCoverage
{
	public class BoundaryBase
	{
		public readonly List<int> baseNumbers;

		public BoundaryBase(List<int> baseNumbers)
		{
			this.baseNumbers = baseNumbers;
		}
	}

	public class TailBoundaryDerived : BoundaryBase
	{
		public readonly List<string> names;

		public TailBoundaryDerived()
		{
			int num = 3;
			List<string> list = new List<string>(num);
			CollectionsMarshal.SetCount(list, num);
			Span<string> span = CollectionsMarshal.AsSpan(list);
			int num2 = 0;
			span[num2] = "alpha";
			num2++;
			span[num2] = "beta";
			span[num2 + 1] = "gamma";
			names = list;
			num2 = 3;
			List<int> list2 = new List<int>(num2);
			CollectionsMarshal.SetCount(list2, num2);
			Span<int> span2 = CollectionsMarshal.AsSpan(list2);
			num = 0;
			span2[num] = 1;
			num++;
			span2[num] = 2;
			span2[num + 1] = 3;
			base._002Ector(list2);
		}
	}

	public class TransitionBoundaryDerived : BoundaryBase
	{
		public readonly List<int> values;

		public string data;

		public TransitionBoundaryDerived()
		{
			int num = 3;
			List<int> list = new List<int>(num);
			CollectionsMarshal.SetCount(list, num);
			Span<int> span = CollectionsMarshal.AsSpan(list);
			int num2 = 0;
			span[num2] = 10;
			num2++;
			span[num2] = 20;
			span[num2 + 1] = 30;
			values = list;
			num2 = 3;
			List<int> list2 = new List<int>(num2);
			CollectionsMarshal.SetCount(list2, num2);
			Span<int> span2 = CollectionsMarshal.AsSpan(list2);
			num = 0;
			span2[num] = 4;
			num++;
			span2[num] = 5;
			span2[num + 1] = 6;
			base._002Ector(list2);
			data = ComputeData();
		}

		private static string ComputeData()
		{
			return "from-function";
		}
	}
}
