using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LabLinkBackend.Models;

public partial class LabLinkDbContext : DbContext
{
    public LabLinkDbContext()
    {
    }

    public LabLinkDbContext(DbContextOptions<LabLinkDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Accession> Accessions { get; set; }

    public virtual DbSet<Addendum> Addenda { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<AppointmentItem> AppointmentItems { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<ClientAccount> ClientAccounts { get; set; }

    public virtual DbSet<CompetencyLog> CompetencyLogs { get; set; }

    public virtual DbSet<ContainerType> ContainerTypes { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Flag> Flags { get; set; }

    public virtual DbSet<InstrumentRef> InstrumentRefs { get; set; }

    public virtual DbSet<InterfaceType> InterfaceTypes { get; set; }

    public virtual DbSet<InvoiceRef> InvoiceRefs { get; set; }

    public virtual DbSet<LabOrder> LabOrders { get; set; }

    public virtual DbSet<LabReport> LabReports { get; set; }

    public virtual DbSet<LabReportPack> LabReportPacks { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Panel> Panels { get; set; }

    public virtual DbSet<PanelTest> PanelTests { get; set; }

    public virtual DbSet<PathologyReview> PathologyReviews { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PaymentMode> PaymentModes { get; set; }

    public virtual DbSet<PaymentRef> PaymentRefs { get; set; }

    public virtual DbSet<PriceRef> PriceRefs { get; set; }

    public virtual DbSet<Qcrecord> Qcrecords { get; set; }

    public virtual DbSet<Recipient> Recipients { get; set; }

    public virtual DbSet<ReportDelivery> ReportDeliveries { get; set; }

    public virtual DbSet<ResultEntry> ResultEntries { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Speciman> Specimen { get; set; }

    public virtual DbSet<SpecimenType> SpecimenTypes { get; set; }

    public virtual DbSet<TechValidation> TechValidations { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Visit> Visits { get; set; }

    public virtual DbSet<Worklist> Worklists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Accession>(entity =>
        {
            entity.HasKey(e => e.AccessionId).HasName("PK__Accessio__B4B2533D91A55B9E");

            entity.ToTable("Accession");

            entity.HasIndex(e => e.AccessionNumber, "UQ__Accessio__AE5A718E0F85B240").IsUnique();

            entity.Property(e => e.AccessionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.AccessionNumber).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Section).HasMaxLength(50);

            entity.HasOne(d => d.Order).WithMany(p => p.Accessions)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Accession_Order");
        });

        modelBuilder.Entity<Addendum>(entity =>
        {
            entity.HasKey(e => e.AddendumId).HasName("PK__Addendum__5F1020D068A42B79");

            entity.ToTable("Addendum");

            entity.Property(e => e.AddedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Order).WithMany(p => p.Addenda)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Addendum_Order");

            entity.HasOne(d => d.User).WithMany(p => p.Addenda)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Addendum_User");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCC2B0F9F50F");

            entity.ToTable("Appointment");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.BookedDateTime).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointment_Patient");

            entity.HasOne(d => d.Phlebotomist).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PhlebotomistId)
                .HasConstraintName("FK_Appointment_User");

            entity.HasOne(d => d.VisitType).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.VisitTypeId)
                .HasConstraintName("FK_Appointment_Visit");
        });

        modelBuilder.Entity<AppointmentItem>(entity =>
        {
            entity.HasKey(e => e.AppItemId).HasName("PK__Appointm__2AB731087A1FB9A1");

            entity.ToTable("AppointmentItem");

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Appointment).WithMany(p => p.AppointmentItems)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppItem_Appointment");

            entity.HasOne(d => d.Panel).WithMany(p => p.AppointmentItems)
                .HasForeignKey(d => d.PanelId)
                .HasConstraintName("FK_AppItem_Panel");

            entity.HasOne(d => d.Test).WithMany(p => p.AppointmentItems)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("FK_AppItem_Test");
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("PK__AuditLog__A17F23989F516BA0");

            entity.ToTable("AuditLog");

            entity.Property(e => e.Action).HasMaxLength(100);
            entity.Property(e => e.Resource).HasMaxLength(100);
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuditLog_User");
        });

        modelBuilder.Entity<ClientAccount>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__ClientAc__E67E1A245BD6D29B");

            entity.ToTable("ClientAccount");

            entity.Property(e => e.ContactInfo).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Type)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<CompetencyLog>(entity =>
        {
            entity.HasKey(e => e.CompetencyLogId).HasName("PK__Competen__E76A996DF8042AE6");

            entity.ToTable("CompetencyLog");

            entity.Property(e => e.CompetencyType).HasMaxLength(100);
            entity.Property(e => e.CompletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Reviewer).WithMany(p => p.CompetencyLogReviewers)
                .HasForeignKey(d => d.ReviewerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompetencyLog_Reviewer");

            entity.HasOne(d => d.StaffUser).WithMany(p => p.CompetencyLogStaffUsers)
                .HasForeignKey(d => d.StaffUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompetencyLog_Staff");
        });

        modelBuilder.Entity<ContainerType>(entity =>
        {
            entity.HasKey(e => e.ContainerTypeId).HasName("PK__Containe__46FA6FD9D73872C8");

            entity.ToTable("ContainerType");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.SpecimenType).HasMaxLength(50);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BED58D910D0");

            entity.ToTable("Department");

            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Flag>(entity =>
        {
            entity.HasKey(e => e.FlagId).HasName("PK__Flags__780D4593B1088662");

            entity.Property(e => e.Description)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FlagType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<InstrumentRef>(entity =>
        {
            entity.HasKey(e => e.InstrumentId).HasName("PK__Instrume__430A5386B333FA88");

            entity.ToTable("InstrumentRef");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Model).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Section).HasMaxLength(50);

            entity.HasOne(d => d.InterfaceType).WithMany(p => p.InstrumentRefs)
                .HasForeignKey(d => d.InterfaceTypeId)
                .HasConstraintName("FK_Instrument_Interface");
        });

        modelBuilder.Entity<InterfaceType>(entity =>
        {
            entity.HasKey(e => e.InterfaceTypeId).HasName("PK__Interfac__BF5D47AB1A5F52B1");

            entity.ToTable("InterfaceType");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<InvoiceRef>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__InvoiceR__D796AAB55F37DD7D");

            entity.ToTable("InvoiceRef");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GeneratedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Tax).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Client).WithMany(p => p.InvoiceRefs)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_Invoice_Client");

            entity.HasOne(d => d.Order).WithMany(p => p.InvoiceRefs)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Order");

            entity.HasOne(d => d.Patient).WithMany(p => p.InvoiceRefs)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Patient");
        });

        modelBuilder.Entity<LabOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__LabOrder__C3905BCFE8228F02");

            entity.ToTable("LabOrder");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Client).WithMany(p => p.LabOrders)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_LabOrder_Client");

            entity.HasOne(d => d.OrderedByUser).WithMany(p => p.LabOrders)
                .HasForeignKey(d => d.OrderedByUserId)
                .HasConstraintName("FK_LabOrder_User");

            entity.HasOne(d => d.Patient).WithMany(p => p.LabOrders)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LabOrder_Patient");
        });

        modelBuilder.Entity<LabReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__LabRepor__D5BD4805FD806413");

            entity.ToTable("LabReport");

            entity.Property(e => e.AuthorizedDate).HasColumnType("datetime");
            entity.Property(e => e.GeneratedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ReportUri).HasColumnName("ReportURI");
            entity.Property(e => e.Version).HasDefaultValue(1);

            entity.HasOne(d => d.AuthorizedByNavigation).WithMany(p => p.LabReports)
                .HasForeignKey(d => d.AuthorizedBy)
                .HasConstraintName("FK_LabReport_User");

            entity.HasOne(d => d.Order).WithMany(p => p.LabReports)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LabReport_Order");
        });

        modelBuilder.Entity<LabReportPack>(entity =>
        {
            entity.HasKey(e => e.LabReportPackId).HasName("PK__LabRepor__6A46AD253081057A");

            entity.ToTable("LabReportPack");

            entity.Property(e => e.GeneratedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Scope).HasMaxLength(255);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E12915FB6C3");

            entity.ToTable("Notification");

            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notification_User");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__OrderIte__57ED0681F61DDCD8");

            entity.ToTable("OrderItem");

            entity.Property(e => e.Department).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItem_Order");

            entity.HasOne(d => d.Panel).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.PanelId)
                .HasConstraintName("FK_OrderItem_Panel");

            entity.HasOne(d => d.Test).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("FK_OrderItem_Test");
        });

        modelBuilder.Entity<Panel>(entity =>
        {
            entity.HasKey(e => e.PanelId).HasName("PK__Panel__49CA68064C2DDDA0");

            entity.ToTable("Panel");

            entity.HasIndex(e => e.PanelCode, "UQ__Panel__75B0AFB7AC7DF449").IsUnique();

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PanelCode).HasMaxLength(50);
            entity.Property(e => e.PanelName).HasMaxLength(255);
        });

        modelBuilder.Entity<PanelTest>(entity =>
        {
            entity.HasKey(e => e.PanelTestId).HasName("PK__PanelTes__E1CE350B09B50239");

            entity.ToTable("PanelTest");

            entity.Property(e => e.ComponentsJson).HasColumnName("ComponentsJSON");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Panel).WithMany(p => p.PanelTests)
                .HasForeignKey(d => d.PanelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PanelTest_Panel");

            entity.HasOne(d => d.Test).WithMany(p => p.PanelTests)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PanelTest_Test");
        });

        modelBuilder.Entity<PathologyReview>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Patholog__74BC79CE155427F1");

            entity.ToTable("PathologyReview");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ReviewDate).HasColumnType("datetime");

            entity.HasOne(d => d.Order).WithMany(p => p.PathologyReviews)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PathologyReview_Order");

            entity.HasOne(d => d.User).WithMany(p => p.PathologyReviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PathologyReview_User");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patient__970EC36608EDAFF3");

            entity.ToTable("Patient");

            entity.HasIndex(e => e.UserId, "UQ__Patient__1788CC4D85CB9787").IsUnique();

            entity.Property(e => e.ContactInfo).HasMaxLength(255);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(100);

            

            entity.HasOne(d => d.User).WithOne(p => p.PatientUser)
                .HasForeignKey<Patient>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patient_User");
        });

        modelBuilder.Entity<PaymentMode>(entity =>
        {
            entity.HasKey(e => e.PaymentModeId).HasName("PK__PaymentM__F95995495A034130");

            entity.ToTable("PaymentMode");

            entity.Property(e => e.Description)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModeName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PaymentRef>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__PaymentR__9B556A38E358416C");

            entity.ToTable("PaymentRef");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");

            entity.HasOne(d => d.Invoice).WithMany(p => p.PaymentRefs)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_Invoice");

            entity.HasOne(d => d.Mode).WithMany(p => p.PaymentRefs)
                .HasForeignKey(d => d.ModeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_Mode");
        });

        modelBuilder.Entity<PriceRef>(entity =>
        {
            entity.HasKey(e => e.PriceId).HasName("PK__PriceRef__49575BAFCCA63FBE");

            entity.ToTable("PriceRef");

            entity.Property(e => e.EffectiveFrom).HasColumnType("datetime");
            entity.Property(e => e.EffectiveTo).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Panel).WithMany(p => p.PriceRefs)
                .HasForeignKey(d => d.PanelId)
                .HasConstraintName("FK_PriceRef_Panel");

            entity.HasOne(d => d.Test).WithMany(p => p.PriceRefs)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("FK_PriceRef_Test");
        });

        modelBuilder.Entity<Qcrecord>(entity =>
        {
            entity.HasKey(e => e.QcId).HasName("PK__QCRecord__F5ECB18C1371668F");

            entity.ToTable("QCRecord");

            entity.Property(e => e.ControlLevel).HasMaxLength(50);
            entity.Property(e => e.ResultValue).HasMaxLength(255);
            entity.Property(e => e.RunDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Units).HasMaxLength(50);

            entity.HasOne(d => d.Instrument).WithMany(p => p.Qcrecords)
                .HasForeignKey(d => d.InstrumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QCRecord_Instrument");

            entity.HasOne(d => d.Test).WithMany(p => p.Qcrecords)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QCRecord_Test");
        });

        modelBuilder.Entity<Recipient>(entity =>
        {
            entity.HasKey(e => e.RecipientId).HasName("PK__Recipien__F0A6024D9FC08155");

            entity.ToTable("Recipient");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.RecipientType).HasMaxLength(50);

            entity.HasOne(d => d.Report).WithMany(p => p.Recipients)
                .HasForeignKey(d => d.ReportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Recipient_Report");
        });

        modelBuilder.Entity<ReportDelivery>(entity =>
        {
            entity.HasKey(e => e.DeliveryId).HasName("PK__ReportDe__626D8FCE67F33EC6");

            entity.ToTable("ReportDelivery");

            entity.Property(e => e.Channel).HasMaxLength(50);
            entity.Property(e => e.DeliveredDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.RecipientType).HasMaxLength(50);

            entity.HasOne(d => d.Report).WithMany(p => p.ReportDeliveries)
                .HasForeignKey(d => d.ReportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReportDelivery_Report");
        });

        modelBuilder.Entity<ResultEntry>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__ResultEn__976902088E748652");

            entity.ToTable("ResultEntry");

            entity.Property(e => e.Analyte).HasMaxLength(100);
            entity.Property(e => e.EnteredDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Source).HasMaxLength(50);
            entity.Property(e => e.Units).HasMaxLength(50);
            entity.Property(e => e.Value).HasMaxLength(255);

            entity.HasOne(d => d.EnteredByNavigation).WithMany(p => p.ResultEntries)
                .HasForeignKey(d => d.EnteredBy)
                .HasConstraintName("FK_ResultEntry_User");

            entity.HasOne(d => d.Flag).WithMany(p => p.ResultEntries)
                .HasForeignKey(d => d.FlagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ResultEntry_Flag");

            entity.HasOne(d => d.OrderItem).WithMany(p => p.ResultEntries)
                .HasForeignKey(d => d.OrderItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ResultEntry_OrderItem");

            entity.HasOne(d => d.Test).WithMany(p => p.ResultEntries)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ResultEntry_Test");
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1A917EB82E");

            entity.ToTable("Role");

            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("Role");
        });

        modelBuilder.Entity<Speciman>(entity =>
        {
            entity.HasKey(e => e.SpecimenId).HasName("PK__Specimen__719B2271ECD3FE50");

            entity.Property(e => e.CollectedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.CollectedByNavigation).WithMany(p => p.Specimen)
                .HasForeignKey(d => d.CollectedBy)
                .HasConstraintName("FK_Specimen_User");

            entity.HasOne(d => d.ContainerType).WithMany(p => p.Specimen)
                .HasForeignKey(d => d.ContainerTypeId)
                .HasConstraintName("FK_Specimen_ContainerType");

            entity.HasOne(d => d.Order).WithMany(p => p.Specimen)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Specimen_Order");

            entity.HasOne(d => d.OrderItem).WithMany(p => p.Specimen)
                .HasForeignKey(d => d.OrderItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Specimen_OrderItem");

            entity.HasOne(d => d.SpecimenType).WithMany(p => p.Specimen)
                .HasForeignKey(d => d.SpecimenTypeId)
                .HasConstraintName("FK_Specimen_SpecimenType");
        });

        modelBuilder.Entity<SpecimenType>(entity =>
        {
            entity.HasKey(e => e.SpecimenTypeId).HasName("PK__Specimen__0218E7DB1A433393");

            entity.ToTable("SpecimenType");

            entity.Property(e => e.TypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<TechValidation>(entity =>
        {
            entity.HasKey(e => e.TvId).HasName("PK__TechVali__F97248AE2536D887");

            entity.ToTable("TechValidation");

            entity.Property(e => e.DeltaCheckJson).HasColumnName("DeltaCheckJSON");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ValidationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Result).WithMany(p => p.TechValidations)
                .HasForeignKey(d => d.ResultId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TechValidation_Result");

            entity.HasOne(d => d.User).WithMany(p => p.TechValidations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TechValidation_User");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__Test__8CC33160062D6592");

            entity.ToTable("Test");

            entity.HasIndex(e => e.Code, "UQ__Test__A25C5AA711F98565").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.RefRangeJson).HasColumnName("RefRangeJSON");
            entity.Property(e => e.TattargetMinutes).HasColumnName("TATTargetMinutes");

            entity.HasOne(d => d.ContainerType).WithMany(p => p.Tests)
                .HasForeignKey(d => d.ContainerTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Test_ContainerType");

            entity.HasOne(d => d.Department).WithMany(p => p.Tests)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Test_Department");

            entity.HasOne(d => d.SpecimenType).WithMany(p => p.Tests)
                .HasForeignKey(d => d.SpecimenTypeId)
                .HasConstraintName("FK_Test_SpecimenType");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C1BF5332B");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__A9D105349E858773").IsUnique();

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastLoginAt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__UserRole__3D978A3546C5285A");

            entity.ToTable("UserRole");

            entity.Property(e => e.AssignedAt)
                .HasColumnType("datetime")
                .HasColumnName("Assigned_At");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Roles).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRole_Role");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRole_User");
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.HasKey(e => e.VisitTypeId).HasName("PK__Visit__9BF3CC522C1C3095");

            entity.ToTable("Visit");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.VisitName).HasMaxLength(100);
        });

        modelBuilder.Entity<Worklist>(entity =>
        {
            entity.HasKey(e => e.WorklistId).HasName("PK__Worklist__F44B6698671EB079");

            entity.ToTable("Worklist");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Section).HasMaxLength(50);

            entity.HasOne(d => d.Instrument).WithMany(p => p.Worklists)
                .HasForeignKey(d => d.InstrumentId)
                .HasConstraintName("FK_Worklist_Instrument");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
