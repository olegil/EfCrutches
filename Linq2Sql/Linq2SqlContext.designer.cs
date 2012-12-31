﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Linq2Sql
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="EfCrutches")]
	public partial class Linq2SqlDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertAccount(Account instance);
    partial void UpdateAccount(Account instance);
    partial void DeleteAccount(Account instance);
    partial void InsertThreadsInFolder(ThreadsInFolder instance);
    partial void UpdateThreadsInFolder(ThreadsInFolder instance);
    partial void DeleteThreadsInFolder(ThreadsInFolder instance);
    partial void InsertMessage(Message instance);
    partial void UpdateMessage(Message instance);
    partial void DeleteMessage(Message instance);
    partial void InsertMessageFolder(MessageFolder instance);
    partial void UpdateMessageFolder(MessageFolder instance);
    partial void DeleteMessageFolder(MessageFolder instance);
    partial void InsertThread(Thread instance);
    partial void UpdateThread(Thread instance);
    partial void DeleteThread(Thread instance);
    #endregion
		
		public Linq2SqlDataContext() : 
				base(global::Linq2Sql.Properties.Settings.Default.EfCrutchesConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public Linq2SqlDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public Linq2SqlDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public Linq2SqlDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public Linq2SqlDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Account> Accounts
		{
			get
			{
				return this.GetTable<Account>();
			}
		}
		
		public System.Data.Linq.Table<ThreadsInFolder> ThreadsInFolders
		{
			get
			{
				return this.GetTable<ThreadsInFolder>();
			}
		}
		
		public System.Data.Linq.Table<Message> Messages
		{
			get
			{
				return this.GetTable<Message>();
			}
		}
		
		public System.Data.Linq.Table<MessageFolder> MessageFolders
		{
			get
			{
				return this.GetTable<MessageFolder>();
			}
		}
		
		public System.Data.Linq.Table<Thread> Threads
		{
			get
			{
				return this.GetTable<Thread>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Account")]
	public partial class Account : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Name;
		
		private EntitySet<MessageFolder> _MessageFolders;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
		
		public Account()
		{
			this._MessageFolders = new EntitySet<MessageFolder>(new Action<MessageFolder>(this.attach_MessageFolders), new Action<MessageFolder>(this.detach_MessageFolders));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Account_MessageFolder", Storage="_MessageFolders", ThisKey="Id", OtherKey="Owner_Id")]
		public EntitySet<MessageFolder> MessageFolders
		{
			get
			{
				return this._MessageFolders;
			}
			set
			{
				this._MessageFolders.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_MessageFolders(MessageFolder entity)
		{
			this.SendPropertyChanging();
			entity.Account = this;
		}
		
		private void detach_MessageFolders(MessageFolder entity)
		{
			this.SendPropertyChanging();
			entity.Account = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ThreadsInFolders")]
	public partial class ThreadsInFolder : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _MessageFolder_Id;
		
		private int _MessageThread_Id;
		
		private EntityRef<MessageFolder> _MessageFolder;
		
		private EntityRef<Thread> _MessageThread;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnMessageFolder_IdChanging(int value);
    partial void OnMessageFolder_IdChanged();
    partial void OnMessageThread_IdChanging(int value);
    partial void OnMessageThread_IdChanged();
    #endregion
		
		public ThreadsInFolder()
		{
			this._MessageFolder = default(EntityRef<MessageFolder>);
			this._MessageThread = default(EntityRef<Thread>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MessageFolder_Id", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int MessageFolder_Id
		{
			get
			{
				return this._MessageFolder_Id;
			}
			set
			{
				if ((this._MessageFolder_Id != value))
				{
					if (this._MessageFolder.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnMessageFolder_IdChanging(value);
					this.SendPropertyChanging();
					this._MessageFolder_Id = value;
					this.SendPropertyChanged("MessageFolder_Id");
					this.OnMessageFolder_IdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MessageThread_Id", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int MessageThread_Id
		{
			get
			{
				return this._MessageThread_Id;
			}
			set
			{
				if ((this._MessageThread_Id != value))
				{
					if (this._MessageThread.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnMessageThread_IdChanging(value);
					this.SendPropertyChanging();
					this._MessageThread_Id = value;
					this.SendPropertyChanged("MessageThread_Id");
					this.OnMessageThread_IdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="MessageFolder_ThreadsInFolder", Storage="_MessageFolder", ThisKey="MessageFolder_Id", OtherKey="Id", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public MessageFolder MessageFolder
		{
			get
			{
				return this._MessageFolder.Entity;
			}
			set
			{
				MessageFolder previousValue = this._MessageFolder.Entity;
				if (((previousValue != value) 
							|| (this._MessageFolder.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._MessageFolder.Entity = null;
						previousValue.ThreadsInFolders.Remove(this);
					}
					this._MessageFolder.Entity = value;
					if ((value != null))
					{
						value.ThreadsInFolders.Add(this);
						this._MessageFolder_Id = value.Id;
					}
					else
					{
						this._MessageFolder_Id = default(int);
					}
					this.SendPropertyChanged("MessageFolder");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Thread_ThreadsInFolder", Storage="_MessageThread", ThisKey="MessageThread_Id", OtherKey="Id", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public Thread Thread
		{
			get
			{
				return this._MessageThread.Entity;
			}
			set
			{
				Thread previousValue = this._MessageThread.Entity;
				if (((previousValue != value) 
							|| (this._MessageThread.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._MessageThread.Entity = null;
						previousValue.ThreadsInFolders.Remove(this);
					}
					this._MessageThread.Entity = value;
					if ((value != null))
					{
						value.ThreadsInFolders.Add(this);
						this._MessageThread_Id = value.Id;
					}
					else
					{
						this._MessageThread_Id = default(int);
					}
					this.SendPropertyChanged("Thread");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Message")]
	public partial class Message : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private bool _IsRead;
		
		private System.DateTime _Date;
		
		private string _Text;
		
		private int _Thread_Id;
		
		private int _Owner_Id;
		
		private int _Sender_Id;
		
		private int _Reciever_Id;
		
		private EntityRef<Account> _Account;
		
		private EntityRef<Account> _Account1;
		
		private EntityRef<Account> _Account2;
		
		private EntityRef<Thread> _MessageThread;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnIsReadChanging(bool value);
    partial void OnIsReadChanged();
    partial void OnDateChanging(System.DateTime value);
    partial void OnDateChanged();
    partial void OnTextChanging(string value);
    partial void OnTextChanged();
    partial void OnThread_IdChanging(int value);
    partial void OnThread_IdChanged();
    partial void OnOwner_IdChanging(int value);
    partial void OnOwner_IdChanged();
    partial void OnSender_IdChanging(int value);
    partial void OnSender_IdChanged();
    partial void OnReceiver_IdChanging(int value);
    partial void OnReceiver_IdChanged();
    #endregion
		
		public Message()
		{
			this._Account = default(EntityRef<Account>);
			this._Account1 = default(EntityRef<Account>);
			this._Account2 = default(EntityRef<Account>);
			this._MessageThread = default(EntityRef<Thread>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsRead", DbType="Bit NOT NULL")]
		public bool IsRead
		{
			get
			{
				return this._IsRead;
			}
			set
			{
				if ((this._IsRead != value))
				{
					this.OnIsReadChanging(value);
					this.SendPropertyChanging();
					this._IsRead = value;
					this.SendPropertyChanged("IsRead");
					this.OnIsReadChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Date", DbType="DateTime NOT NULL")]
		public System.DateTime Date
		{
			get
			{
				return this._Date;
			}
			set
			{
				if ((this._Date != value))
				{
					this.OnDateChanging(value);
					this.SendPropertyChanging();
					this._Date = value;
					this.SendPropertyChanged("Date");
					this.OnDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Text", DbType="NVarChar(MAX)")]
		public string Text
		{
			get
			{
				return this._Text;
			}
			set
			{
				if ((this._Text != value))
				{
					this.OnTextChanging(value);
					this.SendPropertyChanging();
					this._Text = value;
					this.SendPropertyChanged("Text");
					this.OnTextChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Thread_Id", DbType="Int NOT NULL")]
		public int Thread_Id
		{
			get
			{
				return this._Thread_Id;
			}
			set
			{
				if ((this._Thread_Id != value))
				{
					if (this._MessageThread.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnThread_IdChanging(value);
					this.SendPropertyChanging();
					this._Thread_Id = value;
					this.SendPropertyChanged("Thread_Id");
					this.OnThread_IdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Owner_Id", DbType="Int NOT NULL")]
		public int Owner_Id
		{
			get
			{
				return this._Owner_Id;
			}
			set
			{
				if ((this._Owner_Id != value))
				{
					if (this._Account.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnOwner_IdChanging(value);
					this.SendPropertyChanging();
					this._Owner_Id = value;
					this.SendPropertyChanged("Owner_Id");
					this.OnOwner_IdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Sender_Id", DbType="Int NOT NULL")]
		public int Sender_Id
		{
			get
			{
				return this._Sender_Id;
			}
			set
			{
				if ((this._Sender_Id != value))
				{
					if (this._Account2.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnSender_IdChanging(value);
					this.SendPropertyChanging();
					this._Sender_Id = value;
					this.SendPropertyChanged("Sender_Id");
					this.OnSender_IdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Reciever_Id", DbType="Int NOT NULL")]
		public int Receiver_Id
		{
			get
			{
				return this._Reciever_Id;
			}
			set
			{
				if ((this._Reciever_Id != value))
				{
					if (this._Account1.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnReceiver_IdChanging(value);
					this.SendPropertyChanging();
					this._Reciever_Id = value;
					this.SendPropertyChanged("Receiver_Id");
					this.OnReceiver_IdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Account_Message", Storage="_Account", ThisKey="Owner_Id", OtherKey="Id", IsForeignKey=true)]
		public Account Owner
		{
			get
			{
				return this._Account.Entity;
			}
			set
			{
				if ((this._Account.Entity != value))
				{
					this.SendPropertyChanging();
					this._Account.Entity = value;
					this.SendPropertyChanged("Owner");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Account_Message1", Storage="_Account1", ThisKey="Receiver_Id", OtherKey="Id", IsForeignKey=true)]
		public Account Reciever
		{
			get
			{
				return this._Account1.Entity;
			}
			set
			{
				if ((this._Account1.Entity != value))
				{
					this.SendPropertyChanging();
					this._Account1.Entity = value;
					this.SendPropertyChanged("Reciever");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Account_Message2", Storage="_Account2", ThisKey="Sender_Id", OtherKey="Id", IsForeignKey=true)]
		public Account Sender
		{
			get
			{
				return this._Account2.Entity;
			}
			set
			{
				if ((this._Account2.Entity != value))
				{
					this.SendPropertyChanging();
					this._Account2.Entity = value;
					this.SendPropertyChanged("Sender");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Thread_Message", Storage="_MessageThread", ThisKey="Thread_Id", OtherKey="Id", IsForeignKey=true)]
		public Thread Thread
		{
			get
			{
				return this._MessageThread.Entity;
			}
			set
			{
				Thread previousValue = this._MessageThread.Entity;
				if (((previousValue != value) 
							|| (this._MessageThread.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._MessageThread.Entity = null;
						previousValue.Messages.Remove(this);
					}
					this._MessageThread.Entity = value;
					if ((value != null))
					{
						value.Messages.Add(this);
						this._Thread_Id = value.Id;
					}
					else
					{
						this._Thread_Id = default(int);
					}
					this.SendPropertyChanged("Thread");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.MessageFolder")]
	public partial class MessageFolder : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Name;
		
		private int _Owner_Id;
		
		private EntitySet<ThreadsInFolder> _ThreadsInFolders;
		
		private EntityRef<Account> _Account;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnOwner_IdChanging(int value);
    partial void OnOwner_IdChanged();
    #endregion
		
		public MessageFolder()
		{
			this._ThreadsInFolders = new EntitySet<ThreadsInFolder>(new Action<ThreadsInFolder>(this.attach_ThreadsInFolders), new Action<ThreadsInFolder>(this.detach_ThreadsInFolders));
			this._Account = default(EntityRef<Account>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(MAX)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Owner_Id", DbType="Int NOT NULL")]
		public int Owner_Id
		{
			get
			{
				return this._Owner_Id;
			}
			set
			{
				if ((this._Owner_Id != value))
				{
					if (this._Account.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnOwner_IdChanging(value);
					this.SendPropertyChanging();
					this._Owner_Id = value;
					this.SendPropertyChanged("Owner_Id");
					this.OnOwner_IdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="MessageFolder_ThreadsInFolder", Storage="_ThreadsInFolders", ThisKey="Id", OtherKey="MessageFolder_Id")]
		public EntitySet<ThreadsInFolder> ThreadsInFolders
		{
			get
			{
				return this._ThreadsInFolders;
			}
			set
			{
				this._ThreadsInFolders.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Account_MessageFolder", Storage="_Account", ThisKey="Owner_Id", OtherKey="Id", IsForeignKey=true)]
		public Account Account
		{
			get
			{
				return this._Account.Entity;
			}
			set
			{
				Account previousValue = this._Account.Entity;
				if (((previousValue != value) 
							|| (this._Account.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Account.Entity = null;
						previousValue.MessageFolders.Remove(this);
					}
					this._Account.Entity = value;
					if ((value != null))
					{
						value.MessageFolders.Add(this);
						this._Owner_Id = value.Id;
					}
					else
					{
						this._Owner_Id = default(int);
					}
					this.SendPropertyChanged("Account");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_ThreadsInFolders(ThreadsInFolder entity)
		{
			this.SendPropertyChanging();
			entity.MessageFolder = this;
		}
		
		private void detach_ThreadsInFolders(ThreadsInFolder entity)
		{
			this.SendPropertyChanging();
			entity.MessageFolder = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.MessageThread")]
	public partial class Thread : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Subject;
		
		private EntitySet<ThreadsInFolder> _ThreadsInFolders;
		
		private EntitySet<Message> _Messages;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnSubjectChanging(string value);
    partial void OnSubjectChanged();
    #endregion
		
		public Thread()
		{
			this._ThreadsInFolders = new EntitySet<ThreadsInFolder>(new Action<ThreadsInFolder>(this.attach_ThreadsInFolders), new Action<ThreadsInFolder>(this.detach_ThreadsInFolders));
			this._Messages = new EntitySet<Message>(new Action<Message>(this.attach_Messages), new Action<Message>(this.detach_Messages));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Subject", DbType="NVarChar(150) NOT NULL", CanBeNull=false)]
		public string Subject
		{
			get
			{
				return this._Subject;
			}
			set
			{
				if ((this._Subject != value))
				{
					this.OnSubjectChanging(value);
					this.SendPropertyChanging();
					this._Subject = value;
					this.SendPropertyChanged("Subject");
					this.OnSubjectChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Thread_ThreadsInFolder", Storage="_ThreadsInFolders", ThisKey="Id", OtherKey="MessageThread_Id")]
		public EntitySet<ThreadsInFolder> ThreadsInFolders
		{
			get
			{
				return this._ThreadsInFolders;
			}
			set
			{
				this._ThreadsInFolders.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Thread_Message", Storage="_Messages", ThisKey="Id", OtherKey="Thread_Id")]
		public EntitySet<Message> Messages
		{
			get
			{
				return this._Messages;
			}
			set
			{
				this._Messages.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_ThreadsInFolders(ThreadsInFolder entity)
		{
			this.SendPropertyChanging();
			entity.Thread = this;
		}
		
		private void detach_ThreadsInFolders(ThreadsInFolder entity)
		{
			this.SendPropertyChanging();
			entity.Thread = null;
		}
		
		private void attach_Messages(Message entity)
		{
			this.SendPropertyChanging();
			entity.Thread = this;
		}
		
		private void detach_Messages(Message entity)
		{
			this.SendPropertyChanging();
			entity.Thread = null;
		}
	}
}
#pragma warning restore 1591
