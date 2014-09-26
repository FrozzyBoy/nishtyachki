using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UsersQueue.Queue.UserInformtion;

namespace UsersQueue.Queue.Nishtiachki
{
    public enum Nishtiachok_State
    {
        free, locked, wait_for_user, in_using
    }

    [DataContract]
    public class Nishtiachok
    {
        static Nishtiachok()
        {
            Nishtiachki = new List<Nishtiachok>();
            Nishtiachki.Add(new Nishtiachok("1"));
        }

        public static List<Nishtiachok> Nishtiachki;

        public static event EventHandler EventChangeNisht;
        [DataMember]
        public Nishtiachok_State State { get; set; }

        [DataMember]
        public string ID { get; private set; }

        [DataMember]
        public QueueUser owner { get; private set; }

        private Nishtiachok(string id)
        {
            this.State = Nishtiachok_State.free;
            this.ID = id;
        }

        private static bool _onchangeNicht = false;

        public static void OnChangeNisht(Nishtiachok obj, ChangeNishtArg arg)
        {
            if (!_onchangeNicht)
            {
                _onchangeNicht = true;
                try
                {
                    if (EventChangeNisht != null)
                    {
                        EventChangeNisht(obj, arg);
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
            if (this.owner != null && owner == null)
            {
                this.owner.Client.DroppedByServer("nishtiak changed");
            }
            else
            {
                this.owner = owner;
                OnChangeNisht(this, new ChangeNishtArg(TypeOfChanges.change));
            }
            this.owner = owner;
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
                if (m.owner != null)
                {
                    return m.owner.ID == id;
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
            this.owner = null;
            this.State = Nishtiachok_State.free;
            OnChangeNisht(this, new ChangeNishtArg(TypeOfChanges.change));
        }
    }
}
