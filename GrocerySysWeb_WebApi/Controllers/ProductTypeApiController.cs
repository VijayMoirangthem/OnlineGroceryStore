using GrocerySysWeb_DAL.Models;
using GrocerySysWeb_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GrocerySysWeb_WebApi.Controllers
{
    public class ProductTypeApiController : ApiController
    {
        InventoryRepository InvREPO = new InventoryRepository();

        [HttpGet]
        public List<String> getProductType()
        {
            return InvREPO.getProductType();
        }

        [HttpGet]
        public List<InventoryModel> getByProductType(string ProductType)
        {
            return InvREPO.getByProductType(ProductType);
        }
    }
}
