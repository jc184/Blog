using Blog.Contracts.DTO;
using Blog.Core.Exceptions;
using Blog.Core.Handlers.Commands;
using Blog.Core.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Blog.API.Controllers
{
    /// <summary>
    /// Posts Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor for Posts Controller
        /// </summary>
        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves all posts with related comments
        /// </summary>
        /// <response code="200">Posts retrieved</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PostDTO>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllPostsQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new post
        /// </summary>
        /// <response code="201">Post added</response>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Post([FromBody] CreatePostDTO model)
        {
            try
            {
                var command = new CreatePostCommand(model);
                var response = await _mediator.Send(command);
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }

        /// <summary>
        /// Retrieves a specific post by id including related comments
        /// </summary>
        /// <response code="200">Post retrieved</response>
        /// <response code="404">Post not found</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(PostDTO), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var query = new GetPostByIdQuery(id);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                });
            }
        }

        /// <summary>
        /// Deletes a post
        /// </summary>
        /// <response code="200">Post deleted</response>
        /// <response code="400">Bad Request</response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var command = new DeletePostCommand(id);
                var response = await _mediator.Send(command);
                return StatusCode((int)HttpStatusCode.OK, response);
            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }

        /// <summary>
        /// Updates a post
        /// </summary>
        /// <response code="200">Post updated</response>
        /// <response code="400">Bad Request</response>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update([FromBody] UpdatePostDTO model, int id)
        {
            try
            {
                var command = new UpdatePostCommand(model, id);
                var response = await _mediator.Send(command);
                return StatusCode((int)HttpStatusCode.OK, response);
            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }
    }
}
