using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Application.Swagger
{
    public  class SwaggerConfiguration
    {
        public string ProjectVersion {get;set;} = string.Empty; 

        /// <summary>
        /// Gets the Project Name from the configuration
        /// AppSetting: Swagger:ProjectName
        /// </summary>
        public string ProjectName { get; set; } = string.Empty;

        /// <summary>
        /// Gets the Version from the configuration
        /// AppSettings: Swagger:Version
        /// </summary>
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// Gets Project description
        /// </summary>
        public string ProjectDescription { get; set; } = string.Empty;
    }
}
