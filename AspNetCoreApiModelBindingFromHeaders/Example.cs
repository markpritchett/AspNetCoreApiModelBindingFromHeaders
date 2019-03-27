using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreApiModelBindingFromHeaders
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        // POST api/example/single-complex-type-body-and-header
        [HttpPost("single-complex-type-body-and-header")]
        public ActionResult SingleMixedComplexType(Mixed mixed)
        {
            return Ok(mixed);
        }

        // POST api/example/separate-complex-type-for-body-and-header
        [HttpPost("separate-complex-type-for-body-and-header")]
        public ActionResult SeparateComplexTypes(BodyOnly bodyOnly, [FromHeader]HeaderOnly headerOnly)
        {
            return Ok(new SeparateTypesResponse
            {
                BodyOnly = bodyOnly,
                HeaderOnly = headerOnly
            });
        }
    }

    public class Mixed
    {
        [FromBody]
        public string BodyName { get; set; }
        [FromHeader]
        public string HeaderName { get; set; }
    }

    public class BodyOnly
    {
        public string BodyName { get; set; }
    }

    public class HeaderOnly
    {
        [FromHeader]
        public string HeaderName { get; set; }
    }

    public class SeparateTypesResponse
    {
        public BodyOnly BodyOnly { get; set; }
        public HeaderOnly HeaderOnly { get; set; }
    }
}
