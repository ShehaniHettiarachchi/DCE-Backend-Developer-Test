using DCE_Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DCE_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SupplierController : Controller
	{
		private IConfiguration Configuration;

		public SupplierController(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		SqlConnection connString;
		SqlCommand cmd;

		//Get All
		[HttpGet]
		public JsonResult Index()
		{
			List<Supplier> supplierList = new List<Supplier>();
			connString = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection"));
			DataTable dt = new DataTable();
			cmd = new SqlCommand("select * from Supplier", connString);
			connString.Open();
			SqlDataAdapter ad = new SqlDataAdapter(cmd);
			ad.Fill(dt);

			for (int i = 0; i < dt.Rows.Count; i++)
			{
				Supplier obj = new Supplier();
				obj.SupplierId = dt.Rows[i]["SupplierId"].ToString();
				obj.SupplierName = dt.Rows[i]["SupplierName"].ToString();
				obj.CreatedOn = dt.Rows[i]["CreatedOn"].ToString();
				obj.IsActive = Convert.ToInt16(dt.Rows[i]["IsActive"]);
				supplierList.Add(obj);
			}
			connString.Close();
			return Json(dt);
		}
	}
}
