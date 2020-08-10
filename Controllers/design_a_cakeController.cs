using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Cakeservice.Models;

namespace Cakeservice.Controllers
{
    public class design_a_cakeController : ApiController
    {
        private CakeCompanyEntities4 db = new CakeCompanyEntities4();

        // GET: api/design_a_cake
        public IQueryable<design_a_cake> Getdesign_a_cake()
        {
            return db.design_a_cake;
        }

        // POST: api/design_a_cake
        [ResponseType(typeof(design_a_cake))]
        public HttpStatusCode Postdesign_a_cake(design_a_cake url)
        {
            if (!ModelState.IsValid)
            {
                // return 400 Bad request
                return HttpStatusCode.BadRequest;
            }

            // this piece of code is going to download the image, check and get the size so I can compare and implement my logic
            string baseImage = url.@base;
            string logoImage = url.logo;

            // use c# webclient to download the images
            byte[] baseImageData = new WebClient().DownloadData(baseImage);
            byte[] logoImageData = new WebClient().DownloadData(logoImage);

            // I will use system.io memory stream which creates a stream whose backing store is memory.to browse .Net framework
            MemoryStream baseImageStream = new MemoryStream(baseImageData);
            MemoryStream logoImageStream = new MemoryStream(logoImageData);

            Image baseImg = Image.FromStream(baseImageStream);
            Image logoImg = Image.FromStream(logoImageStream);

            // Get the width and heigth of base url and logo url
            int baseImgWidth = baseImg.Width;
            int baseImgHeight = baseImg.Height;

            int logoImgWidth = logoImg.Width;
            int logoImgHeight = logoImg.Height;

            db.design_a_cake.Add(url);
            db.SaveChanges();

            // return a response of 200 OK
            return HttpStatusCode.OK;
        }

        // this method release memory once we are done with it
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}