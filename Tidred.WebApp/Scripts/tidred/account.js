var accountHandler = (function () {

    return {

        getAccountHeader: (function () {

            var token = sessionStorage.getItem("userToken");
            var headers = {};
            if (token) {
                headers.Authorization = 'Bearer ' + token;
            }

            return headers;
        }),

        getUserInfo: (function () {

            var result = {
                userName: sessionStorage.getItem("userName"),
                userId: sessionStorage.getItem("userId"),
                coId: sessionStorage.getItem("coId")
            };

            return result;
        }),

        applyUserInfo: (function (returnUrl) {

            $.ajax({
                type: 'GET',
                url: '/api/account/userinfo',
                headers: accountHandler.getAccountHeader()
            }).done(function (data) {
                sessionStorage.setItem("userName", data.userName);
                sessionStorage.setItem("userId", data.userId);
                sessionStorage.setItem("coId", data.coId);
                window.location.href = returnUrl;
            }).fail(function (err) {
                alert("Error! " + err.responseText);
            });

        }),

        authenticateUser: (function (userName, pass, returnUrl) {

            var loginData = {
                grant_type: 'password',
                username: userName,
                password: pass
            }

            $.ajax({
                type: 'POST',
                url: '/Token',
                data: loginData
            }).done(function (data) {
                // Cache the access token in session storage
                sessionStorage.setItem("userToken", data.access_token);
                // cache user info
                accountHandler.applyUserInfo(returnUrl);
            }).fail(function (err) {
                alert("Error! " + err.responseText);
            });

        }),

        logOff: (function () {

            $.ajax({
                type: 'POST',
                url: '/api/account/logout',
                headers: accountHandler.getAccountHeader()
            }).done(function (data) {
                sessionStorage.removeItem("userToken");
                sessionStorage.removeItem("userName");
                sessionStorage.removeItem("userId");
                sessionStorage.removeItem("coId");
                window.location.href = "/";
            }).fail(function (err) {
                alert("Error! " + err.responseText);
            });

        }),

        changePassword: (function (oldPassword, newPassword, confirmPassword) {

            $.ajax({
                type: 'POST',
                url: '/api/account/ChangePassword',
                headers: accountHandler.getAccountHeader(),
                data: { oldPassword: oldPassword, newPassword: newPassword, confirmPassword: confirmPassword }
            }).done(function (data) {
                alert("Password changed.");
            }).fail(function (err) {
                alert(err.responseText);
            });

        })
    };

}());