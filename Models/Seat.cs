//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CinemaWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Seat
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int price { get; set; }
        public int id_room { get; set; }
        public Nullable<int> quality { get; set; }
    
        public virtual Room Room { get; set; }
    }
}
