authServiceModule
    .factory("authService", AuthService);

function AuthService() {
    return {
        isAuthenticated: function () {
            var cookie = localStorage.getItem("cookie");
            return cookie !== null && cookie !== undefined;
        },
        getCookie: function () {
            return JSON.parse(localStorage.getItem("cookie"));
        },
        setCookie: function (data) {
            localStorage.setItem("cookie", JSON.stringify(data));
        },
        signOut: function () {
            localStorage.removeItem("cookie");
            location.href = "/";
        }
    }
}