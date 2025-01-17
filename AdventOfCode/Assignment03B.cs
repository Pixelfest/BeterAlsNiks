﻿using System.Text;

namespace AdventOfCode
{
	public class Assignment03B : Assignment, IAmAnAssignment
	{
		private readonly List<Elf> _list = new();

		public Assignment03B()
		{
			Load("Input/03.txt");
		}

		public override void Process()
		{
			var total = 0;

			for (var i = 0; i < _list.Count; i += 3)
			{
				var sharedItems = _list[i].Inventory1.Where(item1 => _list[i + 1].Inventory1.Contains(item1));
				var oneItem = sharedItems.FirstOrDefault(itemShared => _list[i + 2].Inventory1.Contains(itemShared));

				total += oneItem;
			}

			Output = $"{total}";
		}

		protected override void ReadLine(string line)
		{
			var elf = new Elf { Inventory1 = line.Select(Get).ToList() };

			_list.Add(elf);
		}

		private byte Get(char a)
		{
			var b = Encoding.ASCII.GetBytes(a.ToString())[0];

			if (b > 96 && b < 123)
				b = (byte)(b - 96);
			else
				b = (byte)(b - 64 + 26);

			return b;
		}

		private class Elf
		{
			public List<byte> Inventory1 { get; init; } = new();
		}
	}
}
