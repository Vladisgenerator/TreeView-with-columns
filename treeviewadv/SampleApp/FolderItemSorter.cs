using System;
using System.Collections;
using System.Windows.Forms;

namespace SampleApp
{
	public class FolderItemSorter : IComparer
	{
		private string _mode;
		private SortOrder _order;

		public FolderItemSorter(string mode, SortOrder order)
		{
			_mode = mode;
			_order = order;
		}

		public int Compare(object x, object y)
		{
			BaseItem a = x as BaseItem;
			BaseItem b = y as BaseItem;
			int res = 0;

			if (_mode == "Date")
				res = DateTime.Compare(a.Param2, b.Param2);
			else if (_mode == "Size")
			{
				if (a.Param1 < b.Param1)
					res = -1;
				else if (a.Param1 > b.Param1)
					res = 1;
			}
			else
				res = string.Compare(a.Name, b.Name);

			if (_order == SortOrder.Ascending)
				return -res;
			else
				return res;
		}

		private string GetData(object x)
		{
			return (x as BaseItem).Name;
		}
	}
}
