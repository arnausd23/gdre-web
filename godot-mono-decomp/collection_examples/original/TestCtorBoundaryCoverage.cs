using System.Collections.Generic;

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
		public readonly List<string> names = ["alpha", "beta", "gamma"];

		public TailBoundaryDerived()
			: base([1, 2, 3])
		{
		}
	}

	public class TransitionBoundaryDerived : BoundaryBase
	{
		public readonly List<int> values = [10, 20, 30];

		public string data;

		public TransitionBoundaryDerived()
			: base([4, 5, 6])
		{
			data = ComputeData();
		}

		private static string ComputeData()
		{
			return "from-function";
		}
	}
}
