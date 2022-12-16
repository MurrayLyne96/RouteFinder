namespace RouteFinderAPI.Controllers.Base;

[ExcludeFromCodeCoverage]
[ApiController]
public class BaseController : ControllerBase
{
    protected ActionResult OkOrNoContent(object value)
    {
        if (HasNoValueOrItems(value)) return NoContent();

        return Ok(value);
    }

    protected ActionResult OkOrNoListContent(IList value)
    {
        if (HasNoValueOrItems(value)) return NoContent();

        return Ok(value);
    }

    protected ActionResult OkOrNoNotFound(object value)
    {
        if (HasNoValueOrItems(value)) return NotFound();

        return Ok(value);
    }

    protected ActionResult NoContentOrNoNotFound(bool value) =>
         value ? NoContent() : NotFound();

    private static bool HasNoValueOrItems(object value)
    {
        return value == null || value is IList { Count: < 1 };
    }
}