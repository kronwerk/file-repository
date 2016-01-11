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
                string query1="SELECT * FROM Repositories WHERE Name='"+model.Name+"';";
                SqlCommand cmd1 = new SqlCommand(query1, conn);
                SqlDataReader dr=cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                dr.Read();
                string dirName = dr.GetValue(0).ToString();
                conn.Close();
                conn.Dispose();
                if (Directory.Exists("~/Repos/" + User.Identity.GetUserId()))
                {
                    DirectoryInfo Dir = new DirectoryInfo(Server.MapPath("~/Repos/" + User.Identity.GetUserId()));
                    Dir.CreateSubdirectory(dirName);
                }
                else
                {
                    DirectoryInfo Dir = new DirectoryInfo(Server.MapPath("~/Repos/" + User.Identity.GetUserId()));
                    Dir.Create();
                    Dir.CreateSubdirectory(dirName);
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
        public ActionResult Edit(int id, RepoEditModel model)
        {
            ViewBag.repoId = id;
            return View(model);
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
        public ActionResult Delete(int id,CreateViewModel model)
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
                    return RedirectToAction("Profile", "Account", model);
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
                    return RedirectToAction("Profile", "Account", model);

                }
                conn.Close();
                conn.Dispose();
                DirectoryInfo Dir = new DirectoryInfo(Server.MapPath("~/Repos/" + User.Identity.GetUserId() +"/"+id));
                Dir.Delete();
                ViewData["Message"] = "Success";
                return RedirectToAction("Profile", "Account");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Can't. " + ex);
                return RedirectToAction("Profile", "Account", model);
            }
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Upload(HttpPostedFileBase file,RepoEditModel model )
        {
             if (Request.Files.Count > 0)
             {
                 string repoId = model.Repo.ToString();
                 string userId = model.User.ToString();
                 string connStr = @"Data Source=(LocalDb)\v11.0;AttachDbFilename=|DataDirectory|\aspnet-WebApplication2-20160108044733.mdf;Initial Catalog=aspnet-WebApplication2-20160108044733;Integrated Security=True";
                 SqlConnection conn = new SqlConnection(connStr);
                 try
                 {
                     conn.Open();
                 }
                 catch (SqlException se)
                 {
                     ModelState.AddModelError("", "can't open connection" + se);
                     return RedirectToAction("Edit",model);
                 }
                 string query = "SELECT * FROM Repositories WHERE Id = '"+repoId+"';";
                 SqlCommand cmd = new SqlCommand(query, conn);
                 SqlDataReader dr = cmd.ExecuteReader();
                 string owner=null;
                 bool flag = false;
                 if (dr.Read())
                 {
                     owner=dr.GetValue(2).ToString();
                     if (owner == userId)
                     {
                         flag = true;
                     }else{
                         query = "SELECT * FROM Connection WHERE Repos = '"+repoId+"';";
                         cmd = new SqlCommand(query, conn);
                         dr.Close();
                         dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
                             return RedirectToAction("Edit?repoId="+model.Repo.ToString(), model);

                         }
                     }
                 }
                  var newFile = Request.Files[0];

                 if (newFile != null && newFile.ContentLength > 0)
                 {
                    var fileName = Path.GetFileName(newFile.FileName);
                    query = "INSERT INTO Files (Name,Path,Repo,LastChange,LastChangeBy,Type) VALUES "
                     + "(@Name, @Path,@Repo,@time,@currentUser,@Type)";
                 cmd = new SqlCommand(query, conn);
                 SqlParameter param = new SqlParameter();
                 param.ParameterName = "@Name";
                 param.Value = fileName.Trim();
                 param.SqlDbType = SqlDbType.NVarChar;
                 cmd.Parameters.Add(param);

                 param = new SqlParameter();
                 param.ParameterName = "@Path";
                 param.Value = "~/Repos/" + owner + "/" + repoId + "/" + fileName;
                 param.SqlDbType = SqlDbType.NVarChar;
                 cmd.Parameters.Add(param);

                 param = new SqlParameter();
                 param.ParameterName = "@Repo";
                 param.Value = repoId;
                 param.SqlDbType = SqlDbType.Int;
                 cmd.Parameters.Add(param);

                 param = new SqlParameter();
                 param.ParameterName = "@time";
                 param.Value = System.DateTime.Now;
                 param.SqlDbType = SqlDbType.DateTime;
                 cmd.Parameters.Add(param);

                 param = new SqlParameter();
                 param.ParameterName = "@currentUser";
                 param.Value = userId;
                 param.SqlDbType = SqlDbType.NVarChar;
                 cmd.Parameters.Add(param);

                 param = new SqlParameter();
                 param.ParameterName = "@type";
                 param.Value = file.ContentType;
                 param.SqlDbType = SqlDbType.NVarChar;
                 dr.Close();
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

                 var path = Path.Combine(Server.MapPath("~/Repos/"+owner+"/"+repoId+"/"), fileName);
                    file.SaveAs(path);
                 }
             }
             return RedirectToAction("Edit",routeValues: new { id = model.Repo.ToString(), model=model });

         }
        public ActionResult DeleteFile(string fileName, int repoId,string repoOwner, CreateViewModel model)
        {
            try
            {
                fileName = fileName.Trim();
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
                    return RedirectToAction("Profile", "Account", model);
                }
                string query = "DELETE FROM Files" +
                    " where Name = @Id AND Repo=@repoId";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = fileName;
                param.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@repoId";
                param.Value = repoId;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Can't update. " + ex);
                    return RedirectToAction("Profile", "Account", model);

                }
                conn.Close();
                conn.Dispose();
                FileInfo File = new FileInfo(Server.MapPath("~/Repos/" + repoOwner + "/" +repoId+"/"+ fileName));
                File.Delete();
                ViewData["Message"] = "Success";
                return RedirectToAction("Edit", routeValues: new { id = repoId, model = model });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Can't. " + ex);
                return RedirectToAction("Edit", routeValues: new { id = repoId, model = model });
            }
        }
    }
}
