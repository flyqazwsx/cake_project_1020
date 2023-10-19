using System;
using System.Collections.Generic;

namespace cake_project.Models;

public partial class Transactions
{
    public int tNO { get; set; }

    public string transTime { get; set; } = null!;

    public string payment { get; set; } = null!;

    public string method { get; set; } = null!;

    public virtual ICollection<Carts> Carts { get; set; } = new List<Carts>();

    public virtual ICollection<Members> Member_Member { get; set; } = new List<Members>();

    public virtual ICollection<Products> Product_pNo { get; set; } = new List<Products>();
}
