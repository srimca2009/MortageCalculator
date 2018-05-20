
function MainCtrl($http, $scope, authService, $location, $rootScope, $state, httpService) {

    
};




function indexCtrl($scope, DTOptionsBuilder, httpService) {

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

    $scope.getAllLoans = function () {

        var promiseGet = httpService.get(getAllLoansUrl, null);

        promiseGet.then(function (result) {
            
            $scope.loans = result.data;
        }, function (err) {
            Console.log(err);
        });
    }

    $scope.getAllLoans();
}

function calcCtrl($scope, httpService) {

    $scope.Calculations = function () {
        if ($scope.calc_form.$valid) {
            var promisePost = httpService.post(calculationUrl, $scope.loan);
            promisePost.then(function (result) {
                $scope.MortgageLoan = result.data;
            }, function (err) {
                $scope.loading = false;
            });
        } else {
            $scope.calc_form.submitted = true;
            $scope.loading = false;
        }
    }


    $scope.getInterest = function () {
        var total = 0;
        if ($scope.MortgageLoan!=undefined) {
            for (var i = 0; i < $scope.MortgageLoan.length; i++) {
                total += $scope.MortgageLoan[i].InterestAmount;
            }
        }
        return total;
    }
}

angular
    .module('app')
    .controller('MainCtrl', MainCtrl)
    .controller('indexCtrl', indexCtrl)
    .controller('calcCtrl', calcCtrl);

