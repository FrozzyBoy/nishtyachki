using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdminApp.Queue;
namespace AdminApp.Nishtiachki
{
    public enum Nishtiachok_State
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

        public static event EventHandler EventChangeNisht;
        private Nishtiachok_State _state;
        public Nishtiachok_State State 
        { 
            get
            {
                return _state;
            } 
            private set
            {                
                _state = value;                
            }
        }
        public string ID { get; private set; }

        public User owner { get; private set; }

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

        public void SetOwner(User owner)
        {
            if (this.owner != null && owner == null)
            {
                //this.owner.Client.DroppedByServer("nishtiak changed");
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

            if (nisht == null)
            {
                return false;
            }

            if ((this.ID == nisht.ID))
            {
                return true;
            }
            else
            {
                return false;
            }
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