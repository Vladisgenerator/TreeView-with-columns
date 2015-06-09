using System;
using System.Drawing;
using System.IO;

namespace SampleApp
{
    /// <summary>
    /// Базовый объект отображения в дереве
    /// </summary>
	public abstract class BaseItem
	{
        /// <summary>
        /// Имя объекта
        /// </summary>
        public abstract string Name
        {
            get;
            set;
        }

        private Image _icon;
        
        /// <summary>
        /// Иконка, отображаемая в дереве возле имени объекта
        /// </summary>
        public Image Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        private long param1; // = String.Empty;

        /// <summary>
        /// Первый параметр объекта
        /// </summary>
        public long Param1
        {
            get { return param1; }
            set { param1 = value; }
        }

        private DateTime param2;

        /// <summary>
        /// Второй параметр объекта
        /// </summary>
        public DateTime Param2
        {
            get { return param2; }
            set { param2 = value; }
        }

        private BaseItem _parent;

        /// <summary>
        /// Родительский объект
        /// </summary>
        public BaseItem Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

		private bool _isChecked;

        /// <summary>
        /// Устанавливает или возвращает значение, указывыающее, был ли отмечен данный объект
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
        /// Модель-владелец объекта
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
    /// Корневой объект дерева
    /// </summary>
	public class RootItem : BaseItem
	{
        /// <summary>
        /// Инициализация корневого объекта
        /// </summary>
        /// <param name="name">Имя корневого объекта</param>
        /// <param name="owner">Модель-владелец корневого объекта</param>
		public RootItem(string name, FolderBrowserModel owner)
		{
			Name = name;
			Owner = owner;
		}

        /// <summary>
        /// Имя корневого объекта
        /// </summary>
		public override string Name
		{
			get;
			set;
		}        
	}


    /// <summary>
    /// Промежуточный объект дерева
    /// </summary>
	public class FolderItem : BaseItem
	{
        /// <summary>
        /// Имя промежуточного объекта
        /// </summary>
        public override string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Инициализация промежуточного объекта дерева
        /// </summary>
        /// <param name="name">Имя промежуточного объекта</param>
        /// <param name="parent">Родительский объект</param>
        /// <param name="owner">Модель-владелец объекта</param>
		public FolderItem(string name, BaseItem parent, FolderBrowserModel owner)
		{
			Name = name;
			Parent = parent;
			Owner = owner;
		}
	}


    /// <summary>
    /// Листовой объект дерева
    /// </summary>
	public class FileItem : BaseItem
	{
        /// <summary>
        /// Имя листового объекта
        /// </summary>
        public override string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Инициализация листового объекта дерева
        /// </summary>
        /// <param name="name">Имя листового объекта</param>
        /// <param name="parent">Родительский объект</param>
        /// <param name="owner">Модель-владелец дерева</param>
		public FileItem(string name, BaseItem parent, FolderBrowserModel owner)
		{
			Name = name;
			Parent = parent;
			Owner = owner;
		}
	}
}
