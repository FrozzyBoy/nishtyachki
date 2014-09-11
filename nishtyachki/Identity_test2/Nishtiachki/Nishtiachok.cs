using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdminApp.Queue;
namespace AdminApp.Nishtiachki
{
    public enum Nishtiachok_State
    {
        free, locked, in_using, wait_for_user
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
            set
            {
                _state = value;
                OnChangeNisht(this, new ChangeNishtArg(TypeOfChanges.change));
            }
        }
        public string ID { get; set; }
        public User owner { get; set; }

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

        public void ChangeNisht(Nishtiachok_State state)
        {
            this.State = state;
        }

        public static Nishtiachok GetNishtiakByUserId(string id)
        {
            return Nishtiachki.Find(m => m.owner.ID == id);
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
        }
    }
}