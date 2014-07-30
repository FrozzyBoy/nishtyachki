using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdminApp.Queue;
namespace AdminApp.Nishtiachki
{
    public enum Nishtiachok_State
    {
        free, locked, in_using
    }

    public class Nishtiachok
    {
        static Nishtiachok()
        {
            Nishtiachki = new List<Nishtiachok>();
            Nishtiachki.Add(new Nishtiachok("default1"));
        }

        public static List<Nishtiachok> Nishtiachki;

        public event EventHandler EventChangeNishtState;
        public Nishtiachok_State State { get; set; }
        public string ID { get; set; }
        public User owner { get; set; }
        public string Im { get; set; }

        private Nishtiachok(string id)
        {
            this.State = Nishtiachok_State.free;
            this.ID = id;
        }

        public void OnEventChangeStatNisht(Nishtiachok obj, ChangeNishtArg arg)
        {
            if (EventChangeNishtState != null)
            {
                EventChangeNishtState(obj, arg);
            }
        }

        public void ChangeNisht(Nishtiachok_State state)
        {
            this.State = state;

            ChangeNishtArg arg = new ChangeNishtArg(TypeOfChanges.change, state);
            OnEventChangeStatNisht(this, arg);
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
            obj.OnEventChangeStatNisht(obj, args);
            UsersQueue.AlertQueue();
        }

        public static void DeleteNishtiachok(string id)
        {
            Nishtiachok obj = new Nishtiachok(id);
            Nishtiachki.Remove(obj);
            ChangeNishtArg args = new ChangeNishtArg(TypeOfChanges.delete);
            obj.OnEventChangeStatNisht(obj, args);

        }

        public static void LockNishtiachok(string id)
        {

            foreach (Nishtiachok n in Nishtiachki)
            {
                if (n.ID == id)
                {
                    n.State = Nishtiachok_State.locked;
                    ChangeNishtArg args = new ChangeNishtArg(TypeOfChanges.change);
                    n.OnEventChangeStatNisht(n, args);
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

    }
}