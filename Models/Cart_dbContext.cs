using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace cake_project.Models;

public partial class Cart_dbContext : DbContext
{
    public Cart_dbContext(DbContextOptions<Cart_dbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carts> Carts { get; set; }

    public virtual DbSet<Members> Members { get; set; }

    public virtual DbSet<PcategorySet> PcategorySet { get; set; }

    public virtual DbSet<Products> Products { get; set; }

    public virtual DbSet<Transactions> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carts>(entity =>
        {
            entity.HasKey(e => e.CartId);

            entity.HasIndex(e => e.Transaction_tNO, "IX_FK_Corresponds");

            entity.HasIndex(e => e.MemberMemberId, "IX_FK_MemberCart");

            entity.HasOne(d => d.MemberMember).WithMany(p => p.Carts)
                .HasForeignKey(d => d.MemberMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MemberCart");

            entity.HasOne(d => d.Transaction_tNONavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.Transaction_tNO)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Corresponds");

            entity.HasMany(d => d.Product_pNo).WithMany(p => p.Cart_Cart)
                .UsingEntity<Dictionary<string, object>>(
                    "Orders",
                    r => r.HasOne<Products>().WithMany()
                        .HasForeignKey("Product_pNo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Orders_Product"),
                    l => l.HasOne<Carts>().WithMany()
                        .HasForeignKey("Cart_CartId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Orders_Cart"),
                    j =>
                    {
                        j.HasKey("Cart_CartId", "Product_pNo");
                        j.HasIndex(new[] { "Product_pNo" }, "IX_FK_Orders_Product");
                    });
        });

        modelBuilder.Entity<Members>(entity =>
        {
            entity.HasKey(e => e.MemberId);

            entity.Property(e => e.Account)
                .HasMaxLength(20)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Roles).HasDefaultValueSql("((1))");
            entity.Property(e => e.email)
                .HasMaxLength(50)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.name)
                .HasMaxLength(50)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            entity.HasMany(d => d.Product_pNo).WithMany(p => p.Member_Member)
                .UsingEntity<Dictionary<string, object>>(
                    "Browses",
                    r => r.HasOne<Products>().WithMany()
                        .HasForeignKey("Product_pNo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Browses_Product"),
                    l => l.HasOne<Members>().WithMany()
                        .HasForeignKey("Member_MemberId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Browses_Member"),
                    j =>
                    {
                        j.HasKey("Member_MemberId", "Product_pNo");
                        j.HasIndex(new[] { "Product_pNo" }, "IX_FK_Browses_Product");
                    });

            entity.HasMany(d => d.Transaction_tNO).WithMany(p => p.Member_Member)
                .UsingEntity<Dictionary<string, object>>(
                    "Confirms",
                    r => r.HasOne<Transactions>().WithMany()
                        .HasForeignKey("Transaction_tNO")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Confirms_Transaction"),
                    l => l.HasOne<Members>().WithMany()
                        .HasForeignKey("Member_MemberId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Confirms_Member"),
                    j =>
                    {
                        j.HasKey("Member_MemberId", "Transaction_tNO");
                        j.HasIndex(new[] { "Transaction_tNO" }, "IX_FK_Confirms_Transaction");
                    });
        });

        modelBuilder.Entity<PcategorySet>(entity =>
        {
            entity.HasKey(e => e.PCid);

            entity.Property(e => e.PCName)
                .HasMaxLength(20)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            entity.HasMany(d => d.Product_pNo).WithMany(p => p.Pcategory_PC)
                .UsingEntity<Dictionary<string, object>>(
                    "PcategoryProduct",
                    r => r.HasOne<Products>().WithMany()
                        .HasForeignKey("Product_pNo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PcategoryProduct_Product"),
                    l => l.HasOne<PcategorySet>().WithMany()
                        .HasForeignKey("Pcategory_PCid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PcategoryProduct_Pcategory"),
                    j =>
                    {
                        j.HasKey("Pcategory_PCid", "Product_pNo");
                        j.HasIndex(new[] { "Product_pNo" }, "IX_FK_PcategoryProduct_Product");
                    });
        });

        modelBuilder.Entity<Products>(entity =>
        {
            entity.HasKey(e => e.pNo);

            entity.Property(e => e.PTime).HasColumnType("datetime");
            entity.Property(e => e.Ptitle)
                .HasMaxLength(50)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.amount)
                .HasMaxLength(20)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.description)
                .HasMaxLength(200)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.pName)
                .HasMaxLength(20)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.price)
                .HasMaxLength(10)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            entity.HasMany(d => d.Transaction_tNO).WithMany(p => p.Product_pNo)
                .UsingEntity<Dictionary<string, object>>(
                    "Records",
                    r => r.HasOne<Transactions>().WithMany()
                        .HasForeignKey("Transaction_tNO")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Records_Transaction"),
                    l => l.HasOne<Products>().WithMany()
                        .HasForeignKey("Product_pNo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Records_Product"),
                    j =>
                    {
                        j.HasKey("Product_pNo", "Transaction_tNO");
                        j.HasIndex(new[] { "Transaction_tNO" }, "IX_FK_Records_Transaction");
                    });
        });

        modelBuilder.Entity<Transactions>(entity =>
        {
            entity.HasKey(e => e.tNO);

            entity.Property(e => e.method).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.payment).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.transTime)
                .HasMaxLength(50)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
