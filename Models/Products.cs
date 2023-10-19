using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cake_project.Models;

public partial class Products
{
    public int pNo { get; set; }
    [Display(Name = "商品名稱")]
    [Required(ErrorMessage = "必填")]
    public string pName { get; set; } = null!;
    [Display(Name = "商品價錢")]
    [Required(ErrorMessage = "必填")]
    public string price { get; set; } = null!;
    [Display(Name = "商品數量")]
    [Required(ErrorMessage = "必填")]
    public string amount { get; set; } = null!;
    [Display(Name = "商品介紹")]
    public string? description { get; set; }
    [Display(Name = "商品分類")]
    public int? PCid { get; set; }
    [Display(Name = "檔案名稱")]
    public string? Ptitle { get; set; }
    [Display(Name = "發佈時間")]
    public DateTime? PTime { get; set; }

    public virtual ICollection<Carts> Cart_Cart { get; set; } = new List<Carts>();

    public virtual ICollection<Members> Member_Member { get; set; } = new List<Members>();

    public virtual ICollection<PcategorySet> Pcategory_PC { get; set; } = new List<PcategorySet>();

    public virtual ICollection<Transactions> Transaction_tNO { get; set; } = new List<Transactions>();
}
