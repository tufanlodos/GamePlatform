authServiceModule
    .component("signOut", {
        controller: function (authService) {
            authService.signOut();
        }
    });