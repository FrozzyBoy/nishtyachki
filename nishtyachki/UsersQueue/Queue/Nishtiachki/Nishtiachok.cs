using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
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
            Nishtiachki.Add(new Nishtiachok("1"));
        }

        public static List<Nishtiachok> Nishtiachki;

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

        private Nishtiachok(string id)
        {
            this.State = Nishtiachok_State.free;
            this.ID = id;
        }

        private static bool _onchangeNicht = false;

        public NishtiakTransferObject GetNishtiakTransferObject()
        {
            NishtiakTransferObject result = new NishtiakTransferObject();

            result.ID = this.ID;
            result.owner = null;

            if (this.Owner != null)
            {
                result.owner = this.Owner.GetQueueUserTransferObject();
            }
            result.State = (int)this.State;

            return result;
        }

        public static void OnChangeNisht(Nishtiachok obj, ChangeNishtArg arg)
        {
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
            if (this.Owner != null && owner == null)
            {
                this.Owner.Client.DroppedByServer("nishtiak changed");
            }
            else
            {
                this.Owner = owner;
                OnChangeNisht(this, new ChangeNishtArg(TypeOfChanges.change));
            }
            this.Owner = owner;
        }

        public void ChangeNishtState(Nishtiachok_State state)
        {
            if ((int)state < (int)Nishtiachok_State.wait_for_user)
            {
                this.SetOwner(null);
            }
            this.State = state;
            OnChangeNisht(this, new ChangeNishtArg(TypeOfChanges.change));
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

        public static Nishtiachok GetNishtiachokByNamme(string id)
        {
            return Nishtiachki.Find(m => m.ID == id);
        }

        public static Nishtiachok GetFreeNishtiachok()
        {
            return Nishtiachki.Find(m => m.State == Nishtiachok_State.free);
        }

        public static void AddNistiachokByAdmin(string id)
        {
            Nishtiachok obj = new Nishtiachok(id);
            Nishtiachki.Add(obj);
            ChangeNishtArg args = new ChangeNishtArg(TypeOfChanges.add);
            OnChangeNisht(obj, args);
        }

        public static void DeleteNishtiachok(string id)
        {
            Nishtiachok obj = new Nishtiachok(id);
            Nishtiachki.Remove(obj);
            ChangeNishtArg args = new ChangeNishtArg(TypeOfChanges.delete);
            OnChangeNisht(obj, args);
        }

        public static void LockNishtiachok(string id)
        {

            foreach (Nishtiachok n in Nishtiachki)
            {
                if (n.ID == id)
                {
                    n.State = Nishtiachok_State.locked;
                    ChangeNishtArg args = new ChangeNishtArg(TypeOfChanges.change);
                    OnChangeNisht(n, args);
                    break;
                }
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
