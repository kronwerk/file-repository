using System;
using System.IO;
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
                if(Directory.Exists("~/"+model.Owner))
                {
                    DirectoryInfo Dir = new DirectoryInfo(Request.MapPath("~/" + model.Owner));
                }
                else
                {
                    DirectoryInfo Dir = new DirectoryInfo(Request.MapPath("~/" + model.Owner));
                    Dir.Create();
                    Dir.CreateSubdirectory(model.Name);
                }
                ViewData["Message"] = "Success";
                return View(model);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Smth wrong." + ex);
                return View(model);
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


        [HttpPost]
        public ActionResult Upload(string userId, int repoId,RepoEditModel model )
        {
             if (Request.Files.Count > 0)
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
                     return View(model);
                 }
                 string query = "SELECT * FROM Repositories WHERE Id = '"+repoId+"';";
                 SqlCommand cmd = new SqlCommand(query, conn);
                 SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                 string owner=null,repoName=null;
                 if (dr.Read())
                 {
                     owner=dr.GetValue(2).ToString();
                     repoName = dr.GetValue(1).ToString();
                     if (dr.GetValue(0).ToString() != userId)
                     {
                         query = "SELECT * FROM Connection WHERE Repos = '"+repoId+"';";
                         cmd = new SqlCommand(query, conn);
                         dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                         bool flag=false;
                         while(dr.Read()){
                             if(dr.GetValue(0).ToString() == userId)
                             {
                                 flag=true;
                                 break;
                             }
                         }
                         if(!flag)
                         {
                             ModelState.AddModelError("", "You don't have pervission!");
                             return View(model);
                         }
                     }
                 }
                  var file = Request.Files[0];

                 if (file != null && file.ContentLength > 0)
                 {
                    var fileName = Path.GetFileName(file.FileName);
                 query = "INSERT INTO Files (Name,Path,Repo,LastChange,Type) VALUES "
                     + "(@Name, @Path,@Repo,@time,@Type)";
                 cmd = new SqlCommand(query, conn);
                 SqlParameter param = new SqlParameter();
                 param.ParameterName = "@Name";
                 param.Value = fileName;
                 param.SqlDbType = SqlDbType.NVarChar;
                 cmd.Parameters.Add(param);

                 param = new SqlParameter();
                 param.ParameterName = "@Path";
                 param.Value = "~/" + owner + "/" + repoName + "/" + fileName;
                 param.SqlDbType = SqlDbType.NVarChar;
                 cmd.Parameters.Add(param);

                 param = new SqlParameter();
                 param.ParameterName = "@Repo";
                 param.Value = repoId;
                 param.SqlDbType = SqlDbType.NVarChar;
                 cmd.Parameters.Add(param);

                 param = new SqlParameter();
                 param.ParameterName = "@time";
                 param.Value = System.DateTime.Now;
                 param.SqlDbType = SqlDbType.DateTime;
                 cmd.Parameters.Add(param);

                 param = new SqlParameter();
                 param.ParameterName = "@type";
                 param.Value = file.ContentType;
                 param.SqlDbType = SqlDbType.DateTime;
                 cmd.Parameters.Add(param);
                 try
                 {
                     cmd.ExecuteNonQuery();
                 }
                 catch (Exception ex)
                 {
                     ModelState.AddModelError("", "Can't update. " + ex);
                     return RedirectToAction("Profile", "Account");
                 }
                 conn.Close();
                 conn.Dispose();
                 ViewData["Message"] = "Success";

                 var path = Path.Combine(Server.MapPath("~/"+owner+"/"+repoName+"/"), fileName);
                    file.SaveAs(path);
                 }
             }

             return RedirectToAction("UploadDocument");
         }
    }
}
