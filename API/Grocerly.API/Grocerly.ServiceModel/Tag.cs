using ServiceStack;
using System;
using System.Collections.Generic;

namespace Grocerly.ServiceModel
{
    [Route("/tags/", "GET")]
    public class GetTags : IReturn<List<TagsResponse>>
    {
    }

    [Route("/tags/{Id}", "GET")]
    public class GetTag : IReturn<TagsResponse>
    {
        public Guid Id { get; set; }
    }

    [Route("/tags/{Name}", "GET")]
    public class GetTagByName : IReturn<TagsResponse>
    {
        public string Name { get; set; }
    }

    public class TagsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
