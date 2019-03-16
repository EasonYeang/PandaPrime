using AutoMapper;

namespace PandaPrime.AM
{
    public class Configuration
    {
        public static void Configure()
        {
            Mapper.Initialize(ctg => ctg.AddProfiles(new[] { "PandaPrime" }));
        }
    }
}