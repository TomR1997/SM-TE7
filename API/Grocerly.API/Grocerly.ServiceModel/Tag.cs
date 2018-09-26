using ServiceStack;
using System;
using System.Collections.Generic;

namespace Grocerly.ServiceModel
{
    [Route("/tags/", "GET")]
    public class GetTags : IReturn<List<TagResponse>>
    {
    }

    [Route("/tags/{Id}", "GET")]
    public class GetTag : IReturn<TagResponse>
    {
        public Guid Id { get; set; }
    }

    [Route("/tags/{Name}", "GET")]
    public class GetTagByName : IReturn<TagResponse>
    {
        public string Name { get; set; }
    }

    public class TagResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
