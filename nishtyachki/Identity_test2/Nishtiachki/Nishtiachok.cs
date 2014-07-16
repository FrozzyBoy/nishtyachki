using System;
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
         public Nishtiachok_State State { get; set; }
         public string Name { get; set; }
         public User owner { get; set; }
        
         

         public Nishtiachok(Nishtiachok_State state, string name)
         {
             this.State = state;
             this.Name = name;
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