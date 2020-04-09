using Microsoft.AspNetCore.Identity;

namespace quiz.server
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName) { return new IdentityError { Code = nameof(DuplicateUserName), Description = $"Il nome utente '{userName}' è già stato utilizzato"  }; }
        public override IdentityError PasswordRequiresNonAlphanumeric() { return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "La password deve contenere almeno un carattere non alfanumerico (!, ?, @, ...)." }; }
        public override IdentityError PasswordRequiresDigit() { return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "La password deve contenere almeno un numero (0-9)" }; }
        public override IdentityError PasswordRequiresLower() { return new IdentityError { Code = nameof(PasswordRequiresLower), Description = "La password deve contenere almeno una lettera minuscola (a-z)" }; }
        public override IdentityError PasswordRequiresUpper() { return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = "La password deve contenere almeno una lettera maiuscola (A-Z)" }; }
    }
}