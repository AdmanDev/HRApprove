namespace HRApprove.API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Represent the base class for controllers.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApiController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator service.</param>
        public BaseApiController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        /// <summary>
        /// Gets the mediator service.
        /// </summary>
        protected IMediator Mediator { get; private set; }
    }
}
