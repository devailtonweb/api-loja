using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;

namespace AppStore.Endpoints;

public static class ProblemDetailsExtensions
{

    /*
     * O this referece a extensão, ou seja,  a classe Notification irá extender esse metodo
     * Ex.: Notificaton.ConvertToProblemDetails()
     */
    public static Dictionary<string, string[]> ConvertToProblemDetails(this IReadOnlyCollection<Notification> notifications) {
        return notifications
                .GroupBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Message).ToArray());

    }

    public static Dictionary<string, string[]> ConvertToProblemDetails(this IEnumerable<IdentityError> error)
    {
        var dictionary = new Dictionary<string, string[]>();
        dictionary.Add("Error", error.Select(e => e.Description).ToArray());

        return dictionary;
    }

}
