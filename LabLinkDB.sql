-- =========================
-- Core Lookup Tables
-- =========================

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Role')
BEGIN
    CREATE TABLE Role (
        RoleId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        Role NVARCHAR(50) NOT NULL
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Flags')
BEGIN
    CREATE TABLE Flags (
        FlagId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        FlagType VARCHAR(20) NOT NULL,
        Description VARCHAR(20),
        IsActive BIT NOT NULL DEFAULT 1
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'PaymentMode')
BEGIN
    CREATE TABLE PaymentMode (
        PaymentModeId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        ModeName VARCHAR(20) NOT NULL,
        Description VARCHAR(20),
        IsActive BIT NOT NULL DEFAULT 1
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Department')
BEGIN
    CREATE TABLE Department (
        DepartmentId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        Name NVARCHAR(100) NOT NULL,
        Code NVARCHAR(20) NOT NULL,
        Description NVARCHAR(255),
        IsActive BIT NOT NULL DEFAULT 1
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'SpecimenType')
BEGIN
    CREATE TABLE SpecimenType (
        SpecimenTypeId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        TypeName NVARCHAR(100) NOT NULL
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'ContainerType')
BEGIN
    CREATE TABLE ContainerType (
        ContainerTypeId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        Name NVARCHAR(100) NOT NULL,
        SpecimenType NVARCHAR(50),
        IsActive BIT NOT NULL DEFAULT 1
    );
END

-- =========================
-- User & Security
-- =========================

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'User')
BEGIN
    CREATE TABLE [User] (
        UserId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        Name NVARCHAR(100) NOT NULL,
        Email NVARCHAR(150) UNIQUE,
        Phone NVARCHAR(20),
        IsActive BIT NOT NULL DEFAULT 1,
        Password NVARCHAR(255) NOT NULL,
        CreatedOn DATETIME,
        LastLoginAt DATETIME,
        UpdatedOn DATETIME DEFAULT GETDATE()
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'UserRole')
BEGIN
    CREATE TABLE UserRole (
        UserRoleId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        UserId INT NOT NULL,
        RoleId INT NOT NULL,
        Assigned_At DATETIME,
        IsActive BIT NOT NULL DEFAULT 1,
        CONSTRAINT FK_UserRole_User FOREIGN KEY (UserId) REFERENCES [User](UserId),
        CONSTRAINT FK_UserRole_Role FOREIGN KEY (RoleId) REFERENCES Role(RoleId)
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'AuditLog')
BEGIN
    CREATE TABLE AuditLog (
        AuditId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        UserId INT NOT NULL,
        Action NVARCHAR(100) NOT NULL,
        Resource NVARCHAR(100),
        Timestamp DATETIME DEFAULT GETDATE(),
        Metadata NVARCHAR(MAX),
        CONSTRAINT FK_AuditLog_User FOREIGN KEY (UserId) REFERENCES [User](UserId)
    );
END

-- =========================
-- Patient & Client
-- =========================

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Patient')
BEGIN
    CREATE TABLE Patient (
        PatientId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        UserId INT NOT NULL UNIQUE,
        Name NVARCHAR(100) NOT NULL,
        DOB DATE,
        Gender CHAR(1) CHECK (Gender IN ('M','F')),
        ContactInfo NVARCHAR(255),
        Address NVARCHAR(MAX),
        PrimaryPhysicianId INT,
        IsActive BIT NOT NULL DEFAULT 1,
        CreatedOn DATETIME DEFAULT GETDATE(),
        CreatedBy INT,
        CONSTRAINT FK_Patient_User FOREIGN KEY (UserId) REFERENCES [User](UserId),
        CONSTRAINT FK_Patient_Physician FOREIGN KEY (PrimaryPhysicianId) REFERENCES [User](UserId)
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'ClientAccount')
BEGIN
    CREATE TABLE ClientAccount (
        ClientId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        Name NVARCHAR(150) NOT NULL,
        Type CHAR(1) CHECK (Type IN ('c','h')),
        ContactInfo NVARCHAR(255),
        Address NVARCHAR(MAX),
        IsActive BIT NOT NULL DEFAULT 1
    );
END

-- =========================
-- Tests & Panels
-- =========================

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Test')
BEGIN
    CREATE TABLE Test (
        TestId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        Code NVARCHAR(50) NOT NULL UNIQUE,
        Name NVARCHAR(255) NOT NULL,
        DepartmentId INT,
        MethodId INT,
        SpecimenTypeId INT,
        ContainerTypeId INT NOT NULL,
        VolumeReq FLOAT,
        Units INT,
        MaxNormalValue INT NOT NULL,
        MinNormalValue INT NOT NULL,
        TATTargetMinutes INT,
        RefRangeJSON NVARCHAR(MAX),
        IsActive BIT NOT NULL DEFAULT 1,
        CONSTRAINT FK_Test_Department FOREIGN KEY (DepartmentId) REFERENCES Department(DepartmentId),
        CONSTRAINT FK_Test_SpecimenType FOREIGN KEY (SpecimenTypeId) REFERENCES SpecimenType(SpecimenTypeId),
        CONSTRAINT FK_Test_ContainerType FOREIGN KEY (ContainerTypeId) REFERENCES ContainerType(ContainerTypeId)
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Panel')
BEGIN
    CREATE TABLE Panel (
        PanelId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        PanelCode NVARCHAR(50) NOT NULL UNIQUE,
        PanelName NVARCHAR(255) NOT NULL,
        IsActive BIT NOT NULL DEFAULT 1
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'PanelTest')
BEGIN
    CREATE TABLE PanelTest (
        PanelTestId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        PanelId INT NOT NULL,
        TestId INT NOT NULL,
        ComponentsJSON NVARCHAR(MAX),
        IsActive BIT NOT NULL DEFAULT 1,
        CONSTRAINT FK_PanelTest_Panel FOREIGN KEY (PanelId) REFERENCES Panel(PanelId),
        CONSTRAINT FK_PanelTest_Test FOREIGN KEY (TestId) REFERENCES Test(TestId)
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'PriceRef')
BEGIN
    CREATE TABLE PriceRef (
        PriceId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        TestId INT,
        PanelId INT,
        Price DECIMAL(18,2) NOT NULL,
        EffectiveFrom DATETIME,
        EffectiveTo DATETIME,
        IsActive BIT NOT NULL DEFAULT 1,
        CONSTRAINT FK_PriceRef_Test FOREIGN KEY (TestId) REFERENCES Test(TestId),
        CONSTRAINT FK_PriceRef_Panel FOREIGN KEY (PanelId) REFERENCES Panel(PanelId),
        CONSTRAINT CHK_PriceRef_TestOrPanel CHECK (TestId IS NOT NULL OR PanelId IS NOT NULL)
    );
END

-- =========================
-- Visits & Appointments
-- =========================

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Visit')
BEGIN
    CREATE TABLE Visit (
        VisitTypeId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        VisitName NVARCHAR(100) NOT NULL,
        IsActive BIT NOT NULL DEFAULT 1
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Appointment')
BEGIN
    CREATE TABLE Appointment (
        AppointmentId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        PatientId INT NOT NULL,
        BookedDateTime DATETIME NOT NULL,
        VisitTypeId INT,
        Address NVARCHAR(255),
        PhlebotomistId INT,
        IsActive BIT NOT NULL DEFAULT 1,
        CONSTRAINT FK_Appointment_Patient FOREIGN KEY (PatientId) REFERENCES Patient(PatientId),
        CONSTRAINT FK_Appointment_Visit FOREIGN KEY (VisitTypeId) REFERENCES Visit(VisitTypeId),
        CONSTRAINT FK_Appointment_User FOREIGN KEY (PhlebotomistId) REFERENCES [User](UserId)
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'AppointmentItem')
BEGIN
    CREATE TABLE AppointmentItem (
        AppItemId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        AppointmentId INT NOT NULL,
        TestId INT,
        PanelId INT,
        Priority INT NOT NULL DEFAULT 0,
        Instructions NVARCHAR(MAX),
        IsActive BIT NOT NULL DEFAULT 1,
        CONSTRAINT FK_AppItem_Appointment FOREIGN KEY (AppointmentId) REFERENCES Appointment(AppointmentId),
        CONSTRAINT FK_AppItem_Test FOREIGN KEY (TestId) REFERENCES Test(TestId),
        CONSTRAINT FK_AppItem_Panel FOREIGN KEY (PanelId) REFERENCES Panel(PanelId),
        CONSTRAINT CHK_AppItem_TestOrPanel CHECK (TestId IS NOT NULL OR PanelId IS NOT NULL)
    );
END

-- =========================
-- Orders & Specimens
-- =========================

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'LabOrder')
BEGIN
    CREATE TABLE LabOrder (
        OrderId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        PatientId INT NOT NULL,
        ClientId INT,
        OrderedByUserId INT,
        OrderDate DATETIME DEFAULT GETDATE(),
        Priority INT NOT NULL DEFAULT 0,
        IsActive BIT NOT NULL DEFAULT 1,
        CONSTRAINT FK_LabOrder_Patient FOREIGN KEY (PatientId) REFERENCES Patient(PatientId),
        CONSTRAINT FK_LabOrder_Client FOREIGN KEY (ClientId) REFERENCES ClientAccount(ClientId),
        CONSTRAINT FK_LabOrder_User FOREIGN KEY (OrderedByUserId) REFERENCES [User](UserId)
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'OrderItem')
BEGIN
    CREATE TABLE OrderItem (
        OrderItemId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        OrderId INT NOT NULL,
        TestId INT,
        PanelId INT,
        Department NVARCHAR(100),
        IsActive BIT NOT NULL DEFAULT 1,
        CONSTRAINT FK_OrderItem_Order FOREIGN KEY (OrderId) REFERENCES LabOrder(OrderId),
        CONSTRAINT FK_OrderItem_Test FOREIGN KEY (TestId) REFERENCES Test(TestId),
        CONSTRAINT FK_OrderItem_Panel FOREIGN KEY (PanelId) REFERENCES Panel(PanelId),
        CONSTRAINT CHK_OrderItem_TestOrPanel CHECK (TestId IS NOT NULL OR PanelId IS NOT NULL)
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Specimen')
BEGIN
    CREATE TABLE Specimen (
        SpecimenId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        OrderId INT NOT NULL,
        OrderItemId INT NOT NULL,
        SpecimenTypeId INT,
        ContainerTypeId INT,
        CollectedBy INT,
        CollectedDate DATETIME,
        RejectionReason NVARCHAR(MAX),
        IsActive BIT NOT NULL DEFAULT 1,
        CONSTRAINT FK_Specimen_Order FOREIGN KEY (OrderId) REFERENCES LabOrder(OrderId),
        CONSTRAINT FK_Specimen_OrderItem FOREIGN KEY (OrderItemId) REFERENCES OrderItem(OrderItemId),
        CONSTRAINT FK_Specimen_SpecimenType FOREIGN KEY (SpecimenTypeId) REFERENCES SpecimenType(SpecimenTypeId),
        CONSTRAINT FK_Specimen_ContainerType FOREIGN KEY (ContainerTypeId) REFERENCES ContainerType(ContainerTypeId),
        CONSTRAINT FK_Specimen_User FOREIGN KEY (CollectedBy) REFERENCES [User](UserId)
    );
END

-- =========================
-- Results & Validation
-- =========================

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'ResultEntry')
BEGIN
    CREATE TABLE ResultEntry (
        ResultId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        OrderItemId INT NOT NULL,
        TestId INT NOT NULL,
        Analyte NVARCHAR(100),
        Value NVARCHAR(255),
        Units NVARCHAR(50),
        FlagId INT NOT NULL,
        EnteredBy INT,
        EnteredDate DATETIME DEFAULT GETDATE(),
        Source NVARCHAR(50),
        IsActive BIT NOT NULL DEFAULT 1,
        CONSTRAINT FK_ResultEntry_OrderItem FOREIGN KEY (OrderItemId) REFERENCES OrderItem(OrderItemId),
        CONSTRAINT FK_ResultEntry_Test FOREIGN KEY (TestId) REFERENCES Test(TestId),
        CONSTRAINT FK_ResultEntry_Flag FOREIGN KEY (FlagId) REFERENCES Flags(FlagId),
        CONSTRAINT FK_ResultEntry_User FOREIGN KEY (EnteredBy) REFERENCES [User](UserId)
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'TechValidation')
BEGIN
    CREATE TABLE TechValidation (
        TvId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        ResultId INT NOT NULL,
        UserId INT NOT NULL,
        DeltaCheckJSON NVARCHAR(MAX),
        ValidationDate DATETIME DEFAULT GETDATE(),
        IsActive BIT NOT NULL DEFAULT 1,
        CONSTRAINT FK_TechValidation_Result FOREIGN KEY (ResultId) REFERENCES ResultEntry(ResultId),
        CONSTRAINT FK_TechValidation_User FOREIGN KEY (UserId) REFERENCES [User](UserId)
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'LabReport')
BEGIN
    CREATE TABLE LabReport (
        ReportId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        OrderId INT NOT NULL,
        Version INT DEFAULT 1,
        ReportURI NVARCHAR(MAX),
        GeneratedDate DATETIME,
        AuthorizedBy INT,
        AuthorizedDate DATETIME,
        IsActive BIT NOT NULL DEFAULT 1,
        CONSTRAINT FK_LabReport_Order FOREIGN KEY (OrderId) REFERENCES LabOrder(OrderId),
        CONSTRAINT FK_LabReport_User FOREIGN KEY (AuthorizedBy) REFERENCES [User](UserId)
    );
END

-- =========================
-- Billing
-- =========================

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'InvoiceRef')
BEGIN
    CREATE TABLE InvoiceRef (
        InvoiceId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        OrderId INT NOT NULL,
        PatientId INT NOT NULL,
        ClientId INT,
        Amount DECIMAL(18,2) NOT NULL,
        Tax DECIMAL(18,2),
        Total DECIMAL(18,2) NOT NULL,
        GeneratedDate DATETIME DEFAULT GETDATE(),
        IsActive BIT NOT NULL DEFAULT 1,
        CONSTRAINT FK_Invoice_Order FOREIGN KEY (OrderId) REFERENCES LabOrder(OrderId),
        CONSTRAINT FK_Invoice_Patient FOREIGN KEY (PatientId) REFERENCES Patient(PatientId),
        CONSTRAINT FK_Invoice_Client FOREIGN KEY (ClientId) REFERENCES ClientAccount(ClientId)
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'PaymentRef')
BEGIN
    CREATE TABLE PaymentRef (
        PaymentId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        InvoiceId INT NOT NULL,
        Amount DECIMAL(18,2) NOT NULL,
        ModeId INT NOT NULL,
        PaymentDate DATETIME,
        IsActive BIT NOT NULL DEFAULT 1,
        CONSTRAINT FK_Payment_Invoice FOREIGN KEY (InvoiceId) REFERENCES InvoiceRef(InvoiceId),
        CONSTRAINT FK_Payment_Mode FOREIGN KEY (ModeId) REFERENCES PaymentMode(PaymentModeId)
    );
END

-- =========================
-- QC & Logs
-- =========================

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'QCRecord')
BEGIN
    CREATE TABLE QCRecord (
        QcId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        InstrumentId INT NOT NULL,
        TestId INT NOT NULL,
        ControlLevel NVARCHAR(50),
        ResultValue NVARCHAR(255),
        Units NVARCHAR(50),
        RunDate DATETIME DEFAULT GETDATE(),
        RuleFlags NVARCHAR(MAX),
        Status NVARCHAR(50) NOT NULL
    );
END