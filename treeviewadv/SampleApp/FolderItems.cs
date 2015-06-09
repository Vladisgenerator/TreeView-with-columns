using System;
using System.Drawing;
using System.IO;

namespace SampleApp
{
    /// <summary>
    /// ������� ������ ����������� � ������
    /// </summary>
	public abstract class BaseItem
	{
        /// <summary>
        /// ��� �������
        /// </summary>
        public abstract string Name
        {
            get;
            set;
        }

        private Image _icon;
        
        /// <summary>
        /// ������, ������������ � ������ ����� ����� �������
        /// </summary>
        public Image Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        private long param1; // = String.Empty;

        /// <summary>
        /// ������ �������� �������
        /// </summary>
        public long Param1
        {
            get { return param1; }
            set { param1 = value; }
        }

        private DateTime param2;

        /// <summary>
        /// ������ �������� �������
        /// </summary>
        public DateTime Param2
        {
            get { return param2; }
            set { param2 = value; }
        }

        private BaseItem _parent;

        /// <summary>
        /// ������������ ������
        /// </summary>
        public BaseItem Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

		private bool _isChecked;

        /// <summary>
        /// ������������� ��� ���������� ��������, ������������, ��� �� ������� ������ ������
        /// </summary>
		public bool IsChecked
		{
			get { return _isChecked; }
			set 
			{ 
				_isChecked = value;
				if (Owner != null)
					Owner.OnNodesChanged(this);
			}
		}

		private FolderBrowserModel _owner;

        /// <summary>
        /// ������-�������� �������
        /// </summary>
		public FolderBrowserModel Owner
		{
			get { return _owner; }
			set { _owner = value; }
		}


		/*
        public override bool Equals(object obj)
		{
			if (obj is BaseItem)
				return Name.Equals((obj as BaseItem).Name);
			else
				return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}*/

		public override string ToString()
		{
			return this.Name;
		}
	}


    /// <summary>
    /// �������� ������ ������
    /// </summary>
	public class RootItem : BaseItem
	{
        /// <summary>
        /// ������������� ��������� �������
        /// </summary>
        /// <param name="name">��� ��������� �������</param>
        /// <param name="owner">������-�������� ��������� �������</param>
		public RootItem(string name, FolderBrowserModel owner)
		{
			Name = name;
			Owner = owner;
		}

        /// <summary>
        /// ��� ��������� �������
        /// </summary>
		public override string Name
		{
			get;
			set;
		}        
	}


    /// <summary>
    /// ������������� ������ ������
    /// </summary>
	public class FolderItem : BaseItem
	{
        /// <summary>
        /// ��� �������������� �������
        /// </summary>
        public override string Name
        {
            get;
            set;
        }

        /// <summary>
        /// ������������� �������������� ������� ������
        /// </summary>
        /// <param name="name">��� �������������� �������</param>
        /// <param name="parent">������������ ������</param>
        /// <param name="owner">������-�������� �������</param>
		public FolderItem(string name, BaseItem parent, FolderBrowserModel owner)
		{
			Name = name;
			Parent = parent;
			Owner = owner;
		}
	}


    /// <summary>
    /// �������� ������ ������
    /// </summary>
	public class FileItem : BaseItem
	{
        /// <summary>
        /// ��� ��������� �������
        /// </summary>
        public override string Name
        {
            get;
            set;
        }

        /// <summary>
        /// ������������� ��������� ������� ������
        /// </summary>
        /// <param name="name">��� ��������� �������</param>
        /// <param name="parent">������������ ������</param>
        /// <param name="owner">������-�������� ������</param>
		public FileItem(string name, BaseItem parent, FolderBrowserModel owner)
		{
			Name = name;
			Parent = parent;
			Owner = owner;
		}
	}
}
