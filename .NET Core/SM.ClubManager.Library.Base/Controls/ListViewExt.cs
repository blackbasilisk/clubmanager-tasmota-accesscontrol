using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SM.ClubManager.Library.Base.Infrastructure.WindowsFormsExtensions;
using SM.ClubManager.Library.Base.Infrastructure.Extensions;
using SM.ClubManager.Library.Base.ControlHelpers;
using System.Drawing;
using System.Threading;
using SM.ClubManager.Library.Base.Infrastructure.Exceptions;
using System.Collections.Concurrent;

namespace SM.ClubManager.Library.Base.Controls
{
    public partial class ListViewExt : ListView, IDisposable
    {
        #region Private Vars            
            private object lockObject;  
            private Color defaultForegroundColour;
            private Color defaultBackgroundColour;
            //private List<ListViewExt> cloneLists;
            private Thread threadMessageQueue;

            CancellationTokenSource cts;
            bool isClosing;
            //private object messageQueueLockObject;        
            private object logLock;
            BlockingCollection<LogEntry> messageQueue;
            bool runMessageQueueLoop;
        BackgroundWorker bwLogProcessor;

        SynchronizationContext uiContext;

        #endregion

        #region Events  & Delegates
        public event ListViewItemDelegate ItemAdded;
            public event ListViewItemRangeDelegate ItemRangeAdded;
            public event ListViewRemoveDelegate ItemRemoved;
            public event ListViewRemoveAtDelegate ItemRemovedAt;

            public delegate void ListViewItemDelegate(ListViewItem item);
            public delegate void ListViewItemRangeDelegate(ListViewItem[] item);
            public delegate void ListViewRemoveDelegate(ListViewItem item);
            public delegate void ListViewRemoveAtDelegate(int index, ListViewItem item);
            #endregion
        
        #region Public Fields
            public new ListViewItemCollectionNewEvent Items;
        #endregion        

        #region Properties
            private bool _isCloneListEntriesEnabled;

            public bool IsCloneListEntriesEnabled
            {
                get { return _isCloneListEntriesEnabled; }
                set { _isCloneListEntriesEnabled = value; }
            }
            
            private bool _isDebugMode;
            public bool IsDebugMode
            {
                get { return _isDebugMode; }
                set { _isDebugMode = value; }
            }

            private bool _includeDateTime;
            public bool IncludeDateTime
            {
                get { return _includeDateTime; }
                set { _includeDateTime = value; }
            }

            private List<ListViewColumn> _listviewColumns;
            public List<ListViewColumn> ListViewColumns
            {
                get { return _listviewColumns; }
            }

            [Obsolete("RESERVED FOR FUTURE USE! This will have no effect when used")]
            public int MaxCharactersPerLine { get; set; }
           
            private int _maxEntries;
            public int MaxEntries
            {
                get
                {
                    return _maxEntries;
                }
                set
                {
                    _maxEntries = value;
                }
            }

            private Color _selectedItemForegroundColor;

            public Color SelectedItemForegroundColor
            {
                get { return _selectedItemForegroundColor; }
                set { _selectedItemForegroundColor = value; }
            }

            private Color _selectedItemBackgroundColor;

            public Color SelectedItemBackgroundColor
            {
                get { return _selectedItemBackgroundColor; }
                set { _selectedItemBackgroundColor = value; }
            }   
        #endregion

        #region Initialize Methods
            public ListViewExt()
                : base()
            {
                InitializeComponent();
                uiContext = SynchronizationContext.Current;
                Initialize();
            }

            public ListViewExt(IContainer container, bool includeDateTime = true)
            {
                if (container != null)
                    container.Add(this);

                InitializeComponent();

                _includeDateTime = includeDateTime;

                Initialize();
            }

            public void Initialize()
            {
                lockObject = new object();
                this.Resize += ListViewExt_Resize;
            isClosing = false;
            cts = new CancellationTokenSource();
            this.BeginUpdate();
                //set default value
                this._maxEntries = 500;
                this.View = View.Details;
                this.FullRowSelect = true;
                this.HeaderStyle = ColumnHeaderStyle.Nonclickable;
                this.FullRowSelect = true;
                this.SelectedItemForegroundColor = System.Drawing.Color.AliceBlue;
                this.SelectedItemBackgroundColor = System.Drawing.Color.White;
                defaultBackgroundColour = DefaultBackColor;
                defaultForegroundColour = DefaultForeColor;
                //messageQueueLockObject = new Object();
            logLock = new Object();
            
                SetupContextMenu();

                ////threadMessageQueue = new Thread(LogLoop);
                ////runMessageQueueLoop = true;
                ////threadMessageQueue.IsBackground = true;
                ////threadMessageQueue.Name = "Logging Thread";
                ////threadMessageQueue.Start();


                Items = new ListViewItemCollectionNewEvent(this);

                //cloneLists  = new List<ListViewExt>();
                
                messageQueue = new BlockingCollection<LogEntry>();

                StartMessageQueueConsumer();

                this.DoubleBuffering(true);
                
                this.EndUpdate();                
            }

        private void StartMessageQueueConsumer()
        {

            try
            {
                messageQueue = new BlockingCollection<LogEntry>();
                bwLogProcessor = new BackgroundWorker();
                bwLogProcessor.DoWork += BwLogProcessor_DoWork;
                bwLogProcessor.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                //we just throw, because  without the log processor the app is useless!! we dnt want the app to run without i
                //so we need to fix if if something goes wrong instead of trying to handle the exception
                throw;
            }





            //Task.Run(() =>
            //    {
            //        try
            //        {
            //            if (!isClosing)
            //            {                           
            //                foreach (LogEntry logEntry in messageQueue.GetConsumingEnumerable())
            //                {
            //                    if (isClosing)
            //                    {
            //                        break;
            //                    }
            //                    ProcessMessageUpdate(logEntry);
            //                }
            //            }
            //        }
            //        catch (OperationCanceledException ex)
            //        {                        
            //            Trace.WriteLine("EXCEPTION: " + ex.Message + " STACKTRACE: " + ex.StackTrace);
            //            Console.WriteLine("EXCEPTION: " + ex.Message + " STACKTRACE: " + ex.StackTrace);
            //        }
            //        catch (Exception ex)
            //        {
            //            throw;
            //        }
            //    });                                             
        }

    
        #endregion

        #region EventHandlers
        private void HandleCopy(object sender, EventArgs e)
            {
                CopyToClipboard();
            }

            private void ListViewExt_Resize(object sender, EventArgs e)
            {
            this.Resize -= ListViewExt_Resize;

            this.BeginUpdate();
                foreach (ColumnHeader item in this.Columns)
                {
                    item.Width = -2;
                }
                this.EndUpdate();
            this.Resize += ListViewExt_Resize;
        }

            private void HandleCut(object sender, EventArgs e) { MessageBox.Show("Not yet implemented, sorry."); }

            private void HandlePaste(object sender, EventArgs e) { MessageBox.Show("Not yet implemented, sorry."); }

        #endregion

        #region Private Methods       
        private void BwLogProcessor_DoWork(object sender, DoWorkEventArgs e)
        {
            
            try
            {
                while (!this.Disposing && !isClosing)
                {                    
                    if(!messageQueue.IsCompleted && messageQueue.Count() > 0)
                    {
                        LogEntry updateLogEntry = messageQueue.Take();
                      
                        ProcessMessageUpdate(updateLogEntry);
                       
                    }
                   
                    if (this.Disposing || isClosing)
                        break;
                    System.Threading.Thread.Sleep(10);
                }
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine("EXCEPTION (BwLogProcessor_DoWork): " + ex.Message + " STACKTRACE: " + ex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION (BwLogProcessor_DoWork): " + ex.Message + " STACKTRACE: " + ex.StackTrace);
                throw;
            }
        }

        private void AddEntryProcess(string text = "", bool isErrorMessage = false, bool isDebugMessage = false, DateTime? overrideDateTime = null)
        {
            try
            {
                if (_listviewColumns == null)
                {
                    _listviewColumns = new List<ListViewColumn>();
                    if (_includeDateTime)
                    {

                        SM.ClubManager.Library.Base.ControlHelpers.ListViewColumn col = ListViewColumn.CreateColumn("DateTime");

                        _listviewColumns.Add(col);
                    }
                    _listviewColumns.Add(ListViewColumn.CreateColumn("Information"));
                }

                if (this.Columns.Count == 0)
                {
                    foreach (var col in _listviewColumns)
                    {
                        this.Columns.Add(col.ColumnHeader, col.Width, col.HorizontalAlignment);
                    }
                }

                Task.Run(() =>
                {
                    if (messageQueue != null && !messageQueue.IsCompleted)
                    {
                        messageQueue.Add(new LogEntry()
                        {
                            IsDebugMessage = isDebugMessage,
                            IsErrorMessage = isErrorMessage,
                            Message = text,
                            OverrideDateTime = overrideDateTime
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during 'AddEntryProcess': " + ex.Message);
                throw;
            }

        }

        //public void LogLoop()
        //{
        //    try
        //    {
        //        while (runMessageQueueLoop)
        //        {
        //            if (messageQueue == null)
        //                continue;

        //            while (messageQueue.Count > 0)
        //            {
        //                try
        //                {
        //                    lock (logLock) // get a lock on the queue
        //                    {
        //                        LogEntry s = new LogEntry();
        //                        bool isTaken = messageQueue.TryTake(out s); //Dequeue();
        //                        if (isTaken)
        //                            ProcessMessageUpdate(s);
        //                    }
        //                }
        //                catch (Exception e)
        //                {
        //                    Console.WriteLine("Error during 'LogLoop': " + e.Message);
        //                    throw;
        //                }
                        
        //            }
        //            //Thread.Sleep(10);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
           
        //}

            private void ProcessMessageUpdate(LogEntry entry)
            {
                //lock object
                //read messageQueue queue
                //pop the last item on queue
                //add to list
                //lock(messageQueueLockObject)
                //{                                                        
                        try
                        {
                            this.InvokeIfRequired(l =>
                            {
                                if (l.Disposing || l.IsDisposed)
                                {
                                   // Trace.WriteLine("Disposing / Disposed... Clearing queue and stopping processes. Remaining messages: " + messageQueue.Count().ToString());
                                    Console.WriteLine("Disposing / Disposed... Clearing queue and stopping processes. Remaining messages: " + messageQueue.Count().ToString());
                                    runMessageQueueLoop = false;
                                    //empty the queue of messages
                                    for (int i = 0; i < messageQueue.Count(); i++)
                                    {
                                        var k = messageQueue.Take();
                                    }
                                    return;

                                }

                                //LogEntry entry = messageQueue.Dequeue();
                                if (entry == null)
                                {
                                    //Trace.WriteLine("LogEntry  is null / empty");
                                    Console.WriteLine("LogEntry  is null / empty");
                                    return;
                                }
                                  
                                //if (string.IsNullOrEmpty(entry.Message))
                                //    return;

                                if (string.IsNullOrEmpty(entry.Message))
                                {
                                   // Trace.WriteLine("LogEntry.Message  is null / empty");
                                    Console.WriteLine("LogEntry.Message  is null / empty");
                                }

                                if (!_isDebugMode && entry.IsDebugMessage)
                                    return;

                                ListViewItem item;
                                if (_includeDateTime)
                                {
                                    if(entry.OverrideDateTime != null)
                                    {
                                        item = new ListViewItem(((DateTime)entry.OverrideDateTime).ToString("dd/MM/yyyy H:mm:ss"));
                                    }
                                    else
                                    {
                                        item = new ListViewItem(DateTime.Now.ToString("dd/MM/yyyy H:mm:ss"));
                                    }
                                   
                                    item.SubItems.Add(entry.Message);
                                }
                                else
                                {
                                    item = new ListViewItem(entry.Message);
                                    //Dave Coates - Removed next line because it was adding duplicate lines when adding non-debug entry
                                    //Items.Add(item);
                                }

                                item.ForeColor = System.Drawing.Color.Black;
                                if (entry.IsErrorMessage)
                                {
                                    item.ForeColor = System.Drawing.Color.Red;
                                }

                                //instead of clearing the control, remove the oldest entry to make room for one more, so that we 
                                //consistently keep the same number of entries once we reach the max limit                   
                                if (l.Items.Count >= _maxEntries)
                                {
                                    l.Items.RemoveAt(l.Items.Count - 1);
                                }
                                
                                if (_isDebugMode)
                                {
                                    if (!entry.IsErrorMessage && entry.IsDebugMessage)
                                    {
                                        item.ForeColor = System.Drawing.Color.RoyalBlue;
                                    }
                                    if(!l.IsDisposed && l != null && item != null)
                                        l.Items.Insert(0, item);
                                }
                                else
                                {
                                    if (!entry.IsDebugMessage)
                                        if (!l.IsDisposed && l != null && item != null)
                                            l.Items.Insert(0, item);
                                }
                                //l.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
                            });
                        }
                   catch(NullReferenceException nux)
                    {
                //Trace.WriteLine("EXCEPTION: " + nux.Message + " STACKTRACE: " + nux.StackTrace);
                Console.WriteLine("EXCEPTION: " + nux.Message + " STACKTRACE: " + nux.StackTrace);
            }
                     catch(Exception  )
                        {
                        throw;  
                        }
                                                                      
                //}//
            }

            private void SetupContextMenu()
            {
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip(this.components);
                //contextMenuStrip.Items.Add(CreateToolStripMenuItem("Cut", (sender, e) => HandleCut(sender,e)));
                contextMenuStrip.Items.Add(CreateToolStripMenuItem("Copy", (sender, e) => HandleCopy(sender, e)));
                //contextMenuStrip.Items.Add(CreateToolStripMenuItem("Paste", (sender, e) => HandlePaste(sender, e)));

                this.ContextMenuStrip = contextMenuStrip;
            }

            private ToolStripItem CreateToolStripMenuItem(string name, EventHandler eventHandler)
            {
                ToolStripItem toolStripItem = new ToolStripMenuItem(name, null, eventHandler);
                toolStripItem.Text = name;
                toolStripItem.Name = name;
                toolStripItem.Available = true;
                return toolStripItem;
            }

            private void CopyToClipboard()
            {          
                //Copy to clipboard
                if (SelectedItems.Count > 0)
                {
                    string copyText = "";
                    if (_includeDateTime)
                    {
                        copyText = this.SelectedItems[0].SubItems[1].Text;
                    }
                    else
                    {
                        copyText = this.SelectedItems[0].Text;
                    }
                    Console.WriteLine("Copying text: " + copyText);
                    Clipboard.SetText(copyText);
                }
            }

            public void Dispose()
            {
                isClosing = true;
                messageQueue.CompleteAdding();
                if (cts != null)
                {
                    cts.Cancel();
                }

                runMessageQueueLoop = false;
                Thread.Sleep(250);
                if(threadMessageQueue != null)
                    threadMessageQueue.Abort();
                threadMessageQueue = null;

                
                base.Dispose();
            }

        #endregion

        #region Public Methods

            //public void AddCloneList(ListViewExt listview)
            //{
            //    cloneLists.Add(listview);
            //}

       

            public void AddEntry(string text = "", bool isErrorMessage = false, bool isDebugMessage = false, DateTime? overrideDateTime = null)
            {
                bool isLockTaken = false;
            //Monitor.TryEnter(lockObject, 1000, ref isLockTaken);
                //isLockTaken = true;

                try
                {                
                    //if (isLockTaken)
                    //{
                        try
                        {
                        ////do task here
                        //lock (lockObject)
                        //{
                        this.InvokeIfRequired(i =>
                        {
                            AddEntryProcess(text, isErrorMessage, IsDebugMode, overrideDateTime);
                        });

                        
                            ////this.InvokeIfRequired(l =>
                            ////{
                            ////try
                            ////{
                               
                            ////    }
                            ////    catch (Exception ex)
                            ////    {
                            ////        Trace.WriteLine("Internal list exception. No action taken. Details: " + ex.Message);
                            ////        Console.WriteLine("Internal list exception. No action taken. Details: " + ex.Message);
                            ////        //swallow it!
                            ////        //I don't want to hear about your troubles
                            ////    }
                            ////});
                        
                        //}
                            //if (IsCloneListEntriesEnabled)
                            //{
                            //    foreach (var item in cloneLists)
                            //    {
                            //        item.AddEntry(text, isErrorMessage, isDebugMessage);
                            //    }
                            //}
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                          
                    //}
                    //else
                    //{

                    //////try
                    //////{

                    //////    this.AddEntry(text ,  isErrorMessage , isDebugMessage );
                    //////}                   
                    //////catch (Exception ex)
                    //////{
                    //////    //SM.ClubManager.Library.Base

                    //////}
                    ////Trace.WriteLine("Lock failed. Message: " + text);
                    //Console.WriteLine("Lock failed. Message: " + text);
                        
                    //}
                }
                catch (Exception e)
                {

                    throw;
                }
                                          
            }

            public void AddBlankLine()
            {
                Items.Add("");
            }

            /// <summary>
            /// Returns a comma delimeted string of all the current entries in the list
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            public string ExportAll()
            {
                StringBuilder sb = new StringBuilder();
                int counter = 0;
                foreach (ListViewItem item in this.Items)
                {
                    string s = string.Format("{0},{1},{2}", counter++, item.Text.ToDateTime().ToString(), item.SubItems[1].Text.Replace("\r", "").Replace("\n", ""));
                    Console.WriteLine(s); //   this.SelectedItems[0].SubItems[1].Text);
                    sb.AppendLine(s);
                }
                return sb.ToString();
            }

            public void OverrideColumns(List<ListViewColumn> columns)
            {
                this.BeginUpdate();

                lock (lockObject)
                {
                    if (this.Items.Count > 0)
                    {
                        this.Items.Clear();
                    }
                    if (_listviewColumns != null)
                        _listviewColumns = null;

                    _listviewColumns = columns;
                }

                this.EndUpdate();
            }

            public ListViewItem FindItem(string searchText)
            {
                ListViewItem foundItem = null;
                foreach (ListViewItem item in this.Items)
                {
                    if (item.Text == searchText)
                    {
                        foundItem = item;
                        break;
                    }
                }
                return foundItem;
            }
          
            public void SetSelectedListItem(string item)
            {
                if (string.IsNullOrEmpty(item))
                    return;

                this.InvokeIfRequired(i =>
                {

                    foreach (ListViewItem it in this.Items)
                    {
                        if(it.Text != item)
                        {
                            it.ForeColor = defaultForegroundColour;
                            it.BackColor = defaultBackgroundColour;
                            it.Selected = false;
                            it.Focused = false;
                        }                        
                    }

                    //find the item that corresponds with the bale number in the bale text                
                    ListViewItem foundItem = this.FindItem(item);
                    this.Focus();
                    if (foundItem != null)
                    {
                        //select it
                        foundItem.ForeColor = SelectedItemForegroundColor;
                        foundItem.BackColor = SelectedItemBackgroundColor;
                        foundItem.EnsureVisible();
                        foundItem.Focused = true;
                    }                                        
                });
            }       
        #endregion

        #region Hidden Methods Raise Events
            internal void AddedItem(ListViewItem lvi)
            {
                if (this.ItemAdded != null)
                    this.ItemAdded(lvi);
            }

            internal void AddedItemRange(ListViewItem[] lvi)
            {
                if (this.ItemRangeAdded != null)
                    this.ItemRangeAdded(lvi);
            }

            internal void RemovedItem(ListViewItem lvi)
            {
                if (lvi == null)
                    return;
                if (this.ItemRemoved != null)
                    this.ItemRemoved(lvi);
            }

            internal void RemovedItem(int index, ListViewItem item)
            {
                if (item.Index < 0)
                    return;
                if (this.ItemRemovedAt != null)
                    this.ItemRemovedAt(index, item);
            }
        #endregion

        #region Inner Class

        public class ListViewItemCollectionNewEvent : System.Windows.Forms.ListView.ListViewItemCollection
        {
            #region Public and Private Members

            private ListView parent;

            #endregion

            #region Constructor

            public ListViewItemCollectionNewEvent(System.Windows.Forms.ListView owner)
                : base(owner)
            {
                parent = owner;
            }


            #endregion

            #region Overriden / new Hidden Methods

            public new void Add(ListViewItem Lvi)
            {
                base.Add(Lvi);
                ((ListViewExt)parent).AddedItem(Lvi);

            }


            public new void AddRange(ListViewItem[] Lvi)
            {
                base.AddRange(Lvi);
                ((ListViewExt)parent).AddedItemRange(Lvi);
            }


            public new void Remove(ListViewItem Lvi)
            {
                base.Remove(Lvi);
               
                ((ListViewExt)parent).RemovedItem(Lvi);
            }


            public new void RemoveAt(int index)
            {
                System.Windows.Forms.ListViewItem lvi = this[index];
                base.RemoveAt(index);
                ((ListViewExt)parent).RemovedItem(index, lvi);
            }


            #endregion

        }

        public class LogEntry
        {
            private string _message;

            public string Message
            {
                get { return _message; }
                set { _message = value; }
            }

            private bool _isErrorMessage;

            public bool IsErrorMessage
            {
                get { return _isErrorMessage; }
                set { _isErrorMessage = value; }
            }

            private bool _isDebugMessage;

            public bool IsDebugMessage
            {
                get { return _isDebugMessage; }
                set { _isDebugMessage = value; }
            }

            private DateTime? _overrideDateTime;

            public DateTime? OverrideDateTime
            {
                get { return _overrideDateTime; }
                set { _overrideDateTime = value; }
            }
        }

        #endregion
    }
}
