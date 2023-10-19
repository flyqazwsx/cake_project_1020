using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cake_project.Models;

public partial class Members
{
    public int MemberId { get; set; }
    [Display(Name ="帳號")]
    public string Account { get; set; } = null!;
    [Display(Name = "密碼")]
    public string Password { get; set; } = null!;
    [Display(Name = "名稱")]
    public string name { get; set; } = null!;
    [Display(Name = "電子郵件")]
    public string email { get; set; } = null!;

    public int? Roles { get; set; }

    public virtual ICollection<Carts> Carts { get; set; } = new List<Carts>();

    public virtual ICollection<Products> Product_pNo { get; set; } = new List<Products>();

    public virtual ICollection<Transactions> Transaction_tNO { get; set; } = new List<Transactions>();
}
