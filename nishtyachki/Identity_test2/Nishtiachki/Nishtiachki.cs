using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminApp.Nishtiachki
{
   static public class NishtiachkiContainer
    {
       public static List<Nishtiachok> Nishtiachki = new List<Nishtiachok>();
        static public event EventHandler EventChangeStatNisht;

     
      static  public void OnEventChangeStatNisht(Nishtiachok obj,ChangeNishtArg arg)
         {
             if (EventChangeStatNisht != null)
             {
                 EventChangeStatNisht(obj, arg);
             }
         }
        
        
     static   public void ChangeNisht()
        {

        }

     static public void AddNistiachok(Nishtiachok obj)
        {
            Nishtiachki.Add(obj);
            ChangeNishtArg arg = new ChangeNishtArg(TypeOfChanges.add);
            OnEventChangeStatNisht(obj, arg);
        }
     static public void DeleteNishtiachok(Nishtiachok obj)
        {
            Nishtiachki.Remove(obj);
            ChangeNishtArg arg = new ChangeNishtArg(TypeOfChanges.delete);
            OnEventChangeStatNisht(obj, arg);
        }
     static public void ChangeStatNishtiachok(Nishtiachok obj, Nishtiachok_State state)
        {

           foreach(Nishtiachok n in Nishtiachki)
           {
               if (n.Equals(obj))
               {
                   obj.State = state;
                   break;
               }
           }
           ChangeNishtArg arg = new ChangeNishtArg(TypeOfChanges.change, state);
           OnEventChangeStatNisht(obj, arg);
            
        }
         
    }
}