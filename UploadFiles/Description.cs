using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadFiles
{
    class Description : Attribute
    {
        public string Text;

        public Description(string text){

            Text = text;
        }
    }
}
