///**
// * Cobros ERP 
// *
// *
// *
// * Functions (controllers) 
// *  - signupcontroller
// *  - logincontroller
// *  - forgotpasswordcontroller
// *  - resetpasswordcontroller
// *  - companycontroller
// */

function signupcontroller($http, $scope, $location, $timeout, authService, notificationService) {

    $scope.savedSuccessfully = false;
    $scope.message = "";

    $scope.registration = {
        //Email: "admin@admin.com",
        //Password: "Admin@123",
        //ConfirmPassword: "Admin@123"
    };

    $scope.signUp = function () {
        $scope.loading = true;
        if ($scope.signup_form.$valid) {
            var promisePost = authService.saveRegistration($scope.registration);

            promisePost.then(function (result) {
                $scope.savedSuccessfully = true;
                $scope.message = "User has been registered successfully, you will be redicted to login page in 2 seconds.";
                startTimer();

            }, function (err) {
                var errors = [];
                for (var key in err.data.ModelState) {
                    for (var i = 0; i < err.data.ModelState[key].length; i++) {
                        errors.push(err.data.ModelState[key][i]);
                    }
                }
                notificationService.showmessage("Sign up error", "Failed to register user due to:" + errors.join(' '), "error");
                $scope.loading = false;
            });
        } else {
            $scope.signup_form.submitted = true;
            $scope.loading = false;
        }
    };

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/login');
        }, 2000);
    }
}

function logincontroller($http, $scope, $location, $timeout, authService, $state, notificationService, httpService) {

    $scope.loginData = {
        username: 'srimca2009@gmail.com',
        password: 'Cobros@123'
    };

    $scope.message = "";

    $scope.login = function () {

        $scope.loading = true;
      
        if ($scope.login_form.$valid) {
         
            var promisePost = authService.login($scope.loginData);

            promisePost.then(function (resp) {
                $scope.userName = resp.userName;
                //Store the token information in the SessionStorage
                //So that it can be accessed for other views
                sessionStorage.setItem('userName', resp.userName);
                sessionStorage.setItem('accessToken', resp.access_token);
                sessionStorage.setItem('refreshToken', resp.refresh_token);
             
                if (sessionStorage.getItem('accessToken') != null) {
                    $scope.getCompany();
                    $state.go("dashboards.dashboard_2");
                }
                else {
                    notificationService.showmessage("Login error", "The user name and password is incorrect", "error");
                }

            }, function (err) {
                $scope.loading = false;
                notificationService.showmessage("Login error", err.error_description, "error");
            });

        } else {
            $scope.login_form.submitted = true;
        }

       
    };

    $scope.getCompany = function () {
        var promisePost = httpService.post(getCompanyUrl, null);

        promisePost.then(function (result) {
            sessionStorage.setItem('companyId', result.data);
            if (result.data == 0) {
                $state.go("company.details.general");
            }
        }, function (err) {
            notificationService.showmessage("Get company error", "", "error");
        });
    }
}

function forgotpasswordcontroller($http, $scope, httpService, toaster, notificationService, $state) {

    $scope.forgot = {
        Email: ''
    }
    $scope.resetpassword = function () {

        if ($scope.forgot_form.$valid) {

            var promisePost = httpService.post(forgotUrl, $scope.forgot);

            promisePost.then(function (resp) {

                notificationService.showmessage("Reset Password", "Password reset link is sent to your mail.", "success");

            }, function (err) {
                notificationService.showmessage("Sign up error", "This email does not exist", "error");
            });
        }
        else {
            $scope.forgot_form.submitted = true;
        }

    }

}

function resetpasswordcontroller($http, $scope, httpService, toaster, $timeout, notificationService, $state, $location) {

    $scope.resetPassword = {
        Email: "",
        Password: "",
        ConfirmPassword: "",
        code: ""
    }

    var parseLocation = function (location) {
        var pairs = location.substring(1).split("&");
        var obj = {};
        var pair;
        var i;

        for (i in pairs) {
            if (pairs[i] === "") continue;

            pair = pairs[i].split("=");
            obj[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1]);
        }

        return obj;
    };

    $scope.resetPassword.Code = $location.search().code;
   
    $scope.reset = function () {

        if ($scope.reset_form.$valid) {
            var promisePost = httpService.post(resetPasswordUrl, $scope.resetPassword);
            promisePost.then(function (result) {
                startTimer();

            }, function (err) {
                var errors = [];
                for (var key in err.data.ModelState) {
                    for (var i = 0; i < err.data.ModelState[key].length; i++) {
                        errors.push(err.data.ModelState[key][i]);
                    }
                }
                notificationService.showmessage("Reset Password error", "Failed to register user due to:" + errors.join(' '), "error");
            });
        } else {
            $scope.reset_form.submitted = true;
        }

    }

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/login');
        }, 2000);
    }

}

function companycontroller($scope, DTOptionsBuilder, httpService) {
    debugger;
    //var promisePost = httpService.post(companyIndexUrl, null);
    var promisePost = httpService.post(getCompanyUrl, null);
    getCompanyUrl

    promisePost.then(function (result) {
        debugger;
    }, function (err) {
        debugger;
    });


    $scope.dtOptions = DTOptionsBuilder.newOptions()
       .withDOM('<"html5buttons"B>Tfgitlp')
       .withButtons([
           { extend: 'copy' },
           { extend: 'csv' },
           { extend: 'excel', title: 'ExampleFile' },
           { extend: 'pdf', title: 'ExampleFile' },
           {
               extend: 'print',
               customize: function (win) {
                   $(win.document.body).addClass('white-bg');
                   $(win.document.body).css('font-size', '10px');

                   $(win.document.body).find('table')
                       .addClass('compact')
                       .css('font-size', 'inherit');
               }
           }
       ]);

    /**
     * persons - Data used in Tables view for Data Tables plugin
     */
    $scope.persons = [
        {
            id: '1',
            firstName: 'Sritharan',
            lastName: 'M'
        },
        {
            id: '2',
            firstName: 'Sandra',
            lastName: 'Jackson'
        },
        {
            id: '3',
            firstName: 'John',
            lastName: 'Underwood'
        },
        {
            id: '4',
            firstName: 'Chris',
            lastName: 'Johnatan'
        },
        {
            id: '5',
            firstName: 'Kim',
            lastName: 'Rosowski'
        }
    ];
}


angular
    .module('app')
    .controller('signupcontroller', signupcontroller)
    .controller('logincontroller', logincontroller)
    .controller('forgotpasswordcontroller', forgotpasswordcontroller)
    .controller('resetpasswordcontroller', resetpasswordcontroller)
    .controller('companycontroller', companycontroller);
