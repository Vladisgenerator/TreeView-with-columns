#pragma warning disable 67  // Event never used

using Aga.Controls.Tree;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;

namespace SampleApp
{
	public class FolderBrowserModel: ITreeModel
	{
		private BackgroundWorker _worker;
		private List<BaseItem> _itemsToRead;
		private Dictionary<string, List<BaseItem>> _cache = new Dictionary<string, List<BaseItem>>();

		public FolderBrowserModel()
		{
			_itemsToRead = new List<BaseItem>();

			_worker = new BackgroundWorker();
			_worker.WorkerReportsProgress = true;
			_worker.DoWork += new DoWorkEventHandler(ReadFilesProperties);
			_worker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
		}

		void ReadFilesProperties(object sender, DoWorkEventArgs e)
		{
			while(_itemsToRead.Count > 0)
			{
				BaseItem item = _itemsToRead[0];
				_itemsToRead.RemoveAt(0);

				Thread.Sleep(50); //emulate time consuming operation
				if (item is FolderItem)
				{
					DirectoryInfo info = new DirectoryInfo(item.Name);
					item.Param2 = info.CreationTime;
				}
				else if (item is FileItem)
				{
					FileInfo info = new FileInfo(item.Name);
					item.Param1 = info.Length;
					item.Param2 = info.CreationTime;
					if (info.Extension.ToLower() == ".ico")
					{
						Icon icon = new Icon(item.Name);
						item.Icon = icon.ToBitmap();
					}
					else if (info.Extension.ToLower() == ".bmp")
					{
						item.Icon = new Bitmap(item.Name);
					}
				}
				_worker.ReportProgress(0, item);
			}
		}

		void ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			OnNodesChanged(e.UserState as BaseItem);
		}

		private TreePath GetPath(BaseItem item)
		{
			if (item == null)
				return TreePath.Empty;
			else
			{
				Stack<object> stack = new Stack<object>();
				while (item != null)
				{
					stack.Push(item);
					item = item.Parent;
				}
				return new TreePath(stack.ToArray());
			}
		}

		public System.Collections.IEnumerable GetChildren(TreePath treePath)
		{
			List<BaseItem> items = null;
			if (treePath.IsEmpty())
			{
				if (_cache.ContainsKey("ROOT"))
					items = _cache["ROOT"];
				else
				{
					items = new List<BaseItem>();
					_cache.Add("ROOT", items);
					foreach (string str in Environment.GetLogicalDrives())
						items.Add(new RootItem(str, this));
				}
			}
			else
			{
				BaseItem parent = treePath.LastNode as BaseItem;
				if (parent != null)
				{
					if (_cache.ContainsKey(parent.Name))
						items = _cache[parent.Name];
					else
					{
						items = new List<BaseItem>();
						try
						{
							foreach (string str in Directory.GetDirectories(parent.Name))
								items.Add(new FolderItem(str, parent, this));
							foreach (string str in Directory.GetFiles(parent.Name))
							{
								FileItem item = new FileItem(str, parent, this);
								items.Add(item);
							}
						}
						catch (IOException)
						{
							return null;
						}
						_cache.Add(parent.Name, items);
						_itemsToRead.AddRange(items);
						if (!_worker.IsBusy)
							_worker.RunWorkerAsync();
					}
				}
			}
			return items;
		}

		public bool IsLeaf(TreePath treePath)
		{
			return treePath.LastNode is FileItem;
		}

		public event EventHandler<TreeModelEventArgs> NodesChanged;
		internal void OnNodesChanged(BaseItem item)
		{
			if (NodesChanged != null)
			{
				TreePath path = GetPath(item.Parent);
				NodesChanged(this, new TreeModelEventArgs(path, new object[] { item }));
			}
		}

		public event EventHandler<TreeModelEventArgs> NodesInserted;
		public event EventHandler<TreeModelEventArgs> NodesRemoved;
		public event EventHandler<TreePathEventArgs> StructureChanged;
		public void OnStructureChanged()
		{
			if (StructureChanged != null)
				StructureChanged(this, new TreePathEventArgs());
		}
	}
}
