namespace CloudPrototyper.Model.Resources
{
    /// <summary>
    /// Hosting environment base class.
    /// </summary>
    public abstract class HostingEnvironment : Resource
    {
        /// <summary>
        /// Application to be deployed here.
        /// </summary>
        public string WithApplication { get; set; }
    }
}
