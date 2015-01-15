var getAccountHeader = function() {

    var token = sessionStorage.getItem("userToken");
    var headers = {};
    if (token) {
        headers.Authorization = 'Bearer ' + token;
    }

    return headers;
};

var getUserInfo = function() {

    $.ajax({
        type: 'GET',
        url: '/api/account/userinfo',
        headers: getAccountHeader()
    }).done(function(data) {
        sessionStorage.setItem("userName", data.UserName);
    }).fail(function(err) {
        alert("Error! " + err.responseText);
    });

};

var authenticateUser = function(userName, pass, returnUrl) {

    var loginData = {
        grant_type: 'password',
        username: userName,
        password: pass
    };

    $.ajax({
        type: 'POST',
        url: '/Token',
        data: loginData
    }).done(function (data) {
        // Cache the access token in session storage
        sessionStorage.setItem("userToken", data.access_token);
        // cache user info
        getUserInfo();
        window.location.href = returnUrl;
    }).fail(function (err) {
        alert("Error! " + err.responseText);
    });

}; 
