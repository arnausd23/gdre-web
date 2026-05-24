using System.Collections.Generic;

namespace ConsoleApp2;

public static class TestCollectionInitWithSpread
{
	public class TestStaticHashSetMemberSpreadInit
	{
		public static readonly HashSet<string> strings;

		static TestStaticHashSetMemberSpreadInit()
		{
			HashSet<string> hashSet = new HashSet<string>();
			foreach (string item in stringListConst1)
			{
				hashSet.Add(item);
			}
			foreach (string item2 in stringListConst2)
			{
				hashSet.Add(item2);
			}
			foreach (string item3 in stringListConst3)
			{
				hashSet.Add(item3);
			}
			hashSet.Add("sixteen");
			hashSet.Add("seventeen");
			hashSet.Add("eighteen");
			hashSet.Add("nineteen");
			hashSet.Add("twenty");
			strings = hashSet;
		}
	}

	public class TestStaticListMemberSpreadInit
	{
		public static readonly List<string> strings;

		static TestStaticListMemberSpreadInit()
		{
			List<string> list = new List<string>();
			list.AddRange(stringListConst1);
			list.AddRange(stringListConst2);
			list.AddRange(stringListConst3);
			list.Add("sixteen");
			list.Add("seventeen");
			list.Add("eighteen");
			list.Add("nineteen");
			list.Add("twenty");
			strings = list;
		}
	}

	public class TestStaticReadOnlySetMemberSpreadInit
	{
		public static readonly IReadOnlySet<string> strings;

		static TestStaticReadOnlySetMemberSpreadInit()
		{
			List<string> list = new List<string>();
			list.AddRange(stringListConst1);
			list.AddRange(stringListConst2);
			list.AddRange(stringListConst3);
			list.Add("sixteen");
			list.Add("seventeen");
			list.Add("eighteen");
			list.Add("nineteen");
			list.Add("twenty");
			strings = new HashSet<string>(new _003C_003Ez__ReadOnlyList<string>(list));
		}
	}

	public class TestStaticReadOnlyListMemberSpreadInit
	{
		public static readonly IReadOnlyList<string> strings;

		static TestStaticReadOnlyListMemberSpreadInit()
		{
			List<string> list = new List<string>();
			list.AddRange(stringListConst1);
			list.AddRange(stringListConst2);
			list.AddRange(stringListConst3);
			list.Add("sixteen");
			list.Add("seventeen");
			list.Add("eighteen");
			list.Add("nineteen");
			list.Add("twenty");
			strings = new _003C_003Ez__ReadOnlyList<string>(list);
		}
	}

	private static IEnumerable<string> stringListConst1 => new _003C_003Ez__ReadOnlyArray<string>(new string[5] { "one", "two", "three", "four", "five" });

	private static IEnumerable<string> stringListConst2 => new _003C_003Ez__ReadOnlyArray<string>(new string[5] { "six", "seven", "eight", "nine", "ten" });

	private static IEnumerable<string> stringListConst3 => new _003C_003Ez__ReadOnlyArray<string>(new string[5] { "eleven", "twelve", "thirteen", "fourteen", "fifteen" });
}
