using System;
using System.Collections.Generic;

namespace cake_project.Models;

public partial class Carts
{
    public int CartId { get; set; }

    public int MemberMemberId { get; set; }

    public int Transaction_tNO { get; set; }

    public virtual Members MemberMember { get; set; } = null!;

    public virtual Transactions Transaction_tNONavigation { get; set; } = null!;

    public virtual ICollection<Products> Product_pNo { get; set; } = new List<Products>();
}
