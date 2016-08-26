using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadFiles
{
    public enum Mime
    {
        [Description("application/msword")]
        DOC,
        [Description("application/vnd.openxmlformats-officedocument.wordprocessingml.document")]
        DOCX,
        [Description("application/vnd.ms-powerpoint")]
        PPT,
        [Description("application/vnd.openxmlformats-officedocument.presentationml.presentation")]
        PPTX,
        [Description("application/vnd.ms-excel")]
        XLS,
        [Description("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        XLSX
    }
}
