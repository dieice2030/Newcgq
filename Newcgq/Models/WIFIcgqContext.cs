using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Newcgq.Models
{
    public partial class WIFIcgqContext : DbContext
    {
        public virtual DbSet<AdminInfo> AdminInfo { get; set; }
        public virtual DbSet<ControllerMessage> ControllerMessage { get; set; }
        public virtual DbSet<DeviceInfo> DeviceInfo { get; set; }
        public virtual DbSet<ReceiveData> ReceiveData { get; set; }
        public virtual DbSet<SendDataAd> SendDataAd { get; set; }
        public virtual DbSet<SendDataDa> SendDataDa { get; set; }
        public virtual DbSet<SendDataIo> SendDataIo { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }

        public WIFIcgqContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminInfo>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Backup1)
                    .HasColumnName("backup1")
                    .HasMaxLength(50);

                entity.Property(e => e.PassWord)
                    .HasColumnName("passWord")
                    .HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .HasColumnName("userName")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ControllerMessage>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Backup1)
                    .HasColumnName("backup1")
                    .HasMaxLength(50);

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasColumnName("data")
                    .HasMaxLength(50);

                entity.Property(e => e.DeviceInfo)
                    .IsRequired()
                    .HasColumnName("deviceInfo")
                    .HasMaxLength(50);

                entity.Property(e => e.Func)
                    .IsRequired()
                    .HasColumnName("func")
                    .HasMaxLength(50);

                entity.Property(e => e.Interval).HasColumnName("interval");

                entity.Property(e => e.IntervalUnit).HasColumnName("intervalUnit");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("userName")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DeviceInfo>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Admoduel).HasColumnName("ADModuel");

                entity.Property(e => e.Damoduel).HasColumnName("DAModuel");

                entity.Property(e => e.DeviceId)
                    .HasColumnName("deviceId")
                    .HasMaxLength(50);

                entity.Property(e => e.Iomoduel).HasColumnName("IOModuel");

                entity.Property(e => e.UserName)
                    .HasColumnName("userName")
                    .HasMaxLength(50);

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.DeviceInfo)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_DeviceInfo_UserInfo");
            });

            modelBuilder.Entity<ReceiveData>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Backup1)
                    .HasColumnName("backup1")
                    .HasMaxLength(50);

                entity.Property(e => e.Backup2)
                    .HasColumnName("backup2")
                    .HasMaxLength(50);

                entity.Property(e => e.Data)
                    .HasColumnName("data")
                    .HasMaxLength(50);

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<SendDataAd>(entity =>
            {
                entity.ToTable("SendDataAD");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Backup1)
                    .HasColumnName("backup1")
                    .HasMaxLength(50);

                entity.Property(e => e.Backup2)
                    .HasColumnName("backup2")
                    .HasMaxLength(50);

                entity.Property(e => e.Data)
                    .HasColumnName("data")
                    .HasMaxLength(50);

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<SendDataDa>(entity =>
            {
                entity.ToTable("SendDataDA");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Backup1)
                    .HasColumnName("backup1")
                    .HasMaxLength(50);

                entity.Property(e => e.Backup2)
                    .HasColumnName("backup2")
                    .HasMaxLength(50);

                entity.Property(e => e.Data)
                    .HasColumnName("data")
                    .HasMaxLength(50);

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<SendDataIo>(entity =>
            {
                entity.ToTable("SendDataIO");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Backup1)
                    .HasColumnName("backup1")
                    .HasMaxLength(50);

                entity.Property(e => e.Backup2)
                    .HasColumnName("backup2")
                    .HasMaxLength(50);

                entity.Property(e => e.Data)
                    .HasColumnName("data")
                    .HasMaxLength(50);

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.UserName);

                entity.Property(e => e.UserName)
                    .HasColumnName("userName")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(50);

                entity.Property(e => e.Coporation)
                    .HasColumnName("coporation")
                    .HasMaxLength(50);

                entity.Property(e => e.Department)
                    .HasColumnName("department")
                    .HasMaxLength(50);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PassWord)
                    .HasColumnName("passWord")
                    .HasMaxLength(50);

                entity.Property(e => e.Sex)
                    .HasColumnName("sex")
                    .HasColumnType("nchar(1)");

                entity.Property(e => e.Tel)
                    .HasColumnName("tel")
                    .HasMaxLength(50);
            });
        }
    }
}
