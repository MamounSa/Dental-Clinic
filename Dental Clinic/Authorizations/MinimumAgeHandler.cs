using Microsoft.AspNetCore.Authorization;

public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var birthDateClaim = context.User.FindFirst("DateofBirth");
        if (birthDateClaim == null) return Task.CompletedTask;

        var birthDate = DateTime.Parse(birthDateClaim.Value);
        var age = DateTime.Today.Year - birthDate.Year;
        if (birthDate > DateTime.Today.AddYears(-age)) age--;

        if (age >= requirement.MinimumAge)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}