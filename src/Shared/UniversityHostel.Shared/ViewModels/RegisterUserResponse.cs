namespace CommonLibrary.ViewModels
{
    using System.Collections.Generic;

    public class RegisterUserResponse
    {
        public bool IsSucceeded { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}

