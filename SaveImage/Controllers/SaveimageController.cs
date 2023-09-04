using SaveImage.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaveImage.Controllers
{
    public class SaveimageController : Controller
    {
        // GET: Saveimage
        public ActionResult Index()
        {
            using (Dbmodel dbmodel = new Dbmodel())
            {
                return View(dbmodel.Empimages.ToList());
            }
                
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Empimage empimage)
        {
            string filename = Path.GetFileNameWithoutExtension(empimage.ImageFile.FileName);
            string extension = Path.GetExtension(empimage.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            empimage.Image = "~/Uploadimage/" + filename;
            filename = Path.Combine(Server.MapPath("~/Uploadimage/"), filename);
            empimage.ImageFile.SaveAs(filename);
            using (Dbmodel dbmodel = new Dbmodel())
            {
                dbmodel.Empimages.Add(empimage);
                dbmodel.SaveChanges();
            }
            ModelState.Clear();
            return View();
        }
    }
}