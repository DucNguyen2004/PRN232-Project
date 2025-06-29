using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace PRN232Project.Utils
{
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        /// <summary>
        /// Fetches an entity via the provided factory.  If null, throws a 404 ProblemException.
        /// </summary>
        protected async Task<T> GetEntityOrThrowAsync<T>(
            Func<Task<T>> fetchEntity,
            int id,
            string entityName)
            where T : class
        {
            var entity = await fetchEntity();
            if (entity == null)
            {
                throw ProblemException.NotFound($"{entityName} with Id {id} not found.");
            }
            return entity;
        }

        /// <summary>
        /// Wraps a successful result in your ApiResponseDto<T> and returns 200 OK.
        /// </summary>
        protected ActionResult<ApiResponseDto<T>> OkResponse<T>(T data)
        {
            return base.Ok(new ApiResponseDto<T> { Data = data });
        }

        /// <summary>
        /// Wraps a 201 CreatedAtAction return with your ApiResponseDto<T> envelope.
        /// </summary>
        protected ActionResult<ApiResponseDto<T>> CreatedResponse<T>(
            string actionName,
            object routeValues,
            T data)
        {
            return CreatedAtAction(actionName, routeValues, new ApiResponseDto<T>
            {
                Data = data
            });
        }
    }
}
