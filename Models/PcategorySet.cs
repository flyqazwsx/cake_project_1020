using System;
using System.Collections.Generic;

namespace cake_project.Models;

public partial class PcategorySet
{
    public int PCid { get; set; }

    public string PCName { get; set; } = null!;

    public virtual ICollection<Products> Product_pNo { get; set; } = new List<Products>();
}
