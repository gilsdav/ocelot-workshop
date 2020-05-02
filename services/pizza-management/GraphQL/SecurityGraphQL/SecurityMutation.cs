using GraphQL.Types;
using PizzaGraphQL.Services;

namespace PizzaGraphQL.GraphQL.SecurityGraphQL
{
    public class SecurityMutation: ObjectGraphType
    {
        public SecurityMutation(IAuthenticationService authenticationService) {
            FieldAsync<StringGraphType>(
                "login",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "user" },
                    new QueryArgument<StringGraphType> { Name = "password" }),
                resolve: async context => {
                    var user = (string)context.Arguments["user"];
                    var password = (string)context.Arguments["password"];
                    return await authenticationService.Login(user, password);
                });
        }
    }
}