using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EfCrutches
{
    internal static class DatabaseInitializerBase
    {
        private const int FoldersCount = 10;
        private const int ThreadsInEachFolder = 10;
        private const int MessagesInEachThread = 100;
        private const int MessagesCount = MessagesInEachThread * ThreadsInEachFolder * FoldersCount;

        internal static void Seed(CrutchContext context)
        {
            Trace.WriteLine("Performing database initialization");
            var mainAccount = new Account {Name = "Entity"};
            var otherAccount = new Account {Name = "Framework"};

            var folders = new List<MessageFolder>(FoldersCount);
            var allThreads = new List<MessageThread>(ThreadsInEachFolder*FoldersCount);
            for (int i = 0; i < FoldersCount; i++)
            {
                var folder = new MessageFolder {Owner = mainAccount, Name = "Folder" + i, Threads = new List<MessageThread>(ThreadsInEachFolder)};
                for (int j = 0; j < ThreadsInEachFolder; j++)
                {
                    var thread = new MessageThread {Subject = string.Format("Thread {0} - {1}", i, j)};
                    folder.Threads.Add(thread);
                    allThreads.Add(thread);
                }
                folders.Add(folder);
            }
            var messages = new List<Message>(MessagesCount);
            for (int i = 0; i < MessagesCount; i++)
            {
                messages.Add(new Message
                    {
                        Owner = mainAccount,
                        Sender = otherAccount,
                        Receiver = mainAccount,
                        Date = DateTime.UtcNow,
                        IsRead = false,
                        Thread = allThreads[i%allThreads.Count],
                        Text = "Message" + i
                    });
            }
            context.WithoutValidation().WithoutChangesDetection();

            context.Accounts.Add(mainAccount);
            context.Accounts.Add(otherAccount);
            Trace.WriteLine("Adding folders");
            for (int i = 0; i < FoldersCount; i++)
            {
                context.Folders.Add(folders[i]);
                context.SaveChanges();
            }

            Parallel.ForEach(Partitioner.Create(0, MessagesCount, 100), range =>
                {
                    using (var tempContext = EfContextFactory.CreateContext().WithoutChangesDetection().WithoutValidation())
                    {
                        tempContext.Entry(mainAccount).State = System.Data.EntityState.Unchanged;
                        tempContext.Entry(otherAccount).State = System.Data.EntityState.Unchanged;

                        for (int i = range.Item1; i < range.Item2; i++)
                        {
                            tempContext.Entry(messages[i].Thread).State = System.Data.EntityState.Unchanged;
                            tempContext.Messages.Add(messages[i]);
                        }
                        tempContext.SaveChanges();
                    }
                    Trace.WriteLine(string.Format("Adding messages {0} - {1}", range.Item1, range.Item2));
                });

            context.SaveChanges();
            GC.Collect(2, GCCollectionMode.Forced, true);
        }
    }

    internal class EfCrutchesDropCreateDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<CrutchContext>
    {
        protected override void Seed(CrutchContext context)
        {
            DatabaseInitializerBase.Seed(context);
            base.Seed(context);
        }
    }

    internal class EfCrutechesDropCreateDatabaseAlways : DropCreateDatabaseAlways<CrutchContext>
    {
        protected override void Seed(CrutchContext context)
        {
            DatabaseInitializerBase.Seed(context);
            base.Seed(context);
        }
    }
}