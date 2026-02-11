using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Project.Model.Domain
{
    public class ProductMaster
    {
        [Key]
        public string PAID { get; set; }
        public string PCODE { get; set; }
        public string PNAME { get; set; }
        public string PTYPE { get; set; }
        public string PCATEGORY { get; set; }
        public string PSUBCATEGORY { get; set; }
        public string PDESCRIPTION { get; set; }
        public string PSALES { get; set; }
        public string PTYPESOFSALES { get; set; }
        public string PPURCHASEUOM { get; set; }
        public string PHSN { get; set; }
        public string PREORDERLEVEL { get; set; }
        public string PFROMOFPRODUCT { get; set; }
        public string PRETURNPOLICY { get; set; }
        public string PPRODUCTUSE { get; set; }
        public string PIMAGES { get; set; }
        public double PIGST { get; set; }
        public double PSGST { get; set; }
        public double PCESS { get; set; }
        public double pcgst { get; set; }
        public string COMPANYID { get; set; }
        public string DEPTID { get; set; }
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string BRANDNAME { get; set; }
        public double RETAILP { get; set; }
        public double DEALERP { get; set; }
        public string RATE { get; set; }
        public string RANGE { get; set; }
        public string DISCOUNTTYPE { get; set; }
        public string SUBCATEGORY { get; set; }
        public string CATEGORY { get; set; }
        public string SALETYPE { get; set; }
        public string PRODUCTTYPE { get; set; }

    }
}
