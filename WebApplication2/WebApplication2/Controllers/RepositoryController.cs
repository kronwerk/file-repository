using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class RepositoryController : Controller
    {
        //
        // GET: /Repository/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Repository/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Repository/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Repository/Create
        [HttpPost]
        public ActionResult Create(CreateViewModel model)
        {
            try
            {
                string connStr = @"Data Source=(LocalDb)\v11.0;AttachDbFilename=|DataDirectory|\aspnet-WebApplication2-20160108044733.mdf;Initial Catalog=aspnet-WebApplication2-20160108044733;Integrated Security=True";
                SqlConnection conn = new SqlConnection(connStr);
                try
                {
                    //пробуем подключится
                    conn.Open();
                }
                catch (SqlException se)
                {
                    ModelState.AddModelError("", "can't open connection"+se);
                    return View(model);
                }
                string query = "INSERT INTO Repositories (Name,Owner,tags,LastChangeR) VALUES "
                    + "(@Name, @Owner,@tags,@time)";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Name";
                param.Value = model.Name;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@Owner";
                param.Value = User.Identity.GetUserId();
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@tags";
                param.Value = model.tags;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@time";
                param.Value = System.DateTime.Now;
                param.SqlDbType = SqlDbType.DateTime;
                cmd.Parameters.Add(param);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "Can't update. "+ ex);
                    return RedirectToAction("Profile","Account");
                }
                conn.Close();
                conn.Dispose();
                ViewData["Message"] = "Success";
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Repository/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Repository/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Repository/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                string connStr = @"Data Source=(LocalDb)\v11.0;AttachDbFilename=|DataDirectory|\aspnet-WebApplication2-20160108044733.mdf;Initial Catalog=aspnet-WebApplication2-20160108044733;Integrated Security=True";
                SqlConnection conn = new SqlConnection(connStr);
                try
                {
                    //пробуем подключится
                    conn.Open();
                }
                catch (SqlException se)
                {
                    ModelState.AddModelError("", "can't open connection" + se);
                }
                string query = "DELETE FROM Repositories" +
                    " where Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = id;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Can't update. " + ex);
                }
                conn.Close();
                conn.Dispose();
                ViewData["Message"] = "Success";
                return RedirectToAction("Profile", "Account");
            }
            catch
            {
                return View();
            }
        }
    }
}
