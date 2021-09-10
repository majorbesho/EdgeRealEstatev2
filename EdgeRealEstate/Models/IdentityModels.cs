using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using EdgeRealEstate.Controllers;
using EdgeRealEstate.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EdgeRealEstate.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {

            return new ApplicationDbContext();
        }
        public DbSet<LKPaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<LKCountry> LKCountries { get; set; }
        public virtual DbSet<FlatType> FlatTypes { get; set; }
        public virtual DbSet<Buildings> Buildingses { get; set; }
        public virtual DbSet<LKAccount> LKAccounts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmloyeeTarget> EmloyeeTargets { get; set; }
        public virtual DbSet<EmployeeAttach> EmployeeAttaches { get; set; }
        public virtual DbSet<LKNationality> LKNationalities { get; set; }
        public virtual DbSet<LKBranch> LKBranchs { get; set; }
        public virtual DbSet<LKTitle> LKTitles { get; set; }
        public virtual DbSet<CustPaperPayment> CustPaperPayments { get; set; }
        public virtual DbSet<CustPaperReceipt> CustPaperReceipts { get; set; }
        public virtual DbSet<Contributor> Contributor { get; set; }
        public virtual DbSet<ContPaperPayment> ContPaperPayments { get; set; }
        public virtual DbSet<ContPaperReceipt> ContPaperReceipts { get; set; }
        public virtual DbSet<contractor> contractor { get; set; }
        public virtual DbSet<contractorPaperPayment> contractorPaperPayments { get; set; }
        public virtual DbSet<contractorPaperReceipt> contractorPaperReceipts { get; set; }
        public virtual DbSet<EmployeeSales> EmployeeSales { get; set; }
        public virtual DbSet<PaymentSystems> PaymentSystems { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Abstract> Abstracts { get; set; }
        //public DbSet<contractorController> contractorController { get; set; }
        public DbSet<AdditionalSpecifications> AdditionalSpecifications { get; set; }
        public DbSet<DegreeOfExcellence> DegreeOfExcellences { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Engennering> Engennerings { get; set; }
        public DbSet<Flat> Flats { get; set; }
        public DbSet<Items> Itemss { get; set; }
        public DbSet<MainLand> MainLands { get; set; }
        public DbSet<MainProject> MainProjects { get; set; }
        public DbSet<MianItems> MianItemss { get; set; }
        public DbSet<Projects> Projectes { get; set; }
        public DbSet<ProjectStatu> ProjectStatus { get; set; }
        public DbSet<ProjectTextDescription> ProjectTextDescriptions { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<Supplieres> Supplieres { get; set; }
        public DbSet<TypesInvestment> TypesInvestments { get; set; }
        public DbSet<units> unitss { get; set; }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ConstructionMaterial> ConstructionMaterials { get; set; }
        public DbSet<ConstructionMaterialPriceVariable> ConstructionMaterialPriceVariables { get; set; }
        public DbSet<AssignedSupplier> AssignedSuppliers { get; set; }
        public DbSet<EngineerReceiveFromContractor> EngineerReceiveFromContractors { get; set; }
        public DbSet<Shareholder> Shareholders { get; set; }
        public DbSet<CashReceiptFromShareholder> CashReceiptFromShareholders { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<StageMaster> StageMasters { get; set; }
        public DbSet<ProjectStage> ProjectStages { get; set; }
        public DbSet<ProjectsAttachment> ProjectsAttachment { get; set; }
       
    
        //public DbSet<_MianItemsConstructionMaterial> MianItemsConstructionMaterials { get; set; }
        public DbSet<OrderJob> OrderJob { get; set; }
        public DbSet<OrderJobDetails> OrderJobDetails { get; set; }
        public virtual DbSet<amiraMaster> amiraMaster { get; set; }
        public virtual DbSet<amiraDetails> amiraDetails { get; set; }
        public virtual DbSet<BillPurchasesDetails> BillPurchasesDetails { get; set; }
        public virtual DbSet<BillPurchasesMaster> BillPurchasesMaster { get; set; }
        public virtual DbSet<LKStore> LKStores { get; set; }
        public virtual DbSet<StoreTransaction> StoreTransactions { get; set; }
        public virtual DbSet<WarehouseWarrantMaster> WarehouseWarrantMaster { get; set; }
        public virtual DbSet<WarehouseWarrantDetails> WarehouseWarrantDetails { get; set; }

        public virtual DbSet<PricesOffersMaster> PricesOffersMaster { get; set; }
        public virtual DbSet<PricesOffersDetails> PricesOffersDetails { get; set; }
        public virtual DbSet<MainLandAttachment> MainLandAttachments { get; set; }
        public virtual DbSet<MianItemsAttachment> MianItemsAttachments { get; set; }
        public virtual DbSet<MainProjectAttachment> MainProjectAttachments { get; set; }
        public virtual DbSet<FlatAttachment> FlatAttachments { get; set; }
        public virtual DbSet<VillaAttachment> VillaAttachments { get; set; }
        public virtual DbSet<MaterialDetail> MaterialDetails { get; set; }
        public virtual DbSet<MainItemDetail> MainItemDetails { get; set; }

        public virtual DbSet<CustomerSelectFlat> CustomerSelectFlat { get; set; }
        //

        public virtual DbSet<ContributerPaymentForProject> ContributerPaymentForProjects { get; set; }

        //public DbSet<contractorController> contractorController { get; set; }


        //


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();


            base.OnModelCreating(modelBuilder);


            //modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            //modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            //modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });



            modelBuilder.Entity<EmployeeSales>()
                .HasRequired(c => c.Villa)
                .WithMany()
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Projects>()
            //    .HasRequired(s => s.Villas)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmployeeSales>()
               .HasRequired(c => c.Flat)
               .WithMany()
               .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Projects>()
            //    .HasRequired(s => s.Flats)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);
        }

        
    }
}