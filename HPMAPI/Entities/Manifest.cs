using System.Collections.Generic;

namespace HPMAPI.Entities
{
    public class Manifest
    {
        public string packageName { get; set; }
        public string documentationLink { get; set; }
        public string communityLink { get; set; }
        public string releaseNotes { get; set; }
        public string licenseFile { get; set; }
        public string author { get; set; }
        public string minimumHEVersion { get; set; }
        public string dateReleased { get; set; }
        public string version { get; set; }
        public IList<App> apps { get; set; }
        public IList<Driver> drivers { get; set; }
    }
}
