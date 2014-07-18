﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdminApp.Queue;
namespace AdminApp.Nishtiachki
{
    public enum Nishtiachok_State
    {
        free,locked,in_using
    }
     public class Nishtiachok
     {
         static List<Nishtiachok> Nishtiachki = new List<Nishtiachok>();
         public event EventHandler EventChangeStateNisht;
         public Nishtiachok_State State { get; set; }
         public string Name { get; set; }
         public User owner { get; set; }


         private Nishtiachok(string name)
         {
             this.State = Nishtiachok_State.free;
             this.Name = name;
         }



        public void OnEventChangeStatNisht(Nishtiachok obj,ChangeNishtArg arg)
         {
             if (EventChangeStateNisht != null)
             {
                 EventChangeStateNisht(obj, arg);
             }
         }
        
        
       public void ChangeNisht()
        {

        }
         public static Nishtiachok GetNishtiakByUserId(string id)
       {
          return Nishtiachki.Find(m => m.owner.ID == id);

       }
       public static Nishtiachok GetNishtiachokBuNamme(string name)
       {
           return Nishtiachki.Find(m => m.Name == name);
       }
         public static Nishtiachok GetFreeNishtiachok()
         {
             return Nishtiachki.Find(m => m.State == Nishtiachok_State.free);
         }
         public static int GetNumOfFreeResources()
         {
             int n = 0;
             for (int i = 0; i < Nishtiachki.Count; i++)
             {
                 if(Nishtiachki[i].State==Nishtiachok_State.free)
                 {
                     n++;
                 }
             }
             return n;
         }

     public static void AddNistiachok(string name)
        {
            Nishtiachok obj = new Nishtiachok(name);
            Nishtiachki.Add(obj);
          
        }
     public static void DeleteNishtiachok(string name)
        {
            Nishtiachok obj = new Nishtiachok(name);
            Nishtiachki.Remove(obj);
           
        }
     public static void LockNishtiachok(string name)
     {

         foreach (Nishtiachok n in Nishtiachki)
         {
             if (n.Name == name)
             {
                 n.State = Nishtiachok_State.locked;                                
             }
         }


     }
         
    











        
         public override bool Equals(Object obj)
         {
             Nishtiachok nisht = obj as Nishtiachok;
             if ((this.Name == nisht.Name) && (this.owner.Equals(nisht.owner)) && (this.State == nisht.State))
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