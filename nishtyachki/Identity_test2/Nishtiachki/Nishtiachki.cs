using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminApp.Nishtiachki
{
   static public class NishtiachkiContainer
    {
        static List<Nishtiachok> Nishtiachki = new List<Nishtiachok>();
        static public event EventHandler EventChangeStatNisht;

        public NishtiachkiContainer()
        {
            
        }
        public void OnEventChangeStatNisht(Nishtiachok obj,ChangeNishtArg arg)
         {
             if (EventChangeStatNisht != null)
             {
                 EventChangeStatNisht(obj, arg);
             }
         }
        
        
        public void ChangeNisht()
        {

        }

        public void AddNistiachok(Nishtiachok obj)
        {
            Nishtiachki.Add(obj);
            ChangeNishtArg arg = new ChangeNishtArg(TypeOfChanges.add);
            OnEventChangeStatNisht(obj, arg);
        }
        public void DeleteNishtiachok(Nishtiachok obj)
        {
            Nishtiachki.Remove(obj);
            ChangeNishtArg arg = new ChangeNishtArg(TypeOfChanges.delete);
            OnEventChangeStatNisht(obj, arg);
        }
        public void ChangeStatNishtiachok(Nishtiachok obj, Nishtiachok_State state)
        {

           foreach(Nishtiachok n in Nishtiachki)
           {
               if (n.Equals(obj))
               {
                   obj.State = state;
               }
           }
           ChangeNishtArg arg = new ChangeNishtArg(TypeOfChanges.changeStat, state);
           OnEventChangeStatNisht(obj, arg);
            
        }
         
    }
}