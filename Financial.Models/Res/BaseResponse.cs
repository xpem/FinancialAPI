﻿namespace Financial.Models.Responses
{
    public record BaseResponse
    {
        public bool Success { get; set; } = true;

        public object? Content { get; init; }

        public Error? Error { get; init; } = null;

        public BaseResponse(object? content, string? errorMessage = null)
        {
            if (!string.IsNullOrEmpty(errorMessage))
            {
                Success = false;
                Content = null;

                if (!string.IsNullOrEmpty(errorMessage))
                    Error = new Error { Message = errorMessage };
            }
            else Content = content;
        }
    }

    public record Error
    {
        public required string Message { get; init; }
    }
}
