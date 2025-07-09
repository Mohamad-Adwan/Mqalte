﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;


namespace ArticlProject.Code

{ 
    
    public class FileHelper
    {
        private readonly IWebHostEnvironment webHost;

        public FileHelper(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
        }
        
        //Uplode in Files
        public string UplodeFile(IFormFile file,string folder)
        {
            if (file != null)
            {
                var fileDir=Path.Combine(webHost.WebRootPath,folder);
                var fileName = Guid.NewGuid()+"-"+file.FileName;
                var filePath= Path.Combine(fileDir,fileName);
                using (FileStream fileStream = new FileStream(filePath,FileMode.Create))
                {
                    file.CopyTo(fileStream);
                    return fileName;
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
