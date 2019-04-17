mainModule
    .config(Config);

Config.$inject = ["$routeProvider"];

function Config($routeProvider) {
    $routeProvider
        .when("/games", {
            template: "<game-list></game-list>"
        })
        .when("/games/:id", {
            template: "<game-details></game-details>"
        })
        .when("/support", {
            template: "<support-form></support-form>"
        })
        .when("/sign-out", {
            template: "<sign-out></sign-out>"
        })
        .otherwise({
            template: "<game-list></game-list>"
        });
}