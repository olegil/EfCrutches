using System;
using System.Collections.Concurrent;
using System.Data.Entity.Validation;
using System.Data.Linq;
using System.Diagnostics;
using System.Linq;
using System.Transactions;
using EntityFramework.Extensions;
using System.Data.Entity;
using Linq2Sql;

namespace EfCrutches
{
    internal class Program
    {
        private static void Main()
        {
            EfContextFactory.InitializeDatabase(new EfCrutchesDropCreateDatabaseIfModelChanges());
            NaviationPropertyCount(5);
            BatchUpdate(100, 5);
            BatchDelete(100, 5);
            UtcTime();
            RequiredAttributeValidation();
            ReadPerformance(100, 5);
            CountOfEntitiesWithSomePending();
            Insertion(100, 5);
            InternalCaching();
        }

        #region tests

        private static void NaviationPropertyCount(int times)
        {
            Trace.WriteLine("--------------Navigation property.Count()--------------");
            Console.WriteLine("--------------Navigation property.Count()--------------");
            var navigationPropertyTime = Timer.Time(() =>
                {
                    using (var context = EfContextFactory.CreateContext())
                    {
                        var folder = context.Folders.First();
                        Trace.WriteLine(string.Format("Found {0} threads in folder", folder.Threads.Count()));
                    }
                }, times);
            var directQueryTime = Timer.Time(() =>
                {
                    using (var context = EfContextFactory.CreateContext())
                    {
                        var folder = context.Folders.First();
                        Trace.WriteLine(string.Format("Found {0} threads in folder", context.Threads.Count(f => f.Folders.Any(e => e.Id == folder.Id))));
                    }
                }, times);
            var linq2SqlNavigationProperty = Timer.Time(() =>
                {
                    using (var context = Linq2SqlContextFactory.CreateContext())
                    {
                        var folder = context.MessageFolders.First();

                        Trace.WriteLine(string.Format("Found {0} threads in folder", folder.ThreadsInFolders.Count()));
                    }
                }, times);
            var linq2SqlDirectQuery = Timer.Time(() =>
                {
                    using (var context = Linq2SqlContextFactory.CreateContext())
                    {
                        var folder = context.MessageFolders.First();

                        Trace.WriteLine(string.Format("Found {0} threads in folder",
                                                      context.Threads.Count(f => f.ThreadsInFolders.Any(e => e.MessageFolder_Id == folder.Id))));
                    }
                }, times);
            Console.WriteLine(
                "Ef Navigation property : {0}, \nEf direct query: {1}, \nLinq2sql navigation property: {2}, \nLinq2sql direct query: {3}",
                navigationPropertyTime, directQueryTime, linq2SqlNavigationProperty, linq2SqlDirectQuery);
        }

        private static void BatchUpdate(int count, int times)
        {
            Trace.WriteLine("--------------Batch update test--------------");
            Console.WriteLine("--------------Batch update test--------------");
            var withoutBatchAndChangesDetectionAndValidation = Timer.Time(() =>
                {
                    using (var context = EfContextFactory.CreateContext().WithoutChangesDetection().WithoutValidation())
                    {
                        var messages = context.Messages.OrderBy(f=>f.Id).Take(count);
                        foreach (var m in messages)
                        {
                            m.IsRead = true;
                        }
                        context.ChangeTracker.DetectChanges();
                        context.SaveChanges();
                    }
                }, times);
            var withoutBatchAndChangesDetection = Timer.Time(() =>
                {
                    using (var context = EfContextFactory.CreateContext().WithoutChangesDetection())
                    {
                        var messages = context.Messages.OrderBy(f => f.Id).Take(count).Include(f => f.Thread)
                                              .Include(f => f.Sender).Include(f => f.Receiver).Include(f => f.Owner);
                        foreach (var m in messages)
                        {
                            m.IsRead = false;
                        }
                        context.ChangeTracker.DetectChanges();
                        context.SaveChanges();
                    }
                }, times);
            var withoutBatchAndValidation = Timer.Time(() =>
                {
                    using (var context = EfContextFactory.CreateContext().WithoutValidation())
                    {
                        var messages = context.Messages.OrderBy(f => f.Id).Take(count);
                        foreach (var m in messages)
                        {
                            m.IsRead = true;
                        }
                        context.SaveChanges();
                    }
                }, times);
            var withoutBatch = Timer.Time(() =>
                {
                    using (var context = EfContextFactory.CreateContext())
                    {
                        var messages = context.Messages.OrderBy(f => f.Id).Take(count).Include(f => f.Thread)
                                              .Include(f => f.Sender).Include(f => f.Receiver).Include(f => f.Owner);
                        foreach (var m in messages)
                        {
                            m.IsRead = false;
                        }
                        context.SaveChanges();
                    }
                }, times);
            var batched = Timer.Time(() =>
                {
                    using (var context = EfContextFactory.CreateContext())
                    {
                        var messages = context.Messages.Where(f => f.Id < count + 1);
                        messages.Update(f => new Message {IsRead = false});
                    }
                }, times);
            Console.WriteLine("Without batch, validation and changes detection: {0}, \nWithout batch and changes detection: {1}\n" +
                              "Without batch and validation: {2}, \nWithout batch: {3}, \nBatched: {4}", withoutBatchAndChangesDetectionAndValidation,
                              withoutBatchAndChangesDetection,
                              withoutBatchAndValidation,
                              withoutBatch,
                              batched);
        }

        private static void BatchDelete(int count, int times)
        {
            Trace.WriteLine("--------------Batch delete test--------------");
            Console.WriteLine("--------------Batch delete test--------------");
            TimeSpan withoutBatch, batched, rawSql, linq2Sql;
            using (new TransactionScope())
            {
                withoutBatch = Timer.Time(() =>
                    {
                        using (var context = EfContextFactory.CreateContext().WithoutValidation().WithoutChangesDetection())
                        {
                            var messages = context.Messages.OrderBy(f=>f.Id).Take(count);
                            foreach (var message in messages)
                            {
                                context.Messages.Remove(message);
                            }
                            context.SaveChanges();
                        }
                    }, times);
            }
            using (new TransactionScope())
            {
                rawSql = Timer.Time(() =>
                    {
                        using (var context = EfContextFactory.CreateContext().WithoutValidation().WithoutChangesDetection())
                        {
                            var ids = context.Messages.OrderBy(f => f.Id).Select(f => f.Id).Take(count);
                            var stringIds = string.Join(",", ids);
                            context.Database.ExecuteSqlCommand(string.Format("delete from Message where Id in ({0})", stringIds));
                        }
                    }, times);
            }
            using (new TransactionScope())
            {
                batched = Timer.Time(() =>
                    {
                        using (var context = EfContextFactory.CreateContext())
                        {
                            var messages = context.Messages.OrderBy(f => f.Id).Take(count);
                            messages.Delete();
                        }
                    }, times);
            }
            using (new TransactionScope())
            {
                linq2Sql = Timer.Time(() =>
                    {
                        using (var context = Linq2SqlContextFactory.CreateContext())
                        {
                            var messages = context.Messages.OrderBy(f => f.Id).Take(count);
                            context.Messages.DeleteAllOnSubmit(messages);
                            context.SubmitChanges();
                        }
                    }, times);
            }
            Console.WriteLine("Delete without batches: {0}, \nraw sql: {1}, \nbatched: {2}, \nLinq2Sql: {3}", withoutBatch, rawSql, batched, linq2Sql);
        }

        private static void UtcTime()
        {
            Console.WriteLine("--------------DateTime test--------------");
            using (new TransactionScope())
            {
                var date = DateTime.UtcNow;
                date = new DateTime(date.Ticks - (date.Ticks%TimeSpan.TicksPerSecond));
                int id;
                using (var context = EfContextFactory.CreateContext())
                {
                    var owner = context.Accounts.First();
                    var thread = context.Threads.First();
                    var message = new Message {Date = date, Owner = owner, Sender = owner, Receiver = owner, Thread = thread};
                    context.Messages.Add(message);
                    context.SaveChanges();
                    id = message.Id;
                }
                using (var context = EfContextFactory.CreateContext())
                {
                    var fromDatabase = context.Messages.Find(id);
                    CompareDates(date, fromDatabase.Date, "Ef");
                }
            }

            using (new TransactionScope())
            {
                var date = DateTime.UtcNow;
                date = new DateTime(date.Ticks - (date.Ticks%TimeSpan.TicksPerSecond));
                int id;
                using (var context = Linq2SqlContextFactory.CreateContext())
                {
                    var owner = context.Accounts.First();
                    var thread = context.Threads.First();
                    var message = new Linq2Sql.Message {Date = date, Owner = owner, Sender = owner, Reciever = owner, Thread = thread};
                    context.Messages.InsertOnSubmit(message);
                    context.SubmitChanges();
                    id = message.Id;
                }
                using (var context = Linq2SqlContextFactory.CreateContext())
                {
                    var fromDatabase = context.Messages.First(f => f.Id == id);
                    CompareDates(date, fromDatabase.Date, "Linq2Sql");
                }
            }
        }

        private static void RequiredAttributeValidation()
        {
            Console.WriteLine("--------------Required attibute validation--------------");
            using (new TransactionScope())
            {
                using (var context = EfContextFactory.CreateContext())
                {
                    var message = context.Messages.Find(1);
                    message.IsRead = true;
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (DbEntityValidationException)
                    {
                        Console.WriteLine("Validation of entity failed because some properies haven't been loaded. Let's do it now.");
                    }
                    var owner = message.Owner;
                    var reciever = message.Receiver;
                    var sender = message.Sender;
                    var thread = message.Thread;

                    context.SaveChanges();
                    Console.WriteLine("Successfully saved changes");
                }
            }
        }

        private static void ReadPerformance(int count, int times)
        {
            Console.WriteLine("--------------Testing reading performance--------------");
            var ef = Timer.Time(() =>
                {
                    using (var context = EfContextFactory.CreateContext())
                    {
                        context.Messages.AsNoTracking().OrderBy(f => f.Id).Take(count)
                               .Include(f => f.Owner).Include(f => f.Sender).Include(f => f.Receiver)
                               .ToList();
                    }
                }, times);
            var linq2Sql = Timer.Time(() =>
                {
                    using (var context = Linq2SqlContextFactory.CreateContext())
                    {
                        DataLoadOptions options = new DataLoadOptions();
                        options.LoadWith<Linq2Sql.Message>(c => c.Owner);
                        options.LoadWith<Linq2Sql.Message>(c => c.Sender);
                        options.LoadWith<Linq2Sql.Message>(c => c.Reciever);
                        context.LoadOptions = options;

                        context.Messages.OrderBy(f => f.Id).Take(count).ToList();
                    }
                }, times);
            Console.WriteLine("Ef: {0}, Linq2sql: {1}", ef, linq2Sql);
        }

        private static void CountOfEntitiesWithSomePending()
        {
            Console.WriteLine("--------------Counting unsaved entities--------------");
            using (new TransactionScope())
            {
                using (var context = EfContextFactory.CreateContext())
                {
                    var countBefore = context.Accounts.Count();
                    context.Accounts.Add(new Account {Name = "third"});
                    var countAfter = context.Accounts.Count();
                    context.SaveChanges();
                    var countAfterSaveChanges = context.Accounts.Count();
                    Console.WriteLine("Ef: count before: {0}, count after: {1}, count after SaveChanges: {2}", countBefore, countAfter,
                                      countAfterSaveChanges);
                }
            }
            using (new TransactionScope())
            {
                using (var context = Linq2SqlContextFactory.CreateContext())
                {
                    var countBefore = context.Accounts.Count();
                    context.Accounts.InsertOnSubmit(new Linq2Sql.Account {Name = "third"});
                    var countAfter = context.Accounts.Count();
                    context.SubmitChanges();
                    var countAfterSubmitChanges = context.Accounts.Count();
                    Console.WriteLine("Linq2sql: count before: {0}, count after: {1}, count after SubmitChanges: {2}", countBefore, countAfter,
                                      countAfterSubmitChanges);
                }
            }
        }

        private static void Insertion(int count, int times)
        {
            Console.WriteLine("--------------Insertion test--------------");
            var efTracked = Timer.Time(() =>
                {
                    using (new TransactionScope())
                    {
                        var parts = Partitioner.Create(0, count, 100).GetPartitions(1).First();
                        while (parts.MoveNext())
                        {
                            using (var context = EfContextFactory.CreateContext())
                            {
                                for (int i = parts.Current.Item1; i < parts.Current.Item2; i++)
                                {
                                    context.Accounts.Add(new Account {Name = "1"});
                                }
                                context.SaveChanges();
                            }
                        }
                    }
                }, times);
            var efNotTracked = Timer.Time(() =>
                {
                    using (new TransactionScope())
                    {
                        var parts = Partitioner.Create(0, count, 100).GetPartitions(1).First();
                        while (parts.MoveNext())
                        {
                            using (var context = EfContextFactory.CreateContext().WithoutChangesDetection().WithoutValidation())
                            {
                                for (int i = parts.Current.Item1; i < parts.Current.Item2; i++)
                                {
                                    context.Accounts.Add(new Account { Name = "1" });
                                }
                                context.SaveChanges();
                            }
                        }
                    }
                }, times);
            var linq2Sql = Timer.Time(() =>
                {
                    using (new TransactionScope())
                    {
                        var parts = Partitioner.Create(0, count, 100).GetPartitions(1).First();
                        while (parts.MoveNext())
                        {
                            using (var context = Linq2SqlContextFactory.CreateContext())
                            {
                                for (int i = parts.Current.Item1; i < parts.Current.Item2; i++)
                                {
                                    context.Accounts.InsertOnSubmit(new Linq2Sql.Account { Name = "1" });
                                }
                                context.SubmitChanges();
                            }
                        }
                    }
                }, times);
            Console.WriteLine("Ef tracked: {0}, \nEf not tracked: {1}, \nLinq2sql: {2}", efTracked, efNotTracked, linq2Sql);
        }

        private static void InternalCaching()
        {
            Console.WriteLine("--------------Internal caching test--------------");
            using (var context = EfContextFactory.CreateContext())
            {
                context.Accounts.Load(); //аккаунты были загружены в кеш где-нибудь в другом месте
                
                var q = context.Messages.Include(f=>f.Owner).First();
                //this won't generate aditional query since owner's already loaded
                Console.WriteLine(q.Owner.Name); 
            }
        }

        #endregion

        #region implementation details

        private static void CompareDates(DateTime first, DateTime second, string prefix)
        {
            Console.WriteLine("{0}: Original date and recieved date are equal: {1}", prefix, first == second);
            Console.WriteLine("{0}: Original date and recieved date after ToUniversalTime() are equal: {1}", prefix, first == second.ToUniversalTime());
            Console.WriteLine("{0}: Original date and recieved date after ToLocalTime() are equal: {1}", prefix, first == second.ToLocalTime());
        }

        #endregion

    }
}
