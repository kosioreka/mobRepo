
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Mobilek
{

using System;
    using System.Collections.Generic;
    
public partial class Car
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Car()
    {

        this.Orders = new HashSet<Order>();

    }


    public int Id { get; set; }

    public string brand { get; set; }

    public string model { get; set; }

    public string colour { get; set; }

    public System.DateTime productionDate { get; set; }

    public int stationId { get; set; }

    public decimal price { get; set; }

    public bool isEfficient { get; set; }



    public virtual Station Station { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Order> Orders { get; set; }

}

}
