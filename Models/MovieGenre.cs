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
    
    public partial class MovieGenre
    {

        public int id { get; set; }
        public int id_movie { get; set; }
        public int id_genre { get; set; }
        

        public virtual Genre Genre { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
