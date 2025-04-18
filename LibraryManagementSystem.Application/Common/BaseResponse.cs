using System.Net;

namespace LibraryManagementSystem.Application.Common
{
    public class BaseResponse<T>
    {
        public T? Data { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }

        public BaseResponse(T? data, HttpStatusCode httpStatusCode, string message)
        {
            HttpStatusCode = httpStatusCode;
            Message = message;
            Timestamp = DateTime.UtcNow;
            Data = data;
        }
        public BaseResponse(T? data, string message)
        {
            Message = message;
            Timestamp = DateTime.UtcNow;
        }
        public BaseResponse(HttpStatusCode httpStatusCode, string message)
        {
            HttpStatusCode = httpStatusCode;
            Message = message;
            Timestamp = DateTime.UtcNow;
        }

        // ✅ When a request is successfully processed and returns data.
        public static BaseResponse<T> SuccessResponse(T? data, string message = "Successfully Operation")
        {
            return new BaseResponse<T>(data, HttpStatusCode.OK, message);
        }

        //  ✅ When a new resource is successfully created (creating author, book, user).
        public static BaseResponse<T> CreatedResponse(T? data, string message = "Resource created successfully")
        {
            return new BaseResponse<T>(data, HttpStatusCode.Created, message);
        }
        public static BaseResponse<T> CreatedResponse(string message = "Resource created successfully")
        {
            return new BaseResponse<T>(HttpStatusCode.Created, message);
        }

        //  ✅ When the operation succeeds but doesn’t need to return data ( successful delete operation).
        public static BaseResponse<T> NoContentResponse(string message = "No Content Available!")
        {
            return new BaseResponse<T>(default, HttpStatusCode.NoContent, message);
        }


        // ❌ When the request is invalid (e.g., missing required fields, incorrect format).
        public static BaseResponse<T> ErrorResponse(string message = "Failed Operation!")
        {
            return new BaseResponse<T>(default, HttpStatusCode.BadRequest, message);
        }
        public static BaseResponse<T> ErrorResponse(T? data, string message = "Failed Operation!")
        {
            return new BaseResponse<T>(data, HttpStatusCode.BadRequest, message);
        }

        // ❌ When the requested resource doesn’t exist (searching for a non-existent book).
        public static BaseResponse<T> NotFoundResponse(string message = "Resource not found")
        {
            return new BaseResponse<T>(HttpStatusCode.NotFound, message);
        }


        // ❌ When an unexpected error occurs in the server.
        public static BaseResponse<T> InternalServerErrorResponse(string message = "An error occurred")
        {
            return new BaseResponse<T>(HttpStatusCode.InternalServerError, message);
        }

        // ❌ When input validation fails (e.g., missing fields, invalid email format).
        public static BaseResponse<T> ValidationErrorResponse(string message = "Validation failed")
        {
            return new BaseResponse<T>(HttpStatusCode.UnprocessableEntity, message);
        }

        // ❌ When a conflict occurs (e.g., trying to create a duplicate entry).
        public static BaseResponse<T> ConflictResponse(string message = "Conflict detected")
        {
            return new BaseResponse<T>(HttpStatusCode.Conflict, message);
        }
    }
}
