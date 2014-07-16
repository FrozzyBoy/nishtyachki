using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdminApp.Queue;
namespace AdminApp.Nishtiachki
{
     enum Nishtiachok_State
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

         

     }
}