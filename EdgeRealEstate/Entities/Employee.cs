using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public partial class Employee
    {
        public Employee()
        {
            this.EmloyeeTargets = new HashSet<EmloyeeTarget>();
            this.EmployeeAttaches = new HashSet<EmployeeAttach>();
            this.CustPaperReceipts = new HashSet<CustPaperReceipt>();
            this.CustPaperReceipts1 = new HashSet<CustPaperReceipt>();
            this.CustPaperPayments = new HashSet<CustPaperPayment>();
            this.CustPaperPayments1 = new HashSet<CustPaperPayment>();
            this.ContPaperReceipts = new HashSet<ContPaperReceipt>();
            this.ContPaperReceipts1 = new HashSet<ContPaperReceipt>();
            this.ContPaperPayments = new HashSet<ContPaperPayment>();
            this.ContPaperPayments1 = new HashSet<ContPaperPayment>();

            this.contractorPaperReceipts = new HashSet<contractorPaperReceipt>();
            this.contractorPaperReceipts1 = new HashSet<contractorPaperReceipt>();
            this.contractorPaperPayments = new HashSet<contractorPaperPayment>();
            this.contractorPaperPayments1 = new HashSet<contractorPaperPayment>();
            this.EmployeeSales = new HashSet<EmployeeSales>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = " يجب ادخال اسم الموظف")]
        public string ARName { get; set; }
        public string ENName { get; set; }
        public int age { get; set; }
        public string Degree { get; set; }
        public int LKNationalityId { get; set; }
        public string religion { get; set; }
        public bool isFemale { get; set; }
        public string nearTele { get; set; }
        public string tele { get; set; }
        public string nationalTele { get; set; }
        public bool isActive { get; set; }
        public string address { get; set; }
        public int LKBranchId { get; set; }
        public string startDate { get; set; }
        public string ContractDur { get; set; }
        public string HashCol { get; set; }
        public bool IsDeleted { get; set; }
        public string Description { get; set; }
        public DateTime DOB { get; set; }
        public DateTime POB { get; set; }
        public string SocStatus { get; set; }

        [ForeignKey("LKTitle")]
        public int TitleId { get; set; }
        public string section { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
        public double salary { get; set; }
        public double Housingallowance { get; set; }

        public virtual LKBranch LKBranch { get; set; }
        public virtual LKTitle LKTitle { get; set; }
        public virtual LKNationality LKNationality { get; set; }
        public virtual ICollection<EmloyeeTarget> EmloyeeTargets { get; set; }
        public virtual ICollection<EmployeeAttach> EmployeeAttaches { get; set; }

        public virtual ICollection<CustPaperReceipt> CustPaperReceipts { get; set; }
        public virtual ICollection<CustPaperReceipt> CustPaperReceipts1 { get; set; }
        public virtual ICollection<CustPaperPayment> CustPaperPayments { get; set; }
        public virtual ICollection<CustPaperPayment> CustPaperPayments1 { get; set; }

        public virtual ICollection<ContPaperReceipt> ContPaperReceipts { get; set; }
        public virtual ICollection<ContPaperReceipt> ContPaperReceipts1 { get; set; }
        public virtual ICollection<ContPaperPayment> ContPaperPayments { get; set; }
        public virtual ICollection<ContPaperPayment> ContPaperPayments1 { get; set; }


        public virtual ICollection<contractorPaperReceipt> contractorPaperReceipts { get; set; }
        public virtual ICollection<contractorPaperReceipt> contractorPaperReceipts1 { get; set; }
        public virtual ICollection<contractorPaperPayment> contractorPaperPayments { get; set; }
        public virtual ICollection<contractorPaperPayment> contractorPaperPayments1 { get; set; }
        public virtual ICollection<EmployeeSales> EmployeeSales { get; set; }


    }
}