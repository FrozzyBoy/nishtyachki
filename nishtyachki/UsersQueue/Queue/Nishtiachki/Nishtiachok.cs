using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Runtime.Serialization;
using UsersQueue.Model;
using UsersQueue.Queue.UserInformtion;
using UsersQueue.Services.TransferObjects;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace UsersQueue.Queue.Nishtiachki
{
    public enum Nishtiachok_State : int
    {
        free, locked, wait_for_user, in_using
    }

    public class Nishtiachok
    {
        static Nishtiachok()
        {
            //Nishtiachki = new List<Nishtiachok>();
            AllChanges += SaveToDbNishtiakChange;
        }

        private static object _lockChangeState = new object();
        private static object _lockSetOwner = new object();

        private static void SaveToDbNishtiakChange(Nishtiachok nisht, ChangeNishtArg info)
        {
            using (var context = new AppDbContext())
            {
                var update = context.Nishtiaki.Single<NishtiakTransferObject>(x=> x.ID == nisht.ID);

                NishtiakLogs log = new NishtiakLogs() { ChangeHappend = DateTime.Now, ChangeWas = info.TypeOfChange.ToString(), NishtiakId = update.ID, State = update.State };

                context.NishtiakLogs.Add(log);

                //var updateValue = context.Nishtiaki.Single<NishtiakTransferObject>();

                if (info.TypeOfChange == TypeOfChanges.delete)
                {
                    update.IsActive = false;
                }

                //var entity = context.Entry(updateValue);
                //var collection = typeof(NishtiakTransferObject).GetProperties();
                //foreach (var item in collection)
                //{
                //    bool ifKey = item.GetCustomAttribute<KeyAttribute>() != null;
                //    bool ifOwner = item.PropertyType == update.owner.GetType();
                //    bool ifAllChanges = item.Name == "AllChanges";

                //    if (!ifKey && !ifOwner && !ifAllChanges)
                //    {
                //        entity.Property(item.Name).IsModified = true;
                //    }
                //}

                //update.AllChanges.Add(log);

                //var entity = context.Entry(update);
                //var collection = entity.Collection(x => x.AllChanges);
                //collection.CurrentValue = update.AllChanges;
                //property.IsModified = true;
                context.SaveChanges();
            }

        }

        private static void SaveToDbNishtiak(Nishtiachok nisht)
        {
            using (var context = new AppDbContext())
            {
                var old = context.Nishtiaki.SingleOrDefault<NishtiakTransferObject>(x => x.ID == nisht.ID);

                if (old == null)
                {
                    context.Nishtiaki.Add(nisht.GetNishtiakTransferObject());
                }
                //else
                //{
                //    var entity = context.Entry(old);
                //    foreach (var item in typeof(NishtiakTransferObject).GetProperties())
                //    {
                //        if (item.GetCustomAttribute<KeyAttribute>() != null)
                //        {
                //            entity.Property(item.Name).IsModified = true;
                //        }
                //    }
                //}
                context.SaveChanges();
            }
        }

        public static List<Nishtiachok> NishtiachkiActive
        {
            get
            {
                List<Nishtiachok> nishtiaki = null;
                using (var content = new AppDbContext())
                {
                    nishtiaki = (from NishtiakTransferObject nisht in content.Nishtiaki
                                 where nisht.IsActive == true
                                 select new Nishtiachok() { ID=nisht.ID, State = (Nishtiachok_State)nisht.State }).ToList<Nishtiachok>();
                }

                return nishtiaki;
            }
            set
            {
                //                
            }
        }

        public static event Action<Nishtiachok, ChangeNishtArg> AllChanges;

        public static List<NishtiakTransferObject> GetAllNishtiakTransferObjects
        {
            get
            {
                var result = new List<NishtiakTransferObject>();
                foreach (var item in NishtiachkiActive)
                {
                    result.Add(item.GetNishtiakTransferObject());
                }
                return result;
            }
        }

        public static event Action EventChangeNisht;
        public Nishtiachok_State State { get; set; }

        public string ID { get; private set; }
        public QueueUser Owner { get; private set; }

        private Nishtiachok()
        {
            this.State = Nishtiachok_State.free;
            this.ID = Guid.NewGuid().ToString();
            
        }

        private static bool _onchangeNicht = false;

        public NishtiakTransferObject GetNishtiakTransferObject()
        {
            NishtiakTransferObject result = new NishtiakTransferObject();

            result.ID = this.ID;

            QueueUserTransferObject owner = null;

            if (this.Owner == null)
            {
                owner = new QueueUserTransferObject();
                owner.ID = "";
                owner.Key = -1;
                owner.Role = 0;
                owner.State = 0;
                owner.PremiumEndDate = QueueUser.DefaultPremiumEndDate;
            }
            else
            {
                owner = this.Owner.GetQueueUserTransferObject();
            }

            result.owner = owner;

            result.State = (int)this.State;
            result.AllChanges = new List<NishtiakLogs>();

            using (var context = new AppDbContext())
            {
                var old = context.Nishtiaki.SingleOrDefault<NishtiakTransferObject>(x => x.ID == this.ID);
                if (old != null)
                {
                    result.AllChanges = old.AllChanges;
                }
            }

            return result;
        }

        public static void OnChangeNisht(Nishtiachok obj, ChangeNishtArg arg)
        {
            if (AllChanges != null)
            {
                AllChanges(obj, arg);
            }

            if (!_onchangeNicht)
            {
                _onchangeNicht = true;
                try
                {
                    if (EventChangeNisht != null)
                    {
                        EventChangeNisht();
                    }
                }
                finally
                {
                    _onchangeNicht = false;
                }
            }
        }

        public void SetOwner(QueueUser owner)
        {
            lock (_lockSetOwner)
            {
                if (this.Owner != null && owner == null)
                {
                    this.Owner.Client.DroppedByServer("nishtiak changed and you dropped");
                    MakeFree();
                }
                else
                {
                    if (this.Owner == null)
                    {
                        this.Owner = owner;
                        OnChangeNisht(this, new ChangeNishtArg(TypeOfChanges.get_owner));
                    }
                    else
                    {
                        bool oldIdAreSame = this.Owner.ID == owner.ID;
                        this.Owner = owner;

                        if (oldIdAreSame)
                        {
                            OnChangeNisht(this, new ChangeNishtArg(TypeOfChanges.get_owner));
                        }
                    }
                }
            }
        }

        public void ChangeNishtState(Nishtiachok_State state)
        {
            lock (_lockChangeState)
            {
                this.State = state;

                if ((int)state < (int)Nishtiachok_State.wait_for_user)
                {
                    this.SetOwner(null);
                }
                OnChangeNisht(this, new ChangeNishtArg(TypeOfChanges.change_state));
            }
        }

        public static Nishtiachok GetNishtiakByUserId(string id)
        {
            return NishtiachkiActive.Find((m) =>
            {
                if (m.Owner != null)
                {
                    return m.Owner.ID == id;
                }
                return false;
            });
        }

        public static Nishtiachok GetNishtiachokByName(string id)
        {
            return NishtiachkiActive.Find(m => m.ID == id);
        }

        public static Nishtiachok GetFreeNishtiachok()
        {
            return NishtiachkiActive.Find(m => m.State == Nishtiachok_State.free);
        }

        public static void AddNistiachokByAdmin()
        {
            Nishtiachok obj = new Nishtiachok();

            SaveToDbNishtiak(obj);
            SaveToDbNishtiakChange(obj, new ChangeNishtArg(TypeOfChanges.create));

            //NishtiachkiActive.Add(obj);
            ChangeNishtArg args = new ChangeNishtArg(TypeOfChanges.add);
            OnChangeNisht(obj, args);
        }

        public static void DeleteNishtiachok(string id)
        {
            var obj = GetNishtiachokByName(id);

            if (obj != null)
            {
                obj.MakeFree();
                ChangeNishtArg args = new ChangeNishtArg(TypeOfChanges.delete);
                OnChangeNisht(obj, args);
            }
            else
            {
                throw new ArgumentNullException("нет ништяка, который надо удалить");
            }
        }

        public static void LockNishtiachok(string id)
        {
            var n = GetNishtiachokByName(id);

            if (n != null && n.ID == id)
            {
                n.State = Nishtiachok_State.locked;
                ChangeNishtArg args = new ChangeNishtArg(TypeOfChanges.blocked);
                OnChangeNisht(n, args);
            }
        }

        public override bool Equals(Object obj)
        {
            Nishtiachok nisht = obj as Nishtiachok;

            bool result = false;

            if (nisht != null)
            {
                result = this.ID == nisht.ID;
            }

            return result;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        internal void MakeFree()
        {
            if (this.State == Nishtiachok_State.in_using || this.State == Nishtiachok_State.wait_for_user)
            {
                Owner.State = UserCurrentState.Online;
                this.Owner = null;
                this.State = Nishtiachok_State.free;
                OnChangeNisht(this, new ChangeNishtArg(TypeOfChanges.opened));
            }
            else
            {
                if (this.State == Nishtiachok_State.locked)
                {
                    this.State = Nishtiachok_State.free;
                }
            }
        }
    }
}
