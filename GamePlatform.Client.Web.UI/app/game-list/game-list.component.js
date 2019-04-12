gameListModule
    .component("gameList", {
        templateUrl: "../app/game-list/game-list.template.html",
        controller: GameListCtrl
    });

GameListCtrl.$inject = ["$http", "authService"];

function GameListCtrl($http, authService) {
    console.log(authService.isAuthenticated());
    if (!authService.isAuthenticated())
        location.href = "/";

    var cookie = authService.getCookie();

    $http({
        url: "http://localhost:59389/api/game",
        method: "get",
        headers: {
            "authorization": `Bearer ${cookie.token}`
        }
    }).then((res) => {
        this.games = res.data;
    });
}