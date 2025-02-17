using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessObject.Models
{
    public class FileUpload
    {
        public IFormFile file {  get; set; }
        public  string product {  get; set; }

    }
}
