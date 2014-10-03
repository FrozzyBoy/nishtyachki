using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Runtime.Serialization;
using UsersQueue.Model;
using UsersQueue.Queue.UserInformtion;
using UsersQueue.Services.TransferObjects;

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
            Nishtiachki = new List<Nishtiachok>();
            AllChanges += SaveToDb;
        }

        private static object _lockChangeState = new object();
        private static object _lockSetOwner = new object();

        private static void SaveToDb(Nishtiachok nisht, ChangeNishtArg info)
        {
            var addData = nisht.GetNishtiakTransferObject();

            using (var context = new AppDbContext())
            {
                addData.ChangeWas = info.TypeOfChange.ToString();
                context.Nishtiaki.Add(addData);
                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                }
                catch(Exception e)
                { 
                }
            }

        }

        public static List<Nishtiachok> Nishtiachki;
        public static event Action<Nishtiachok, ChangeNishtArg> AllChanges;

        public static List<NishtiakTransferObject> GetAllNishtiakTransferObjects
        {
            get
            {
                var result = new List<NishtiakTransferObject>();
                foreach (var item in Nishtiachki)
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
            SaveToDb(this, new ChangeNishtArg(TypeOfChanges.create));
        }

        private static bool _onchangeNicht = false;

        public NishtiakTransferObject GetNishtiakTransferObject()
        {
            NishtiakTransferObject result = new NishtiakTransferObject();

            result.ID = this.ID;

            var owner = new QueueUserTransferObject();

            if (this.Owner == null)
            {
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
                    this.Owner = owner;
                    OnChangeNisht(this, new ChangeNishtArg(TypeOfChanges.change));
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
                OnChangeNisht(this, new ChangeNishtArg(TypeOfChanges.change));
            }
        }

        public static Nishtiachok GetNishtiakByUserId(string id)
        {
            return Nishtiachki.Find((m) =>
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
            return Nishtiachki.Find(m => m.ID == id);
        }

        public static Nishtiachok GetFreeNishtiachok()
        {
            return Nishtiachki.Find(m => m.State == Nishtiachok_State.free);
        }

        public static void AddNistiachokByAdmin()
        {
            Nishtiachok obj = new Nishtiachok();
            Nishtiachki.Add(obj);
            ChangeNishtArg args = new ChangeNishtArg(TypeOfChanges.add);
            OnChangeNisht(obj, args);
        }

        public static void DeleteNishtiachok(string id)
        {
            var obj = GetNishtiachokByName(id);

            if (obj != null)
            {
                Nishtiachki.Remove(obj);
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
                ChangeNishtArg args = new ChangeNishtArg(TypeOfChanges.change);
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
            this.Owner = null;
            this.State = Nishtiachok_State.free;
            OnChangeNisht(this, new ChangeNishtArg(TypeOfChanges.change));
        }
    }
}
