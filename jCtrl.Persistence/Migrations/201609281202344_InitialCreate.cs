namespace jCtrl.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adverts",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 25),
                        Description = c.String(maxLength: 150),
                        ImageFilename_Desktop = c.String(nullable: false, maxLength: 50),
                        ImageFilename_Device = c.String(nullable: false, maxLength: 50),
                        LinkUrl = c.String(maxLength: 150),
                        PlayerId = c.String(maxLength: 25),
                        VideoId = c.String(maxLength: 25),
                        ExpiresUtc = c.DateTime(nullable: false),
                        IsPriority = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Branch_Id = c.Int(nullable: false),
                        AdvertType_Id = c.String(nullable: false, maxLength: 1),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdvertTypes", t => t.AdvertType_Id)
                .ForeignKey("dbo.Branches", t => t.Branch_Id)
                .Index(t => t.ExpiresUtc)
                .Index(t => t.IsPriority)
                .Index(t => t.IsActive)
                .Index(t => t.Branch_Id)
                .Index(t => t.AdvertType_Id);
            
            CreateTable(
                "dbo.AdvertTypes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 1),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BranchCode = c.String(nullable: false, maxLength: 3),
                        SiteCode = c.String(nullable: false, maxLength: 2),
                        FlagFilename = c.String(nullable: false, maxLength: 50),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SortOrder = c.Short(nullable: false),
                        Currency_Code = c.String(nullable: false, maxLength: 3),
                        Language_Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(maxLength: 20),
                        EmailAddress = c.String(maxLength: 250),
                        AddressLine1 = c.String(nullable: false, maxLength: 35),
                        AddressLine2 = c.String(maxLength: 35),
                        TownCity = c.String(maxLength: 30),
                        CountyState = c.String(maxLength: 30),
                        PostalCode = c.String(maxLength: 10),
                        CountryName = c.String(nullable: false, maxLength: 50),
                        IsVerifiedAddress = c.Boolean(nullable: false),
                        Country_Code = c.String(nullable: false, maxLength: 2),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Code)
                .ForeignKey("dbo.Currencies", t => t.Currency_Code)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .Index(t => t.BranchCode, unique: true)
                .Index(t => t.SiteCode, unique: true)
                .Index(t => t.SortOrder)
                .Index(t => t.Currency_Code)
                .Index(t => t.Language_Id)
                .Index(t => t.Country_Code);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 2),
                        InternationalDialingCode = c.String(maxLength: 3),
                        IsEuropean = c.Boolean(nullable: false),
                        IsMemberOfEEC = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.CountryTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlternativeSpellings = c.String(),
                        Country_Code = c.String(nullable: false, maxLength: 2),
                        Title = c.String(nullable: false),
                        Language_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Code)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .Index(t => t.Country_Code)
                .Index(t => t.Language_Id);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 2),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Code);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 3),
                        Name = c.String(nullable: false, maxLength: 60),
                        Symbol = c.String(maxLength: 1),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AccountNumber = c.String(maxLength: 10),
                        AccountName = c.String(maxLength: 35),
                        Title = c.String(maxLength: 5),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 30),
                        CompanyName = c.String(maxLength: 35),
                        CompanyTaxNo = c.String(maxLength: 25),
                        CompanyTaxNo_LastChecked = c.DateTime(),
                        InternalComment = c.String(maxLength: 200),
                        IsMarketingSubscriber = c.Boolean(nullable: false),
                        IsPaperlessBilling = c.Boolean(nullable: false),
                        IsCreditAccount = c.Boolean(nullable: false),
                        IsOnStop = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CustomerAccountType_Code = c.String(nullable: false, maxLength: 1),
                        Branch_Id = c.Int(nullable: false),
                        Language_Id = c.Int(nullable: false),
                        PaymentMethod_Code = c.String(nullable: false, maxLength: 2),
                        ShippingMethod_Id = c.Int(),
                        TradingTerms_Code = c.String(nullable: false, maxLength: 2),
                        AddressLine1 = c.String(nullable: false, maxLength: 35),
                        AddressLine2 = c.String(maxLength: 35),
                        TownCity = c.String(maxLength: 30),
                        CountyState = c.String(maxLength: 30),
                        PostalCode = c.String(maxLength: 10),
                        CountryName = c.String(nullable: false, maxLength: 50),
                        IsVerifiedAddress = c.Boolean(nullable: false),
                        Country_Code = c.String(nullable: false, maxLength: 2),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                        UserAccount_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerAccountTypes", t => t.CustomerAccountType_Code)
                .ForeignKey("dbo.Branches", t => t.Branch_Id)
                .ForeignKey("dbo.Countries", t => t.Country_Code)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .ForeignKey("dbo.PaymentMethods", t => t.PaymentMethod_Code)
                .ForeignKey("dbo.ShippingMethods", t => t.ShippingMethod_Id)
                .ForeignKey("dbo.CustomerTradingLevels", t => t.TradingTerms_Code)
                .ForeignKey("dbo.AspNetUsers", t => t.UserAccount_Id)
                .Index(t => t.AccountNumber)
                .Index(t => t.AccountName)
                .Index(t => t.LastName)
                .Index(t => t.CompanyName)
                .Index(t => t.IsMarketingSubscriber)
                .Index(t => t.IsActive)
                .Index(t => t.CustomerAccountType_Code)
                .Index(t => t.Branch_Id)
                .Index(t => t.Language_Id)
                .Index(t => t.PaymentMethod_Code)
                .Index(t => t.ShippingMethod_Id)
                .Index(t => t.TradingTerms_Code)
                .Index(t => t.Country_Code)
                .Index(t => t.UserAccount_Id);
            
            CreateTable(
                "dbo.CustomerAccountTypes",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 1),
                        Name = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.CustomerEmailAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsMarketing = c.Boolean(nullable: false),
                        IsBilling = c.Boolean(nullable: false),
                        IsVerified = c.Boolean(nullable: false),
                        VerificationToken = c.Guid(),
                        Customer_Id = c.Guid(nullable: false),
                        Address = c.String(nullable: false, maxLength: 255),
                        IsDefault = c.Boolean(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.Address)
                .Index(t => t.IsDefault);
            
            CreateTable(
                "dbo.PaymentCards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 25),
                        EncyptedData = c.String(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        Customer_Id = c.Guid(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.IsDefault)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 2),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Code)
                .Index(t => t.IsActive, name: "Idx_PaymentMethod_Active");
            
            CreateTable(
                "dbo.PaymentMethodTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaymentMethod_Code = c.String(nullable: false, maxLength: 2),
                        Title = c.String(nullable: false),
                        Language_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .ForeignKey("dbo.PaymentMethods", t => t.PaymentMethod_Code)
                .Index(t => t.PaymentMethod_Code)
                .Index(t => t.Language_Id);
            
            CreateTable(
                "dbo.CustomerPhoneNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDefault = c.Boolean(nullable: false),
                        PhoneNumberType_Id = c.Int(nullable: false),
                        Customer_Id = c.Guid(nullable: false),
                        InternationalCode = c.String(maxLength: 3),
                        AreaCode = c.String(nullable: false),
                        Number = c.String(nullable: false, maxLength: 15),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.PhoneNumberTypes", t => t.PhoneNumberType_Id)
                .Index(t => t.IsDefault)
                .Index(t => t.PhoneNumberType_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.PhoneNumberTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        IsDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDefault);
            
            CreateTable(
                "dbo.CustomerShippingAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Customer_Id = c.Guid(nullable: false),
                        DisplayName = c.String(nullable: false, maxLength: 20),
                        IsDefault = c.Boolean(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(maxLength: 20),
                        EmailAddress = c.String(maxLength: 250),
                        AddressLine1 = c.String(nullable: false, maxLength: 35),
                        AddressLine2 = c.String(maxLength: 35),
                        TownCity = c.String(maxLength: 30),
                        CountyState = c.String(maxLength: 30),
                        PostalCode = c.String(maxLength: 10),
                        CountryName = c.String(nullable: false, maxLength: 50),
                        IsVerifiedAddress = c.Boolean(nullable: false),
                        Country_Code = c.String(nullable: false, maxLength: 2),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Code)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.IsDefault)
                .Index(t => t.Country_Code);
            
            CreateTable(
                "dbo.ShippingMethods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProviderReference = c.String(maxLength: 10),
                        Title = c.String(nullable: false, maxLength: 35),
                        MaxWeightKgs = c.Decimal(nullable: false, precision: 8, scale: 3),
                        MaxDimensionCms = c.Decimal(nullable: false, precision: 5, scale: 1),
                        MaxVolumeCm3 = c.Int(nullable: false),
                        CostPrice = c.Decimal(nullable: false, precision: 6, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 6, scale: 2),
                        InternalMethodId = c.Int(),
                        SortOrder = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Branch_Id = c.Int(nullable: false),
                        ShippingProvider_Id = c.Int(nullable: false),
                        ShippingCoverageLevel_Id = c.String(nullable: false, maxLength: 1),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.Branch_Id)
                .ForeignKey("dbo.ShippingCoverageLevels", t => t.ShippingCoverageLevel_Id)
                .ForeignKey("dbo.ShippingProviders", t => t.ShippingProvider_Id)
                .Index(t => t.SortOrder)
                .Index(t => t.IsActive)
                .Index(t => t.Branch_Id)
                .Index(t => t.ShippingProvider_Id)
                .Index(t => t.ShippingCoverageLevel_Id);
            
            CreateTable(
                "dbo.ShippingCoverageLevels",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 1),
                        Name = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.ShippingProviders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        LogoFilename = c.String(maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsActive);
            
            CreateTable(
                "dbo.CustomerTradingLevels",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 2),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.CustomerVehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ModelYear = c.Short(nullable: false),
                        DisplayName = c.String(nullable: false, maxLength: 20),
                        RegistrationNumber = c.String(maxLength: 20),
                        EngineNumber = c.String(maxLength: 20),
                        ChassisNumber = c.String(maxLength: 20),
                        VIN = c.String(maxLength: 20),
                        Notes = c.String(maxLength: 200),
                        IsDefault = c.Boolean(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Customer_Id = c.Guid(nullable: false),
                        Vehicle_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.Vehicles", t => t.Vehicle_Id)
                .Index(t => t.IsDefault)
                .Index(t => t.SortOrder)
                .Index(t => t.IsActive)
                .Index(t => t.Customer_Id)
                .Index(t => t.Vehicle_Id);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Marque_Id = c.Int(nullable: false),
                        Range_Id = c.Int(nullable: false),
                        Model_Id = c.Int(nullable: false),
                        ModelVariant_Id = c.Int(nullable: false),
                        Body_Id = c.Int(nullable: false),
                        EngineType_Id = c.Int(nullable: false),
                        Engine_Id = c.Int(nullable: false),
                        Transmission_Id = c.Int(nullable: false),
                        Drivetrain_Id = c.Int(),
                        Steering_Id = c.Int(),
                        TrimLevel_Id = c.Int(),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VehicleBodyVariants", t => t.Body_Id)
                .ForeignKey("dbo.VehicleDrivetrainVariants", t => t.Drivetrain_Id)
                .ForeignKey("dbo.VehicleEngineVariants", t => t.Engine_Id)
                .ForeignKey("dbo.VehicleEngineTypeVariants", t => t.EngineType_Id)
                .ForeignKey("dbo.VehicleMarques", t => t.Marque_Id)
                .ForeignKey("dbo.VehicleModels", t => t.Model_Id)
                .ForeignKey("dbo.VehicleModelVariants", t => t.ModelVariant_Id)
                .ForeignKey("dbo.VehicleRanges", t => t.Range_Id)
                .ForeignKey("dbo.VehicleSteeringVariants", t => t.Steering_Id)
                .ForeignKey("dbo.VehicleTransmissionVariants", t => t.Transmission_Id)
                .ForeignKey("dbo.VehicleTrimLevelVariants", t => t.TrimLevel_Id)
                .Index(t => t.SortOrder)
                .Index(t => t.IsActive)
                .Index(t => t.Marque_Id)
                .Index(t => t.Range_Id)
                .Index(t => t.Model_Id)
                .Index(t => t.ModelVariant_Id)
                .Index(t => t.Body_Id)
                .Index(t => t.EngineType_Id)
                .Index(t => t.Engine_Id)
                .Index(t => t.Transmission_Id)
                .Index(t => t.Drivetrain_Id)
                .Index(t => t.Steering_Id)
                .Index(t => t.TrimLevel_Id);
            
            CreateTable(
                "dbo.VehicleBodyVariants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.SortOrder)
                .Index(t => t.IsActive);
            
            CreateTable(
                "dbo.VehicleBodyVariantTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body_Id = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Language_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VehicleBodyVariants", t => t.Body_Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .Index(t => t.Body_Id)
                .Index(t => t.Language_Id);
            
            CreateTable(
                "dbo.VehicleDrivetrainVariants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.SortOrder)
                .Index(t => t.IsActive);
            
            CreateTable(
                "dbo.VehicleDrivetrainVariantTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Drivetrain_Id = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Language_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VehicleDrivetrainVariants", t => t.Drivetrain_Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .Index(t => t.Drivetrain_Id)
                .Index(t => t.Language_Id);
            
            CreateTable(
                "dbo.VehicleEngineVariants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.SortOrder)
                .Index(t => t.IsActive);
            
            CreateTable(
                "dbo.VehicleEngineVariantTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Engine_Id = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Language_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VehicleEngineVariants", t => t.Engine_Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .Index(t => t.Engine_Id)
                .Index(t => t.Language_Id);
            
            CreateTable(
                "dbo.VehicleEngineTypeVariants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.SortOrder)
                .Index(t => t.IsActive);
            
            CreateTable(
                "dbo.VehicleEngineTypeVariantTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EngineType_Id = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Language_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VehicleEngineTypeVariants", t => t.EngineType_Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .Index(t => t.EngineType_Id)
                .Index(t => t.Language_Id);
            
            CreateTable(
                "dbo.VehicleMarques",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.SortOrder)
                .Index(t => t.IsActive);
            
            CreateTable(
                "dbo.VehicleMarqueTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Marque_Id = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Language_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .ForeignKey("dbo.VehicleMarques", t => t.Marque_Id)
                .Index(t => t.Marque_Id)
                .Index(t => t.Language_Id);
            
            CreateTable(
                "dbo.VehicleModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.SortOrder)
                .Index(t => t.IsActive);
            
            CreateTable(
                "dbo.VehicleModelTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model_Id = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Language_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .ForeignKey("dbo.VehicleModels", t => t.Model_Id)
                .Index(t => t.Model_Id)
                .Index(t => t.Language_Id);
            
            CreateTable(
                "dbo.VehicleModelVariants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.SortOrder)
                .Index(t => t.IsActive);
            
            CreateTable(
                "dbo.VehicleModelVariantTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Variant_Id = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Language_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .ForeignKey("dbo.VehicleModelVariants", t => t.Variant_Id)
                .Index(t => t.Variant_Id)
                .Index(t => t.Language_Id);
            
            CreateTable(
                "dbo.VehicleRanges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartYear = c.String(maxLength: 4),
                        EndYear = c.String(maxLength: 4),
                        SortOrder = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.SortOrder)
                .Index(t => t.IsActive);
            
            CreateTable(
                "dbo.VehicleRangeTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Range_Id = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Language_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .ForeignKey("dbo.VehicleRanges", t => t.Range_Id)
                .Index(t => t.Range_Id)
                .Index(t => t.Language_Id);
            
            CreateTable(
                "dbo.VehicleSteeringVariants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.SortOrder)
                .Index(t => t.IsActive);
            
            CreateTable(
                "dbo.VehicleSteeringVariantTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Steering_Id = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Language_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .ForeignKey("dbo.VehicleSteeringVariants", t => t.Steering_Id)
                .Index(t => t.Steering_Id)
                .Index(t => t.Language_Id);
            
            CreateTable(
                "dbo.VehicleTransmissionVariants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.SortOrder)
                .Index(t => t.IsActive);
            
            CreateTable(
                "dbo.VehicleTransmissionVariantTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Transmission_Id = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Language_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .ForeignKey("dbo.VehicleTransmissionVariants", t => t.Transmission_Id)
                .Index(t => t.Transmission_Id)
                .Index(t => t.Language_Id);
            
            CreateTable(
                "dbo.VehicleTrimLevelVariants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.SortOrder)
                .Index(t => t.IsActive);
            
            CreateTable(
                "dbo.VehicleTrimLevelVariantTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrimLevel_Id = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Language_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .ForeignKey("dbo.VehicleTrimLevelVariants", t => t.TrimLevel_Id)
                .Index(t => t.TrimLevel_Id)
                .Index(t => t.Language_Id);
            
            CreateTable(
                "dbo.WebOrders",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        OrderNo = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        InternalCustNo = c.String(maxLength: 10),
                        CustomerTaxNo = c.String(maxLength: 25),
                        CustomerOrderRef = c.String(maxLength: 15),
                        CustomerConfirmationRequired = c.Boolean(nullable: false),
                        CustomerConfirmationTimestamp = c.DateTime(),
                        InternalQuoteNo = c.String(maxLength: 5),
                        InternalQuoteDate = c.DateTime(),
                        InternalOrderNo = c.String(maxLength: 5),
                        InternalOrderDate = c.DateTime(),
                        BillingName = c.String(nullable: false, maxLength: 100),
                        BillingAddressLine1 = c.String(nullable: false, maxLength: 100),
                        BillingAddressLine2 = c.String(maxLength: 100),
                        BillingTownCity = c.String(maxLength: 30),
                        BillingCountyState = c.String(maxLength: 30),
                        BillingPostalCode = c.String(maxLength: 10),
                        BillingCountryName = c.String(nullable: false, maxLength: 50),
                        BillingCountryCode = c.String(nullable: false, maxLength: 2),
                        DeliveryName = c.String(nullable: false, maxLength: 100),
                        DeliveryAddressLine1 = c.String(nullable: false, maxLength: 100),
                        DeliveryAddressLine2 = c.String(maxLength: 100),
                        DeliveryTownCity = c.String(maxLength: 30),
                        DeliveryCountyState = c.String(maxLength: 30),
                        DeliveryPostalCode = c.String(maxLength: 10),
                        DeliveryCountryName = c.String(nullable: false, maxLength: 50),
                        DeliveryCountryCode = c.String(nullable: false, maxLength: 2),
                        DeliveryContactNumber = c.String(nullable: false, maxLength: 25),
                        ShippingMethod_Id = c.Int(nullable: false),
                        ShippingMethodName = c.String(nullable: false, maxLength: 50),
                        EstimatedShippingWeightKgs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EstimatedShippingCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShippingCharge = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShippingTaxRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GoodsAtRate1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GoodsTaxRate1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GoodsAtRate2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GoodsTaxRate2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GrandTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Language_Id = c.Int(nullable: false),
                        Branch_Id = c.Int(nullable: false),
                        Customer_Id = c.Guid(nullable: false),
                        CustomerVehicle_Id = c.Int(),
                        PaymentMethod_Code = c.String(nullable: false, maxLength: 2),
                        PaymentCard_Id = c.Int(),
                        Voucher_Id = c.Int(),
                        WebOrderStatus_Id = c.String(nullable: false, maxLength: 1),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.Branch_Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.CustomerVehicles", t => t.CustomerVehicle_Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .ForeignKey("dbo.PaymentCards", t => t.PaymentCard_Id)
                .ForeignKey("dbo.PaymentMethods", t => t.PaymentMethod_Code)
                .ForeignKey("dbo.WebOrderStatus", t => t.WebOrderStatus_Id)
                .ForeignKey("dbo.Vouchers", t => t.Voucher_Id)
                .Index(t => new { t.OrderDate, t.OrderNo }, unique: true, name: "Idx_WebOrder")
                .Index(t => new { t.InternalQuoteDate, t.InternalQuoteNo }, unique: true, name: "Idx_WebOrder_InternalQuote")
                .Index(t => new { t.InternalOrderDate, t.InternalOrderNo }, unique: true, name: "Idx_WebOrder_InternalOrder")
                .Index(t => t.Language_Id)
                .Index(t => t.Branch_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.CustomerVehicle_Id)
                .Index(t => t.PaymentMethod_Code)
                .Index(t => t.PaymentCard_Id)
                .Index(t => t.Voucher_Id)
                .Index(t => t.WebOrderStatus_Id);
            
            CreateTable(
                "dbo.WebOrderItems",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        LineNo = c.Short(nullable: false),
                        PartNumber = c.String(nullable: false, maxLength: 20),
                        PartTitle = c.String(nullable: false, maxLength: 20),
                        DiscountCode = c.String(nullable: false, maxLength: 2),
                        PackedHeightCms = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PackedWidthCms = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PackedDepthCms = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PackedWeightKgs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RetailPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Surcharge = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuantityRequired = c.Int(nullable: false),
                        QuantityAllocated = c.Int(nullable: false),
                        QuantityBackOrdered = c.Int(nullable: false),
                        QuantityPicked = c.Int(nullable: false),
                        QuantityPacked = c.Int(nullable: false),
                        QuantityInvoiced = c.Int(nullable: false),
                        QuantityCredited = c.Int(nullable: false),
                        WebOrder_Id = c.Guid(nullable: false),
                        WebOrderItemStatus_Id = c.String(nullable: false, maxLength: 1),
                        Customer_Id = c.Guid(nullable: false),
                        CustomerLevel_Id = c.String(nullable: false, maxLength: 2),
                        Branch_Id = c.Int(nullable: false),
                        BranchProduct_Id = c.Guid(),
                        Product_Id = c.Int(),
                        TaxRateCategory_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.Branch_Id)
                .ForeignKey("dbo.BranchProducts", t => t.BranchProduct_Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.CustomerTradingLevels", t => t.CustomerLevel_Id)
                .ForeignKey("dbo.WebOrderItemStatus", t => t.WebOrderItemStatus_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .ForeignKey("dbo.TaxRateCategories", t => t.TaxRateCategory_Id)
                .ForeignKey("dbo.WebOrders", t => t.WebOrder_Id)
                .Index(t => t.PartNumber)
                .Index(t => t.WebOrder_Id)
                .Index(t => t.WebOrderItemStatus_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.CustomerLevel_Id)
                .Index(t => t.Branch_Id)
                .Index(t => t.BranchProduct_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.TaxRateCategory_Id);
            
            CreateTable(
                "dbo.BranchProducts",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        RetailPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TradePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ClearancePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvgCostPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Surcharge = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MinStockLevel = c.Int(nullable: false),
                        MaxStockLevel = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Branch_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                        BranchStatus_Code = c.String(nullable: false, maxLength: 1),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.Branch_Id)
                .ForeignKey("dbo.ProductStatus", t => t.BranchStatus_Code)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.RetailPrice)
                .Index(t => t.ClearancePrice)
                .Index(t => t.IsActive)
                .Index(t => new { t.Branch_Id, t.Product_Id }, unique: true, name: "Idx_BranchProduct")
                .Index(t => t.BranchStatus_Code);
            
            CreateTable(
                "dbo.ProductStatus",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 1),
                        Name = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartNumber = c.String(nullable: false, maxLength: 20),
                        BasePartNumber = c.String(nullable: false, maxLength: 20),
                        CommodityCode = c.String(maxLength: 14),
                        ApplicationList = c.String(maxLength: 4000),
                        ItemHeightCms = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ItemWidthCms = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ItemDepthCms = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ItemWeightKgs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PackedHeightCms = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PackedWidthCms = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PackedDepthCms = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PackedWeightKgs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsShipable = c.Boolean(nullable: false),
                        IsShipableByAir = c.Boolean(nullable: false),
                        IsPackable = c.Boolean(nullable: false),
                        IsPackableLoose = c.Boolean(nullable: false),
                        IsRotatable = c.Boolean(nullable: false),
                        IsQualityAssured = c.Boolean(nullable: false),
                        IsWebsiteApproved = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ProductType_Code = c.String(nullable: false, maxLength: 2),
                        CountryOfOrigin_Code = c.String(maxLength: 2),
                        DiscountLevel_Code = c.String(nullable: false, maxLength: 2),
                        TaxRateCategory_Id = c.Int(nullable: false),
                        ComponentStatus_Code = c.String(nullable: false, maxLength: 1),
                        PartStatus_Code = c.String(nullable: false, maxLength: 1),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                        ProductBrand_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductComponentStatus", t => t.ComponentStatus_Code)
                .ForeignKey("dbo.Countries", t => t.CountryOfOrigin_Code)
                .ForeignKey("dbo.DiscountLevels", t => t.DiscountLevel_Code)
                .ForeignKey("dbo.ProductStatus", t => t.PartStatus_Code)
                .ForeignKey("dbo.ProductBrands", t => t.ProductBrand_Id)
                .ForeignKey("dbo.ProductTypes", t => t.ProductType_Code)
                .ForeignKey("dbo.TaxRateCategories", t => t.TaxRateCategory_Id)
                .Index(t => t.PartNumber)
                .Index(t => t.BasePartNumber)
                .Index(t => t.IsShipable)
                .Index(t => t.IsShipableByAir)
                .Index(t => t.IsPackable)
                .Index(t => t.IsPackableLoose)
                .Index(t => t.IsRotatable)
                .Index(t => t.IsQualityAssured)
                .Index(t => t.IsWebsiteApproved)
                .Index(t => t.IsActive)
                .Index(t => t.ProductType_Code)
                .Index(t => t.CountryOfOrigin_Code)
                .Index(t => t.DiscountLevel_Code)
                .Index(t => t.TaxRateCategory_Id)
                .Index(t => t.ComponentStatus_Code)
                .Index(t => t.PartStatus_Code)
                .Index(t => t.ProductBrand_Id);
            
            CreateTable(
                "dbo.ProductAlternatives",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        AlternativeProduct_Id = c.Int(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.AlternativeProduct_Id })
                .ForeignKey("dbo.Products", t => t.AlternativeProduct_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.AlternativeProduct_Id);
            
            CreateTable(
                "dbo.ProductComponentStatus",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 1),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.DiscountLevels",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 2),
                        Retail = c.Decimal(nullable: false, precision: 5, scale: 2),
                        Level1 = c.Decimal(nullable: false, precision: 5, scale: 2),
                        Level2 = c.Decimal(nullable: false, precision: 5, scale: 2),
                        Level3 = c.Decimal(nullable: false, precision: 5, scale: 2),
                        Level4 = c.Decimal(nullable: false, precision: 5, scale: 2),
                        Level5 = c.Decimal(nullable: false, precision: 5, scale: 2),
                        Level6 = c.Decimal(nullable: false, precision: 5, scale: 2),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.ProductDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Filename = c.String(nullable: false, maxLength: 40),
                        Title = c.String(maxLength: 25),
                        SortOrder = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Language_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.SortOrder)
                .Index(t => t.IsActive)
                .Index(t => t.Language_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Filename = c.String(nullable: false, maxLength: 40),
                        SortOrder = c.Int(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Product_Id = c.Int(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.SortOrder)
                .Index(t => t.IsDefault)
                .Index(t => t.IsActive)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.ProductLinks",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        LinkedProduct_Id = c.Int(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.LinkedProduct_Id })
                .ForeignKey("dbo.Products", t => t.LinkedProduct_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.LinkedProduct_Id);
            
            CreateTable(
                "dbo.ProductBrands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        LogoFilename = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 2),
                        Name = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.ProductQuantityBreakDiscountLevels",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        MinMargin = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Retail = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Level2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Level3 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Level4 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Level5 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Level6 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Quantity })
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.IsActive);
            
            CreateTable(
                "dbo.ProductSupersessions",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        ReplacementProduct_Id = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.ReplacementProduct_Id })
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .ForeignKey("dbo.Products", t => t.ReplacementProduct_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.ReplacementProduct_Id);
            
            CreateTable(
                "dbo.TaxRateCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductTexts",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        Language_Id = c.Int(nullable: false),
                        ShortTitle = c.String(nullable: false, maxLength: 20),
                        LongTitle = c.String(maxLength: 100),
                        ShortDescription = c.String(maxLength: 200),
                        LongDescription = c.String(maxLength: 500),
                        Keywords = c.String(maxLength: 200),
                        SalesNotes = c.String(maxLength: 200),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Language_Id })
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.Language_Id)
                .Index(t => t.ShortTitle);
            
            CreateTable(
                "dbo.BranchProductOffers",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 25),
                        OfferPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpiryDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        BranchProduct_Id = c.Guid(nullable: false),
                        ProductImage_Id = c.Int(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BranchProducts", t => t.BranchProduct_Id)
                .ForeignKey("dbo.ProductImages", t => t.ProductImage_Id)
                .Index(t => t.ExpiryDate)
                .Index(t => t.IsActive)
                .Index(t => t.BranchProduct_Id)
                .Index(t => t.ProductImage_Id);
            
            CreateTable(
                "dbo.WebOrderItemStatus",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 1),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WebOrderEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WebOrder_Id = c.Guid(nullable: false),
                        WebOrderEventType_Id = c.String(nullable: false, maxLength: 2),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WebOrderEventTypes", t => t.WebOrderEventType_Id)
                .ForeignKey("dbo.WebOrders", t => t.WebOrder_Id)
                .Index(t => t.WebOrder_Id)
                .Index(t => t.WebOrderEventType_Id);
            
            CreateTable(
                "dbo.WebOrderEventTypes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 2),
                        Name = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WebOrderEventNotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false, maxLength: 1000),
                        Internal = c.Boolean(nullable: false),
                        WebOrderEvent_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WebOrderEvents", t => t.WebOrderEvent_Id)
                .Index(t => t.WebOrderEvent_Id);
            
            CreateTable(
                "dbo.WebOrderStatus",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 1),
                        Name = c.String(nullable: false, maxLength: 50),
                        IsOnGoing = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vouchers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 14),
                        Title = c.String(nullable: false, maxLength: 25),
                        MinSpend = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValidFromUtc = c.DateTime(nullable: false),
                        ValidToUtc = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Branch_Id = c.Int(nullable: false),
                        Customer_Id = c.Guid(),
                        VoucherType_Id = c.String(nullable: false, maxLength: 1),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.Branch_Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.VoucherTypes", t => t.VoucherType_Id)
                .Index(t => t.Code)
                .Index(t => t.ValidToUtc)
                .Index(t => t.IsActive)
                .Index(t => t.Branch_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.VoucherType_Id);
            
            CreateTable(
                "dbo.VoucherTypes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 1),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BranchIntroductions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Branch_Id = c.Int(nullable: false),
                        Intro = c.String(nullable: false),
                        More = c.String(),
                        Language_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.Branch_Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .Index(t => t.Branch_Id)
                .Index(t => t.Language_Id);
            
            CreateTable(
                "dbo.BranchOpeningTimes",
                c => new
                    {
                        Branch_Id = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        OpensUtc = c.Time(nullable: false, precision: 7),
                        ClosesUtc = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => new { t.Branch_Id, t.Day })
                .ForeignKey("dbo.Branches", t => t.Branch_Id)
                .Index(t => t.Branch_Id);
            
            CreateTable(
                "dbo.PackingContainers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        InternalHeightCms = c.Decimal(nullable: false, precision: 6, scale: 2),
                        InternalWidthCms = c.Decimal(nullable: false, precision: 6, scale: 2),
                        InternalDepthCms = c.Decimal(nullable: false, precision: 6, scale: 2),
                        InternalVolumeCm3 = c.Long(nullable: false),
                        ExternalHeightCms = c.Decimal(nullable: false, precision: 6, scale: 2),
                        ExternalWidthCms = c.Decimal(nullable: false, precision: 6, scale: 2),
                        ExternalDepthCms = c.Decimal(nullable: false, precision: 6, scale: 2),
                        ExternalVolumeCm3 = c.Long(nullable: false),
                        MaxWeightKgs = c.Decimal(nullable: false, precision: 8, scale: 3),
                        UnitWeightKgs = c.Decimal(nullable: false, precision: 8, scale: 3),
                        UnitPrice = c.Decimal(nullable: false, precision: 6, scale: 3),
                        IsUpsOnly = c.Boolean(nullable: false),
                        UPS_Package_Ref = c.String(maxLength: 2),
                        SortOrder = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Branch_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.Branch_Id)
                .Index(t => t.SortOrder)
                .Index(t => t.IsActive)
                .Index(t => t.Branch_Id);
            
            CreateTable(
                "dbo.BranchPaymentMethods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Branch_Id = c.Int(nullable: false),
                        PaymentMethod_Code = c.String(nullable: false, maxLength: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.Branch_Id)
                .ForeignKey("dbo.PaymentMethods", t => t.PaymentMethod_Code)
                .Index(t => t.SortOrder, name: "Idx_BranchPaymentMethod_SortOrder")
                .Index(t => t.IsDefault, name: "Idx_BranchPaymentMethod_Default")
                .Index(t => t.IsActive, name: "Idx_BranchPaymentMethod_Active")
                .Index(t => t.Branch_Id)
                .Index(t => t.PaymentMethod_Code);
            
            CreateTable(
                "dbo.BranchTaxRates",
                c => new
                    {
                        Branch_Id = c.Int(nullable: false),
                        TaxRateCategory_Id = c.Int(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.Branch_Id, t.TaxRateCategory_Id })
                .ForeignKey("dbo.Branches", t => t.Branch_Id)
                .ForeignKey("dbo.TaxRateCategories", t => t.TaxRateCategory_Id)
                .Index(t => t.Branch_Id)
                .Index(t => t.TaxRateCategory_Id);
            
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        PartNumber = c.String(nullable: false, maxLength: 20),
                        PartTitle = c.String(nullable: false, maxLength: 20),
                        DiscountCode = c.String(nullable: false, maxLength: 2),
                        RetailPrice = c.Decimal(nullable: false, precision: 8, scale: 2),
                        UnitPrice = c.Decimal(nullable: false, precision: 8, scale: 2),
                        Surcharge = c.Decimal(nullable: false, precision: 8, scale: 2),
                        QuantityRequired = c.Int(nullable: false),
                        ExpiresUtc = c.DateTime(nullable: false),
                        IsExpired = c.Boolean(nullable: false),
                        IsCheckedOut = c.Boolean(nullable: false),
                        Customer_Id = c.Guid(nullable: false),
                        CustomerLevel_Id = c.String(nullable: false, maxLength: 2),
                        Branch_Id = c.Int(nullable: false),
                        BranchProduct_Id = c.Guid(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.Branch_Id)
                .ForeignKey("dbo.BranchProducts", t => t.BranchProduct_Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.CustomerTradingLevels", t => t.CustomerLevel_Id)
                .Index(t => t.PartNumber)
                .Index(t => t.Customer_Id)
                .Index(t => t.CustomerLevel_Id)
                .Index(t => t.Branch_Id)
                .Index(t => t.BranchProduct_Id);
            
            CreateTable(
                "dbo.CatalogueApplications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Model_Id = c.Int(nullable: false),
                        Category_Id = c.Int(nullable: false),
                        Section_Id = c.Int(nullable: false),
                        SubSection_Id = c.Int(nullable: false),
                        Assembly_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CatalogueAssemblies", t => t.Assembly_Id)
                .ForeignKey("dbo.CatalogueCategories", t => t.Category_Id)
                .ForeignKey("dbo.CatalogueModels", t => t.Model_Id)
                .ForeignKey("dbo.CatalogueCategories", t => t.Section_Id)
                .ForeignKey("dbo.CatalogueCategories", t => t.SubSection_Id)
                .Index(t => t.SortOrder)
                .Index(t => t.IsActive)
                .Index(t => t.Model_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Section_Id)
                .Index(t => t.SubSection_Id)
                .Index(t => t.Assembly_Id);
            
            CreateTable(
                "dbo.CatalogueAssemblies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                        Illustration_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CatalogueAssemblyIllustrations", t => t.Illustration_Id)
                .Index(t => t.IsActive)
                .Index(t => t.Illustration_Id);
            
            CreateTable(
                "dbo.CatalogueAssemblyIllustrations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Filename = c.String(nullable: false, maxLength: 40),
                        Filename_Thb = c.String(maxLength: 50),
                        Filename_Sml = c.String(maxLength: 50),
                        Filename_Med = c.String(maxLength: 50),
                        Filename_Lrg = c.String(maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsActive);
            
            CreateTable(
                "dbo.CatalogueAssemblyNodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnnotationRef = c.String(maxLength: 20),
                        SortOrder = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Assembly_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                        Parent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CatalogueAssemblies", t => t.Assembly_Id)
                .ForeignKey("dbo.CatalogueAssemblyNodes", t => t.Parent_Id)
                .Index(t => t.SortOrder)
                .Index(t => t.IsActive)
                .Index(t => t.Assembly_Id)
                .Index(t => t.Parent_Id);
            
            CreateTable(
                "dbo.CatalogueAssemblyNodeProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartNumber = c.String(nullable: false, maxLength: 20),
                        QuantityOfFit = c.String(maxLength: 20),
                        FromBreakPoint = c.String(maxLength: 20),
                        ToBreakPoint = c.String(maxLength: 20),
                        SortOrder = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Node_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                        ProductDetails_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CatalogueAssemblyNodes", t => t.Node_Id)
                .ForeignKey("dbo.Products", t => t.ProductDetails_Id)
                .Index(t => t.PartNumber)
                .Index(t => t.SortOrder)
                .Index(t => t.IsActive)
                .Index(t => t.Node_Id)
                .Index(t => t.ProductDetails_Id);
            
            CreateTable(
                "dbo.CatalogueAssemblyNodeTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Node_Id = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Language_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .ForeignKey("dbo.CatalogueAssemblyNodes", t => t.Node_Id)
                .Index(t => t.Node_Id)
                .Index(t => t.Language_Id);
            
            CreateTable(
                "dbo.CatalogueAssemblyTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Assembly_Id = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Language_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CatalogueAssemblies", t => t.Assembly_Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .Index(t => t.Assembly_Id)
                .Index(t => t.Language_Id);
            
            CreateTable(
                "dbo.CatalogueCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsActive);
            
            CreateTable(
                "dbo.CatalogueCategoryIntroductions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category_Id = c.Int(nullable: false),
                        Intro = c.String(nullable: false),
                        More = c.String(),
                        Language_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CatalogueCategories", t => t.Category_Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Language_Id);
            
            CreateTable(
                "dbo.CatalogueCategoryTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category_Id = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Language_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CatalogueCategories", t => t.Category_Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Language_Id);
            
            CreateTable(
                "dbo.CatalogueModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartYear = c.String(maxLength: 4),
                        EndYear = c.String(maxLength: 4),
                        SortOrder = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Family_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CatalogueFamilies", t => t.Family_Id)
                .Index(t => t.SortOrder)
                .Index(t => t.IsActive)
                .Index(t => t.Family_Id);
            
            CreateTable(
                "dbo.CatalogueFamilies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartYear = c.String(maxLength: 4),
                        EndYear = c.String(maxLength: 4),
                        SortOrder = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.SortOrder)
                .Index(t => t.IsActive);
            
            CreateTable(
                "dbo.CatalogueFamilyTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Family_Id = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Language_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CatalogueFamilies", t => t.Family_Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .Index(t => t.Family_Id)
                .Index(t => t.Language_Id);
            
            CreateTable(
                "dbo.CatalogueModelTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model_Id = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Language_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .ForeignKey("dbo.CatalogueModels", t => t.Model_Id)
                .Index(t => t.Model_Id)
                .Index(t => t.Language_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DistinctProductCount = c.Int(nullable: false),
                        ImageFilename = c.String(maxLength: 50),
                        SortOrder = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CategoryType_Id = c.Int(nullable: false),
                        Parent_Id = c.Int(),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoryTypes", t => t.CategoryType_Id)
                .ForeignKey("dbo.Categories", t => t.Parent_Id)
                .Index(t => t.SortOrder)
                .Index(t => t.IsActive)
                .Index(t => t.CategoryType_Id)
                .Index(t => t.Parent_Id);
            
            CreateTable(
                "dbo.CategoryTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoryIntroductions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category_Id = c.Int(nullable: false),
                        Intro = c.String(nullable: false),
                        More = c.String(),
                        Language_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Language_Id);
            
            CreateTable(
                "dbo.CategoryProducts",
                c => new
                    {
                        Category_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                        QuantityOfFit = c.String(maxLength: 20),
                        FromBreakPoint = c.String(maxLength: 20),
                        ToBreakPoint = c.String(maxLength: 20),
                        IsActive = c.Boolean(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.Product_Id })
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.IsActive);
            
            CreateTable(
                "dbo.CategoryTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category_Id = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Language_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Language_Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        AllowedOrigin = c.String(nullable: false, maxLength: 500),
                        Secret = c.String(nullable: false),
                        RefreshTokenTTL = c.Int(nullable: false),
                        IsSecure = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsActive);
            
            CreateTable(
                "dbo.RefreshTokens",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Client_Id = c.Guid(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                        IssuedTimestampUtc = c.DateTime(nullable: false),
                        ExpiresTimestampUtc = c.DateTime(nullable: false),
                        ProtectedTicket = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        LastLoginTimestampUtc = c.DateTime(),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                        Customer_Id = c.Guid(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ShowEventDateTimes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartsUtc = c.DateTime(nullable: false),
                        EndsUtc = c.DateTime(nullable: false),
                        Event_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShowEvents", t => t.Event_Id)
                .Index(t => t.Event_Id);
            
            CreateTable(
                "dbo.ShowEvents",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 30),
                        Description = c.String(maxLength: 500),
                        EventUrl = c.String(maxLength: 500),
                        Location = c.String(maxLength: 30),
                        MapUrl = c.String(maxLength: 500),
                        ImageFilename = c.String(maxLength: 50),
                        IsAttending = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Branch_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.Branch_Id)
                .Index(t => t.IsActive)
                .Index(t => t.Branch_Id);
            
            CreateTable(
                "dbo.InterfacePhrases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassName = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 200),
                        Context = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InterfacePhraseTranslations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        Language_Id = c.Int(nullable: false),
                        InterfacePhrase_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InterfacePhrases", t => t.InterfacePhrase_Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .Index(t => t.Language_Id)
                .Index(t => t.InterfacePhrase_Id);
            
            CreateTable(
                "dbo.Locales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 5),
                        CurrencyCode = c.String(nullable: false, maxLength: 3),
                        CurrencySymbol = c.String(nullable: false, maxLength: 1),
                        Language_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .Index(t => t.Language_Id);
            
            CreateTable(
                "dbo.PackageManifestItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        PartNumber = c.String(nullable: false, maxLength: 20),
                        Title = c.String(nullable: false, maxLength: 25),
                        PackedHeightCms = c.Decimal(nullable: false, precision: 6, scale: 2),
                        PackedWidthCms = c.Decimal(nullable: false, precision: 6, scale: 2),
                        PackedDepthCms = c.Decimal(nullable: false, precision: 6, scale: 2),
                        PackedWeightKgs = c.Decimal(nullable: false, precision: 8, scale: 3),
                        Product_Id = c.Int(nullable: false),
                        CountryOfOrigin_Code = c.String(maxLength: 2),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                        Package_PackageNo = c.Short(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryOfOrigin_Code)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .ForeignKey("dbo.Packages", t => t.Package_PackageNo)
                .Index(t => t.Product_Id)
                .Index(t => t.CountryOfOrigin_Code)
                .Index(t => t.Package_PackageNo);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        ShippingQuote_Id = c.Guid(nullable: false),
                        PackageNo = c.Short(nullable: false, identity: true),
                        HeightCms = c.Decimal(nullable: false, precision: 6, scale: 2),
                        WidthCms = c.Decimal(nullable: false, precision: 6, scale: 2),
                        DepthCms = c.Decimal(nullable: false, precision: 6, scale: 2),
                        WeightKgs = c.Decimal(nullable: false, precision: 8, scale: 3),
                        VolumetricWeightKgs = c.Decimal(nullable: false, precision: 8, scale: 3),
                        Confidence_Volume = c.Decimal(nullable: false, precision: 5, scale: 2),
                        Confidence_Weight = c.Decimal(nullable: false, precision: 5, scale: 2),
                        PackingContainer_Id = c.Int(),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PackageNo)
                .ForeignKey("dbo.ShippingQuotes", t => t.ShippingQuote_Id)
                .ForeignKey("dbo.PackingContainers", t => t.PackingContainer_Id)
                .Index(t => t.ShippingQuote_Id)
                .Index(t => t.PackingContainer_Id);
            
            CreateTable(
                "dbo.ShippingQuotes",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        RecipientName = c.String(nullable: false, maxLength: 35),
                        RecipientAddressLine1 = c.String(nullable: false, maxLength: 35),
                        RecipientAddressLine2 = c.String(maxLength: 35),
                        RecipientTownCity = c.String(maxLength: 30),
                        RecipientCountyState = c.String(maxLength: 30),
                        RecipientPostalCode = c.String(maxLength: 10),
                        RecipientCountryName = c.String(nullable: false, maxLength: 50),
                        RecipientCountryCode = c.String(nullable: false, maxLength: 2),
                        RecipientPhoneNumber = c.String(maxLength: 20),
                        ServiceReference = c.String(maxLength: 10),
                        ServiceDescription = c.String(nullable: false, maxLength: 35),
                        PackagesCount = c.Short(nullable: false),
                        EstimatedWeightKgs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CostPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Branch_Id = c.Int(nullable: false),
                        Customer_Id = c.Guid(nullable: false),
                        ServiceProvider_Id = c.Int(nullable: false),
                        ShippingMethod_Id = c.Int(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.Branch_Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.ShippingProviders", t => t.ServiceProvider_Id)
                .ForeignKey("dbo.ShippingMethods", t => t.ShippingMethod_Id)
                .Index(t => t.Branch_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.ServiceProvider_Id)
                .Index(t => t.ShippingMethod_Id);
            
            CreateTable(
                "dbo.Tweets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 250),
                        LinkUrl = c.String(maxLength: 250),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VoucherRedemptions",
                c => new
                    {
                        Voucher_Id = c.Int(nullable: false),
                        WebOrder_Id = c.Guid(nullable: false),
                        Customer_Id = c.Guid(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Voucher_Id, t.WebOrder_Id, t.Customer_Id })
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.Vouchers", t => t.Voucher_Id)
                .ForeignKey("dbo.WebOrders", t => t.WebOrder_Id)
                .Index(t => t.Voucher_Id)
                .Index(t => t.WebOrder_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.WishListItems",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        PartNumber = c.String(nullable: false, maxLength: 20),
                        PartTitle = c.String(nullable: false, maxLength: 20),
                        Quantity = c.Int(nullable: false),
                        WishList_Id = c.Guid(nullable: false),
                        Customer_Id = c.Guid(nullable: false),
                        Product_Id = c.Int(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .ForeignKey("dbo.WishLists", t => t.WishList_Id)
                .Index(t => t.WishList_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.WishLists",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        DisplayName = c.String(nullable: false, maxLength: 20),
                        Customer_Id = c.Guid(nullable: false),
                        CreatedByUsername = c.String(nullable: false),
                        UpdatedByUsername = c.String(nullable: false),
                        RowVersion = c.Int(nullable: false),
                        CreatedTimestampUtc = c.DateTime(nullable: false),
                        UpdatedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Packages", "PackingContainer_Id", "dbo.PackingContainers");
            DropForeignKey("dbo.PackageManifestItems", "Package_PackageNo", "dbo.Packages");
            DropForeignKey("dbo.WishListItems", "WishList_Id", "dbo.WishLists");
            DropForeignKey("dbo.WishLists", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.WishListItems", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.WishListItems", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.VoucherRedemptions", "WebOrder_Id", "dbo.WebOrders");
            DropForeignKey("dbo.VoucherRedemptions", "Voucher_Id", "dbo.Vouchers");
            DropForeignKey("dbo.VoucherRedemptions", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Packages", "ShippingQuote_Id", "dbo.ShippingQuotes");
            DropForeignKey("dbo.ShippingQuotes", "ShippingMethod_Id", "dbo.ShippingMethods");
            DropForeignKey("dbo.ShippingQuotes", "ServiceProvider_Id", "dbo.ShippingProviders");
            DropForeignKey("dbo.ShippingQuotes", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.ShippingQuotes", "Branch_Id", "dbo.Branches");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PackageManifestItems", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.PackageManifestItems", "CountryOfOrigin_Code", "dbo.Countries");
            DropForeignKey("dbo.Locales", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.InterfacePhraseTranslations", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.InterfacePhraseTranslations", "InterfacePhrase_Id", "dbo.InterfacePhrases");
            DropForeignKey("dbo.ShowEventDateTimes", "Event_Id", "dbo.ShowEvents");
            DropForeignKey("dbo.ShowEvents", "Branch_Id", "dbo.Branches");
            DropForeignKey("dbo.RefreshTokens", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Customers", "UserAccount_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RefreshTokens", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.CategoryTitles", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.CategoryTitles", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.CategoryProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.CategoryProducts", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Categories", "Parent_Id", "dbo.Categories");
            DropForeignKey("dbo.CategoryIntroductions", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.CategoryIntroductions", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Categories", "CategoryType_Id", "dbo.CategoryTypes");
            DropForeignKey("dbo.CatalogueApplications", "SubSection_Id", "dbo.CatalogueCategories");
            DropForeignKey("dbo.CatalogueApplications", "Section_Id", "dbo.CatalogueCategories");
            DropForeignKey("dbo.CatalogueApplications", "Model_Id", "dbo.CatalogueModels");
            DropForeignKey("dbo.CatalogueModelTitles", "Model_Id", "dbo.CatalogueModels");
            DropForeignKey("dbo.CatalogueModelTitles", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.CatalogueModels", "Family_Id", "dbo.CatalogueFamilies");
            DropForeignKey("dbo.CatalogueFamilyTitles", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.CatalogueFamilyTitles", "Family_Id", "dbo.CatalogueFamilies");
            DropForeignKey("dbo.CatalogueApplications", "Category_Id", "dbo.CatalogueCategories");
            DropForeignKey("dbo.CatalogueCategoryTitles", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.CatalogueCategoryTitles", "Category_Id", "dbo.CatalogueCategories");
            DropForeignKey("dbo.CatalogueCategoryIntroductions", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.CatalogueCategoryIntroductions", "Category_Id", "dbo.CatalogueCategories");
            DropForeignKey("dbo.CatalogueApplications", "Assembly_Id", "dbo.CatalogueAssemblies");
            DropForeignKey("dbo.CatalogueAssemblyTitles", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.CatalogueAssemblyTitles", "Assembly_Id", "dbo.CatalogueAssemblies");
            DropForeignKey("dbo.CatalogueAssemblyNodeTitles", "Node_Id", "dbo.CatalogueAssemblyNodes");
            DropForeignKey("dbo.CatalogueAssemblyNodeTitles", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.CatalogueAssemblyNodeProducts", "ProductDetails_Id", "dbo.Products");
            DropForeignKey("dbo.CatalogueAssemblyNodeProducts", "Node_Id", "dbo.CatalogueAssemblyNodes");
            DropForeignKey("dbo.CatalogueAssemblyNodes", "Parent_Id", "dbo.CatalogueAssemblyNodes");
            DropForeignKey("dbo.CatalogueAssemblyNodes", "Assembly_Id", "dbo.CatalogueAssemblies");
            DropForeignKey("dbo.CatalogueAssemblies", "Illustration_Id", "dbo.CatalogueAssemblyIllustrations");
            DropForeignKey("dbo.CartItems", "CustomerLevel_Id", "dbo.CustomerTradingLevels");
            DropForeignKey("dbo.CartItems", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.CartItems", "BranchProduct_Id", "dbo.BranchProducts");
            DropForeignKey("dbo.CartItems", "Branch_Id", "dbo.Branches");
            DropForeignKey("dbo.Adverts", "Branch_Id", "dbo.Branches");
            DropForeignKey("dbo.BranchTaxRates", "TaxRateCategory_Id", "dbo.TaxRateCategories");
            DropForeignKey("dbo.BranchTaxRates", "Branch_Id", "dbo.Branches");
            DropForeignKey("dbo.BranchPaymentMethods", "PaymentMethod_Code", "dbo.PaymentMethods");
            DropForeignKey("dbo.BranchPaymentMethods", "Branch_Id", "dbo.Branches");
            DropForeignKey("dbo.PackingContainers", "Branch_Id", "dbo.Branches");
            DropForeignKey("dbo.BranchOpeningTimes", "Branch_Id", "dbo.Branches");
            DropForeignKey("dbo.BranchIntroductions", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.BranchIntroductions", "Branch_Id", "dbo.Branches");
            DropForeignKey("dbo.Branches", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.WebOrders", "Voucher_Id", "dbo.Vouchers");
            DropForeignKey("dbo.Vouchers", "VoucherType_Id", "dbo.VoucherTypes");
            DropForeignKey("dbo.Vouchers", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Vouchers", "Branch_Id", "dbo.Branches");
            DropForeignKey("dbo.WebOrders", "WebOrderStatus_Id", "dbo.WebOrderStatus");
            DropForeignKey("dbo.WebOrders", "PaymentMethod_Code", "dbo.PaymentMethods");
            DropForeignKey("dbo.WebOrders", "PaymentCard_Id", "dbo.PaymentCards");
            DropForeignKey("dbo.WebOrderEvents", "WebOrder_Id", "dbo.WebOrders");
            DropForeignKey("dbo.WebOrderEventNotes", "WebOrderEvent_Id", "dbo.WebOrderEvents");
            DropForeignKey("dbo.WebOrderEvents", "WebOrderEventType_Id", "dbo.WebOrderEventTypes");
            DropForeignKey("dbo.WebOrders", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.WebOrderItems", "WebOrder_Id", "dbo.WebOrders");
            DropForeignKey("dbo.WebOrderItems", "TaxRateCategory_Id", "dbo.TaxRateCategories");
            DropForeignKey("dbo.WebOrderItems", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.WebOrderItems", "WebOrderItemStatus_Id", "dbo.WebOrderItemStatus");
            DropForeignKey("dbo.WebOrderItems", "CustomerLevel_Id", "dbo.CustomerTradingLevels");
            DropForeignKey("dbo.WebOrderItems", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.WebOrderItems", "BranchProduct_Id", "dbo.BranchProducts");
            DropForeignKey("dbo.BranchProductOffers", "ProductImage_Id", "dbo.ProductImages");
            DropForeignKey("dbo.BranchProductOffers", "BranchProduct_Id", "dbo.BranchProducts");
            DropForeignKey("dbo.BranchProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductTexts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductTexts", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.Products", "TaxRateCategory_Id", "dbo.TaxRateCategories");
            DropForeignKey("dbo.ProductSupersessions", "ReplacementProduct_Id", "dbo.Products");
            DropForeignKey("dbo.ProductSupersessions", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductQuantityBreakDiscountLevels", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductType_Code", "dbo.ProductTypes");
            DropForeignKey("dbo.Products", "ProductBrand_Id", "dbo.ProductBrands");
            DropForeignKey("dbo.Products", "PartStatus_Code", "dbo.ProductStatus");
            DropForeignKey("dbo.ProductLinks", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductLinks", "LinkedProduct_Id", "dbo.Products");
            DropForeignKey("dbo.ProductImages", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductDocuments", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductDocuments", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.Products", "DiscountLevel_Code", "dbo.DiscountLevels");
            DropForeignKey("dbo.Products", "CountryOfOrigin_Code", "dbo.Countries");
            DropForeignKey("dbo.Products", "ComponentStatus_Code", "dbo.ProductComponentStatus");
            DropForeignKey("dbo.ProductAlternatives", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductAlternatives", "AlternativeProduct_Id", "dbo.Products");
            DropForeignKey("dbo.BranchProducts", "BranchStatus_Code", "dbo.ProductStatus");
            DropForeignKey("dbo.BranchProducts", "Branch_Id", "dbo.Branches");
            DropForeignKey("dbo.WebOrderItems", "Branch_Id", "dbo.Branches");
            DropForeignKey("dbo.WebOrders", "CustomerVehicle_Id", "dbo.CustomerVehicles");
            DropForeignKey("dbo.WebOrders", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.WebOrders", "Branch_Id", "dbo.Branches");
            DropForeignKey("dbo.CustomerVehicles", "Vehicle_Id", "dbo.Vehicles");
            DropForeignKey("dbo.Vehicles", "TrimLevel_Id", "dbo.VehicleTrimLevelVariants");
            DropForeignKey("dbo.VehicleTrimLevelVariantTitles", "TrimLevel_Id", "dbo.VehicleTrimLevelVariants");
            DropForeignKey("dbo.VehicleTrimLevelVariantTitles", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.Vehicles", "Transmission_Id", "dbo.VehicleTransmissionVariants");
            DropForeignKey("dbo.VehicleTransmissionVariantTitles", "Transmission_Id", "dbo.VehicleTransmissionVariants");
            DropForeignKey("dbo.VehicleTransmissionVariantTitles", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.Vehicles", "Steering_Id", "dbo.VehicleSteeringVariants");
            DropForeignKey("dbo.VehicleSteeringVariantTitles", "Steering_Id", "dbo.VehicleSteeringVariants");
            DropForeignKey("dbo.VehicleSteeringVariantTitles", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.Vehicles", "Range_Id", "dbo.VehicleRanges");
            DropForeignKey("dbo.VehicleRangeTitles", "Range_Id", "dbo.VehicleRanges");
            DropForeignKey("dbo.VehicleRangeTitles", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.Vehicles", "ModelVariant_Id", "dbo.VehicleModelVariants");
            DropForeignKey("dbo.VehicleModelVariantTitles", "Variant_Id", "dbo.VehicleModelVariants");
            DropForeignKey("dbo.VehicleModelVariantTitles", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.Vehicles", "Model_Id", "dbo.VehicleModels");
            DropForeignKey("dbo.VehicleModelTitles", "Model_Id", "dbo.VehicleModels");
            DropForeignKey("dbo.VehicleModelTitles", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.Vehicles", "Marque_Id", "dbo.VehicleMarques");
            DropForeignKey("dbo.VehicleMarqueTitles", "Marque_Id", "dbo.VehicleMarques");
            DropForeignKey("dbo.VehicleMarqueTitles", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.Vehicles", "EngineType_Id", "dbo.VehicleEngineTypeVariants");
            DropForeignKey("dbo.VehicleEngineTypeVariantTitles", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.VehicleEngineTypeVariantTitles", "EngineType_Id", "dbo.VehicleEngineTypeVariants");
            DropForeignKey("dbo.Vehicles", "Engine_Id", "dbo.VehicleEngineVariants");
            DropForeignKey("dbo.VehicleEngineVariantTitles", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.VehicleEngineVariantTitles", "Engine_Id", "dbo.VehicleEngineVariants");
            DropForeignKey("dbo.Vehicles", "Drivetrain_Id", "dbo.VehicleDrivetrainVariants");
            DropForeignKey("dbo.VehicleDrivetrainVariantTitles", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.VehicleDrivetrainVariantTitles", "Drivetrain_Id", "dbo.VehicleDrivetrainVariants");
            DropForeignKey("dbo.Vehicles", "Body_Id", "dbo.VehicleBodyVariants");
            DropForeignKey("dbo.VehicleBodyVariantTitles", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.VehicleBodyVariantTitles", "Body_Id", "dbo.VehicleBodyVariants");
            DropForeignKey("dbo.CustomerVehicles", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Customers", "TradingTerms_Code", "dbo.CustomerTradingLevels");
            DropForeignKey("dbo.Customers", "ShippingMethod_Id", "dbo.ShippingMethods");
            DropForeignKey("dbo.ShippingMethods", "ShippingProvider_Id", "dbo.ShippingProviders");
            DropForeignKey("dbo.ShippingMethods", "ShippingCoverageLevel_Id", "dbo.ShippingCoverageLevels");
            DropForeignKey("dbo.ShippingMethods", "Branch_Id", "dbo.Branches");
            DropForeignKey("dbo.CustomerShippingAddresses", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.CustomerShippingAddresses", "Country_Code", "dbo.Countries");
            DropForeignKey("dbo.CustomerPhoneNumbers", "PhoneNumberType_Id", "dbo.PhoneNumberTypes");
            DropForeignKey("dbo.CustomerPhoneNumbers", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Customers", "PaymentMethod_Code", "dbo.PaymentMethods");
            DropForeignKey("dbo.PaymentMethodTitles", "PaymentMethod_Code", "dbo.PaymentMethods");
            DropForeignKey("dbo.PaymentMethodTitles", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.PaymentCards", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Customers", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.CustomerEmailAddresses", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Customers", "Country_Code", "dbo.Countries");
            DropForeignKey("dbo.Customers", "Branch_Id", "dbo.Branches");
            DropForeignKey("dbo.Customers", "CustomerAccountType_Code", "dbo.CustomerAccountTypes");
            DropForeignKey("dbo.Branches", "Currency_Code", "dbo.Currencies");
            DropForeignKey("dbo.Branches", "Country_Code", "dbo.Countries");
            DropForeignKey("dbo.CountryTitles", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.CountryTitles", "Country_Code", "dbo.Countries");
            DropForeignKey("dbo.Adverts", "AdvertType_Id", "dbo.AdvertTypes");
            DropIndex("dbo.WishLists", new[] { "Customer_Id" });
            DropIndex("dbo.WishListItems", new[] { "Product_Id" });
            DropIndex("dbo.WishListItems", new[] { "Customer_Id" });
            DropIndex("dbo.WishListItems", new[] { "WishList_Id" });
            DropIndex("dbo.VoucherRedemptions", new[] { "Customer_Id" });
            DropIndex("dbo.VoucherRedemptions", new[] { "WebOrder_Id" });
            DropIndex("dbo.VoucherRedemptions", new[] { "Voucher_Id" });
            DropIndex("dbo.ShippingQuotes", new[] { "ShippingMethod_Id" });
            DropIndex("dbo.ShippingQuotes", new[] { "ServiceProvider_Id" });
            DropIndex("dbo.ShippingQuotes", new[] { "Customer_Id" });
            DropIndex("dbo.ShippingQuotes", new[] { "Branch_Id" });
            DropIndex("dbo.Packages", new[] { "PackingContainer_Id" });
            DropIndex("dbo.Packages", new[] { "ShippingQuote_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PackageManifestItems", new[] { "Package_PackageNo" });
            DropIndex("dbo.PackageManifestItems", new[] { "CountryOfOrigin_Code" });
            DropIndex("dbo.PackageManifestItems", new[] { "Product_Id" });
            DropIndex("dbo.Locales", new[] { "Language_Id" });
            DropIndex("dbo.InterfacePhraseTranslations", new[] { "InterfacePhrase_Id" });
            DropIndex("dbo.InterfacePhraseTranslations", new[] { "Language_Id" });
            DropIndex("dbo.ShowEvents", new[] { "Branch_Id" });
            DropIndex("dbo.ShowEvents", new[] { "IsActive" });
            DropIndex("dbo.ShowEventDateTimes", new[] { "Event_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "Customer_Id" });
            DropIndex("dbo.RefreshTokens", new[] { "User_Id" });
            DropIndex("dbo.RefreshTokens", new[] { "Client_Id" });
            DropIndex("dbo.Clients", new[] { "IsActive" });
            DropIndex("dbo.CategoryTitles", new[] { "Language_Id" });
            DropIndex("dbo.CategoryTitles", new[] { "Category_Id" });
            DropIndex("dbo.CategoryProducts", new[] { "IsActive" });
            DropIndex("dbo.CategoryProducts", new[] { "Product_Id" });
            DropIndex("dbo.CategoryProducts", new[] { "Category_Id" });
            DropIndex("dbo.CategoryIntroductions", new[] { "Language_Id" });
            DropIndex("dbo.CategoryIntroductions", new[] { "Category_Id" });
            DropIndex("dbo.Categories", new[] { "Parent_Id" });
            DropIndex("dbo.Categories", new[] { "CategoryType_Id" });
            DropIndex("dbo.Categories", new[] { "IsActive" });
            DropIndex("dbo.Categories", new[] { "SortOrder" });
            DropIndex("dbo.CatalogueModelTitles", new[] { "Language_Id" });
            DropIndex("dbo.CatalogueModelTitles", new[] { "Model_Id" });
            DropIndex("dbo.CatalogueFamilyTitles", new[] { "Language_Id" });
            DropIndex("dbo.CatalogueFamilyTitles", new[] { "Family_Id" });
            DropIndex("dbo.CatalogueFamilies", new[] { "IsActive" });
            DropIndex("dbo.CatalogueFamilies", new[] { "SortOrder" });
            DropIndex("dbo.CatalogueModels", new[] { "Family_Id" });
            DropIndex("dbo.CatalogueModels", new[] { "IsActive" });
            DropIndex("dbo.CatalogueModels", new[] { "SortOrder" });
            DropIndex("dbo.CatalogueCategoryTitles", new[] { "Language_Id" });
            DropIndex("dbo.CatalogueCategoryTitles", new[] { "Category_Id" });
            DropIndex("dbo.CatalogueCategoryIntroductions", new[] { "Language_Id" });
            DropIndex("dbo.CatalogueCategoryIntroductions", new[] { "Category_Id" });
            DropIndex("dbo.CatalogueCategories", new[] { "IsActive" });
            DropIndex("dbo.CatalogueAssemblyTitles", new[] { "Language_Id" });
            DropIndex("dbo.CatalogueAssemblyTitles", new[] { "Assembly_Id" });
            DropIndex("dbo.CatalogueAssemblyNodeTitles", new[] { "Language_Id" });
            DropIndex("dbo.CatalogueAssemblyNodeTitles", new[] { "Node_Id" });
            DropIndex("dbo.CatalogueAssemblyNodeProducts", new[] { "ProductDetails_Id" });
            DropIndex("dbo.CatalogueAssemblyNodeProducts", new[] { "Node_Id" });
            DropIndex("dbo.CatalogueAssemblyNodeProducts", new[] { "IsActive" });
            DropIndex("dbo.CatalogueAssemblyNodeProducts", new[] { "SortOrder" });
            DropIndex("dbo.CatalogueAssemblyNodeProducts", new[] { "PartNumber" });
            DropIndex("dbo.CatalogueAssemblyNodes", new[] { "Parent_Id" });
            DropIndex("dbo.CatalogueAssemblyNodes", new[] { "Assembly_Id" });
            DropIndex("dbo.CatalogueAssemblyNodes", new[] { "IsActive" });
            DropIndex("dbo.CatalogueAssemblyNodes", new[] { "SortOrder" });
            DropIndex("dbo.CatalogueAssemblyIllustrations", new[] { "IsActive" });
            DropIndex("dbo.CatalogueAssemblies", new[] { "Illustration_Id" });
            DropIndex("dbo.CatalogueAssemblies", new[] { "IsActive" });
            DropIndex("dbo.CatalogueApplications", new[] { "Assembly_Id" });
            DropIndex("dbo.CatalogueApplications", new[] { "SubSection_Id" });
            DropIndex("dbo.CatalogueApplications", new[] { "Section_Id" });
            DropIndex("dbo.CatalogueApplications", new[] { "Category_Id" });
            DropIndex("dbo.CatalogueApplications", new[] { "Model_Id" });
            DropIndex("dbo.CatalogueApplications", new[] { "IsActive" });
            DropIndex("dbo.CatalogueApplications", new[] { "SortOrder" });
            DropIndex("dbo.CartItems", new[] { "BranchProduct_Id" });
            DropIndex("dbo.CartItems", new[] { "Branch_Id" });
            DropIndex("dbo.CartItems", new[] { "CustomerLevel_Id" });
            DropIndex("dbo.CartItems", new[] { "Customer_Id" });
            DropIndex("dbo.CartItems", new[] { "PartNumber" });
            DropIndex("dbo.BranchTaxRates", new[] { "TaxRateCategory_Id" });
            DropIndex("dbo.BranchTaxRates", new[] { "Branch_Id" });
            DropIndex("dbo.BranchPaymentMethods", new[] { "PaymentMethod_Code" });
            DropIndex("dbo.BranchPaymentMethods", new[] { "Branch_Id" });
            DropIndex("dbo.BranchPaymentMethods", "Idx_BranchPaymentMethod_Active");
            DropIndex("dbo.BranchPaymentMethods", "Idx_BranchPaymentMethod_Default");
            DropIndex("dbo.BranchPaymentMethods", "Idx_BranchPaymentMethod_SortOrder");
            DropIndex("dbo.PackingContainers", new[] { "Branch_Id" });
            DropIndex("dbo.PackingContainers", new[] { "IsActive" });
            DropIndex("dbo.PackingContainers", new[] { "SortOrder" });
            DropIndex("dbo.BranchOpeningTimes", new[] { "Branch_Id" });
            DropIndex("dbo.BranchIntroductions", new[] { "Language_Id" });
            DropIndex("dbo.BranchIntroductions", new[] { "Branch_Id" });
            DropIndex("dbo.Vouchers", new[] { "VoucherType_Id" });
            DropIndex("dbo.Vouchers", new[] { "Customer_Id" });
            DropIndex("dbo.Vouchers", new[] { "Branch_Id" });
            DropIndex("dbo.Vouchers", new[] { "IsActive" });
            DropIndex("dbo.Vouchers", new[] { "ValidToUtc" });
            DropIndex("dbo.Vouchers", new[] { "Code" });
            DropIndex("dbo.WebOrderEventNotes", new[] { "WebOrderEvent_Id" });
            DropIndex("dbo.WebOrderEvents", new[] { "WebOrderEventType_Id" });
            DropIndex("dbo.WebOrderEvents", new[] { "WebOrder_Id" });
            DropIndex("dbo.BranchProductOffers", new[] { "ProductImage_Id" });
            DropIndex("dbo.BranchProductOffers", new[] { "BranchProduct_Id" });
            DropIndex("dbo.BranchProductOffers", new[] { "IsActive" });
            DropIndex("dbo.BranchProductOffers", new[] { "ExpiryDate" });
            DropIndex("dbo.ProductTexts", new[] { "ShortTitle" });
            DropIndex("dbo.ProductTexts", new[] { "Language_Id" });
            DropIndex("dbo.ProductTexts", new[] { "Product_Id" });
            DropIndex("dbo.ProductSupersessions", new[] { "ReplacementProduct_Id" });
            DropIndex("dbo.ProductSupersessions", new[] { "Product_Id" });
            DropIndex("dbo.ProductQuantityBreakDiscountLevels", new[] { "IsActive" });
            DropIndex("dbo.ProductQuantityBreakDiscountLevels", new[] { "Product_Id" });
            DropIndex("dbo.ProductLinks", new[] { "LinkedProduct_Id" });
            DropIndex("dbo.ProductLinks", new[] { "Product_Id" });
            DropIndex("dbo.ProductImages", new[] { "Product_Id" });
            DropIndex("dbo.ProductImages", new[] { "IsActive" });
            DropIndex("dbo.ProductImages", new[] { "IsDefault" });
            DropIndex("dbo.ProductImages", new[] { "SortOrder" });
            DropIndex("dbo.ProductDocuments", new[] { "Product_Id" });
            DropIndex("dbo.ProductDocuments", new[] { "Language_Id" });
            DropIndex("dbo.ProductDocuments", new[] { "IsActive" });
            DropIndex("dbo.ProductDocuments", new[] { "SortOrder" });
            DropIndex("dbo.ProductAlternatives", new[] { "AlternativeProduct_Id" });
            DropIndex("dbo.ProductAlternatives", new[] { "Product_Id" });
            DropIndex("dbo.Products", new[] { "ProductBrand_Id" });
            DropIndex("dbo.Products", new[] { "PartStatus_Code" });
            DropIndex("dbo.Products", new[] { "ComponentStatus_Code" });
            DropIndex("dbo.Products", new[] { "TaxRateCategory_Id" });
            DropIndex("dbo.Products", new[] { "DiscountLevel_Code" });
            DropIndex("dbo.Products", new[] { "CountryOfOrigin_Code" });
            DropIndex("dbo.Products", new[] { "ProductType_Code" });
            DropIndex("dbo.Products", new[] { "IsActive" });
            DropIndex("dbo.Products", new[] { "IsWebsiteApproved" });
            DropIndex("dbo.Products", new[] { "IsQualityAssured" });
            DropIndex("dbo.Products", new[] { "IsRotatable" });
            DropIndex("dbo.Products", new[] { "IsPackableLoose" });
            DropIndex("dbo.Products", new[] { "IsPackable" });
            DropIndex("dbo.Products", new[] { "IsShipableByAir" });
            DropIndex("dbo.Products", new[] { "IsShipable" });
            DropIndex("dbo.Products", new[] { "BasePartNumber" });
            DropIndex("dbo.Products", new[] { "PartNumber" });
            DropIndex("dbo.BranchProducts", new[] { "BranchStatus_Code" });
            DropIndex("dbo.BranchProducts", "Idx_BranchProduct");
            DropIndex("dbo.BranchProducts", new[] { "IsActive" });
            DropIndex("dbo.BranchProducts", new[] { "ClearancePrice" });
            DropIndex("dbo.BranchProducts", new[] { "RetailPrice" });
            DropIndex("dbo.WebOrderItems", new[] { "TaxRateCategory_Id" });
            DropIndex("dbo.WebOrderItems", new[] { "Product_Id" });
            DropIndex("dbo.WebOrderItems", new[] { "BranchProduct_Id" });
            DropIndex("dbo.WebOrderItems", new[] { "Branch_Id" });
            DropIndex("dbo.WebOrderItems", new[] { "CustomerLevel_Id" });
            DropIndex("dbo.WebOrderItems", new[] { "Customer_Id" });
            DropIndex("dbo.WebOrderItems", new[] { "WebOrderItemStatus_Id" });
            DropIndex("dbo.WebOrderItems", new[] { "WebOrder_Id" });
            DropIndex("dbo.WebOrderItems", new[] { "PartNumber" });
            DropIndex("dbo.WebOrders", new[] { "WebOrderStatus_Id" });
            DropIndex("dbo.WebOrders", new[] { "Voucher_Id" });
            DropIndex("dbo.WebOrders", new[] { "PaymentCard_Id" });
            DropIndex("dbo.WebOrders", new[] { "PaymentMethod_Code" });
            DropIndex("dbo.WebOrders", new[] { "CustomerVehicle_Id" });
            DropIndex("dbo.WebOrders", new[] { "Customer_Id" });
            DropIndex("dbo.WebOrders", new[] { "Branch_Id" });
            DropIndex("dbo.WebOrders", new[] { "Language_Id" });
            DropIndex("dbo.WebOrders", "Idx_WebOrder_InternalOrder");
            DropIndex("dbo.WebOrders", "Idx_WebOrder_InternalQuote");
            DropIndex("dbo.WebOrders", "Idx_WebOrder");
            DropIndex("dbo.VehicleTrimLevelVariantTitles", new[] { "Language_Id" });
            DropIndex("dbo.VehicleTrimLevelVariantTitles", new[] { "TrimLevel_Id" });
            DropIndex("dbo.VehicleTrimLevelVariants", new[] { "IsActive" });
            DropIndex("dbo.VehicleTrimLevelVariants", new[] { "SortOrder" });
            DropIndex("dbo.VehicleTransmissionVariantTitles", new[] { "Language_Id" });
            DropIndex("dbo.VehicleTransmissionVariantTitles", new[] { "Transmission_Id" });
            DropIndex("dbo.VehicleTransmissionVariants", new[] { "IsActive" });
            DropIndex("dbo.VehicleTransmissionVariants", new[] { "SortOrder" });
            DropIndex("dbo.VehicleSteeringVariantTitles", new[] { "Language_Id" });
            DropIndex("dbo.VehicleSteeringVariantTitles", new[] { "Steering_Id" });
            DropIndex("dbo.VehicleSteeringVariants", new[] { "IsActive" });
            DropIndex("dbo.VehicleSteeringVariants", new[] { "SortOrder" });
            DropIndex("dbo.VehicleRangeTitles", new[] { "Language_Id" });
            DropIndex("dbo.VehicleRangeTitles", new[] { "Range_Id" });
            DropIndex("dbo.VehicleRanges", new[] { "IsActive" });
            DropIndex("dbo.VehicleRanges", new[] { "SortOrder" });
            DropIndex("dbo.VehicleModelVariantTitles", new[] { "Language_Id" });
            DropIndex("dbo.VehicleModelVariantTitles", new[] { "Variant_Id" });
            DropIndex("dbo.VehicleModelVariants", new[] { "IsActive" });
            DropIndex("dbo.VehicleModelVariants", new[] { "SortOrder" });
            DropIndex("dbo.VehicleModelTitles", new[] { "Language_Id" });
            DropIndex("dbo.VehicleModelTitles", new[] { "Model_Id" });
            DropIndex("dbo.VehicleModels", new[] { "IsActive" });
            DropIndex("dbo.VehicleModels", new[] { "SortOrder" });
            DropIndex("dbo.VehicleMarqueTitles", new[] { "Language_Id" });
            DropIndex("dbo.VehicleMarqueTitles", new[] { "Marque_Id" });
            DropIndex("dbo.VehicleMarques", new[] { "IsActive" });
            DropIndex("dbo.VehicleMarques", new[] { "SortOrder" });
            DropIndex("dbo.VehicleEngineTypeVariantTitles", new[] { "Language_Id" });
            DropIndex("dbo.VehicleEngineTypeVariantTitles", new[] { "EngineType_Id" });
            DropIndex("dbo.VehicleEngineTypeVariants", new[] { "IsActive" });
            DropIndex("dbo.VehicleEngineTypeVariants", new[] { "SortOrder" });
            DropIndex("dbo.VehicleEngineVariantTitles", new[] { "Language_Id" });
            DropIndex("dbo.VehicleEngineVariantTitles", new[] { "Engine_Id" });
            DropIndex("dbo.VehicleEngineVariants", new[] { "IsActive" });
            DropIndex("dbo.VehicleEngineVariants", new[] { "SortOrder" });
            DropIndex("dbo.VehicleDrivetrainVariantTitles", new[] { "Language_Id" });
            DropIndex("dbo.VehicleDrivetrainVariantTitles", new[] { "Drivetrain_Id" });
            DropIndex("dbo.VehicleDrivetrainVariants", new[] { "IsActive" });
            DropIndex("dbo.VehicleDrivetrainVariants", new[] { "SortOrder" });
            DropIndex("dbo.VehicleBodyVariantTitles", new[] { "Language_Id" });
            DropIndex("dbo.VehicleBodyVariantTitles", new[] { "Body_Id" });
            DropIndex("dbo.VehicleBodyVariants", new[] { "IsActive" });
            DropIndex("dbo.VehicleBodyVariants", new[] { "SortOrder" });
            DropIndex("dbo.Vehicles", new[] { "TrimLevel_Id" });
            DropIndex("dbo.Vehicles", new[] { "Steering_Id" });
            DropIndex("dbo.Vehicles", new[] { "Drivetrain_Id" });
            DropIndex("dbo.Vehicles", new[] { "Transmission_Id" });
            DropIndex("dbo.Vehicles", new[] { "Engine_Id" });
            DropIndex("dbo.Vehicles", new[] { "EngineType_Id" });
            DropIndex("dbo.Vehicles", new[] { "Body_Id" });
            DropIndex("dbo.Vehicles", new[] { "ModelVariant_Id" });
            DropIndex("dbo.Vehicles", new[] { "Model_Id" });
            DropIndex("dbo.Vehicles", new[] { "Range_Id" });
            DropIndex("dbo.Vehicles", new[] { "Marque_Id" });
            DropIndex("dbo.Vehicles", new[] { "IsActive" });
            DropIndex("dbo.Vehicles", new[] { "SortOrder" });
            DropIndex("dbo.CustomerVehicles", new[] { "Vehicle_Id" });
            DropIndex("dbo.CustomerVehicles", new[] { "Customer_Id" });
            DropIndex("dbo.CustomerVehicles", new[] { "IsActive" });
            DropIndex("dbo.CustomerVehicles", new[] { "SortOrder" });
            DropIndex("dbo.CustomerVehicles", new[] { "IsDefault" });
            DropIndex("dbo.ShippingProviders", new[] { "IsActive" });
            DropIndex("dbo.ShippingMethods", new[] { "ShippingCoverageLevel_Id" });
            DropIndex("dbo.ShippingMethods", new[] { "ShippingProvider_Id" });
            DropIndex("dbo.ShippingMethods", new[] { "Branch_Id" });
            DropIndex("dbo.ShippingMethods", new[] { "IsActive" });
            DropIndex("dbo.ShippingMethods", new[] { "SortOrder" });
            DropIndex("dbo.CustomerShippingAddresses", new[] { "Country_Code" });
            DropIndex("dbo.CustomerShippingAddresses", new[] { "IsDefault" });
            DropIndex("dbo.CustomerShippingAddresses", new[] { "Customer_Id" });
            DropIndex("dbo.PhoneNumberTypes", new[] { "IsDefault" });
            DropIndex("dbo.CustomerPhoneNumbers", new[] { "Customer_Id" });
            DropIndex("dbo.CustomerPhoneNumbers", new[] { "PhoneNumberType_Id" });
            DropIndex("dbo.CustomerPhoneNumbers", new[] { "IsDefault" });
            DropIndex("dbo.PaymentMethodTitles", new[] { "Language_Id" });
            DropIndex("dbo.PaymentMethodTitles", new[] { "PaymentMethod_Code" });
            DropIndex("dbo.PaymentMethods", "Idx_PaymentMethod_Active");
            DropIndex("dbo.PaymentCards", new[] { "Customer_Id" });
            DropIndex("dbo.PaymentCards", new[] { "IsDefault" });
            DropIndex("dbo.CustomerEmailAddresses", new[] { "IsDefault" });
            DropIndex("dbo.CustomerEmailAddresses", new[] { "Address" });
            DropIndex("dbo.CustomerEmailAddresses", new[] { "Customer_Id" });
            DropIndex("dbo.Customers", new[] { "UserAccount_Id" });
            DropIndex("dbo.Customers", new[] { "Country_Code" });
            DropIndex("dbo.Customers", new[] { "TradingTerms_Code" });
            DropIndex("dbo.Customers", new[] { "ShippingMethod_Id" });
            DropIndex("dbo.Customers", new[] { "PaymentMethod_Code" });
            DropIndex("dbo.Customers", new[] { "Language_Id" });
            DropIndex("dbo.Customers", new[] { "Branch_Id" });
            DropIndex("dbo.Customers", new[] { "CustomerAccountType_Code" });
            DropIndex("dbo.Customers", new[] { "IsActive" });
            DropIndex("dbo.Customers", new[] { "IsMarketingSubscriber" });
            DropIndex("dbo.Customers", new[] { "CompanyName" });
            DropIndex("dbo.Customers", new[] { "LastName" });
            DropIndex("dbo.Customers", new[] { "AccountName" });
            DropIndex("dbo.Customers", new[] { "AccountNumber" });
            DropIndex("dbo.Languages", new[] { "Code" });
            DropIndex("dbo.CountryTitles", new[] { "Language_Id" });
            DropIndex("dbo.CountryTitles", new[] { "Country_Code" });
            DropIndex("dbo.Branches", new[] { "Country_Code" });
            DropIndex("dbo.Branches", new[] { "Language_Id" });
            DropIndex("dbo.Branches", new[] { "Currency_Code" });
            DropIndex("dbo.Branches", new[] { "SortOrder" });
            DropIndex("dbo.Branches", new[] { "SiteCode" });
            DropIndex("dbo.Branches", new[] { "BranchCode" });
            DropIndex("dbo.Adverts", new[] { "AdvertType_Id" });
            DropIndex("dbo.Adverts", new[] { "Branch_Id" });
            DropIndex("dbo.Adverts", new[] { "IsActive" });
            DropIndex("dbo.Adverts", new[] { "IsPriority" });
            DropIndex("dbo.Adverts", new[] { "ExpiresUtc" });
            DropTable("dbo.WishLists");
            DropTable("dbo.WishListItems");
            DropTable("dbo.VoucherRedemptions");
            DropTable("dbo.Tweets");
            DropTable("dbo.ShippingQuotes");
            DropTable("dbo.Packages");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PackageManifestItems");
            DropTable("dbo.Locales");
            DropTable("dbo.InterfacePhraseTranslations");
            DropTable("dbo.InterfacePhrases");
            DropTable("dbo.ShowEvents");
            DropTable("dbo.ShowEventDateTimes");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.RefreshTokens");
            DropTable("dbo.Clients");
            DropTable("dbo.CategoryTitles");
            DropTable("dbo.CategoryProducts");
            DropTable("dbo.CategoryIntroductions");
            DropTable("dbo.CategoryTypes");
            DropTable("dbo.Categories");
            DropTable("dbo.CatalogueModelTitles");
            DropTable("dbo.CatalogueFamilyTitles");
            DropTable("dbo.CatalogueFamilies");
            DropTable("dbo.CatalogueModels");
            DropTable("dbo.CatalogueCategoryTitles");
            DropTable("dbo.CatalogueCategoryIntroductions");
            DropTable("dbo.CatalogueCategories");
            DropTable("dbo.CatalogueAssemblyTitles");
            DropTable("dbo.CatalogueAssemblyNodeTitles");
            DropTable("dbo.CatalogueAssemblyNodeProducts");
            DropTable("dbo.CatalogueAssemblyNodes");
            DropTable("dbo.CatalogueAssemblyIllustrations");
            DropTable("dbo.CatalogueAssemblies");
            DropTable("dbo.CatalogueApplications");
            DropTable("dbo.CartItems");
            DropTable("dbo.BranchTaxRates");
            DropTable("dbo.BranchPaymentMethods");
            DropTable("dbo.PackingContainers");
            DropTable("dbo.BranchOpeningTimes");
            DropTable("dbo.BranchIntroductions");
            DropTable("dbo.VoucherTypes");
            DropTable("dbo.Vouchers");
            DropTable("dbo.WebOrderStatus");
            DropTable("dbo.WebOrderEventNotes");
            DropTable("dbo.WebOrderEventTypes");
            DropTable("dbo.WebOrderEvents");
            DropTable("dbo.WebOrderItemStatus");
            DropTable("dbo.BranchProductOffers");
            DropTable("dbo.ProductTexts");
            DropTable("dbo.TaxRateCategories");
            DropTable("dbo.ProductSupersessions");
            DropTable("dbo.ProductQuantityBreakDiscountLevels");
            DropTable("dbo.ProductTypes");
            DropTable("dbo.ProductBrands");
            DropTable("dbo.ProductLinks");
            DropTable("dbo.ProductImages");
            DropTable("dbo.ProductDocuments");
            DropTable("dbo.DiscountLevels");
            DropTable("dbo.ProductComponentStatus");
            DropTable("dbo.ProductAlternatives");
            DropTable("dbo.Products");
            DropTable("dbo.ProductStatus");
            DropTable("dbo.BranchProducts");
            DropTable("dbo.WebOrderItems");
            DropTable("dbo.WebOrders");
            DropTable("dbo.VehicleTrimLevelVariantTitles");
            DropTable("dbo.VehicleTrimLevelVariants");
            DropTable("dbo.VehicleTransmissionVariantTitles");
            DropTable("dbo.VehicleTransmissionVariants");
            DropTable("dbo.VehicleSteeringVariantTitles");
            DropTable("dbo.VehicleSteeringVariants");
            DropTable("dbo.VehicleRangeTitles");
            DropTable("dbo.VehicleRanges");
            DropTable("dbo.VehicleModelVariantTitles");
            DropTable("dbo.VehicleModelVariants");
            DropTable("dbo.VehicleModelTitles");
            DropTable("dbo.VehicleModels");
            DropTable("dbo.VehicleMarqueTitles");
            DropTable("dbo.VehicleMarques");
            DropTable("dbo.VehicleEngineTypeVariantTitles");
            DropTable("dbo.VehicleEngineTypeVariants");
            DropTable("dbo.VehicleEngineVariantTitles");
            DropTable("dbo.VehicleEngineVariants");
            DropTable("dbo.VehicleDrivetrainVariantTitles");
            DropTable("dbo.VehicleDrivetrainVariants");
            DropTable("dbo.VehicleBodyVariantTitles");
            DropTable("dbo.VehicleBodyVariants");
            DropTable("dbo.Vehicles");
            DropTable("dbo.CustomerVehicles");
            DropTable("dbo.CustomerTradingLevels");
            DropTable("dbo.ShippingProviders");
            DropTable("dbo.ShippingCoverageLevels");
            DropTable("dbo.ShippingMethods");
            DropTable("dbo.CustomerShippingAddresses");
            DropTable("dbo.PhoneNumberTypes");
            DropTable("dbo.CustomerPhoneNumbers");
            DropTable("dbo.PaymentMethodTitles");
            DropTable("dbo.PaymentMethods");
            DropTable("dbo.PaymentCards");
            DropTable("dbo.CustomerEmailAddresses");
            DropTable("dbo.CustomerAccountTypes");
            DropTable("dbo.Customers");
            DropTable("dbo.Currencies");
            DropTable("dbo.Languages");
            DropTable("dbo.CountryTitles");
            DropTable("dbo.Countries");
            DropTable("dbo.Branches");
            DropTable("dbo.AdvertTypes");
            DropTable("dbo.Adverts");
        }
    }
}
