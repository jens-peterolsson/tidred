//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tidred.Repo
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProjectFixedPrice
    {
        public long ProjectId { get; set; }
        public long Price { get; set; }
    
        public virtual Project Project { get; set; }
    }
}
